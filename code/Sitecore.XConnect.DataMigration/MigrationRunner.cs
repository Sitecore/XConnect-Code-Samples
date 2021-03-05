// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Serilog;
using Sitecore.XConnect.DataMigration.Source.Model;
using Sitecore.XConnect.DataMigration.Target;

namespace Sitecore.XConnect.DataMigration
{
    public class MigrationRunner : IDisposable
    {
        private readonly MigrationSettings _settings;

        private readonly BlockingCollection<ContactModel> _contactsToMigrate;

        private AppDomain _sourceDomain;

        public MigrationRunner(MigrationSettings settings)
        {
            _contactsToMigrate = new BlockingCollection<ContactModel>();
            _settings = settings;
        }

        public async Task Run()
        {
            var sourceReader = InitializeSourceDomain();

            sourceReader.Initialize(_settings);

            var producer = Task.Factory.StartNew(() =>
            {
                ContactCursorResult readResult;

                try
                {
                    readResult = sourceReader.Execute();
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                    Log.Error(ex.StackTrace);
                    throw;
                }

                foreach (var contact in readResult.Contacts)
                {
                    _contactsToMigrate.Add(contact);
                }

                while (readResult.HasRecords)
                {
                    var bookmark = readResult.Bookmark;

                    readResult = sourceReader.Execute(bookmark);

                    foreach (var contact in readResult.Contacts)
                    {
                        _contactsToMigrate.Add(contact);
                    }
                }

                _contactsToMigrate.CompleteAdding();
            },
            TaskCreationOptions.LongRunning);

            var numberOfContactsToSubmit = _settings.SubmitBatchSize;

            var consumers = new Task[_settings.ThreadsNumber];

            for (var i = 0; i < consumers.Length; i++)
            {
                consumers[i] = Task.Run(async () =>
                {
                    var targetWriter = new XConnectWriter(_settings);

                    while (!_contactsToMigrate.IsCompleted)
                    {
                        var batch = new List<ContactModel>(numberOfContactsToSubmit);

                        for (var y = 0; y < numberOfContactsToSubmit; y++)
                        {
                            ContactModel contactModel = null;

                            // Blocks if _contactsToMigrate.Count == 0.
                            // IOE means that Take() was called on a completed collection.
                            // Some other thread can call CompleteAdding after we pass the
                            // IsCompleted check but before we call Take.
                            // In this example, we can simply catch the exception since the
                            // loop will break on the next iteration.
                            try
                            {
                                contactModel = _contactsToMigrate.Take(); // Blocks until a new contact is available and removes an item from the collection.
                            }
                            catch (InvalidOperationException) { }

                            if (contactModel != null)
                            {
                                batch.Add(contactModel);
                            }
                        }

                        try
                        {
                            await targetWriter.SubmitAsync(batch).ConfigureAwait(false);
                        }
                        catch (Exception ex)
                        {
                            Log.Error(ex.Message);
                            Log.Error(ex.StackTrace);
                            throw;
                        }
                    }
                });
            }

            await producer.ConfigureAwait(false);
            await Task.WhenAll(consumers).ConfigureAwait(false);
        }

        public void Dispose()
        {
            try
            {
                AppDomain.Unload(_sourceDomain);
            }
            catch (AppDomainUnloadedException e)
            {
                Log.Error(e.GetType().FullName);
                Log.Error("The application domain doesn't exist and can't be unloaded.");
            }

            _contactsToMigrate?.Dispose();
        }

        private IXConnectReader InitializeSourceDomain()
        {
            Log.Information("Create and initialize XConnect Source in a separate domain.");

            var sourceDomainDirectory = Path.Combine(AppContext.BaseDirectory, "Source");

            var sourceDomainConfigurationFile =
                Path.Combine(sourceDomainDirectory, "Sitecore.XConnect.DataMigration.Source.dll.config");

            Log.Information("Source domain application base {0}", sourceDomainDirectory);
            Log.Information("Source domain configuration file {0}", sourceDomainConfigurationFile);

            var domainInfo = new AppDomainSetup
            {
                ApplicationBase = sourceDomainDirectory, ConfigurationFile = sourceDomainConfigurationFile, PrivateBinPath = sourceDomainDirectory
            };

            _sourceDomain = AppDomain.CreateDomain("XConnectSource", AppDomain.CurrentDomain.Evidence, domainInfo);

            return (IXConnectReader)_sourceDomain.CreateInstanceAndUnwrap("Sitecore.XConnect.DataMigration.Source", "Sitecore.XConnect.DataMigration.Source.XConnectReader");
        }
    }
}
