using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using DienMayws.Models;
namespace DienMayws.Controllers
{
    public class AdminChungLoaiController : Controller
    {
        DienMayDbContext db = new DienMayDbContext();
        // GET: AdminChungLoai
        public ActionResult Index()
        {
            // Lấy tất cả thông tin từ bảng ChungLoai
            List<ChungLoai> items = db.ChungLoais.ToList();

            ViewBag.ChungLoaiAct = "active";
            // Chỉ định view mặc định hiển thị và truyền dữ liệu sang
            return View(items);
        }

        public ActionResult Delete(int id)
        {
            ChungLoai item = db.ChungLoais.Find(id);
            db.ChungLoais.Remove(item);
            db.SaveChanges();
            // Xóa thành công, gọi action Index
            return RedirectToAction("Index");
        }

        //GET: AdminChungLoai/Edit/id
        public ViewResult Edit(int id)
        {
            ChungLoai item = db.ChungLoais.Find(id);
            return View(item);
        }
        [HttpPost]
        public ActionResult Edit(ChungLoai chungLoaiEdited)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chungLoaiEdited).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                // Lưu thành công, gọi action Index
                return RedirectToAction("Index");
            }
            return View(chungLoaiEdited);
        }

        #region Xử lý thêm
        // GET: AdminChungLoai/AddItem
        public ViewResult AddItem()
        {
            return View();
        }

        // cách 1:
        [HttpPost]
        public ActionResult AddItem(ChungLoai chungLoaiMoi)
        {
            db.ChungLoais.Add(chungLoaiMoi);
            db.SaveChanges();
            // Lưu thành công, gọi action Index (để trở về View xem Danh sách chủng loại)
            return RedirectToAction("Index");
        }

        //cách 2:
        //[HttpPost]
        //public RedirectToRouteResult AddItem(string txtTen, string txtBiDanh)
        //{
        //    ChungLoai chungLoaiMoi = new ChungLoai();
        //    chungLoaiMoi.Ten = txtTen;
        //    chungLoaiMoi.BiDanh = txtBiDanh;
        //    db.ChungLoais.Add(chungLoaiMoi);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

       

        #endregion
    }
}