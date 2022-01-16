using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Rahmano_mst.Models;
namespace Rahmano_mst.Controllers
{
    public class KodePosController : Controller
    {
        kodeposDB kabdb = new kodeposDB();
        List<kodepos> lprov = new List<kodepos>();
        isiList lst = new isiList();
        public ActionResult Index()
        {
            ViewBag.provinsi = new SelectList(lst.lstProvinsi(), "provinsi_id", "provinsi_name", 0);
            ViewBag.kabupaten = new SelectList(lst.lstKabupaten(0), "kabupaten_id", "kabupaten_name", 0);

            return View();
        }
        public JsonResult SearchList(int id, string kpos)
        {
            return Json(new List<kodepos>(kabdb.listKodepos(id, kpos)));
        }
        public JsonResult getKabupaten(int id)
        {
            JsonResult js = Json(lst.lstKabupaten(id));
            js.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return js;
        }
        public JsonResult getKecamatan(int id)
        {
            JsonResult js = Json(lst.lstKecamatan(id));
            js.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return js;
        }
        public JsonResult lstKelurahanKopos(int id, int kpos)
        {
            JsonResult js = Json(kabdb.lstKelurahanKopos(id, kpos));
            js.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return js;
        }
        public JsonResult listKoposKelurhan(int id)
        {
            JsonResult js = Json(kabdb.listKoposKelurhan(id));
            js.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            return js;
        }

        public ActionResult Edit(int id = 0)
        {
            kodepos kbt = kabdb.dataKodepos(id);
            ViewBag.provinsi = new SelectList(lst.lstProvinsi(), "provinsi_id", "provinsi_name", kbt.provinsi_id);
            ViewBag.kabupaten = new SelectList(lst.lstKabupaten(kbt.provinsi_id), "kabupaten_id", "kabupaten_name", 0);
            ViewBag.kecamatan = new SelectList(lst.lstKecamatan(0), "kecamatan_id", "kecamatan_name", 0);

            return View(kbt);
        }
        public JsonResult Simpan()
        {
            if (Session["UID"] == null) { Session["UID"] = 1; }
            //kodepos kab = serializer.Deserialize<kodepos>(Request["kodepos"].ToString());

            var serializer = new JavaScriptSerializer();
            var kodepos = Request["kodepos"].ToString();
            var kelurahankopos = Request["kelurahankopos"].ToString();

            var jsonSettings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple
            };

            var kab = JsonConvert.DeserializeObject<kodepos>(kodepos, jsonSettings);
            var isi = JsonConvert.DeserializeObject<List<kelurahanKopos>>(kelurahankopos, jsonSettings);
            kab.kelurahan_Kopos = isi;

            return Json(kabdb.simpanKodepos(kab, Convert.ToInt16(Session["UID"])), JsonRequestBehavior.AllowGet);
        }
        public JsonResult Hapus(int id)
        {
            if (Session["UID"] == null) { Session["UID"] = 1; }
            return Json(kabdb.hapusKodepos(id, Convert.ToInt16(Session["UID"])), JsonRequestBehavior.AllowGet);
        }

    }
}
