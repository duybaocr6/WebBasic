using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebdoAn.Models;

namespace WebdoAn.Models
{
    public class CommonData
    {
        private SalesOnlineEntities1 dc;
        public CommonData()
        {
            this.dc = new SalesOnlineEntities1();
        }
        public IEnumerable<loaiSP> LoaiHang
        {
            get
            {
                return this.dc.loaiSPs;
            }
        }
        public List<string> Nhasanxuat
        {
            get
            {
                List<string> kq = new List<string>();
                foreach (sanPham i in this.dc.sanPhams)
                {
                    if (!kq.Contains(i.nhaSanXuat.Trim()))
                        kq.Add(i.nhaSanXuat.Trim());
                }
                return kq;
            }
        }
        public IEnumerable<sanPham> Sanphammoi(int n)
        {
            return dc.sanPhams.OrderByDescending(x => x.ngayDang).Take(n);
        }
      
    }
}