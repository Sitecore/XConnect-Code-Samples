// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using Sitecore.XConnect.DataMigration.Source.Model.Events;

namespace Sitecore.XConnect.DataMigration.Target.Mapper.EventModels
{
    public class GoalModelMapper : BaseEventModelMapper<GoalModel, Goal>
    {
        protected override Goal CreateEvent(GoalModel sourceEvent)
        {
            return new Goal(sourceEvent.DefinitionId, sourceEvent.Timestamp);
        }
    }
}
