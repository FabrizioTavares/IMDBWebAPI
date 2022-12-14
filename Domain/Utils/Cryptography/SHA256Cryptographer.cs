using System.Security.Cryptography;
using System.Text;

namespace Domain.Utils.Cryptography;

public class SHA256Cryptographer : ICryptographer
{

    private readonly HashAlgorithm _hashAlgorithm;
    private readonly RandomNumberGenerator _randomNumberGenerator;


    public SHA256Cryptographer()
    {
        _hashAlgorithm = SHA256.Create();
        _randomNumberGenerator = RandomNumberGenerator.Create();
    }

    public byte[] Hash(string rawText, byte[] salt)
    {
        var rawBytes = Encoding.UTF8.GetBytes(rawText);
        var combinedBytes = rawBytes.Concat(salt).ToArray();
        var hash = _hashAlgorithm.ComputeHash(combinedBytes);
        return hash;
    }

    public bool Verify(string rawText, byte[] hashedText, byte[] salt)
    {
        var hash = Hash(rawText, salt);
        return hashedText.SequenceEqual(hash);
    }

    public byte[] GenerateSalt()
    {
        var salt = new byte[8];
        _randomNumberGenerator.GetBytes(salt);
        return salt;
    }
}