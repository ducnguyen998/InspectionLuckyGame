using CommunityToolkit.Mvvm.ComponentModel;
using InspectionLuckyGame.Core;
using InspectionLuckyGame.Navigation.Define;
using InspectionLuckyGame.Navigation.Service;
using InspectionLuckyGame.Navigation.Store;
using InspectionLuckyGame.UI.Bars;
using InspectionLuckyGame.UI.View;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace InspectionLuckyGame
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();

            #region ServiceProvider

            services.AddSingleton(s => s);

            #endregion

            #region Core

            services.AddSingleton<Logger>();
            services.AddSingleton<Document>();
            services.AddSingleton<LuckySpinProvider>();

            #endregion

            #region Bars

            #endregion

            #region Stores

            services.AddSingleton<StoreNavigationMainWindow>();

            #endregion

            #region Views

            services.AddSingleton<HomeViewmodel>();
            services.AddSingleton<PrizeConsolationFoam>();
            services.AddSingleton<PrizeConsolationHairDryer>();
            services.AddSingleton<PrizeThirdHeater>();
            services.AddSingleton<PrizeSecondSmartWatch>();
            services.AddSingleton<PrizeFirstRobotCleaner>();
            services.AddSingleton<WinnerViewmodel>();

            #endregion

            #region Initialize services

            services.AddSingleton(s => CreateServiceNavigation(s, EView.Home));

            #endregion

            #region Main window

            services.AddSingleton<MainWindowViewmodel>();
            services.AddSingleton(s => CreateMainWindow(s));

            #endregion

            serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            this.serviceProvider.GetRequiredService<Document>().Initialize();
            this.serviceProvider.GetRequiredService<INavigationService>().Navigate();
            this.serviceProvider.GetRequiredService<MainWindow>().Show();
        }

        private MainWindow CreateMainWindow(IServiceProvider serviceProvider)
        {
            return new MainWindow()
            {
                DataContext = serviceProvider.GetRequiredService<MainWindowViewmodel>()
            };
        }

        private ObservableObject CreateTopBarViewmodel(IServiceProvider serviceProvider, EView eView)
        {
            return new TopBarViewmodel(CreateTopBarNavigationViewmodel(serviceProvider, eView));
        }

        private ObservableObject CreateNavigationBarMainWindowViewmodel(IServiceProvider serviceProvider)
        {
            return new NavigationBarViewmodel(
                serviceProvider,
                CreateServiceNavigation(serviceProvider, EView.Home),
                CreateServiceNavigation(serviceProvider, EView.ConsolationFoam),
                CreateServiceNavigation(serviceProvider, EView.ConsolationHairDryer),
                CreateServiceNavigation(serviceProvider, EView.ThirdHeater),
                CreateServiceNavigation(serviceProvider, EView.SecondSmartWatch),
                CreateServiceNavigation(serviceProvider, EView.FirstRobotCleaner),
                CreateServiceNavigation(serviceProvider, EView.Winner)
                );
        }

        private TopBarNavigationViewmodel CreateTopBarNavigationViewmodel(IServiceProvider serviceProvider, EView eView)
        {
            switch (eView)
            {
                default:
                case EView.Home:
                    return new TopBarNavigationViewmodel(
                        serviceProvider,
                        CreateServiceNavigation(serviceProvider, EView.Home),
                        CreateServiceNavigation(serviceProvider, EView.ConsolationFoam)
                        );
                case EView.ConsolationFoam:
                    return new TopBarNavigationViewmodel(
                        serviceProvider,
                        CreateServiceNavigation(serviceProvider, EView.Home),
                        CreateServiceNavigation(serviceProvider, EView.ConsolationHairDryer)
                        );
                case EView.ConsolationHairDryer:
                    return new TopBarNavigationViewmodel(
                        serviceProvider,
                        CreateServiceNavigation(serviceProvider, EView.ConsolationFoam),
                        CreateServiceNavigation(serviceProvider, EView.ThirdHeater)
                        );
                case EView.ThirdHeater:
                    return new TopBarNavigationViewmodel(
                        serviceProvider,
                        CreateServiceNavigation(serviceProvider, EView.ConsolationHairDryer),
                        CreateServiceNavigation(serviceProvider, EView.SecondSmartWatch)
                        );
                case EView.SecondSmartWatch:
                    return new TopBarNavigationViewmodel(
                        serviceProvider,
                        CreateServiceNavigation(serviceProvider, EView.ThirdHeater),
                        CreateServiceNavigation(serviceProvider, EView.FirstRobotCleaner)
                        );
                case EView.FirstRobotCleaner:
                    return new TopBarNavigationViewmodel(
                        serviceProvider,
                        CreateServiceNavigation(serviceProvider, EView.SecondSmartWatch),
                        CreateServiceNavigation(serviceProvider, EView.Winner)
                        );
                case EView.Winner:
                    return new TopBarNavigationViewmodel(
                        serviceProvider,
                        CreateServiceNavigation(serviceProvider, EView.FirstRobotCleaner),
                        CreateServiceNavigation(serviceProvider, EView.Winner)
                        );
            }
        }

        private INavigationService CreateServiceNavigation(IServiceProvider serviceProvider, EView eView)
        {
            return new NavigationMainWindowService(
                      serviceProvider.GetRequiredService<StoreNavigationMainWindow>(),
                () => CreateTopBarViewmodel(serviceProvider, eView),
                () => CreateNavigationBarMainWindowViewmodel(serviceProvider),
                () => CreateLayoutViewmodel(serviceProvider, eView)
                );
        }

        private ObservableObject CreateLayoutViewmodel(IServiceProvider serviceProvider, EView eView)
        {
            switch (eView)
            {
                default:
                case EView.Home:
                    return serviceProvider.GetRequiredService<HomeViewmodel>();
                case EView.ConsolationFoam:
                    return serviceProvider.GetRequiredService<PrizeConsolationFoam>();
                case EView.ConsolationHairDryer:
                    return serviceProvider.GetRequiredService<PrizeConsolationHairDryer>();
                case EView.ThirdHeater:
                    return serviceProvider.GetRequiredService<PrizeThirdHeater>();
                case EView.SecondSmartWatch:
                    return serviceProvider.GetRequiredService<PrizeSecondSmartWatch>();
                case EView.FirstRobotCleaner:
                    return serviceProvider.GetRequiredService<PrizeFirstRobotCleaner>();
                case EView.Winner:
                    return serviceProvider.GetRequiredService<WinnerViewmodel>();
            }
        }

    }
}
