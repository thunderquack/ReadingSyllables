namespace ReadingSyllables.SyllablesGenerator
{
    internal abstract class AbstractSyllableGenerator
    {
        protected readonly Settings settings;

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