using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TaxDedutions.DB;
using TaxDedutions.ViewModels;
using Xamarin.Forms;

namespace TaxDedutions.Views
{
    public partial class Family : ContentPage
    {
        private RecordDatabase db;
        public Family()
        {
            InitializeComponent();
            db = new RecordDatabase();
            InitUI();
        }

        Entry usernameEntry;
        Entry passwordEntry;
        ActivityIndicator indicator;
        Switch rememberSwitchAndroid;
        //  ExtendedSwitch rememberSwitchiOS;

        private void InitUI()
        {
           // var logo = new Image { Source = ImageSource.FromFile("splash.png") };

            var lblWelcome = new Label { Text = "Bienvenido a FamilyReport", FontSize = 16, TextColor = Color.White, HorizontalOptions = LayoutOptions.Center };

            var lblFirst = new Label { Text = "Usted necesita crear su cuenta.", FontSize = 12, TextColor = Color.White, HorizontalOptions = LayoutOptions.Center };

            usernameEntry = new Entry { Placeholder = "Email", StyleId = "UserId" };
           // usernameEntry.PlaceholderColor = Color.FromHex("#58666E");
            usernameEntry.TextColor = Color.White;
            // usernameEntry.SetBinding(Entry.TextProperty, "Username");
            usernameEntry.FontSize = 16;

            passwordEntry = new Entry { IsPassword = true, Placeholder = "Contraseña", StyleId = "PassId" };
            //passwordEntry.PlaceholderColor = Color.FromHex("#58666E");
            passwordEntry.TextColor = Color.White;
            //passwordEntry.SetBinding(Entry.TextProperty, "Password");
            passwordEntry.FontSize = 16;


            var loginButton = new Button { Text = "LOGIN", TextColor = Color.White };
            loginButton.HorizontalOptions = LayoutOptions.FillAndExpand;
            loginButton.BackgroundColor = Color.FromHex("#A4287D");

            loginButton.Clicked += LoginButton_Clicked;

            var newButton = new Button { Text = "CREAR", TextColor = Color.White };
            newButton.HorizontalOptions = LayoutOptions.FillAndExpand;
            newButton.BackgroundColor = Color.FromHex("#404040");

            newButton.Clicked += NewButton_Clicked;


            BoxView spacingBox = new BoxView();
            spacingBox.HeightRequest = 30;
            spacingBox.BackgroundColor = Color.Transparent;

            BoxView spacingBox2 = new BoxView();
            spacingBox2.HeightRequest = 30;
            spacingBox2.BackgroundColor = Color.Transparent;

            indicator = new ActivityIndicator();
            indicator.HorizontalOptions = LayoutOptions.FillAndExpand;
            indicator.VerticalOptions = LayoutOptions.CenterAndExpand;
            SetPropertiesIndicator(false);


            if (Device.OS == TargetPlatform.iOS)
            {
                if (CheckExistFamily())
                {
                    ContentLayout.VerticalOptions = LayoutOptions.StartAndExpand;
                    ContentLayout.Padding = new Thickness(30);
                    // ContentLayout.Children.Add(logo);
                    ContentLayout.Children.Add(spacingBox);
                    ContentLayout.Children.Add(lblWelcome);
                    ContentLayout.Children.Add(spacingBox2);
                    ContentLayout.Children.Add(usernameEntry);
                    ContentLayout.Children.Add(passwordEntry);
                    ContentLayout.Children.Add(loginButton);
                    // ContentLayout.Children.Add(rememberLayout);
                    ContentLayout.Children.Add(indicator);
                }
            }
            else
            {
             

                if (CheckExistFamily())
                {
                    ContentLayout.VerticalOptions = LayoutOptions.Center;
                    ContentLayout.Padding = new Thickness(30);
                    // ContentLayout.Children.Add(logo);
                    ContentLayout.Children.Add(spacingBox);
                    ContentLayout.Children.Add(lblWelcome);
                    ContentLayout.Children.Add(spacingBox2);
                    ContentLayout.Children.Add(usernameEntry);
                    ContentLayout.Children.Add(passwordEntry);
                    ContentLayout.Children.Add(loginButton);
                    // ContentLayout.Children.Add(rememberLayout);
                   
                }
                else
                {
                    ContentLayout.VerticalOptions = LayoutOptions.StartAndExpand;
                    ContentLayout.Padding = new Thickness(30);
                    // ContentLayout.Children.Add(logo);
                    ContentLayout.Children.Add(spacingBox);
                    ContentLayout.Children.Add(lblWelcome);
                    ContentLayout.Children.Add(spacingBox2);
                    ContentLayout.Children.Add(lblFirst);
                    ContentLayout.Children.Add(usernameEntry);
                    ContentLayout.Children.Add(passwordEntry);
                    ContentLayout.Children.Add(newButton);
                }

                ContentLayout.Children.Add(indicator);
            }


          
        }

        private bool CheckExistFamily()
        {
           var family =  db.GetFamily();
            return family != null;
        }

        private void SetPropertiesIndicator(bool value)
        {
            indicator.IsVisible = indicator.IsRunning = value;
        }


        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            if (indicator.IsRunning == true) return;

            try
            {
                SetPropertiesIndicator(false);

            }
            catch
            {
                await DisplayAlert("Error", "Error cargando la cuenta. Intente de nuevo por favor.", "SI");
                SetPropertiesIndicator(false);
            }
        }

        private async void NewButton_Clicked(object sender, EventArgs e)
        {
            if (indicator.IsRunning == true) return;

            try
            {
                SetPropertiesIndicator(true);
                //Call service y save cuenta
                if (string.IsNullOrEmpty(usernameEntry.Text) ||
                 string.IsNullOrEmpty(passwordEntry.Text))
                {
                    await DisplayAlert("Crear cuenta", "Su email y contraseña son requeridos.", "SI");
                    SetPropertiesIndicator(false);
                    return;
                }

                if (!Email())
                {
                    await DisplayAlert("Crear cuenta", "Su email no es correcto.", "OK");
                    SetPropertiesIndicator(false);
                    return;
                }


            }
            catch
            {
                await DisplayAlert("Error", "Error salvando la cuenta. Intente de nuevo por favor.", "SI");
                SetPropertiesIndicator(false);
            }
        }

        private bool Email()
        {
            string email = usernameEntry.Text;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success)
                return true;
            else
                return false;
        }

    }
}
