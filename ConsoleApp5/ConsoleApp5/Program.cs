using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace ConsoleApp5
{
    class Program
    {

        //自动兑奖
        private static void kaijiang() 
        {
            int i = 0;
            while (true)
            {
                Database db = new Database();
                //string sql = "select * from bet where betStatus='待开奖'";
                //SqlDataReader sdr = db.getDataReader(sql);
                //    //db.close();
                //    DataTable dt = sdr.GetSchemaTable();
                DataTable dataTable = db.getDataTable();
                if (dataTable.Rows.Count>0)
                {
                    foreach (DataRow row in dataTable.Rows)
                    {
                        string betType = row["betType"].ToString();
                        string typeName = "";//投注类型，中文表示形式
                        string period = row["period"].ToString();
                        //string table = betType + "Result";
                        string result = db.getResult(betType, period);


                       

                        if (result != "")
                        {
                            //string tema;//特码
                            float peiLv;//赔率
                            string userName;
                            string simUid;
                            string betMoney;
                            string position;
                            string winMoney;//当期投注赢的金额
                            float singleMoney;//单注金额
                            float money;//
                            float winMoneys;//兑奖余额加上当期投注赢的金额
                            string positionType;//投注类型
                            string positionCheck;//兑奖--类型+位置
                            positionType = row["positionType"].ToString();
                            string result0;//截取开奖结果用于转换显示格式
                            string resultOpen = "";//用于生成消息显示的开奖结果"(0,0,0,0,0)"
                           
                            position = row["position"].ToString();
                            if (betType.Contains("cqssc"))
                            {
                                typeName = "重庆时时彩";
                                result0 = result.Substring(0, 9);
                                resultOpen = "(" + result0.Replace('-', ',') + ")";
                            }
                            else if (betType.Contains("bjsc"))
                            {
                                typeName = "北京赛车";
                            }
                            else if(betType.Contains("Six"))
                            {
                                typeName = "香港六合彩";
                                result0 = result.Substring(0, 21);
                                resultOpen = "(" + result0.Replace('-', ',') + ")";
                                //tema= result.Substring(19, 2);
                            }

                            string orderNum = row["orderNum"].ToString();//订单编号
                            peiLv = float.Parse(db.getContent("peiLvTable", "peiLv", "betType", "Six", "positionType", positionType));
                            if (positionType == "任选号码")
                            {
                                bool bo = true;
                                string[] positionNumbers = position.Split(',');
                                foreach (string  item in positionNumbers)
                                {
                                    positionCheck = positionType + item;
                                    if (result.Contains(positionCheck))
                                    {
                                         simUid = row["Uid"].ToString();
                                         betMoney = row["money"].ToString();//获取投注金额
                                         userName = row["userName"].ToString();
                                        winMoney = row["winMoney"].ToString();
                                         money = float.Parse(db.getContent("userInfo", "money", "userName", userName));//获取用户当前余额
                                         winMoneys = money + float.Parse(winMoney);
                                        db.updateContent("bet", "betStatus", "中得" + winMoney + "币", "orderNum", orderNum);//兑奖后，将投注标记是否中奖
                                        db.updateContent("userInfo", "money", winMoneys.ToString(), "userName", userName);//奖金加入余额中
                                        string news = "恭喜你,你投注第" + period + "期" + typeName + "开奖结果为\t\n" + resultOpen + "\t\n您投注的" + positionType + ":" + position + "中奖了!!！\t\n" + winMoney + "金币已加入您的账户，请注意查收!!";
                                        db.insertContent("message", "idSend", "idReceive", "netName", "news", "status", "source", "time", "2", simUid, userName, news, "未读", "中奖通知", DateTime.Now.ToString("yyyy-MM-dd h:mm:ss"));//系统消息默认id=2；
                                        Console.WriteLine(news);
                                        bo = false;
                                        break;
                                    }
                                }
                                if (bo)
                                {
                                    db.updateContent("bet", "betStatus", "未中奖", "orderNum", orderNum);//兑奖后，将投注标记是否中奖
                                }

                            }
                            else if (positionType == "三中三")
                            {

                                string[] positionNumbers = position.Split(','); ;
                                string[] danzhu = new string[3];//单注
                                List<string[]> list = new List<string[]>();//单注集合
                                int count = 0;
                                for (int z = 0; z < Math.Pow(2, positionNumbers.Length); z++)
                                {
                                    int a = 0;

                                    for (int j = 0; j < positionNumbers.Length; j++)
                                    {
                                        if ((z >> j & 1) == 1)
                                        {
                                            a++;
                                            danzhu.Append(positionNumbers[j]);
                                        }
                                    };
                                    if (a == 3)
                                    {
                                        count++;
                                        list.Add(danzhu);
                                    }
                                }
                                foreach (string[] item in list)//遍历每一注
                                {
                                    bool bo = true;
                                    foreach (string num in item)//遍历每一注的数再和开奖结果比较
                                    {
                                        if (!result.Contains(num))
                                        {
                                            bo = false;
                                        }
                                    }
                                    if (bo)
                                    {
                                        count++;
                                    }
                                }
                                if (count>0)
                                {
                                    float winMoney3V3;
                                     singleMoney = float.Parse(row["singleMoney"].ToString());
                                    winMoney3V3 = singleMoney * count * peiLv;
                                    simUid = row["Uid"].ToString();
                                    betMoney = row["money"].ToString();//获取投注金额
                                    userName = row["userName"].ToString();
                                    money = float.Parse(db.getContent("userInfo", "money", "userName", userName));//获取用户当前余额
                                    winMoneys = money+winMoney3V3;
                                    db.updateContent("bet", "betStatus", "中得" + winMoney3V3 + "币", "orderNum", orderNum);//兑奖后，将投注标记是否中奖
                                    db.updateContent("userInfo", "money", winMoneys.ToString(), "userName", userName);//奖金加入余额中
                                    string news = "恭喜你,你投注第" + period + "期" + typeName + "开奖结果为\t\n" + resultOpen + "\t\n您投注的" + positionType + ":" + position + "中奖了!!！\t\n" + winMoney3V3 + "金币已加入您的账户，请注意查收!!";
                                    db.insertContent("message", "idSend", "idReceive", "netName", "news", "status", "source", "time", "2", simUid, userName, news, "未读", "中奖通知", DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"));//系统消息默认id=2；
                                    Console.WriteLine(news);
                                }
                                else
                                {
                                    db.updateContent("bet", "betStatus", "未中奖", "orderNum", orderNum);//兑奖后，将投注标记是否中奖
                                }

                            }
                            else
                            {
                                positionCheck = positionType + position;
                                if (result.Contains(positionCheck))
                                {
                                     simUid = row["Uid"].ToString();
                                     betMoney = row["money"].ToString();//获取投注金额
                                     userName = row["userName"].ToString();
                                    winMoney = row["winMoney"].ToString();
                                     money = float.Parse(db.getContent("userInfo", "money", "userName", userName));//获取用户当前余额
                                     winMoneys = money + float.Parse(winMoney);
                                    db.updateContent("bet", "betStatus", "中得" + winMoney + "币", "orderNum", orderNum);//兑奖后，将投注标记是否中奖
                                    db.updateContent("userInfo", "money", winMoneys.ToString(), "userName", userName);//奖金加入余额中
                                    string news = "恭喜你,你投注第" + period + "期" + typeName + "开奖结果为\t\n" + resultOpen + "\t\n您投注的" + positionType + ":" + position + "中奖了!!！\t\n" + winMoney + "金币已加入您的账户，请注意查收!!";
                                    db.insertContent("message", "idSend", "idReceive", "netName", "news", "status", "source", "time", "2", simUid, userName, news, "未读", "中奖通知", DateTime.Now.ToString("yyyy-MM-dd H:mm:ss"));//系统消息默认id=2；
                                    Console.WriteLine(news);
                                }
                                else
                                {
                                    db.updateContent("bet", "betStatus", "未中奖", "orderNum", orderNum);//兑奖后，将投注标记是否中奖
                                }
                            }
                           
                        }
                       

                    }
                }
                i++;
                Console.WriteLine("自动兑奖"+i+"次-----"+DateTime.Now.ToLongDateString());
                Thread.Sleep(2000);
            }
        }

        //获取站外开奖结果
        private static void getOutResult()
        {
            string  stopPeriod = "0";
            int  qishu =0;
            while (true)
            {
                DateTime dt = DateTime.Now;
                Database db1 = new Database();
                GetLottery getLottery = new GetLottery();
                stopPeriod = db1.getContent("cqsscResult", "period", "stopBet", "已截止");//获取当前已经截止的期号
                if (stopPeriod != "")
                {
                    bool flag = true; 
                    while (flag)
                    {
                        Console.WriteLine("正在获取第"+stopPeriod+"期开奖结果！请老伟耐心等待！");
                        string periodNunber = getLottery.getQishu();//获取站外开奖结果最近一期的期号（带日期）
                        if (long.Parse(stopPeriod)<long.Parse(periodNunber))
                        {
                            db1.updateContent("cqsscResult","stopBet","已开奖","period",stopPeriod);
                            flag = false;
                            break;
                        }
                        else if (periodNunber == stopPeriod)//如果站外获取的期数，是已标记截止的，那就相当于开奖结果出来了
                        {

                            string resultBall = getLottery.getResult();
                            if (resultBall != "")
                            {
                                int totalNum;//5个球总和值
                                string resultForList="("+resultBall+",";
                                string dxds = "";//大小单双
                                string databaseResult = resultBall + " ";
                                string[] arr = resultBall.Split('-');

                                for (int i = 0; i < arr.Length; i++)
                                {
                                    databaseResult += "第" + (i + 1) + "球号码" + arr[i];
                                    if (int.Parse(arr[i]) % 2 == 1)
                                    {
                                        databaseResult += "第" + (i + 1) + "球单双单";
                                    }
                                    else
                                    {
                                        databaseResult += "第" + (i + 1) + "球单双双";
                                    }

                                    if (int.Parse(arr[i]) < 5)
                                    {
                                        databaseResult +="第"+ (i + 1) + "球大小小";
                                    }
                                    else
                                    {
                                        databaseResult += "第" + (i + 1) + "球大小大";
                                    }

                                }

                                //计算总和
                                int zonghe = 0;
                                foreach (string item in arr)
                                {
                                    zonghe += int.Parse(item);
                                }

                                resultForList += "总和" + zonghe + ",";

                                if (zonghe < 23)
                                {
                                    databaseResult += "总和大小小";
                                    resultForList += "小";
                                }
                                else
                                {
                                    databaseResult += "总和大小大";
                                    resultForList += "大";
                                }

                                if (zonghe % 2 == 1)
                                {
                                    databaseResult += "总和单双单";
                                    resultForList += "单)";
                                }
                                else
                                {
                                    databaseResult += "总和单双双";
                                    resultForList += "双)";
                                }

                                databaseResult += "前三" + arr[0] + arr[1] + arr[2] + " 中三" + arr[1] + arr[2] + arr[3] + " 后三" + arr[2] + arr[3] + arr[4];

                                if (int.Parse(arr[0]) == int.Parse(arr[1]) && int.Parse(arr[0]) == int.Parse(arr[2]))
                                {
                                    databaseResult += " 前三豹子";
                                }
                                if (int.Parse(arr[1]) == int.Parse(arr[2]) && int.Parse(arr[1]) == int.Parse(arr[3]))
                                {
                                    databaseResult += " 中三豹子";
                                }
                                if (int.Parse(arr[2]) == int.Parse(arr[3]) && int.Parse(arr[2]) == int.Parse(arr[4]))
                                {
                                    databaseResult += " 后三豹子";
                                }

                                db1.updateContent("cqsscResult", "resultBall", "result", "resultForList","stopBet", resultBall, databaseResult,resultForList,"已开奖", "period", periodNunber);//将开奖结果和开奖状态写入数据库
                                Console.WriteLine("获取开奖结果成功！-----------------------------");
                                Console.WriteLine("第" + periodNunber + "期开奖结果为:" + databaseResult);
                                flag = false;
                            }

                        }
                        else
                        {
                            
                            Thread.Sleep(5000);
                        }
                    }
                }
                else
                {          
                    int hour = 0;//第一期截至时间
                    int min = 27;//第一期截至时间
                    int sec = 42;//第一期截至时间 
                    string time1 = dt.ToString("yyyyMMdd");
                    TimeSpan ts = dt - new DateTime(1970, 1, 1, 0, 0, 0, 0);//当前总时间戳
                    long time2 = Convert.ToInt64(ts.TotalMilliseconds);
                    long todayStamp = time2 % (24 * 60 * 60 * 1000);

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

                    string currentPeriod;
                    //判断当前投注期数是否存在，不存在就创建
                    if (qishu == 59)
                    {
                        currentPeriod = dt.AddDays(1).ToString("yyyyMMdd") + 001;
                    }

                    currentPeriod = time1 + (qishu + 1);
                    if (!db1.existPeriod(currentPeriod, "cqsscResult"))
                    {
                        db1.insertContent("cqsscResult", "period", currentPeriod);
                    }

                    if (qishu < 9)
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
                    Console.WriteLine("获取第" + currentPeriod + "期开奖结果监控中");
                    Thread.Sleep(5000);
                }
               
            }
        }

        static void Main(string[] args)
        {

            Thread thread1 = new Thread(new ThreadStart(getOutResult));
            //Thread thread2 = new Thread(new ThreadStart(kaijiang));
            Thread thread3 = new Thread(new ThreadStart(judgeStop.judge));
            thread1.Start();
           // thread2.Start();
            thread3.Start();
            
            }
        
            //    //DateTime dt = DateTime.Now;
            //    //DateTime dt1 = new DateTime(2000,1,1);
            //    //System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            //    //Console.WriteLine(startTime);
            //    int i = 6;
            //    Console.WriteLine(6*1.66);
            //    string dt = GetTimeStamp();
            // Console.WriteLine(dt);
          
            //Console.ReadKey();

       
   //     private static int[] reverse(int[] arr)
   //     {
   //         int[] result=new int[arr.Length];
   //         int j = arr.Length-1;
   //         for (int i = 0; i < arr.Length; i++)
   //         {
   //             result[j] = arr[i];
   //             j--;
   //         }
   //         return result;
   //     }
   //     static void Main(string[] args) { 
   //     int[] array = { 15, 2, 3, 4, 6,88, 1, 7, 8, 9, 13 };
   //     for (int i = 0; i<array.Length; i++)
			//{
   //             for (int j = i+1; j < array.Length; j++)
   //             {
   //                 if (array[i]>array[j])
   //                 {
   //                     array[i] = array[i] + array[j];
   //                     array[j] = array[i] - array[j];
   //                     array[i] = array[i] - array[j];
   //                 }
   //             }

			//}
   //         int[] array1=reverse(array);
        

   //         foreach (int item in array1)
   //         {
   //             Console.WriteLine(item);
   //         }
   //         Console.ReadKey();
   //     }




        //static string status;
        //public static void method()
        //{
        //    for (int i = 0; i < 5000; i++)
        //    {
        //        status = Thread.CurrentThread.ThreadState.ToString();
        //        if (i % 5 == 0)
        //        {
        //            Console.WriteLine("线程run！" + status);
        //        }
        //    }
        //}
        //static void Main(string[] args)
        //{
        //    ThreadStart ts = new ThreadStart(method);
        //    Thread thread = new Thread(ts);
        //    thread.Start();

        //    while (thread.IsAlive)
        //    {
        //        Console.WriteLine("线程运行中");
        //        Thread.Sleep(1);
        //    }

        //    Console.ReadLine();
        //}
        //public void setXml(string path,string node,string key,string value) 
        //{

        //    FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
        //    StreamWriter sw = new StreamWriter(fs);
        //    string too = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
        //    sw.WriteLine(too);
        //    sw.Close();
        //    fs.Close();
        //    XmlDocument xd = new XmlDocument();
        //   // xd.Load(path);
        //    XmlElement xn = xd.CreateElement("StationSetting");
        //    xd.AppendChild(xn);
        //    XmlElement xe;
        //    if (!xn.HasChildNodes)
        //    {
        //        //XmlNode nodes= xn.SelectSingleNode(node);
        //        xe = xd.CreateElement(node);
        //        xn.AppendChild(xe);

        //    }
        //    else
        //    {
        //        XmlNode xmlnode = xn.SelectSingleNode(node);
        //        xe = (XmlElement)xmlnode;

        //    }
        //    xe.SetAttribute(key,value);
        //    xd.Save(path);
        //}
        //static void Main(string[] args)
        //{
        // int[] a = { 8, 15, 6, 33, 2, 22, 16, 37, 28 };
        // int s = 0;
        // for (int i = 0; i < a.Length; i++)
        // {
        //     for (int j= i+1; j <a.Length; j++)
        //     {
        //         if (a[i]>a[j])
        //         {
        //             s = a[i];
        //             a[i] = a[j];
        //             a[j] = s;
        //         }

        //     }
        // }

        // int a0 = 0;
        // int total = 0;
        // int m =5;
        // for (int i = 1; i <m+1; i++)
        // {
        //     if (i%2==0)
        //     {
        //         total = total - i;
        //     }
        //     else
        //     {
        //         total = total + i;
        //     }

        // }
        // Console.WriteLine(total);

        //foreach (int ss in a)

        // {
        //     Console.Write("{0} ", ss);
        // }



        //Random ra = new Random();
        //int a, b, c, d, e;
        //int a1, b1, c1, d1, e1;
        //int tt = 0;
        //int tt1=0;
        //bool flag0 = false; ;
        //for (int i = 0; i < 100; i++)
        //{
        //    int da1 = ra.Next(1, 101);
        //    int xiao1 = ra.Next(1, 101);
        //    int dan1 = ra.Next(1, 101);
        //    int shuang1 = ra.Next(1, 101);
        //    int da2 = ra.Next(1, 101);
        //    int xiao2 = ra.Next(1, 101);
        //    int dan2 = ra.Next(1, 101);
        //    int shuang2 = ra.Next(1, 101);
        //    int da3 = ra.Next(1, 101);
        //    int xiao3 = ra.Next(1, 101);
        //    int dan3 = ra.Next(1, 101);
        //    int shuang3 = ra.Next(1, 101);
        //    int da4 = ra.Next(1, 101);
        //    int xiao4 = ra.Next(1, 101);
        //    int dan4 = ra.Next(1, 101);
        //    int shuang4 = ra.Next(1, 101);
        //    int da5 = ra.Next(1, 101);
        //    int xiao5 = ra.Next(1, 101);
        //    int dan5 = ra.Next(1, 101);
        //    int shuang5 = ra.Next(1, 101);
        //    int da= ra.Next(1, 101);
        //    int xiao = ra.Next(1, 101);
        //    int f = ra.Next(1,101);
        //   if (f < 20)
        //{
        //    bool flag = true;
        //    while (flag)
        //    {
        //        a = ra.Next(0, 10);
        //        b = ra.Next(0, 10);
        //        c = ra.Next(0, 10);
        //        d = ra.Next(0, 10);
        //        e = ra.Next(0, 10);

        //        if (da>xiao? a + b + c + d + e > 22: a + b + c + d + e <23)
        //        {
        //            Console.WriteLine(a + " " + b + " " + c + " " + d + " " + e + "  总和大DADADA");

        //                tt++;
        //                flag0 = true;
        //                //if()
        //        }
        //    }

        //}
        //else 
        //{
        //    bool fl = true;
        //    while (fl)
        //    {
        //        a1 = ra.Next(0, 10);
        //        b1 = ra.Next(0, 10);
        //        c1 = ra.Next(0, 10);
        //        d1 = ra.Next(0, 10);
        //        e1 = ra.Next(0, 10);
        //        if (da > xiao ? a1 + b1 + c1 + d1 + e1 > 22 : a1 + b1 + c1 + d1 + e1 < 23)
        //        {
        //            Console.WriteLine(a1 + " " + b1 + " " + c1 + " " + d1 + " " + e1 + "  总和小");
        //            fl = false;
        //                tt1++;
        //        }
        //    }
        //    }
        //}
        //    Thread.Sleep(50);
        //}
        //Console.WriteLine("概率10%总大：{0}   总小：{1}", tt, tt1);

        //XmlDocument xd = new XmlDocument();
        //   string path = "StationSettings.xml";
        //   xd.Load(path);
        //XmlNode xn = xd.SelectSingleNode("StationSettings");
        //XmlNode xn1 = xn.SelectSingleNode("Stations");
        //XmlNode xn2 = xn1.SelectSingleNode("Station");
        //string ss = xn2.Attributes["Version"].Value;
        //string path = "sett.xml";
        //string a = "12";
        //string b = "66";
        //string c = "88";
        //string d = "55";


        //Program con = new Program();
        // con.setXml(path, a, b, c);
        // con.setXml(path, b, b, c);
        // con.setXml(path, c, b, c);
        // con.setXml(path, a, b, c);
        // con.setXml(path, a, b, c);
        //  Console.WriteLine(ss);
        //Console.WriteLine("111");
        //    Console.ReadKey();
            
        //}
    }
}
