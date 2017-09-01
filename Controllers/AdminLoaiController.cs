using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DienMayws.Models;

namespace DienMayws.Controllers
{
    public class AdminLoaiController : Controller
    {
        #region 1- Biến cục bộ sử dụng trong Controller
        // Khai báo & khởi tạo biến db
        private DienMayDbContext db = new DienMayDbContext();

        // Giải phóng biến db
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        #endregion

        #region 2- Xử lý - Xem thông tin
        // GET: AdminLoai
        public ActionResult Index()
        {
            try
            {
                var loais = db.Loais.Include(l => l.ChungLoai);
                @ViewBag.LoaiAct = "active";
                return View(loais.ToList());
            }
            catch(Exception e)
            {
                object errorMsg = "Không truy cập được dữ liệu.<br/>Lý do: " + e.Message;
                return View("Error", errorMsg);
            }
        }

        // GET: AdminLoai/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null || id <= 0)
            {
                return RedirectToAction("Index");
            }
            try
            {
                //Loai loai = db.Loais.Find(id);
                Loai loai = db.Loais.Include("ChungLoai").SingleOrDefault(p => p.LoaiID == id);
                if (loai == null)
                {
                    object errorMsg = string.Format("ID Loại: <b>{0}</b> không tồn tại!", id);
                    return View("Error", errorMsg);
                }
                return View(loai);
            }
            catch(Exception e)
            {
                object errorMsg = "Không truy cập được dữ liệu.<br/>Lý do: " + e.Message;
                return View("Error", errorMsg);
            }
        }
        #endregion

        #region 3- Xử lý - Thêm thông tin
        // GET: AdminLoai/Create
        public ActionResult Create()
        {
            ViewBag.ChungLoaiID = new SelectList(db.ChungLoais, "ChungLoaiID", "Ten");
            return View();
        }

        // POST: AdminLoai/Create
        [HttpPost]
        [ValidateAntiForgeryToken]//Chống mạo danh
        public ActionResult Create(/*[Bind(Include = "LoaiID,Ten,ChungLoaiID,BiDanh")]*/ Loai loai)
        {
            //Kiểm tra trùng
            int d = db.Loais.Count(p => p.Ten == loai.Ten.Trim());
            if (d > 0) ModelState.AddModelError("Ten", "Tên loại này đã tồn tại.");

            if (ModelState.IsValid)
            {//Xử lý khi nhập dữ liệu hợp lệ
                try
                {
                    loai.BiDanh = XuLyDuLieu.LoaiBoDauTiengViet(loai.Ten);
                    db.Loais.Add(loai);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch(Exception e)
                {
                    object errorMsg = "Không truy cập được dữ liệu.<br/>Lý do: " + e.Message;
                    return View("Error", errorMsg);
                }
                
            }
            //Xử lý khi dữ liệu nhập không hợp lệ
            ViewBag.ChungLoaiID = new SelectList(db.ChungLoais, "ChungLoaiID", "Ten", loai.ChungLoaiID);
            return View(loai);
        }
        #endregion
         
        #region 4- Xử lý - Hiệu chỉnh thông tin
        // GET: AdminLoai/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null || id <= 0)
            {
                return RedirectToAction("Index");
            }
            try
            {
                Loai loai = db.Loais.Find(id);
                if (loai == null) throw new Exception(string.Format("ID Loại: <b>{0}</b> không tồn tại!", id));
                ViewBag.ChungLoaiID = new SelectList(db.ChungLoais, "ChungLoaiID", "Ten", loai.ChungLoaiID);
                return View(loai);
            }
            catch (Exception e)
            {
                object errorMsg = "Không truy cập được dữ liệu.<br/>Lý do: " + e.Message;
                return View("Error", errorMsg);
            }



            //if (id == null || id <= 0)
            //{
            //    object errorMsg = "Dữ liệu truy cập không tồn tại";
            //    return View("Error", errorMsg);
            //}

            ////Loai loai = db.Loais.Find(id);
            //Loai loai = db.Loais.Include("ChungLoai").SingleOrDefault(p => p.LoaiID == id);

            //if (loai == null)
            //{
            //    object errorMsg = string.Format("ID Loại: <b>{0}</b> không tồn tại", id);
            //    return View("Error", errorMsg);
            //}
            //ViewBag.ChungLoaiID = new SelectList(db.ChungLoais, "ChungLoaiID", "Ten", loai.ChungLoaiID);
            //return View(loai);
        }

        // POST: AdminLoai/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(/*[Bind(Include = "LoaiID,Ten,ChungLoaiID,BiDanh")]*/ Loai loai)
        {
            //Kiểm tra trùng
            int d = db.Loais.Count(p => p.LoaiID != loai.LoaiID && p.Ten == loai.Ten.Trim());
            if (d > 0) ModelState.AddModelError("Ten", "Tên loại này đã tồn tại.");

            if (ModelState.IsValid)
            {//Xử lý khi nhập dữ liệu hợp lệ
                try
                {
                    loai.BiDanh = XuLyDuLieu.LoaiBoDauTiengViet(loai.Ten);
                    db.Entry(loai).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch(Exception e)
                {
                    object errorMsg = "Không truy cập được dữ liệu.<br/>Lý do: " + e.Message;
                    return View("Error", errorMsg);
                }
                
            }
            //Xử lý khi dữ liệu nhập không hợp lệ (trở lại view Edit)
            ViewBag.ChungLoaiID = new SelectList(db.ChungLoais, "ChungLoaiID", "Ten", loai.ChungLoaiID);
            return View(loai);



            //if (ModelState.IsValid)
            //{
            //    db.Entry(loai).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //ViewBag.ChungLoaiID = new SelectList(db.ChungLoais, "ChungLoaiID", "Ten", loai.ChungLoaiID);
            //return View(loai);
        }
        #endregion

        #region 5- Xử lý - Xóa thông tin
        // GET: AdminLoai/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null || id <= 0)
            {
                return RedirectToAction("Index");
            }
            try
            {
                Loai loai = db.Loais.Include("ChungLoai").SingleOrDefault(p => p.LoaiID == id);
                if (loai == null) throw new Exception(string.Format("ID Loại: <b>{0}</b> không tồn tại!", id));
                return View(loai);
            }
            catch (Exception e)
            {
                object errorMsg = "Không truy cập được dữ liệu.<br/>Lý do: " + e.Message;
                return View("Error", errorMsg);
            }



            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Loai loai = db.Loais.Find(id);
            //if (loai == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(loai);
        }

        // POST: AdminLoai/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Loai loai = db.Loais.Find(id);
                db.Loais.Remove(loai);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                object errorMsg = "Không hủy được dữ liệu.<br/>Lý do: " + e.Message;
                return View("Error", errorMsg);
            }
            
        }
        #endregion

    }
}
