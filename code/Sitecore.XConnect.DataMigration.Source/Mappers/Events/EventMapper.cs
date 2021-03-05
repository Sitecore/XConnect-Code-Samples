// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using Sitecore.XConnect.DataMigration.Source.Model.Events;

namespace Sitecore.XConnect.DataMigration.Source.Mappers.Events
{
    public class EventMapper : BaseEventMapper<Event, EventModel>
    {
        protected override EventModel CreateModel(Event sourceEvent)
        {
            return new EventModel();
        }
    }
}
