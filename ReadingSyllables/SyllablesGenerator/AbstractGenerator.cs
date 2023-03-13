using Microsoft.Extensions.DependencyInjection;
using ReadingSyllables.Models;
using System.Security.Policy;

namespace ReadingSyllables.SyllablesGenerator
{
    internal abstract class AbstractGenerator
    {
        public Settings Settings { get; private set; }
        protected string prevSyllable = "";
        protected Random random = new();
        public int Size { get; set; }

        public SyllablesContext Context
        {
            get
            {
                return Program.host.Services.GetRequiredService<SyllablesContext>();
            }
        }

        private AbstractGenerator()
        {
        }

        public AbstractGenerator(Settings settings)
        {
            Settings = settings;
            Size = settings.Size;
        }

        public abstract string GetShortSettings();
        public abstract string NextSyllable();        
    }
}