using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
namespace mvcproject.Models
{
    public class companydb
    {
        string constring = ConfigurationManager.ConnectionStrings["testcon"].ConnectionString;
        SqlConnection con;
        public companydb()
        {
            con = new SqlConnection(constring);
        }

        public List<companyclass> selectcomp()
        {
            var getdata = new List<companyclass>();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_selectcompany", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    var ob = new companyclass
                    {
                        cid = Convert.ToInt32(sdr["company_id"]),//set
                        cname = sdr["company_name"].ToString()
                    };
                    getdata.Add(ob);
                }
                con.Close();
                return getdata;
            }
            catch
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                throw;
            }
        }
    }
}