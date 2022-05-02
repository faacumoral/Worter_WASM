using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Worter.Common
{
    public static class Extensions
    {
        private static Random rng = new Random();

        public static string GetConnectionString(this IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(Constants.CONSTANTS.Keys.WORTER_CONTEXT);
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("Connection string cannot be blank.");
            }
            return connectionString;
        }

        public static string GetEncryptPassword(this IConfiguration configuration)
        {
            var encrypt = configuration[Constants.CONSTANTS.Keys.ENCRYPT_PASSWORD];
            if (string.IsNullOrEmpty(encrypt))
            {
                throw new ArgumentException("Encrypt password cannot be blank.");
            }
            return encrypt;
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static T GetRandom<T>(this IList<T> list)
        {
            var randomIndex = rng.Next(list.Count);
            var element = list[randomIndex];
            list.RemoveAt(randomIndex);
            return element;
        }
    }
}
