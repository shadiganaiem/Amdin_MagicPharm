using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MagicPharm.Models.DataTable
{
    public class WatchRepairGridSource
    {
        public int ID { get; set; }

        public string Number { get; set; }

        public string ClientName { get; set; }

        public string ClientPhone { get; set; }

        public string WatchBrand { get; set; }

        public string WatchBarcode { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? ReceiptDate { get; set; }
        public string Status { get; set; }

    }
}