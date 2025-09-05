using AuthAPI.Domain.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.Domain.Entities
{
	public class User
	{
		public Guid Id { get; set; }
		public PhoneNumber? PhoneNumber { get; set; }
		public string? PasswordHash { get; set; }
		public UserAccessFail? AccessFail { get; set; }
		private User() { }
		public User(PhoneNumber phoneNumber)
		{
			Id = Guid.NewGuid();
			PhoneNumber = phoneNumber;
			AccessFail = new UserAccessFail(this);
		}
	}
}
