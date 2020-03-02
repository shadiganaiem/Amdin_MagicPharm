using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MagicPharm.Models.DataTable
{
    public class WatchOrderGridSource
    {
        public int ID { get; set; }
        public string ClientName { get; set; }
        public string ClientPhone { get; set; }
        public string WatchBrand { get; set; }
        public string WatchCatalogNumber { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? GuaranteeDate { get; set; }
    }
}