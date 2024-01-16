using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace fk_lib
{
    public class fkMysqlCommand
    {
        DataSet ds;
        MySqlDataAdapter da;
        string sqlCommand;
        MySqlCommand cmd;
        MySqlConnection conn;
        MySqlDataReader dr;

        private string MyConStr;
        public fkMysqlCommand()
        {
        }
        public fkMysqlCommand(string sqlstr)
        {
            //sqlstr = "Server=localhost;Database=mysqltest;uid=root;pwd=Ft80132123";
            MyConStr = sqlstr;          
        }
        public bool Connect()
        {
            conn = new MySqlConnection(MyConStr);
            try
            {
                conn.Open();
                return true;
                //da = new MySqlDataAdapter(sqlCommand, conn);

                //DataSet ds = new DataSet();
                //da.Fill(ds, "user");
                //dataGrid1.DataContext = ds.Tables["user"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }
        public DataTable Inqury( string inquryStr, string tag)
        {
            //sqlCommand = "select * from user";
            try
            {
                conn = new MySqlConnection(MyConStr);
                conn.Open();
                sqlCommand = inquryStr;
                da = new MySqlDataAdapter(sqlCommand, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, tag);
                return ds.Tables[tag];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return null;
        }
        public bool InsertGateway(string insertStr)
        {
            try
            {
                cmd = conn.CreateCommand();
                cmd.CommandText = insertStr;
                //cmd.CommandText = "insert into user(g_station,g_version,g_interface,g_sensor_number,d_connect_time,d_interface,d_serial) values" +
                // "(@name,@name1,@name2,@name3,@name4,@name5,@name6)";
                cmd.Parameters.Add("@name", MySqlDbType.TinyText).Value = "EPU-1";
                cmd.Parameters.Add("@name1", MySqlDbType.TinyText).Value = "";
                cmd.Parameters.Add("@name2", MySqlDbType.TinyText).Value = "rs485";
                cmd.Parameters.Add("@name3", MySqlDbType.UInt16).Value = 3;
                cmd.Parameters.Add("@name4", MySqlDbType.UInt32).Value = 1521313256;
                cmd.Parameters.Add("@name5", MySqlDbType.TinyText).Value = "rs485";
                cmd.Parameters.Add("@name6", MySqlDbType.TinyText).Value = "flow";
                conn.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return false;
        }
        public bool InsertData(string insertStr)
        {
            try
            {
                conn = new MySqlConnection(MyConStr);
                conn.Open();
                cmd = conn.CreateCommand();
                cmd.CommandText = insertStr;          
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Environment.Exit(Environment.ExitCode);
            }
            finally
            {
                conn.Close();
            }
            return false;
        }
        public bool Delete(string deleteStr, string tag, int id)
        {
            try
            {
                cmd = conn.CreateCommand();
                conn.Open();
                //cmd.CommandText = "delete from user where id=@id";//删除语句，已ID为条件删除
                cmd.Parameters.Add("@"+ tag, MySqlDbType.Int32).Value = id;
                cmd.CommandText = deleteStr;
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return false;
        }
        public bool UpdateInquery(string updateStr, string tag, int value)
        {
            try
            {
                cmd = conn.CreateCommand();
                conn.Open();
                //cmd.CommandText = "update user set 表號=@name where id=@id";
                cmd.CommandText = updateStr;
                cmd.Parameters.Add("@" + tag, MySqlDbType.Int32).Value = value;
                //cmd.Parameters.Add("@name", MySqlDbType.VarChar).Value = "ANFHSKJFSLKJSFLKJFLSKJ";
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return false;
        }
    }
}
