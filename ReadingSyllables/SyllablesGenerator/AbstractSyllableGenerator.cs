﻿namespace ReadingSyllables.SyllablesGenerator
{
    internal abstract class AbstractSyllableGenerator
    {
        public Settings Settings { get; private set; }
        protected string prevSyllable = ""; 
        protected Random random = new();

        public abstract string GenerateSyllable();

        private AbstractSyllableGenerator()
        {
        }

        public AbstractSyllableGenerator(Settings settings)
        {
            Settings = settings;
        }

        public abstract string GetShortSettings();
    }
}