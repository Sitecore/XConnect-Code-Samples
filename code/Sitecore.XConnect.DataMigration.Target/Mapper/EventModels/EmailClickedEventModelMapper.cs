// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using Sitecore.EmailCampaign.Model.XConnect.Events;
using Sitecore.XConnect.DataMigration.Source.Model.Events;

namespace Sitecore.XConnect.DataMigration.Target.Mapper.EventModels
{
    public class EmailClickedEventModelMapper : BaseEmailEventModelMapper<EmailClickedEventModel, EmailClickedEvent>
    {
        protected override EmailClickedEvent CreateEmailEvent(EmailClickedEventModel sourceEvent)
        {
            return new EmailClickedEvent(sourceEvent.Timestamp)
            {
                Url = sourceEvent.Url
            };
        }
    }
}
