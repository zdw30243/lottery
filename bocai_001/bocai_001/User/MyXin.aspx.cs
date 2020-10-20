using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace bocai_001.User
{
    public partial class MyXin : System.Web.UI.Page
    {
        protected string _Uid = "null";
        protected string _result = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            SupperUserInfo su = (SupperUserInfo)Session["userInfor"];
            if (su.isLogin)
            {
                string netName;
                string _simpid;
                Database db = new Database();
                _Uid = su.Uid;
                 netName = su.netName;
                _simpid = su.getsimpleUid();
                SqlDataReader sqlreader;

                sqlreader = db.getDataReader("message","idSend,news,idNews","idReceive",_simpid,"status","未读");//获取当前用户未读消息
                while (sqlreader.Read())
                {
                    if (sqlreader["idSend"].ToString()=="2")
                    {
                        _result = "<a href=\"/User/Xin_Look.Aspx?Sid="+ sqlreader["idNews"] + "&amp;Cid="+_Uid+"&amp;v=1\">"+ sqlreader["news"] + "</a>" + "<br/>" + _result;
                    }
                    else
                    {
                        _result = "<a href =\"/User/Xin_Add.Aspx?Sid="+ sqlreader["idSend"] + "&amp;Cid="+ _Uid + "&amp;v=1\" >"+ sqlreader["news"] + "</a> "+"<br/>" + _result;
                    }
                  
                }
                db.close();
            }
            else
            {
                Response.Redirect("/User/Login.aspx?Cid=<%=_Uid %>& amp;v=1");
            }
        }
    }
}