using System;
using Prism.Navigation;

namespace LostArkMobile.ViewModels
{
    public class NavigationRootViewModel : ViewModelBase
    {
        public NavigationRootViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }
    }
}
