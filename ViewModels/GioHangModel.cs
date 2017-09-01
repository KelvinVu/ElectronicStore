using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DienMayws.ViewModels
{
    public class GioHangModel
    {
        // Field - Biến thành viên
        private List<GioHangItem> _items = new List<GioHangItem>();

        // Property
        public List<GioHangItem> Items
        {
            get { return _items; }
        }

        // Methods
        public void Add(GioHangItem item)
        {
            
            var gioHangItem = _items.Find(p => p.SanPham.SanPhamID == item.SanPham.SanPhamID);
            if(gioHangItem==null)
            {
                _items.Add(item);
            }
            else
            {
                gioHangItem.SoLuong += item.SoLuong;
            }
        }

        public void Update(int id,int soLuong)
        {
            var gioHangItem = _items.Find(p => p.SanPham.SanPhamID == id);
            gioHangItem.SoLuong = soLuong;
        }

        public void Remove(int id)
        {
            var gioHangItem = _items.Find(p => p.SanPham.SanPhamID == id);
            _items.Remove(gioHangItem);
        }

        public int TongSoLuong()
        {
            int kq = 0;
            kq = _items.Sum(p => p.SoLuong);
            return kq;
        }
        public int TongTriGia()
        {
            int kq = 0;
            kq = _items.Sum(p => (p.SoLuong*p.SanPham.GiaBan));
            return kq;
        }
        public int TongSanPham()
        {
            return _items.Count;
        }
        public void Clear()
        {
            _items.Clear();
        }
    }
}