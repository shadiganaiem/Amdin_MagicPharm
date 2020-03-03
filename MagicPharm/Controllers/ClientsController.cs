using System;
using System.Linq;
using System.Web.Mvc;
using MagicPharm.Models;
using System.Data.Entity;
using MagicPharm.Models.DataTable;

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
        public ActionResult Index() => View();

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

        /// <summary>
        /// Edit Client PartialView
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            var client = _context.Clients.First(x => x.ID == id);

            return PartialView("~/Views/Clients/Partials/Edit.cshtml", client);
        }

        [HttpPost]
        public JsonResult Edit(Client model)
        {
            var succ = false;
            try
            {
                var client = _context.Clients.First(x => x.ID == model.ID);
                client.FullName = model.FullName;
                client.Phone = model.Phone;
                client.Telephone = model.Telephone;
                _context.SaveChanges();
                succ = true;
            }
            catch (Exception)
            {

            }
            return new JsonResult { Data = new { succ = succ }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}