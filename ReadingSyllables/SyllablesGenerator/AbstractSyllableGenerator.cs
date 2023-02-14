namespace ReadingSyllables.SyllablesGenerator
{
    internal abstract class AbstractSyllableGenerator
    {
        public Settings Settings { get; private set; }
        protected string prevSyllable = ""; 
        protected Random random = new();

        public abstract string GenerateSyllable();

        public AbstractSyllableGenerator(Settings settings)
        {
            this.Settings = settings;
        }

        private AbstractSyllableGenerator()
        {
        }
    }
}