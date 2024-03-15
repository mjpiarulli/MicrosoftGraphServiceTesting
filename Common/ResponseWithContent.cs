namespace Common
{
    public class ResponseWithContent<T> : Response
    {
        public T Content { get; set; }
    }
}
