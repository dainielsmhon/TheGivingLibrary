using BLL;
using DATA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Policy;
using System.Web;

namespace DAL
{
    public class UserDAL
    {
        public static User GetById(int id)
        {
            User Tmp = null;
            using (DbContext Db = new DbContext())
            {
                string Sql = $" SELECT * FROM T_Users WHERE UserId={id}";
                DataTable Dt = Db.Execute(Sql);
                if (Dt.Rows.Count > 0)
                {
                    Tmp = new User()
                    {
                        UserId = int.Parse(Dt.Rows[0]["UserId"] + ""),
                        UserName = Dt.Rows[0]["UserName"] + "",
                        Name = Dt.Rows[0]["Name"] + "",
                        UserPass = Dt.Rows[0]["UserPass"] + "",
                        Email = Dt.Rows[0]["Email"] + "",
                        Phone = Dt.Rows[0]["Phone"] + "",
                        Adress = Dt.Rows[0]["Adress"] + "",
                        JoinDate = DateTime.Parse(Dt.Rows[0]["JoinDate"] + ""),
                        IsAdmin = bool.Parse(Dt.Rows[0]["IsAdmin"] + "")
                    };
                }
            }
            return Tmp;
        }

        public static List<User> Get()
        {
            List<User> LstTmp = new List<User>();
            using (DbContext Db = new DbContext())
            {
                string Sql = $" SELECT * FROM T_Users ";
                DataTable Dt = Db.Execute(Sql);
                for (int i = 0; i < Dt.Rows.Count; i++)
                {
                    User Tmp = new User()
                    {
                        UserId = int.Parse(Dt.Rows[i]["UserId"] + ""),
                        UserName = Dt.Rows[i]["UserName"] + "",
                        Name = Dt.Rows[i]["Name"] + "",
                        UserPass = Dt.Rows[i]["UserPass"] + "",
                        Email = Dt.Rows[i]["Email"] + "",
                        Phone = Dt.Rows[i]["Phone"] + "",
                        Adress = Dt.Rows[i]["Adress"] + "",
                        JoinDate = DateTime.Parse(Dt.Rows[i]["JoinDate"] + ""),
                        IsAdmin = bool.Parse(Dt.Rows[i]["IsAdmin"] + "")

                    };
                    LstTmp.Add(Tmp);
                }
            }
            return LstTmp;
        }

        public static int Delete(int id)
        {
            int RecCount = 0;
            using (DbContext Db = new DbContext())
            {
                string Sql = $" DELETE FROM T_Users WHERE UserId={id}";
                RecCount = Db.ExecuteNonQuery(Sql);
            }
            return RecCount;
        }

        public static int Save(User Tmp)
        {
            int RecCount = 0;
            using (DbContext Db = new DbContext())
            {
                string Sql = "";
                if (Tmp.UserId == -1)
                {
                    Sql = $"INSERT INTO T_Users (Name,UserName,UserPass,Email,Phone,Adress,JoinDate,IsAdmin) Values ";
                    Sql += $" (N'{Tmp.Name}',N'{Tmp.UserName}',N'{Tmp.UserPass}',N'{Tmp.Email}',N'{Tmp.Phone}',N'{Tmp.Adress}','{Tmp.JoinDate:yyyy-MM-dd}', '{(Tmp.IsAdmin ? 1 : 0)}')";
                }
                else
                {
                    Sql = $"UPDATE T_Users SET ";
                    Sql += $"UserName=N'{Tmp.UserName}', ";
                    Sql += $"Name=N'{Tmp.Name}', ";
                    Sql += $"UserPass=N'{Tmp.UserPass}', ";
                    Sql += $"Email=N'{Tmp.Email}', ";
                    Sql += $"Phone=N'{Tmp.Phone}', ";
                    Sql += $"Adress=N'{Tmp.Adress}', ";
                    Sql += $"JoinDate='{Tmp.JoinDate:yyyy-MM-dd}', "; // ← פסיק כאן ✔️
                    Sql += $"IsAdmin={(Tmp.IsAdmin ? 1 : 0)} ";         // ← אין פסיק בסוף!
                    Sql += $"WHERE UserId={Tmp.UserId}";
                }
                RecCount = Db.ExecuteNonQuery(Sql);
            }
            return RecCount;
        }
    }
}
