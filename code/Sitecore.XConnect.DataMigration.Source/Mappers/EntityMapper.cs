// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using System;
using System.Collections.Generic;
using Serilog;
using Sitecore.XConnect.DataMigration.Source.Mappers.Filters;
using Sitecore.XConnect.DataMigration.Source.Model;

namespace Sitecore.XConnect.DataMigration.Source.Mappers
{
    internal class EntityMapper : MarshalByRefObject
    {
        private readonly IContactFilterManager _contactFilterManager;
        private readonly IContactIdentifierFilterManager _contactIdentifierFilterManager;

        private readonly FacetMapperRegistry _facetMapperRegistry;
        private readonly EventMapperRegistry _eventMapperRegistry;

        public EntityMapper()
        {
            _contactFilterManager = new ContactFilterManager();
            _contactIdentifierFilterManager = new ContactIdentifierFilterManager();
            _facetMapperRegistry = new FacetMapperRegistry();
            _eventMapperRegistry = new EventMapperRegistry();
        }

        public IReadOnlyCollection<ContactModel> ToContactProxyModel(IReadOnlyCollection<Contact> contacts)
        {
            var result = new List<ContactModel>();

            foreach (var contact in contacts)
            {
                // Skip mapping for the contact if one of the defined filters is applied
                if (_contactFilterManager.Apply(contact))
                {
                    Log.Verbose("The contact {0} migration was skipped by one of the filters.", contact.Id);
                    continue;
                }

                var contactProxyModel = new ContactModel();

                MapContactFacets(contact, contactProxyModel);

                MapIdentifiers(contact, contactProxyModel);

                MapInteractions(contact, contactProxyModel);

                result.Add(contactProxyModel);
            }

            return result;
        }

        private void MapContactFacets(Contact contact, ContactModel contactProxyModel)
        {
            foreach (var facet in contact.Facets)
            {
                var facetMapper = _facetMapperRegistry.GetMapper(facet.Value.GetType());

                // If facet mapper is registered for the facet it will be used for explicit proxy mapping,
                // otherwise, facet will be serialized as is.
                // The facet mapper is required if you have any changes between the XConnect source and target facet models.
                if (facetMapper != null)
                {
                    var facetModel = facetMapper.Map(facet.Value);

                    contactProxyModel.FacetModel.Add(facet.Key, facetModel);
                }
                else
                {
                    contactProxyModel.Facets.Add(facet.Key, facet.Value.WithClearedConcurrency());
                }
            }
        }

        private void MapIdentifiers(Contact contact, ContactModel contactProxyModel)
        {
            foreach (var identifier in contact.Identifiers)
            {
                // Skip mapping for the contact identifier if one of the defined filters is applied
                if (_contactIdentifierFilterManager.Apply(identifier))
                {
                    Log.Verbose("The contact identifier {0}:{1} migration was skipped by the filter", identifier.Source, identifier.Identifier);
                    continue;
                }

                var identifierSource = identifier.Source == Constants.AliasIdentifierSource
                    ? MigrationConstants.AliasOldSourceIdentifier
                    : identifier.Source;

                var contactIdentifierProxy = new ContactIdentifierModel
                {
                    Identifier = identifier.Identifier,
                    Source = identifierSource,
                    IdentifierType = identifier.IdentifierType
                };

                contactProxyModel.Identifiers.Add(contactIdentifierProxy);
            }
        }

        private void MapInteractions(Contact contact, ContactModel contactProxyModel)
        {
            foreach (var interaction in contact.Interactions)
            {
                var interactionProxy = new InteractionModel
                {
                    CampaignId = interaction.CampaignId,
                    ChannelId = interaction.ChannelId,
                    //DeviceProfile = interaction.DeviceProfile, // As far as DeviceProfiles are not migrated they can be null
                    EngagementValue = interaction.EngagementValue,
                    StartDateTime = interaction.StartDateTime,
                    EndDateTime = interaction.EndDateTime,
                    Initiator = interaction.Initiator,
                    UserAgent = interaction.UserAgent,
                    VenueId = interaction.VenueId
                };

                MapEvents(interaction, interactionProxy);

                foreach (var facet in interaction.Facets)
                {
                    interactionProxy.Facets.Add(facet.Key, facet.Value.WithClearedConcurrency());
                }

                contactProxyModel.Interactions.Add(interactionProxy);
            }
        }

        private void MapEvents(Interaction interaction, InteractionModel interactionProxy)
        {
            foreach (var sourceEvent in interaction.Events)
            {
                var eventMapper = _eventMapperRegistry.GetMapper(sourceEvent.GetType());

                if (eventMapper == null)
                {
                    throw new Exception(@"Event mapper for the event type {sourceEvent.GetType().ToString()} is not registered.");
                }

                var eventModel = eventMapper.Map(sourceEvent);

                interactionProxy.Events.Add(eventModel);
            }
        }
    }
}
