// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

namespace Sitecore.XConnect.DataMigration.Source.Mappers.Filters.ContactFilters
{
    public class AnonymousContactFilter : IContactFilter
    {
        public bool Apply(Contact contact)
        {
            return !contact.IsKnown;
        }
    }
}
