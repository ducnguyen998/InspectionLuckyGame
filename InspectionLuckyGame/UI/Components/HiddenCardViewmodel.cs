using CommunityToolkit.Mvvm.ComponentModel;
using InspectionLuckyGame.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace InspectionLuckyGame.UI.Components
{
    public class HiddenCardViewmodel : ObservableObject
    {
        public event EventHandler<bool> IsShowChanged;

        public string HiddenValue
        {
			get { return hiddenValue; }
			private set { hiddenValue = value; OnPropertyChanged(); }
        }

        public bool IsShow
        {
            get { return isShow; }
            set { isShow = value; OnPropertyChanged(); OnIsShowChanged(value); }
        }

        public void SetValue(string value, bool isShow = false)
        {
            this.HiddenValue = value;
            this.IsShow = isShow;
        }

        private void OnIsShowChanged(bool isShow)
        {
            this.IsShowChanged?.Invoke(this, isShow);
        }

        private string hiddenValue;

        private bool isShow;

    }
}
