// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using System;
using System.Collections.Generic;
using Sitecore.XConnect.DataMigration.Source.Model;
using Sitecore.XConnect.DataMigration.Target.Mapper.EventModels;

namespace Sitecore.XConnect.DataMigration.Target.Mapper
{
    internal class EntityModelMapper
    {
        private readonly FacetModelMapperRegistry _facetModelMapperRegistry;
        private readonly EventModelMapperRegistry _eventMapperRegistry;

        public EntityModelMapper()
        {
            _facetModelMapperRegistry = new FacetModelMapperRegistry();
            _eventMapperRegistry = new EventModelMapperRegistry();
        }

        public void ToXConnectOperations(IXdbContext client, IReadOnlyCollection<ContactModel> contacts)
        {
            foreach (var contactProxy in contacts)
            {
                var contact = new Contact();

                client.AddContact(contact);

                foreach (var identifier in contactProxy.Identifiers)
                {
                    client.AddContactIdentifier(contact, new ContactIdentifier(
                        identifier.Source, identifier.Identifier, identifier.IdentifierType));
                }

                MapFacetModels(client, contact, contactProxy);

                foreach (var interactionProxy in contactProxy.Interactions)
                {
                    var interaction = new Interaction(
                        contact,
                        interactionProxy.Initiator,
                        interactionProxy.ChannelId,
                        interactionProxy.UserAgent)
                    {
                        CampaignId = interactionProxy.CampaignId,
                        VenueId = interactionProxy.VenueId
                    };

                    MapEventModels(interactionProxy, interaction);

                    foreach (var interactionFacetItem in interactionProxy.Facets)
                    {
                        client.SetFacet(interaction, interactionFacetItem.Key, interactionFacetItem.Value);
                    }

                    client.AddInteraction(interaction);
                }
            }
        }

        private void MapFacetModels(IXdbContext client, Contact contact, ContactModel contactProxy)
        {
            foreach (var facet in contactProxy.Facets)
            {
                client.SetFacet(contact, facet.Key, facet.Value);
            }

            foreach (var facetModel in contactProxy.FacetModel)
            {
                var facetMapper = _facetModelMapperRegistry.GetMapper(facetModel.Value.GetType());

                if (facetMapper == null)
                {
                    throw new Exception(@"Facet mapper for the facet model type {facetModel.GetType().ToString()} is not registered.");
                }

                var xcFacet = facetMapper.Map(facetModel.Value);

                client.SetFacet(contact, facetModel.Key, xcFacet);
            }
        }

        private void MapEventModels(InteractionModel interactionProxy, Interaction interaction)
        {
            foreach (var eventModel in interactionProxy.Events)
            {
                IEventModelMapper eventMapper = _eventMapperRegistry.GetMapper(eventModel.GetType());

                if (eventMapper == null)
                {
                    throw new Exception(@"Event mapper for the event model type {eventModel.GetType().ToString()} is not registered.");
                }

                var xcEvent = eventMapper.Map(eventModel);

                interaction.Events.Add(xcEvent);
            }
        }
    }
}
