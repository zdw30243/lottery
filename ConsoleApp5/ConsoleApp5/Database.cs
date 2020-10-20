using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace ConsoleApp5
{
     class Database
    {
        SqlConnection con;
        private void open()
        {
            if (con == null)
            {
                string constr = "server=ZDW;database=bocai;user=sa;pwd=zdw123456";
                con = new SqlConnection(constr);
            }
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }


        }


        public void insertContent(string table, string columnName, string value)
        {
            try
            {
                string sqlsetcon = "insert into " + table + " ( " + columnName + ")values(" + value + ")";
                SqlCommand command = createcommand(sqlsetcon);
                command.ExecuteNonQuery();
                close();
            }
            catch (Exception)
            {

            }
            
            close();
        }

        public void insertContent(string table, string columnName1,string columnName2,string value1, string value2)
        {
            string sqlsetcon = "insert into " + table + " ( " + columnName1+","+ columnName2+ ")values('" + value1+"','"+ value2+ "')";
            SqlCommand command = createcommand(sqlsetcon);
            command.ExecuteNonQuery();
            close();
        }

        public void insertContent(string table, string columnName1, string columnName2, string columnName3, string value1, string value2, string value3)
        {
            string sqlsetcon = "insert into " + table + " ( " + columnName1 + "," + columnName2 + "," + columnName3+ ")values('" + value1 + "','" + value2 + "','" + value3 +"')";
            SqlCommand command = createcommand(sqlsetcon);
            command.ExecuteNonQuery();
            close();
        }

        public void insertContent(string table, string columnName1, string columnName2, string columnName3, string columnName4, string columnName5, string columnName6, string columnName7, string value1, string value2, string value3, string value4, string value5, string value6, string value7)
        {
            string sqlsetcon = "insert into " + table + " ( " + columnName1 + "," + columnName2 + "," + columnName3 + "," + columnName4 + "," + columnName5 + "," + columnName6 + "," + columnName7 + ")values('" + value1 + "','" + value2 + "','" + value3 + "','" + value4 + "','" + value5 + "','" + value6 + "','" + value7+ "')";
            SqlCommand command = createcommand(sqlsetcon);
            command.ExecuteNonQuery();
            close();
        }
        public void updateContent(string table,string columnName,string value,string whereColumn,string whereValue) 
        {
            string sqlsetcon = "update "+table+" set "+columnName+"='"+value+"' where "+whereColumn+"='"+whereValue+"'";
            SqlCommand command = createcommand(sqlsetcon);
            command.ExecuteNonQuery();
            close();
        }

        public void updateContent(string table, string columnName1,string columnName2, string value1,string value2, string whereColumn, string whereValue)
        {
            string sqlsetcon = "update " + table + " set " + columnName1 + "='" + value1+"',"+columnName2+"='"+value2 + "' where " + whereColumn + "='" + whereValue + "'";
            SqlCommand command = createcommand(sqlsetcon);
            command.ExecuteNonQuery();
            close();
        }

        public void updateContent(string table, string columnName1, string columnName2, string columnName3, string value1, string value2, string value3, string whereColumn, string whereValue)
        {
            string sqlsetcon = "update " + table + " set " + columnName1 + "='" + value1 + "'," + columnName2 + "='" + value2 + "'," + columnName3 + "='" + value3 + "' where " + whereColumn + "='" + whereValue + "'";
            SqlCommand command = createcommand(sqlsetcon);
            command.ExecuteNonQuery();
            close();
        }

        public void updateContent(string table, string columnName1, string columnName2, string columnName3, string columnName4, string value1, string value2, string value3, string value4, string whereColumn, string whereValue)
        {
            string sqlsetcon = "update " + table + " set " + columnName1 + "='" + value1 + "'," + columnName2 + "='" + value2 + "'," + columnName3 + "='" + value3 + "'," + columnName4 + "='" + value4 + "' where " + whereColumn + "='" + whereValue + "'";
            SqlCommand command = createcommand(sqlsetcon);
            command.ExecuteNonQuery();
            close();
        }

        public void updateContent(string table, string columnName1, string columnName2, string columnName3, string columnName4, string columnName5, string value1, string value2, string value3, string value4, string value5, string whereColumn, string whereValue)
        {
            string sqlsetcon = "update " + table + " set " + columnName1 + "='" + value1 + "'," + columnName2 + "='" + value2 + "'," + columnName3 + "='" + value3 + "'," + columnName4 + "='" + value4 + "'," + columnName5 + "='" + value5 + "' where " + whereColumn + "='" + whereValue + "'";
            SqlCommand command = createcommand(sqlsetcon);
            command.ExecuteNonQuery();
            close();
        }
        public string md5(string pwd)
        {
            string returnPwd = "";
            MD5 md5 = MD5.Create();
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(pwd);
            bytes = md5.ComputeHash(bytes);
            md5.Clear();
            for (int i = 0; i < bytes.Length; i++)
            {
                returnPwd += Convert.ToString(bytes[i], 16).PadLeft(2, '0');
            }
            return returnPwd.PadLeft(32, '0');
        }

        //public string getUid(string userName)
        //{
        //     return getContent(userName, "Uid"); 
        //}

        public bool matching(string name, string dataBaseContent)
        {
            bool bo = false;
            string sql = "select " + dataBaseContent + " from userInfo";
            SqlCommand cmd = createcommand(sql);
            SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                if (name == dr[dataBaseContent].ToString())
                {
                    bo = true;
                    break;
                }
            }
            close();
            return bo;
        }
    
        public bool isStopBet(string period,string table) 
        {
            bool bo=false ;
            string sql_isStop = "select stopBet from " + table +" where period ='" + period + "'";
            SqlCommand get_stop = createcommand(sql_isStop);
            if (get_stop.ExecuteScalar().ToString()!=null)//如果没截至，数据库中还是空值
            {
                if (get_stop.ExecuteScalar().ToString()=="已截止")
                {
                    bo = true;
                }
               
            }
            
            close();
            return bo;
           
        }

        public int bet(string id, string period,string position,string money ,string table,string win)
        {
            int i=-1;
            string sqlbet = "insert into "+table+ " (Uid,period,position,money,winMoney,betStatus)values ('" + id + "','" + period+"','"+position+"','"+money+"','"+win+"','待开奖')";
                //"set position='" + position + "',money='"+money+", period='"+period+"'";
            SqlCommand comBet = createcommand(sqlbet);
            
                i = comBet.ExecuteNonQuery();
            
                
            close();
            return i;
        }



        public string getResult(string table,string period) 
        {
            string result = "";
            string sqlTypeResult = "select result from " + table + "Result where period='" + period + "'";// 根据判断是否有开奖结果，来进行兑奖。因为表后面是以Result结尾的，所以为了方便写在这里了
            SqlCommand conResult = createcommand(sqlTypeResult);
            if (conResult.ExecuteScalar()!=null)
            {
                result = conResult.ExecuteScalar().ToString();
            }
            close();
            return result;
        }
        public string getContent(string table, string content, string whereClumn, string whereValues)
        {
            string target = "";
            string sql = "select " + content + " from " + table + " where " + whereClumn + "='" + whereValues + "'";
            SqlCommand cmd = createcommand(sql);
            if (cmd.ExecuteScalar()!=null)
            {
                target = cmd.ExecuteScalar().ToString();
            }
            close();
            return target;
        }

        public string getContent(string table, string content, string whereClumn, string whereValues, string whereClumn2, string whereValues2)
        {
            string target = "";
            string sql = "select " + content + " from " + table + " where " + whereClumn + "='" + whereValues + "' and " + whereClumn2 + "='" + whereValues2 + "'";
            SqlCommand cmd = createcommand(sql);
            if (cmd.ExecuteScalar() != null)
            {
                target = cmd.ExecuteScalar().ToString();
            }
            close();
            return target;
        }
        public bool existPeriod(string periodNumber,string table) //期数是否存在，用于判断截至 和 获取外部开奖
        {
            open();
            bool bo = false;
            DataSet ds = new DataSet();
            string sqlbo = "select * from " + table + " where period='" + periodNumber + "'";
            SqlDataAdapter sda = new SqlDataAdapter(sqlbo,con);
            sda.Fill(ds,table);
            if (ds.Tables[0].Rows.Count>0)
            {
               bo = true;
            }
            
            //DataSet ds = new DataSet();
            //string sqlbo = "select * from "+table;
            //SqlCommand sco = createcommand(sqlbo);
            //SqlDataReader sda = sco.ExecuteReader();
            //while (sda.Read())
            //{
            //    if (sda["period"].ToString() == periodNumber)
            //    {
            //        bo = true;
            //        break;
            //    }
            //}
            close();
            return bo;
        }


        
        public DataTable getDataTable()//自动兑奖专用方法
        {
            open();
            DataTable dt = null; ;
            DataSet ds = new DataSet();
            string sql = "select * from bet where betStatus='待开奖'";
            SqlDataAdapter sda = new SqlDataAdapter(sql,con);
            sda.Fill(ds,"bet");
            if (ds.Tables.Count>0)
            {
                dt = ds.Tables[0];
            }
            close();
            return dt;
        }
        public  void close()
        {
            if (con != null)
            {
                con.Close();
            }
        }

        public  SqlDataReader getDataReader(string sql) 
        {
            SqlDataReader dr;
            SqlCommand cmd = createcommand(sql);
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return dr;

        }
        private SqlCommand createcommand(string sqlStr)
        {
            open();
            SqlCommand com = new SqlCommand(sqlStr, con);
            return com;
        }
        public void runSql(string sqlStr)
        {
            SqlCommand cmd = createcommand(sqlStr);
            cmd.ExecuteNonQuery();
            close();
        }
        public string runSqlInt(string sqlStr)
        {
            SqlCommand cmd = createcommand(sqlStr);
            string t = cmd.ExecuteScalar().ToString();
            close();
            return t;
        }
        public SqlDataReader runSql(string sqlStr, out SqlDataReader dr)
        {
            SqlCommand cmd = createcommand(sqlStr);
            dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return dr;

        }

        public DataTable ConverDataReaderToDataTable(SqlDataReader datareader)
        {
            DataTable dt = new DataTable();
            DataTable schemaTable = datareader.GetSchemaTable();
            try
            {
                foreach (DataRow myRow in schemaTable.Rows)
                {
                    // DataColumn myDataColum = new DataColumn(myRow[0].ToString());
                    DataColumn myDataColum = new DataColumn(myRow[0].ToString());

                    myDataColum.DataType = System.Type.GetType("System.String");
                    dt.Columns.Add(myDataColum);
                }
                while (datareader.Read())
                {
                    DataRow mydataRow = dt.NewRow();
                    for (int i = 0; i < schemaTable.Rows.Count; i++)
                    {
                        mydataRow[i] = datareader[i].ToString();
                    }
                    dt.Rows.Add(mydataRow);
                    mydataRow = null;
                }
                schemaTable = null;
                datareader.Close();
                return dt;
            }
            catch (Exception ex)
            {

                throw new Exception("转换出错", ex);
            }
        }
    }

}