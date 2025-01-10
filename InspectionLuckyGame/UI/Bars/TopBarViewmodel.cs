using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspectionLuckyGame.UI.Bars
{
    public class TopBarViewmodel : ObservableObject
    {
		public bool IsNavigationBarOpen
		{
			get 
			{ 
				return isNavigationBarOpen; 
			}
			set 
			{ 
				isNavigationBarOpen = value;
				OnPropertyChanged();
			}
		}

		public TopBarNavigationViewmodel Navigation
		{
			get 
			{ 
				return navigation; 
			}
			set 
			{ 
				navigation = value;
				OnPropertyChanged();
            }
		}


		private bool isNavigationBarOpen;

        private TopBarNavigationViewmodel navigation;

        public TopBarViewmodel(TopBarNavigationViewmodel navigation)
        {
            this.navigation = navigation;
        }
    }
}
