using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SharedGoals.Data.Models
{
    using static DataConstants.Creator;

    public class Creator
    {
        public string Id { get; init; } = Guid.NewGuid().ToString();

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        public string UserId { get; set; }

        public IEnumerable<Goal> Goals { get; init; } = new List<Goal>();
    }
}
