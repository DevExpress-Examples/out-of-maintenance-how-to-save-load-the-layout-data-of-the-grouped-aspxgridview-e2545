<%@ Page Language="vb" AutoEventWireup="true" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.v15.1, Version=15.1.15.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web" TagPrefix="dx" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>How to save/store ASPxGridView's layout</title>
</head>
<body>
	<form runat="server" id="form1">
	<dx:ASPxGridView ID="grid" runat="server" AutoGenerateColumns="False" ClientInstanceName="grid"
		DataSourceID="SqlDataSource1" KeyFieldName="ProductID" OnCustomCallback="grid_CustomCallback">
		<SettingsPager EllipsisMode="None" PageSize="15">
		</SettingsPager>
		<Columns>
			<dx:GridViewDataTextColumn FieldName="ProductID" ReadOnly="True" VisibleIndex="0">
				<EditFormSettings Visible="False" />
			</dx:GridViewDataTextColumn>
			<dx:GridViewDataTextColumn FieldName="ProductName" VisibleIndex="1">
			</dx:GridViewDataTextColumn>
			<dx:GridViewDataTextColumn FieldName="CategoryID" VisibleIndex="2">
			</dx:GridViewDataTextColumn>
		</Columns>
		<Settings ShowGroupPanel="True" />
	</dx:ASPxGridView>
	<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:NorthwindConnectionString %>"
		SelectCommand="SELECT [ProductID], [ProductName], [CategoryID] FROM [Products]">
	</asp:SqlDataSource>
	<dx:ASPxButton ID="buttonLoad" runat="server" AutoPostBack="False" Text="Load Layout">
		<ClientSideEvents Click="function(s, e) {
			grid.PerformCallback();
		}" />
	</dx:ASPxButton>
	<dx:ASPxButton ID="buttonSave" runat="server" AutoPostBack="False" Text="Save Layout">
		<ClientSideEvents Click="function(s, e) {
			callback.PerformCallback();
		}" />
	</dx:ASPxButton>
	<dx:ASPxCallback ID="callback" runat="server" ClientInstanceName="callback" OnCallback="callback_Callback">
	</dx:ASPxCallback>
	</form>
</body>
</html>