using Autorization.Services;
using Prism.Mvvm;

namespace Autorization.ViewModels
{
    public class ViewAViewModel : BindableBase
    {
        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public ViewAViewModel(AutorizationService autoService)
        {
            Message = "View A from your Prism Module";
        }
    }
}
