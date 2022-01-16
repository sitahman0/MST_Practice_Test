using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Rahmano_mst.Models
{
    public class kabupaten
    {
        public int kabupaten_id { get; set; }
        public int provinsi_id { get; set; }
        public int nomor { get; set; }
        public string provinsi_name { get; set; }
        public string kabupaten_name { get; set; }
        public int jenis { get; set; }
        public string jenis_kota { get; set; }
        public string kabupaten_desc { get; set; }
    }
    public class kabupatenDB
    {
        SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings["DBConnection"]);
        SqlCommand com;
        SqlDataReader dr;

        public List<kabupaten> listKabupaten(int id , string kab)
        {
            List<kabupaten> lst = new List<kabupaten>();
            com = new SqlCommand("spKabupaten_List", cn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@provinsi_id", SqlDbType.Int).Value = id;
            com.Parameters.Add("@kabupaten_name", SqlDbType.VarChar, 50).Value = kab;
            int nom = 0;

            cn.Open();
            dr = com.ExecuteReader();

            kabupaten pr;
            while (dr.Read())
            {
                pr = new kabupaten();
                nom += 1;
                pr.provinsi_id = Convert.ToInt16(dr["provinsi_id"]);
                pr.kabupaten_id = Convert.ToInt16(dr["kabupaten_id"]);
                pr.provinsi_name = dr["provinsi_name"].ToString();
                pr.kabupaten_name = dr["kabupaten_name"].ToString();
                pr.jenis = Convert.ToInt16(dr["jenis"]);
                pr.jenis_kota = dr["jenis_kota"].ToString();
                pr.kabupaten_desc = dr["kabupaten_desc"].ToString();
                pr.nomor = nom;
                lst.Add(pr);
            }
            cn.Close();

            return lst;
        }
        public kabupaten dataKabupaten(int kabupaten_id)
        {
            com = new SqlCommand("spKabupaten_Detail", cn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@kabupaten_id", SqlDbType.Int).Value = kabupaten_id;

            cn.Open();
            dr = com.ExecuteReader();

            kabupaten usr = new kabupaten(); ;
            while (dr.Read())
            {
                usr.provinsi_id = Convert.ToInt16(dr["provinsi_id"]);
                usr.kabupaten_id = Convert.ToInt16(dr["kabupaten_id"]);
                usr.kabupaten_name = dr["kabupaten_name"].ToString();
                usr.jenis = Convert.ToInt16(dr["jenis"]);
                usr.kabupaten_desc = dr["kabupaten_desc"].ToString();
            }
            cn.Close();

            return usr;
        }
        public pesan simpanKabupaten(kabupaten prod, int uid)
        {
            com = new SqlCommand("spKabupaten_Save", cn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@kabupaten_id", SqlDbType.Int).Value = prod.kabupaten_id;
            com.Parameters.Add("@provinsi_id", SqlDbType.Int).Value = prod.provinsi_id;
            com.Parameters.Add("@kabupaten_name", SqlDbType.VarChar, 50).Value = prod.kabupaten_name;
            com.Parameters.Add("@jenis", SqlDbType.Int).Value = prod.jenis;
            com.Parameters.Add("@kabupaten_desc", SqlDbType.VarChar, 250).Value = prod.kabupaten_desc;
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
        public pesan hapusKabupaten(int id, int uid)
        {
            com = new SqlCommand("spKabupaten_Delete", cn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@kabupaten_id", SqlDbType.Int).Value = id;
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