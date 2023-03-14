namespace ReadingSyllables.Exceptions
{
    internal class NotEnoughWordsException : GeneratorException
    {
        public NotEnoughWordsException() : base("Not enough words, try increase size of generator")
        {

        }
    }
}
