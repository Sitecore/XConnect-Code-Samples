// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

namespace Sitecore.XConnect.DataMigration.Source.Model
{
    public static class MigrationConstants
    {
        // 'Alias' identifier source is system type of identifiers which are not allowed to set
        // explicitly for the contact via AddContactIdentifierOperation, so we need to keep the reference
        // between target and source XConnect data.
        // 'AliasOld' identifier on the target XConnect contains 'Alias' contact identifier value from the source XConnect.
        public static readonly string AliasOldSourceIdentifier => "AliasOld";
    }
}
