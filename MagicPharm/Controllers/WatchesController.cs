using MagicPharm.Models;
using MagicPharm.Models.DataTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MagicPharm.Controllers
{
    public class WatchesController : Controller
    {
        private readonly ApplicationDbContext _context = null;

        public WatchesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Watches
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAllWatchesOrders()
        {
            var orders = _context.WatchOrders.Include("Client").Include("WatchBrand").ToList();
            var allOrders = orders.Select(x => new WatchOrderGridSource
            {
                ID = x.ID,
                ClientPhone = x.Client.Phone,
                CreationDate = x.CreationDate,
                ClientName = x.Client.FullName,
                WatchBrand = x.WatchBrand.Name,
                GuaranteeDate = x.GuaranteeDate,
                WatchCatalogNumber = x.WatchCatalogNumber,
            });

            return new JsonResult { Data = new { data = allOrders }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetWatchBrands(string searchTerm)
        {
            List<WatchBrand> brands = null;

            if (string.IsNullOrEmpty(searchTerm))
                brands = _context.WatchBrands.ToList();
            else
                brands = _context.WatchBrands.Where(x => x.Name.Contains(searchTerm)).ToList();

            var selectList = brands.Select(x => new
            {
                id = x.ID,
                text = x.Name
            }).ToList();

            return Json(selectList, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddOrder() => PartialView("~/Views/Watches/Partials/AddOrder.cshtml");

        [HttpPost]
        public ActionResult AddOrder(WatchOrder model)
        {
            try
            {
                var client = model.Client;
                var isClientExists = _context.Clients.FirstOrDefault(x=> x.Phone.Equals(client.Phone) && x.FullName.Equals(client.FullName));

                if (isClientExists != null)
                {
                    model.Client = null;
                    model.ClientId = isClientExists.ID;
                    isClientExists.LastOrderDate = DateTime.Now;
                }
                else
                {
                    model.Client.LastOrderDate = DateTime.Now;
                    model.Client.CreationDate = DateTime.Now;
                }
                model.CreationDate = DateTime.Now;
                _context.WatchOrders.Add(model);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Index", "Watches");
        }
    }
}