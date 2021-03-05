// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using Sitecore.XConnect.DataMigration.Source.Model.Events;

namespace Sitecore.XConnect.DataMigration.Target.Mapper.EventModels
{
    public class EventModelMapper : BaseEventModelMapper<EventModel, Event>
    {
        protected override Event CreateEvent(EventModel sourceEvent)
        {
            return new Event(sourceEvent.DefinitionId, sourceEvent.Timestamp);
        }
    }
}
