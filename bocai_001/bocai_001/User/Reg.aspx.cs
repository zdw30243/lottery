using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

namespace bocai_001.User
{
    public partial class Reg : System.Web.UI.Page
    {
        protected string _remind = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
               
                Database db = new Database();
                bool boo = true;

                while (boo)
                {
                    if (tbPhone.Text.Trim() == "")
                    {
                        _remind = "请输入正确手机号！";
                        boo=false;
                        MultiView1.SetActiveView(View2);
                        break;
                    }

                    MatchCollection mc = Regex.Matches(tbPhone.Text.Trim(), "1\\d{10}");
                    if (mc.Count == 0)
                    {
                        _remind = "请输入正确手机号！";
                        boo = false;
                        MultiView1.SetActiveView(View2);
                        break;
                    }

                    if (tbNetname.Text.Trim() == "")
                    {
                        _remind = "请输入昵称！";
                        boo = false;
                        MultiView1.SetActiveView(View2);
                        break;
                    }

                    if (tbPwd.Text.Trim() == "" || tbPwd1.Text.Trim() == "")
                    {
                        _remind = "必须输入密码！";
                        boo = false;
                        MultiView1.SetActiveView(View2);
                        break; 
                    }

                    if (tbPwd.Text != tbPwd1.Text)
                    {
                        _remind = "两次密码输入不一致！";
                        boo = false;
                        MultiView1.SetActiveView(View2);
                        break;
                    }


                    if (db.matching(tbNetname.Text.Trim(), "netName"))
                    {
                        _remind = "昵称已被使用";
                        boo = false;
                        MultiView1.SetActiveView(View2);
                        break;
                    }
                    if (db.matching(tbPhone.Text.Trim(),"userName"))
                    {
                        _remind = "此用户名已被注册！";
                        boo = false;
                        MultiView1.SetActiveView(View2);
                        break;
                    }

                    if (tbPwd.Text.Trim().Length < 6)
                    {
                        _remind = "密码需至少6位数！";
                        boo = false;
                        MultiView1.SetActiveView(View2);
                        break;
                    }
                    string sqlAdd = "insert into userInfo (userName,netName,Pwd) values ('" + tbPhone.Text.Trim() + "','" + tbNetname.Text.Trim() + "','" + db.md5(tbPwd.Text.Trim())+"')";
                    db.runSql(sqlAdd); 
                    boo = false;
                    _remind = "恭喜你，注册成功！";
                    MultiView1.SetActiveView(View3);

                }
                
       
            }
            else 
            {
                MultiView1.SetActiveView(View1);
            }
        }

    }
}