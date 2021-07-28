using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using static SharedGoals.Data.DataConstants;

namespace SharedGoals.Data.Models
{
    public class Creator
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(CreatorNameMaxLength)]
        public string Name { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; init; }

        public IEnumerable<Goal> Goals { get; init; } = new List<Goal>();
    }
}
