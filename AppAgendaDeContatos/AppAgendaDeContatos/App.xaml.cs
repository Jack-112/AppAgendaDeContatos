using AppAgendaDeContatos.Helper;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppAgendaDeContatos
{
    public partial class App : Application
    {
        static SQLiteDataBaseHelper db;
        public static SQLiteDataBaseHelper Database
        {
            get
            {

                if (db == null)
                {

                    string path = Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        "AppAgendaContatos.db3"
                    );

                    db = new SQLiteDataBaseHelper(path);
                }

                return db;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new View.inicio()); ;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
