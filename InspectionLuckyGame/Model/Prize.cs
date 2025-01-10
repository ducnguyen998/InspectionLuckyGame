using InspectionLuckyGame.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InspectionLuckyGame.Model
{
    public enum EPrizeLevel
    {
        [Description("Giải Khuyến Khích")]
        Consolation,
        [Description("Giải Ba")]
        Third,
        [Description("Giải Nhì")]
        Second,
        [Description("Giải Nhất")]
        First,
        [Description("Không có giải")]
        Spare
    }

    public enum EPrizeUnique
    {
        [Description("Gối cao su Memory Foam")]
        ConsolationFoam,
        [Description("Máy sấy tóc Hyperboost")]
        ConsolationHairDryer,
        [Description("Quạt sưởi Apolo")]
        ThirdHeater,
        [Description("Đồng hồ thông minh Xiaomi")]
        SecondSmartWatch,
        [Description("Robot hút bụi Xiaomi")]
        FirstRobotCleaner
    }

    public class Prize
    {
        public static Prize Create(EPrizeUnique unique)
        {
            return new Prize(
                unique,
                unique.GetPrizeLevel(), 
                unique.GetDescription()
                );
        }

        Prize(EPrizeUnique unique, EPrizeLevel level, string name)
        {
            Unique = unique;
            Level = level;
            Description = level.GetDescription();
            Name = name;
        }

        public EPrizeLevel Level { get; set; }

        public EPrizeUnique Unique { get; set; }

        public string Name { get; private set; }

        public string Description { get; private set; }
    }
}
