using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SharedGoals.Data.Models
{
    using static DataConstants;
    public class Tag
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(TagNameMaxLength)]
        public string Name { get; set; }

        public IEnumerable<Goal> Goals { get; init; } = new List<Goal>();
    }
}
