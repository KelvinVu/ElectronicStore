using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DienMayws.Models;
namespace DienMayws.Controllers
{
    public class ChungLoaiController : Controller
    {
        DienMayDbContext db = new DienMayDbContext();

       
        
        
        // GET: ChungLoai
         [ChildActionOnly]
        public PartialViewResult _ChungLoaiPartial()
        {
            //Kieu tuong minh
            List<ChungLoai> items = db.ChungLoais.Include("Loais").ToList();
            ViewBag.ChungLoais = items;

                ////Kieu Dynamic
              //ViewBag.ChungLoais  = db.ChungLoais.Include("Loai").ToList();


            return PartialView();

        }
    }
}