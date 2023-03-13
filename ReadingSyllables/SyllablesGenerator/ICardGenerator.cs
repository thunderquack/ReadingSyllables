namespace ReadingSyllables.SyllablesGenerator
{
    internal interface ICardGenerator
    {
        string DoBad();

        string DoAverage();

        string DoGood();
    }
}