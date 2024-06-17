using LdtParser.Rule;

namespace LdtParser.Structure;
public class FieldEntryType {
    private int _fieldIdentifier;
    private List<IRule> _rules = new();
    private FieldEntryAssumption _assumption = FieldEntryAssumption.NoAssumption;
}