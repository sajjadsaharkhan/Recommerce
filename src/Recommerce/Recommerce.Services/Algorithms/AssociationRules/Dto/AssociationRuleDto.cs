namespace Recommerce.Services.Algorithms.AssociationRules.Dto;

public class AssociationRuleDto
{
    public ItemSetDto Antecedent { get; }
    public ItemSetDto Consequent { get; }
    public double Confidence { get; }
    
    public AssociationRuleDto(ItemSetDto antecedent, ItemSetDto consequent, double confidence)
    {
        Antecedent = antecedent;
        Consequent = consequent;
        Confidence = confidence;
    }
}