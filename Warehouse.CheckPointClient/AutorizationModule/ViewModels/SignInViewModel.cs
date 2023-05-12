using Microsoft.Xaml.Behaviors.Core;
using Prism.Mvvm;
using System.Windows.Input;
using Warehouse.CheckPointClient.Services;

namespace Autorization.ViewModels
{
    public class SignInViewModel : BindableBase
    {
        private string login;
        private string password;
        private ActionCommand signInCommand;
        private readonly AutorizationService authService;
        private bool hasError;

        public string Login { get => login; set { SetProperty(ref login, value); HasError = false; } }
        public string Password { get => password; set { SetProperty(ref password, value); HasError = false; } }
        public ICommand SignInCommand => signInCommand ??= new ActionCommand(SignIn);
        public bool HasError { get => hasError; set => SetProperty(ref hasError, value); }

        public SignInViewModel(AutorizationService authService)
        {
            this.authService = authService;
            authService.RequiredAutorize = true;
        }

        private void SignIn()
        {
            if(!authService.Autorize(login, password))
                HasError = true;
        }
    }
}
