using InspectionLuckyGame.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace InspectionLuckyGame.Converters
{
    public class PrizeUniqueToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is EPrizeUnique prizeUnique)
            {
                switch (prizeUnique)
                {
                    case EPrizeUnique.ConsolationFoam:
                        return Brushes.YellowGreen;
                    case EPrizeUnique.ConsolationHairDryer:
                        return Brushes.GreenYellow;
                    case EPrizeUnique.ThirdHeater:
                        return Brushes.Orchid;
                    case EPrizeUnique.SecondSmartWatch:
                        return Brushes.LavenderBlush;
                    case EPrizeUnique.FirstRobotCleaner:
                        return Brushes.MistyRose;
                    default:
                        return Brushes.Transparent;
                }
            }

            return Brushes.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
