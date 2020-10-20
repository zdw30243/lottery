using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bocai_001.User
{
    public partial class Xin_Look : System.Web.UI.Page
    {
        protected string _source = "";
        protected string _time = "";
        protected string _Uid = "";
        protected string _result = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            SupperUserInfo su = (SupperUserInfo)Session["userInfor"];
            if (su.isLogin)
            {
                string idNews;
                _Uid = su.Uid;
                idNews = Request["Sid"].ToString();
                Database db = new Database();
                SqlDataReader sqlreader;

                sqlreader = db.getDataReader("message", "source,news,time", "idNews", idNews);//查找消息
                while (sqlreader.Read())
                {
                    _source = sqlreader["source"].ToString();
                    _time= sqlreader["time"].ToString();
                    _result= sqlreader["news"].ToString();
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