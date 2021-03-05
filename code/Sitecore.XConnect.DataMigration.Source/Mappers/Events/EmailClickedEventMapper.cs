// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using Sitecore.EmailCampaign.Model.XConnect.Events;
using Sitecore.XConnect.DataMigration.Source.Model.Events;

namespace Sitecore.XConnect.DataMigration.Source.Mappers.Events
{
    public class EmailClickedEventMapper : BaseEmailEventMapper<EmailClickedEvent, EmailClickedEventModel>
    {
        protected override EmailClickedEventModel CreateEmailModel(EmailClickedEvent sourceEvent)
        {
            return new EmailClickedEventModel
            {
                Url = sourceEvent.Url
            };
        }
    }
}
