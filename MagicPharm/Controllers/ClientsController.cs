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

        public JsonResult GetClients()
        {
            var clients = _context.Clients.Select(x => new
            {
                id = x.ID.ToString(),
                text = x.FullName
            });
            return new JsonResult { Data = new { data = clients }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

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


    }
}