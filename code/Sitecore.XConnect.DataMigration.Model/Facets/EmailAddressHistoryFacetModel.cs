// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using System;
using System.Collections.Generic;

namespace Sitecore.XConnect.DataMigration.Source.Model.Facets
{
    [Serializable]
    public class EmailAddressHistoryFacetModel : FacetModel
    {
        public List<EmailAddressHistoryEntryModel> History { get; set; }
    }

    [Serializable]
    public class EmailAddressHistoryEntryModel
    {
        public int Id { get; set; }

        public string EmailAddress { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
