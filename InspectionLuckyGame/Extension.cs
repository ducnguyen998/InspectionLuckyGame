using InspectionLuckyGame.Model;
using InspectionLuckyGame.UI.View;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace InspectionLuckyGame
{
    public static class Extension
    {
        public static bool IsPartialMatching(this SpinBasicViewmodel spinner, Employee employee)
        {
            var mask = spinner.HiddenCrossword.HiddenCardList
                .Select(x => x.IsShow).ToList();

            var charList = employee.GenID.ToCharArray().ToList();

            var currList = spinner.HiddenCrossword.HiddenCardList;

            if (currList.Count != charList.Count())
            {
                return true;
            }

            var ret = true;


            for (int i = 0; i < currList.Count; i++)
            {
                if (mask[i])
                {
                    if (currList[i].HiddenValue != charList[i].ToString())
                    {
                        ret = false;
                        break;
                    }
                }
            }

            return ret;
        }

        public static BitmapImage ToBitmapImage(this Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }
    }
}
