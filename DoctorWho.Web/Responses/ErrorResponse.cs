namespace DoctorWho.Web.Responses
{
    public class ErrorResponse
    {
        public ICollection<ErrorModel> Errors { get; set; } = new List<ErrorModel>();
    }
}
