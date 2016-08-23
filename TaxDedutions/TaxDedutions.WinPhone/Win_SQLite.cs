using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;
using Xamarin.Forms;
using Windows.Storage;
using System.IO;
using TaxDedutions.WinPhone;
using TaxDedutions.Interfaces;

[assembly: Dependency(typeof(WinSQLite))]

namespace TaxDedutions.WinPhone
{
    public class WinSQLite : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            var dbName = "Records.db3";
            var path = Path.Combine(ApplicationData.
              Current.LocalFolder.Path, dbName);
            return new SQLiteConnection(path);
        }
    }
}
