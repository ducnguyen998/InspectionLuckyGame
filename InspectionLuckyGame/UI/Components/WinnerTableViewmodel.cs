using CommunityToolkit.Mvvm.ComponentModel;
using InspectionLuckyGame.Core;
using InspectionLuckyGame.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspectionLuckyGame.UI.Components
{
    public class WinnerTableViewmodel : ObservableObject
    {
        public ObservableCollection<Winner> WinnerCollection { get; set; }

        public WinnerTableViewmodel()
        {
            this.WinnerCollection = new ObservableCollection<Winner>();
        }

        public void Initialize()
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                this.WinnerCollection.Clear();
            });
        }

        public void AddWiner(Winner winner)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                this.WinnerCollection.Add(winner);
            });
        }
    }
}
