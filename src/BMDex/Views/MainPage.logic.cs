using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using BMDex.Abstractions;
using BMDex.Services;
using BMDex.ViewModels;
using PokeApiNet;
using Xamarin.Forms;

namespace BMDex.Views
{
    public partial class MainPage
    {

        public MainPage()
        {
            BindingContext = DependencyService.Get<MainViewModel>();
            Content = Build();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await OnAppearingAsync().ConfigureAwait(false);
        }

        private async Task OnAppearingAsync()
        {
            if (BindingContext is IInitialize viewModel)
                await viewModel.InitializeAsync();
        }
    }
}
