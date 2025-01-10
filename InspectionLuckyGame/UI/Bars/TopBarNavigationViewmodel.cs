using CommunityToolkit.Mvvm.ComponentModel;
using InspectionLuckyGame.Navigation.Command;
using InspectionLuckyGame.Navigation.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InspectionLuckyGame.UI.Bars
{
    public class TopBarNavigationViewmodel : ObservableObject
    {
        public ICommand PrevPageCommand { get; set; }
        public ICommand NextPageCommand { get; set; }

        private readonly IServiceProvider serviceProvider;

        public TopBarNavigationViewmodel(
            IServiceProvider serviceProvider,
            INavigationService navigationServicePrev,
            INavigationService navigationServiceNext
            )
        {
            this.serviceProvider = serviceProvider;
            this.PrevPageCommand = new CommandNavigation<ObservableObject>(navigationServicePrev);
            this.NextPageCommand = new CommandNavigation<ObservableObject>(navigationServiceNext);
        }
    }
}
