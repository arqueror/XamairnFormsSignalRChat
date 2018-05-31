using System;
using System.IO;
using App.Infrastructure;
using RaccoonMsgr.Android;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppRuntimeSettings))]
namespace RaccoonMsgr.Android
{
    class AppRuntimeSettings : RuntimeSettingsBase
    {
        public AppRuntimeSettings()
        {

        }
        public override SQLiteConnection CreateSqLiteConnection()
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, DatabaseFilename);
            //var currentPlatform = new SQLitePlatformAndroid();
            var connection = new SQLiteConnection(path);
            return connection;
        }
        public override SQLiteAsyncConnection CreateSqLiteAsyncConnection()
        {
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, DatabaseFilename);
            //var currentPlatform = new SQLitePlatformAndroid();
            var connection = new SQLiteAsyncConnection(path);
            return connection;
        }
    }
}