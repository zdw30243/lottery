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
    //开奖结果
    public partial class List : System.Web.UI.Page
    {
        protected string _Uid="null";
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
       // protected string _pageText = "2";
        Database db = new Database();

        protected void Page_Load(object sender, EventArgs e)
        {
           
            SupperUserInfo su = (SupperUserInfo)Session["userInfor"];
            if (su.isLogin)
            {
                string _simpid;
                _simpid = su.getsimpleUid();
                 _Uid = su.Uid;
                string userName = su.userName;
                _netNameLink = "<a href=\"/User/Default.aspx?Cid=" + _Uid + "&ampv=1\">" + su.netName + "[ID=" + su.getsimpleUid() + "]</a>";
                _money = db.getContent("userInfo", "money", "userName", userName);
                _login = "<a href=\"/User/Default.Aspx?Cid = " + _Uid + " & amp; v=1\">我的地盘</a>";
                _login1 = "<a href=\"/User/Default.Aspx?AC=Exit&amp;Cid =" + _Uid + " & amp; v=1\">安全退出</a>";
                int newsNum = db.getNewsNumber(_simpid);//获取当前用户未读消息条数
                if (newsNum > 0)
                {
                    _message = "[系统]<a href=\"/User/MyXin.aspx?LX=0&amp;Cid=" + _Uid + "&amp;v=1\">你有(" + newsNum + ")条未读消息！</a>";
                }
            }
            SetPage();//加载开奖结果
        }


        private DataTable getDataTable()
        {
            ArrayList dates = db.getDateOrPeriodArray("cqsscResult","date");//获取开奖结果的日期的集合（不重复）
            DataTable dt = new DataTable();
            dt.Columns.Add("qishu");
            dt.Columns.Add("resuslBall");
            foreach (string date in dates)
            {

                SqlDataReader sdReader;
                DataRow dr = dt.NewRow();
                dr[0] = "【" + date + "】";
                dt.Rows.Add(dr);
                int index = dt.Rows.Count;//期号或日期所在位置后一位
                sdReader = db.getDataReader("cqsscResult", "qishu,resultForList", "date", date);
                while (sdReader.Read())
                {
                    if (sdReader[1].ToString()!="")
                    {
                        DataRow dr0 = dt.NewRow();
                        dr0[0] = "第" + sdReader[0] + "期";
                        dr0[1] = sdReader[1];
                        dt.Rows.InsertAt(dr0,index);//插入期号或日期所在位置后一位
                    }
                   
                }
                db.close();
            }
            return dt;
        }

        public void SetPage()
        {
            //_pageText = "2";
            int PerPageNum = 15;
            DataTable dtget = getDataTable();
            _totalPiece = dtget.Rows.Count;
            if (dtget.Rows.Count % PerPageNum == 0)
            {
                _totalPage = dtget.Rows.Count / PerPageNum;
            }
            else
            {
                _totalPage = dtget.Rows.Count / PerPageNum + 1;
            }

            if (_totalPage>1)
            {
                if (Request["Page"] != null && Request["Page"] != "1")
                {
                    if (int.Parse(Request["Page"]) <= _totalPage)
                    {
                        _currentPage = int.Parse(Request["Page"]);
                        _pagePre = int.Parse(Request["Page"]) - 1;
                        _pageNext = int.Parse(Request["Page"]) + 1;
                        //_pageText = (int.Parse(Request["Page"]) + 1).ToString();
                        _previousPage = "<a href=\"/ssc/List.Aspx?&amp;Cid=" + _Uid + "&amp;v=1&amp;page=" + _pagePre + "\">[上页]</a>";
                    }
                    else
                    {
                        _currentPage = int.Parse(Request["Page"]) - 1;
                        _pagePre = int.Parse(Request["Page"]) - 2;
                        _pageNext = int.Parse(Request["Page"]);
                        //_pageText = int.Parse(Request["Page"]).ToString();
                        _previousPage = "<a href=\"/ssc/List.Aspx?&amp;Cid=" + _Uid + "&amp;v=1&amp;page=" + _pagePre + "\">[上页]</a>";                      
                    }
                }
            }
           


            int startValue = (_currentPage - 1) * PerPageNum;
            for (int i = startValue; i < startValue + PerPageNum; i++)
            {
                if (dtget.Rows.Count > i)
                {
                    DataRow dr = dtget.Rows[i];
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