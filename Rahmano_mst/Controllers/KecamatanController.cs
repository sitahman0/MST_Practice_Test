using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Rahmano_mst.Models;

namespace Rahmano_mst.Controllers
{
    public class KecamatanController : Controller
    {
        kecamatanDB kabdb = new kecamatanDB();
        List<kecamatan> lprov = new List<kecamatan>();
        isiList lst = new isiList();
        public ActionResult Index()
        {
            ViewBag.provinsi = new SelectList(lst.lstProvinsi(), "provinsi_id", "provinsi_name", 0);
            ViewBag.kabupaten = new SelectList(lst.lstKabupaten(0), "kabupaten_id", "kabupaten_name", 0);
            ViewBag.kecamatan = "";
            return View();
        }
        public JsonResult SearchList(int id, int kid, string kec)
        {
            return Json(new List<kecamatan>(kabdb.listKecamatan(id, kid, kec)));
        }
        public JsonResult getKabupaten(int id)
        {
            JsonResult js = Json(lst.lstKabupaten(id));
            js.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return js;
        }

        public ActionResult Edit(int id = 0)
        {
            kecamatan kbt = kabdb.dataKecamatan(id);
            ViewBag.provinsi = new SelectList(lst.lstProvinsi(), "provinsi_id", "provinsi_name", kbt.provinsi_id);
            ViewBag.kabupaten = new SelectList(lst.lstKabupaten(kbt.provinsi_id), "kabupaten_id", "kabupaten_name", kbt.kabupaten_id);
            return View(kbt);
        }
        public JsonResult Simpan()
        {
            if (Session["UID"] == null) { Session["UID"] = 1; }
            var serializer = new JavaScriptSerializer();
            kecamatan kab = serializer.Deserialize<kecamatan>(Request["kecamatan"].ToString());

            return Json(kabdb.simpanKecamatan(kab, Convert.ToInt16(Session["UID"])), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Hapus(int id)
        {
            if (Session["UID"] == null) { Session["UID"] = 1; }
            return Json(kabdb.hapusKecamatan(id, Convert.ToInt16(Session["UID"])), JsonRequestBehavior.AllowGet);
        }

    }
}
