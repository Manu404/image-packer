using ImagePacker.Client.Common.Interface;
using System.Windows.Input;

namespace ImagePacker.Client.ViewModel
{
    public interface IMenuViewModel : IViewModel
    {
        ICommand NewCommand { get; }
        ICommand OpenCommand { get; }
        ICommand SaveCommand { get; }

        void SetMainViewModel(IMainViewModel viewModel);
    }
}