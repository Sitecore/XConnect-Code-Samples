// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using Sitecore.EmailCampaign.Model.XConnect.Events;
using Sitecore.XConnect.DataMigration.Source.Model.Events;

namespace Sitecore.XConnect.DataMigration.Source.Mappers.Events
{
    public abstract class BaseEmailEventMapper<TEmailEvent, TEmailModel> : BaseEventMapper<TEmailEvent, TEmailModel>
        where TEmailEvent : EmailEvent
        where TEmailModel : EmailEventModel
    {
        protected override TEmailModel CreateModel(TEmailEvent sourceEvent)
        {
            var model = CreateEmailModel(sourceEvent);
            AddCommonEmailEventData(sourceEvent, model);

            return model;
        }

        protected abstract TEmailModel CreateEmailModel(TEmailEvent sourceEvent);

        protected void AddCommonEmailEventData(EmailEvent sourceEvent, EmailEventModel targetModel)
        {
            targetModel.MessageId = sourceEvent.MessageId;
            targetModel.InstanceId = sourceEvent.InstanceId;
            targetModel.ManagerRootId = sourceEvent.ManagerRootId;
            targetModel.EmailAddressHistoryEntryId = sourceEvent.EmailAddressHistoryEntryId;
            targetModel.MessageLanguage = sourceEvent.MessageLanguage;
            targetModel.TestValueIndex = sourceEvent.TestValueIndex;
        }
    }
}
