// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using System;
using System.Collections.Generic;
using Sitecore.XConnect.DataMigration.Source.Model.Facets;

namespace Sitecore.XConnect.DataMigration.Source.Model
{
    [Serializable]
    public class ContactModel
    {
        public ContactModel()
        {
            Identifiers = new List<ContactIdentifierModel>();
            Facets = new Dictionary<string, Facet>();
            FacetModel = new Dictionary<string, FacetModel>();
            Interactions = new List<InteractionModel>();
        }

        public IList<ContactIdentifierModel> Identifiers { get; }

        public IDictionary<string, Facet> Facets { get; }

        public IDictionary<string, FacetModel> FacetModel { get; }

        public IList<InteractionModel> Interactions { get; }
    }
}
