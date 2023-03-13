namespace ReadingSyllables.SyllablesGenerator
{
    internal class RandomSyllablesGenerator : AbstractGenerator
    {
        private List<char> CONSONANTS = new List<char>() {
            'б', 'в', 'г', 'д', 'ж', 'з', 'й', 'к', 'л', 'м', 'н', 'п', 'р', 'с', 'т', 'ф', 'х', 'ц', 'ч', 'ш', 'щ'
        };

        private List<char> VOVELS = new List<char>() {
            'а', 'е', 'и', 'о', 'у', 'ы', 'э', 'ю', 'я'
        };

        public RandomSyllablesGenerator(Settings settings) : base(settings)
        {
        }

        protected override string NextSyllable()
        {
            switch (Size)
            {
                case 2:
                    int idx = random.Next(0, VOVELS.Count - 1);
                    char vovel = VOVELS[idx];
                    bool dangerousVovel = false;
                    if (vovel == 'ы' || vovel == 'ю' || vovel == 'я')
                    {
                        dangerousVovel = true;
                    }
                    List<char> consonants = CONSONANTS.Take(int.MaxValue).ToList();
                    consonants.Remove('й');
                    if (dangerousVovel)
                    {
                        consonants.Remove('ж');
                        consonants.Remove('ч');
                        consonants.Remove('ш');
                        consonants.Remove('щ');
                    }
                    idx = random.Next(0, consonants.Count - 1);
                    char consonant = consonants[idx];
                    return $"{consonant}{vovel}";                    

                default:
                    return "";
            }
        }

        public override string GetShortSettings()
        {
            return $"Random - Length: {Size}";
        }
    }
}