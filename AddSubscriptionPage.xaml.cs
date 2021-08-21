using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace FinanceApp
{
    public partial class AddSubscriptionPage : ContentPage
    {
        public AddSubscriptionPage()
        {
            InitializeComponent();
            BindingContext = new addSubscription();

            freqPicker.Items.Add("Weekly");
            freqPicker.Items.Add("Monthly");
            freqPicker.Items.Add("Yearly");
        }

        void Save_Clicked(System.Object sender, System.EventArgs e)
        {
            if(checkAllEntries())
            {
                string[] infoArray = new string[4];
                infoArray[0] = nameText.Text;
                infoArray[1] = categText.Text;
                infoArray[2] = costText.Text;
                infoArray[3] = freqPicker.SelectedItem.ToString();
                MessagingCenter.Send(this, "AddNewSub", infoArray);
                DisplayAlert("Subscription Added", "The subscription \"" + nameText.Text + "\" has been added to the homepage.", "Ok");
            }
            else
            {
                DisplayAlert("Error", "Please fill in all prompts correctly.", "Ok");
            }
        }

        bool checkAllEntries()
        {
            if(string.IsNullOrWhiteSpace(nameText.Text) || string.IsNullOrWhiteSpace(categText.Text) || string.IsNullOrWhiteSpace(costText.Text) || freqPicker.SelectedIndex == -1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    public class addSubscription
    {
        public static string SubName { get; set; }
        public static string SubCateg { get; set; }
        public static string SubCost { get; set; }
        public static string SubFreq { get; set; }
    }
}
