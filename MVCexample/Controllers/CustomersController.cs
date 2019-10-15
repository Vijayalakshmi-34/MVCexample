using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCexample.Models;
using System.Data.Entity;

namespace MVCexample.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {
        private ApplicationDbContext DbContext = null;
        public CustomersController()
        {
            DbContext = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DbContext.Dispose();
            }
        }
        [AllowAnonymous]
        // GET: Customers
        public ActionResult Index()
        {
            // List<Customer> customers = GetCustomers(); //instead of List<Customer> we can use var

            var customers = DbContext.Customers.Include(m => m.MembershipType).ToList();
            return View("DisplayCustomer", customers);
        }

        //public List<Customer> GetCustomers()
        //{
        //    List<Customer> customers = new List<Customer>
        //    {
        //        new Customer{ID=1,CustomerName="Viji",BirthDate=Convert.ToDateTime("17/07/1998"),Gender="Female",MobileNumber=9677695719},
        //        new Customer{ID=2,CustomerName="Hari",BirthDate=Convert.ToDateTime("25/09/2004"),Gender="Male",MobileNumber=9874560321},
        //        new Customer{ID=3,CustomerName="Maha",BirthDate=Convert.ToDateTime("02/11/1999"),Gender="Female",MobileNumber=7708965231}
        //    };

        //    return customers;
        //}
        public ActionResult Details(int id)
        {
            var customers = DbContext.Customers.Include(m => m.MembershipType).ToList().SingleOrDefault(a => a.ID == id);
            return View(customers);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var Customers = new Customer();
            ViewBag.Gender = ListGender();
            ViewBag.MembershipTypeID = ListMemberShip();
            return View(Customers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Create(Customer CustomerFromView)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.Gender = ListGender();
                ViewBag.MembershipTypeID = ListMemberShip();
                return View(CustomerFromView);
            }

            DbContext.Customers.Add(CustomerFromView);
            DbContext.SaveChanges();
            return RedirectToAction("Index", "Customers");
        }

        [HttpGet]
        public ActionResult EditCustomer(int id)
        {
            var Customers = DbContext.Customers.FirstOrDefault(c=>c.ID==id);
            if(Customers!=null)
            {
                ViewBag.Gender = ListGender();
                ViewBag.MembershipTypeID = ListMemberShip();
                return View(Customers);
            }
            return HttpNotFound("Invalid Customer ID");
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult EditCustomer(Customer customerFromView)
        {
            if(ModelState.IsValid)
            {
                var customerInDB = DbContext.Customers.FirstOrDefault(c => c.ID == customerFromView.ID);

                customerInDB.CustomerName = customerFromView.CustomerName;
                customerInDB.BirthDate = customerFromView.BirthDate;
                customerInDB.Gender = customerFromView.Gender;
                customerInDB.MobileNumber = customerFromView.MobileNumber;
                customerInDB.City = customerFromView.City;
                customerInDB.MembershipTypeID = customerFromView.MembershipTypeID;

                DbContext.SaveChanges();
                return RedirectToAction("Index", "Customers");

            }
            else
            {
                ViewBag.Gender = ListGender();
                ViewBag.MembershipTypeID = ListMemberShip();
                return View(customerFromView);
            }
            
        }
        [HttpGet]
        
        public ActionResult RemoveCustomer(int id)
        {
            var Customers = DbContext.Customers.FirstOrDefault(c => c.ID == id);
            if (Customers != null)
            {
                return View(Customers);
            }
            return HttpNotFound("Invalid Customer ID");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult RemoveCustomer(Customer customerFromView)
        {
            var customerInDB = DbContext.Customers.FirstOrDefault(c => c.ID == customerFromView.ID);

            DbContext.Customers.Remove(customerInDB);
            DbContext.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }
        
        public IEnumerable<SelectListItem> ListGender()
        {
            var gender = new List<SelectListItem>
            {
                new SelectListItem{Text="Select a gender",Value="0",Disabled=true,Selected=true},
                new SelectListItem{Text="Male",Value="Male" },
                new SelectListItem{Text="Female",Value="Female" },
                new SelectListItem{Text="Others",Value="Others" }
            };
            return gender;
        }

        [NonAction]
        public IEnumerable<SelectListItem> ListMemberShip()
        {
            var Membership = (from m in DbContext.membershipTypes.AsEnumerable()
                              select new SelectListItem
                              {
                                  Text = m.Type,
                                  Value = m.ID.ToString()
                              }).ToList();
            
            Membership.Insert(0, new SelectListItem { Text = "---Select---", Value = "0",Disabled=true,Selected=true });

            return Membership;
        }
    }
}
   
