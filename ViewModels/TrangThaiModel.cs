using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DienMayws.ViewModels
{
    public class TrangThaiModel
    {
        public string TrangThaiID { get; set; }
        public string Ten { get; set; }

        public TrangThaiModel(string trangThaiID, string ten)
        {
            this.TrangThaiID = trangThaiID;
            this.Ten = ten;
        }
    }
}