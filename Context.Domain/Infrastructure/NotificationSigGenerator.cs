using Example.Notific.Context.Domain.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Example.Notific.Context.Domain.Infrastructure
{
    public class NotificationSigGenerator : INotificationSigGenerator
    {
        public string ComputeHMACSHA256(string key, string message)
        {
            HMACSHA256 hmacsha256 = new HMACSHA256(UTF8Encoding.UTF8.GetBytes(key));
            byte[] hashsha256 = hmacsha256.ComputeHash(UTF8Encoding.UTF8.GetBytes(message));
            return ByteToBase64String(hashsha256);
        }
        /// <summary>
        /// Convert bytes array to Base64 string
        /// </summary>
        /// <param name="buff"></param>
        /// <returns></returns>
        private string ByteToBase64String(byte[] buff)
        {
            return Convert.ToBase64String(buff);
        }
    }
}
