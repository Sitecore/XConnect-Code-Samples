// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

namespace Sitecore.XConnect.DataMigration.Source.Mappers.Filters.IdentifierFilters
{
    public interface IContactIdentifierFilter
    {
        bool Apply(ContactIdentifier contactIdentifier);
    }
}
