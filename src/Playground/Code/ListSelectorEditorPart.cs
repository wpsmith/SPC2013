using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Playground.Code
{
    public class ListSelectorEditorPart : EditorPart
    {
        private Playground.SessionCount.SessionCount webpart;
        private DropDownList ddlLists;

        protected override void OnLoad(EventArgs e)
        {
            Title = "Einstellungen";
            Description = "Liste zur Anzeige auswählen";
            GroupingText = "Auswahl Liste";
            webpart = WebPartToEdit as Playground.SessionCount.SessionCount;
            base.OnLoad(e);
        }

        protected override void CreateChildControls()
        {
            SPWeb web = SPContext.Current.Web;
            var lists = web.GetListsOfType(SPBaseType.GenericList)
                        .OfType<SPList>().
                        Where(list => !list.Hidden)//keine versteckten Listen anzeigen
                        .Select(list => new ListItem
                        {
                            Text = list.Title,
                            Value = list.ID.ToString()
                        });
            ddlLists = new DropDownList();
            ddlLists.Items.AddRange(lists.ToArray());
            Controls.Add(ddlLists);
            base.CreateChildControls();
        }

        public override void SyncChanges()
        {
            EnsureChildControls();
            SPList currentLib = null;
            try
            {
                currentLib = SPContext.Current.Web.Lists[webpart.ListName];
                ddlLists.SelectedValue = currentLib.ID.ToString();
            }
            catch (Exception)
            {
            }
        }

        public override bool ApplyChanges()
        {
            EnsureChildControls();
            if (ddlLists.SelectedIndex > -1)//es ist etwas ausgewählt worden
            {
                Guid id = new Guid(ddlLists.SelectedValue);
                webpart.ListName = SPContext.Current.Web.Lists[id].Title;
                return true;
            }
            return false;
        }
    }
}
