using System.Text;
using StackOverflowWebApi.Migrations;

namespace ApiFrontEnd.Utils
{
    public static class Encryption
    {
        private static int _key =5;

        public static string Encrypt(this string incode)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var ch in incode)
            {
                sb.Append((char)(ch + _key));
            }

            return sb.ToString();

        }

        public static string Decrypt(this string incode)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var ch in incode)
            {
                sb.Append((char)(ch - _key));
            }

            return sb.ToString();

        }
    }
}