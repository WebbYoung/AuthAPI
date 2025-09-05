using AuthAPI.Domain.Contracts;
using AuthAPI.Domain.Entities;
using AuthAPI.Domain.Repository;
using AuthAPI.Domain.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.Domain.Services
{
	public class UserDomainService
	{ 
		private readonly ISmsCodeSender smsSender;
		private readonly IUserDomainRepository repository;
		private readonly IUserService userService;
		private readonly IUserAccessFail userAccessFail;
		public DateTime LockOutEnd { get; set; }
		public UserDomainService(IUserDomainRepository repository,ISmsCodeSender smsSender,IUserService userService,IUserAccessFail accessFail)
		{
			this.repository = repository;
			this.smsSender = smsSender;
	        this.userService = userService;
			this.userAccessFail = accessFail;
		}
		public bool IsLockOut(User user)
		{
			return userAccessFail.isLockOut(user.AccessFail);
		}
		public void ResetAccessFail(User user)
		{
			userAccessFail.Reset(user.AccessFail);
		}
		public void AccessFail(User user)
		{
			userAccessFail.Failed(user.AccessFail, LockOutEnd);
		}

		public async Task<UserAccessResult> CheckLoginAsync(PhoneNumber phoneNumber,string password)
		{
			User? user = await repository.FindOneAsync(phoneNumber);
			UserAccessResult result = user switch
			{
				null => UserAccessResult.PhoneNumberNotFound,
				(var u) when IsLockOut(u) => UserAccessResult.LockOut,
				(var u) when !userService.HasPassword(u)=>UserAccessResult.NoPassword,
				(var u) when userService.VerifyPassword(u,password)=>UserAccessResult.OK,
				_=>UserAccessResult.PasswordError
			};
			if (user != null)
			{
				if (result == UserAccessResult.OK)
				{
					ResetAccessFail(user);
				}
				else
				{
					AccessFail(user);
				}
			}
			await repository.PublishEventAsync(new Events.UserAccessResultEvent(phoneNumber, result));
			return result;
		}
	
		public async Task<CheckCodeResult> CheckCodeAsync(PhoneNumber phoneNumber,string code)
		{
			var user = await repository.FindOneAsync(phoneNumber);
			if (user == null)
				return CheckCodeResult.PhoneNumberNotFound;
			if (IsLockOut(user))
				return CheckCodeResult.Lockout;
			string? codeInServer = await repository.RetrievePhoneCodeAsync(phoneNumber);
			if (string.IsNullOrEmpty(codeInServer))
			{
				return CheckCodeResult.CodeError;
			}
			if (code == codeInServer)
			{
				return CheckCodeResult.OK;
			}
			else
			{
				AccessFail(user);
				return CheckCodeResult.CodeError;
			}
		}
	}
}
