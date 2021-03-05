// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using System;
using System.Collections.Generic;

namespace Sitecore.XConnect.DataMigration.Source.Model.Events
{
    [Serializable]
    public class EventModel
    {
        public Guid Id { get; set; }

        public Dictionary<string, string> CustomValues { get; set; } = new Dictionary<string, string>();

        public string Data { get; set; }

        public string DataKey { get; set; }

        public Guid DefinitionId { get; set; }

        public Guid ItemId { get; set; }

        public int EngagementValue { get; set; }

        public Guid? ParentEventId { get; set; }

        public string Text { get; set; }

        public DateTime Timestamp { get; set; }

        public TimeSpan Duration { get; set; }
    }
}
