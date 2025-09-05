namespace AuthAPI.Application.Models.Responses
{
	public class Response
	{
		public int Code { get; set; }
		public string Message { get; set; } = string.Empty;
		public Response(int code, string message) 
		{
			Code = code;
			Message = message;
		}
	}
}
