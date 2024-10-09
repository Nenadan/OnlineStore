namespace OnlineStore.Models.BaseModels
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
