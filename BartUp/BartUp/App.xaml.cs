using BartUp.ViewModels;
using BartUp.Views;
using Prism;
using Prism.Ioc;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace BartUp
{
    public partial class App : Prism.DryIoc.PrismApplication
    {
        public App(IPlatformInitializer initializer) : base(initializer) { }

        public App() : this(null) { }
        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MainPage, MainViewModel>();
            
        }
    }
}
