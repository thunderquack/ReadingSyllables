namespace ReadingSyllables.Models
{
    internal class SyllablesRatingUploadedFile : UploadedFile
    {
        public new UploadedFileType UploadedFileType { get; set; } = UploadedFileType.SyllablesRating;
    }
}