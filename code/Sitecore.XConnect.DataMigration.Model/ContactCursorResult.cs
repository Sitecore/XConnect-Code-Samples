// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using System;
using System.Collections.Generic;

namespace Sitecore.XConnect.DataMigration.Source.Model
{
    [Serializable]
    public class ContactCursorResult
    {
        public ContactCursorResult()
        {
            Contacts = new List<ContactModel>();
            HasRecords = false;
        }

        public ContactCursorResult(IReadOnlyCollection<ContactModel> contacts, byte[] bookmark, bool hasRecords)
        {
            Contacts = contacts;
            Bookmark = bookmark;
            HasRecords = hasRecords;
        }

        public IReadOnlyCollection<ContactModel> Contacts { get; internal set; }

        public byte[] Bookmark { get; set; }

        public bool HasRecords { get; set; }
    }
}
