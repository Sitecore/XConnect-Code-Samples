// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using Sitecore.XConnect.Collection.Model;
using Sitecore.XConnect.DataMigration.Source.Model.Events;

namespace Sitecore.XConnect.DataMigration.Target.Mapper.EventModels
{
    public class SearchEventModelMapper : BaseEventModelMapper<SearchEventModel, SearchEvent>
    {
        protected override SearchEvent CreateEvent(SearchEventModel sourceEvent)
        {
            return new SearchEvent(sourceEvent.Timestamp)
            {
                Keywords = sourceEvent.Keywords
            };
        }
    }
}
