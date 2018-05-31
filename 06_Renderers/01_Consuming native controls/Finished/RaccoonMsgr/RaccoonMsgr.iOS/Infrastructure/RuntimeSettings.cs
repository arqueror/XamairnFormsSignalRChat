using System;
using System.IO;
using App.Infrastructure;
using RaccoonMsgr.iOS;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppRuntimeSettings))]
namespace RaccoonMsgr.iOS
{
    class AppRuntimeSettings : RuntimeSettingsBase
    {
        public AppRuntimeSettings()
        {

        }
        public override SQLiteConnection CreateSqLiteConnection()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var libraryPath = Path.Combine(documentsPath, "..", "Library");
            var path = Path.Combine(libraryPath, DatabaseFilename);
            //var currentPlatform = new SQLitePlatformIOS();
            var connection = new SQLiteConnection(path);
            return connection;
        }
        public override SQLiteAsyncConnection CreateSqLiteAsyncConnection()
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var libraryPath = Path.Combine(documentsPath, "..", "Library");
            var path = Path.Combine(libraryPath, DatabaseFilename);
            //var currentPlatform = new SQLitePlatformIOS();
            var connection = new SQLiteAsyncConnection(path);
            return connection;
        }
    }
}