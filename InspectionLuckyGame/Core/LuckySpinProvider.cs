using InspectionLuckyGame.Model;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace InspectionLuckyGame.Core
{
    public class LuckySpinProvider
    {
        private readonly Random randomTooler;

        private readonly Document document;

        private readonly List<Employee> remainingEmployeeList;

        private readonly Logger logger;

        public LuckySpinProvider(IServiceProvider serviceProvider)
        {
            this.logger = serviceProvider.GetService<Logger>();
            this.document = serviceProvider.GetRequiredService<Document>();
            this.remainingEmployeeList = new List<Employee>();
            this.InitializeEligibleEmployees();
            this.randomTooler = new Random();
        }

        public IEnumerable<Winner> Spin(EPrizeUnique prizeUnique)
        {
            IEnumerable<Employee> spinEmployees;

            switch (prizeUnique)
            {
                default:
                case EPrizeUnique.ConsolationFoam:
                case EPrizeUnique.ConsolationHairDryer:
                    spinEmployees = SpinForCommon();
                    break;
                case EPrizeUnique.ThirdHeater:
                case EPrizeUnique.SecondSmartWatch:
                case EPrizeUnique.FirstRobotCleaner:
                    spinEmployees = SpinForSpecial();
                    break;
            }

            foreach (Employee employee in spinEmployees)
            {
                var winner = employee.ToWinner(prizeUnique);

                if (winner != null)
                {
                    this.document.AddWiner(winner);
                }

                yield return winner;
            }
        }

        public Employee SpinDemo()
        {
            return this.Sample();
        }

        public void Restore(IEnumerable<Winner> restoreWinners)
        {
            foreach (Winner winner in restoreWinners)
            {
                this.remainingEmployeeList.Add(new Employee()
                {
                    GenID = winner.GenID,
                    FullName = winner.FullName
                });

                this.document.Restore(winner);

                this.logger.Write($"Restore : {winner.GenID} | {winner.FullName} \n Remaining employee number : {remainingEmployeeList.Count}");
            }
        }

        public void InitializeEligibleEmployees()
        {
            {
                this.remainingEmployeeList.Clear();
            }

            if (document.EmployeeList.Count == 0)
            {
                document.Initialize();
            }

            foreach (var employee in document.EmployeeList)
            {
                this.remainingEmployeeList.Add(employee);
            }
        }

        private Employee Sample()
        {
            return this.remainingEmployeeList.OrderBy(x => randomTooler.Next()).FirstOrDefault();
        }

        private Employee SpinOnce()
        {
            var winner = this.Sample();

            if (winner != null)
            {
                this.remainingEmployeeList.Remove(winner);
                this.logger.Write($"Remove : {winner.GenID} | {winner.FullName} \n Remaining employee number : {remainingEmployeeList.Count}");
            }

            return winner;
        }

        private IEnumerable<Employee> SpinForCommon()
        {
            for (int i = 0; i < 5; i++)
            {
                yield return this.SpinOnce();
            }
        }

        private IEnumerable<Employee> SpinForSpecial()
        {
            for (int i = 0; i < 1; i++)
            {
                yield return this.SpinOnce();
            }
        }
    }
}
