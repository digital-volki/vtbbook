namespace vtbbook.Core.Common
{
    public interface ISettings
    {
        public string ContentRootPath { get; set; }
        string GetValue(string name);
        T GetValue<T>(string name);
        T GetSection<T>(string name) where T : class, new();
    }
}
