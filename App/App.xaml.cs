using Data;
namespace App
{
    public partial class App : Application
    {
        public static DbActions Db { get; set; }
        public App()
        {
            InitializeComponent();
            Db = DbActions.GetDb();
            MainPage = new AppShell();
        }
    }
}