namespace MagicPharm.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class WatchRepair
    {
        public int ID { get; set; }

        public int ClientId { get; set; }

        public int WatchBrandId { get; set; }

        [Required]
        public string WatchBarcode { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? ReceiptDate { get; set; }

        public int StatusId { get; set; }

        public virtual Client Client { get; set; }

        public virtual Status Status { get; set; }

        public virtual WatchBrand WatchBrand { get; set; }
    }
}
