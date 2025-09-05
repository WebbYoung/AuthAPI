using AuthAPI.Domain.Services;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AuthAPI.Domain.Commons
{
	public class HashService:IHashHelper
	{
		public string HashPassword(string password)
		{
			// 生成随机盐
			byte[] salt = new byte[16];
			using (var rng = RandomNumberGenerator.Create())
			{
				rng.GetBytes(salt);
			}

			// PBKDF2 哈希
			byte[] hash = KeyDerivation.Pbkdf2(
				password: password,
				salt: salt,
				prf: KeyDerivationPrf.HMACSHA256,
				iterationCount: 100_000,
				numBytesRequested: 32);

			// 存储 盐 + 哈希（Base64）
			return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
		}

		public bool VerifyPassword(string password, string storedHash)
		{
			var parts = storedHash.Split(':');
			byte[] salt = Convert.FromBase64String(parts[0]);
			byte[] stored = Convert.FromBase64String(parts[1]);

			// 再计算一次
			byte[] hash = KeyDerivation.Pbkdf2(
				password: password,
				salt: salt,
				prf: KeyDerivationPrf.HMACSHA256,
				iterationCount: 100_000,
				numBytesRequested: 32);

			return CryptographicOperations.FixedTimeEquals(hash, stored);
		}
	}
}
