using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Wiki.Api.Data.Utils
{
    public class Criptografia
    {
        private static readonly TripleDESCryptoServiceProvider TripleDes = new TripleDESCryptoServiceProvider();
        private static readonly MD5CryptoServiceProvider Md5 = new MD5CryptoServiceProvider();
        private const string SKey = "2021MeetGroupAPI";

        public static byte[] Md5Hash(string sValue)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(sValue);
            return Md5.ComputeHash(bytes);
        }

        public static string Encrypt(string sEncrypt)
        {
            string str;
            try
            {
                TripleDes.Key = Md5Hash(SKey);
                TripleDes.Mode = CipherMode.ECB;
                byte[] bytes = Encoding.ASCII.GetBytes(sEncrypt);
                str = Convert.ToBase64String(TripleDes.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length));
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                str = string.Empty;
                ProjectData.ClearProjectError();
            }
            return str;
        }

        public static string Decrypt(string sEncrypt)
        {
            string empty;
            try
            {
                TripleDes.Key = Md5Hash(SKey);
                TripleDes.Mode = CipherMode.ECB;
                byte[] inputBuffer = Convert.FromBase64String(sEncrypt);
                empty = Encoding.ASCII.GetString(TripleDes.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length));
            }
            catch (Exception ex)
            {
                ProjectData.SetProjectError(ex);
                empty = string.Empty;
                ProjectData.ClearProjectError();
            }
            return empty;
        }
    }
}
