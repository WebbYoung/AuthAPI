using AuthAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.Domain.Contracts
{
	public interface IUserAccessFail
	{
		public void Reset(UserAccessFail accessFail);
		public void Failed(UserAccessFail accessFai,DateTime? lockOutEnd);
		public bool isLockOut(UserAccessFail accessFail);
	}
}
