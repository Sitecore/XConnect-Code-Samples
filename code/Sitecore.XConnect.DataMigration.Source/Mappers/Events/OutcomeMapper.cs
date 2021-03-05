// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using Sitecore.XConnect.DataMigration.Source.Model.Events;

namespace Sitecore.XConnect.DataMigration.Source.Mappers.Events
{
    public class OutcomeMapper : BaseEventMapper<Outcome, OutcomeModel>
    {
        protected override OutcomeModel CreateModel(Outcome sourceEvent)
        {
            return new OutcomeModel
            {
                CurrencyCode = sourceEvent.CurrencyCode,
                MonetaryValue = sourceEvent.MonetaryValue
            };
        }
    }
}
