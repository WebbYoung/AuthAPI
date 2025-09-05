namespace AuthAPI.Application.UoWs
{
	[AttributeUsage(AttributeTargets.Method)]
	public class DbAttribute:Attribute
	{
		public Type[] DbContextTypes { get; init; }
		public DbAttribute(params Type[] dbContextTypes)
		{
			this.DbContextTypes = dbContextTypes;
		}
	}
}
