using System;
using System.Linq;
using System.Web.Mvc;
using MagicPharm.Models;
using System.Data.Entity;
using System.Collections.Generic;
using MagicPharm.Models.DataTable;

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

        /// <summary>
        /// Watches Brands Main Page.
        /// </summary>
        /// <returns></returns>
        public ActionResult Brands() => View();

        /// <summary>
        /// Watches Repairs Maian Page.
        /// </summary>
        /// <returns></returns>
        public ActionResult Repairs() => View();

        /// <summary>
        /// Returns all watches orders 
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// returns All Watch brands as List to choose from.
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
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

        /// <summary>
        /// returns All brands GridSource For DataTable
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// returns all repairs GridSource for DataTable
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// AddOrder Partial View .
        /// </summary>
        /// <returns></returns>
        public ActionResult AddOrder() => PartialView("~/Views/Watches/Partials/AddOrder.cshtml");

        /// <summary>
        /// AddBrand Partial View.
        /// </summary>
        /// <returns></returns>
        public ActionResult AddBrand() => PartialView("~/Views/Watches/Partials/AddBrand.cshtml");

        /// <summary>
        /// AddRepair Partial View.
        /// </summary>
        /// <returns></returns>
        public ActionResult AddRepair() => PartialView("~/Views/Watches/Partials/AddRepair.cshtml");

        /// <summary>
        /// Edit Brand Parial View.
        /// </summary>
        /// <param name="brandId"></param>
        /// <returns></returns>
        public ActionResult EditBrand(int brandId)
        {
            var brand = _context.WatchBrands.First(x => x.ID == brandId);
            return PartialView("~/Views/Watches/Partials/EditBrand.cshtml", brand);
        }

        /// <summary>
        /// Edit Order Partial view
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public ActionResult EditOrder(int orderId)
        {
            var order = _context.WatchOrders.Include("Client").Include("WatchBrand").First(x => x.ID == orderId);
            return PartialView("~/Views/Watches/Partials/EditOrder.cshtml", order);
        }

        /// <summary>
        /// Edit Repair Partial
        /// </summary>
        /// <param name="repairId"></param>
        /// <returns></returns>
        public ActionResult EditRepair(int repairId)
        {
            var statuses = _context.Statuses.Where(x => x.ID >= 1 && x.ID <= 4).Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.ID.ToString()
            }).ToList();
            TempData["statuses"] = statuses.ToList();

            var repair = _context.WatchRepairs.Include("Client").Include("WatchBrand").First(x => x.ID == repairId);
            return PartialView("~/Views/Watches/Partials/EditRepair.cshtml", repair);

        }

        /// <summary>
        /// Watches Repair statuses as list to choose from.
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Add Brand 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Edit Brand
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Add Order
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Add Repair
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Edit Repair model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult EditRepair(WatchRepair model)
        {
            var succ = false;
            try
            {
                var repair = _context.WatchRepairs.First(x => x.ID == model.ID);
                repair.Number = model.Number;
                repair.WatchBrandId = model.WatchBrandId;
                repair.WatchBarcode = model.WatchBarcode;
                repair.Description = model.Description;
                if (model.StatusId != repair.StatusId)
                {
                    if (model.StatusId == 3 && repair.EndDate == null)
                        repair.EndDate = DateTime.Now;
                    else if (model.StatusId == 4 && repair.ReceiptDate == null)
                        repair.ReceiptDate = DateTime.Now;
                }
                repair.StatusId = model.StatusId;
                _context.SaveChanges();
                succ = true;
            }
            catch (Exception ex)
            {

            }
            return new JsonResult { Data = new { succ = succ }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

    }
}