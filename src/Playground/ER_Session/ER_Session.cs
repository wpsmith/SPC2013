using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;

namespace Playground.ER_Session
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class ER_Session : SPItemEventReceiver
    {
        public override void ItemAdding(SPItemEventProperties properties)
        {
            base.ItemAdding(properties);
        }

        public override void ItemUpdating(SPItemEventProperties properties)
        {
            base.ItemUpdating(properties);
        }

        public override void ItemDeleting(SPItemEventProperties properties)
        {
            base.ItemDeleting(properties);
        }

        public override void ItemAdded(SPItemEventProperties properties)
        {
            base.ItemAdded(properties);
            properties.ListItem["Title"] = String.Format("{0} in {1} ({2})", properties.ListItem.Title, properties.ListItem["SessionRoom"], properties.ListItem["Speaker"]);

            properties.ListItem.Update();
        }


    }
}