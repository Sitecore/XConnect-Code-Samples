// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using Sitecore.XConnect.Collection.Model;
using Sitecore.XConnect.DataMigration.Source.Model.Events;

namespace Sitecore.XConnect.DataMigration.Source.Mappers.Events
{
    public class PageViewEventMapper : BaseEventMapper<PageViewEvent, PageViewEventModel>
    {
        protected override PageViewEventModel CreateModel(PageViewEvent sourceEvent)
        {
            var model = new PageViewEventModel
            {
                ItemLanguage = sourceEvent.ItemLanguage,
                ItemVersion = sourceEvent.ItemVersion,
                Url = sourceEvent.Url
            };

            if (sourceEvent.SitecoreRenderingDevice != null)
            {
                model.SitecoreRenderingDevice = new SitecoreDeviceDataModel
                {
                    Id = sourceEvent.SitecoreRenderingDevice.Id,
                    Name = sourceEvent.SitecoreRenderingDevice.Name
                };
            }

            return model;
        }
    }
}
