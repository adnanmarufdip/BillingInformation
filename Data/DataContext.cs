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

        public bool AddBill(BillModel billModel)
        {
            connection();
            SqlCommand cmd = new SqlCommand("AddNewBill", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ProductName", billModel.ProductName);
            cmd.Parameters.AddWithValue("@UnitPrice", billModel.UnitPrice);
            cmd.Parameters.AddWithValue("@Quantity", billModel.Quantity);
            cmd.Parameters.AddWithValue("@Amount", billModel.Amount);
            cmd.Parameters.AddWithValue("@Date", billModel.Date);
            cmd.Parameters.AddWithValue("@Time", billModel.Time);

            con.Open();
            int queryStatus = cmd.ExecuteNonQuery();
            con.Close();

            return (queryStatus >= 1) ? true : false;
        }
    }
}