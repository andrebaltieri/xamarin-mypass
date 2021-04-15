using System;
using System.Threading.Tasks;
using MyPass.Core;
using MyPass.Core.Models;
using MyPass.Core.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MyPass
{
    public partial class CreatePassword : ContentPage
    {
        public CreatePassword()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            InputPassword.Text = PasswordGenerator.Generate();
        }

        void Refresh(object sender, EventArgs e)
        {
            InputPassword.Text = PasswordGenerator.Generate();
        }

        void CopyToClipboard(object sender, EventArgs e)
        {
            _ = CopyAndDisplay();
        }

        void Save(object sender, EventArgs e)
        {
            _ = SaveToLocal();
        }

        async Task CopyAndDisplay()
        {
            await Clipboard.SetTextAsync(InputPassword.Text);
            await DisplayAlert(
                "All right!",
                "The password was copied to your clipboard",
                "OK");
        }

        async Task SaveToLocal()
        {
            try
            {
                var item = new PasswordItem(InputTitle.Text, InputPassword.Text);
                var json = await SecureStorage.GetAsync("passwords");
                var result = StorageService.Add(item, json);
                await SecureStorage.SetAsync("passwords", result);
                await Navigation.PopAsync();
            }
            catch
            {
                await DisplayAlert(
                    "Ops!",
                    "This device does not support secure storage",
                    "OK");
            }
        }


    }
}
