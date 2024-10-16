namespace OnlineStore.Models.BaseModels
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CosmosAttribute : Attribute
    {
        public string DocType { get; set; }
        public string Container { get; set; }

        public CosmosAttribute(string docType, string container)
        {
            this.DocType = docType;
            this.Container = container;
        }
    }
}
