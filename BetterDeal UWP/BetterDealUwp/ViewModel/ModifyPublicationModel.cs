using BetterDeal.DataAccessObject;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml.Navigation;

namespace BetterDeal.ViewModel
{
    public class ModifyPublicationModel : ViewModelBase
    {
        private INavigationService _navigationService = null;
        public Publication SelectedPublication { get; set; }
        
        private ICommand _buttonBack;
        public ICommand ButtonBack
        {
            get
            {
                if (_buttonBack == null)
                    _buttonBack = new RelayCommand(async () => await BackEvent());
                return _buttonBack;
            }
            set { _buttonBack = value; }
        }

        private ICommand _buttonSend;
        public ICommand ButtonSend
        {
            get
            {
                if (_buttonSend == null)
                    _buttonSend = new RelayCommand(async () => await SendNewPublicationEvent());
                return _buttonSend;
            }
            set { _buttonSend = value; }
        }

        public ModifyPublicationModel(INavigationService navigationService = null)
        {
            _navigationService = navigationService;
        }

        private string _descriptionValue;
        private string _titleValue;

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            SelectedPublication = (Publication)e.Parameter;
            _titleValue = SelectedPublication.Title;
            _descriptionValue = SelectedPublication.Description;
        }

        // private string _descriptionValue = SelectedPublication.Title;
        public string TitleValue
        {
            get { return _titleValue; }
            set
            {
                _titleValue = value;
                RaisePropertyChanged("TitleValue");
            }
        }

        public string DescriptionValue
        {
            get { return _descriptionValue; }
            set
            {
                _descriptionValue = value;
                RaisePropertyChanged("DescriptionValue");
            }
        }



        private async Task BackEvent()
        {
            if (_navigationService != null)
                _navigationService.NavigateTo("WelcomePage");
            else
                throw new NotImplementedException();
        }


    

        private async Task SendNewPublicationEvent()
        {
            if (DescriptionValidation() && TitleValidation())
            {
                try
                {
                    var ds = new DataService();
                    try
                    {

                        await ds.ModifyPubliction(SelectedPublication.Id, _titleValue, _descriptionValue);

                        var messageDialog = new MessageDialog("Success");
                        await messageDialog.ShowAsync();

                        _navigationService.NavigateTo("WelcomePage");
                    }
                    catch (Exception e)
                    {
                        var dialog = new Windows.UI.Popups.MessageDialog(e.Message, "Erreur");

                        dialog.Commands.Add(new Windows.UI.Popups.UICommand("Ok") { Id = 0 });

                        var result = dialog.ShowAsync();
                    }
                }catch(Exception e)
                {
                    var dialog = new Windows.UI.Popups.MessageDialog(e.Message, "Erreur");

                    dialog.Commands.Add(new Windows.UI.Popups.UICommand("Ok") { Id = 0 });

                    var result = dialog.ShowAsync();
                }

            }
            else
                throw new NotImplementedException();
        }
       
        private bool TitleValidation()
        {
            if (TitleValue != null)
            {

                return true;
            }
            return false;
        }

        private bool DescriptionValidation()
        {
            if (DescriptionValue != null)
            {

                return true;
            }
            return false;
        }

    }
}
