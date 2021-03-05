// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using Sitecore.XConnect.Collection.Model;
using Sitecore.XConnect.DataMigration.Source.Model.Events;

namespace Sitecore.XConnect.DataMigration.Target.Mapper.EventModels
{
    public class DownloadEventModelMapper : BaseEventModelMapper<DownloadEventModel, DownloadEvent>
    {
        protected override DownloadEvent CreateEvent(DownloadEventModel sourceEvent)
        {
            return new DownloadEvent(sourceEvent.Timestamp, sourceEvent.ItemId);
        }
    }
}
