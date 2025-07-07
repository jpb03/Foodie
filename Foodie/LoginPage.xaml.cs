using Microsoft.Maui.Controls;
using System;
using System.Linq;

namespace Foodie
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            var user = App.Database.Table<Usuario>()
                .FirstOrDefault(u => u.Nombre == usernameEntry.Text && u.Contrasena == passwordEntry.Text);

            if (user != null)
            {
                await Navigation.PushAsync(new MainPage(user.Nombre));
            }
            else
            {
                await DisplayAlert("Error", "Usuario o contraseña incorrectos", "OK");
            }
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(usernameEntry.Text) && !string.IsNullOrWhiteSpace(passwordEntry.Text))
            {
                var existente = App.Database.Table<Usuario>().FirstOrDefault(u => u.Nombre == usernameEntry.Text);
                if (existente == null)
                {
                    App.Database.Insert(new Usuario
                    {
                        Nombre = usernameEntry.Text,
                        Contrasena = passwordEntry.Text
                    });
                    await DisplayAlert("Éxito", "Usuario registrado", "OK");
                }
                else
                {
                    await DisplayAlert("Aviso", "Usuario ya existe", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "Ingrese usuario y contraseña", "OK");
            }
        }
    }
}
