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
                Title = "Entry Record",
                IconSource = "Invoice.png",
                TargetType = typeof(EntryRecord)
            });

            this.Add(new MenuItem()
            {
                Title = "Leads",
              //  IconSource = "leads.png",
               // TargetType = typeof(LeadsPage)
            });

            this.Add(new MenuItem()
            {
                Title = "Accounts",
               // IconSource = "accounts.png",
               // TargetType = typeof(AccountsPage)
            });

            this.Add(new MenuItem()
            {
                Title = "Opportunities",
              //  IconSource = "opportunities.png",
               // TargetType = typeof(OpportunitiesPage)
            });
        }
    }
}
