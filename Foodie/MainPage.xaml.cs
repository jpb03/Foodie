using Microsoft.Maui.Controls;
using System;

namespace Foodie
{
    public partial class MainPage : ContentPage
    {
        string username;

        public MainPage(string usuario)
        {
            InitializeComponent();
            username = usuario;
            welcomeLabel.Text = $"👋 Hola, {username}!";
        }

        private async void OnRecetasClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RecetasPage());
        }

        private async void OnDesayunoClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DesayunoPage());
        }

        private async void OnAlmuerzoClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AlmuerzoPage());
        }

        private async void OnPostresClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PostresPage());
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}
