using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TaxDedutions
{
    public static class Constants
    {
        public static Dictionary<string, Color> Deductions = new Dictionary<string, Color>
        {
            { "Uncategorized", Color.Red
    },
            { "Medical and Dental Expenses", Color.Aqua
},
            { "Deductible Taxes", Color.Blue },
            { "Home Mortgage Points", Color.Gray },
            { "Interest Expense", Color.Lime },
            { "Charitable Contributions", Color.Navy },
            { "Miscellaneous Expenses", Color.Purple },
            { "Business Use of Home", Color.Silver },
            { "Business Use of Car", Color.White },
            { "Business Travel Expenses", Color.Yellow },
            { "Business Entertainment Expenses", Color.Green },
            { "Work-Related Education Expenses", Color.Pink },
            { "Employee Business Expenses", Color.Silver },
            { "Casualty, Disaster, and Theft Losses", Color.Teal }
        };

        public static Dictionary<string, string> DeductionsLinks = new Dictionary<string, string>
        {
            { "Uncategorized", ""
    },
            { "Medical and Dental Expenses", "https://www.irs.gov/taxtopics/tc502.html"
},
            { "Deductible Taxes", "https://www.irs.gov/taxtopics/tc503.html" },
            { "Home Mortgage Points", "https://www.irs.gov/taxtopics/tc504.html" },
            { "Interest Expense", "https://www.irs.gov/taxtopics/tc505.html" },
            { "Charitable Contributions", "https://www.irs.gov/taxtopics/tc506.html" },
            { "Miscellaneous Expenses", "https://www.irs.gov/taxtopics/tc507.html" },
            { "Business Use of Home", "https://www.irs.gov/taxtopics/tc508.html" },
            { "Business Use of Car", "https://www.irs.gov/taxtopics/tc509.html" },
            { "Business Travel Expenses", "https://www.irs.gov/taxtopics/tc510.html" },
            { "Business Entertainment Expenses", "https://www.irs.gov/taxtopics/tc511.html" },
            { "Work-Related Education Expenses", "https://www.irs.gov/taxtopics/tc512.html" },
            { "Employee Business Expenses", "https://www.irs.gov/taxtopics/tc513.html" },
            { "Casualty, Disaster, and Theft Losses", "https://www.irs.gov/taxtopics/tc514.html" }
        };
    }
}
