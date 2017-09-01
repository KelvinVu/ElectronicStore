using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DienMayws.Models;
using PagedList;
namespace DienMayws.Controllers
{


    
    public class DemoController : Controller
    {

        DienMayDbContext db = new DienMayDbContext();

        //Demo PageList

        public ViewResult EXPageList(int? page)
        {
            //var pageNumber = page ?? 1;

            int pageNumber = (page == null ? 1 : page.Value);
            int pageSize = 12; // Số sản phẩm  trên 1 trang
            var onePageOfProducts = db.SanPhams
                                        .OrderByDescending(p => p.SanPhamID)
                                        .ToPagedList(pageNumber, pageSize);
                                        


            ViewBag.OnePageOfProducts = onePageOfProducts;
            return View();


        }


        public ViewResult EXPageList2(int? page)
        {
            //var pageNumber = page ?? 1;

            int pageNumber = (page == null ? 1 : page.Value);
            int pageSize = 6; // Số sản phẩm  trên 1 trang
            var onePageOfProducts = db.SanPhams
                                        .OrderByDescending(p => p.SanPhamID)
                                        .ToPagedList(pageNumber, pageSize);



            ViewBag.OnePageOfProducts = onePageOfProducts;
            return View();


        }



        // GET: Demo
        public ActionResult Index()
        {
            bool? p=null;
            var kq1 = p ?? null;
            var kq2 = p ?? true;

            var kq3 = p ?? false;

            int? n = null;
            var kq4 = n ?? null;
            var kq5 = n ?? 1;
            n = 3;
            var kq6 = n ?? 1;

            return View();
        
        }


      
        // GET: Demo/TestModel
        public ActionResult TestModel()
        {
            DienMayDbContext db = new DienMayDbContext();
            List<Loai> items = db.Loais.ToList();

            return View(items);
        }

        // GET: Demo/TestDienMayLayout
        public ActionResult TestDienMayLayout()
        {
            return View();
        }

        // GET: Demo/TestMyLayout
        public ActionResult TestMyLayout()
        {
            return View();
        }
        public ActionResult TestAdminMyLayout()
        {
            return View();
        }
    }
}