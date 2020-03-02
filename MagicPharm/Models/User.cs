namespace MagicPharm.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        public int ID { get; set; }

        [Required]
        [StringLength(20)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [StringLength(20)]
        public string Firsname { get; set; }

        [Required]
        [StringLength(20)]
        public string Lastname { get; set; }

        public int StatusId { get; set; }

        public int RoleId { get; set; }

        public DateTime CreationDate { get; set; }

        public virtual Role Role { get; set; }

        public virtual Status Status { get; set; }
    }
}
