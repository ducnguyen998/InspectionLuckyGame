using CommunityToolkit.Mvvm.ComponentModel;
using InspectionLuckyGame.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InspectionLuckyGame.UI.Components
{
    public class HiddenCrosswordViewmodel : ObservableObject
    {
        public ObservableCollection<HiddenCardViewmodel> HiddenCardList { get; set; }

        public event EventHandler CardFlipped;

        public HiddenCrosswordViewmodel()
        {
            this.HiddenCardList = new ObservableCollection<HiddenCardViewmodel>();
            this.HiddenCardList.CollectionChanged += HiddenCardList_CollectionChanged;
            this.Initialize();
        }

        public Visibility HiddenCrosswordNameVisibility
        {
            get { return nameVisibility; }
            set { nameVisibility = value; OnPropertyChanged(); }
        }

        public Employee CurrentEmployee
        {
            get { return currentEmployee; }
            set { currentEmployee = value; OnPropertyChanged(); }
        }

        public void Initialize()
        {
            this.HiddenCardList.Clear();

            for (int i = 0; i < 8; i++)
            {
                this.HiddenCardList.Add(new HiddenCardViewmodel());
                this.HiddenCardList.Last().SetValue("");
            }
        }

        public void SetWinnerHidden(Employee employee)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                this.setString(employee.GenID, false);
            });

            {
                this.CurrentEmployee = employee;
            }
        }

        public void SetWinnerVisible(Employee employee)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                this.setString(employee.GenID, true);
            });

            {
                this.CurrentEmployee = employee;
            }
        }

        private void setString(string genID, bool isShow)
        {
            this.HiddenCardList.Clear();

            var digitArray = genID.ToCharArray();

            foreach (var digit in digitArray)
            {
                this.HiddenCardList.Add(new HiddenCardViewmodel());
                this.HiddenCardList.Last().SetValue(digit.ToString(), isShow);
            }
        }

        private void HiddenCardList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    foreach (HiddenCardViewmodel hiddenCard in e.NewItems)
                    {
                        hiddenCard.IsShowChanged += OnIsShowChanged;
                    }
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:

                    if (e.OldItems == null) break;

                    foreach (HiddenCardViewmodel hiddenCard in e.OldItems)
                    {
                        hiddenCard.IsShowChanged -= OnIsShowChanged;
                    }
                    break;
            }
        }

        private void OnIsShowChanged(object sender, bool e)
        {
            this.HiddenCrosswordNameVisibility = this.GetHiddenCrosswordNameVisibility();
            this.OnCardFlipped();
        }

        private void OnCardFlipped()
        {
            this.CardFlipped?.Invoke(this, null);
        }

        private Visibility GetHiddenCrosswordNameVisibility()
        {
            if (this.HiddenCardList.Where(x => x.IsShow).Count() == this.HiddenCardList.Count())
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Hidden;
            }
        }

        private Visibility nameVisibility;

        private Employee currentEmployee;

    }
}
