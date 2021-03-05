// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using Sitecore.XConnect.DataMigration.Source.Model.Facets;

namespace Sitecore.XConnect.DataMigration.Source.Mappers.Facets
{
    public abstract class BaseFacetMapper<TFacet, TFacetModel> : IFacetMapper<TFacet>
        where TFacet : Facet
        where TFacetModel : FacetModel
    {
        public FacetModel Map(TFacet sourceFacet)
        {
            return CreateModel(sourceFacet);
        }

        public FacetModel Map(Facet sourceFacet)
        {
            return Map((TFacet)sourceFacet);
        }

        protected abstract TFacetModel CreateModel(TFacet sourceFacet);
    }
}
