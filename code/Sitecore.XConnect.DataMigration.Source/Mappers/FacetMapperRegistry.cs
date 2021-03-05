// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using System;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.EmailCampaign.Model.XConnect.Facets;
using Sitecore.XConnect.Collection.Model;
using Sitecore.XConnect.DataMigration.Source.Mappers.Facets;

namespace Sitecore.XConnect.DataMigration.Source.Mappers
{
    internal class FacetMapperRegistry
    {
        private readonly IServiceProvider _serviceProvider;

        public FacetMapperRegistry()
        {
            _serviceProvider = new ServiceCollection()
                .AddSingleton<IFacetMapper<ConsentInformation>, ConsentInformationFacetMapper>()
                .AddSingleton<IFacetMapper<EmailAddressHistory>, EmailAddressHistoryFacetMapper>()
                .BuildServiceProvider();
        }

        public IFacetMapper GetMapper(Type eventType)
        {
            Type facetMapperType = typeof(IFacetMapper<>).MakeGenericType(eventType);
            return _serviceProvider.GetService(facetMapperType) as IFacetMapper;
        }
    }
}
