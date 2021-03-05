// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using Sitecore.XConnect.Collection.Model;

namespace Sitecore.XConnect.DataMigration.Source.Mappers.Filters.ContactFilters
{
    public class ForgottenContactFilter : IContactFilter
    {
        public bool Apply(Contact contact)
        {
            if (contact.Facets.TryGetValue(ConsentInformation.DefaultFacetKey, out Facet facet))
            {
                if ((facet as ConsentInformation).ExecutedRightToBeForgotten)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
