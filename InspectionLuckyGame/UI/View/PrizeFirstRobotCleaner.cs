using InspectionLuckyGame.Core;
using InspectionLuckyGame.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspectionLuckyGame.UI.View
{
    public class PrizeFirstRobotCleaner : SpinAdvancedViewmodel
    {
        public PrizeFirstRobotCleaner(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            this.Prize = serviceProvider.GetRequiredService<Document>().PrizeDictionary[EPrizeUnique.FirstRobotCleaner];
        }
    }
}
