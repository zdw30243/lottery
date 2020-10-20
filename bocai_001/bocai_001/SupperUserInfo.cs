using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;


namespace bocai_001
{
    public class SupperUserInfo
    {
       // public string simpid;//没加密id
        public string Uid;//加密id；
        public string userName;//用户名
        public string netName;//网名
        public string Pwd;//密码
        public bool isLogin;

        public SupperUserInfo() 
        {
            Uid = "";
            userName = "";
            netName = "";
            Pwd = "";
            isLogin = false;
            try
            {
                getCookies();
                tryLogin();
            }
            catch
            {

               
            }
            
          
            
        }

        public string getsimpleUid()//获取没有加密Uid
        {
            string id;
            Database db = new Database();
            id=db.getContent("userInfo","Uid", "userName",userName);
            return id;
        }

        public void outLogin()
        {
            isLogin = false;
        }
        private string getUid()//获取加密Uid，存cookie或URL使用加密
        {
            string getuid = "";
            Database db = new Database();
            string getuid0 = db.getContent("userInfo","Uid", "userName",userName);
            getuid=db.md5(getuid0);
            return getuid;
        }
        public string getnetName()
        {
            string getnetName = "";
            Database db = new Database();
            getnetName = db.getContent("userInfo","netName", "userName",userName);
            return getnetName;
        }

        public Boolean getisLogin() 
        {
            return isLogin;
        }
        public Boolean tryLogin() 
        {
            isLogin = false;
            Database db = new Database();
            if ( userName !="" && db.getContent("userInfo","Pwd", "userName", userName)==Pwd && db.getContent("userInfo", "Pwd", "userName", userName)!="")
            {
                Uid =getUid();
                netName = getnetName();
                isLogin = true;
                //simpid = getsimpleUid();
            }
            return isLogin;

        }

        public void setCookies()
        {
            HttpCookie cookie = new HttpCookie("userLogin");
            cookie.Values.Add("cUid",Uid);
            cookie.Values.Add("cuserName",userName);
            cookie.Values.Add("cnetName",netName);
            cookie.Values.Add("cPwd",Pwd);
            //cookie.Values.Add("csimpid", simpid);
            cookie.Expires = DateTime.Now.AddDays(30);
            HttpContext.Current.Response.Cookies.Add(cookie);

        }

        public void getCookies()
        {
            if (HttpContext.Current.Request.Cookies["userLogin"]!=null) 
            {
                HttpCookie cook = HttpContext.Current.Request.Cookies["userLogin"]; 
                    Uid = cook.Values["cUid"];
                    netName = cook.Values["cnetName"];
                    Pwd = cook.Values["cPwd"];
                    userName = cook.Values["cuserName"];
                    //simpid= cook.Values["csimpid"];
            }
        }
    }
}