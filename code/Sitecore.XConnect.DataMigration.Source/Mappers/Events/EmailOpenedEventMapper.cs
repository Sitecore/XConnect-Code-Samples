﻿// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using Sitecore.EmailCampaign.Model.XConnect.Events;
using Sitecore.XConnect.DataMigration.Source.Model.Events;

namespace Sitecore.XConnect.DataMigration.Source.Mappers.Events
{
    public class EmailOpenedEventMapper : BaseEmailEventMapper<EmailOpenedEvent, EmailOpenedEventModel>
    {
        protected override EmailOpenedEventModel CreateEmailModel(EmailOpenedEvent sourceEvent)
        {
            return new EmailOpenedEventModel();
        }
    }
}
