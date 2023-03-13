using Microsoft.Extensions.DependencyInjection;
using ReadingSyllables.Models;

namespace ReadingSyllables.SyllablesGenerator
{
    internal abstract class AbstractGenerator
    {
        public Settings Settings { get; private set; }
        protected string previousSyllable = "";
        protected string currentSyllable = "";
        protected string nextSyllable = "";
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

        protected abstract string GenerateSyllable();

        public string GetCurrentSyllableAndGenerateNext()
        {
            previousSyllable = currentSyllable;
            currentSyllable = nextSyllable;
            string tempSyllable;
            do
            {
                tempSyllable = GenerateSyllable();
            }
            while (tempSyllable == nextSyllable);
            nextSyllable = tempSyllable;
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

        public string GetNextSyllable()
        {
            return nextSyllable;
        }
    }
}