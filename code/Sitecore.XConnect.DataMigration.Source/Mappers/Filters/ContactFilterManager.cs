// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using System.Collections.Generic;
using System.Linq;
using Sitecore.XConnect.DataMigration.Source.Mappers.Filters.ContactFilters;

namespace Sitecore.XConnect.DataMigration.Source.Mappers.Filters
{
    public class ContactFilterManager : IContactFilterManager
    {
        private readonly List<IContactFilter> _contactFilters;

        public ContactFilterManager()
        {
            _contactFilters = new List<IContactFilter>
            {
                new AnonymousContactFilter(),
                new ForgottenContactFilter(),
                new MergedContactFilter()
            };
        }

        public bool Apply(Contact contact)
        {
            return _contactFilters.Any(filter => filter.Apply(contact));
        }
    }
}
