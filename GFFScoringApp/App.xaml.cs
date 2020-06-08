using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using GFFScoringApp.Services;
using GFFScoringApp.Views;
using Xamarin.Forms.Internals;

namespace GFFScoringApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            Device.SetFlags(new[] {
                "CarouselView_Experimental",
                "IndicatorView_Experimental"
            });

            DependencyService.Register<MockDataStore>();
            var mainPage = new MainPage();
            MainPage = new NavigationPage(mainPage) { Title = "GFF" }; // your page here
            

            ((MainPage)mainPage).Splash.Clicked += async (object sender, EventArgs e) =>
            {
                await MainPage.Navigation.PushAsync(new CharactersPage());
            };

           

        }

        public static async Task Sleep(int ms)
        {
            await Task.Delay(ms);
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
