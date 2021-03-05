// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using Sitecore.XConnect.DataMigration.Source.Model.Events;

namespace Sitecore.XConnect.DataMigration.Target.Mapper.EventModels
{
    internal interface IEventModelMapper
    {
        Event Map(EventModel sourceEventModel);
    }

    internal interface IEventModelMapper<in TEventModel> : IEventModelMapper
        where TEventModel : EventModel
    {
        Event Map(TEventModel sourceEvent);
    }
}
