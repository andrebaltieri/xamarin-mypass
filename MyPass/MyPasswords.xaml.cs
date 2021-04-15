using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MyPass.Core.Models;
using MyPass.Core.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MyPass
{
    public partial class MyPasswords : ContentPage
    {
        public ObservableCollection<PasswordItem> Passwords { get; set; }
        private bool _isRefreshing = false;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }
        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsRefreshing = true;

                    await LoadAsync();

                    IsRefreshing = false;
                });
            }
        }

        public MyPasswords()
        {
            InitializeComponent();
            Passwords = new ObservableCollection<PasswordItem>();
            PasswordsListView.ItemsSource = Passwords;
        }

        private async Task LoadAsync()
        {
            Passwords.Clear();
            try
            {
                var json = await SecureStorage.GetAsync("passwords");
                var passwords = StorageService.Get(json);
                foreach (var password in passwords)
                {
                    Passwords.Add(password);
                }
            }
            catch
            {
                await DisplayAlert(
                    "Ops!",
                    "This device does not support secure storage",
                    "OK");
            }
        }

        protected async override void OnAppearing()
        {
            await LoadAsync();
        }

        async void OnAddClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreatePassword());
        }

        public async void OnDelete(object sender, EventArgs e)
        {
            var result = await DisplayAlert(
                "Delete this password?",
                "This process can't be undone",
                "OK",
                "Cancel");

            if (result)
            {
                var mi = ((MenuItem)sender);
                var item = (PasswordItem)mi.CommandParameter;
                Passwords.Remove(item);

                try
                {
                    var json = StorageService.Update(Passwords.ToList());
                    await SecureStorage.SetAsync("passwords", json);
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

        public void OnTapped(object sender, ItemTappedEventArgs e)
        {
            var passwordItem = e.Item as PasswordItem;
            _ = CopyToClipboard(passwordItem.Password);
            DisplayAlert(passwordItem.Title, "Password copied to clipboard", "OK");
        }

        private async Task CopyToClipboard(string text)
        {
            await Clipboard.SetTextAsync(text);
        }
    }
}
