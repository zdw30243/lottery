﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RxHm.aspx.cs" Inherits="bocai_001.ssc.RxHm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!--#include file="/head1.html"-->
        <title>总和大小-重-庆-时-时-彩</title>
        <!--#include file="/head2.html"-->
    </head><body>
<form id="form1" runat="server">
    <%=_message %>
    <div class="main"><div class="main"><div class="header1"><p class="logoli">伟哥①国际</p>
    <p class="login"><%=_login %></p>
    <p class="login1"><%=_login1 %></p>
          </div></form>
       <!--#include file="/nameAndMoney.html"-->
        </ div>
        <%=currentQishu %><br><div class="stoptime"><div>本期截止：<span id="stoptime">10分28秒</span></div><script>
      window.onload=function(){
        var odiv=document.getElementById("stoptime");
        function totwo(e){
          return e<10?"0"+e:""+e;
        }
        function go(){
            var time1 = new Date();

            var timeends="<%=data %>";
            var time2=new Date(timeends);
            var conS=Math.floor((time2.getTime()-time1.getTime())/1000);
            var day=totwo(Math.floor(conS/86400));
            var hour=totwo(Math.floor(conS%86400/3600));
            var min=totwo(Math.floor(conS%86400%3600/60));
            var s=totwo(Math.floor(conS%60));
            var html=""; 
       if(Math.floor(day)>0) html+=day+"天"; 
       if(Math.floor(hour)>0) html+=hour+"时"; 
       if(Math.floor(min)>0) html+=min+"分"; 
       html+=s+"秒"; 
            odiv.innerHTML=html;
            if(conS<0){
              clearInterval(time);
              odiv.innerHTML="开奖中...";
            }          
        }
        go();
        var time=setInterval(go,1000);
      }
        </script> 
    <a class="Refresh" href="RxHm.Aspx?P=<%=_P %>&amp;Cid=<%=_Uid %>&amp;v=1&amp;Nc=55287">刷新</a><br></div><div class="number">上期开奖：<%=_result %></div></div><hr /><strong>所有游戏下注后都不能撤单，请所有玩家下注时看好了再确认下注。</strong><br>
<asp:MultiView ID="MultiView1" runat="server">  
    <asp:View ID="View1" runat="server">
    <div class="gameContainer"> 
    <div class="numtitle"> 
     <div class="sectionLabel">
      <%=_ballName %>
     </div> 
     <div class="odds">
      赔率：
      <span>9.7</span>
     </div> 
    </div> 
    <div class="numContainer"> 
     <div class="selectItemRight"> 
      <div class="selectNumBody"> 
       <div class="selectMark"> 
        <div class="selectNumItem">
         0
        </div> 
       </div> 
       <div class="selectMark"> 
        <div class="selectNumItem">
         1
        </div> 
       </div> 
       <div class="selectMark"> 
        <div class="selectNumItem">
         2
        </div> 
       </div> 
       <div class="selectMark"> 
        <div class="selectNumItem">
         3
        </div> 
       </div> 
       <div class="selectMark"> 
        <div class="selectNumItem">
         4
        </div> 
       </div> 
       <div class="selectMark"> 
        <div class="selectNumItem">
         5
        </div> 
       </div> 
       <div class="selectMark"> 
        <div class="selectNumItem">
         6
        </div> 
       </div> 
       <div class="selectMark"> 
        <div class="selectNumItem">
         7
        </div> 
       </div> 
       <div class="selectMark"> 
        <div class="selectNumItem">
         8
        </div> 
       </div> 
       <div class="selectMark"> 
        <div class="selectNumItem">
         9
        </div> 
       </div> 
      </div> 
      <div class="btnContainer"> 
       <div class="btnItem">
        全
       </div> 
       <div class="btnItem">
        大
       </div> 
       <div class="btnItem">
        小
       </div> 
       <div class="btnItem">
        单
       </div> 
       <div class="btnItem">
        双
       </div> 
       <div class="btnItem">
        清
       </div> 
      </div> 
     </div> 
    </div> 
   </div>
   <form method="post" name="form1" action="RxHm.Aspx?Cid=<%=_Uid %>&amp;P=1&amp;v=1&amp;Ac=Bet">
    <div class="write">
     <div class="wnumber">
      <div class="writename">
       下注号码
      </div>
      <div class="writeselect">
       <input class="numberselect" name="P1" type="text" format="*M" emptyok="true" size="18" />
       <a class="clear">清</a>
      </div>
     </div>
     <div class="wmoney">
      <div class="writename">
       下注金额(单注):
      </div>
      <div class="writeselect">
       <input class="money" type="text" format="*N" name="Multiple" />
       <a class="clear">清</a>
      </div>
     </div>
     <div class="quickmoney">
      <div class="quickItem" name="10">
       10元
      </div>
      <div class="quickItem" name="50">
       50元
      </div>
      <div class="quickItem" name="100">
       100元
      </div>
      <div class="quickItem" name="200">
       200元
      </div>
      <div class="quickItem" name="500">
       500元
      </div>
      <div class="quickItem" name="1000">
       1000元
      </div>
      <div class="quickItem" name="5000">
       5000元
      </div>
      <div class="quickItem" name="10000">
       10000元
      </div>
      <div class="quickItem" name="50000">
       50000元
      </div>
     </div>
    </div>
    <div class="sub">
     <input class="submit" type="submit" value="立即下注" />
    </div>
   </form><strong>温馨提示：最多下注7个号码，超过7个号码一律作为刷量/无风险投注，没收本金及盈利 下注前请注意！</strong><br/>单个号码最高每期可投注10000金币<br/><hr/>玩法提示:<br/>投注的号码与开出的号码万位(第1球)一致即中奖<br/>选号示例:<br/>选号:万位 1<br/>开号:1(万位) 1 3 4 0<br/>中奖:投注额*9.7<br/>
        </asp:View>
    <asp:View ID="View2" runat="server">
       <form method="post" action="RxHm.aspx?Cid=<%=_Uid %>&amp;v=2&amp;Ac=Bet">
        <%=_ballName %>:<%=_a %><br />下注金额:<%=_b %>币,需要<%=_totalMoney %>币<br />
        预计奖金:<%=winMoney%><br /><%=_d %><br /></form>
    </asp:View>
    <asp:View ID="View3" runat="server">
       <%=_stopOrSuccess %>
       </asp:View>
        </asp:MultiView>
        <hr><span class="ddd-qs"><a href="Default.Aspx?Cid=<%=_Uid %>&amp;v=1">[返回重庆时时彩]</a></span><br>
        <!--#include file="../bottom.html"-->
         时间:<% Response.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));%> <p class="link"></p>
        <!--#include file="../bottom1.html"-->
</body></html>
