using System.Security.Cryptography;
using System.Text;

namespace Domain.Utils.Cryptography
{
    public class SHA256Cryptographer : ICryptographer
    {

        private readonly HashAlgorithm _hashAlgorithm;

        public SHA256Cryptographer()
        {
            _hashAlgorithm = SHA256.Create();
        }

        public string Hash(string password)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password);
            byte[] hash = _hashAlgorithm.ComputeHash(bytes);
            return Convert.ToHexString(hash);
        }


        public bool Verify(string password, string hash)
        {
            string newHash = Hash(password);
            return newHash.Equals(hash);
        }
    }
}
