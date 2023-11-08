namespace UserIdentity.Presentation.ApiResponse
{
    public class ApiResponse<T>
    {
        public int ResponseStatus { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
    }
}
