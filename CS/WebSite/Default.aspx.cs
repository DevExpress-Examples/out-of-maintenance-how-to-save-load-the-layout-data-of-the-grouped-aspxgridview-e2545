using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using System.Collections.ObjectModel;
using System.Text;

public partial class _Default : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {
        if (Session["layout"] == null)
            Session["layout"] = new Dictionary<int, string>();
    }
    protected void callback_Callback(object source, DevExpress.Web.ASPxCallback.CallbackEventArgs e) {
        grid.DataBind();
        Dictionary<int, string> dictionary = Session["layout"] as Dictionary<int, string>;
        if (dictionary != null) {
            if (grid.GetGroupedColumns().Count == 0) {
                Session["fields"] = string.Empty;
                Session["rowscount"] = 0;
                return;
            }
            dictionary.Clear();
            for (int i = 0; i < grid.VisibleRowCount; i++) {
                if (grid.IsGroupRow(i) && grid.IsRowExpanded(i)) {
                    dictionary[i] = "Saved";
                }
            }
            Session["rowscount"] = grid.VisibleRowCount;
            ReadOnlyCollection<GridViewDataColumn> cols = grid.GetGroupedColumns() as ReadOnlyCollection<GridViewDataColumn>;
            StringBuilder sb = new StringBuilder();
            foreach (GridViewDataColumn item in cols) {
                sb.Append(item.FieldName);
                sb.Append(';');
            }
            sb.Remove(sb.Length - 1, 1);
            Session["fields"] = sb.ToString();
        }
    }
    protected void grid_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e) {
        Dictionary<int, string> dictionary = Session["layout"] as Dictionary<int, string>;
        if (dictionary == null || Session["rowscount"] == null || Session["fields"] == null)
            return;

        for (int i = 0; i < grid.Columns.Count; i++)
            grid.UnGroup(grid.Columns[i]);
        grid.CollapseAll();

        string[] fields = Session["fields"].ToString().Split(';');
        if (fields[0] == string.Empty)
            return;
        foreach (string field in fields) {
            grid.GroupBy(grid.Columns[field]);
        }
        for (int i = 0; i < Convert.ToInt32(Session["rowscount"]); i++) {
            if (dictionary.ContainsKey(i))
                grid.ExpandRow(i);
        }
    }
}
