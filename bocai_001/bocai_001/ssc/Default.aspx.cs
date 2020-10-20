using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace bocai_001.ssc
{
    public partial class Default : System.Web.UI.Page
    {
        protected string _Uid="null";
        protected int qishu=0;
        protected string _netNameLink = "<a href=\"/User/Login.aspx\">游客[ID=0]</a>";
        protected string _money = "0";
        protected string _login = "<a href=\"/User/Login.Aspx? Cid = null & amp; v=1\">会员登陆</a>";
        protected string _login1 = "<a href=\"/User/Reg.Aspx? Cid = null & amp; v=1\">会员注册</a>";
        protected string  _currentQishu;
        protected string _datetime;
        protected string _stopTime;
        protected string _openTime;
        protected int _nextPeriod;
        protected string _currentBet = "";
        protected string _ball = "<a href = \"/ssc/List.Aspx?Cid=null&amp;v=1\"> 摇 </a>";
        protected string _ball1="";
        protected string _ball2="";
        protected string _ball3="";
        protected string _ball4="";
        protected string _ball5="";
        protected string userName;
        protected string _message = "";//消息
        protected string _tongGao = "";//通告
        DateTime dt = DateTime.Now;
        protected void Page_Load(object sender, EventArgs e)
        {
            SupperUserInfo su = (SupperUserInfo)Session["userInfor"];
            Database db = new Database();

            string time = dt.ToString("yyyyMMdd");
            string monthDay = dt.ToString("MM-dd");
            int year = (int)dt.Year;
            int month = (int)dt.Month;
            int day = (int)dt.Day;
            int hour=0;//小时
            int min=0;//分钟
            int sec=0;//秒

            _tongGao = db.getContent("tongGao","message");//获取通告

            //计算最近已经开奖的期数
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

            if (su.isLogin)//判断是否已经登陆
            {
                string _simpid;
                _simpid = su.getsimpleUid();
                _Uid = su.Uid;
                userName = su.userName;
                int betMoney = 0;
                string betPeriod;//带日期期数
                string qishu0;//不带日期，但前面加0的期数
                _netNameLink = "<a href=\"/User/Default.aspx?Cid=" + _Uid + "&ampv=1\">" + su.netName + "[ID=" + su.getsimpleUid() + "]</a>";
                _money = db.getContent("userInfo","money","userName",userName);
                _login = "<a href=\"/User/Default.Aspx?Cid = " + _Uid + " & amp; v=1\">我的地盘</a>";
                _login1 = "<a href=\"/User/Default.Aspx?AC=Exit&amp;Cid =" + _Uid + " & amp; v=1\">安全退出</a>";
                int newsNum = db.getNewsNumber(_simpid);//获取当前用户未读消息条数

                if (newsNum > 0)
                {
                    _message = "[系统]<a href=\"/User/MyXin.aspx?LX=0&amp;Cid=" + _Uid + "&amp;v=1\">你有(" + newsNum + ")条未读消息！</a>";
                }

                //获取投注期的带日期期号
                if (qishu == 59)
                {
                    betPeriod = dt.AddDays(1).ToString("yyyyMMdd") + "001";
                    qishu0 = "001";
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

                    betPeriod = time + qishu0;//显示当前投注期数号：年月日期数号

                }
                betMoney = db.getBetMoney("cqssc", betPeriod);
                _currentBet = "本期已下注:<a href=\"\">" + betMoney + "</a>金币";//获取当期投注总额
            }



            //获取开奖号码
            string qishuS;
            if (qishu < 10)
            {
                qishuS = "00" + qishu;
            }
            else
            {
                qishuS = "0" + qishu;
            }
            string period0 = time + qishuS;//最近开奖的这期
             string resultball5=db.getContent("cqsscResult", "resultBall", "period",period0);//获取五个球开奖结果
            if (resultball5!="")
            {
                string[] arr = resultball5.Split('-');
                _ball =" <a href = \"/ssc/List.Aspx?Cid="+_Uid+"&amp;v=1\" > "+arr[0]+ " </a><a href = \"/ssc/List.Aspx?Cid="+_Uid+"&amp;v=1\" >"+ arr[1] + "</ a><a href = \"/ssc/List.Aspx?Cid=" + _Uid + "&amp;v=1\" >" + arr[2] + "</ a><a href = \"/ssc/List.Aspx?Cid=" + _Uid + "&amp;v=1\" >" + arr[3] + "</ a><a href = \"/ssc/List.Aspx?Cid=" + _Uid + "&amp;v=1\" >" + arr[4] + "</ a>";
            }
        
             

            //设置当前期数用于显示
           
            if (qishu==59)
            {
                double stampNotOneday = GetTimeStamp() % (24 * 60 * 60 * 1000);//获取不足一天的时间戳
                int remainHour = (int)stampNotOneday /( 60 * 60 * 1000);//获取时间戳的小时数；
                if (remainHour==0)
                {
                    _currentQishu = time + "-001";//显示当前期数号：年月日期数号
                }
                else
                {
                    _currentQishu = dt.AddDays(1).ToString("yyyyMMdd") + "-001";//显示当前期数号：年月日期数号
                }
                
            }
            else 
            {
                string qishuS0;
                if (qishu < 9) 
                {
                    qishuS0 = "-00" + (qishu + 1);
                }
                else
                {
                    qishuS0 = "-0" + (qishu + 1);
                }

                _currentQishu = time + qishuS0;//显示当前期数号：年月日期数号

            }
           
            
            //计算截至时间和距离开奖时间
            if (qishu == 59)
            {
                hour = 0;
                min = 27;
                sec = 52;
            }
            else if (qishu < 9)
            {
                int t0 = 27 * 60 + 52;//第一期截至时相对当天0时的秒数
                int t_1 = 20 * 60; //每一期增加的秒数
                int times = qishu;
                int totle = t0 + t_1 * times;
                 hour = totle / 3600;
                 min = totle % 3600 / 60;
                 sec = totle % 60;
               
            }
            else
            {
                int t0 = 7 * 3600 + 27 * 60 + 52;//早上7:30开
                int t_1 = 20 * 60; //每一期增加的秒数
                int times = qishu - 9;
                int totle = t0 + t_1 * times;
                    hour = totle / 3600;
                    min = totle % 3600 / 60;
                   sec = totle % 60;       
            }

            //计算截止剩余时间
            TimeSpan timeSpan = new DateTime(year,month,day,hour,min,sec)-dt;
            double totalsec= timeSpan.TotalSeconds;
            if (totalsec<0)
            {
                _stopTime = "本期已截止！";
               
            }
            else
            {
                int min0 = (int)totalsec / 60;
                int sec0 = (int)totalsec % 60;

                if (min0>0)
                {
                    _stopTime = min0 + "分钟" + sec0 + "秒";

                }
                else
                {
                    _stopTime = sec0 + "秒";
                }
            }

            //计算下期还剩余多久时间
            int remainmin = min + 3;
            int remainsec = sec -51;
            TimeSpan timeSpan1 = new DateTime(year, month, day, hour, remainmin, remainsec) - dt;//提前2分9秒截止
            double totalsec1 = timeSpan1.TotalSeconds;
            if (totalsec1 < 0)
            {
                _openTime= "00分钟00秒";
            }
            else
            {
                int min1 = (int)totalsec1/ 60;
                int sec1 = (int)totalsec1 % 60;

                if (min1 > 0)
                {
                    _openTime = min1 + "分钟" + sec1 + "秒";

                }
                else
                {
                    _openTime = sec1 + "秒";
                }

            }

            _nextPeriod = qishu;//下一期期数
            _datetime = monthDay + " " + hour + ":" + (min + 3);//本期开奖时间
        }
        private long GetTimeStamp()
        {
            TimeSpan ts = dt - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalMilliseconds);
        }
    }
}