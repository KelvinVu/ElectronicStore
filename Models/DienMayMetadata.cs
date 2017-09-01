using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
namespace DienMayws.Models
{
    [MetadataTypeAttribute(typeof(ChungLoai.ChungLoaiMetadata))]
    public partial class ChungLoai
    {
        internal sealed class ChungLoaiMetadata
        {
            [Display(Name="ID Chủng loại")]
            public int ChungLoaiID;
            
            [Display(Name = "Tên chủng loại")]
            [Required(ErrorMessage="Tên chủng loại không được để trống.")]
            public string Ten;

            [Display(Name = "Bí danh")]
            public string BiDanh;
        }
    }

    [MetadataTypeAttribute(typeof(HoaDon.HoaDonMetadata))]
    public partial class HoaDon
    {
      
        internal sealed class HoaDonMetadata
        {
            [Display(Name = "ID Hóa đơn")]
            public int HoaDonID;

            [Display(Name = "Ngày đặt hàng")]
            [DisplayFormat(DataFormatString="{0:dd-MM-yyyy}")]
            public System.DateTime NgayDatHang;

            [Display(Name = "Họ tên khách hàng")]
            public string HoTenKhach;

            [Display(Name = "Địa chỉ")]
            //[ScaffoldColumn(false)]
            [DataType(dataType: DataType.MultilineText)]
            public string DiaChi;

            [Display(Name = "Điện thoại")]
            [DataType(DataType.PhoneNumber)]
            public string DienThoai;

            [Display(Name = "Email")]
            [DataType(DataType.EmailAddress)]
            public string Email;

            [Display(Name = "Tổng tiền")]
            [DisplayFormat(DataFormatString="{0:#,##0 VNĐ}")]
            public int TongTien;
        }
    }


    [MetadataTypeAttribute(typeof(Loai.LoaiMetadata))]
    public partial class Loai
    {
        internal sealed class LoaiMetadata
        {
            [Display(Name = "ID Loại")]
            public int LoaiID;

            [Display(Name = "Tên loại")]
            [Required(ErrorMessage="{0} không được để trống")]
            [MaxLength(50, ErrorMessage="{0} tối đa {1} ký tự")]
            [MinLength(2, ErrorMessage = "{0} tối thiểu {1} ký tự")]
            public string Ten;

            [Display(Name = "ID Chủng loại")]
            public Nullable<int> ChungLoaiID;

            [Display(Name = "Bí danh")]
            public string BiDanh;
        }
    }


    [MetadataTypeAttribute(typeof(SanPham.SanPhamMetadata))]
    public partial class SanPham
    {
        internal sealed class SanPhamMetadata
        {
            [Display(Name = "ID Sản phẩm")]
            public int SanPhamID;

            [Display(Name = "Nhà sản xuất")]
            public Nullable<int> NhaSanXuatID;

            [Display(Name = "Loại")]
            public Nullable<int> LoaiID;

            [Display(Name = "Tên sản phẩm")]
            [Required(ErrorMessage = "{0} không được để trống")]
            [MaxLength(50, ErrorMessage = "{0} tối đa {1} ký tự")]
            [MinLength(2, ErrorMessage = "{0} tối thiểu {1} ký tự")]
            public string Ten;

            [Display(Name = "Trạng thái")]
            [MaxLength(5, ErrorMessage = "{0} tối đa {1} ký tự")]
            public string TrangThai;

            [Display(Name = "Mô tả")]
            [MaxLength(250, ErrorMessage = "{0} tối đa {1} ký tự")]
            public string MoTa;

            [Display(Name = "Giá bán")]
            [Required(ErrorMessage = "{0} không được để trống")]
            [DisplayFormat(DataFormatString = "{0:#,##0 VNĐ}")]
            public int GiaBan;

            [Display(Name = "Số lượng")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public int SoLuong;

            [Display(Name = "Kích cỡ")]
            [MaxLength(50, ErrorMessage = "{0} tối đa {1} ký tự")]
            public string KichCo;

            [Display(Name = "Băng tần")]
            [MaxLength(50, ErrorMessage = "{0} tối đa {1} ký tự")]
            public string BangTan;

            [Display(Name = "Xuất xứ")]
            [MaxLength(50, ErrorMessage = "{0} tối đa {1} ký tự")]
            public string XuatXu;

            [Display(Name = "Đặc tính sản phẩm")]
            [MaxLength(50, ErrorMessage = "{0} tối đa {1} ký tự")]
            public string DacTinh;

            [Display(Name = "Hình ảnh")]
            public string Hinh;

            [Display(Name = "Bí danh")]
            public string BiDanh;
        }
    }
}