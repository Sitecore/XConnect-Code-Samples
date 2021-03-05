// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using Sitecore.XConnect.Collection.Model;
using Sitecore.XConnect.DataMigration.Source.Model.Events;

namespace Sitecore.XConnect.DataMigration.Source.Mappers.Events
{
    public class SearchEventMapper : BaseEventMapper<SearchEvent, SearchEventModel>
    {
        protected override SearchEventModel CreateModel(SearchEvent sourceEvent)
        {
            return new SearchEventModel
            {
                Keywords = sourceEvent.Keywords
            };
        }
    }
}
