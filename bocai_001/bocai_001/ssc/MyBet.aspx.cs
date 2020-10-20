using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bocai_001.ssc
{
    public partial class MyBet : System.Web.UI.Page
    {
        protected string _Uid="null";
        string userName = "";
        protected int _pageNext = 2;
        protected int _pagePre;
        protected string _netNameLink = "<a href=\"/User/Login.aspx\">游客[ID=0]</a>";
        protected string _money = "0";
        protected string _login = "<a href=\"/User/Login.Aspx? Cid = null & amp; v=1\">会员登陆</a>";
        protected string _login1 = "<a href=\"/User/Reg.Aspx? Cid = null & amp; v=1\">会员注册</a>";
        protected string _result = "";
        protected string _previousPage = "";
        protected int _currentPage = 1;
        protected int _totalPage = 0;
        protected int _totalPiece = 0;
        protected string _message = "";//消息
        Database db = new Database();

        protected void Page_Load(object sender, EventArgs e)
        {
            
            SupperUserInfo su = (SupperUserInfo)Session["userInfor"];
            if (su.isLogin)
            {
                string _simpid;
                _simpid = su.getsimpleUid();
                _Uid = su.Uid;
                 userName = su.userName;
                _netNameLink = "<a href=\"/User/Default.aspx?Cid=" + _Uid + "&amp;v=1\">" + su.netName + "[ID=" + su.getsimpleUid() + "]</a>";
                _money = db.getContent("userInfo", "money", "userName", userName);
                _login = "<a href=\"/User/Default.Aspx?Cid = " + _Uid + " & amp; v=1\">我的地盘</a>";
                _login1 = "<a href=\"/User/Default.Aspx?AC=Exit&amp;Cid =" + _Uid + " & amp; v=1\">安全退出</a>";
                SetPage();//加载开奖结果
                int newsNum = db.getNewsNumber(_simpid);//获取当前用户未读消息条数
                if (newsNum > 0)
                {
                    _message = "[系统]<a href=\"/User/MyXin.aspx?LX=0&amp;Cid=" + _Uid + "&amp;v=1\">你有(" + newsNum + ")条未读消息！</a>";
                }
            }
            else
            {
                Response.Redirect("/User/Login.aspx?Cid=null& amp;v=1");
            }
        }


        private DataTable getDataTable()
        {
           
            DataTable dt = new DataTable();
            dt.Columns.Add("0");
            dt.Columns.Add("1");
            dt.Columns.Add("2");
            dt.Columns.Add("3");

            SqlDataReader sdReader;
  
            sdReader = db.getDataReader("bet", "period,position,money,betStatus", "userName", userName);
            while (sdReader.Read())
            {
                DataRow dr0 = dt.NewRow();
                dr0[0] = "第"+sdReader[0]+"期:";
                dr0[1] = sdReader[1];
                dr0[2] = "共"+sdReader[2]+"币";
                dr0[3] = "(" + sdReader[3] + ")";
                dt.Rows.InsertAt(dr0,0);
            }
            db.close();
           
            return dt;
        }


        public void SetPage()
        {
            page.Text = "2";
            int PerPageNum = 15;
            DataTable dtget = getDataTable();
            _totalPiece = dtget.Rows.Count;
            if (dtget.Rows.Count % PerPageNum == 0)
            {
                _totalPage = dtget.Rows.Count / PerPageNum;
                if (_totalPage == 0)
                {
                    _totalPage = 1;
                }
            }
            else
            {
                _totalPage = dtget.Rows.Count / PerPageNum + 1;
            }

            if (_totalPage > 1)
            {
                if (Request["Page"] != null && Request["Page"] != "1")
                {
                    if (int.Parse(Request["Page"]) <= _totalPage)
                    {
                        _currentPage = int.Parse(Request["Page"]);
                        _pagePre = int.Parse(Request["Page"]) - 1;
                        _pageNext = int.Parse(Request["Page"]) + 1;
                        page.Text = (int.Parse(Request["Page"]) + 1).ToString();
                        _previousPage = "<a href=\"/ssc/MyBet.Aspx?&amp;Cid=" + _Uid + "&amp;v=1&amp;page=" + _pagePre + "\">[上页]</a>";
                    }
                    else
                    {
                        _currentPage = int.Parse(Request["Page"]) - 1;
                        _pagePre = int.Parse(Request["Page"]) - 2;
                        _pageNext = int.Parse(Request["Page"]);
                        page.Text = int.Parse(Request["Page"]).ToString();
                        _previousPage = "<a href=\"/ssc/MyBet.Aspx?&amp;Cid=" + _Uid + "&amp;v=1&amp;page=" + _pagePre + "\">[上页]</a>";
                    }
                }
            }



            int startValue = (_currentPage - 1) * PerPageNum;
            for (int i = startValue; i < startValue + PerPageNum; i++)
            {
                if (dtget.Rows.Count > i)
                {

                    DataRow dr = dtget.Rows[i];
                    //if (!dr[dtget.Columns[0].ToString()].ToString().StartsWith("【"))//如果不是以"【"开头，就标记序号
                    //{
                    //    _result += (i + 1) + ".";
                    //}
                    foreach (DataColumn item in dtget.Columns)
                    {
                        _result += dr[item.ColumnName].ToString();
                    }
                    _result += "<br />";
                }
                else
                {
                    break;
                }
            }

        }
    }
}