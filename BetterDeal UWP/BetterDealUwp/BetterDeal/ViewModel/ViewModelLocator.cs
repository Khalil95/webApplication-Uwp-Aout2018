using BetterDeal.View;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterDeal.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<WelcomeViewModel>();
            SimpleIoc.Default.Register<NewPublicationModel>();
            SimpleIoc.Default.Register<ModifyPublicationModel>();

            NavigationService navigationPage = new NavigationService();
            SimpleIoc.Default.Register<INavigationService>(() => navigationPage);
            navigationPage.Configure("MainPage", typeof(MainPage));
            navigationPage.Configure("WelcomePage", typeof(WelcomePage));
            navigationPage.Configure("NewPublicationPage", typeof(NewPublicationPage));
            navigationPage.Configure("ModifyPublicationPage", typeof(ModifyPublicationPage));


        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public WelcomeViewModel Welcome
        {
            get
            {
                return ServiceLocator.Current.GetInstance<WelcomeViewModel>();
            }
        }

        public NewPublicationModel NewPublication
        {
            get
            {
                return ServiceLocator.Current.GetInstance<NewPublicationModel>();
            }
        }

        public ModifyPublicationModel ModifyPublication
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ModifyPublicationModel>();
            }
        }

        




    }
}
