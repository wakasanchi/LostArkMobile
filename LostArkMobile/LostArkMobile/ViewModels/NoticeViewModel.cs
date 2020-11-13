using LostArkMobile.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LostArkMobile.ViewModels
{
    public class NoticeViewModel : ViewModelBase
    {
        public NoticeViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "お知らせ";
        }
    }
}
