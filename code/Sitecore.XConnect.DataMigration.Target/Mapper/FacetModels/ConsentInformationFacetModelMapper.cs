// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using Sitecore.XConnect.Collection.Model;
using Sitecore.XConnect.DataMigration.Source.Model.Facets;

namespace Sitecore.XConnect.DataMigration.Target.Mapper.FacetModels
{
    public class ConsentInformationFacetModelMapper : BaseFacetModelMapper<ConsentInformationFacetModel, ConsentInformation>
    {
        protected override ConsentInformation CreateFacet(ConsentInformationFacetModel sourceFacet)
        {
            return new ConsentInformation
            {
                ConsentRevoked = sourceFacet.ConsentRevoked, DoNotMarket = sourceFacet.DoNotMarket, ExecutedRightToBeForgotten = sourceFacet.ExecutedRightToBeForgotten
            };
        }
    }
}
