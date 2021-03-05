// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using System;

namespace Sitecore.XConnect.DataMigration.Source.Model.Facets
{
    [Serializable]
    public class ConsentInformationFacetModel : FacetModel
    {
        public bool ConsentRevoked { get; set; }

        public bool DoNotMarket { get; set; }

        public bool ExecutedRightToBeForgotten { get; set; }
    }
}
