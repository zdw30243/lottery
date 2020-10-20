<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Xin_Add.aspx.cs" Inherits="bocai_001.ssc.Xin_Add" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!--#include file="/head1.html"-->
        <title>对话</title>
        <!--#include file="/head2.html"-->
    </head><body>
        <form name="form1" method="post" action="/User/Xin_Save.Aspx?Cid=<%=_Uid %>&amp;Sid=<%=_idReceive %>&amp;v=1">
            <div>
            <textarea  name="Text" cols="20"  rows="3"></textarea>
                <input type="submit"  class="bottom" value="发送" /><br />
                </div>
        </form>
         <%=_chatContent %>
        <hr/>
        <a href="/User/Default.Aspx?Cid=<%=_Uid %>&amp;v=1">返回会员中心</a><br/>
        <span class="ddd-qs">
            <a href="/Default.Aspx?Cid=<%=_Uid %>&amp;v=1">【返回博中首页】</a>
        </span><br/>
     <!--#include file="../bottom.html"-->
          时间:<% Response.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));%> <p class="link"></p>
        <!--#include file="../bottom1.html"-->
</body>
</html>
