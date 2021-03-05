// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using System.Collections.Generic;
using Sitecore.ContentTesting.Model.xConnect;
using Sitecore.XConnect.DataMigration.Source.Model.Events;

namespace Sitecore.XConnect.DataMigration.Target.Mapper.EventModels
{
    public class PersonalizationEventModelMapper : BaseEventModelMapper<PersonalizationEventModel, PersonalizationEvent>
    {
        protected override PersonalizationEvent CreateEvent(PersonalizationEventModel sourceEvent)
        {
            var model = new PersonalizationEvent(sourceEvent.Timestamp)
            {
                Combination = sourceEvent.Combination
            };

            if (sourceEvent.ExposedRules != null)
            {
                model.ExposedRules = new List<PersonalizationRuleData>();

                foreach (PersonalizationRuleDataModel rule in sourceEvent.ExposedRules)
                {
                    model.ExposedRules.Add(new PersonalizationRuleData
                    {
                        RuleId = rule.RuleId,
                        RuleSetId = rule.RuleSetId,
                        IsOriginal = rule.IsOriginal
                    });
                }
            }

            return model;
        }
    }
}
