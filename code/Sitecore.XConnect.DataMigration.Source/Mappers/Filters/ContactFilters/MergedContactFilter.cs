// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using Sitecore.XConnect.Collection.Model;

namespace Sitecore.XConnect.DataMigration.Source.Mappers.Filters.ContactFilters
{
    public class MergedContactFilter : IContactFilter
    {
        public bool Apply(Contact contact)
        {
            return contact.Facets.ContainsKey(MergeInfo.DefaultFacetKey);
        }
    }
}
