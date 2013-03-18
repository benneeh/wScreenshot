namespace wScreenshot.Interface
{
    public interface IUploadMethod
    {
        string Name { get; }

        IUpload Upload();
    }
}