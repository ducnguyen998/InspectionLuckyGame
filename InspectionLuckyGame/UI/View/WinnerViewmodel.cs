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

namespace InspectionLuckyGame.UI.View
{
    public class WinnerViewmodel : ObservableObject
    {
        private readonly Document document;

        public ObservableCollection<Winner> WinnerCollection
        {
            get
            {
                return document.WinnerCollection;
            }
        }

        public ObservableCollection<Employee> RemainingEmployeeList
        {
            get
            {
                return document.RemainingEmployeeList;
            }
        }

        public WinnerViewmodel(IServiceProvider serviceProvider)
        {
            this.document = serviceProvider.GetRequiredService<Document>();
        }
    }
}
