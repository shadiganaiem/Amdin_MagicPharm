namespace MagicPharm.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Client
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Client()
        {
            WatchOrders = new HashSet<WatchOrder>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(25)]
        public string FullName { get; set; }

        [Required]
        [StringLength(12)]
        public string Phone { get; set; }

        [StringLength(12)]
        public string Telephone { get; set; }

        [StringLength(30)]
        public string Email { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? LastOrderDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WatchOrder> WatchOrders { get; set; }
    }
}
