using Microsoft.EntityFrameworkCore;

namespace ReadingSyllables.Models
{
    [Index(nameof(Name), IsUnique = true)]
    internal class Syllable
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string Name { get; set; }
        public DateTime NextShow { get; set; } = DateTime.UtcNow;
        public int Show { get; set; } = 0;
        public int ShowCounter { get; set; } = 0;
        public HashSet<Word>? Words { get; set; }

        public override string? ToString()
        {
            return $"{Name} - {NextShow.ToLocalTime()}";
        }
    }
}