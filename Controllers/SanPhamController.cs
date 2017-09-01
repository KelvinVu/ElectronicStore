using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;


using DienMayws.Models;
namespace DienMayws.Controllers
{
    public class SanPhamController : Controller
    {
        DienMayDbContext db = new DienMayDbContext();
        // GET: SanPham
        [ChildActionOnly]
        public PartialViewResult _MayGiatMoiPartial()
        {
            // Đọc 3 sản phẩm mới nhất có LoaiID = 12 (Máy giặt)
            List<SanPham> items = db.SanPhams
                                   .Where(p => p.LoaiID == 12)
                                   .OrderByDescending(p => p.SanPhamID)
                                   .Take(3)
                                   .ToList();
            // Chỉ định Partial view mặc định hiển thị và truyền dữ liệu sang
            return PartialView(items);
        }



        [ChildActionOnly]
        public PartialViewResult _SanPhamMoiPartial(int id)
        {
            // Đọc 3 sản phẩm mới nhất có LoaiID =id được truyền vào (Máy giặt)
            List<SanPham> items = db.SanPhams
                                   .Where(p => p.LoaiID == id)
                                   .OrderByDescending(p => p.SanPhamID)
                                   .Take(3)
                                   .ToList();
            // Chỉ định Partial view mặc định hiển thị và truyền dữ liệu sang
            return PartialView(items);
        }

        public ViewResult TimKiem(string GiaTriTim)
        {
            List<SanPham> items = db.SanPhams.Where(p => p.Ten.Contains(GiaTriTim)).ToList();
            return View("KetQuaTimKiem", items);
        }

       
        
        [ChildActionOnly]

        public PartialViewResult _SanPhamNoiBatPartial()
        {
            var item = db.SanPhams
                .Where(p => p.TrangThai == "nb")
                .OrderByDescending(p => p.SanPhamID)
                .Take(3).ToList();


            
            return PartialView(item);
            //Neu đứng trên View truyền từ Model Object
            //neu dung Model thi truyen tu tham  so model


        }

        //GET: SanPham/DanhSach/3
        //Dùng WebGrid
        public ViewResult DanhSach2(int? id)
        {
            IEnumerable<SanPham> dsSanPham = null;
            if (id == null || id <= 0)
            {
                dsSanPham = db.SanPhams

                    .Where(p => p.TrangThai == "new")
                    .OrderByDescending(p => p.SanPhamID)
                    .Take(24);
               
                ViewBag.TieuDe = "Sản phẩm mới";
            }
            else
            {
                Loai loai = db.Loais.Find(id);
                if (loai != null)
                {
                    ViewBag.TieuDe = "Sản phẩm loại - " + loai.Ten;
                    //lay cac san pham thoa theo loai ID dc yeu cau 
                    dsSanPham = db.SanPhams
                    .Where(p => p.LoaiID == id);
                }
                else
                {
                    ViewBag.TieuDe = string.Format("Loại ID {0} không tồn tại", id);
                }
            }

            // Chỉ định view mac đinh65
            return View(dsSanPham);
            //return View("List", sanPhams);//pt6

        }


         public ViewResult DanhSach3(int? id)
        {
            //IEnumerable<SanPham> dsSanPham = null;
            if (id == null || id <= 0)
            {
              var   dsSanPham = db.SanPhams
                   
                    .Where(p => p.TrangThai == "new")
                    .OrderByDescending(p => p.SanPhamID)
                    .Take(24)
                    .AsEnumerable()
                    .Select(p => new { p.SanPhamID, p.Ten, DonGia=p.GiaBan.ToString("#,##0VND"), p.Hinh, NoiDungMoTa = p.MoTa == null ? "" :p.MoTa.Length<=20? p.MoTa: p.MoTa.Substring(0,4) });
               
                ViewBag.TieuDe = "Sản phẩm mới";

                   return View(dsSanPham);
            }
            else
            {
                Loai loai = db.Loais.Find(id);
                if (loai != null)
                {
                    ViewBag.TieuDe = "Sản phẩm loại - " + loai.Ten;
                    //lay cac san pham thoa theo loai ID dc yeu cau 
                    //Neu khoing co where co take phai la Ieunumrable
                 var   dsSanPham = db.SanPhams
                    .Where(p => p.LoaiID == id)
                    .AsEnumerable()
                   .Select(p => new { p.SanPhamID, p.Ten, DonGia = p.GiaBan.ToString("#,##0VND"), p.Hinh, NoiDungMoTa = p.MoTa == null ? "" : p.MoTa.Length <= 20 ? p.MoTa : p.MoTa.Substring(0, 4) });
                       return View(dsSanPham);
                }
                else
                {
                    ViewBag.TieuDe = string.Format("Loại ID {0} không tồn tại", id);
                   return View();
                }
            }
             
            //return View("List", sanPhams);//pt6

        }

        public ViewResult DanhSach(int? id,int? page)
        {  
            
            var pageNumber = page ?? 1;
            int pageSize = 4; // Số sản phẩm  trên 1 trang
          


            IPagedList<SanPham> sanPhams = null;
            if (id == null || id <= 0)
            {
                //Lấy 24 sản phẩm mới
                sanPhams = db.SanPhams.OrderByDescending(p => p.SanPhamID).Take(24).ToPagedList(pageNumber,pageSize);
                ViewBag.TieuDe = "sản phẩm mới";
                ViewBag.LoaiID = id;
            }
            else
            {
                Loai loai = db.Loais.Find(id);
                if (loai != null)
                {
                    //Lấy các sản phẩm thỏa theo LoaiID được yêu cầu
                    sanPhams = db.SanPhams.Where(p => p.LoaiID == id).OrderByDescending(p=>p.SanPhamID).ToPagedList(pageNumber, pageSize);


                }

                else
                {
                    ViewBag.TieuDe = string.Format("Loai ID: {0} không tồn tại", id);

                }


            }

            ViewBag.LoaiID = id;

            //Chỉ định View 'List' hiển thị và truyền dữ liệu sang
            return View("List", sanPhams);//method 6

        }


        // GET: AdminLoai/ChiTiet/5
        public ActionResult ChiTiet(int? id)
        {
            if (id == null || id <= 0)
            {
                return RedirectToAction("Index","Home");
            }
            try
            {
                //Loai loai = db.Loais.Find(id);
                SanPham sanPham = db.SanPhams.Include("Loai").Include("NhaSanXuat")
                                                .SingleOrDefault(p => p.SanPhamID == id);
                if (sanPham == null)
                {
                    object errorMsg = string.Format("ID Sản phẩm: <b>{0}</b> không tồn tại!", id);
                    return View("ThongBao", errorMsg);
                }
                return View(sanPham);
            }
            catch (Exception e)
            {
                object errorMsg = "Không truy cập được dữ liệu.<br/>Lý do: " + e.Message;
                return View("ThongBao", errorMsg);
            }
        }
      

    }
}