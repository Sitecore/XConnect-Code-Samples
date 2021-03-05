// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using Sitecore.XConnect.DataMigration.Source.Model.Facets;

namespace Sitecore.XConnect.DataMigration.Source.Mappers.Facets
{
    internal interface IFacetMapper
    {
        FacetModel Map(Facet sourceFacet);
    }

    internal interface IFacetMapper<in TFacet> : IFacetMapper
        where TFacet : Facet
    {
        FacetModel Map(TFacet sourceEvent);
    }
}
