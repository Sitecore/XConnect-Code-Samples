// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using System;

namespace Sitecore.XConnect.DataMigration.Source.Model.Events
{
    [Serializable]
    public class DispatchFailedEventModel : EmailEventModel
    {
        public string FailureReason { get; set; }
    }
}
