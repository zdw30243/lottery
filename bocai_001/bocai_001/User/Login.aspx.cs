using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bocai_001.User
{
    
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected string _stt = "";//提示
        protected string _Uid = "null";
        protected void Page_Load(object sender, EventArgs e)
        {
            SupperUserInfo su =  (SupperUserInfo)Session["userInfor"];
            Database db = new Database();
            if (IsPostBack)
            {
                if (tbUserName.Text.Trim() == "" || tbPwd.Text.Trim() == "")
                {
                    _stt = "用户名或密码不能为空！";
                    MultiView1.SetActiveView(View2);

                }
                else
                {
                    su.userName = tbUserName.Text.Trim();
                    su.Pwd = db.md5(tbPwd.Text.Trim());
                    if (!su.tryLogin())
                    {
                        _stt = "请输入正确的用户名或密码！";
                        MultiView1.SetActiveView(View2);
                    }
                    else
                    {

                        _stt = "登陆成功！"; 
                        su.setCookies();
                        _Uid = su.Uid;
                        MultiView1.SetActiveView(View3);
                       
                      
                    }
                }
            }
            else
            {
                MultiView1.SetActiveView(View1);
            }
        }
    }
}