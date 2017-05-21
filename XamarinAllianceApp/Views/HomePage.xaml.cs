using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinAllianceApp.Services;

namespace XamarinAllianceApp.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        private bool authenticated = false;

        public HomePage()
        {
            InitializeComponent();
        }

        async void OnLoginButton_Clicked(object sender, EventArgs e)
        {
            if (App.Authenticator != null)
               authenticated = await App.Authenticator.Authenticate();

            // Set syncItems to true to synchronize the data on startup when offline is enabled.
            //if (authenticated == true)
            //    await RefreshItems(true, syncItems: false);
        }

        async void OnCharacterListButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CharacterListPage());
        }

        async void OnReceiveCreditButton_Clicked(object sender, EventArgs e)
        {
            var guid = await CharacterService.DefaultManager.CurrentClient.InvokeApiAsync("/api/XamarinAlliance/ReceiveCredit");
            guidLabel.Text = guid.ToString();
        }
    }
}
