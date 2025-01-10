using InspectionLuckyGame.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InspectionLuckyGame.Core
{
    public static class Helper
    {
        private const string emmloyeeCsvLocation = @"Resources\Employees.csv";
        
        public static string GetDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any())
            {
                return attributes.First().Description;
            }

            return value.ToString();
        }

        public static IEnumerable<Employee> GetEmployees()
        {
            var lines = File.ReadAllLines(emmloyeeCsvLocation);

            foreach (var line in lines)
            {
                var arr = line.Split(',');

                if (arr[0] != "STT")
                {
                    yield return new Employee()
                    {
                        FullName = arr[1],
                        GenID = arr[2],
                    };
                }
            }
        }

        public static ObservableCollection<Employee> GetEmployeeCollection()
        {
            var employeeCollection = new ObservableCollection<Employee>();

            var employees = GetEmployees();

            foreach (Employee employee in employees)
            {
                employeeCollection.Add(employee);
            }

            return employeeCollection;
        }

        public static Dictionary<EPrizeUnique, Prize> BuildPrizeList()
        {
            var prizeList = new Dictionary<EPrizeUnique, Prize>();

            foreach (EPrizeUnique unique in Enum.GetValues(typeof(EPrizeUnique)))
            {
                prizeList.Add(unique, Prize.Create(unique));
            }

            return prizeList;
        }
    
        public static Dictionary<EPrizeUnique, List<Employee>> BuildWinnerDictionary()
        {
            var winnerDict = new Dictionary<EPrizeUnique, List<Employee>>();

            foreach (EPrizeUnique item in Enum.GetValues(typeof(EPrizeUnique)))
            {
                winnerDict.Add(item, new List<Employee>());
            }

            return winnerDict;
        }

        public static EPrizeLevel GetPrizeLevel(this EPrizeUnique prizeUnique)
        {
            switch (prizeUnique)
            {
                case EPrizeUnique.ConsolationFoam:
                case EPrizeUnique.ConsolationHairDryer:
                    return EPrizeLevel.Consolation;
                case EPrizeUnique.ThirdHeater:
                    return EPrizeLevel.Third;
                case EPrizeUnique.SecondSmartWatch:
                    return EPrizeLevel.Second;
                case EPrizeUnique.FirstRobotCleaner:
                    return EPrizeLevel.First;
                default:
                    return EPrizeLevel.Spare;
            }
        }

        public static Winner ToWinner(this Employee employee, EPrizeUnique prizeUnique)
        {
            return new Winner()
            {
                GenID = employee.GenID,
                FullName = employee.FullName,
                Prize = Prize.Create(prizeUnique),
            };
        }
    }
}
