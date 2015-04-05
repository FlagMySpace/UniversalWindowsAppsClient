using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.Mvvm.Interfaces;
using Parse;
using SSWindows.Interfaces;
using SSWindows.Models;

namespace SSWindows.ViewModels
{
    public class ProfilePageViewModel : ViewModel, IProfilePageViewModel
    {
        public Person Person { get; set; }

        public ProfilePageViewModel(INavigationService navigationService)
        {
            Person = new Person();
            NavigationService = navigationService;
        }

        public ProfilePageViewModel()
        {

        }

        public INavigationService NavigationService { get; set; }

        public async Task UpdateProfile(string oldUsername, string oldPassword)
        {
            var error = await Person.Update(oldUsername, oldPassword);

            if (error.Any())
            {
                await new MessageDialog(error, "Error").ShowAsync();
            }

            if (ParseUser.CurrentUser == null)
            {
                NavigationService.ClearHistory();
                NavigationService.Navigate(App.Experiences.Login.ToString(), null);
            }
            else
            {
                await new MessageDialog("your changes have been saved", "Success").ShowAsync();
            }
        }
    }
}
