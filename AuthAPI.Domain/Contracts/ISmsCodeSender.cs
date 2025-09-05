using AuthAPI.Domain.Values;

namespace AuthAPI.Domain.Contracts
{
	public interface ISmsCodeSender
	{
		Task SendCodeAsync(PhoneNumber phoneNumber,string code);
	}
}
