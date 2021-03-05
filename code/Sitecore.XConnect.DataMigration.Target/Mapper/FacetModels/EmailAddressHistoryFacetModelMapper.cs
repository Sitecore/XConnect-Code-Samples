// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using System.Collections.Generic;
using Sitecore.EmailCampaign.Model.XConnect.Facets;
using Sitecore.XConnect.DataMigration.Source.Model.Facets;

namespace Sitecore.XConnect.DataMigration.Target.Mapper.FacetModels
{
    public class EmailAddressHistoryFacetModelMapper : BaseFacetModelMapper<EmailAddressHistoryFacetModel, EmailAddressHistory>
    {
        protected override EmailAddressHistory CreateFacet(EmailAddressHistoryFacetModel sourceFacetModel)
        {
            var facet = new EmailAddressHistory();

            if (sourceFacetModel.History.Count != 0)
            {
                facet.History = new List<EmailAddressHistoryEntry>();
            }

            foreach (var entry in sourceFacetModel.History)
            {
                var emailAddressHistoryEntry = new EmailAddressHistoryEntry
                {
                    Id = entry.Id,
                    EmailAddress = entry.EmailAddress,
                    Timestamp = entry.Timestamp
                };

                facet.History.Add(emailAddressHistoryEntry);
            }

            return facet;
        }
    }
}
