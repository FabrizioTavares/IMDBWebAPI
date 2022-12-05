namespace Domain.Utils.Cryptography
{
    public interface ICryptographer
    {
        string Hash(string password);
        bool Verify(string password, string hash);
    }
}
