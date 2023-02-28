using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReadingSyllables.Models;

namespace ReadingSyllables
{
    internal static class Program
    {

#pragma warning disable CS8618 
        internal static IHost host;
#pragma warning restore CS8618 

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            ApplicationConfiguration.Initialize();
            var builder = Host.CreateDefaultBuilder();
            builder.ConfigureServices(
                services =>
                    services.AddSingleton<SyllablesContext>());
            host = builder.Build();
            Application.Run(new FormSyllables());
        }
    }
}