using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;



namespace UserDataStorage.Logic
{
    public class HashCryptoService
    {
        /// <summary>
        /// Метод сравнения байтов двух объектов
        /// </summary>
        /// <param name="b0"></param>
        /// <param name="b1"></param>
        /// <returns></returns>
        private static bool ByteArraysEqual(byte[] b0, byte[] b1)
        {
            if (b0 == b1)
            {
                return true;
            }

            if (b0 == null || b1 == null)
            {
                return false;
            }

            if (b0.Length != b1.Length)
            {
                return false;
            }

            for (int i = 0; i < b0.Length; i++)
            {
                if (b0[i] != b1[i])
                {
                    return false;
                }
            }

            return true;
        }
        /// <summary>
        /// Метод хеширует пароль используя Rfc2898DeriveBytes
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string EncryptHashPassword(string password)
        {
            byte[] salt;
            byte[] buffer;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 16, 1000))
            {
                salt = bytes.Salt;
                buffer = bytes.GetBytes(32);
            }

            byte[] arrayToReturn = new byte[49];
            Buffer.BlockCopy(salt, 0, arrayToReturn, 1, 16);
            Buffer.BlockCopy(buffer, 0, arrayToReturn, 17, 32);
            return Convert.ToBase64String(arrayToReturn);

        }
        /// <summary>
        /// Метод сравнения пароля пользователя и пароля-хеша в БД
        /// </summary>
        /// <param name="hashpassword"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool VerifyHashPassword(string hashpassword, string password)
        {
            byte[] buffer1;
            if (hashpassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            byte[] passbytes = Convert.FromBase64String(hashpassword);
            if ((passbytes.Length != 49) || (passbytes[0] != 0))
            {
                return false;
            }
            byte[] n_array = new byte[16];
            Buffer.BlockCopy(passbytes, 1, n_array, 0, 16);
            byte[] buffer2 = new byte[32];
            Buffer.BlockCopy(passbytes, 17, buffer2, 0, 32);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, n_array, 1000))
            {
                buffer1 = bytes.GetBytes(32);
            }
            return ByteArraysEqual(buffer2, buffer1);
        }
    }


}
