using CommunityToolkit.Mvvm.ComponentModel;
using InspectionLuckyGame.Navigation.Command;
using InspectionLuckyGame.Navigation.Service;
using InspectionLuckyGame.UI.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InspectionLuckyGame.UI.Bars
{
    public class NavigationBarViewmodel : ObservableObject
    {
        public ICommand NavigateHome { get; set; }
        public ICommand NavigatePrizeConsolationFoam { get; set; }
        public ICommand NavigatePrizeConsolationHairDryer { get; set; }
        public ICommand NavigatePrizeThirdHeater { get; set; }
        public ICommand NavigatePrizeSecondSmartWatch { get; set; }
        public ICommand NavigatePrizeFirstRobotCleaner { get; set; }
        public ICommand NavigateWinner { get; set; }

        private readonly IServiceProvider serviceProvider;

        public NavigationBarViewmodel(
            IServiceProvider   serviceProvider,
            INavigationService navigationServiceHome,
            INavigationService navigationServicePrizeConsolationFoam,
            INavigationService navigationServicePrizeConsolationHairDryer,
            INavigationService navigationServicePrizeThirdHeater,
            INavigationService navigationServicePrizeSecondSmartWatch,
            INavigationService navigationServicePrizeFirstRobotCleaner,
            INavigationService navigationServiceWinner
            )
        {
            this.serviceProvider = serviceProvider;
            this.NavigateHome 
                = new CommandNavigation<HomeViewmodel>(navigationServiceHome);
            this.NavigatePrizeConsolationFoam 
                = new CommandNavigation<PrizeConsolationFoam>(navigationServicePrizeConsolationFoam);
            this.NavigatePrizeConsolationHairDryer 
                = new CommandNavigation<PrizeConsolationHairDryer>(navigationServicePrizeConsolationHairDryer);
            this.NavigatePrizeThirdHeater 
                = new CommandNavigation<PrizeThirdHeater>(navigationServicePrizeThirdHeater);
            this.NavigatePrizeSecondSmartWatch 
                = new CommandNavigation<PrizeThirdHeater>(navigationServicePrizeSecondSmartWatch);
            this.NavigatePrizeFirstRobotCleaner 
                = new CommandNavigation<PrizeFirstRobotCleaner>(navigationServicePrizeFirstRobotCleaner);
            this.NavigateWinner 
                = new CommandNavigation<WinnerViewmodel>(navigationServiceWinner);
        }
    }
}
