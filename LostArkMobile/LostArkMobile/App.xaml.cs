using Prism;
using Prism.Ioc;
using LostArkMobile.ViewModels;
using LostArkMobile.Views;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using Xamarin.Forms;
using System.Collections.Generic;

namespace LostArkMobile
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
            var absID = "ca-app-pub-8838207688406048/9378457141";
#if DEBUG
            absID = "ca-app-pub-3940256099942544/6300978111";
#endif
            MarcTron.Plugin.CrossMTAdmob.Current.AdsId = absID;
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("MDPRoot/NavigationRoot/EventList");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<Notice, NoticeViewModel>();
            containerRegistry.RegisterForNavigation<EventList, EventListViewModel>();
            containerRegistry.RegisterForNavigation<EventSetting, EventSettingViewModel>();
            containerRegistry.RegisterForNavigation<NavigationRoot, NavigationRootViewModel>();
            containerRegistry.RegisterForNavigation<MDPRoot, MDPRootViewModel>();
        }
    }
}
