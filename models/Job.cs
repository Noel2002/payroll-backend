namespace DotNetTest.Models;

public class Job
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Rate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public User User { get; set; }

}
