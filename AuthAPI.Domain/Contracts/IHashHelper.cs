using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.Domain.Services
{
	public interface IHashHelper
	{
		public string HashPassword(string password);
		public bool VerifyPassword(string password, string storedHash);
	}
}
