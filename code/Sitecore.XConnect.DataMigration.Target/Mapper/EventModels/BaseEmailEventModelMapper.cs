// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using Sitecore.EmailCampaign.Model.XConnect.Events;
using Sitecore.XConnect.DataMigration.Source.Model.Events;

namespace Sitecore.XConnect.DataMigration.Target.Mapper.EventModels
{
    public abstract class BaseEmailEventModelMapper<TEmailModel, TEmailEvent> : BaseEventModelMapper<TEmailModel, TEmailEvent>
        where TEmailEvent : EmailEvent
        where TEmailModel : EmailEventModel
    {
        protected override TEmailEvent CreateEvent(TEmailModel sourceEvent)
        {
            var emailEvent = CreateEmailEvent(sourceEvent);
            AddCommonEmailEventData(sourceEvent, emailEvent);

            return emailEvent;
        }

        protected abstract TEmailEvent CreateEmailEvent(TEmailModel sourceEvent);

        protected void AddCommonEmailEventData(EmailEventModel sourceEvent, EmailEvent targetEvent)
        {
            targetEvent.MessageId = sourceEvent.MessageId;
            targetEvent.InstanceId = sourceEvent.InstanceId;
            targetEvent.ManagerRootId = sourceEvent.ManagerRootId;
            targetEvent.EmailAddressHistoryEntryId = sourceEvent.EmailAddressHistoryEntryId;
            targetEvent.MessageLanguage = sourceEvent.MessageLanguage;
            targetEvent.TestValueIndex = sourceEvent.TestValueIndex;
        }
    }
}
