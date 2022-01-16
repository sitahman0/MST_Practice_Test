using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Rahmano_mst.Models
{
    public class isiList
    {
        SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings["DBConnection"]);
        SqlCommand com;
        SqlDataReader dr;

        public List<provinsi> lstProvinsi()
        {
            List<provinsi> lst = new List<provinsi>();
            com = new SqlCommand("spList_Provinsi", cn);
            com.CommandType = CommandType.StoredProcedure;

            cn.Open();
            dr = com.ExecuteReader();

            provinsi gr;
            while (dr.Read())
            {
                gr = new provinsi();
                gr.provinsi_id = Convert.ToInt16(dr["PROVINSI_ID"]);
                gr.provinsi_name = dr["PROVINSI_NAMe"].ToString();
                lst.Add(gr);
            }
            cn.Close();

            return lst;
        }
        public List<kabupaten> lstKabupaten(int pid = 0)
        {
            List<kabupaten> lst = new List<kabupaten>();
            com = new SqlCommand("spList_Kabupaten", cn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@provinsi_id", SqlDbType.Int).Value = pid;

            cn.Open();
            dr = com.ExecuteReader();

            kabupaten gr;
            while (dr.Read())
            {
                gr = new kabupaten();
                gr.kabupaten_id = Convert.ToInt16(dr["kabupaten_id"]);
                gr.kabupaten_name = dr["kabupaten_name"].ToString();
                lst.Add(gr);
            }
            cn.Close();

            return lst;
        }
        public List<kecamatan> lstKecamatan(int kid = 0)
        {
            List<kecamatan> lst = new List<kecamatan>();
            com = new SqlCommand("spList_Kecamatan", cn);
            com.CommandType = CommandType.StoredProcedure;
            //com.Parameters.Add("@kabupaten_id", SqlDbType.Int).Value = pid;
            com.Parameters.Add("@kabupaten_id", SqlDbType.Int).Value = kid;

            cn.Open();
            dr = com.ExecuteReader();

            kecamatan gr;
            while (dr.Read())
            {
                gr = new kecamatan();
                gr.kecamatan_id = Convert.ToInt16(dr["kecamatan_id"]);
                gr.kecamatan_name = dr["kecamatan_name"].ToString();
                lst.Add(gr);
            }
            cn.Close();

            return lst;
        }
        public List<kelurahan> lstKelurahan(int kid = 0)
        {
            List<kelurahan> lst = new List<kelurahan>();
            com = new SqlCommand("spList_Kelurahan", cn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@kecamatan_id", SqlDbType.Int).Value = kid;

            cn.Open();
            dr = com.ExecuteReader();

            kelurahan gr;
            while (dr.Read())
            {
                gr = new kelurahan();
                gr.kelurahan_id = Convert.ToInt16(dr["kelurahan_id"]);
                gr.kelurahan_name = dr["kelurahan_name"].ToString();
                lst.Add(gr);
            }
            cn.Close();

            return lst;
        }
    }
}