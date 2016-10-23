using MTA.XFingerprint;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Sample
{
    public class App : Application
    {
        public App()
        {
            // The root page of your application
            Button btFingerprint = new Button() {Text= "Start", VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };
            
            Label result = new Label() { HorizontalTextAlignment=TextAlignment.Center, IsVisible = false, Text= "Waiting for fingerprint input", BackgroundColor=Color.Gray, TextColor= Color.White, FontSize=15, FontAttributes= FontAttributes.Bold };
            btFingerprint.Clicked += (s, e) =>
            {
                result.IsVisible = true;
                XFingerprint.Current.CheckPermissions();
                if (XFingerprint.Current.IsAvailable())
                {

                    btFingerprint.IsEnabled = false;
                    result.Text = "Wait for fingerprint input";
                    result.TextColor = Color.White;

                    XFingerprint.Current.RequestFingerprint(
                   () =>
                   {
                       btFingerprint.IsEnabled = true;
                       btFingerprint.Text = "Start new Scan";
                       result.Text = "Passed";
                       result.TextColor = Color.Green;                       
                   },
                   () =>
                   {
                       result.Text = "Refused";
                       result.TextColor = Color.Red;
                   },
                   10);
                }
                else
                {
                    btFingerprint.IsEnabled = true;
                    btFingerprint.Text = "Retry";
                    result.TextColor = Color.Red;
                    result.Text = "No fingerprint available";
                }
            };
            MainPage = new ContentPage
            {

                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children = { btFingerprint , result }
                }
            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
