// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using System.Collections.Generic;
using Sitecore.XConnect.DataMigration.Source.Model.Events;

namespace Sitecore.XConnect.DataMigration.Target.Mapper.EventModels
{
    public abstract class BaseEventModelMapper<TEventModel, TEvent> : IEventModelMapper<TEventModel>
        where TEvent : Event
        where TEventModel : EventModel
    {
        public Event Map(TEventModel sourceEvent)
        {
            var targetEvent = CreateEvent(sourceEvent);
            AddCommonData(sourceEvent, targetEvent);

            return targetEvent;
        }

        public Event Map(EventModel sourceEvent)
        {
            return Map((TEventModel)sourceEvent);
        }

        protected abstract TEvent CreateEvent(TEventModel sourceEvent);

        protected void AddCommonData(TEventModel sourceModelEvent, TEvent targetEvent)
        {
            foreach (KeyValuePair<string, string> customValue in sourceModelEvent.CustomValues)
            {
                targetEvent.CustomValues.Add(customValue.Key, customValue.Value);
            }

            targetEvent.Data = sourceModelEvent.Data;
            targetEvent.DataKey = sourceModelEvent.DataKey;
            targetEvent.ItemId = sourceModelEvent.ItemId;
            targetEvent.EngagementValue = sourceModelEvent.EngagementValue;
            targetEvent.Id = sourceModelEvent.Id;
            targetEvent.ParentEventId = sourceModelEvent.ParentEventId;
            targetEvent.Text = sourceModelEvent.Text;
            targetEvent.Duration = sourceModelEvent.Duration;
        }
    }
}
