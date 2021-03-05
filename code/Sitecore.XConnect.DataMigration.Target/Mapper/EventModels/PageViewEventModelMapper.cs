// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using Sitecore.XConnect.Collection.Model;
using Sitecore.XConnect.DataMigration.Source.Model.Events;

namespace Sitecore.XConnect.DataMigration.Target.Mapper.EventModels
{
    public class PageViewEventModelMapper : BaseEventModelMapper<PageViewEventModel, PageViewEvent>
    {
        protected override PageViewEvent CreateEvent(PageViewEventModel sourceEvent)
        {
            var model = new PageViewEvent(
                sourceEvent.Timestamp,
                sourceEvent.ItemId,
                sourceEvent.ItemVersion,
                sourceEvent.ItemLanguage)
            {
                Url = sourceEvent.Url
            };

            if (sourceEvent.SitecoreRenderingDevice != null)
            {
                model.SitecoreRenderingDevice = new SitecoreDeviceData(
                    sourceEvent.SitecoreRenderingDevice.Id,
                    sourceEvent.SitecoreRenderingDevice.Name);
            }

            return model;
        }
    }
}
