using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DienMayws.Models;
using DienMayws.ViewModels;
using System.IO;

namespace DienMayws.Controllers
{
    public class AdminSanPhamController : Controller
    {
        private DienMayDbContext db = new DienMayDbContext();

        #region 1- Xem danh sách - sử dụng WebGird
        // GET: AdminSanPham
        public ActionResult Index()
        {
            try
            {
                IEnumerable<SanPham> sanPhams = db.SanPhams
                                                  .Include(s => s.Loai)
                                                  .Include(s => s.NhaSanXuat)
                                                  .ToList();
                //var sanPhams = db.SanPhams.Include(s => s.Loai).Include(s => s.NhaSanXuat);
                return View(sanPhams.ToList());
            }
            catch (Exception e)
            {
                object errorMsg = "Không truy cập được dữ liệu.<br/>Lý do: " + e.Message;
                return View("Error", errorMsg);
            }
        }
        
        // GET: AdminSanPham/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: AdminSanPham/Create
        public ActionResult Create()
        {
            ViewBag.LoaiID = new SelectList(db.Loais, "LoaiID", "Ten");
            ViewBag.NhaSanXuatID = new SelectList(db.NhaSanXuats, "NhaSanXuatID", "Ten");
            return View();
        }

        // POST: AdminSanPham/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SanPhamID,NhaSanXuatID,LoaiID,Ten,TrangThai,MoTa,GiaBan,SoLuong,KichCo,BangTan,Camera,GPRS,XuatXu,DacTinh,Hinh,BiDanh")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LoaiID = new SelectList(db.Loais, "LoaiID", "Ten", sanPham.LoaiID);
            ViewBag.NhaSanXuatID = new SelectList(db.NhaSanXuats, "NhaSanXuatID", "Ten", sanPham.NhaSanXuatID);
            return View(sanPham);
        }
        #endregion


        #region 2: Hiệu chỉnh|Sửa -Ứng dụng hàm Bind (Từ MVC5)


        // GET: AdminSanPham/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null || id <= 0)
            {
                return RedirectToAction("Index");
            }

