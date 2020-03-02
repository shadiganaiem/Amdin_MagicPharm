using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MagicPharm.Models.DataTable
{
    public class ClientGridSource
    {
        public int ID { get; set; }
        public string Phone { get; set; }
        public string FullName { get; set; }
        public string TelePhone { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastOrderdate { get; set; }
    }
}