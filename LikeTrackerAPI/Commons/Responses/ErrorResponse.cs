namespace LikeTrackerAPI.Commons.Responses
{
    public class ErrorResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseDescription { get; set; }


        public static T Create<T>(string errorCode, string errorMessage) where T : BasicResponse, new()
        {
            var response = new T
            {
                IsSuccessful = false,
                Error = new ErrorResponse
                {
                    ResponseCode = errorCode,
                    ResponseDescription = errorMessage
                }
            };
            return response;
        }

        public override string ToString()
        {
            return $"{ResponseCode} :-: {ResponseDescription}";
        }
    }
}
