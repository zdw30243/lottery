<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyXin.aspx.cs" Inherits="bocai_001.User.MyXin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <!--#include file="/head1.html"-->
        <title>任选大小-重-庆-时-时-彩</title>
        <!--#include file="/head2.html"-->
    </head><body>
    <form id="form1" runat="server">
        <div>
            <%=_result%>
        </div>
    </form>
    <hr /><span class="ddd-qs"><a href="Default.Aspx?Cid=<%=_Uid %>&amp;v=1">[返回首页]</a></span><br />
        <!--#include file="../bottom.html"-->
          时间:<% Response.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));%> <p class="link"></p>
        <!--#include file="../bottom1.html"-->
</body>
</html>
