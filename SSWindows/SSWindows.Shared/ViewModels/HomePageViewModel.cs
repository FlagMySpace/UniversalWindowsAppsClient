using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using SSWindows.Interfaces;

namespace SSWindows.ViewModels
{
    public class HomePageViewModel : ViewModel, IHomePageViewModel
    {
        public HomePageViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public INavigationService NavigationService { get; set; }
    }
}
