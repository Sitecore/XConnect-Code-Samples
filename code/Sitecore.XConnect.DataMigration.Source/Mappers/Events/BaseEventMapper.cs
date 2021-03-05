// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using Sitecore.XConnect.DataMigration.Source.Model.Events;

namespace Sitecore.XConnect.DataMigration.Source.Mappers.Events
{
    public abstract class BaseEventMapper<TEvent, TEventModel> : IEventMapper<TEvent>
        where TEvent : Event
        where TEventModel : EventModel
    {
        public EventModel Map(TEvent sourceEvent)
        {
            var model = CreateModel(sourceEvent);
            AddCommonData(sourceEvent, model);

            return model;
        }

        public EventModel Map(Event sourceEvent)
        {
            return Map((TEvent)sourceEvent);
        }

        protected abstract TEventModel CreateModel(TEvent sourceEvent);

        protected void AddCommonData(TEvent sourceEvent, TEventModel targetModel)
        {
            targetModel.CustomValues = sourceEvent.CustomValues;
            targetModel.Data = sourceEvent.Data;
            targetModel.DataKey = sourceEvent.DataKey;
            targetModel.DefinitionId = sourceEvent.DefinitionId;
            targetModel.ItemId = sourceEvent.ItemId;
            targetModel.EngagementValue = sourceEvent.EngagementValue;
            targetModel.Id = sourceEvent.Id;
            targetModel.ParentEventId = sourceEvent.ParentEventId;
            targetModel.Text = sourceEvent.Text;
            targetModel.Timestamp = sourceEvent.Timestamp;
            targetModel.Duration = sourceEvent.Duration;
        }
    }
}
