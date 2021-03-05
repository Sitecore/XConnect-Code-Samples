// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using System;

namespace Sitecore.XConnect.DataMigration.Source.Model
{
    [Serializable]
    public class ContactIdentifierModel
    {
        public string Source { get; set; }

        public string Identifier { get; set; }

        public ContactIdentifierType IdentifierType { get; set; }
    }
}
