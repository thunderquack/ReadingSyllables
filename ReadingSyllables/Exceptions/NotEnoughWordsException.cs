namespace ReadingSyllables.Exceptions
{
    internal class NotEnoughWordsException : Exception
    {
        public NotEnoughWordsException() : base("Not enough words, try increase size of generator")
        {

        }
    }
}
