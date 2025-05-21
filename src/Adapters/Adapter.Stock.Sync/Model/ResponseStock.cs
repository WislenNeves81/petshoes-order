namespace Adapter.Stock.Sync.Model
{
    internal class ResponseStock<T>
    {
        public ResponseStock()
        {
            Success = false;
            Result = default!;
        }
        public T Result { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public void WithSuccess(T result)
        {
            Result = result;
            Success = true;
        }

        public void WithError(string message)
        {
            Success = false;
            Message = message;
        }
    }
}
