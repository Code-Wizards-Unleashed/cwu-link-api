namespace CwuLink.Models;

public class Blog
{
    public Person Person { get; set; }

    public List<Post> Posts { get; set; }
}