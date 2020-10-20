using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bocai_001.ssc
{
    public partial class Xin_Add : System.Web.UI.Page
    {
        protected string _chatContent = "";
        protected string _Uid = "null";
        protected string _idReceive = "null";//聊天对象id
        protected string _time = "";//消息发送时间
        DateTime dt = DateTime.Now;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            SupperUserInfo su = (SupperUserInfo)Session["userInfor"];
   
            if (su.isLogin)
            {
               
                string netName;
                string _simpid;
                string _dateTime="";//消息发送距离现在的时间
                _Uid = su.Uid;
                netName = su.netName;
                _simpid = su.getsimpleUid();
                Database db = new Database();

                _idReceive = Request["Sid"];
                SqlDataReader sqlreader;
                sqlreader = db.getDataReaderor("message", "netName,news,time,status", "idSend",_simpid,"idReceive",_idReceive);
                while (sqlreader.Read())
                {
                    _time = sqlreader["time"].ToString();
                    _dateTime = gettime();
                    if (sqlreader["netName"].ToString()==netName)
                    {
                        _chatContent = "我:"+sqlreader["news"] +"("+ _dateTime +","+ sqlreader["status"] + ")<br />" + _chatContent;
                    }
                    else
                    {
                        _chatContent = sqlreader["netName"] + ":" + sqlreader["news"] + "(" + _dateTime + "," + sqlreader["status"] + ")<br />" + _chatContent;
                    }
                   
                }
                db.close();
            }
            else
            {
                Response.Redirect("/User/Login.aspx?Cid=<%=_Uid %>& amp;v=1");
            }
        }

        private string gettime() 
        {
            int day;
            int hour;
            int min;
            int sec;
            string time="";
            TimeSpan ts = dt - Convert.ToDateTime(_time);
            double timeS = ts.TotalSeconds;
            day = (int)(timeS /86400);
            hour = (int)(timeS %86400 / 3600);
            min= (int)(timeS %86400 % 3600/60);
            sec = (int)(timeS % 86400 % 3600 %60);
            if (day>0)
            {
                time = day + "天前";
            }
            else if(hour>0)
            {
                time = hour + "小时前";
            }
            else if(min>0)
            {
                time = min + "分钟前";
            }
            else
            {
                time = sec + "秒前";
            }
            return time;
        }
    }
}