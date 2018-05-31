using System;
using SQLite;

namespace App.Infrastructure
{
    public abstract class RuntimeSettingsBase
    {
        public abstract SQLiteAsyncConnection CreateSqLiteAsyncConnection();
        public abstract SQLiteConnection CreateSqLiteConnection();
        public string DatabaseFilename { get; } = "bf_Database.db3";
        public RuntimeSettingsBase()
        {
        }
    }
}
