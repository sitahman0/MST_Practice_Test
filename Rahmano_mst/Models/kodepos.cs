using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Rahmano_mst.Models
{
    public class kodepos
    {
        public int kodepos_id { get; set; }
        public int provinsi_id { get; set; }
        //public int kabupaten_id { get; set; }
        //public int kecamatan_id { get; set; }
        public int nomor { get; set; }
        public string provinsi_name { get; set; }
        public string kodepos_no { get; set; }
        public string kodepos_desc { get; set; }
        public virtual List<kelurahanKopos> kelurahan_Kopos { get; set; }

    }
    public class kelurahanKopos
    {
        public int kelurahan_id { get; set; }
        //public int kodepos_id { get; set; }
        public int nomor { get; set; }
        public string provinsi_name { get; set; }
        public string kabupaten_name { get; set; }
        public string kecamatan_name { get; set; }
        public string kelurahan_name { get; set; }
        public int dipilih { get; set; }
        public string masuk { get; set; }

        private static readonly SqlMetaData[] myRecordSchema = {
                new SqlMetaData("kelurahan_id", SqlDbType.Int),
                new SqlMetaData("nomor", SqlDbType.Int),
                new SqlMetaData("provinsi_name", SqlDbType.Text),
                new SqlMetaData("kabupaten_name", SqlDbType.Text),
                new SqlMetaData("kecamatan_name", SqlDbType.Text),
                new SqlMetaData("kelurahan_name", SqlDbType.Text),
                new SqlMetaData("dipilih", SqlDbType.Int),
                new SqlMetaData("masuk", SqlDbType.Text)
            };

        public SqlDataRecord ToSqlDataRecord()
        {
            var record = new SqlDataRecord(myRecordSchema);
            record.SetInt32(0, kelurahan_id);
            record.SetInt32(1, nomor);
            record.SetString(2, provinsi_name);
            record.SetString(3, kabupaten_name);
            record.SetString(4, kecamatan_name);
            record.SetString(5, kelurahan_name);
            record.SetInt32(6, dipilih);
            record.SetString(7, masuk);
            return record;
        }
    }
    public class kodeposDB
        {
        SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings["DBConnection"]);
        SqlCommand com;
        SqlDataReader dr;

        public List<kodepos> listKodepos(int id, string kpos)
        {
            List<kodepos> lst = new List<kodepos>();
            com = new SqlCommand("spKodepos_List", cn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@provinsi_id", SqlDbType.Int).Value = id;
            com.Parameters.Add("@kodepos_no", SqlDbType.VarChar, 5).Value = kpos;
            int nom = 0;

            cn.Open();
            dr = com.ExecuteReader();

            kodepos pr;
            while (dr.Read())
            {
                pr = new kodepos();
                nom += 1;
                pr.provinsi_id = Convert.ToInt16(dr["provinsi_id"]);
                pr.kodepos_id = Convert.ToInt16(dr["kodepos_id"]);
                pr.provinsi_name = dr["provinsi_name"].ToString();
                pr.kodepos_no = dr["kodepos_no"].ToString();
                pr.kodepos_desc = dr["kodepos_desc"].ToString();
                pr.nomor = nom;
                lst.Add(pr);
            }
            cn.Close();

            return lst;
        }
        public kodepos dataKodepos(int kodepos_id)
        {
            com = new SqlCommand("spKodepos_Detail", cn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@kodepos_id", SqlDbType.Int).Value = kodepos_id;

            cn.Open();
            dr = com.ExecuteReader();

            kodepos usr = new kodepos(); ;
            while (dr.Read())
            {
                usr.provinsi_id = Convert.ToInt16(dr["provinsi_id"]);
                usr.kodepos_id = Convert.ToInt16(dr["kodepos_id"]);
                usr.kodepos_no = dr["kodepos_no"].ToString();
                usr.kodepos_desc = dr["kodepos_desc"].ToString();
            }
            cn.Close();

            return usr;
        }
        public List<kelurahanKopos> lstKelurahanKopos(int kid = 0, int kpos = 0)
        {
            List<kelurahanKopos> lst = new List<kelurahanKopos>();
            com = new SqlCommand("spList_KelurahanKopos", cn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@kecamatan_id", SqlDbType.Int).Value = kid;
            com.Parameters.Add("@kodepos_id", SqlDbType.Int).Value = kpos;

            cn.Open();
            dr = com.ExecuteReader();
            int nom = 0;

            kelurahanKopos gr;
            while (dr.Read())
            {
                gr = new kelurahanKopos();
                nom += 1;
                gr.nomor = nom;
                gr.kelurahan_id = Convert.ToInt16(dr["kelurahan_id"]);
                gr.provinsi_name = dr["provinsi_name"].ToString();
                gr.kabupaten_name = dr["kabupaten_name"].ToString();
                gr.kecamatan_name = dr["kecamatan_name"].ToString();
                gr.kelurahan_name = dr["kelurahan_name"].ToString();
                gr.dipilih = Convert.ToInt16(dr["dipilih"]);
                gr.masuk = dr["masuk"].ToString();
                lst.Add(gr);
            }
            cn.Close();

            return lst;
        }
        public List<kelurahanKopos> listKoposKelurhan(int id = 0)
        {
            List<kelurahanKopos> lst = new List<kelurahanKopos>();
            com = new SqlCommand("spKodeposKelurahan_List", cn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@kodepos_id", SqlDbType.Int).Value = id;

            cn.Open();
            dr = com.ExecuteReader();
            int nom = 0;

            kelurahanKopos gr;
            while (dr.Read())
            {
                gr = new kelurahanKopos();
                nom += 1;
                gr.nomor = nom;
                gr.kelurahan_id = Convert.ToInt16(dr["kelurahan_id"]);
                gr.provinsi_name = dr["provinsi_name"].ToString();
                gr.kabupaten_name = dr["kabupaten_name"].ToString();
                gr.kecamatan_name = dr["kecamatan_name"].ToString();
                gr.kelurahan_name = dr["kelurahan_name"].ToString();
                gr.dipilih = Convert.ToInt16(dr["dipilih"]);
                gr.masuk = dr["masuk"].ToString();
                lst.Add(gr);
            }
            cn.Close();

            return lst;
        }

        public pesan simpanKodepos(kodepos prod, int uid)
        {
            com = new SqlCommand("spKodepos_Save", cn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@kodepos_id", SqlDbType.Int).Value = prod.kodepos_id;
            com.Parameters.Add("@provinsi_id", SqlDbType.Int).Value = prod.provinsi_id;
            com.Parameters.Add("@kodepos_no", SqlDbType.VarChar, 5).Value = prod.kodepos_no;
            com.Parameters.Add("@kodepos_desc", SqlDbType.VarChar, 250).Value = prod.kodepos_desc;
            com.Parameters.Add("@USERID", SqlDbType.Char, 1).Value = uid;
            com.Parameters.AddWithValue("@KODE_POS_DTL", prod.kelurahan_Kopos.Select(c => c.ToSqlDataRecord()));
            com.Parameters[5].SqlDbType = SqlDbType.Structured;
            com.Parameters[5].TypeName = "dbo.type_Kode_pos_dtl";
            com.Parameters[5].Direction = ParameterDirection.Input;

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
        public pesan hapusKodepos(int id, int uid)
        {
            com = new SqlCommand("spKodePos_Delete", cn);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add("@kodepos_id", SqlDbType.Int).Value = id;
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