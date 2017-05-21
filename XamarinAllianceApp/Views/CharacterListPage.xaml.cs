using System;
using Xamarin.Forms;
using XamarinAllianceApp.Models;
using XamarinAllianceApp.Services;
using XamarinAllianceApp.ViewModels;

namespace XamarinAllianceApp.Views
{
    public partial class CharacterListPage : ContentPage
    {
        private CharacterListViewModel viewModel;

        public CharacterListPage()
        {
            InitializeComponent();

            BindingContext = viewModel = new CharacterListViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var character = e.SelectedItem as Character;

            if (character == null)
            {
                return; //ItemSelected is called on deselection, which results in SelectedItem being set to null
            }

            //await DisplayAlert("Item Selected", character.Name, "Ok");
            await Navigation.PushAsync(new CharacterDetailPage(new CharacterDetailViewModel(character)));

            // Deselect item
            characterList.SelectedItem = null;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)// && authenticated)
            {
                viewModel.LoadItemsCommand.Execute(null);

                // Hide the Sign-in button.
                //this.loginButton.IsVisible = false;
            }
        }
    }
}

