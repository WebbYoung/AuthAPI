using AuthAPI.Domain.Contracts;
using AuthAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.Domain.Services
{
	public enum UserAccessResult
	{
		OK,
		PhoneNumberNotFound,
		LockOut,
		NoPassword,
		PasswordError
	}
	public class AccessFailService : IUserAccessFail
	{
		public void Failed(UserAccessFail accessFail, DateTime? lockOutEnd=null)
		{
			int count=accessFail.AccessFailedCount++;
			if (count >= 3)
			{
				accessFail.LockOut = true;
				accessFail.LockOutEnd = lockOutEnd??DateTime.Now.AddHours(2);
			}
		}

		public bool isLockOut(UserAccessFail accessFail)
		{
			if (accessFail.LockOut)
			{
				if (accessFail.LockOutEnd >= DateTime.Now)
				{
					return true;
				}
				else
				{
					accessFail.AccessFailedCount = 0;
					accessFail.LockOutEnd = null;
					return false;
				}
			}
			return false;
		}


		public void Reset(UserAccessFail accessFail)
		{
			accessFail.LockOut = false;
			accessFail.LockOutEnd = null;
			accessFail.AccessFailedCount = 0;
		}
	}
}
