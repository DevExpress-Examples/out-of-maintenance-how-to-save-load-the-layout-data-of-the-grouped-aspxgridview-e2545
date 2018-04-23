Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports DevExpress.Web.ASPxGridView
Imports System.Collections.ObjectModel
Imports System.Text

Partial Public Class _Default
	Inherits System.Web.UI.Page
	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
		If Session("layout") Is Nothing Then
			Session("layout") = New Dictionary(Of Integer, String)()
		End If
	End Sub
	Protected Sub callback_Callback(ByVal source As Object, ByVal e As DevExpress.Web.ASPxCallback.CallbackEventArgs)
		grid.DataBind()
		Dim dictionary As Dictionary(Of Integer, String) = TryCast(Session("layout"), Dictionary(Of Integer, String))
		If dictionary IsNot Nothing Then
			If grid.GetGroupedColumns().Count = 0 Then
				Session("fields") = String.Empty
				Session("rowscount") = 0
				Return
			End If
			dictionary.Clear()
			For i As Integer = 0 To grid.VisibleRowCount - 1
				If grid.IsGroupRow(i) AndAlso grid.IsRowExpanded(i) Then
					dictionary(i) = "Saved"
				End If
			Next i
			Session("rowscount") = grid.VisibleRowCount
			Dim cols As ReadOnlyCollection(Of GridViewDataColumn) = TryCast(grid.GetGroupedColumns(), ReadOnlyCollection(Of GridViewDataColumn))
			Dim sb As New StringBuilder()
			For Each item As GridViewDataColumn In cols
				sb.Append(item.FieldName)
				sb.Append(";"c)
			Next item
			sb.Remove(sb.Length - 1, 1)
			Session("fields") = sb.ToString()
		End If
	End Sub
	Protected Sub grid_CustomCallback(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs)
		Dim dictionary As Dictionary(Of Integer, String) = TryCast(Session("layout"), Dictionary(Of Integer, String))
		If dictionary Is Nothing OrElse Session("rowscount") Is Nothing OrElse Session("fields") Is Nothing Then
			Return
		End If

		For i As Integer = 0 To grid.Columns.Count - 1
			grid.UnGroup(grid.Columns(i))
		Next i
		grid.CollapseAll()

		Dim fields() As String = Session("fields").ToString().Split(";"c)
		If fields(0) = String.Empty Then
			Return
		End If
		For Each field As String In fields
			grid.GroupBy(grid.Columns(field))
		Next field
		For i As Integer = 0 To Convert.ToInt32(Session("rowscount")) - 1
			If dictionary.ContainsKey(i) Then
				grid.ExpandRow(i)
			End If
		Next i
	End Sub
End Class
