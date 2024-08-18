namespace BusinessAction.API.Models
{
    public class CamundaRequestModel
    {
        public string Woid { get; set; }
        public string TaskId {  get; set; }
        public string processDefinitionKey { get; set; }
        public string processInstanceId { get; set; }
        public string OrderType { get; set; }
        public String data { get; set; }

    }
}
