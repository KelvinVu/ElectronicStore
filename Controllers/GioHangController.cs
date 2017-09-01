using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DienMayws.Models;
using DienMayws.ViewModels;

namespace DienMayws.Controllers
{
     
    public class GioHangController : Controller
    {

        DienMayDbContext db = new DienMayDbContext();
        [HttpPost]
        public ActionResult Them(int sanPhamID, int soLuong =1) 
        {
            var gioHang = Session["GioHang"] as GioHangModel;


            if(gioHang== null)
            {
                gioHang = new GioHangModel();
                Session["GioHang"] = gioHang;

            }

            var sanPhamChonMua = db.SanPhams.Find(sanPhamID);
            var item = new GioHangItem(sanPhamChonMua, soLuong);
            gioHang.Add(item);
            return RedirectToAction("Index");
        }



        // GET: GioHang/Index
        public ActionResult Index()
        {
            // Tham chiếu đến giỏ hàng trong Session
            var gioHang = Session["GioHang"] as GioHangModel;
            if (gioHang == null || gioHang.TongSanPham() == 0)
            {// Giỏ hàng trống, quay về trang chủ
                return RedirectToAction("Index", "Home");
            }
            // Giỏ hàng có thông tin, chỉ định view hiển thị và truyền thông tin sang
            return View(gioHang);
        }



        #region 2 Hiệu chỉnh & Xóa
        [HttpPost]
        public RedirectToRouteResult HieuChinh(int sanPhamID, int soLuong)
        {
            // Tham chiếu đến giỏ hàng lưu trong Session
            var gioHang = Session["GioHang"] as GioHangModel;
            gioHang.Update(sanPhamID, soLuong);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public RedirectToRouteResult Xoa(int sanPhamID)
        {

            // Tham chiếu đến giỏ hàng lưu trong Session
            var gioHang = Session["GioHang"] as GioHangModel;
            gioHang.Remove(sanPhamID);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult DatMua()
        {
            // Gọi view để nhập thông tin HoaDon
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken] // Chống mạo danh
        public ActionResult DatMua(HoaDon hoaDon)
        {
            // Xử lý phát sinh HoaDon và HoaDonChiTiet
            // ...
            // Đặt hàng thành công 
            return View("DatHangThanhCong", hoaDon);
        }

        [ChildActionOnly]
        public PartialViewResult ThongKe()
        {
            var gioHang = Session["GioHang"] as GioHangModel;
            int tongSL = 0;
            if (gioHang != null && gioHang.TongSanPham() > 0) tongSL = gioHang.TongSoLuong();
            return PartialView("_ThongKeGioHangPartial", tongSL);
        }

        #endregion
   
    }
}