using Microsoft.Maui.Controls;
using System;
using IMProWater.Data;

namespace IMProWater
{
    public partial class RegisterPage : ContentPage
    {
        private bool _isPasswordVisible = false; // Flaga widoczno�ci has�a
        private bool _isRepeatPasswordVisible = false; // Flaga widoczno�ci powt�rzonego has�a

        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void OnRegisterButtonClicked(object sender, EventArgs e)
        {
            // Sprawdzenie, czy kontrolki nie s� null i uzyskanie ich tekst�w
            string email = EmailEntry?.Text;
            string password = PasswordEntry?.Text;
            string repeatPassword = RepeatPasswordEntry?.Text;

            // Sprawdzenie, czy pola s� wype�nione
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(repeatPassword))
            {
                await DisplayAlert("B��d", "Wszystkie pola musz� by� wype�nione.", "OK");
                return;
            }

            // Sprawdzenie, czy has�a s� identyczne
            if (password != repeatPassword)
            {
                await DisplayAlert("B��d", "Has�a musz� by� identyczne.", "OK");
                return;
            }

            // Sprawdzanie, czy has�o spe�nia wymogi
            if (!IsPasswordValid(password))
            {
                await DisplayAlert("B��d", "Has�o musi zawiera� co najmniej 5 znak�w, w tym du�� liter�, znak specjalny oraz cyfr�.", "OK");
                return;
            }

            // Sprawdzenie, czy email ju� istnieje w bazie danych
            var existingUser = await Database.DatabaseInstance.Table<User>()
                                  .Where(u => u.Email == email)
                                  .FirstOrDefaultAsync();

            if (existingUser != null)
            {
                await DisplayAlert("B��d", "Podany email istnieje w bazie danych", "OK");
            }
            else
            {
                var user = new User { Email = email, Password = password };
                await Database.DatabaseInstance.InsertAsync(user);

                await DisplayAlert("Sukces", "Rejestracja zako�czona pomy�lnie!", "OK");
                await Navigation.PushAsync(new LoginPage());
            }
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void OnExitButtonClicked(object sender, EventArgs e)
        {
            Application.Current.Quit();
        }

        // Funkcja sprawdzaj�ca poprawno�� has�a
        private bool IsPasswordValid(string password)
        {
            if (password.Length < 5) return false;

            bool hasUpperCase = System.Text.RegularExpressions.Regex.IsMatch(password, @"[A-Z]");
            bool hasSpecialChar = System.Text.RegularExpressions.Regex.IsMatch(password, @"[!@#$%^&*(),.?""{}|<>]");
            bool hasDigit = System.Text.RegularExpressions.Regex.IsMatch(password, @"\d");

            return hasUpperCase && hasSpecialChar && hasDigit;
        }

        // Funkcja obs�uguj�ca widoczno�� has�a
        private void OnTogglePasswordVisibilityClicked(object sender, EventArgs e)
        {
            _isPasswordVisible = !_isPasswordVisible;
            PasswordEntry.IsPassword = !_isPasswordVisible;

            ((ImageButton)sender).Source = _isPasswordVisible ? "eye_off.png" : "eye.png";
        }

        // Funkcja obs�uguj�ca widoczno�� powt�rzenia has�a
        private void OnToggleRepeatPasswordVisibilityClicked(object sender, EventArgs e)
        {
            _isRepeatPasswordVisible = !_isRepeatPasswordVisible;
            RepeatPasswordEntry.IsPassword = !_isRepeatPasswordVisible;

            ((ImageButton)sender).Source = _isRepeatPasswordVisible ? "eye_off.png" : "eye.png";
        }

        // Funkcja obs�uguj�ca przej�cie do strony logowania, je�li u�ytkownik ju� ma konto
        private async void OnLoginTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }
    }
}
