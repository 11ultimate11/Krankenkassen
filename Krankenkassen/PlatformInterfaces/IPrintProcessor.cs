namespace Krankenkassen.PlatformInterfaces;

public interface IPrintProcessor
{
    void Print(Stream stream);
    void Print(string path);
}