// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using Sitecore.XConnect.DataMigration.Source.Model.Facets;

namespace Sitecore.XConnect.DataMigration.Target.Mapper.FacetModels
{
    public abstract class BaseFacetModelMapper<TFacetModel, TFacet> : IFacetModelMapper<TFacetModel>
        where TFacet : Facet
        where TFacetModel : FacetModel
    {
        public Facet Map(TFacetModel sourceEvent)
        {
            return CreateFacet(sourceEvent);
        }

        public Facet Map(FacetModel sourceFacet)
        {
            return Map((TFacetModel)sourceFacet);
        }

        protected abstract TFacet CreateFacet(TFacetModel sourceFacet);
    }
}
