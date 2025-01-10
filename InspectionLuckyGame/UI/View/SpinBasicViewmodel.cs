using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using InspectionLuckyGame.Core;
using InspectionLuckyGame.Model;
using InspectionLuckyGame.Properties;
using InspectionLuckyGame.UI.Components;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace InspectionLuckyGame.UI.View
{
    public class SpinBasicViewmodel : ObservableObject
    {
        protected readonly IServiceProvider serviceProvider;

        public SpinBasicViewmodel(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.WinnerTable = new WinnerTableViewmodel();
            this.WinableEmployeeTable = new WinnerTableViewmodel();
            this.HiddenCrossword = new HiddenCrosswordViewmodel();
            this.SpinCommand = new AsyncRelayCommand(DoSpin);
            this.ResetPrizeCommand = new RelayCommand(ResetPrize);
            this.BackgroundPrize = Resources.bg_prize.ToBitmapImage();
        }

        public ICommand SpinCommand { get; set; }
        public ICommand ResetPrizeCommand { get; set; }


        public HiddenCrosswordViewmodel HiddenCrossword { get; set; }

        public WinnerTableViewmodel WinnerTable { get; set; }

        public WinnerTableViewmodel WinableEmployeeTable { get; set; }

        public Prize Prize { get; set; }

        public BitmapImage BackgroundPrize { get; set; }

        protected virtual async Task DoSpin()
        {
            this.serviceProvider.GetRequiredService<Logger>().Write($"Spin for : {Prize.Name} | {Prize.Description}");

            var spinProvider = this.serviceProvider.GetRequiredService<LuckySpinProvider>();

            foreach (var winner in spinProvider.Spin(this.Prize.Unique))
            {
                for (int i = 0; i < 50; i++)
                {
                    await OnEmployeeSample(spinProvider.SpinDemo());
                }

                await OnEmployeeWin(winner);
            }
        }

        protected virtual async Task OnEmployeeWin(Winner winner)
        {
            this.HiddenCrossword.SetWinnerHidden(winner);

            await Task.Delay(1000);

            foreach (var hiddenCard in this.HiddenCrossword.HiddenCardList)
            {
                hiddenCard.IsShow = true;

                await Task.Delay(650);
            }

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
        protected virtual async Task OnEmployeeSample(Employee employee)
        {
            await Task.Run(() =>
            {
                this.HiddenCrossword.SetWinnerVisible(employee);
            });

            await Task.Delay(50);
        }
        protected virtual void ResetPrize()
        {
            this.serviceProvider.GetRequiredService<Logger>().Write($"Restore spin for : {Prize.Name} | {Prize.Description}");

            this.serviceProvider.GetRequiredService<LuckySpinProvider>().Restore(WinnerTable.WinnerCollection);
            this.WinnerTable.Initialize();
            this.HiddenCrossword.Initialize();
        }

    }
}
