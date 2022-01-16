using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Rahmano_mst.Models
{
    public class provinsi
    {
        public int provinsi_id { get; set; }
        public int nomor { get; set; }
        public string provinsi_name { get; set; }
        public string provinsi_desc { get; set; }
    }
    public class provinsiDB
    {
        SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings["DBConnection"]);
        SqlCommand com;
        SqlDataReader dr;

        public List<provinsi> listProvinsi(string prov)
        {
            List<provinsi> lst = new List<provinsi>();
            com = new SqlCommand("spProvinsi_List", cn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@provinsi_name", SqlDbType.VarChar, 50).Value = prov;
            int nom = 0;

            cn.Open();
            dr = com.ExecuteReader();

            provinsi pr;
            while (dr.Read())
            {
                pr = new provinsi();
                nom += 1;
                pr.provinsi_id = Convert.ToInt16(dr["provinsi_id"]);
                pr.provinsi_name = dr["provinsi_name"].ToString();
                pr.provinsi_desc = dr["provinsi_desc"].ToString();
                pr.nomor = nom;
                lst.Add(pr);
            }
            cn.Close();

            return lst;
        }
        public provinsi dataProvinsi(int provinsi_id)
        {
            com = new SqlCommand("spProvinsi_Detail", cn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@provinsi_id", SqlDbType.Int).Value = provinsi_id;

            cn.Open();
            dr = com.ExecuteReader();

            provinsi usr = new provinsi(); ;
            while (dr.Read())
            {
                usr.provinsi_id = Convert.ToInt16(dr["provinsi_id"]);
                usr.provinsi_name = dr["provinsi_name"].ToString();
                usr.provinsi_desc = dr["provinsi_desc"].ToString();
            }
            cn.Close();

            return usr;
        }
        public pesan simpanProvinsi(provinsi prod, int uid)
        {
            com = new SqlCommand("spProvinsi_Save", cn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@provinsi_id", SqlDbType.Int).Value = prod.provinsi_id;
            com.Parameters.Add("@provinsi_name", SqlDbType.VarChar, 50).Value = prod.provinsi_name;
            com.Parameters.Add("@provinsi_desc", SqlDbType.VarChar, 250).Value = prod.provinsi_desc;
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
        public pesan hapusProvinsi(int id, int uid)
        {
            com = new SqlCommand("spProvinsi_Delete", cn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@provinsi_id", SqlDbType.Int).Value = id;
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