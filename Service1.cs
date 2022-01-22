using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceTextMe
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        string constring = "Data Source=LAPTOP-3HPK6NGD\\SQLEXPRESS;Initial Catalog=textMeDB;Persist Security Info=True;User ID=sa;Password=priyaihsan";
        SqlConnection connection;
        SqlCommand com;

        public string deleteMessage(string IDMessage)
        {
            string respont = "gagal";
            try
            {
                string sql = "delete from dbo.Message where idMessage = '" + IDMessage + "'";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                respont = "sukses";
            }
            catch (Exception ea)
            {
                Console.WriteLine(ea);
            }
            return respont;
        }

        public string deleteUser(string IDpengguna)
        {
            string respont = "gagal";
            try
            {
                string sql = "delete from dbo.Pengguna where idPengguna = '" + IDpengguna + "'";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                respont = "sukses";
            }
            catch (Exception eq)
            {
                Console.WriteLine(eq);
            }
            return respont;
        }

        public List<listMessage> listMessage(string nameUser)
        {
            List<listMessage> MessageFull = new List<listMessage>();
            try
            {
                string sql = "select idMessage,Message from dbo.Message p join dbo.Pengguna l on p.idPengguna = l.idPengguna where namaPengguna = '"+ nameUser +"'";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    listMessage data = new listMessage();
                    data.idMessage = reader.GetString(0);
                    data.Message = reader.GetString(1);
                    MessageFull.Add(data);
                }
                connection.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            return MessageFull;
        }

        public List<listUsername> listUsername()
        {
            List<listUsername> UsernameFull = new List<listUsername>();
            try
            {
                string sql = "select idPengguna, namaPengguna from dbo.Pengguna";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    listUsername data = new listUsername();
                    data.IDUser = reader.GetString(0);
                    data.NameUser = reader.GetString(1);
                    UsernameFull.Add(data);
                }
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return UsernameFull;
        }

        public string sendMessage(string IDMessage, string Message, int idPengguna)
        {
            string respont = "gagal";
            try
            {
                string sql = "insert into dbo.Message values ('" + IDMessage + "','" + Message + "'," + idPengguna + ")";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open();
                com.ExecuteNonQuery();
                connection.Close();

                respont = "sukses";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return respont;
        }

        public string Login(string nameUser, string passwordUser)
        {
            string a = "gagal";
            string nmAdmin = "";
            string nmPassAdmin = "";
            string User = "";
            try
            {
                string sql = "select namaAdmin, passwordAdmin from dbo.Admin where idAdmin = 1";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = com.ExecuteReader();
                while (reader.Read())
                {
                    nmAdmin = reader.GetString(0);
                    nmPassAdmin = reader.GetString(1);
                }
                connection.Close();

                if (nameUser == nmAdmin && passwordUser == nmPassAdmin)
                {
                    a = "Admin";
                }
                else
                {

                    string sql2 = "select namaPengguna from dbo.Pengguna where namaPengguna = '" + nameUser + "' and passwordPengguna = '" + passwordUser + "'";
                    connection = new SqlConnection(constring);
                    com = new SqlCommand(sql2, connection);
                    connection.Open();
                    SqlDataReader reader2 = com.ExecuteReader();
                    while (reader2.Read())
                    {
                        User = reader2.GetString(0);
                    }
                    connection.Close();
                    a = User;
                }

            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            return a;
        }

        public string registrasiUser(string IDUser, string nameUser, string passwordUser)
        {
            bool test = true;
            try
            {
                List<string> AuthorList = new List<string>();
                string sql = "select namaPengguna from dbo.Pengguna";
                connection = new SqlConnection(constring);
                com = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader2 = com.ExecuteReader();
                while (reader2.Read())
                {
                    AuthorList.Add(reader2.GetString(0));
                }
                connection.Close();
                for (int i = 0; i < AuthorList.Count; i++)
                {
                    if (AuthorList[i].Contains(nameUser))
                    {
                        test = false;
                    }
                }

                if (test == true)
                {
                    string sql2 = "insert into dbo.Pengguna values('" + IDUser + "','" + nameUser + "','" + passwordUser + "')";
                    connection = new SqlConnection(constring);
                    com = new SqlCommand(sql2, connection);
                    connection.Open();
                    com.ExecuteNonQuery();
                    connection.Close();
                    
                    return "sukses";
                }
                else
                {
                    return "gagal , nama sudah ada";
                }


            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

    }
}
