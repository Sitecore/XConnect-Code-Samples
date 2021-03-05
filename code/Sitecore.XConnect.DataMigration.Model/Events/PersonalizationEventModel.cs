// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using System;
using System.Collections.Generic;

namespace Sitecore.XConnect.DataMigration.Source.Model.Events
{
    [Serializable]
    public class PersonalizationEventModel : EventModel
    {
        public List<PersonalizationRuleDataModel> ExposedRules { get; set; }

        public byte[] Combination { get; set; }
    }

    [Serializable]
    public class PersonalizationRuleDataModel
    {
        public Guid RuleId { get; set; }

        public Guid RuleSetId { get; set; }

        public bool IsOriginal { get; set; }
    }
}
