using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxDedutions.Interfaces;
using TaxDedutions.Models;
using Xamarin.Forms;

namespace TaxDedutions.DB
{
    public class RecordDatabase
    {
        private SQLiteConnection conn;

        //CREATE  
        public RecordDatabase()
        {
            conn = DependencyService.Get<ISQLite>().GetConnection();
            conn.CreateTable<Record>();
            conn.CreateTable<Family>();
        }

        #region Record

        //READ  
        public IEnumerable<Record> GetRecords()
        {
            var records = (from mem in conn.Table<Record>() select mem);
            return records.ToList().OrderByDescending(x => x.DateCreate);
        }
        //INSERT  
        public string AddRecord(Record record)
        {
            conn.Insert(record);
            return "success";
        }
        //DELETE  
        public string DeleteRecord(int id)
        {
            conn.Delete<Record>(id);
            return "success";
        }

        #endregion

        #region Family

        //READ  
        public Family GetFamily()
        {
            var families = (from mem in conn.Table<Family>() select mem);
            return families.ToList().FirstOrDefault();
        }
        //INSERT  
        public string AddFamily(Family family)
        {
            conn.Insert(family);
            return "success";
        }

        #endregion
    }
}
