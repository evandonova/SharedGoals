using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static SharedGoals.Data.DataConstants;

namespace SharedGoals.Data.Models
{
    public class Tag
    {
        public int Id { get; init; }

        [Required]
        [MaxLength(TagNameMaxLength)]
        public string Name { get; set; }

        public IEnumerable<PersonalGoal> PersonalGoals { get; init; } = new List<PersonalGoal>();
        public IEnumerable<TeamGoal> TeamGoals { get; init; } = new List<TeamGoal>();
    }
}
