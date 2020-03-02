using MagicPharm.Models;
using MagicPharm.Models.DataTable;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        public ActionResult Index() => View();

        public ActionResult Brands() => View();

        public ActionResult Repairs() => View();

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

        public JsonResult GetAllBrands()
        {
            var brands = _context.WatchBrands.Select(x => new WatchBrandGridSource
            {
                ID = x.ID,
                Name = x.Name,
                Symbol = x.Symbol,
                Barcode = x.Barcode
            });

            return new JsonResult { Data = new { data = brands }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetAllRepairs()
        {
            var repairs = _context.WatchRepairs.Include("Client").Include("Status").Include("WatchBrand").Select(x => new WatchRepairGridSource
            {
                ID = x.ID,
                ClientName = x.Client.FullName,
                ClientPhone = x.Client.Phone,
                WatchBrand = x.WatchBrand.Name,
                WatchBarcode = x.WatchBarcode,
                CreationDate = x.CreationDate,
                EndDate = x.EndDate,
                ReceiptDate = x.ReceiptDate,
                Status = x.Status.Name,
                Description = x.Description,
            }).ToList();

            return new JsonResult { Data = new { data = repairs }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult AddOrder() => PartialView("~/Views/Watches/Partials/AddOrder.cshtml");

        public ActionResult AddBrand() => PartialView("~/Views/Watches/Partials/AddBrand.cshtml");

        public ActionResult AddRepair() => PartialView("~/Views/Watches/Partials/AddRepair.cshtml");

        public ActionResult EditBrand(int brandId)
        {
            var brand = _context.WatchBrands.First(x => x.ID == brandId);
            return PartialView("~/Views/Watches/Partials/EditBrand.cshtml", brand);
        }

        public JsonResult GetWatchRepairStatuses(string searchTerm)
        {
            List<Status> statuses = null;

            if (string.IsNullOrEmpty(searchTerm))
                statuses = _context.Statuses.Where(x => x.ID >= 1 && x.ID <= 4).ToList();
            else
                statuses = _context.Statuses.Where(x => x.Name.Contains(searchTerm) && x.ID >= 1 && x.ID <= 4).ToList();

            var selectList = statuses.Select(x => new
            {
                id = x.ID,
                text = x.Name
            }).ToList();

            return Json(selectList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddBrand(WatchBrand model)
        {
            var succ = false;
            try
            {
                var isExists = _context.WatchBrands.Any(x => x.Name.Equals(model.Name));

                if (!isExists)
                {
                    _context.WatchBrands.Add(model);
                    _context.SaveChanges();
                    succ = true;
                }

            }
            catch (Exception ex)
            {

            }
            return new JsonResult { Data = new { succ = succ }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult EditBrand(WatchBrand model)
        {
            var succ = false;
            try
            {
                _context.Entry(model).State = EntityState.Modified;
                _context.SaveChanges();
                succ = true;
            }
            catch (Exception ex)
            {

            }
            return new JsonResult { Data = new { succ = succ }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult AddOrder(WatchOrder model)
        {
            var succ = false;
            try
            {

                if (model.Client.ID != 0)
                {
                    var client = _context.Clients.First(x => x.ID == model.Client.ID);
                    model.ClientId = client.ID;
                    if (!string.IsNullOrEmpty(model.Client.Telephone))
                        client.Telephone = model.Client.Telephone;
                    client.Phone = model.Client.Phone;
                    client.LastOrderDate = DateTime.Now;
                    model.Client = null;
                }
                else
                {
                    model.Client.LastOrderDate = DateTime.Now;
                    model.Client.CreationDate = DateTime.Now;
                }
                model.CreationDate = DateTime.Now;
                _context.WatchOrders.Add(model);
                _context.SaveChanges();
                succ = true;
            }
            catch (Exception ex)
            {

            }
            return new JsonResult { Data = new { succ = succ }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        [HttpPost]
        public JsonResult AddRepair(WatchRepair model)
        {
            var succ = false;
            try
            {

                if (model.Client.ID != 0)
                {
                    model.ClientId = model.Client.ID;
                    model.Client = null;
                }
                else
                    model.Client.CreationDate = DateTime.Now;

                model.CreationDate = DateTime.Now;
                model.StatusId = 1;

                _context.WatchRepairs.Add(model);
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