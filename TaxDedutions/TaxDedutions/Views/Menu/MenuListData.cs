using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxDedutions.Views
{
    public class MenuListData : List<MenuItem>
    {
        public MenuListData()
        {
            this.Add(new MenuItem()
            {
                Title = " ",
                //   IconSource = "MyRoutes.png",
                // TargetType = typeof(EntryRecord)
            });

            this.Add(new MenuItem()
            {
                Title = "MAIN",
                IconSource = "Main.png",
                TargetType = typeof(MainPage)
            });

            this.Add(new MenuItem()
            {
                Title = "ADD INVOICE",
                IconSource = "Invoice.png",
                TargetType = typeof(EntryRecord)
            });

            this.Add(new MenuItem()
            {
                Title = "VIEW INVOICES",
                IconSource = "List.png",
                TargetType = typeof(RecordList)
            });

            this.Add(new MenuItem()
            {
                Title = "EXPORT",
                IconSource = "Export.png",
               // TargetType = typeof(AccountsPage)
            });

            this.Add(new MenuItem()
            {
                Title = "DEDUCTIONS",
                IconSource = "Info.png",
               // TargetType = typeof(OpportunitiesPage)
            });
        }
    }
}
