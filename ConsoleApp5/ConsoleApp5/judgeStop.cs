using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    class judgeStop
    {
       
        public static void judge() 
        {
            while (true)
            {
                int qishu;
                //int qishuNum;
                int hour = 0;//第一期截至时间
                int min = 27;//第一期截至时间
                int sec = 42;//第一期截至时间
                Database db = new Database();
                DateTime dt = DateTime.Now;
                int year = (int)dt.Year;
                int month = (int)dt.Month;
                int day = (int)dt.Day;
                string time0 = dt.ToString("yyyyMMdd");
                TimeSpan ts = dt - new DateTime(1970, 1, 1, 0, 0, 0, 0);//当前总时间戳
                long time1 = Convert.ToInt64(ts.TotalMilliseconds);
                long todayStamp = time1 % (24 * 60 * 60 * 1000);

                if (todayStamp < 30 * 60 * 1000)
                {
                    qishu = 59;

                }
                else if (todayStamp < (3 * 60 * 60 * 1000 + 11 * 60 * 1000))
                {
                    qishu = (int)(todayStamp - 30 * 60 * 1000) / (20 * 60 * 1000) + 1;//凌晨9期的期数                 
                }
                else if (todayStamp < (7 * 60 * 60 * 1000 + 30 * 60 * 1000))//凌晨不开奖时间段
                {
                    qishu = 9;
                }
                else
                {
                    qishu = (int)(todayStamp - (7 * 60 * 60 * 1000 + 30 * 60 * 1000)) / (20 * 60 * 1000) + 10;//早上7点30开始             
                }


                //判断当前投注期数是否存在，不存在就创建
                string period0 ;//带日期期数
                string qishu0 ;//不带日期，但前面加0的期数
                string date0;//当前投注期开奖日期
                if (qishu == 59)
                {
                    date0 = dt.AddDays(1).ToLongDateString();
                    time0 = dt.AddDays(1).ToString("yyyyMMdd");
                    period0 = time0 + "001";
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
                    date0 = dt.ToLongDateString();
                    period0 = time0 + qishu0;//显示当前期数号：年月日期数号

                }

                if (!db.existPeriod(period0, "cqsscResult"))
                {
                    db.insertContent("cqsscResult", "date", "period","qishu",date0,period0,qishu0);//新建一行
                }

                if (qishu == 59)
                {

                    //data = time0 + " 00:27:42";

                }
                //else if (int.Parse(getLottery.Qishu) < 9)
                else if (qishu < 9)
                {
                    int t0 = 27 * 60 + 52;//第一期截至时相对当天0时的秒数
                    int t_1 = 20 * 60; //每一期增加的秒数
                    int times = qishu;
                    int totle = t0 + t_1 * times;
                    hour = totle / 3600;
                    min = totle % 3600 / 60;
                    sec = totle % 60;
                    // data = time0 + " " + hour + ":" + min + ":" + sec;
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
                    //data = time0 + " " + hour + ":" + min + ":" + sec;

                }

                //开始判断是否截止
                bool flag = true;
                while (flag)
                {
                    TimeSpan ts1 = new DateTime(year, month, day, hour, min, sec) - DateTime.Now ;
                    double timeStamp = ts1.TotalMilliseconds;
                    Console.WriteLine("第" + period0 + "期," + "截止监控中----预计"+(int)(timeStamp/1000) +"秒截止"+ DateTime.Now.ToString());
                    if (timeStamp < 0)
                    {
                        if (!db.isStopBet(period0, "cqsscResult"))
                        {
                            db.updateContent("cqsscResult","stopBet","已截止","period",period0);
                            Console.WriteLine("第" + period0 + "期已截止--------------**************" + DateTime.Now.ToString());
                        }

                        Thread.Sleep(5000);                    
                        flag = false;
                        break;
                    }
                    
                    if (timeStamp<15000)
                    {
                        Thread.Sleep(200);
                    }
                    else
                    {
                        Thread.Sleep(5000);
                    }
                    //timeStamp= (new DateTime(year, month, day, hour, min, sec) - DateTime.Now).TotalMilliseconds;//再次获取当前离截止时间的时间戳
                }
               
            }

            
        }
       
    }
}
