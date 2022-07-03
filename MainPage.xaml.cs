namespace Phoneword;

public partial class MainPage : ContentPage
{
	public MainPage()
    {
        InitializeComponent();
    }

    string translatedNumber;

    private void OnTranslate(object sender, EventArgs e)
    {
        string enteredNumber = PhoneNumberText.Text;
        translatedNumber = Core.PhonewordTranslator.ToNumber(enteredNumber);

        if (!string.IsNullOrEmpty(translatedNumber))
        {
            CallButton.IsEnabled = true;
            CallButton.Text = "Call " + translatedNumber;
        }
        else
        {
            CallButton.IsEnabled = false;
            CallButton.Text = "Call";
        }
    }

    async void OnCall(object sender, System.EventArgs e)
    {
        if (await this.DisplayAlert(
            "Dial a number",
            "Would you like to call" + translatedNumber + "?",
            "Yes",
            "No"))
        {
            try
            {
                PhoneDialer.Open(translatedNumber);
            }
            catch (ArgumentException)
            {
                await DisplayAlert("Unable to dial", "Phone number was not valid", "OK");
            }
            catch (FeatureNotSupportedException)
            {
                await DisplayAlert("Unable to dial", "Phone dialing not supported.", "OK");
            }
            catch (Exception)
            {
                await DisplayAlert("Unable to dial", "Phone dialing failed.", "OK");
            }
        }
    }
}


