namespace ImagePacker.Client.Common.Interface
{ 
    public interface IFactory<out T, in Y>
    {
        T Build(Y data);
    }
    public interface IFactory<out T>
    {
        T Build();
    }
}