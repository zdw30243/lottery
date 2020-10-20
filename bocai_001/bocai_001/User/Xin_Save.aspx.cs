using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bocai_001.User
{
    public partial class Xin_Save : System.Web.UI.Page
    {
        protected string _send = "";//页面提示内容
        protected string _Uid = "";
        protected string _idReceive;//消息接收者id
        protected void Page_Load(object sender, EventArgs e)
        {


            SupperUserInfo su = (SupperUserInfo)Session["userInfor"];
            Database db = new Database();
            if (su.isLogin)
            {
                string _chartContent = "";
                string _netName ;
                string _simpid ;
                _Uid = su.Uid;
                _netName = su.netName;
                _simpid = su.getsimpleUid();
                _idReceive = Request["Sid"];
                _chartContent = Request["Text"];
                if (_chartContent!="")
                {
                    db.insertContent("message", "idSend", "idReceive", "netName", "news", "status", "source", "time", _simpid, _idReceive, _netName, _chartContent, "未读", _netName, DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"));
                    _send = "消息发送成功!!";
                }
                else
                {
                    _send = "请输入您要发送的内容!";
                }


            }
            else
            {
                Response.Redirect("/User/Login.aspx?Cid=<%=_Uid %>& amp;v=1");
            }
        }
    }
}