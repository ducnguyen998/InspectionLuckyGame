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
    public class PrizeConsolationFoam : SpinBasicViewmodel
    {
        public PrizeConsolationFoam(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            this.Prize = serviceProvider.GetRequiredService<Document>().PrizeDictionary[EPrizeUnique.ConsolationFoam];
            this.ImagePrize = Resources._4_2.ToBitmapImage();
        }
    }
}
