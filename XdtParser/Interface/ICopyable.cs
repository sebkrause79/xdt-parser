namespace XdtParser.Interface;

internal interface ICopyable<T>
{
    T GetClearedCopy();
}