namespace WebApplication1.Model
{
    public class Issue
    {
        public Fields fields { get; set; }
        public Issue()
        {
            fields = new Fields();
        }
    }

    public class Fields
    {
        public Project project { get; set; }
        public string summary { get; set; }
        public string description { get; set; }
        public IssueType issuetype { get; set; }
        public Fields()
        {
            project = new Project();
            issuetype = new IssueType();
        }
    }

    public class Project
    {
        public string key { get; set; }
    }

    public class IssueType
    {
        public string name { get; set; }
    }
}
