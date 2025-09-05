using AuthAPI.Domain.Entities;
using AuthAPI.Domain.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.Domain.Services
{
	public interface IUserService
	{
		public bool HasPassword(User user);
		public void ChangePassword(User user, string newPassword);
		public bool VerifyPassword(User user, string password);
	}
}
