// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using System;
using System.Collections.Generic;
using Serilog;
using Sitecore.ContentTesting.Model.xConnect.Models;
using Sitecore.EmailCampaign.Model.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Client.WebApi;
using Sitecore.XConnect.Collection.Model;
using Sitecore.XConnect.DataMigration.Source.Model;
using Sitecore.XConnect.Schema;
using Sitecore.Xdb.Common.Web;

namespace Sitecore.XConnect.DataMigration.Source
{
    public class XConnectReader : MarshalByRefObject, IXConnectReader
    {
        private IReadOnlyXdbContext _client;
        private ContactEnumeratorReader _contactEnumerator;
        private MigrationSettings _settings;

        public void Initialize(MigrationSettings settings)
        {
            _client = CreateClient(settings);
            _settings = settings;

            Log.Information("XConnect Source - Collection endpoint: {0}", _settings.SourceCollectionEndpoint);
            Log.Information("XConnect Source - Contact data extraction batch size: {0}", _settings.ContactExtractionBatchSize);
            Log.Information("XConnect Source - Contact facet keys for migration: {0}", _settings.ContactFacetKeys);
            Log.Information("XConnect Source - Interaction facet keys for migration: {0}", _settings.InteractionFacetKeys);

            _contactEnumerator = new ContactEnumeratorReader(
                _settings.ContactExtractionBatchSize,
                _settings.ContactFacetKeys,
                _settings.InteractionFacetKeys);
        }

        public ContactCursorResult Execute()
        {
            if (_client == null)
            {
                throw new Exception("The XConnect Client is not initialized, please execute InitializeAsync first.");
            }

            if (_contactEnumerator == null)
            {
                throw new Exception("The Contact Enumerator is not initialized, please execute InitializeAsync first.");
            }

            Log.Information("Read batch of contacts from the XConnect Source Client");

            return _contactEnumerator.ReadBatch(_client).Result;
        }

        public ContactCursorResult Execute(byte[] bookmark)
        {
            if (_client == null)
            {
                throw new Exception("The XConnect Client is not initialized, please execute InitializeAsync first.");
            }

            if (_contactEnumerator == null)
            {
                throw new Exception("The Contact Enumerator is not initialized, please execute InitializeAsync first.");
            }

            if (bookmark == null)
            {
                throw new Exception("The bookmark is null.");
            }

            Log.Information("Read batch of contacts from the XConnect Source Client");

            return _contactEnumerator.ReadBatch(_client, bookmark).Result;
        }

        private static XConnectClient CreateClient(MigrationSettings settings)
        {
            Log.Information("Create XConnect Source Client");

            try
            {
                var clientConfiguration = CreateXConnectClientConfiguration(settings);
                clientConfiguration.InitializeAsync().Wait();
                return new XConnectClient(clientConfiguration);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw;
            }
        }

        private static XConnectClientConfiguration CreateXConnectClientConfiguration(MigrationSettings settings)
        {
            List<IHttpClientModifier> clientModifiers = new List<IHttpClientModifier>();
            var timeoutClientModifier = new TimeoutHttpClientModifier(new TimeSpan(0, 0, 20));
            clientModifiers.Add(timeoutClientModifier);

            var collectionClient = new CollectionWebApiClient(
                new Uri($"{settings.SourceCollectionEndpoint}/odata"),
                clientModifiers,
                new[]
                {
                    CreateCertificateHttpClientHandlerModifier(settings.SourceCollectionCertificate)
                });

            var searchClient = new SearchWebApiClient(
                new Uri($"{settings.SourceSearchEndpoint}/odata"),
                clientModifiers,
                new[]
                {
                    CreateCertificateHttpClientHandlerModifier(settings.SourceSearchCertificate)
                });

            var configurationClient = new ConfigurationWebApiClient(
                new Uri($"{ settings.SourceConfigurationEndpoint}/configuration"),
                clientModifiers,
                new[]
                {
                    CreateCertificateHttpClientHandlerModifier(settings.SourceConfigurationCertificate)
                });

            XdbModel[] models =
            {
                CollectionModel.Model,
                EmailCollectionModel.Model,
                CustomDataModel.Model
            };

            return new XConnectClientConfiguration(new XdbRuntimeModel(models), collectionClient, searchClient, configurationClient, true);
        }

        private static CertificateHttpClientHandlerModifier CreateCertificateHttpClientHandlerModifier(string certificate)
        {
            var collectionOptions = CertificateHttpClientHandlerModifierOptions.Parse(certificate);

            return new CertificateHttpClientHandlerModifier(collectionOptions);
        }
    }
}
