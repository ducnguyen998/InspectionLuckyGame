using CommunityToolkit.Mvvm.ComponentModel;
using InspectionLuckyGame.Navigation.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspectionLuckyGame
{
    public class MainWindowViewmodel : ObservableObject
    {
        private readonly StoreNavigationBase storeNavigation;

        public MainWindowViewmodel(StoreNavigationMainWindow storeNavigation)
        {
            this.storeNavigation = storeNavigation;
            this.storeNavigation.CurrentViewmodelChanged += () => OnPropertyChanged(nameof(CurrentViewmodel));
        }

        public ObservableObject CurrentViewmodel => storeNavigation.CurrentViewmodel;
    }
}
