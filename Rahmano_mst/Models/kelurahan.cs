using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Rahmano_mst.Models
{
    public class kelurahan
    {
        public int kelurahan_id { get; set; }
        public int kecamatan_id { get; set; }
        public int provinsi_id { get; set; }
        public int kabupaten_id { get; set; }
        public int nomor { get; set; }
        public string provinsi_name { get; set; }
        public string kabupaten_name { get; set; }
        public string kecamatan_name { get; set; }
        public string kelurahan_name { get; set; }
        public string kelurahan_desc { get; set; }
    }
    public class kelurahanDB
    {
        SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings["DBConnection"]);
        SqlCommand com;
        SqlDataReader dr;

        public List<kelurahan> listKelurahan(int id, int kid, int kcid, string kel)
        {
            List<kelurahan> lst = new List<kelurahan>();
            com = new SqlCommand("spKelurahan_List", cn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@provinsi_id", SqlDbType.Int).Value = id;
            com.Parameters.Add("@kabupaten_id", SqlDbType.Int).Value = kid;
            com.Parameters.Add("@kecamatan_id", SqlDbType.Int).Value = kcid;
            com.Parameters.Add("@kelurahan_name", SqlDbType.VarChar, 50).Value = kel;
            int nom = 0;

            cn.Open();
            dr = com.ExecuteReader();

            kelurahan pr;
            while (dr.Read())
            {
                pr = new kelurahan();
                nom += 1;
                pr.provinsi_id = Convert.ToInt16(dr["provinsi_id"]);
                pr.kabupaten_id = Convert.ToInt16(dr["kabupaten_id"]);
                pr.kecamatan_id = Convert.ToInt16(dr["kecamatan_id"]);
                pr.kelurahan_id = Convert.ToInt16(dr["kelurahan_id"]);
                pr.provinsi_name = dr["provinsi_name"].ToString();
                pr.kabupaten_name = dr["kabupaten_name"].ToString();
                pr.kecamatan_name = dr["kecamatan_name"].ToString();
                pr.kelurahan_name = dr["kelurahan_name"].ToString();
                pr.kelurahan_name = dr["kelurahan_name"].ToString();
                pr.kelurahan_desc = dr["kelurahan_desc"].ToString();
                pr.nomor = nom;
                lst.Add(pr);
            }
            cn.Close();

            return lst;
        }
        public kelurahan dataKelurahan(int kelurahan_id)
        {
            com = new SqlCommand("spKelurahan_Detail", cn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@kelurahan_id", SqlDbType.Int).Value = kelurahan_id;

            cn.Open();
            dr = com.ExecuteReader();

            kelurahan usr = new kelurahan(); ;
            while (dr.Read())
            {
                usr.provinsi_id = Convert.ToInt16(dr["provinsi_id"]);
                usr.kabupaten_id = Convert.ToInt16(dr["kabupaten_id"]);
                usr.kecamatan_id = Convert.ToInt16(dr["kecamatan_id"]);
                usr.kelurahan_id = Convert.ToInt16(dr["kelurahan_id"]);
                usr.kelurahan_name = dr["kelurahan_name"].ToString();
                usr.kelurahan_desc = dr["kelurahan_desc"].ToString();
            }
            cn.Close();

            return usr;
        }
        public pesan simpanKelurahan(kelurahan prod, int uid)
        {
            com = new SqlCommand("spKelurahan_Save", cn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@kelurahan_id", SqlDbType.Int).Value = prod.kelurahan_id;
            com.Parameters.Add("@kecamatan_id", SqlDbType.Int).Value = prod.kecamatan_id;
            com.Parameters.Add("@kabupaten_id", SqlDbType.Int).Value = prod.kabupaten_id;
            com.Parameters.Add("@provinsi_id", SqlDbType.Int).Value = prod.provinsi_id;
            com.Parameters.Add("@kelurahan_name", SqlDbType.VarChar, 50).Value = prod.kelurahan_name;
            com.Parameters.Add("@kelurahan_desc", SqlDbType.VarChar, 250).Value = prod.kelurahan_desc;
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
        public pesan hapusKelurahan(int id, int uid)
        {
            com = new SqlCommand("spKelurahan_Delete", cn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@kelurahan_id", SqlDbType.Int).Value = id;
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