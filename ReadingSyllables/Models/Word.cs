using Microsoft.EntityFrameworkCore;

namespace ReadingSyllables.Models
{
    [Index(nameof(Name), IsUnique = true)]
    internal class Word
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime NextShow { get; set; } = DateTime.UtcNow;
        public int Show { get; set; } = 0;
        public int ShowCounter { get; set; } = 0;
        public List<Syllable>? Syllables { get; set; }
    }
}
