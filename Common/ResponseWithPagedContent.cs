namespace Common
{
    public class ResponseWithPagedContent<T> : ResponsePaged
    {
        public T Content { get; set; }
    }
}
