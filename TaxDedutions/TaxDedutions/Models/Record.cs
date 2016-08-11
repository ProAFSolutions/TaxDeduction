using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxDedutions.Models
{
    public class Record
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }

        public DateTime DateCreate { get; set; }
        public string Image { get; set; }
        public bool HasImage { get; set; }
        public string Amount { get; set; }
        public string Type { get; set; }

        public string Description { get; set; }

        public string Note { get; set; }

        public Record()
        {
        }
    }
}
