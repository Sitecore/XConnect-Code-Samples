// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using Sitecore.EmailCampaign.Model.XConnect.Events;
using Sitecore.XConnect.DataMigration.Source.Model.Events;

namespace Sitecore.XConnect.DataMigration.Target.Mapper.EventModels
{
    public class UnsubscribedFromEmailEventModelMapper : BaseEmailEventModelMapper<UnsubscribedFromEmailEventModel, UnsubscribedFromEmailEvent>
    {
        protected override UnsubscribedFromEmailEvent CreateEmailEvent(UnsubscribedFromEmailEventModel sourceEvent)
        {
            return new UnsubscribedFromEmailEvent(sourceEvent.Timestamp);
        }
    }
}
