// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using System;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.XConnect.DataMigration.Source.Model.Events;
using Sitecore.XConnect.DataMigration.Target.Mapper.EventModels;

namespace Sitecore.XConnect.DataMigration.Target.Mapper
{
    internal class EventModelMapperRegistry
    {
        private readonly IServiceProvider _serviceProvider;

        public EventModelMapperRegistry()
        {
            _serviceProvider = new ServiceCollection()
                .AddSingleton<IEventModelMapper<EventModel>, EventModelMapper>()
                .AddSingleton<IEventModelMapper<GoalModel>, GoalModelMapper>()
                .AddSingleton<IEventModelMapper<OutcomeModel>, OutcomeModelMapper>()
                .AddSingleton<IEventModelMapper<CampaignEventModel>, CampaignEventModelMapper>()
                .AddSingleton<IEventModelMapper<PageViewEventModel>, PageViewEventModelMapper>()
                .AddSingleton<IEventModelMapper<DownloadEventModel>, DownloadEventModelMapper>()
                .AddSingleton<IEventModelMapper<SearchEventModel>, SearchEventModelMapper>()
                .AddSingleton<IEventModelMapper<MVTestTriggeredModel>, MVTestTriggeredModelMapper>()
                .AddSingleton<IEventModelMapper<PersonalizationEventModel>, PersonalizationEventModelMapper>()
                .AddSingleton<IEventModelMapper<BounceEventModel>, BounceEventModelMapper>()
                .AddSingleton<IEventModelMapper<DispatchFailedEventModel>, DispatchFailedEventModelMapper>()
                .AddSingleton<IEventModelMapper<EmailClickedEventModel>, EmailClickedEventModelMapper>()
                .AddSingleton<IEventModelMapper<EmailOpenedEventModel>, EmailOpenedEventModelMapper>()
                .AddSingleton<IEventModelMapper<EmailSentEventModel>, EmailSentEventModelMapper>()
                .AddSingleton<IEventModelMapper<SpamComplaintEventModel>, SpamComplaintEventModelMapper>()
                .AddSingleton<IEventModelMapper<UnsubscribedFromEmailEventModel>, UnsubscribedFromEmailEventModelMapper>()
                .AddSingleton<IEventModelMapper<EmailEventModel>, EmailEventModelMapper>()
                .BuildServiceProvider();
        }

        public IEventModelMapper GetMapper(Type eventType)
        {
            Type eventMapperType = typeof(IEventModelMapper<>).MakeGenericType(eventType);
            return _serviceProvider.GetService(eventMapperType) as IEventModelMapper;
        }
    }
}
