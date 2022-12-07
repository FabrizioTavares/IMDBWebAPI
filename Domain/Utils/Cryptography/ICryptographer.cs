namespace Domain.Utils.Cryptography
{
    public interface ICryptographer
    {
        byte[] Hash(string rawText, byte[] salt);
        bool Verify(string rawText, byte[] hashedText, byte[] salt);
    }
}
