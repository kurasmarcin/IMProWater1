using Microsoft.Maui.Controls;
using System;

namespace IMProWater
{
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private async void OnSekcjeTapped(object sender, EventArgs e)
        {
            // Przekazujemy domy�lny numer sekcji, np. "1"
            await Navigation.PushAsync(new SectionsPage());
        }

        private async void OnUstawieniaTapped(object sender, EventArgs e)
        {
            // Przejd� do strony Ustawienia
            await Navigation.PushAsync(new SettingsPage());
        }

        private async void OnStatusTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StatusPage());
        }

        // Logika wylogowania
        private async void OnLogoutButtonClicked(object sender, EventArgs e)
        {
            // Przekierowanie u�ytkownika do LoginPage
            await Navigation.PushAsync(new LoginPage());
        }

        // Funkcja obs�uguj�ca zamkni�cie aplikacji
        private void OnExitButtonClicked(object sender, EventArgs e)
        {
            Application.Current.Quit();
        }
    }
}
