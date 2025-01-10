using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspectionLuckyGame.Navigation.Store
{
    public class StoreNavigationBase
    {
        public event Action CurrentViewmodelChanged;

        public ObservableObject CurrentViewmodel
        {
            get
            {
                return this.currentViewmodel;
            }
            set
            {
                if (this.currentViewmodel != value)
                {
                    this.currentViewmodel = value;
                    this.OnCurrentViewmodelChanged();
                }
            }
        }

        private ObservableObject currentViewmodel;

        private void OnCurrentViewmodelChanged()
        {
            this.CurrentViewmodelChanged?.Invoke();
        }
    }
}
