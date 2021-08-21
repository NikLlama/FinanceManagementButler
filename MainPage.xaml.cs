using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FinanceApp
{
    public partial class MainPage : ContentPage
    {
        SubscriptionViewModel svm = new SubscriptionViewModel();

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new SubscriptionViewModel();
        }

        async void Add_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new AddSubscriptionPage());
        }

        void weeklyButton_Clicked(System.Object sender, System.EventArgs e)
        {
            totalCostText.Text = "Your Weekly Costs: Approx $" + Math.Round(svm.yearlyCost / 52, 2, MidpointRounding.AwayFromZero);
        }

        void monthlyButton_Clicked(System.Object sender, System.EventArgs e)
        {
            totalCostText.Text = "Your Monthly Costs: Approx $" + Math.Round(svm.yearlyCost / 12, 2, MidpointRounding.AwayFromZero);
        }

        void yearlyButton_Clicked(System.Object sender, System.EventArgs e)
        {
            totalCostText.Text = "Your Yearly Costs: Approx $" + Math.Round(svm.yearlyCost, 2, MidpointRounding.AwayFromZero);
        }
    }

    public class SubscriptionViewModel
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public string Cost { get; set; }
        public string Frequency { get; set; }

        public float yearlyCost;

        private ObservableCollection<SubscriptionViewModel> _subscriptions;
        public ObservableCollection<SubscriptionViewModel> Subscriptions
        {
            get
            {
                return _subscriptions ?? (_subscriptions = new ObservableCollection<SubscriptionViewModel>());
            }
        }

        public SubscriptionViewModel()
        {
            MessagingCenter.Subscribe<AddSubscriptionPage, string[]>(this, "AddNewSub", (sender, infoArray) =>
            {
                var subscription = new SubscriptionViewModel()
                {
                    Name = " " + infoArray[0].ToUpper(),
                    Category = "Category: " + infoArray[1],
                    Cost = " Cost: $" + infoArray[2],
                    Frequency = "Frequency: " + infoArray[3]
                };

                calculateCost(infoArray[3], float.Parse(infoArray[2]));

                Subscriptions.Add(subscription);
            });
        }

        private void calculateCost(string freq, float cost)
        {
            switch (freq)
            {
                case "Weekly":
                    yearlyCost += cost * 52;
                    break;
                case "Monthly":
                    yearlyCost += cost * 12;
                    break;
                case "Yearly":
                    yearlyCost += cost;
                    break;
            }
        }
    }
}
