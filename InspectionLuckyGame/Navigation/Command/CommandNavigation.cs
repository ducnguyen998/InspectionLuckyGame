using InspectionLuckyGame.Navigation.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspectionLuckyGame.Navigation.Command
{
    public class CommandNavigation<TViewmodel> : CommandBase where TViewmodel : class
    {
        private readonly INavigationService navigationServicee;

        public CommandNavigation(INavigationService navigationServicee)
        {
            this.navigationServicee = navigationServicee;
        }

        public override void Execute(object parameter)
        {
            this.navigationServicee.Navigate();    
        }
    }
}
