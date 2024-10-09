using System.Net;

namespace OnlineStore.Models.BaseModels
{
    public class ApiResponseModel
    {
        public object? ResponseContent;

        public HttpStatusCode StatuseCode;

        public bool IsSuccessfull;
    }
}
