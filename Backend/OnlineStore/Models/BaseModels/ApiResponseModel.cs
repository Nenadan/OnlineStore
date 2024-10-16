using System.Net;

namespace OnlineStore.Models.BaseModels
{
    public class ApiResponseModel
    {
        public object? ResponseContent { get; set; }
        public HttpStatusCode StatuseCode { get; set; }
        public bool IsSuccessfull { get; set; }
    }
}
