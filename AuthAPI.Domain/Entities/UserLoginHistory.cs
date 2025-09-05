using AuthAPI.Domain.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.Domain.Entities
{
	public class UserLoginHistory
	{
		public long Id { get; init; }
		public Guid? UserId { get; init; }
		public PhoneNumber? PhoneNumber { get; init; }
		public DateTime CreateTime { get; init; }
		public string? Message { get; init; }
		private UserLoginHistory() { }
		public UserLoginHistory(Guid? userId, PhoneNumber? phoneNumber, string? message)
		{
			UserId = userId;
			PhoneNumber = phoneNumber;
			CreateTime = DateTime.Now;
			Message = message;
		}
	}
}
