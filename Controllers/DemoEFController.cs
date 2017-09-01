using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DienMayws.Models;

namespace DienMayws.Controllers
{
    public class DemoEFController : Controller
    {
        DienMayDbContext db = new DienMayDbContext();
        
        // GET: DemoEF
        public ActionResult Index()
        {
            int maxGiaBan = db.SanPhams.Max(p => p.GiaBan);
            var dsSP1 = db.SanPhams
                            .Where(p => p.GiaBan == maxGiaBan)
                            .ToList();

            var dsSP2 = db.SanPhams
                            .Where(p => p.HoaDonChiTiets.Sum(x => x.SoLuong) > 50)
                            .ToList();
            
            return View();
        }
    }
}