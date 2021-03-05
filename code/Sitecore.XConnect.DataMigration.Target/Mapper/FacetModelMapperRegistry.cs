// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using System;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.XConnect.DataMigration.Source.Model.Facets;
using Sitecore.XConnect.DataMigration.Target.Mapper.FacetModels;

namespace Sitecore.XConnect.DataMigration.Target.Mapper
{
    internal class FacetModelMapperRegistry
    {
        private readonly IServiceProvider _serviceProvider;

        public FacetModelMapperRegistry()
        {
            _serviceProvider = new ServiceCollection()
                .AddSingleton<IFacetModelMapper<ConsentInformationFacetModel>, ConsentInformationFacetModelMapper>()
                .AddSingleton<IFacetModelMapper<EmailAddressHistoryFacetModel>, EmailAddressHistoryFacetModelMapper>()
                .BuildServiceProvider();
        }

        public IFacetModelMapper GetMapper(Type eventType)
        {
            Type facetModelMapperType = typeof(IFacetModelMapper<>).MakeGenericType(eventType);
            return _serviceProvider.GetService(facetModelMapperType) as IFacetModelMapper;
        }
    }
}
