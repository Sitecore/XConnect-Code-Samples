// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using Sitecore.XConnect.Collection.Model;
using Sitecore.XConnect.DataMigration.Source.Model.Events;

namespace Sitecore.XConnect.DataMigration.Target.Mapper.EventModels
{
    public class CampaignEventModelMapper : BaseEventModelMapper<CampaignEventModel, CampaignEvent>
    {
        protected override CampaignEvent CreateEvent(CampaignEventModel sourceEvent)
        {
            return new CampaignEvent(sourceEvent.CampaignDefinitionId, sourceEvent.Timestamp);
        }
    }
}
