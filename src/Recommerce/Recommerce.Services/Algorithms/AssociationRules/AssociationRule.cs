using Recommerce.Services.Algorithms.AssociationRules.Dto;

namespace Recommerce.Services.Algorithms.AssociationRules;

public class AssociationRule
{
    public ItemSetDto Antecedent { get; }
    public ItemSetDto Consequent { get; }
    public double Confidence { get; }
    
    public AssociationRule(ItemSetDto antecedent, ItemSetDto consequent, double confidence)
    {
        Antecedent = antecedent;
        Consequent = consequent;
        Confidence = confidence;
    }
}