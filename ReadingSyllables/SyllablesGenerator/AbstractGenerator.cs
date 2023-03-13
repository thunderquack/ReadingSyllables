using Microsoft.Extensions.DependencyInjection;
using ReadingSyllables.Models;

namespace ReadingSyllables.SyllablesGenerator
{
    internal abstract class AbstractGenerator
    {
        public Settings Settings { get; private set; }
        protected string previousPiece = "";
        protected string currentPiece = "";
        protected string nextPiece = "";
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
            // Generating three times in order to fill in
            // previous, current and next
            for (int i = 0; i < 3; i++)
            {
                GetCurrentPieceAndGenerateNext();
            }
        }

        public abstract string GetShortSettings();

        protected abstract string Generate();

        public string GetCurrentPieceAndGenerateNext()
        {
            previousPiece = currentPiece;
            currentPiece = nextPiece;
            string temp;
            do
            {
                temp = Generate();
            }
            while (temp == nextPiece);
            nextPiece = temp;
            return currentPiece;
        }

        public string GetCurrentPiece()
        {
            return currentPiece;
        }

        public string GetPreviousPiece()
        {
            return previousPiece;
        }

        public string GetNextPiece()
        {
            return nextPiece;
        }
        protected void Save()
        {
            Context.SaveChanges();
        }
    }
}