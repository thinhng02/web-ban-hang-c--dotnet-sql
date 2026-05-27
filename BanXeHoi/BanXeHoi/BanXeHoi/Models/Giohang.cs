using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BanXeHoi.Models
{
    public class Giohang
    {
        dbBanXeDataContext data = new dbBanXeDataContext();
        public int maxe { get; set; }

        [Display(Name = "Tên xe")]
        public string tenxe { get; set; }

        [Display(Name = "Ảnh bìa")]
        public string hinh { get; set; }
        [Display(Name = "Giá bán")]
        public Double giaban { get; set; }
        [Display(Name = "Số lượng")]
        public int iSoluong { get; set; }
        [Display(Name = "Thành tiền")]
        public Double dThanhtien
        {
            get { return iSoluong * giaban; }
        }
        public Giohang(int id)
        {
            maxe = id;
            XeHoi xeHoi = data.XeHois.Single(n => n.maxe == maxe);
            tenxe = xeHoi.tenxe;
            hinh = xeHoi.hinh;
            giaban = double.Parse(xeHoi.giaban.ToString());
            iSoluong = 1;
        }
    }
}