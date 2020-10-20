using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Collections;

namespace bocai_001
{
     class Database
    {
        SqlConnection con;
        private void open()
        {
            if (con == null)
            {
                string constr = "server=***;database=bocai;user=sa;pwd=******";
                con = new SqlConnection(constr);
            }
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
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

        public void updateContent(string table, string columnName, string value, string whereColumn, string whereValue)
        {
            string sqlsetcon = "update " + table + " set " + columnName + "='" + value + "' where " + whereColumn + "='" + whereValue + "'";
            SqlCommand command = createcommand(sqlsetcon);
            command.ExecuteNonQuery();
            close();
        }

        public bool isStopBet(string period,string table) 
        {
            bool bo=false ;
            string sql_isStop = "select stopBet from " + table +" where period ='" + period + "'";
            SqlCommand get_stop = createcommand(sql_isStop);
            if (get_stop.ExecuteScalar()!= null)
            {
                if (get_stop.ExecuteScalar().ToString()!="")
                {
                    bo = true;
                }
            }
            
            close();
            return bo;
           
        }

        public void insertContent(string table, string columnName1, string columnName2, string columnName3, string columnName4, string columnName5, string columnName6, string columnName7, string value1, string value2, string value3, string value4, string value5, string value6, string value7)
        {
            string sqlsetcon = "insert into " + table + " ( " + columnName1 + "," + columnName2 + "," + columnName3 + "," + columnName4 + "," + columnName5 + "," + columnName6 + "," + columnName7 + ")values('" + value1 + "','" + value2 + "','" + value3 + "','" + value4 + "','" + value5 + "','" + value6 + "','" + value7 + "')";
            SqlCommand command = createcommand(sqlsetcon);
            command.ExecuteNonQuery();
            close();
        }
        public int bet(string betType,string userName, string netName, string period,string position,string positionType, string money ,string table,string win,string betTime)
        {
            int i=-1;
            string sqlbet = "insert into "+table+ " (userName,betType,netName,period,position,positionType,money,winMoney,betTime,betStatus)values ('" + userName + "','" + betType + "','" + netName + "','" + period+"','"+position+"','" + positionType + "','" + money+"','"+win+ "','" + betTime + "','待开奖')";
                //"set position='" + position + "',money='"+money+", period='"+period+"'";
            SqlCommand comBet = createcommand(sqlbet);
            
                i = comBet.ExecuteNonQuery();
            
                
            close();
            return i;
        }
        public int bet(string betType, string userName, string netName, string period, string position, string positionType, string singleMoney, string money, string table, string win, string betTime)
        {
            int i = -1;
            string sqlbet = "insert into " + table + " (userName,betType,netName,period,position,positionType,singleMoney,money,winMoney,betTime,betStatus)values ('" + userName + "','" + betType + "','" + netName + "','" + period + "','" + position + "','" + positionType + "','" + singleMoney + "','" + money + "','" + win + "','" + betTime + "','待开奖')";
            //"set position='" + position + "',money='"+money+", period='"+period+"'";
            SqlCommand comBet = createcommand(sqlbet);

            i = comBet.ExecuteNonQuery();


            close();
            return i;
        }

        public int getBetMoney(string betType,string period)
        {
            int moneys = 0;
            string sqlBetMoney = "select money from bet where betType='"+betType+"'and period='"+period+"'";
            SqlCommand command = createcommand(sqlBetMoney);
            SqlDataReader sdr = command.ExecuteReader(CommandBehavior.CloseConnection);
            while(sdr.Read())
            {
                
                moneys += int.Parse(sdr["money"].ToString());
            }
            close();
            return moneys;
        }

        public ArrayList getDateOrPeriodArray(string table, string data,string whereColumn,string whereValue)
        {
            ArrayList target = new ArrayList();
            ArrayList dates = new ArrayList();
            string OnlyDate = "";
            string sql = "select " + data + " from " + table+"where "+whereColumn+"='"+whereValue+"'";
            SqlCommand cmd = createcommand(sql);
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                if (reader[data].ToString() != "")
                {
                    string date = reader[data].ToString();
                    dates.Add(date);
                }

            }
            close();
            dates.Sort();
            //dates.OrderBy(s=>Regex.Match(s,"\\d+日"));//Array数组才行
            foreach (string date0 in dates)
            {
                if (OnlyDate == "")
                {
                    target.Add(date0);
                    OnlyDate = date0;
                }
                else
                {
                    if (OnlyDate != date0)
                    {
                        OnlyDate = date0;
                        target.Add(date0);
                    }
                }
            }
            return target;
        }

