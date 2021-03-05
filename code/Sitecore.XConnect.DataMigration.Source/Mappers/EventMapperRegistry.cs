// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using System;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.ContentTesting.Model.xConnect;
using Sitecore.EmailCampaign.Model.XConnect.Events;
using Sitecore.XConnect.Collection.Model;
using Sitecore.XConnect.DataMigration.Source.Mappers.Events;

namespace Sitecore.XConnect.DataMigration.Source.Mappers
{
    internal class EventMapperRegistry
    {
        private readonly IServiceProvider _serviceProvider;

        public EventMapperRegistry()
        {
            _serviceProvider = new ServiceCollection()
                .AddSingleton<IEventMapper<Event>, EventMapper>()
                .AddSingleton<IEventMapper<Goal>, GoalMapper>()
                .AddSingleton<IEventMapper<Outcome>, OutcomeMapper>()
                .AddSingleton<IEventMapper<CampaignEvent>, CampaignEventMapper>()
                .AddSingleton<IEventMapper<PageViewEvent>, PageViewEventMapper>()
                .AddSingleton<IEventMapper<DownloadEvent>, DownloadEventMapper>()
                .AddSingleton<IEventMapper<SearchEvent>, SearchEventMapper>()
                .AddSingleton<IEventMapper<MVTestTriggered>, MVTestTriggeredMapper>()
                .AddSingleton<IEventMapper<PersonalizationEvent>, PersonalizationEventMapper>()
                .AddSingleton<IEventMapper<BounceEvent>, BounceEventMapper>()
                .AddSingleton<IEventMapper<DispatchFailedEvent>, DispatchFailedEventMapper>()
                .AddSingleton<IEventMapper<EmailClickedEvent>, EmailClickedEventMapper>()
                .AddSingleton<IEventMapper<EmailOpenedEvent>, EmailOpenedEventMapper>()
                .AddSingleton<IEventMapper<EmailSentEvent>, EmailSentEventMapper>()
                .AddSingleton<IEventMapper<SpamComplaintEvent>, SpamComplaintEventMapper>()
                .AddSingleton<IEventMapper<UnsubscribedFromEmailEvent>, UnsubscribedFromEmailEventMapper>()
                .AddSingleton<IEventMapper<EmailEvent>, EmailEventMapper>()
                .BuildServiceProvider();
        }

        public IEventMapper GetMapper(Type eventType)
        {
            Type eventMapperType = typeof(IEventMapper<>).MakeGenericType(eventType);
            return _serviceProvider.GetService(eventMapperType) as IEventMapper;
        }
    }
}
