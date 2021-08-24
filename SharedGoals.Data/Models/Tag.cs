using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SharedGoals.Data.Models
{
    using static DataConstants.Tag;
    public class Tag
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        public IEnumerable<Goal> Goals { get; init; } = new List<Goal>();
    }
}
