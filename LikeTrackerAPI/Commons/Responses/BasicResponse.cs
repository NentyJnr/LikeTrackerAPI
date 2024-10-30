using System.ComponentModel;

namespace LikeTrackerAPI.Commons.Responses
{
    public class BasicResponse
    {
        [DefaultValue(true)]
        public bool IsSuccessful { get; set; }
        [DefaultValue(null)]
        public ErrorResponse Error { get; set; }

        public BasicResponse()
        {
            IsSuccessful = false;
        }
        public BasicResponse(bool isSuccessful = false)
        {
            IsSuccessful = isSuccessful;
        }
    }
}
