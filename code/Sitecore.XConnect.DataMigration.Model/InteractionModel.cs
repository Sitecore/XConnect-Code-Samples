// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using System;
using System.Collections.Generic;
using Sitecore.XConnect.DataMigration.Source.Model.Events;

namespace Sitecore.XConnect.DataMigration.Source.Model
{
    [Serializable]
    public class InteractionModel
    {
        public InteractionModel()
        {
            Facets = new Dictionary<string, Facet>();
            Events = new List<EventModel>();
        }

        public IDictionary<string, Facet> Facets { get; set; }

        public List<EventModel> Events { get; set; }

        public Guid? CampaignId { get; set; }

        public Guid ChannelId { get; set; }

        public int EngagementValue { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public InteractionInitiator Initiator { get; set; }

        public string UserAgent { get; set; }

        public Guid? VenueId { get; set; }
    }
}
