namespace OnlineStore.Models.BaseModel
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CosmosAttribute : Attribute
    {
        public string DocType { get; set; }

        public CosmosAttribute(string docType)
        {
            this.DocType = docType;
        }
    }
}
