// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using Sitecore.XConnect.Collection.Model;
using Sitecore.XConnect.DataMigration.Source.Model.Events;

namespace Sitecore.XConnect.DataMigration.Source.Mappers.Events
{
    public class CampaignEventMapper : BaseEventMapper<CampaignEvent, CampaignEventModel>
    {
        protected override CampaignEventModel CreateModel(CampaignEvent sourceEvent)
        {
            return new CampaignEventModel
            {
                CampaignDefinitionId = sourceEvent.CampaignDefinitionId
            };
        }
    }
}
