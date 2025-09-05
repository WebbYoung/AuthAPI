using AuthAPI.Domain.Commons;
using AuthAPI.Domain.Entities;
using AuthAPI.Domain.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.Domain.Services
{
	public enum CheckCodeResult 
	{
		OK,
		PhoneNumberNotFound,
		Lockout,
		CodeError
	}

	public class UserService: IUserService
	{
		private readonly IHashHelper hashHelper;
		public UserService(IHashHelper hashHelper)
		{
			this.hashHelper = hashHelper;
		}
		public bool HasPassword(User user)
		{
			return !string.IsNullOrEmpty(user.PasswordHash);
		}
		public void ChangePassword(User user,string newPassword)
		{
			if(string.IsNullOrEmpty(newPassword) || newPassword.Length < 6)
			{
				throw new ArgumentException("密码长度不能少于6位", nameof(newPassword));
			}
			user.PasswordHash = hashHelper.HashPassword(newPassword);
		}
		public bool VerifyPassword(User user,string password)
		{
			if(string.IsNullOrEmpty(user.PasswordHash))
			{
				throw new InvalidOperationException("用户未设置密码");
			}
			return hashHelper.VerifyPassword(password, user.PasswordHash);
		}
		public void ChangePhoneNumber(User user, PhoneNumber phoneNumber)
		{
			if(phoneNumber==null)
			{
				throw new ArgumentException("手机号不能为空", nameof(phoneNumber));
			}
			user.PhoneNumber = phoneNumber;
		}
	}

}
