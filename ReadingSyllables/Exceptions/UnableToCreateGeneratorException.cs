namespace ReadingSyllables.Exceptions
{
    internal class UnableToCreateGeneratorException : Exception
    {
        public UnableToCreateGeneratorException(Exception exception) : base("Error during generator creation", exception)
        {
        }
    }
}