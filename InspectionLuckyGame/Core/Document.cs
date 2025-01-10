using InspectionLuckyGame.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InspectionLuckyGame.Core
{
    public class Document
    {
        public List<Employee> EmployeeList { get; set; }

        public Dictionary<EPrizeUnique, Prize> PrizeDictionary { get; set; }

        public Dictionary<EPrizeUnique, List<Employee>> WinnerDictionary { get; set; }

        public ObservableCollection<Winner> WinnerCollection { get; private set; }

        public ObservableCollection<Employee> RemainingEmployeeList { get; private set; }

        public void AddWiner(Winner winner)
        {
            this.WinnerDictionary[winner.Prize.Unique].Add(winner);
            ///
            this.WinnerCollection.Add(winner);
            this.RemainingEmployeeList.Remove(this.RemainingEmployeeList.Where(x => x.GenID == winner.GenID).FirstOrDefault());
        }

        public void Restore(Winner restoreWinner)
        {
            this.RemainingEmployeeList.Add(new Employee()
            {
                GenID = restoreWinner.GenID,
                FullName = restoreWinner.FullName
            });

            var removeWinner = this.WinnerCollection.Where(x => x.GenID == restoreWinner.GenID).FirstOrDefault();

            if (removeWinner != null)
            {
                this.WinnerCollection.Remove(removeWinner);
                this.WinnerDictionary[restoreWinner.Prize.Unique].Remove(removeWinner);
            }
        }

        public void Initialize()
        {
            this.EmployeeList = Helper.GetEmployees().ToList();
            this.PrizeDictionary = Helper.BuildPrizeList();
            this.WinnerDictionary = Helper.BuildWinnerDictionary();
            this.WinnerCollection = new ObservableCollection<Winner>();
            this.RemainingEmployeeList = Helper.GetEmployeeCollection();
        }
    }
}
