// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using Sitecore.XConnect.DataMigration.Source.Model.Events;

namespace Sitecore.XConnect.DataMigration.Source.Mappers.Events
{
    public class GoalMapper : BaseEventMapper<Goal, GoalModel>
    {
        protected override GoalModel CreateModel(Goal sourceEvent)
        {
            return new GoalModel();
        }
    }
}
