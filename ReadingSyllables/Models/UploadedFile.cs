namespace ReadingSyllables.Models
{
    internal abstract class UploadedFile
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string HashSum { get; set; }
        public UploadedFileType UploadedFileType { get; set; }
    }
}