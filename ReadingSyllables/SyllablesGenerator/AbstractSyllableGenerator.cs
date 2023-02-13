namespace ReadingSyllables.SyllablesGenerator
{
    internal abstract class AbstractSyllableGenerator
    {
        protected readonly Settings settings;
        protected string prevSyllable = ""; 
        protected Random random = new();

        public abstract string GenerateSyllable();

        public AbstractSyllableGenerator(Settings settings)
        {
            this.settings = settings;
        }

        private AbstractSyllableGenerator()
        {
        }
    }
}