using BetterDeal.DataAccessObject;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;

namespace BetterDeal.ViewModel
{
    public class WelcomeViewModel : ViewModelBase
    {
        private INavigationService _navigationService = null;
        private Publication _selectedPublication;
        private ObservableCollection<Publication> _newsfeed = null;

        private ICommand _buttonDelete;
        public ICommand ButtonDelete
        {
            get
            {
                if (_buttonDelete == null)
                    _buttonDelete = new RelayCommand(async () => await DeleteEvent());
                return _buttonDelete;
            }
            set { _buttonDelete = value; }
        }

        private ICommand _buttonNewPublication;
        public ICommand ButtonNewPublication
        {
            get
            {
                if (_buttonNewPublication == null)
                    _buttonNewPublication = new RelayCommand(async () => await NewPublicationEvent());
                return _buttonNewPublication;
            }
            set { _buttonNewPublication = value; }
        }

        private ICommand _buttonModifyPublication;
        public ICommand ButtonModifyPublication 
        {
            get
            {
                if (_buttonModifyPublication == null)
                    _buttonModifyPublication = new RelayCommand(async () => await ModifyPublicationEvent());
                return _buttonModifyPublication;
            }
            set { _buttonModifyPublication = value; }
        }

        private ICommand _buttonRefresh;
        public ICommand ButtonRefresh
        {
            get
            {
                if (_buttonRefresh == null)
                    _buttonRefresh = new RelayCommand(async () => await RefreshEvent());
                return _buttonRefresh;
            }
            set { _buttonRefresh = value; }
        }

        private ICommand _buttonLogout;
        public ICommand ButtonLogout
        {
            get {
                if (_buttonLogout == null)
                    _buttonLogout = new RelayCommand(async () => await LogoutEvent());
                return _buttonLogout;
            }
            set { _buttonRefresh = value; }
        }


        public ObservableCollection<Publication> NewsFeed
        {
            get { return _newsfeed; }
            set
            {
                if (_newsfeed == value)
                {
                    return;
                }
                _newsfeed = value;
                RaisePropertyChanged("NewsFeed");
            }
        }

        public Publication SelectedPublication
        {
            get { return _selectedPublication; }
            set
            {
                _selectedPublication = value;
                if (_newsfeed != null)
                {
                    RaisePropertyChanged("SelectedPublication");
                }

            }
        }

        public WelcomeViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            var newsFeed = new NewsFeed();
            if (IsInDesignMode)
            {
                var publications = new List<Publication>();
                for (int i = 0; i < 40; i++)
                {
                    publications.Add(new Publication()
                    {
                        Title = "No Data",
                        Description = "No Data",
                        ApplicationUser = null
                    });
                }
                newsFeed.PublicationNewsFeed = publications;
                NewsFeed = new ObservableCollection<Publication>(publications);
            }
            else
            {
                InitializeAsync();
            }
        }

        public async Task InitializeAsync()
        {
            var service = new DataService();
            var newsfeed = await service.GetAllPublications();
            if (newsfeed != null)
            {
                NewsFeed = new ObservableCollection<Publication>(newsfeed.Reverse());
            }
            else {
                var messageDialog = new MessageDialog("Votre session a exiprée");
                await messageDialog.ShowAsync();
                MainViewModel._isLoading = false;
                _navigationService.NavigateTo("MainPage");
            }
        }


        private async Task NewPublicationEvent()
        {
            if(_navigationService != null)
                _navigationService.NavigateTo("NewPublicationPage");
            else
                throw new NotImplementedException();

        }

        private async Task LogoutEvent()
        {
            if (_navigationService != null) {
                DataService._logged = false;
                DataService._user = null;
                DataService._client = null;
                MainViewModel._isLoading = false;
                _navigationService.NavigateTo("MainPage");
            }
               
            else{
                throw new NotImplementedException();
            }
        }



        private async Task RefreshEvent()
        {
            if (_navigationService != null)
            {
                InitializeAsync();
            }
            else
            {
                throw new NotImplementedException();
            }
            
        }

        private async Task DeleteEvent()
        {
            if (_navigationService != null)
            {
                if(CanExecute())
                {
                    var ds = new DataService();
                    try
                    {
                        await ds.DeletePublication(_selectedPublication.Id);

                        var messageDialog = new MessageDialog("Supprimer");
                        await messageDialog.ShowAsync();

                        this.InitializeAsync();
                    }
                    catch (Exception e)
                    {
                        var dialog = new Windows.UI.Popups.MessageDialog(e.Message, "Erreur");

                        dialog.Commands.Add(new Windows.UI.Popups.UICommand("Ok") { Id = 0 });

                        var result = dialog.ShowAsync();
                    }
                }


            }
            else
            {
                throw new NotImplementedException();
            }
            
        }

        private async Task ModifyPublicationEvent()
        {
            if (_navigationService != null)
            {
                if (CanExecute())
                {
                    _navigationService.NavigateTo("ModifyPublicationPage", SelectedPublication);
                 
                }
            }
            else
            {
                throw new NotImplementedException();
            }
            
        }

        private bool CanExecute()
        {
            return (SelectedPublication != null);
        }

    }
}
