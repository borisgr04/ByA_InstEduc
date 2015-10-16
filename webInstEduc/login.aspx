<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Skeleton.WebAPI.login" %>

<!DOCTYPE html>

<html lang="es">
<head id="Head1" runat="server">
<meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Col. Calleja Real &reg</title>

    <script type="text/javascript" src="jqscripts/jquery-1.10.1.min.js"></script>
    <!-- Latest compiled and minified CSS -->
	<link rel="stylesheet" href="//netdna.bootstrapcdn.com/bootstrap/3.0.0/css/bootstrap.min.css">
	
	<!-- Optional theme -->
	<link rel="stylesheet" href="//netdna.bootstrapcdn.com/bootstrap/3.0.0/css/bootstrap-theme.min.css">
	
	<!-- Latest compiled and minified JavaScript -->
	<script src="//netdna.bootstrapcdn.com/bootstrap/3.0.0/js/bootstrap.min.js"></script>
	
	<style>
        body {
  padding-top: 40px;
  padding-bottom: 40px;
  background-color: #eee;
  background-color: #fff;
}

.form-signin-img {
  max-width: 400px;
  padding: 5px;
  margin: 0 auto;
  background-color: #fff;
  
  background-color: #3276b1;
  border-color: #285e8e;
  color: #ffffff;
  font-size: 16px;
  text-align:center;
}
.form-signin {
  max-width: 400px;
  padding: 15px;
  margin: 0 auto;
  background-color:#eee;
}
.form-signin .form-signin-heading,
.form-signin .checkbox {
  margin-bottom: 10px;
}
.form-signin .checkbox {
  font-weight: normal;
}
.form-signin .form-control {
  position: relative;
  font-size: 16px;
  height: auto;
  padding: 10px;
  -webkit-box-sizing: border-box;
     -moz-box-sizing: border-box;
          box-sizing: border-box;
}
.form-signin .form-control:focus {
  z-index: 2;
}
.form-signin input[type="text"] {
  margin-bottom: -1px;
  border-bottom-left-radius: 0;
  border-bottom-right-radius: 0;
}
.form-signin input[type="password"] {
  margin-bottom: 10px;
  border-top-left-radius: 0;
  border-top-right-radius: 0;
}
    </style>
	
</head>

    
<body>	
    <div class="form-signin-img">
        <h3> Colegio Calleja Real &reg</h3>
        <%--<img src="Images/imagesLogin/sircc.png" alt="Usuario"
            style="margin: 0px;" />--%>
    </div>
    <form id="form1" runat="server" class="form-signin">
        <div class="container">
            <h2>Login</h2>
            <asp:TextBox runat="server" ID="UserName" class="form-control" required autofocus />
            <%--<input type="text" runat="server" class="form-control" id="txtUserName" placeholder="Identificación" required autofocus>--%>
            <br />
            <asp:TextBox runat="server" ID="Password" TextMode="Password" class="form-control" required />
            <asp:Label ID="lbMsg" runat="server" Text=""></asp:Label>
            <label class="checkbox">
                <input type="checkbox" value="remember-me" runat="server" id="chkRemember"  >
                Mantener la sesion iniciada
            </label>
            <asp:Button ID="BtnIniciar" runat="server" Text="Iniciar" CssClass="btn btn-lg btn-primary btn-block"
                 OnClick="BtnIniciar_Click1" />
            <asp:Literal runat="server" ID="StatusText" />
        </div>


    </form>

    <div class="container">
    </div>
    <!-- /container -->


    <!-- Bootstrap core JavaScript
    ================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    
</body>
</html>
