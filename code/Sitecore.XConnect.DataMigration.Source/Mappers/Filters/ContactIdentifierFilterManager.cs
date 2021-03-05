// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using System.Collections.Generic;
using System.Linq;
using Sitecore.XConnect.DataMigration.Source.Mappers.Filters.IdentifierFilters;

namespace Sitecore.XConnect.DataMigration.Source.Mappers.Filters
{
    public class ContactIdentifierFilterManager : IContactIdentifierFilterManager
    {
        private readonly List<IContactIdentifierFilter> _contactIdentifiersFilters;

        public ContactIdentifierFilterManager()
        {
            _contactIdentifiersFilters = new List<IContactIdentifierFilter>
            {
                new MergeIdentifierFilter()
            };
        }

        public bool Apply(ContactIdentifier contactIdentifier)
        {
            return _contactIdentifiersFilters.Any(filter => filter.Apply(contactIdentifier));
        }
    }
}
