// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

namespace Sitecore.XConnect.DataMigration.Source.Mappers.Filters
{
    public interface IContactFilterManager
    {
        bool Apply(Contact contact);
    }
}
