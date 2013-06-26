using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using System.Drawing;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using System.Collections.Generic;
using Playground.Code;

namespace Playground.SessionCount
{
    [ToolboxItemAttribute(false)]
    public partial class SessionCount : WebPart
    {
        // Heben Sie die Auskommentierung des folgenden SecurityPermission-Attributs nur auf, wenn Sie eine Leistungsprofilerstellung für eine Farmlösung
        // mithilfe der Instrumentation-Methode durchführen, und entfernen Sie dann das SecurityPermission-Attribut, wenn der Code bereit für die
        // Produktion ist. Da das SecurityPermission-Attribut die Sicherheitsüberprüfung für Aufrufer Ihres
        // Konstruktors umgeht, wird es für Produktionszwecke nicht empfohlen.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public SessionCount()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //ListName ist noch nicht gesetzt
            if (String.IsNullOrEmpty(ListName))
            {
                Label errorLabel = new Label
                {
                    Text = "Das Webpart muss noch konfiguriert werden!",
                    ForeColor = Color.Red
                };
                Controls.Add(errorLabel);
                return;
            }
            try
            {
                SPWeb web = SPContext.Current.Web;
                SPList list = web.Lists[ListName];
                LabelListeName.Text = ListName;
                LabelListItemCount.Text = list.ItemCount.ToString();
            }
            catch (Exception ex)
            {
                Label errorLabel = new Label
                {
                    Text = ex.Message,
                    ForeColor = Color.Red
                };
                Controls.Add(errorLabel);
            }
        }

        //editor part beim webpart anmelden
        [WebBrowsable(true)]//es werde sichtbar! (Anweisung an Sharepoint)
        [Personalizable(PersonalizationScope.User)]//ein User(in dem Fall der Entwickler) stellt für alle anderen ein,
        //dass es Personalisierbar ist, d.h. der User kann das Webpart auf einer Seite ziehen
        [Microsoft.SharePoint.WebPartPages.SPWebCategoryName("Einstellungen")]
        [WebDisplayName("Name der Liste")]
        public string ListName { get; set; }
        
        public override EditorPartCollection CreateEditorParts()
        {
            var editorParts = new List<EditorPart>();
            var editorPart = new ListSelectorEditorPart();
            editorPart.ID = this.ID + "_listSelector";
            editorParts.Add(editorPart);
            var existing = base.CreateEditorParts();
            return new EditorPartCollection(existing, editorParts);
        }
    }
}
