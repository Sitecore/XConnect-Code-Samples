// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

namespace Sitecore.XConnect.DataMigration.Source.Model
{
    public interface IXConnectReader
    {
        void Initialize(MigrationSettings settings);

        ContactCursorResult Execute();

        ContactCursorResult Execute(byte[] bookmark);
    }
}
