using AuthAPI.Domain.Entities;
using AuthAPI.Domain.Events;
using AuthAPI.Domain.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.Domain.Repository
{
	public interface IUserDomainRepository
	{
		public Task<User?> FindOneAsync(PhoneNumber phoneNumber);
		public Task<User?> FindOneAsync(Guid Id);
		public Task AddNewLoginHistoryAsync(PhoneNumber phoneNumber,string msg);
		public Task SavePhoneCodeAsync(PhoneNumber phoneNumber,string code);
		public Task PublishEventAsync(UserAccessResultEvent @event);
		public Task<string?> RetrievePhoneCodeAsync(PhoneNumber phoneNumber);
	}
}
