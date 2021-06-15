using BMDex.Services;
using BMDex.ViewModels;
using PokeApiNet;
using Xamarin.Forms;

namespace BMDex
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Views.MainPage());
            DependencyService.Register<IAbilityService, AbilityService>();
            DependencyService.Register<IPokemonService, PokemonService>();
            DependencyService.Register<PokeApiClient>();
            DependencyService.Register<MainViewModel>();
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
