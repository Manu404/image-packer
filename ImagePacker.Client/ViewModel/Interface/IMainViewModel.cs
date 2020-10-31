using ImagePacker.Client.Common.Interface;
using ImagePacker.Client.Model;

namespace ImagePacker.Client.ViewModel
{
    public interface IMainViewModel : IViewModel
    {
        IMenuViewModel MenuViewModel { get; }
        PackProject Project { get; set; }
        bool IsBusy { get; }

        void AddFiles();
        void Exit();
    }
}