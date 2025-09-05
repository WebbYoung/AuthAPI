using AuthAPI.Domain.Contracts;
using AuthAPI.Domain.Values;
namespace AuthAPI.Infrastructure.ACL
{
	public class SmsCodeSender : ISmsCodeSender
	{
		public Task SendCodeAsync(PhoneNumber phoneNumber, string code)
		{
			Console.WriteLine($"向{phoneNumber.RegionCode}-{phoneNumber.Number}发送验证码{code}");
			return Task.CompletedTask;
		}
	}
}
