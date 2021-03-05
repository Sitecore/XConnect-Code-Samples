// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;
using Sitecore.ContentTesting.Model.xConnect.Models;
using Sitecore.EmailCampaign.Model.XConnect;
using Sitecore.XConnect.Client;
using Sitecore.XConnect.Client.WebApi;
using Sitecore.XConnect.Collection.Model;
using Sitecore.XConnect.DataMigration.Source.Model;
using Sitecore.XConnect.DataMigration.Target.Mapper;
using Sitecore.XConnect.Operations;
using Sitecore.XConnect.Schema;
using Sitecore.Xdb.Common.Web;

namespace Sitecore.XConnect.DataMigration.Target
{
    public class XConnectWriter
    {
        private readonly IXdbContext _client;
        private readonly EntityModelMapper _mapper;

        private MigrationSettings _settings;

        public XConnectWriter(MigrationSettings settings)
        {
            _client = CreateClient(settings);

            _mapper = new EntityModelMapper();

            _settings = settings;
        }

        public async Task SubmitAsync(IReadOnlyCollection<ContactModel> contactModels)
        {
            _mapper.ToXConnectOperations(_client, contactModels);

            try
            {
                await _client.SubmitAsync().ConfigureAwait(false);
            }
            catch (XdbExecutionException ex)
            {
                // Get any operations that failed
                var operations = ex.GetOperations(_client)
                    .Where(x => x.Status == XdbOperationStatus.Failed);

                foreach (var ops in operations)
                {
                    if (ops is AddContactOperation addContact)
                    {
                        if (addContact.Result != null && addContact.Result.Status == SaveResultStatus.AlreadyExists)
                        {
                            var aliasOldIdentifier = addContact.Entity.Identifiers
                                .First(x => x.Source == MigrationConstants.AliasOldSourceIdentifier);

                            if (aliasOldIdentifier != null)
                            {
                                Log.Error("Contact with identifier {0}={1} Already Exist, the contact migration is skipped.",
                                    aliasOldIdentifier.Source,
                                    aliasOldIdentifier.Identifier);
                            }

                            return;
                        }
                    }

                    Log.Error(ex.Message);
                    return;
                }
            }
        }

        private static XConnectClient CreateClient(MigrationSettings settings)
        {
            var clientConfiguration = CreateXConnectClientConfiguration(settings);
            clientConfiguration.InitializeAsync().Wait();
            return new XConnectClient(clientConfiguration);
        }

        private static XConnectClientConfiguration CreateXConnectClientConfiguration(MigrationSettings settings)
        {
            List<IHttpClientModifier> clientModifiers = new List<IHttpClientModifier>();
            var timeoutClientModifier = new TimeoutHttpClientModifier(new TimeSpan(0, 0, 20));
            clientModifiers.Add(timeoutClientModifier);

            var collectionClient = new CollectionWebApiClient(
                new Uri($"{settings.TargetCollectionEndpoint}/odata"),
                clientModifiers,
                new[]
                {
                    CreateCertificateHttpClientHandlerModifier(settings.TargetCollectionCertificate)
                });

            var searchClient = new SearchWebApiClient(
                new Uri($"{settings.TargetSearchEndpoint}/odata"),
                clientModifiers,
                new[]
                {
                    CreateCertificateHttpClientHandlerModifier(settings.TargetSearchCertificate)
                });

            var configurationClient = new ConfigurationWebApiClient(
                new Uri($"{ settings.TargetConfigurationEndpoint}/configuration"),
                clientModifiers,
                new[]
                {
                    CreateCertificateHttpClientHandlerModifier(settings.TargetConfigurationCertificate)
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
