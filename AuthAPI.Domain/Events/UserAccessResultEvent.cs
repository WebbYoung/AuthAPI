using AuthAPI.Domain.Services;
using AuthAPI.Domain.Values;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.Domain.Events
{
	public record UserAccessResultEvent(PhoneNumber PhoneNumber,UserAccessResult Result):INotification
	{
	}
}
