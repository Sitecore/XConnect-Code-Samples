// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using Sitecore.XConnect.DataMigration.Source.Model.Events;

namespace Sitecore.XConnect.DataMigration.Target.Mapper.EventModels
{
    public class OutcomeModelMapper : BaseEventModelMapper<OutcomeModel, Outcome>
    {
        protected override Outcome CreateEvent(OutcomeModel sourceEvent)
        {
            return new Outcome(
                sourceEvent.DefinitionId,
                sourceEvent.Timestamp,
                sourceEvent.CurrencyCode,
                sourceEvent.MonetaryValue);
        }
    }
}
