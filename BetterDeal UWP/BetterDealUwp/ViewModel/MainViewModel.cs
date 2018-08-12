using BetterDeal.DataAccessObject;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Services.Maps;
using Windows.UI.Popups;

namespace BetterDeal.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private INavigationService _navigationService;



        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            IsLoading = false;
        }
        public static bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }


        private ICommand _loginButtonClick;
        public ICommand LoginButtonClick
        {
            get
            {
                if (_loginButtonClick == null)
                    _loginButtonClick = new RelayCommand(async () => await LoginEvent());
                return _loginButtonClick;
            }
            set { _loginButtonClick = value; }
        }

        private string _emailValue;

        public string EmailValue
        {
            get { return _emailValue; }
            set
            {
                _emailValue = value;
                RaisePropertyChanged("EmailValue");
            }
        }

        private string _passwordValue;

        public string PasswordValue
        {
            get { return _passwordValue; }
            set
            {
                _passwordValue = value;
                RaisePropertyChanged("PasswordValue");
            }
        }

        private async Task LoginEvent()
        {
            if (IsValidEmail(_emailValue) && IsValidPassword(_passwordValue))
            {
                IsLoading = true;
                var ds = new DataService();
                try
                {
                    await ds.Login(_emailValue, _passwordValue);

                    if (DataService._user.Status != "admin")
                    {
                        IsLoading = false;
                        throw new Exception("Vous n'etes pas admin !");
                    }
                    _navigationService.NavigateTo("WelcomePage");
                }
                catch (Exception e)
                {
                    IsLoading = false;
                    var dialog = new Windows.UI.Popups.MessageDialog(e.Message, "Erreur");

                    dialog.Commands.Add(new Windows.UI.Popups.UICommand("Ok") { Id = 0 });

                    var result = dialog.ShowAsync();
                }
            }
            else {
                var dialog = new Windows.UI.Popups.MessageDialog("Email/password isnt valid !");

                dialog.Commands.Add(new Windows.UI.Popups.UICommand("Ok") { Id = 0 });

                var result = dialog.ShowAsync();
            }
                
        }

        public bool IsValidPassword(string password)
        {
            if (password != null)
            {
                if (password.Length > 6 && password.Length < 50)
                    return true;
            }
            return false;
        }


        public bool IsValidEmail(string email) {
            if (new EmailAddressAttribute().IsValid(email)) return true;
            return false;
        }

    }
}