        public ArrayList getDateOrPeriodArray(string table,string data)
        {
            ArrayList target = new ArrayList() ;
            ArrayList dates = new ArrayList();
            string OnlyDate = "";
            string sql = "select "+data+" from " + table;
            SqlCommand cmd = createcommand(sql);
            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            while(reader.Read())
            {
                if (reader[data].ToString()!="")
                {
                    string date = reader[data].ToString();
                    dates.Add(date);
                }
               
            }
            close();
            dates.Sort();
            dates.Reverse();
            //dates.OrderBy(s=>Regex.Match(s,"\\d+日"));//Array数组才行
            foreach (string date0 in dates)
            {
                if (OnlyDate=="")
                {
                    target.Add(date0);
                    OnlyDate = date0;
                }
                else
                {
                    if (OnlyDate!=date0)
                    {
                        OnlyDate = date0;
                        target.Add(date0);
                    }
                }
            }
            return target;
        }


        public string getContent(string table, string content)
        {
            string target = "";
            string sql = "select " + content + " from " + table ;
            SqlCommand cmd = createcommand(sql);
            if (cmd.ExecuteScalar() != null)
            {
                target = cmd.ExecuteScalar().ToString();
            }
            close();
            return target;
        }
        public string getContent(string table,string content,string whereClumn, string whereValues) 
        {
            string target="";
            string sql = "select "+ content+" from "+table+" where "+whereClumn+"='" + whereValues + "'";
            SqlCommand cmd = createcommand(sql);
            if (cmd.ExecuteScalar() != null)
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
        public SqlDataReader getDataReader(string table, string content, string whereClumn, string whereValues) 
        {
            string sqldatereader = "select " + content + " from " + table + " where " + whereClumn + "='" + whereValues + "'";
            SqlCommand cmd = createcommand(sqldatereader);
            SqlDataReader sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return sdr;
        }

        public SqlDataReader getDataReader(string table, string content, string whereClumn, string whereValues, string andwhereClumn, string danwhereValues)
        {
            string sqldatereader = "select " + content + " from " + table + " where " + whereClumn + "='" + whereValues + "' and " + andwhereClumn + "='" + danwhereValues + "'";
            SqlCommand cmd = createcommand(sqldatereader);
            SqlDataReader sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return sdr;
        }

        public SqlDataReader getDataReaderor(string table, string content, string whereClumn, string whereValues, string andwhereClumn, string andwhereValues)//查聊天记录
        {
            string sqldatereader = "select " + content + " from " + table + " where (" + whereClumn + "='" + whereValues + "' and " + andwhereClumn + "='" + andwhereValues + "') or ("+ whereClumn + "='" + andwhereValues + "' and " + andwhereClumn + "='" + whereValues + "')";
            SqlCommand cmd = createcommand(sqldatereader);
            SqlDataReader sdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return sdr;
        }

        //public string getContentNumber(string table,string content,string whereclumn,string whereValue)
        //{
        //    string target = "";
        //    string sql = "select " + content + " from " + table + " where "+whereclumn+"='"+ whereValue + "'";
        //    SqlCommand cmd = createcommand(sql);
        //    target = cmd.ExecuteScalar().ToString();
        //    close();
        //    return target;
        //}

        public int getNewsNumber(string idReceive)
        {
            open();
            int target =0;
            string sql = "select * from message  where status='未读' and idReceive='"+ idReceive + "'";
            //SqlCommand cmd = createcommand(sql);
            DataSet ds = new DataSet();
            SqlDataAdapter sda = new SqlDataAdapter(sql,con);
            sda.Fill(ds,"message");
            target = ds.Tables[0].Rows.Count;
            return target;
        }


        
        public void close()
        {
            if (con != null)
            {
                con.Close();
            }
        }

        private SqlDataReader getDataReader(string sql) 
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