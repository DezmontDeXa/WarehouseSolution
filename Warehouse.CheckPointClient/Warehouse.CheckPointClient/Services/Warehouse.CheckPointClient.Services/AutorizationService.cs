using NLog;
using SharedLibrary.DataBaseModels;
using System;
using System.Linq;

namespace Warehouse.CheckPointClient.Services
{
    public class AutorizationService
    {
        private ILogger logger = LogManager.GetCurrentClassLogger();

        public bool RequiredAutorize { get; set; } = false;

        public event EventHandler Authorized;

        public AutorizationService()
        {
            
        }

        public bool Autorize(string login, string password)
        {
            using(var db = new WarehouseContext())
            {
                var user = db.Users.FirstOrDefault(x => x.Login == login);
                if(user ==null)
                {
                    logger.Warn($"Имя пользователя не найдено \"{login}\"");
                    return false;
                }
                else
                {
                    var passHash = CreateMD5(password);

                    if (user.PasswordHash == passHash)
                    {
                        logger.Warn($"Авторизован пользователь с логином: \"{login}\"");
                        Authorized?.Invoke(this, EventArgs.Empty);
                        return true;
                    }
                    else
                    {
                        logger.Warn($"Пользователь с логином \"{login}\" указал не верный пароль");
                        return false;
                    }
                }
            }
        }

        private static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes).ToLower(); // .NET 5 +

                // Convert the byte array to hexadecimal string prior to .NET 5
                // StringBuilder sb = new System.Text.StringBuilder();
                // for (int i = 0; i < hashBytes.Length; i++)
                // {
                //     sb.Append(hashBytes[i].ToString("X2"));
                // }
                // return sb.ToString();
            }
        }
    }
}
