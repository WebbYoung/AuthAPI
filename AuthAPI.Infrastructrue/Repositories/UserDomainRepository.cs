using AuthAPI.Domain.Entities;
using AuthAPI.Domain.Events;
using AuthAPI.Domain.Repository;
using AuthAPI.Domain.Values;
using AuthAPI.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Zack.Infrastructure.EFCore;

namespace AuthAPI.Infrastructure.Repositories
{
	public class UserDomainRepository : IUserDomainRepository
	{
		private readonly MySqlDbContext context;
		private readonly IDistributedCache disCache;
		private readonly IMediator mediator;
		public UserDomainRepository(MySqlDbContext context,IDistributedCache disCache,IMediator mediator)
		{
			this.context = context;
			this.disCache = disCache;
			this.mediator = mediator;
		}
		public async Task AddNewLoginHistoryAsync(PhoneNumber phoneNumber, string msg)
		{
			var user = await FindOneAsync(phoneNumber);
			UserLoginHistory history = new(user?.Id,phoneNumber,msg);
			await context.LoginHistories.AddAsync(history);
		}

		public async Task<User?> FindOneAsync(PhoneNumber phoneNumber)
		{
			return await context.Users.SingleOrDefaultAsync(ExpressionHelper.MakeEqual((User u) => u.PhoneNumber, phoneNumber));
		}

		public async Task<User?> FindOneAsync(Guid Id)
		{
			return await context.Users.FindAsync(typeof(User),Id);
		}

		public Task PublishEventAsync(UserAccessResultEvent _event)
		{
			return mediator.Publish(_event);
		}

		public async Task<string?> RetrievePhoneCodeAsync(PhoneNumber phoneNumber)
		{
			string fullNumber = phoneNumber.RegionCode + phoneNumber.Number;
			string cacheKey = $"LoginByPhoneAndCode_{fullNumber}";
			string? code = await disCache.GetStringAsync(cacheKey);
			await disCache.RemoveAsync(cacheKey);
			return code;
		}

		public async Task SavePhoneCodeAsync(PhoneNumber phoneNumber,string code)
		{
			string fullNumber = phoneNumber.RegionCode + phoneNumber.Number;
			string cacheKey = $"LoginByPhoneAndCode_{fullNumber}";
			await disCache.SetStringAsync(cacheKey, code);
		}

	}
}
