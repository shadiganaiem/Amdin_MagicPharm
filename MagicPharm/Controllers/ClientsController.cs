using MagicPharm.Models;
using MagicPharm.Models.DataTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MagicPharm.Controllers
{
    public class ClientsController : Controller
    {
        private readonly ApplicationDbContext _context = null;

        public ClientsController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Clients
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Get all clients list 
        /// </summary>
        /// <returns></returns>
        public JsonResult GetClients()
        {
            var clients = _context.Clients.Select(x => new
            {
                id = x.ID.ToString(),
                text = x.FullName
            });
            return new JsonResult { Data = new { data = clients }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        /// <summary>
        /// get all clients list as a gridSource
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAllClients()
        {

            var clients = _context.Clients.Select(x => new ClientGridSource
            {
                ID = x.ID,
                FullName = x.FullName,
                CreationDate = x.CreationDate,
                Phone = x.Phone,
                TelePhone = x.Telephone,
                LastOrderdate = x.LastOrderDate
            });
            return new JsonResult { Data = new { data = clients }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }


        /// <summary>
        /// Try to find client matched the given phone number
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public JsonResult FindClient(string phone)
        {
            var clientName = string.Empty;
            var clientId = string.Empty;
            var clientTelephone = string.Empty;
            try
            {
                var client = _context.Clients.FirstOrDefault(x => x.Phone.Equals(phone) || x.Telephone.Equals(phone));

                if (client != null)
                {
                    clientName = client.FullName;
                    clientId = client.ID.ToString();
                    clientTelephone = client.Telephone;
                }
            }
            catch (Exception)
            {

            }

            return new JsonResult { Data = new { clientName = clientName, clientId = clientId, clientTelephone = clientTelephone }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}