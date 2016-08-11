using System;
using System.IO;
using Xamarin.Forms;
using SQLite;
using TaxDedutions.iOS;
using TaxDedutions.Interfaces;

[assembly: Dependency(typeof(IOS_SQLite))]
namespace TaxDedutions.iOS
{
    public class IOS_SQLite : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            var dbName = "Records.sqlite";
            string dbPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder  
            string libraryPath = Path.Combine(dbPath, "..", "Library"); // Library folder  
            var path = Path.Combine(libraryPath, dbName);
            var conn = new SQLite.SQLiteConnection(path);
            return conn;
        }
    }
}
