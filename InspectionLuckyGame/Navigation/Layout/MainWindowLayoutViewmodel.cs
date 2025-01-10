using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspectionLuckyGame.Navigation.Layout
{
    public class MainWindowLayoutViewmodel : ObservableObject
    {
        public MainWindowLayoutViewmodel(
            ObservableObject topBarPresenter, 
            ObservableObject navigationBarPresenter, 
            ObservableObject mainContentPresenter
            )
        {
            TopBar = topBarPresenter;
            NavigationBar = navigationBarPresenter;
            MainContentPresenter = mainContentPresenter;
        }

        public ObservableObject TopBar { get; }
        public ObservableObject NavigationBar { get; }
        public ObservableObject MainContentPresenter { get; }
    }
}
