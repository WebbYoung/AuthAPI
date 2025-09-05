using AuthAPI.Application.Models.Requests;
using AuthAPI.Application.Models.Responses;
using AuthAPI.Application.UoWs;
using AuthAPI.Domain.Entities;
using AuthAPI.Domain.Repository;
using AuthAPI.Domain.Services;
using AuthAPI.Infrastructure.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace AuthAPI.Application.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly UserDomainService userDomainService;
		private readonly MySqlDbContext context;
		private readonly IUserDomainRepository userDomainRepository;
		private readonly IUserService userService;
		public UsersController(UserDomainService userDomainService, MySqlDbContext context,IUserDomainRepository repository,IUserService userService)
		{
			this.userDomainRepository = repository;
			this.userDomainService = userDomainService;
			this.context = context;
			this.userService = userService;
		}
		[Db(typeof(MySqlDbContext))]
		[HttpGet]
		public async Task<IActionResult> Login(SignInRequest req)
		{
			if (req == null || req.PhoneNumber == null || req.password == null)
			{
				return BadRequest(new Response(400, "用户名或密码为空"));
			}
			if (await userDomainRepository.FindOneAsync(req.PhoneNumber) is not null)
			{
				return BadRequest(new Response(400, "用户已存在"));
			}
			if (req?.password?.Length <= 3)
			{
				return BadRequest(new Response(400,"密码长度不能小于3"));
			}
			var result = await userDomainService.CheckLoginAsync(req.PhoneNumber, req.password);
			switch (result)
			{
				case UserAccessResult.OK:
					return Ok(new Response(200, "登录成功"));
				case UserAccessResult.PhoneNumberNotFound:
				case UserAccessResult.NoPassword:
				case UserAccessResult.PasswordError:
					return NotFound(new Response(400,"用户名或密码错误"));
				case UserAccessResult.LockOut:
					return BadRequest(new Response(400, "用户已锁定"));
				default:
					throw new ApplicationException("未知枚举值");
			};
		}
		[Db(typeof(MySqlDbContext))]
		[HttpPost]
		public async Task<IActionResult> Register(AddUserRequest req)
		{
			if (req == null || req.PhoneNumber == null||req.password==null)
			{
				return BadRequest(new Response(400, "用户名或密码为空"));
			}
			if(await userDomainRepository.FindOneAsync(req.PhoneNumber) is not null)
			{
				return BadRequest(new Response(400, "用户已存在"));
			}
			var user = new User(req.PhoneNumber);
			userService.ChangePassword(user, req.password);
			await context.Users.AddAsync(user);
			return CreatedAtAction("Register", new Response(200,"注册成功"));
		}
	}
}
