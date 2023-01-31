namespace DevMedical.Exceptions;

public sealed class DevMedicalException : Exception
{
    public DevMedicalException(string message) : base(message)
    {
    }
}