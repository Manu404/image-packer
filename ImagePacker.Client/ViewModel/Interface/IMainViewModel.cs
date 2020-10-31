using ImagePacker.Client.Common.Interface;

namespace ImagePacker.Client.ViewModel
{
    public interface IMainViewModel : IViewModel
    {
        IMenuViewModel MenuViewModel { get; }
    }
}