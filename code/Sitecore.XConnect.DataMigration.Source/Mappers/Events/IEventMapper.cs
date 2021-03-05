// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using Sitecore.XConnect.DataMigration.Source.Model.Events;

namespace Sitecore.XConnect.DataMigration.Source.Mappers.Events
{
    internal interface IEventMapper
    {
        EventModel Map(Event sourceEvent);
    }

    internal interface IEventMapper<in TEvent> : IEventMapper
        where TEvent : Event
    {
        EventModel Map(TEvent sourceEvent);
    }
}
