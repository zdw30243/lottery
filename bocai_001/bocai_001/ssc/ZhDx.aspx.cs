using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bocai_001.ssc
{
    public partial class ZhDx : System.Web.UI.Page
    {
        string netName = "";
        protected string _Uid = "";
        protected string data = "";
        protected string ball1= "0";
        protected string ball2= "0";
        protected string ball3= "0";
        protected string ball4= "0";
        protected string ball5= "0";
        protected int qishu;
        protected string currentQishu= "当前期数：第000期";
        protected string _login = "<a href=\"/User/Login.Aspx? Cid = null & amp; v=1\">会员登陆</a>";
        protected string _login1 = "<a href=\"/User/Reg.Aspx? Cid = null & amp; v=1\">会员注册</a>";
        protected string _netNameLink = "<a href=\"/User/Login.aspx\">游客[ID=0]</a>";
        protected string _money = "0";
        protected string _simpid;
        protected string userName;
        protected string _a = "";
        protected string _b = "";
        protected string winMoney = "";
        protected string _d = "";
        protected string _stopOrSuccess;
        protected string _result;
        protected string _message="";//消息
        DateTime dt = DateTime.Now;

        protected void Page_Load(object sender, EventArgs e)
        {

            string qishuS;
            SupperUserInfo su = (SupperUserInfo)Session["userInfor"];
            Database db = new Database();
            string time = dt.ToString("yyyyMMdd");//用于期数的年月日
            string time0 = dt.ToShortDateString();//用于给javascript的截止时间的年月日
            long time1 = GetTimeStamp();
            long todayStamp = time1 % (24 * 60 * 60 * 1000);

            if (todayStamp < 30 * 60 * 1000)
            {
                qishu = 59;

            }
            else if (todayStamp < (3 * 60 * 60 * 1000 + 11 * 60 * 1000))
            {
                int qishuNum = (int)(todayStamp - 30 * 60 * 1000) / (20 * 60 * 1000) + 1;
                qishu = qishuNum;

            }
            else if (todayStamp < (7 * 60 * 60 * 1000 + 30 * 60 * 1000))
            {
                qishu = 9;

            }
            else
            {
                int qishuNum1 = (int)(todayStamp - (7 * 60 * 60 * 1000 + 30 * 60 * 1000)) / (20 * 60 * 1000) + 10;
                qishu = qishuNum1;

            }

           
            if (qishu < 10)
            {
                qishuS = "00" + qishu;
            }
            else
            {
                qishuS = "0" + qishu;
            }
            currentQishu = "第" + time + "-" + qishuS + "期";//显示当前期数号：年月日期数号
            
            
            if (qishu == 59)
            {

                data = time0 + " 00:27:42";
            }
            else if (qishu < 9)
            {
                int t0 = 27 * 60 + 52;//第一期截至时相对当天0时的秒数
                int t_1 = 20 * 60; //每一期增加的秒数
                int times = qishu;
                int totle = t0 + t_1 * times;
                int hour = totle / 3600;
                int min = totle % 3600 / 60;
                int sec = totle % 60;
                data = time0 + " " + hour + ":" + min + ":" + sec;
            }
            else
            {
                int t0 = 7 * 3600 + 27 * 60 + 52;//早上7:30开
                int t_1 = 20 * 60; //每一期增加的秒数
                int times = qishu - 9;
                int totle = t0 + t_1 * times;
                int hour = totle / 3600;
                int min = totle % 3600 / 60;
                int sec = totle % 60;
                data = time0 + " " + hour + ":" + min + ":" + sec;

            }


            //判断登陆
            if (su.isLogin)
            {
                 _Uid = su.Uid;
                userName = su.userName;
                netName= su.netName;
                _simpid = su.getsimpleUid();
                _netNameLink = "<a href=\"/User/Default.aspx?Cid=" + _Uid + "&ampv=1\">" + su.netName + "[ID=" + _simpid + "]</a>";
                _money = db.getContent("userInfo","money","userName",userName);
                _login = "<a href=\"/User/Default.Aspx?Cid = " + _Uid + " & amp; v=1\">我的地盘</a>";
                _login1 = "<a href=\"/User/Default.Aspx?AC=Exit&amp;Cid =" + _Uid + " & amp; v=1\">安全退出</a>";

                int newsNum= db.getNewsNumber(_simpid);//获取当前用户未读消息条数
                if (newsNum>0)
                {
                    _message = "[系统]<a href=\"/User/MyXin.aspx?LX=0&amp;Cid=" + _Uid + "&amp;v=1\">你有("+newsNum+")条未读消息！</a>";
                }
            }

            string periodBet;
            string qishu0;//不带日期，但前面加0的期数
            if (qishu == 59)
            {
                periodBet = dt.AddDays(1).ToString("yyyyMMdd") +"001";
            }
            else
            {

                if (qishu < 9)
                {
                    qishu0 = "00" + (qishu + 1);
                }
                else
                {
                    qishu0 = "0" + (qishu + 1);
                }

                periodBet = time + qishu0;//显示当前投注期数号：年月日期数号

            }

            if (db.isStopBet(periodBet, "cqsscResult"))//判断是否截止
            {
                _stopOrSuccess = "本期已经截止！";
                MultiView1.SetActiveView(View3);
            }
            else
            {
                if (Request["Ac"] == "Bet")
                {

                    float peiLv;//赔率
                    if (int.Parse(Request["v"]) == 1)
                    {

                        if (Request["Multiple"] == ""|| Request["P1"]=="")
                        {
                            MultiView1.SetActiveView(View1);
                        }
                        else
                        {
                            if (int.Parse(Request["P1"]) == 1)
                            {
                                _a = "小";
                            }
                            else
                            {
                                _a = "大";
                            }
                            peiLv = float.Parse(db.getContent("peiLvTable", "peiLv", "betType", "ssc", "peiLvType", "大小单双"));//获取赔率值
                            _b = Request["Multiple"];
                            int i = int.Parse(_b);
                            winMoney = (i * peiLv).ToString();

                            if (!su.isLogin)
                            {
                                _d = "卵弹琴，你没有那么多金币,<a href=\"/User/Default.aspx?Cid=<%=_Uid %>\" >前去充值</ a>";
                                 MultiView1.SetActiveView(View2);
                            }
                            else
                            {
                                if (float.Parse(db.getContent("userInfo","money","userName",userName)) < i)
                                {
                                    _d = "卵弹琴，你没有那么多金币,<a href=\"/User/Default.aspx?Cid=<%=_Uid %>\" >前去充值</ a>";
                                     MultiView1.SetActiveView(View2);
                                }
                                else
                                {
                                Session["position"] = _a;
                                Session["money"] = _b;
                                Session["winMoney"]= winMoney;
                                _d = "<input type=\"submit\"  class=\"submit\" value=\"立即下注\" >";
                                MultiView1.SetActiveView(View2);
                               }
                            }
                        }
                    }
                    else if (int.Parse(Request["v"]) == 2)//V=2代表一切投注条件都满足了，包括余额充足
                      {
                        _a= Session["position"].ToString();
                         _b = Session["money"].ToString();//总共费用
                         winMoney = Session["winMoney"].ToString();
                        //string period = DateTime.Now.ToString("yyyyMMdd") + qishu;
                        int check= db.bet("cqssc", userName,netName,periodBet, _a, "总和大小",_b,"Bet",winMoney,DateTime.Now.ToString("yyyy-MM-dd h:mm:ss"));

                        if (check==1)
                        {
                            float moneys=float.Parse(db.getContent("userInfo","money","userName",userName));
                            float remainMoney = moneys - float.Parse(_b);
                            db.updateContent("userInfo","money",remainMoney.ToString(),"userName",su.userName);
                            _stopOrSuccess = "恭喜你，下注成功！";

                            MultiView1.SetActiveView(View3);
                        }

                      }
                }
                else
              {
                 MultiView1.SetActiveView(View1);
              }
            }
            string period0;//最近开奖的这期
            if (qishu < 10)
            {
                qishuS = "00" + qishu;
            }
            else
            {
                qishuS = "0" + qishu;
            }
            period0 = time + qishuS;//最近开奖的这期
            string resultball5 = db.getContent("cqsscResult", "resultBall", "period", period0);//获取五个球开奖结果
            if (resultball5 != "")
            {
                string[] arr = resultball5.Split('-');
                ball1 = arr[0];
                ball2 = arr[1];
                ball3 = arr[2];
                ball4 = arr[3];
                ball5 = arr[4];
                _result = " <div >" + ball1 + "</div><div >" + ball2 + "</div><div >" + ball3 + "</div><div >" + ball4 + "</div><div>" + ball5 + "</div>";
            }
            else
            {
                _result = " <div >摇</ div>";
            }
        }

        private long GetTimeStamp()
        {
            TimeSpan ts = dt- new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds);
        }
    }

}
