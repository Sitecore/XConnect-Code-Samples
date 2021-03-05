// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using Sitecore.XConnect.Serialization;

namespace Sitecore.XConnect.DataMigration.Source
{
    internal static class FacetExtensions
    {
        public static T WithClearedConcurrency<T>(this T facet) where T : Facet
        {
            DeserializationHelpers.SetConcurrencyToken(facet, null);
            return facet;
        }
    }
}
