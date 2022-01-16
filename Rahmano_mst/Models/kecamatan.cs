using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Rahmano_mst.Models
{
    public class kecamatan
    {
        public int kecamatan_id { get; set; }
        public int provinsi_id { get; set; }
        public int kabupaten_id { get; set; }
        public int nomor { get; set; }
        public string provinsi_name { get; set; }
        public string kabupaten_name { get; set; }
        public string jenis_kota { get; set; }
        public string kecamatan_name { get; set; }
        public string kecamatan_desc { get; set; }
    }
    public class kecamatanDB
    {
        SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings["DBConnection"]);
        SqlCommand com;
        SqlDataReader dr;

        public List<kecamatan> listKecamatan(int id, int kid, string kec)
        {
            List<kecamatan> lst = new List<kecamatan>();
            com = new SqlCommand("spKecamatan_List", cn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@provinsi_id", SqlDbType.Int).Value = id;
            com.Parameters.Add("@kabupaten_id", SqlDbType.Int).Value = kid;
            com.Parameters.Add("@kecamatan_name", SqlDbType.VarChar, 50).Value = kec;
            int nom = 0;

            cn.Open();
            dr = com.ExecuteReader();

            kecamatan pr;
            while (dr.Read())
            {
                pr = new kecamatan();
                nom += 1;
                pr.provinsi_id = Convert.ToInt16(dr["provinsi_id"]);
                pr.kabupaten_id = Convert.ToInt16(dr["kabupaten_id"]);
                pr.kecamatan_id = Convert.ToInt16(dr["kecamatan_id"]);
                pr.provinsi_name = dr["provinsi_name"].ToString();
                pr.kabupaten_name = dr["kabupaten_name"].ToString();
                pr.jenis_kota = dr["jenis_kota"].ToString();
                pr.kecamatan_name = dr["kecamatan_name"].ToString();
                pr.kecamatan_desc = dr["kecamatan_desc"].ToString();
                pr.nomor = nom;
                lst.Add(pr);
            }
            cn.Close();

            return lst;
        }
        public kecamatan dataKecamatan(int kecamatan_id)
        {
            com = new SqlCommand("spKecamatan_Detail", cn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@kecamatan_id", SqlDbType.Int).Value = kecamatan_id;

            cn.Open();
            dr = com.ExecuteReader();

            kecamatan usr = new kecamatan(); ;
            while (dr.Read())
            {
                usr.provinsi_id = Convert.ToInt16(dr["provinsi_id"]);
                usr.kabupaten_id = Convert.ToInt16(dr["kabupaten_id"]);
                usr.kecamatan_id = Convert.ToInt16(dr["kecamatan_id"]);
                usr.kecamatan_name = dr["kecamatan_name"].ToString();
                usr.kecamatan_desc = dr["kecamatan_desc"].ToString();
            }
            cn.Close();

            return usr;
        }
        public pesan simpanKecamatan(kecamatan prod, int uid)
        {
            com = new SqlCommand("spKecamatan_Save", cn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@kecamatan_id", SqlDbType.Int).Value = prod.kecamatan_id;
            com.Parameters.Add("@kabupaten_id", SqlDbType.Int).Value = prod.kabupaten_id;
            com.Parameters.Add("@provinsi_id", SqlDbType.Int).Value = prod.provinsi_id;
            com.Parameters.Add("@kecamatan_name", SqlDbType.VarChar, 50).Value = prod.kecamatan_name;
            com.Parameters.Add("@kecamatan_desc", SqlDbType.VarChar, 250).Value = prod.kecamatan_desc;
            com.Parameters.Add("@USERID", SqlDbType.Char, 1).Value = uid;

            pesan psn = new pesan();
            cn.Open();
            dr = com.ExecuteReader();

            while (dr.Read())
            {
                psn.kid = Convert.ToInt16(dr["KID"]);
                psn.msg = dr["MSG"].ToString();
                psn.salah = Convert.ToInt16(dr["SALAH"]);
            }
            cn.Close();

            return psn;
        }
        public pesan hapusKecamatan(int id, int uid)
        {
            com = new SqlCommand("spKecamatan_Delete", cn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@kecamatan_id", SqlDbType.Int).Value = id;
            com.Parameters.Add("@USERID", SqlDbType.Int).Value = uid;

            pesan psn = new pesan();
            cn.Open();
            dr = com.ExecuteReader();

            while (dr.Read())
            {
                psn.kid = Convert.ToInt16(dr["KID"]);
                psn.msg = dr["MSG"].ToString();
            }
            cn.Close();

            return psn;
        }
    }
}