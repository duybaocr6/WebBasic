using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebdoAn.Models;

namespace WebdoAn.Models
{
    public class Giohang
    {
        SalesOnlineEntities1 data = new SalesOnlineEntities1();
        public string sMaSP { set; get; }
        public string sTenSP { set; get; }
        public string sAnhbia { set; get; }
        public double iDonggia { set; get; }
        public int iSoluong { set; get; }
        public Double dThanhtien
        {
            get
            {
                return iSoluong * iDonggia;
            }
        }
        public Giohang(string maSP)
        {
            sMaSP = maSP;
            sanPham sanpham = data.sanPhams.Single(n => n.maSP == sMaSP);
            sTenSP = sanpham.tenSP;
            sAnhbia = sanpham.hinhDD;
            iDonggia = double.Parse(sanpham.giaBan.ToString());
            iSoluong = 1;
        }
    }
}