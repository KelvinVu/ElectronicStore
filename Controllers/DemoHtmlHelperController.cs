using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DienMayws.Models;
namespace DienMayws.Controllers
{
    public class DemoHtmlHelperController : Controller
    {
        DienMayDbContext db = new DienMayDbContext();
        // GET: DemoHtmlHelper
        public ActionResult Index()
        {
            return View();
        }

        // GET: DemoHtmlHelper/ExDisplay
        public ViewResult ExDisplay()
        {
            HoaDon item = db.HoaDons.Find(1);
            return View(item);
        }
        // GET: DemoHtmlHelper/ExDisplayForModel
        public ViewResult ExDisplayForModel()
        {
            HoaDon item = db.HoaDons.Find(1);
            return View(item);
        }
    }
}