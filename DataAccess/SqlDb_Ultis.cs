using NSX_Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class SqlDb_Ultis
    {
        private string strErrorMessage;
        private int intErrorNumber;
        private string IErrorMsg;
        private int IErrorCode;
        public string ErrorMessage
        {
            get
            {
                return strErrorMessage;
            }
        }
        /// <summary>
        /// int: Số lỗi khi truy cập SQL
        /// </summary>
        public int ErrorNumber
        {
            get
            {
                return intErrorNumber;
            }
        }
        public SqlDb_Ultis()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static void ExeNonSQL(string strSQL)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = 2000;
            SqlConnection cn = ConnectDB.GetConnection();
            cmd.Parameters.Clear();
            cmd.CommandText = strSQL;
            cmd.CommandType = CommandType.Text;
            try
            {
                cmd.Connection = cn;
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (SqlException e)
            {
                LoggingManager.WriteLogEx("Exception when ExeNonSQL message: " + e.Message + " Source: " + e.Source + " StackTrace: " + e.StackTrace);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                cn.Dispose();
            }

        }

        /// <summary>
        /// Thực thi 1 câu lệnh SQL dạng select có trả về dữ liệu là 1 bảng
        /// </summary>
        public static DataTable ExeSQLToDataTable(string strSQL)
        {
            SqlConnection cn = ConnectDB.GetConnection();
            SqlDataAdapter Adapter = new SqlDataAdapter(strSQL, cn);
            DataTable dt = new DataTable();
            try
            {
                Adapter.Fill(dt);
                cn.Close();
            }
            catch (SqlException e)
            {
                LoggingManager.WriteLogEx("Exception when ExeSQLToDataTable message: " + e.Message + " Source: " + e.Source + " StackTrace: " + e.StackTrace);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                cn.Dispose();
            }
            return dt;
        }

        /// <summary>
        /// Thực thi 1 câu lệnh SQL dạng select có trả về dữ liệu là DataSet
        /// </summary>
        /// <return>
        /// DataSet
        /// </return>
        public static DataSet ExeSQLToDataSet(string strSQL)
        {
            SqlConnection cn = ConnectDB.GetConnection();
            SqlDataAdapter Adapter = new SqlDataAdapter(strSQL, cn);
            DataSet ds = new DataSet();
            try
            {
                Adapter.Fill(ds);
                cn.Close();
            }
            catch (SqlException e)
            {
                LoggingManager.WriteLogEx("Exception when ExeSQLToDataSet message: " + e.Message + " Source: " + e.Source + " StackTrace: " + e.StackTrace);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                cn.Dispose();
            }
            return ds;
        }
        /// <summary>
        /// Thực thi câu lênh sql trả về giá trị lớn nhất
        /// </summary>
        public static int ExeSQLGetMax(string strSQL)
        {
            int max = 0;
            SqlConnection cn = ConnectDB.GetConnection();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cn.Open();
                cmd.Connection = cn;
                max = Convert.ToInt16(cmd.ExecuteScalar());
                cn.Close();
            }
            catch { }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                cn.Dispose();
            }
            return max;
        }
        /// <summary>
        /// Thực thi câu lênh sql trả về 1 chuỗi (String)
        /// </summary>
        public static string ExeSQLGetData(string strSQL)
        {
            string data = "";
            SqlConnection cn = ConnectDB.GetConnection();
            SqlCommand cmd = new SqlCommand(strSQL, cn);
            SqlDataReader dr;
            try
            {
                cn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    data = Convert.ToString(dr.GetSqlString(0));
                }
                cn.Close();
                cmd.Dispose();
            }
            catch { }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                cn.Dispose();
            }
            return data;
        }
        /// <summary>
        /// Thực thi câu lênh sql trả về giá trị nguyên
        /// </summary>
        public static int ExeSQLGetInt(string strSQL)
        {
            int value = 0;
            SqlConnection cn = ConnectDB.GetConnection();
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(strSQL, cn);
                value = (int)cmd.ExecuteScalar();
                cn.Close();
                cmd.Dispose();
            }
            catch { }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                cn.Dispose();
            }
            return value;
        }
        /// <summary>
        /// Thực thi câu lênh sql trả về giá trị kiểu double
        /// </summary>
        public static double ExeSQLGetDouble(string strSQL)
        {
            double value = 0;
            SqlConnection cn = ConnectDB.GetConnection();
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(strSQL, cn);
                value = (double)cmd.ExecuteScalar();
                cn.Close();
                cmd.Dispose();
            }
            catch { }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                cn.Dispose();
            }
            return value;
        }
        public static float ExeSQLGetFloat(string strSQL)
        {
            float data = 0;
            SqlConnection cn = ConnectDB.GetConnection();
            SqlCommand cmd = new SqlCommand(strSQL, cn);
            SqlDataReader dr;
            try
            {
                cn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    data = float.Parse(dr[0].ToString());
                }
                cn.Close();
                cmd.Dispose();
            }
            catch { }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                cn.Dispose();
            }
            return data;
        }
        public static decimal ExeSQLGetDecimal(string strSQL)
        {
            decimal value = 0;
            SqlConnection cn = ConnectDB.GetConnection();
            try
            {
                cn.Open();
                SqlCommand cmd = new SqlCommand(strSQL, cn);
                value = (decimal)cmd.ExecuteScalar();
                cn.Close();
                cmd.Dispose();
            }
            catch { }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                cn.Dispose();
            }
            return value;
        }


        /// <summary>
        ///Các hàm thực thi Stored Procedure
        /// </summary>

        /// <summary>
        /// Thực thi 1 StoreProcedure không trả lại giá trị
        /// </summary>
        public static void ExeNonStored(string StoredName, params object[] pars)
        {
            SqlConnection cn = ConnectDB.GetConnection();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandText = StoredName;
                cmd.Connection = cn;
                cn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < pars.Length; i += 2)
                {
                    SqlParameter pa = new SqlParameter(pars[i].ToString(), pars[i + 1]);
                    cmd.Parameters.Add(pa);
                }
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                cn.Dispose();
            }
        }
        public static void ExeNonStoredPayMent(string StoredName, params object[] pars)
        {
            SqlConnection cn = ConnectDB.GetConnection();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandText = StoredName;
                cmd.Connection = cn;
                cn.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < pars.Length; i += 2)
                {
                    SqlParameter pa = new SqlParameter(pars[i].ToString(), pars[i + 1]);
                    cmd.Parameters.Add(pa);
                }
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch (Exception e)
            {
                LoggingManager.WriteLogEx("Inser giao dịch thất bại:" + e.Message + e.Source + e.Data);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                cn.Dispose();
            }
        }

        /// <summary>
        /// Thực thi 1 StoreProcedure trả vể dữ liệu dạng bảng
        ///</summary>
        ///

        public static DataTable ExeStoredToDataTable(string StoredName, params object[] para)
        {
            DataTable dt = new DataTable();
            SqlConnection cn = ConnectDB.GetConnection();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = cn;
                cn.Open();
                cmd.CommandText = StoredName;
                cmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < para.Length; i += 2)
                {
                    SqlParameter pa = new SqlParameter(para[i].ToString(), para[i + 1]);
                    cmd.Parameters.Add(pa);
                }
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(dt);
                cn.Close();
                cmd.Dispose();
            }
            catch (SqlException e)
            {
                LoggingManager.WriteLogEx("Exception when ExeStoredToDataTable message: " + e.Message + " Source: " + e.Source + " StackTrace: " + e.StackTrace);
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                cn.Dispose();
            }
            return dt;
        }
        /// <summary>
        /// Thực thi 1 StoreProcedure trả về 1 DataSet
        ///</summary>
        public static DataSet ExeStoredToDataSet(string StoredName, params object[] para)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlConnection cn = ConnectDB.GetConnection();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = cn;
                cn.Open();
                cmd.CommandText = StoredName;
                cmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < para.Length; i += 2)
                {
                    SqlParameter pa = new SqlParameter(para[i].ToString(), para[i + 1]);
                    cmd.Parameters.Add(pa);
                }
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(ds);
                cn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                cn.Dispose();
            }
            return ds;
        }
        /// <summary>
        /// Thực thi 1 StoreProcedure trả về dữ liệu là 1 chuỗi (String)
        ///</summary>
        public static string ExeStoredGetData(string StoredName, params object[] para)
        {
            string data = "";
            SqlConnection cn = ConnectDB.GetConnection();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            try
            {
                cmd.Connection = cn;
                cn.Open();
                cmd.CommandText = StoredName;
                cmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < para.Length; i += 2)
                {
                    SqlParameter pa = new SqlParameter(para[i].ToString(), para[i + 1]);
                    cmd.Parameters.Add(pa);
                }
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    data = Convert.ToString(dr.GetString(0));
                }
                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                cn.Dispose();
            }
            return data;
        }
        /// <summary>
        /// Thực thi 1 StoreProcedure trả về dữ liệu là 1 số nguyên
        ///</summary>
        public static int ExeStoredGetInt(string StoredName, params object[] para)
        {
            int data = 0;
            SqlConnection cn = ConnectDB.GetConnection();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            try
            {
                cmd.Connection = cn;
                cn.Open();
                cmd.CommandText = StoredName;
                cmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < para.Length; i += 2)
                {
                    SqlParameter pa = new SqlParameter(para[i].ToString(), para[i + 1]);
                    cmd.Parameters.Add(pa);
                }
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    data = Convert.ToInt16(dr[0]);
                }
                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                cn.Dispose();
            }
            return data;
        }
        /// <summary>
        /// Thực thi 1 StoreProcedure trả về dữ liệu là 1 số nguyên
        ///</summary>
        public static double ExeStoredGetDouble(string StoredName, params object[] para)
        {
            double data = 0;
            SqlConnection cn = ConnectDB.GetConnection();
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            try
            {
                cmd.Connection = cn;
                cn.Open();
                cmd.CommandText = StoredName;
                cmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < para.Length; i += 2)
                {
                    SqlParameter pa = new SqlParameter(para[i].ToString(), para[i + 1]);
                    cmd.Parameters.Add(pa);
                }
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    data = Convert.ToDouble(dr[0]);
                }
                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                cn.Dispose();
            }
            return data;
        }

        /// <summary>
        /// Thực thi 1 StoreProcedure trả về số lượng bản ghi
        ///</summary>
        public static int ExeStoredCount(string StoredName, params object[] para)
        {
            int count = 0;
            SqlConnection cn = ConnectDB.GetConnection();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.Connection = cn;
                cn.Open();
                cmd.CommandText = StoredName;
                cmd.CommandType = CommandType.StoredProcedure;
                for (int i = 0; i < para.Length; i += 2)
                {
                    SqlParameter pa = new SqlParameter(para[i].ToString(), para[i + 1]);
                    cmd.Parameters.Add(pa);
                }
                count = (int)cmd.ExecuteScalar();
                cn.Close();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                cn.Dispose();
            }
            return count;

        }
        /// <summary>
        /// Thực thi 1 StoreProcedure trả về Giá trị lớn nhất
        ///</summary>
        public static long ExeStoredGetMax(string StoredName)
        {
            long max = 0;
            SqlConnection cn = ConnectDB.GetConnection();
            SqlCommand cmd = new SqlCommand();
            try
            {
                cmd.CommandText = StoredName;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = cn;
                cn.Open();
                max = Convert.ToInt64(cmd.ExecuteScalar());
                cn.Close();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                    cn.Close();
                cn.Dispose();
            }
            return max;
        }
    }
}