            try
            {
                SanPham sanPham = db.SanPhams.Find(id);
                if (sanPham == null) //throw new Exception("ID Sản phẩm không tồn tại");
                {
                    object errorMsg = string.Format("ID Sản phẩm <b>{0}</b> không tồn tại!", id);
                    return View("Error", errorMsg);
                }

                ViewBag.LoaiID = new SelectList(db.Loais, "LoaiID", "Ten", sanPham.LoaiID);
                ViewBag.NhaSanXuatID = new SelectList(db.NhaSanXuats, "NhaSanXuatID", "Ten", sanPham.NhaSanXuatID);
                
                // tạo nguồn dữ liệu cho DropdownList TrangThai 

                // Cách 1 (mảng kiểu vô danh - AnonymousType)
                //var lstTrangThai = new[]
                //{
                //    new {TrangThaiID= "bt", Ten="Bình thường"},
                //    new {TrangThaiID= "nb", Ten="Nổi bật"},
                //    new {TrangThaiID= "new", Ten="Mới nhập kho"}
                //};

                // Cách 2:
                TrangThaiModel[] lstTrangThai = new TrangThaiModel[]
                                                    {
                                                        new TrangThaiModel("bt", "Bình thường"),
                                                        new TrangThaiModel("nb", "Nổi bật"),
                                                        new TrangThaiModel("new", "Mới nhập")
                                                    };

                ViewBag.TrangThai = new SelectList(lstTrangThai, "TrangThaiID", "Ten", sanPham.TrangThai);
                // Giả sử thông tin bảo mật là BiDanh (ko truyền dữ liệu lên view)
                sanPham.BiDanh = null;
                return View(sanPham);
            }
            catch(Exception e)
            {
                object errorMsg = "Không truy cập được dữ liệu.<br/>Lý do: " + e.Message;
                return View("Error", errorMsg);
            }
        }

        // POST: AdminSanPham/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SanPhamID,NhaSanXuatID,LoaiID,Ten,TrangThai,GiaBan,SoLuong")] SanPham sanPham)
        {
            // Kiểm tra trùng tên sản phẩm - BTVN
            //...
            if (ModelState.IsValid)
            {// Trường hợp dữ liệu nhập hợp lệ
                try
                {
                    SanPham mSanPham = db.SanPhams.Find(sanPham.SanPhamID);
                    TryUpdateModel(mSanPham, "", new string[] { "SanPhamID", "NhaSanXuatID", "LoaiID", "Ten", "TrangThai", "GiaBan" });
                    mSanPham.BiDanh = XuLyDuLieu.LoaiBoDauTiengViet(mSanPham.Ten);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch(Exception e)
                {
                    object errorMsg = "Không truy cập được dữ liệu.<br/>Lý do: " + e.Message;
                    return View("Error", errorMsg);
                }
                
            }
            // Trường hợp dữ liệu nhập không hợp lệ



            ViewBag.LoaiID = new SelectList(db.Loais, "LoaiID", "Ten", sanPham.LoaiID);
            ViewBag.NhaSanXuatID = new SelectList(db.NhaSanXuats, "NhaSanXuatID", "Ten", sanPham.NhaSanXuatID);
            // Tạo nguồn dữ liệu cho DropdownList TrangThai (mảng kiểu vô danh
            var lstTrangThai = new[]
                {
                    new {TrangThaiID= "bt", Ten="Bình thường"},
                    new {TrangThaiID= "nb", Ten="Nổi bật"},
                    new {TrangThaiID= "new", Ten="Mới nhập kho"}
                };
            ViewBag.TrangThai = new SelectList(lstTrangThai, "TrangThaiID", "Ten", sanPham.TrangThai);
            return View(sanPham);
        }

        #endregion

        #region 3- Xử lý Upload hình sản phẩm

        // POST: AdminSanPham/UploadPhoto/5
        public ActionResult UploadPhoto(int? id)
        {
            if (id == null || id <= 0)
            {
                return RedirectToAction("Index");
            }

            try
            {
                SanPham sanPham = db.SanPhams.Find(id);
                if (sanPham == null)
                {
                    object errorMsg = string.Format("ID Sản phẩm <b>{0}</b> không tồn tại!", id);
                    return View("Error", errorMsg);
                }

                ViewBag.TenSanPham = sanPham.Ten;
                return View(sanPham.SanPhamID);
            }
            catch (Exception e)
            {
                object errorMsg = "Không truy cập được dữ liệu.<br/>Lý do: " + e.Message;
                return View("Error", errorMsg);
            }
        }


        [HttpPost]
        public ActionResult UploadPhoto(int? id, HttpPostedFileBase fileHinh)
        {
            SanPham sanpham = db.SanPhams.Find(id);
            if (fileHinh != null || fileHinh.ContentLength > 0)
            {
                SanPham sanPham = db.SanPhams.Find(id);
                string fileName = string.Format("{0}{1}", sanPham.SanPhamID, Path.GetExtension(fileHinh.FileName));
                string path = Server.MapPath("~/photo/");
                try
                {
                    fileHinh.SaveAs(path + fileName);
                    sanPham.Hinh = fileName;
                    db.Entry(sanPham).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    object errorMsg = "Upload hình không thành công.<br/>Lý do: " + e.Message;
                    return View("Error", errorMsg);
                }
            }
            ViewBag.TenSanPham = sanpham.Ten;
            return View(sanpham.SanPhamID);
        }


        //[HttpPost]
        //public ActionResult UploadPhoto(int? id, HttpPostedFileBase fileHinh)
        //{
        //    if (fileHinh != null || fileHinh.ContentLength > 0)
        //    {
        //        SanPham sanPham = db.SanPhams.Find(id);
        //        string fileName = string.Format("{0}{1}", sanPham.SanPhamID, Path.GetExtension(fileHinh.FileName));
        //        string path = Server.MapPath("~/photo/");
        //        try
        //        {
        //            fileHinh.SaveAs(path + fileName);
        //            sanPham.Hinh = fileName;
        //            db.Entry(sanPham).State = EntityState.Modified;
        //            db.SaveChanges();
        //            return RedirectToAction("Index");
        //        }
        //        catch (Exception e)
        //        {
        //            object errorMsg = "Upload hình không thành công.<br/>Lý do: " + e.Message;
        //            return View("Error", errorMsg);
        //        }
        //    }

        //    return RedirectToAction("UploadPhoto", new { id = id });
        //}

        #endregion

        // GET: AdminSanPham/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: AdminSanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
