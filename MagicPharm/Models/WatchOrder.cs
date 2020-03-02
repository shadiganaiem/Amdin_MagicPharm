namespace MagicPharm.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class WatchOrder
    {
        public int ID { get; set; }

        public int ClientId { get; set; }

        public int WatchBrandId { get; set; }

        [Required]
        public string WatchCatalogNumber { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? GuaranteeDate { get; set; }

        public double? PreDiscountPrice { get; set; }

        public double? Discount { get; set; }

        public virtual Client Client { get; set; }

        public virtual WatchBrand WatchBrand { get; set; }
    }
}
