using CommunityToolkit.Mvvm.ComponentModel;
using InspectionLuckyGame.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace InspectionLuckyGame.UI.View
{
    public class HomeViewmodel : ObservableObject
    {
        public BitmapImage BackgroundHome { get; set; }

        public HomeViewmodel()
        {
            this.BackgroundHome = Resources.bg_home.ToBitmapImage();
        }
    }
}
