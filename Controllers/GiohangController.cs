using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebdoAn.Models;

namespace WebdoAn.Controllers
{
    public class GiohangController : Controller
    {
        // GET: Giohang
        SalesOnlineEntities1 data = new SalesOnlineEntities1();
        public List<Giohang> Laygiohang()
        {
            List<Giohang> listGiohang = Session["Giohang"] as List<Giohang>;
            if (listGiohang == null)
            {
                listGiohang = new List<Giohang>();
                Session["Giohang"] = listGiohang;
            }
            return listGiohang;
        }
        public ActionResult ThemGioHang(string sMaSP, string strURL)
        {
            List<Giohang> listGiohang = Laygiohang();
            Giohang sanpham = listGiohang.Find(n => n.sMaSP.Equals(sMaSP));
            if (sanpham != null)
            {
                sanpham.iSoluong++;
                return Redirect(strURL);
            }
            else
            {
                sanpham = new Giohang(sMaSP);
                listGiohang.Add(sanpham);
                return Redirect(strURL);
            }
        }
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                iTongSoLuong = lstGiohang.Sum(n => n.iSoluong);
            }
            return iTongSoLuong;
        }
        private double TongTien()
        {
            double iTongTien = 0;
            List<Giohang> lstGiohang = Session["Giohang"] as List<Giohang>;
            if (lstGiohang != null)
            {
                iTongTien = lstGiohang.Sum(n => n.dThanhtien);
            }
            return iTongTien;
        }
        public ActionResult Giohang()
        {
            List<Giohang> lstGiohang = Laygiohang();
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(lstGiohang);
        }
        public ActionResult GiohangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return PartialView();
        }

        public ActionResult XoaGiohang(string iMaSP)
        {
            List<Giohang> lstGiohang = Laygiohang();
            Giohang sanpham = lstGiohang.SingleOrDefault(n => n.sMaSP == iMaSP);
            if (sanpham != null)
            {
                lstGiohang.RemoveAll(n => n.sMaSP == iMaSP);
                return RedirectToAction("Giohang");
            }
            if (lstGiohang.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("Giohang");
        }
        public ActionResult CapnhatGiohang(string iMaSP ,FormCollection f)
        {
            List<Giohang> lstGiohang = Laygiohang();
            Giohang sanpham = lstGiohang.SingleOrDefault(n => n.sMaSP == iMaSP);
            if (sanpham != null)
            {
                sanpham.iSoluong = int.Parse(f["txtSoluong"].ToString());
            }
            return RedirectToAction("Giohang");
        }
        public ActionResult XoaTatcaGiohang()
        {
            List<Giohang> lstGiohang = Laygiohang();
            lstGiohang.Clear();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult Dathang()
        {
            if (Session["Taikhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("Login", "User");
            }
            if (Session["Giohang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            List<Giohang> lstGiohang = Laygiohang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return View(lstGiohang);
        }
        public ActionResult Dathang(FormCollection collection)
        {
            //Them don hang
            donHang ddh = new donHang();
            taiKhoanTV tk = (taiKhoanTV)Session["Taikhoan"];
            List<Giohang> gh = Laygiohang();
            ddh.ghiChu = tk.ghiChu;
            ddh.diaChiGH = tk.diaChi;
            ddh.ngayDat = DateTime.Now;
            var ngaygiao = String.Format("{0:MM/dd/yyyy}", collection["Ngaygiao"]);
            ddh.ngayGH = DateTime.Parse(ngaygiao);
            ddh.trangThai = 0;
            ddh.daKichHoat = true;
            data.donHangs.Add(ddh);
            Session["Giohang"] = null;
            return RedirectToAction("Xacnhandonhang", "Giohang");
        }
        public ActionResult Xacnhandonhang()
        {
            return View();
        }
    }
}