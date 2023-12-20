namespace AdventOfCode2023; 

public class Day19: Day
{
    public Day19(string input) : base(input) { }

    public override string ExecuteA()
    {
        List<string> lines = new FileUtils(input).GetLines();
        bool doesWorkflows = true;
        List<Workflow> workflows = new();
        List<Part> parts = new();
        while (lines.Count > 0)
        {
            string line = lines[0];
            lines.RemoveAt(0);
            if (line.Count() > 0)
            {
                if (doesWorkflows)
                {
                    workflows.Add(new Workflow(line));
                }
                else
                {
                    parts.Add(new Part(line));
                }
            }
            else
            {
                doesWorkflows = false;
            }
        }
        Dictionary<Part, string> destinationDirectory = new();
        foreach (Part part in parts)
        {
            Workflow workflow = workflows.First(workflow => workflow.Name == "in");
            string workflowName = "in";
            while (workflowName != "A" && workflowName != "R")
            {
                workflowName = workflow.Execute(part);
                workflow = workflows.FirstOrDefault(workflow => workflow.Name == workflowName);
            }
            destinationDirectory.Add(part, workflowName);
        }


        List<Part> accepted = destinationDirectory.Keys
            .Where(part => destinationDirectory[part] == "A")
            .ToList();
        return accepted
            .Select(part => part.Sum())
            .Sum()
            .ToString();
    }

    public override string ExecuteB()
    {
        throw new NotImplementedException();
    }
}
