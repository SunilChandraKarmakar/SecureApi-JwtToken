namespace ClaimAuthorizationApi.Model.ResponseModel
{
    public class ResponseStatusModel
    {
        public ResponseStatusModel(ResponseCode responseCode, string responseMessage, Object? dataSet)
        {
            ResponseCode = responseCode;
            ResponseMessage = responseMessage;
            DataSet = dataSet;
        }

        public ResponseCode ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public object? DataSet { get; set; }
    }
}
