// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using System;

namespace Sitecore.XConnect.DataMigration.Source.Model.Events
{
    [Serializable]
    public class PageViewEventModel : EventModel
    {
        public string ItemLanguage { get; set; }

        public int ItemVersion { get; set; }

        public string Url { get; set; }

        public SitecoreDeviceDataModel SitecoreRenderingDevice { get; set; }
    }

    [Serializable]
    public class SitecoreDeviceDataModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
