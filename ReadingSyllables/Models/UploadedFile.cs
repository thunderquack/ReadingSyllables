using Microsoft.EntityFrameworkCore;

namespace ReadingSyllables.Models
{
    [Index(nameof(HashSum), IsUnique = true)]
    public class UploadedFile
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string HashSum { get; set; }
        public UploadedFileType UploadedFileType { get; set; }
    }
}