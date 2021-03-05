// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using System.Collections.Generic;
using Sitecore.EmailCampaign.Model.XConnect.Facets;
using Sitecore.XConnect.DataMigration.Source.Model.Facets;

namespace Sitecore.XConnect.DataMigration.Source.Mappers.Facets
{
    public class EmailAddressHistoryFacetMapper : BaseFacetMapper<EmailAddressHistory, EmailAddressHistoryFacetModel>
    {
        protected override EmailAddressHistoryFacetModel CreateModel(EmailAddressHistory sourceFacet)
        {
            var model = new EmailAddressHistoryFacetModel();

            if (sourceFacet.History.Count != 0)
            {
                model.History = new List<EmailAddressHistoryEntryModel>();
            }

            foreach (var entry in sourceFacet.History)
            {
                var emailAddressHistoryEntry = new EmailAddressHistoryEntryModel
                {
                    Id = entry.Id,
                    EmailAddress = entry.EmailAddress,
                    Timestamp = entry.Timestamp
                };

                model.History.Add(emailAddressHistoryEntry);
            }

            return model;
        }
    }
}
