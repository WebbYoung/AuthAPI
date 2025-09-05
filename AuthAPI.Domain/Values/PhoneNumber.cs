using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.Domain.Values
{
	public record PhoneNumber(int RegionCode,string Number)
	{

	}
}
