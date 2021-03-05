// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using Sitecore.EmailCampaign.Model.XConnect.Events;
using Sitecore.XConnect.DataMigration.Source.Model.Events;

namespace Sitecore.XConnect.DataMigration.Target.Mapper.EventModels
{
    public class EmailOpenedEventModelMapper : BaseEmailEventModelMapper<EmailOpenedEventModel, EmailOpenedEvent>
    {
        protected override EmailOpenedEvent CreateEmailEvent(EmailOpenedEventModel sourceEvent)
        {
            return new EmailOpenedEvent(sourceEvent.Timestamp);
        }
    }
}
