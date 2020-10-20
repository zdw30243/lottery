﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace bocai_001
{
    public class GetLottery
    {
        public string Qishu
        {
            get { return getQishu(); }
        }
        public string Result
        {
            get { return getResult(); }
        }

        public string ball1
        {
            get { return getBall(1); }
        }
        public string ball2
        {
            get { return getBall(2); }
        }
        public string ball3
        {
            get { return getBall(3); }
        }
        public string ball4
        {
            get { return getBall(4); }
        }
        public string ball5
        {
            get { return getBall(5); }
        }

        //获取指定球的号码
        private string getBall(int num)
        {
            string ball = "";
            string result = getResult();
            string[] strarray = result.Split('-');
            ball = strarray[num-1];
            return ball;
        }

        //获取最近一期的期数
        private string getQishu()
        {

            System.Net.HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("https://bzbz7.bz/ssc/List.Aspx?Cid=null&v=1");    //创建一个请求示例
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();  //获取响应，即发送请求
            Stream responseStream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
            string html = streamReader.ReadToEnd();
            streamReader.Close();//及时关闭
            //responseStream.Close();//及时关闭
            response.Close();
            string st0 = html.Replace(" ", "");
            string st1 = st0.Replace("总和", "总和 ");
            MatchCollection match = Regex.Matches(st1, "(?<=日)\\S+?(?=总和)");//截取最近一期开奖记录匹配字符串集合，其实就一个
            string stOK = "";
            string qishu = "";
            if (match.Count > 0)
            {
                stOK = match[0].Value;//获取包含最近开奖期数和结果的网站源码字符串

                int i = stOK.IndexOf('第');
                qishu = stOK.Substring(i + 1, 3);//获取到最新期数
            }
            return qishu;
        }

        //获取最近一期开奖结果
        private string getResult()
        {

            System.Net.HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("https://bzbz7.bz/ssc/List.Aspx?Cid=null&v=1");    //创建一个请求示例
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();  //获取响应，即发送请求
            Stream responseStream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
            string html = streamReader.ReadToEnd();
            streamReader.Close();
            string st0 = html.Replace(" ", "");
            string st1 = st0.Replace("总和", "总和 ");
            MatchCollection match = Regex.Matches(st1, "(?<=日)\\S+?(?=总和)");//截取最近一期开奖记录匹配字符串集合，其实就一个
            string stOK = "";
            string stResult = "";
            if (match.Count > 0)
            {
                stOK = match[0].Value;//获取包含最近开奖期数和结果的网站源码字符串
                MatchCollection matchResult = Regex.Matches(stOK, "\\d-\\d-\\d-\\d-\\d");

                if (matchResult.Count > 0)
                {
                    stResult = matchResult[0].Value;

                }

            }
            return stResult;

        }
    }
}