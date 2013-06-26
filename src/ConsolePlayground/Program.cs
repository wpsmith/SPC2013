using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsolePlayground
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var site = new SPSite("http://sps6:4711"))
            {
                using (var web = site.OpenWeb())
                {
                    var sessionList = web.GetList(@"/Lists/Schulungen"); 

                    foreach (var item in sessionList.Items.Cast<SPListItem>())
                    {
                        Console.WriteLine("Title: " + item.Title + " Room: " + item["Raumnummer"]);
                    }

                    Console.ReadLine();
                }
            }
            
        }
    }
}
