using BLL;
using DATA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;

namespace DAL
{
    public class SupplierDAL
    {
        public static Supplier GetById(int id)
        {
            Supplier Tmp = null;
            DbContext Db = new DbContext();
            string Sql = $"SELECT * FROM T_Suppliers WHERE SupplierId={id}";  
            DataTable Dt = Db.Execute(Sql);

            if (Dt.Rows.Count > 0)
            {
                Tmp = new Supplier()
                {
                    SupplierId = int.Parse(Dt.Rows[0]["SupplierId"] + ""),  
                    SupplierName = Dt.Rows[0]["SupplierName"] + "", 
                    SAddress = Dt.Rows[0]["SAddress"] + "",
                    SPhone = Dt.Rows[0]["SPhone"] + "",
                    SWeb = Dt.Rows[0]["SWeb"] + "",
                    SEmail = Dt.Rows[0]["SEmail"] + "",
                    Added = DateTime.Parse(Dt.Rows[0]["Added"] + ""),
                    Contact = Dt.Rows[0]["Contact"] + ""
                };
            }

            return Tmp; // אם לא מצא כלום מחזיר null
        }

        public static List<Supplier> Get()
        {
            List<Supplier> LstTmp = new List<Supplier>();
            DbContext Db = new DbContext();
            string Sql = "SELECT * FROM T_Suppliers";  
            DataTable Dt = Db.Execute(Sql);
            for (int i = 0; i < Dt.Rows.Count; i++)
            {
                Supplier Tmp = new Supplier()
                {
                    SupplierId = int.Parse(Dt.Rows[i]["SupplierId"] + ""),  
                    SupplierName = Dt.Rows[i]["SupplierName"] + "",  
                    SAddress = Dt.Rows[i]["SAddress"] + "",
                    SPhone = Dt.Rows[i]["SPhone"] + "",
                    SWeb = Dt.Rows[i]["SWeb"] + "",
                    SEmail = Dt.Rows[i]["SEmail"] + "",
                    Added = DateTime.Parse(Dt.Rows[i]["Added"] + ""),
                    Contact = Dt.Rows[i]["Contact"] + ""
                };
                LstTmp.Add(Tmp);
            }

            return LstTmp;
        }

        public static int Delete(int id)
        {
            DbContext Db = new DbContext();
            string Sql = $"DELETE FROM T_Suppliers WHERE SupplierId={id}";  
            int RecCount = Db.ExecuteNonQuery(Sql);
            return RecCount;
        }

        public static int Save(Supplier Tmp)
        {
            int RecCount = 0;
            DbContext Db = new DbContext();
            string Sql = "";

            if (Tmp.SupplierId == -1)
            {
                Sql = $"INSERT INTO T_Suppliers (SupplierName, SAddress, SPhone, SWeb, SEmail, Added, Contact) VALUES ";
                Sql += $"(N'{Tmp.SupplierName}', N'{Tmp.SAddress}', N'{Tmp.SPhone}', N'{Tmp.SWeb}', N'{Tmp.SEmail}', '{Tmp.Added.ToString("yyyy-MM-dd")}', N'{Tmp.Contact}')";
            }
            else
            {
                Sql = $"UPDATE T_Suppliers SET ";
                Sql += $"SupplierName = N'{Tmp.SupplierName}', ";
                Sql += $"SAddress = N'{Tmp.SAddress}', ";
                Sql += $"SPhone = N'{Tmp.SPhone}', ";
                Sql += $"SWeb = N'{Tmp.SWeb}', ";
                Sql += $"SEmail = N'{Tmp.SEmail}', ";
                Sql += $"Contact = N'{Tmp.Contact}', ";
                Sql += $"Added = '{Tmp.Added.ToString("yyyy-MM-dd")}' ";
                Sql += $"WHERE SupplierId = {Tmp.SupplierId}";  
            }

            RecCount = Db.ExecuteNonQuery(Sql);

            if (Tmp.SupplierId == -1)
            {
                Tmp.SupplierId = Db.GetMaxId("T_Suppliers", "SupplierId"); 
            }

            return RecCount;
        }
    }
}
