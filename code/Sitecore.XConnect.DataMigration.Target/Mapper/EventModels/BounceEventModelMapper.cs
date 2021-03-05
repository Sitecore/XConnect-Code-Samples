// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using Sitecore.EmailCampaign.Model.XConnect.Events;
using Sitecore.XConnect.DataMigration.Source.Model.Events;

namespace Sitecore.XConnect.DataMigration.Target.Mapper.EventModels
{
    public class BounceEventModelMapper : BaseEmailEventModelMapper<BounceEventModel, BounceEvent>
    {
        protected override BounceEvent CreateEmailEvent(BounceEventModel sourceEvent)
        {
            return new BounceEvent(sourceEvent.Timestamp)
            {
                BounceType = sourceEvent.BounceType,
                BounceReason = sourceEvent.BounceReason
            };
        }
    }
}
