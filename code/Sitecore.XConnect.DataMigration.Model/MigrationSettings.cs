// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using System;
using System.Configuration;

namespace Sitecore.XConnect.DataMigration.Source.Model
{
    [Serializable]
    public class MigrationSettings
    {
        public MigrationSettings()
        {
            SourceCollectionCertificate = ConfigurationManager.ConnectionStrings["xconnect.collection.certificate.93"].ConnectionString;
            SourceCollectionEndpoint = ConfigurationManager.ConnectionStrings["xconnect.collection.93"].ConnectionString;

            SourceSearchCertificate = ConfigurationManager.ConnectionStrings["xconnect.search.certificate.93"].ConnectionString;
            SourceSearchEndpoint = ConfigurationManager.ConnectionStrings["xconnect.search.93"].ConnectionString;

            SourceConfigurationCertificate = ConfigurationManager.ConnectionStrings["xconnect.configuration.certificate.93"].ConnectionString;
            SourceConfigurationEndpoint = ConfigurationManager.ConnectionStrings["xconnect.configuration.93"].ConnectionString;

            TargetCollectionCertificate = ConfigurationManager.ConnectionStrings["xconnect.collection.certificate.101"].ConnectionString;
            TargetCollectionEndpoint = ConfigurationManager.ConnectionStrings["xconnect.collection.101"].ConnectionString;

            TargetSearchCertificate = ConfigurationManager.ConnectionStrings["xconnect.search.certificate.101"].ConnectionString;
            TargetSearchEndpoint = ConfigurationManager.ConnectionStrings["xconnect.search.101"].ConnectionString;

            TargetConfigurationCertificate = ConfigurationManager.ConnectionStrings["xconnect.configuration.certificate.101"].ConnectionString;
            TargetConfigurationEndpoint = ConfigurationManager.ConnectionStrings["xconnect.configuration.101"].ConnectionString;

            ContactExtractionBatchSize = int.Parse(ConfigurationManager.AppSettings["Source.ContactExtraction.BatchSize"]);

            ContactFacetKeys = ConfigurationManager.AppSettings["Source.ContactFacetKeys"].Split(',');
            InteractionFacetKeys = ConfigurationManager.AppSettings["Source.InteractionFacetKeys"].Split(',');

            SubmitBatchSize = int.Parse(ConfigurationManager.AppSettings["Target.Submit.BatchSize"]);
            ThreadsNumber = int.Parse(ConfigurationManager.AppSettings["Target.Submit.NumberOfThreads"]);
        }

        public string SourceCollectionCertificate { get; }

        public string SourceSearchCertificate { get; }

        public string SourceConfigurationCertificate { get; }

        public string SourceCollectionEndpoint { get; }

        public string SourceSearchEndpoint { get; }

        public string SourceConfigurationEndpoint { get; }

        public string TargetCollectionCertificate { get; }

        public string TargetSearchCertificate { get; }

        public string TargetConfigurationCertificate { get; }

        public string TargetCollectionEndpoint { get; }

        public string TargetSearchEndpoint { get; }

        public string TargetConfigurationEndpoint { get; }

        public int ContactExtractionBatchSize { get; }

        public string[] ContactFacetKeys { get; }

        public string[] InteractionFacetKeys { get; }

        public int SubmitBatchSize { get; }

        public int ThreadsNumber { get; }
    }
}
