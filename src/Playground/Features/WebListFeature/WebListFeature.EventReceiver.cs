using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;

namespace Playground.Features.WebListFeature
{
    /// <summary>
    /// Durch diese Klasse werden Ereignisse behandelt, die während der Aktivierung, Deaktivierung, Installation, Deinstallation und bei Upgrades von Funktionen ausgelöst werden.
    /// </summary>
    /// <remarks>
    /// Die an diese Klasse angefügte GUID kann beim Verpacken verwendet werden und sollte nicht geändert werden.
    /// </remarks>

    [Guid("a1004e21-3a24-4df3-a9c8-69057c8b0f0f")]
    public class WebListFeatureEventReceiver : SPFeatureReceiver
    {
        //Erzeugen von 3 Listen per Code
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPWeb web = properties.Feature.Parent as SPWeb; // as SPWeb ist ein weicher Cast 
            //-> entspricht es nicht dem Typ, fliegt keine Exception,sindern es wird null zurückgegeben
            if (web != null)
            {
                SPList list = web.Lists["Schulungen"];

                //add first item
                SPListItem item = list.AddItem();
                item[SPBuiltInFieldId.Title] = "ASP.NET";
                //SessionRoom (GUID aus Elements.xml von CustomFields kopiert)
                item[new Guid("{b14ba9c8-5ef8-462f-883a-5bbd79919e3a}")]= "234";
                //Speaker
                item[new Guid("{10F85203-8414-43AD-84D9-DC5D7F704687}")] = "Krause";
                item.Update();
               
                //add second item
                item = list.AddItem();
                item[SPBuiltInFieldId.Title] = "CSS";
                //SessionRoom (GUID aus Elements.xml von CustomFields kopiert)
                item[new Guid("{b14ba9c8-5ef8-462f-883a-5bbd79919e3a}")] = "255";
                //Speaker
                item[new Guid("{10F85203-8414-43AD-84D9-DC5D7F704687}")] = "Jäger";
                item.Update();

                //add third item
                item = list.AddItem();
                item[SPBuiltInFieldId.Title] = "C#";
                //SessionRoom (GUID aus Elements.xml von CustomFields kopiert)
                item[new Guid("{b14ba9c8-5ef8-462f-883a-5bbd79919e3a}")] = "a.110";
                //Speaker
                item[new Guid("{10F85203-8414-43AD-84D9-DC5D7F704687}")] = "Mang";
                item.Update();
            }
        }


        // Auskommentierung der Methode unten aufheben, um das Ereignis zu behandeln, das vor der Deaktivierung einer Funktion ausgelöst wird.

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
        }


        // Auskommentierung der Methode unten aufheben, um das Ereignis zu behandeln, das nach der Installation einer Funktion ausgelöst wird.

        //public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        //{
        //}


        // Auskommentierung der Methode unten aufheben, um das Ereignis zu behandeln, das vor der Deinstallation einer Funktion ausgelöst wird.

        //public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        //{
        //}

        // Auskommentierung der Methode unten aufheben, um das Ereignis zu behandeln, das bei der Aktualisierung einer Funktion ausgelöst wird.

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}
    }
}
