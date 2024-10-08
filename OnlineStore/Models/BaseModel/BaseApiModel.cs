namespace OnlineStore.Models.BaseModel
{
    public class BaseApiModel<T>
    {
        public T Id { get; set; }
        public string DocType { get; set; }
    }
}
