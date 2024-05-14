﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GaHipHop_Repository.Entity
{
    [Table("Role")]
    public class Role
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}
