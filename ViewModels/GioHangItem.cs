using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using DienMayws.Models;

namespace DienMayws.ViewModels
{
    public class GioHangItem
    {
        // Properties
        public SanPham SanPham { get; set; }
        public int SoLuong { get; set; }
        // Constructors
        public GioHangItem() { }
        public GioHangItem(SanPham sanPham, int soLuong)
        {
            this.SanPham = sanPham;
            this.SoLuong = soLuong;
        }

    }
}