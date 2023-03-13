using Microsoft.Extensions.DependencyInjection;
using ReadingSyllables.Models;

namespace ReadingSyllables.SyllablesGenerator
{
    internal abstract class AbstractGenerator
    {
        public Settings Settings { get; private set; }
        protected string previousSyllable = "";
        protected string currentSyllable = "";
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
        protected abstract string NextSyllable();
        public string GetCurrentSyllableAndGenerateNext()
        {
            previousSyllable = currentSyllable;
            string tempSyllable;
            do
            {
                tempSyllable = NextSyllable();
            }
            while (tempSyllable == currentSyllable);
            currentSyllable = tempSyllable;
            return previousSyllable;
        }
        public string GetCurrentSyllable()
        {
            return currentSyllable;
        }
        public string GetPreviousSyllable()
        {
            return previousSyllable;
        }
    }
}