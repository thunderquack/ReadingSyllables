namespace ReadingSyllables.Models
{
    internal class StructuredWordsUploadedFile : UploadedFile
    {
        public new UploadedFileType UploadedFileType { get; set; } = UploadedFileType.StructuredWords;
    }
}