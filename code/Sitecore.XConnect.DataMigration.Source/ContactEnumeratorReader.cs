// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sitecore.XConnect.DataMigration.Source.Mappers;
using Sitecore.XConnect.DataMigration.Source.Model;

namespace Sitecore.XConnect.DataMigration.Source
{
    internal class ContactEnumeratorReader
    {
        private readonly int _batchSize;
        private readonly string[] _contactFacetKeys;
        private readonly string[] _interactionFacetKeys;

        private readonly EntityMapper _mapper;

        public ContactEnumeratorReader(int batchSize, string[] contactFacetKeys, string[] interactionFacetKeys)
        {
            _batchSize = batchSize;
            _contactFacetKeys = contactFacetKeys;
            _interactionFacetKeys = interactionFacetKeys;

            _mapper = new EntityMapper();
        }

        public async Task<ContactCursorResult> ReadBatch(IReadOnlyXdbContext client)
        {
            IAsyncEntityBatchEnumerator<Contact> contactCursor = await CreateEnumerator(client).ConfigureAwait(false);

            return await MoveNext(contactCursor).ConfigureAwait(false);
        }

        public async Task<ContactCursorResult> ReadBatch(IReadOnlyXdbContext client, byte[] bookmark)
        {
            IAsyncEntityBatchEnumerator<Contact> contactCursor = await CreateEnumerator(client, bookmark).ConfigureAwait(false);

            return await MoveNext(contactCursor).ConfigureAwait(false);
        }

        private async Task<ContactCursorResult> MoveNext(IAsyncEntityBatchEnumerator<Contact> contactCursor)
        {
            if (contactCursor == null)
            {
                throw new ArgumentNullException("Contact cursor is null");
            }

            var hasMore = await contactCursor.MoveNext().ConfigureAwait(false);

            if (!hasMore)
            {
                return new ContactCursorResult();
            }

            IReadOnlyCollection<Contact> contacts = contactCursor.Current;

            if (contacts == null)
            {
                return new ContactCursorResult();
            }

            IReadOnlyCollection<ContactModel> result = _mapper.ToContactProxyModel(contacts);

            return new ContactCursorResult(result, contactCursor.GetBookmark(), hasMore);
        }

        private async Task<IAsyncEntityBatchEnumerator<Contact>> CreateEnumerator(IReadOnlyXdbContext client)
        {
            return await client.CreateContactEnumerator(
                new ContactExpandOptions(_contactFacetKeys)
                {
                    Interactions = new RelatedInteractionsExpandOptions(_interactionFacetKeys)
                    {
                        // Get all interactions
                        EndDateTime = DateTime.MaxValue,
                        StartDateTime = DateTime.MinValue
                    }
                },
                _batchSize)
                .ConfigureAwait(false);
        }

        private async Task<IAsyncEntityBatchEnumerator<Contact>> CreateEnumerator(IReadOnlyXdbContext client, byte[] bookmark)
        {
            return await client.CreateContactEnumerator(
                bookmark,
                new ContactExpandOptions(_contactFacetKeys)
                {
                    Interactions = new RelatedInteractionsExpandOptions(_interactionFacetKeys)
                    {
                        // Get all interactions
                        EndDateTime = DateTime.MaxValue,
                        StartDateTime = DateTime.MinValue
                    }
                },
                _batchSize)
                .ConfigureAwait(false);
        }
    }
}
