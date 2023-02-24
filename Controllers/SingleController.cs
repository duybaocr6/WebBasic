using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebdoAn.Models;

namespace WebdoAn.Controllers
{
    public class SingleController : Controller
    {
        // GET: Single
        SalesOnlineEntities1 data = new SalesOnlineEntities1();
        public ActionResult Index(string id)
        {
            var sanpham = from s in data.sanPhams
                          where s.maSP == id
                          select s;
            return View(sanpham.Single());
        }
    }
}