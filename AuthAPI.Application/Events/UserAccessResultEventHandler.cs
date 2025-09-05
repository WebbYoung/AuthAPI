using AuthAPI.Domain.Events;
using AuthAPI.Domain.Repository;
using AuthAPI.Infrastructure.Contexts;
using MediatR;
using System.Runtime.CompilerServices;

namespace AuthAPI.Application.Events
{
	public class UserAccessResultEventHandler : NotificationHandler<UserAccessResultEvent>
	{
		private readonly IUserDomainRepository repository;
		private readonly MySqlDbContext mySqlDb;
		public UserAccessResultEventHandler(IUserDomainRepository repository,MySqlDbContext mySqlDb)
		{
			this.repository = repository;
			this.mySqlDb = mySqlDb;
		}
		protected override void Handle(UserAccessResultEvent notification)
		{
			repository.AddNewLoginHistoryAsync(notification.PhoneNumber,$"登录结果{notification.Result}");
			mySqlDb.SaveChanges();
		}
	}
}
