using Microsoft.EntityFrameworkCore;

namespace ReadingSyllables.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Word
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public DateTime NextShow { get; set; } = DateTime.UtcNow;
        public int Show { get; set; } = 0;
        public int ShowCounter { get; set; } = 0;
        public virtual HashSet<Syllable> Syllables { get; set; }
        public virtual string? Construction { get; set; }
    }
}