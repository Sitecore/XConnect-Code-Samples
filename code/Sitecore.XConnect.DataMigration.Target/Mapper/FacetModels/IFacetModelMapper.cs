// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using Sitecore.XConnect.DataMigration.Source.Model.Facets;

namespace Sitecore.XConnect.DataMigration.Target.Mapper.FacetModels
{
    internal interface IFacetModelMapper
    {
        Facet Map(FacetModel sourceFacetModel);
    }

    internal interface IFacetModelMapper<in TFacetModel> : IFacetModelMapper
        where TFacetModel : FacetModel
    {
        Facet Map(TFacetModel sourceFacet);
    }
}
