using InspectionLuckyGame.Core;
using InspectionLuckyGame.Model;
using InspectionLuckyGame.Properties;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspectionLuckyGame.UI.View
{
    public class PrizeThirdHeater : SpinAdvancedViewmodel
    {
        public PrizeThirdHeater(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            this.Prize = serviceProvider.GetRequiredService<Document>().PrizeDictionary[EPrizeUnique.ThirdHeater];
            this.ImagePrize = Resources._3.ToBitmapImage();
        }
    }
}
