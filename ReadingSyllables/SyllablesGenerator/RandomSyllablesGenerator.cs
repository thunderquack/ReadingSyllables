namespace ReadingSyllables.SyllablesGenerator
{
    internal class RandomSyllablesGenerator : AbstractSyllableGenerator
    {
        private List<char> CONSONANTS = new List<char>() {
            'б', 'в', 'г', 'д', 'ж', 'з', 'й', 'к', 'л', 'м', 'н', 'п', 'р', 'с', 'т', 'ф', 'х', 'ц', 'ч', 'ш', 'щ'
        };

        private List<char> VOVELS = new List<char>() {
            'а', 'е', 'и', 'о', 'у', 'ы', 'э', 'ю', 'я'
        };

        private Random r = new Random();

        public int Length { get; private set; }

        public void SetLength(int length)
        {
            Length = length;
        }

        public RandomSyllablesGenerator(Settings settings) : base(settings)
        {
        }

        public override string GenerateSyllable()
        {
            switch (Length)
            {
                case 2:
                    int idx = r.Next(0, VOVELS.Count - 1);
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
                    idx = r.Next(0, consonants.Count - 1);
                    char consonant = consonants[idx];
                    string res = $"{consonant}{vovel}".ToUpper();
                    if (res == prevSyllable)
                    {
                        return GenerateSyllable();
                    }
                    prevSyllable = $"{consonant}{vovel}".ToUpper();
                    return prevSyllable;

                default:
                    return "";
            }
        }
    }
}