// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using Sitecore.XConnect.Collection.Model;
using Sitecore.XConnect.DataMigration.Source.Model.Facets;

namespace Sitecore.XConnect.DataMigration.Source.Mappers.Facets
{
    public class ConsentInformationFacetMapper : BaseFacetMapper<ConsentInformation, ConsentInformationFacetModel>
    {
        protected override ConsentInformationFacetModel CreateModel(ConsentInformation sourceFacet)
        {
            return new ConsentInformationFacetModel
            {
                ConsentRevoked = sourceFacet.ConsentRevoked,
                DoNotMarket = sourceFacet.DoNotMarket,
                ExecutedRightToBeForgotten = sourceFacet.ExecutedRightToBeForgotten
            };
        }
    }
}
