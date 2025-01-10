using CommunityToolkit.Mvvm.ComponentModel;
using InspectionLuckyGame.Core;
using InspectionLuckyGame.Model;
using InspectionLuckyGame.UI.Components;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InspectionLuckyGame.UI.View
{
    public class SpinAdvancedViewmodel : SpinBasicViewmodel
    {
        public SpinAdvancedViewmodel(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            this.HiddenCrossword.CardFlipped += HiddenCrossword_CardFlipped;
        }

        protected async override Task DoSpin()
        {
            this.serviceProvider.GetRequiredService<Logger>().Write($"Spin for : {Prize.Name} | {Prize.Description}");

            var spinProvider = this.serviceProvider.GetRequiredService<LuckySpinProvider>();

            foreach (var winner in spinProvider.Spin(this.Prize.Unique))
            {
                for (int i = 0; i < 30; i++)
                {
                    await OnEmployeeSample(spinProvider.SpinDemo());
                }

                await OnEmployeeWin(winner);
            }
        }

        protected async override Task OnEmployeeWin(Winner winner)
        {
            this.HiddenCrossword.SetWinnerHidden(winner);

            //foreach (var hiddenCard in this.HiddenCrossword.HiddenCardList)
            //{
            //    hiddenCard.IsShow = true;

            //    await Task.Delay(650);
            //}

            while (true)
            {
                if (this.HiddenCrossword.HiddenCrosswordNameVisibility == System.Windows.Visibility.Visible)
                {
                    break;
                }

                await Task.Delay(50);
            }

            this.WinnerTable.AddWiner(winner);

            await Task.Delay(1050);
        }

        protected async override Task OnEmployeeSample(Employee employee)
        {
            await Task.Run(() =>
            {
                this.HiddenCrossword.SetWinnerVisible(employee);
            });

            await Task.Delay(100);
        }


        private void HiddenCrossword_CardFlipped(object sender, EventArgs e)
        {
            this.WinableEmployeeTable.Initialize();

            var winableEmployees = this.serviceProvider.GetRequiredService<Document>().EmployeeList.Where(x => this.IsPartialMatching(x));

            foreach (var winableEmployee in winableEmployees)
            {
                this.WinableEmployeeTable.AddWiner(new Winner()
                {
                    GenID = winableEmployee.GenID,
                    FullName = winableEmployee.FullName,
                });
            }

            OnPropertyChanged(nameof(WinableEmployeeTable));

            // Show all when winable count = 1

            if (this.WinableEmployeeTable.WinnerCollection.Count == 1)
            {
                foreach (HiddenCardViewmodel hiddenCard in HiddenCrossword.HiddenCardList)
                {
                    if (hiddenCard.IsShow == false)
                    {
                        hiddenCard.IsShow = true;
                    }
                }
            }
        }

    }
}
