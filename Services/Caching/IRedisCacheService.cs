namespace Products_service.Services.Caching
{
    public interface IRedisCacheService
    {
        // T meen any attribute type => string, int ...
        // GetData<T> => meen that we are going to pass an any att type
        public Task<T?> GetData<T>(string key);
        public Task SetData<T>(string key, T data);
    }
}
