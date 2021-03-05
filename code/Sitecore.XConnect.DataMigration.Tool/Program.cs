// © 2021 Sitecore Corporation A/S. All rights reserved. Sitecore® is a registered trademark of Sitecore Corporation A/S.

using System;
using System.Threading.Tasks;
using Serilog;
using Sitecore.XConnect.DataMigration.Source.Model;

namespace Sitecore.XConnect.DataMigration.Tool
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console()
                .WriteTo.File("logs/XConnectDataMigration.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Log.Information("XConnect data migration has been started.");

            try
            {
                using (var runner = new MigrationRunner(new MigrationSettings()))
                {
                    await runner.Run().ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                Log.Error(ex.StackTrace);
                throw;
            }

            Log.Information("XConnect data migration has been completed.");
        }
    }
}
