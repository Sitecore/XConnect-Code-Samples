// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using System;
using System.Collections.Generic;

namespace Sitecore.XConnect.DataMigration.Source.Model.Events
{
    [Serializable]
    public class MVTestTriggeredModel : EventModel
    {
        public byte[] Combination { get; set; }

        public List<Guid> EligibleRules { get; set; }

        public DateTime? ExposureTime { get; set; }

        public bool? FirstExposure { get; set; }

        public bool IsSuspended { get; set; }

        public int ValueAtExposure { get; set; }

        public int ContactVisitIndex { get; set; }

        public string DeviceType { get; set; }
    }
}
