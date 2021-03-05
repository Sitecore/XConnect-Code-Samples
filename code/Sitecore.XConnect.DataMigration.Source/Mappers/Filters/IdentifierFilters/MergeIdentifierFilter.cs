// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

namespace Sitecore.XConnect.DataMigration.Source.Mappers.Filters.IdentifierFilters
{
    public class MergeIdentifierFilter : IContactIdentifierFilter
    {
        public bool Apply(ContactIdentifier contactIdentifier)
        {
            return contactIdentifier.Source == Constants.MergeIdentifierSource;
        }
    }
}
