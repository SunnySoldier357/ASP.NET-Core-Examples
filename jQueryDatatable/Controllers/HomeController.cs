using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using jQueryDatatable.Models;
using Microsoft.AspNetCore.Mvc;

namespace jQueryDatatable.Controllers
{
    public class HomeController : Controller
    {
        //* Private Properties
        private DatabaseContext _db;

        //* Constructors
        public HomeController(DatabaseContext db) => _db = db;

        //* Public Methods
        public IActionResult Index() => View();

        public IActionResult LoadData()
        {
            try
            {
                string draw = HttpContext.Request.Form["draw"].FirstOrDefault();

                // Skip number of Rows count
                string start = Request.Form["start"].FirstOrDefault();

                // Paging length 10, 20
                string length = Request.Form["length"].FirstOrDefault();

                // Sort column name
                string sortColumn = Request.Form["columns[" +
                    Request.Form["order[0][column]"].FirstOrDefault() +
                    "][name]"].FirstOrDefault();

                // Sort column direction (asc, desc)
                string sortColumnDirection = Request.Form["order[0][dir]"]
                    .FirstOrDefault();

                // Search value from (search box)
                string searchValue = Request.Form["search[value]"].FirstOrDefault();

                // Paging Size (10, 20, 50, 100)
                int pageSize = null != length ? Convert.ToInt32(length) : 0;

                int skip = null != start ? Convert.ToInt32(start) : 0;

                int recordsTotal = 0;

                // Getting all Customer data
                var customerData = from c in _db.Customers
                                   select c;
                // Sorting
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                    customerData = customerData.OrderBy(sortColumn + " " + sortColumnDirection);

                // Search
                if (!string.IsNullOrEmpty(searchValue))
                    customerData = customerData.Where(c => c.Name == searchValue);

                // Total number of rows
                recordsTotal = customerData.Count();

                // Paging
                var data = customerData
                    .Skip(skip)
                    .Take(pageSize)
                    .ToList();

                // Returning Json Data
                return Json(new
                {
                    draw,
                    recordsFiltered = recordsTotal,
                    recordsTotal,
                    data
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    return RedirectToAction("Index", "Home");

                return View();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    return RedirectToAction("Index", "Home");

                int result = 0;

                return Json(data: result > 0);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}