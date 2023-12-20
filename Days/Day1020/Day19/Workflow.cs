using System.Linq;

namespace AdventOfCode2023;

public class Workflow
{
    public string Name { get; set; }
    List<Condition> Conditions { get; set; }

    public Workflow(string input)
    {
        Name = input.Split(new char[] { '{', '}' }, StringSplitOptions.None)[0];
        List<string> conditionsString = input
            .Split(new char[] { '{', '}' }, StringSplitOptions.None)[1]
            .Split(',')
            .ToList();
        Conditions = conditionsString
            .Select<string, Condition>(conditionString => 
            { 
                string[] conditionParts = conditionString.Split(new char[] { '>' , '<', ':'});
                if (conditionString.Contains('<'))
                {
                    return new SmallerThanCondition(int.Parse(conditionParts[1]), conditionParts[0], conditionParts[2]);
                }
                if (conditionString.Contains('>'))
                {
                    return new GreaterThanCondition(int.Parse(conditionParts[1]), conditionParts[0], conditionParts[2]);
                }
                return new PassCondition(conditionParts[0]);
            })
            .ToList();
    }

    public string Execute(Part part)
    {
        for (int i = 0; i < Conditions.Count; i++)
        {
            String? newPosition = Conditions[i].Execute(part);
            if (newPosition != null)
            {
                return newPosition;
            }
        }
        return null;
    }

}
abstract class Condition
{
    public int Threshold { get; set; }
    public string Destination { get; set; }
    public string Target { get; set; }
    public abstract string? Execute(Part part);
    public Condition(int  threshold, string target, string destination)
    {
        this.Threshold = threshold;
        this.Target = target;
        this.Destination = destination;
    }

    public int GetValueToUse(Part part)
    {
        if (Target == "x")
            return part.XValue;
        if (Target == "m")
            return part.MValue;
        if (Target == "a")
            return part.AValue;
        if (Target == "s")
            return part.SValue;
        throw new ArgumentException($"Unsupported target: {Target}");

    }
}

class SmallerThanCondition : Condition
{
    public SmallerThanCondition(int threshold, string target, string destination) : base(threshold, target, destination)
    {
    }

    public override string? Execute(Part part)
    {
        if (GetValueToUse(part) < Threshold)
        {
            return Destination;
        }
        return null;
    }
}
class GreaterThanCondition : Condition
{
    public GreaterThanCondition(int threshold, string target, string destination) : base(threshold, target, destination)
    {
    }

    public override string? Execute(Part part)
    {
        if (GetValueToUse(part) > Threshold)
        {
            return Destination;
        }
        return null;
    }
}
class PassCondition : Condition
{
    public PassCondition(string destination) : base(-1, "", destination)
    {
    }

    public override string? Execute(Part part)
    {
        return Destination;
    }
}
