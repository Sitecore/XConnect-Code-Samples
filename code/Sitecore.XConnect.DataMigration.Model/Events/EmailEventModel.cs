// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using System;

namespace Sitecore.XConnect.DataMigration.Source.Model.Events
{
    [Serializable]
    public class EmailEventModel : EventModel
    {
        public Guid MessageId { get; set; }

        public Guid InstanceId { get; set; }

        public Guid ManagerRootId { get; set; }

        public int EmailAddressHistoryEntryId { get; set; }

        public string MessageLanguage { get; set; }

        public byte? TestValueIndex { get; set; }
    }
}
