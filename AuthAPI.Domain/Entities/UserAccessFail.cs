using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.Domain.Entities
{
	public class UserAccessFail
	{
		public Guid Id { get; set; }
		public Guid UserId { get; set; }
		public User? User { get; set; }
		public bool LockOut;
		public DateTime? LockOutEnd { get; set; }
		public int AccessFailedCount { get; set; }
		private UserAccessFail() { }

		public UserAccessFail(User user) 
		{
			Id = Guid.NewGuid();
			User = user;
			UserId = user.Id;
		}
	}
}
