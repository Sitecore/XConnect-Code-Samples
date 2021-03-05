// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using Sitecore.XConnect.Collection.Model;
using Sitecore.XConnect.DataMigration.Source.Model.Events;

namespace Sitecore.XConnect.DataMigration.Source.Mappers.Events
{
    public class DownloadEventMapper : BaseEventMapper<DownloadEvent, DownloadEventModel>
    {
        protected override DownloadEventModel CreateModel(DownloadEvent sourceEvent)
        {
            return new DownloadEventModel();
        }
    }
}
