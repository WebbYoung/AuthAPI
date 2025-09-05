using AuthAPI.Domain.Values;

namespace AuthAPI.Application.Models.Requests
{
	public abstract class ApiRequest
	{
		public PhoneNumber? PhoneNumber { get; set; }
		public string? password { get; set; }
	}
}
