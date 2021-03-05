// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using Sitecore.EmailCampaign.Model.XConnect.Events;
using Sitecore.XConnect.DataMigration.Source.Model.Events;

namespace Sitecore.XConnect.DataMigration.Target.Mapper.EventModels
{
    public class DispatchFailedEventModelMapper : BaseEmailEventModelMapper<DispatchFailedEventModel, DispatchFailedEvent>
    {
        protected override DispatchFailedEvent CreateEmailEvent(DispatchFailedEventModel sourceEvent)
        {
            return new DispatchFailedEvent(sourceEvent.Timestamp)
            {
                FailureReason = sourceEvent.FailureReason
            };
        }
    }
}
