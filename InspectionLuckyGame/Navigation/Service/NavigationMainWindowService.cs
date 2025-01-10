using CommunityToolkit.Mvvm.ComponentModel;
using InspectionLuckyGame.Navigation.Layout;
using InspectionLuckyGame.Navigation.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace InspectionLuckyGame.Navigation.Service
{
    public class NavigationMainWindowService : INavigationService
    {
        private readonly StoreNavigationBase storeNavigation;
        private readonly Func<ObservableObject> createTopBar;
        private readonly Func<ObservableObject> createNavigationBar;
        private readonly Func<ObservableObject> createMainContent;

        public NavigationMainWindowService(
            StoreNavigationBase storeNavigation,
            Func<ObservableObject> createTopBar,
            Func<ObservableObject> createNavigationBar,
            Func<ObservableObject> createMainContent)
        {
            this.storeNavigation = storeNavigation;
            this.createTopBar = createTopBar;
            this.createNavigationBar = createNavigationBar;
            this.createMainContent = createMainContent;
        }

        public void Navigate()
        {
            this.storeNavigation.CurrentViewmodel = new MainWindowLayoutViewmodel(
                createTopBar(), createNavigationBar(), createMainContent()
                );
        }
    }
}
