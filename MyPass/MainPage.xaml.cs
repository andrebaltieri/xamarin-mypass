using System.Threading.Tasks;
using MyPass.Core.Services;
using Xamarin.Forms;

namespace MyPass
{
    public partial class MainPage : ContentPage
    {
        private string _authType = string.Empty;

        public MainPage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            await LoginAndNavigate();
        }

        private async Task LoginAndNavigate()
        {
            _authType = DependencyService.Get<IBiometricAuthenticateService>().GetAuthenticationType();
            if (!_authType.Equals("None"))
            {
                lblAuthenticationMessage.Text = "Use " + _authType + " to unlock this App";
                if (_authType.Equals("TouchId") || _authType.Equals("FaceId"))
                {
                    if (await GetAuthResults())
                    {
                        lblAuthenticationMessage.Text = "Navigating to your passwords...";
                        await Navigation.PushAsync(new MyPasswords());
                    }
                }
            }
        }

        private async Task<bool> GetAuthResults()
        {
            var result = await DependencyService.Get<IBiometricAuthenticateService>().AuthenticateUserIDWithTouchID();
            if (result)
            {
                lblAuthenticationMessage.Text = "Authentication successfully";
                return true;
            }

            lblAuthenticationMessage.Text = "Authentication failed";
            return false;
        }
    }
}
