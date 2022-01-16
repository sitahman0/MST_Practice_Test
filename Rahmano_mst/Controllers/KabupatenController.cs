using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Rahmano_mst.Models;

namespace Rahmano_mst.Controllers
{
    public class KabupatenController : Controller
    {
        kabupatenDB kabdb = new kabupatenDB();
        List<kabupaten> lprov = new List<kabupaten>();
        isiList lst = new isiList();
        public ActionResult Index()
        {
            ViewBag.provinsi = new SelectList(lst.lstProvinsi(), "provinsi_id", "provinsi_name", 0);
            ViewBag.kabupaten = "";
            return View();
        }
        public JsonResult SearchList(int id, string kab)
        {
            return Json(new List<kabupaten>(kabdb.listKabupaten(id, kab)));
        }
        public ActionResult Edit(int id = 0)
        {
            kabupaten kbt = kabdb.dataKabupaten(id);
            ViewBag.provinsi = new SelectList(lst.lstProvinsi(), "provinsi_id", "provinsi_name", kbt.provinsi_id);
            return View(kbt);
        }
        public JsonResult Simpan()
        {
            if (Session["UID"] == null) { Session["UID"] = 1; }
            var serializer = new JavaScriptSerializer();
            kabupaten kab = serializer.Deserialize<kabupaten>(Request["kabupaten"].ToString());

            return Json(kabdb.simpanKabupaten(kab, Convert.ToInt16(Session["UID"])), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Hapus(int id)
        {
            if (Session["UID"] == null) { Session["UID"] = 1; }
            return Json(kabdb.hapusKabupaten(id, Convert.ToInt16(Session["UID"])), JsonRequestBehavior.AllowGet);
        }

    }
}
