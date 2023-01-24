namespace ReadingSyllables
{
    internal static class SyllablesGenerator
    {
        private static List<char> CONSONANTS = new List<char>() {
            'б', 'в', 'г', 'д', 'ж', 'з', 'й', 'к', 'л', 'м', 'н', 'п', 'р', 'с', 'т', 'ф', 'х', 'ц', 'ч', 'ш', 'щ'
        };
        private static List<char> VOVELS = new List<char>() {
            'а', 'е', 'и', 'о', 'у', 'ы', 'э', 'ю', 'я' 
        };
        private static Random r = new Random();

        public static string GenerateSyllable(int length, string prevSyllable = "")
        {
            switch (length)
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
                    string res = ($"{consonant}{vovel}").ToUpper();
                    if (res == prevSyllable)
                    {
                        return GenerateSyllable(length, prevSyllable);
                    }
                    return ($"{consonant}{vovel}").ToUpper();
                default:
                    return "";
            }
        }
    }
}
