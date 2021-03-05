// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using System.Collections.Generic;
using Sitecore.ContentTesting.Model.xConnect;
using Sitecore.XConnect.DataMigration.Source.Model.Events;

namespace Sitecore.XConnect.DataMigration.Source.Mappers.Events
{
    public class PersonalizationEventMapper : BaseEventMapper<PersonalizationEvent, PersonalizationEventModel>
    {
        protected override PersonalizationEventModel CreateModel(PersonalizationEvent sourceEvent)
        {
            var model = new PersonalizationEventModel
            {
                Combination = sourceEvent.Combination,
            };

            if (sourceEvent.ExposedRules != null)
            {
                model.ExposedRules = new List<PersonalizationRuleDataModel>();

                foreach (PersonalizationRuleData rule in sourceEvent.ExposedRules)
                {
                    model.ExposedRules.Add(
                        new PersonalizationRuleDataModel
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
