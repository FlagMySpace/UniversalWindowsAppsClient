using System;
using System.Linq;
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
        public ProfilePageViewModel(INavigationService navigationService, IPerson person)
        {
            Person = person;
            NavigationService = navigationService;
        }

        public ProfilePageViewModel()
        {
        }

        public IPerson Person { get; set; }
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
                await new MessageDialog("your changes have been saved, if you change your email address, you need to check your email for verification", "Success").ShowAsync();
                if (NavigationService.CanGoBack())
                {
                    NavigationService.GoBack();
                }
                else
                {
                    NavigationService.ClearHistory();
                    NavigationService.Navigate(App.Experiences.Home.ToString(), null);
                }
            }
        }
    }
}