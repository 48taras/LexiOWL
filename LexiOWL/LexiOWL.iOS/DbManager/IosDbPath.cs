using LexiOWL.iOS.DbManager;
using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(IosDbPath))]
namespace LexiOWL.iOS.DbManager
{
    public class IosDbPath : IDbPath
    {
        public string GetDatabasePath(string sqliteFilename)
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", sqliteFilename);
        }
    }
}