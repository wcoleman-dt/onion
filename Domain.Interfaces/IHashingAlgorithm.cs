namespace Domain.Interfaces
{
    public interface IHashingAlgorithm
    {
        byte[] ComputeHash(byte[] membersToHash);
    }
}
