using System;
using System.Threading.Tasks;

namespace MyPass.Core.Services
{
    public interface IBiometricAuthenticateService
    {
        String GetAuthenticationType();
        Task<bool> AuthenticateUserIDWithTouchID();
        bool fingerprintEnabled();
    }
}
