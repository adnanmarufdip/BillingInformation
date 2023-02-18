using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BillingInformation.Models;

namespace BillingInformation.Data
{
    public class DataContext
    {
        private SqlConnection con;
        private void connection()
        {
            string constring = ConfigurationManager.ConnectionStrings["DataCon"].ToString();
            con = new SqlConnection(constring);
        }

        public List<ProductModel> GetProducts()
        {
            connection();
            List<ProductModel> productList = new List<ProductModel>();

            SqlCommand sqlCmd = new SqlCommand("GetAllProducts", con);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCmd);

            DataTable dT = new DataTable();

            con.Open();
            sqlDataAdapter.Fill(dT);
            con.Close();

            foreach (DataRow dR in dT.Rows)
            {
                productList.Add(new ProductModel
                {
                    ProductId = Convert.ToInt32(dR["ProductId"]),
                    ProductName = Convert.ToString(dR["ProductName"]),
                    UnitPrice = Convert.ToDecimal(dR["UnitPrice"])
                });
            }

            return productList;
        }
    }
}