using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebdoAn.Models;

namespace WebdoAn.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        SalesOnlineEntities1 nd = new SalesOnlineEntities1();
        // GET: Register
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Resgister()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Resgister(FormCollection collection, taiKhoanTV kh)
        {
            var ho = collection["HoKH"];
            var ten = collection["TenKH"];
            var tendn = collection["TenDN"];
            var gt = collection["Gioitinh"];
            var matkhau = collection["Matkhau"];
            var nhaplaimatkhau = collection["Nhaplaimatkhau"];
            var diachi = collection["Diachi"];
            var email = collection["Email"];
            var dienthoai = collection["Dienthoai"];
            var ngaysinh = String.Format("{0:MM/dd/yyyy}", collection["Ngaysinh"]);
            if (String.IsNullOrEmpty(ho))
            {
                ViewData["Loi1"] = "Họ không được để trống";
            }
            else if (String.IsNullOrEmpty(ten))
            {
                ViewData["Loi2"] = "Tên không được để trống";
            }
            else if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi3"] = "Vui lòng nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi4"] = "Vui lòng nhập mật khẩu";
            }
            else if (String.IsNullOrEmpty(nhaplaimatkhau))
            {
                ViewData["Loi5"] = "Vui lòng nhập lại mật khẩu";
            }
            else if (String.IsNullOrEmpty(email))
            {
                ViewData["Loi6"] = "Email không được bỏ trống";
            }
            else if (String.IsNullOrEmpty(dienthoai))
            {
                ViewData["Loi7"] = "Vui lòng nhập số điện thoại";
            }
            else
            {
                kh.hoDem = ho;
                kh.tenTV = ten;
                kh.taiKhoan = tendn;
                kh.matKhau = matkhau;
                kh.email = email;
                kh.diaChi = diachi;
                kh.soDT = dienthoai;
                kh.ngaysinh = DateTime.Parse(ngaysinh);
                nd.taiKhoanTVs.Add(kh);
                nd.SaveChanges();
                return RedirectToAction("Login");
            }
            return this.Resgister();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var tendn = collection["TenDN"];
            var matkhau = collection["Matkhau"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
            }
            else
            {
                taiKhoanTV kh = nd.taiKhoanTVs.SingleOrDefault(x => x.taiKhoan == tendn && x.matKhau == matkhau);
                if (kh != null)
                {
                    // ViewBag.Thongbao = "Chúc mừng đăng nhập thành công";
                    Session["Taikhoan"] = kh;
                    return RedirectToAction("Index", "Home");
                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return View();
        }

    }
}