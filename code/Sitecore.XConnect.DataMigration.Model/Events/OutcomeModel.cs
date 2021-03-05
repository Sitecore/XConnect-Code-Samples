// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using System;

namespace Sitecore.XConnect.DataMigration.Source.Model.Events
{
    [Serializable]
    public class OutcomeModel : EventModel
    {
        public string CurrencyCode { get; set; }

        public decimal MonetaryValue { get; set; }
    }
}
