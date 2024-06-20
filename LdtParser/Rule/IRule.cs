namespace LdtParser.Rule;
internal interface IRule 
{
    string Name { get; }
    ErrorState ErrorState { get; }
    bool Validate();
}