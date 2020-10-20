<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="bocai_001.ssc.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<!--#include file="/head1.html"-->
    <title>老伟的网站</title>
    <!--#include file="/head2.html"-->
</head>
<body>
     <%=_message %>
    <div class="main"><div class="main"><div class="header1"><p class="logoli">重庆时时彩</p>
<p class="login"><%=_login %></p>
<p class="login1"><%=_login1 %></p>
</div>[<a href="/Room/Default.Aspx?Cid=<%=_Uid %>&amp;v=1">聊室</a>]<a href="/Room/Add.Aspx?Sid=95047&amp;Lx=0&amp;Cid=<%=_Uid %>&amp;v=1">江加成</a>(ID:95047)说:88888[13:11]<br/><strong><%=_tongGao %></strong><br/>
       <span class="biao2"></span>
        <!--#include file="/nameAndMoney.html"-->
        <div class="sscnr2"><span class="sscnr3">快捷</span><a href="/View/View.Aspx?id=25&amp;Cid=<%=_Uid %>&amp;v=1">充值</a><a href="/View/View.Aspx?id=54&amp;Cid=<%=_Uid %>&amp;v=1">域名</a><a href="/User/Default.Aspx?Ac=Edit&amp;Cid=<%=_Uid %>&amp;v=1">改密</a><a href="/View/View.Aspx?id=16&amp;Cid=<%=_Uid %>&amp;v=1">活动</a><br/>
            <span class="sscnr6">互动</span><a href="/User/Friends.Aspx?Cid=<%=_Uid %>&amp;v=1">好友</a><a href="/Room/Default.Aspx?Cid=<%=_Uid %>&amp;v=1">聊室</a><a href="/Book/Default.Aspx?Cid=<%=_Uid %>&amp;v=1">留言</a><a href="/User/Service.Aspx?Cid=<%=_Uid %>&amp;v=1">客服</a></div>
        <div class="linebz"><a href="/View/View.Aspx?id=1&amp;Cid=<%=_Uid %>&amp;v=1"><span class="biao">公告</span>重庆时时彩投注规则说明!</a></div><div class="title2"><strong> 投注信息  </strong></div><div class="sscnr7"><a href="List.Aspx?Cid=<%=_Uid %>&amp;v=1">开奖记录</a>
            <a href="/ssc/MyBet.Aspx?Lx=1&amp;Cid=<%=_Uid %>&amp;v=1">我的下注</a><a href="BetList.Aspx?Ac=History&amp;Cid=<%=_Uid %>&amp;v=1">网友历史</a><a href="/Ssc/Default.Aspx?Nc=637368093422332579&amp;Cid=<%=_Uid %>&amp;v=1">刷新开奖</a></div>
        <span class="sscnr8">本期第<%=_currentQishu %>期:</span><br/><%=_currentBet %><br/>开奖时间:<%=_datetime %><br/>封盘时间:<%=_stopTime %><br/>离开奖还有:<% =_openTime %><br/>重庆时时彩第<strong><%=qishu %></strong>期开奖号码:<div class="qiuqiu"><%=_ball %></div>
        
        <div class="title2"><strong> 球数玩法  </strong></div><span class="ssc-qs">离中奖更近一步</span><span class="ssc-qs2"><a href="./KxHm.Aspx?Cid=<%=_Uid %>&amp;v=1">五星快投</a></span><br/>

        <span class="ssc-qs3">一球(万位)</span><span class="ssc-qs4"><a href="/ssc/RxHm.Aspx?P=1&amp;Cid=<%=_Uid %>&amp;v=1">号码</a> <a href="/ssc/RxDs.Aspx?P=1&amp;Cid=<%=_Uid %>&amp;v=1">单双</a> <a href="/ssc/RxDx.Aspx?P=1&amp;Cid=<%=_Uid %>&amp;v=1">大小</a></span><br/>
        <span class="ssc-qs3">二球(千位)</span><span class="ssc-qs4"><a href="/ssc/RxHm.Aspx?P=2&amp;Cid=<%=_Uid %>&amp;v=1">号码</a> <a href="/ssc/RxDs.Aspx?P=2&amp;Cid=<%=_Uid %>&amp;v=1">单双</a> <a href="/ssc/RxDx.Aspx?P=2&amp;Cid=<%=_Uid %>&amp;v=1">大小</a></span><br/>
        <span class="ssc-qs3">三球(百位)</span><span class="ssc-qs4"><a href="/ssc/RxHm.Aspx?P=3&amp;Cid=<%=_Uid %>&amp;v=1">号码</a> <a href="/ssc/RxDs.Aspx?P=3&amp;Cid=<%=_Uid %>&amp;v=1">单双</a> <a href="/ssc/RxDx.Aspx?P=3&amp;Cid=<%=_Uid %>&amp;v=1">大小</a></span><br/>
        <span class="ssc-qs3">四球(十位)</span><span class="ssc-qs4"><a href="/ssc/RxHm.Aspx?P=4&amp;Cid=<%=_Uid %>&amp;v=1">号码</a> <a href="/ssc/RxDs.Aspx?P=4&amp;Cid=<%=_Uid %>&amp;v=1">单双</a> <a href="/ssc/RxDx.Aspx?P=4&amp;Cid=<%=_Uid %>&amp;v=1">大小</a></span><br/>
        <span class="ssc-qs3">五球(个位)</span><span class="ssc-qs4"><a href="/ssc/RxHm.Aspx?P=5&amp;Cid=<%=_Uid %>&amp;v=1">号码</a> <a href="/ssc/RxDs.Aspx?P=5&amp;Cid=<%=_Uid %>&amp;v=1">单双</a> <a href="/ssc/RxDx.Aspx?P=5&amp;Cid=<%=_Uid %>&amp;v=1">大小</a></span>
        <div class="title2"><strong> 总和玩法 </strong></div>
              <div class="sscnr2">
                  <a href="/ssc/ZhDs.Aspx?P=5&amp;Cid=<%=_Uid %>&amp;v=1">总和单双</a>
                  <a href="/ssc/ZhDx.Aspx?P=5&amp;Cid=<%=_Uid %>&amp;v=1">总和大小</a> 
                  <a href="/ssc/LhDs.Aspx?P=5&amp;Cid=<%=_Uid %>&amp;v=1">龙虎和</a>
              </div> 
        <div class="title2"><strong>特殊玩法</strong></div>
        <div class="sscnr2">
                   <a href="/ssc/R3Tz.Aspx?P=0&amp;Cid=<%=_Uid %>&amp;v=1">前三投注</a>
                   <a href="/ssc/R3Tz.Aspx?P=1&amp;Cid=<%=_Uid %>&amp;v=1">中三投注</a>
                   <a href="/ssc/R3Tz.Aspx?P=2&amp;Cid=<%=_Uid %>&amp;v=1">后三投注</a>
        </div> 
        <div class="title2"><strong> 连码投注</strong></div>
        <div class="sscnr2">
                   <a href="/ssc/LmH2.Aspx?Cid=<%=_Uid %>&amp;v=1">后二投注</a>
                   <a href="LmH3.Aspx?Cid=<%=_Uid %>&amp;v=1">后三投注</a>
                   <a href="LmH4.Aspx?Cid=<%=_Uid %>&amp;v=1">后四投注</a>
        </div> 
        <div class="title2"><strong>梭哈玩法 </strong></div>
        <div class="sscnr2">
            <a href="/ssc/jack.Aspx?Cid=<%=_Uid %>&amp;v=1">梭哈玩法</a>
            <a href="/ssc/cow.Aspx?Cid=<%=_Uid %>&amp;v=1">牛牛玩法</a>
        </div>
            <a href="/ssc/Default.Aspx?Nc=637368093422332579&amp;Cid=<%=_Uid %>&amp;v=1"><div class="gotop"></div></a>
    <!--#include file="../bottom.html"-->
          时间:<% Response.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));%> <p class="link"></p>
        <!--#include file="../bottom1.html"-->
</body>
</html>
