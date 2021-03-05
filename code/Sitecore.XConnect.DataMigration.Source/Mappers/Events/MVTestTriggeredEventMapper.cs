// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using Sitecore.ContentTesting.Model.xConnect;
using Sitecore.XConnect.DataMigration.Source.Model.Events;

namespace Sitecore.XConnect.DataMigration.Source.Mappers.Events
{
    public class MVTestTriggeredMapper : BaseEventMapper<MVTestTriggered, MVTestTriggeredModel>
    {
        protected override MVTestTriggeredModel CreateModel(MVTestTriggered sourceEvent)
        {
            return new MVTestTriggeredModel
            {
                Combination = sourceEvent.Combination,
                EligibleRules = sourceEvent.EligibleRules,
                ExposureTime = sourceEvent.ExposureTime,
                FirstExposure = sourceEvent.FirstExposure,
                IsSuspended = sourceEvent.IsSuspended,
                ValueAtExposure = sourceEvent.ValueAtExposure,
                ContactVisitIndex = sourceEvent.ContactVisitIndex,
                DeviceType = sourceEvent.DeviceType
            };
        }
    }
}
