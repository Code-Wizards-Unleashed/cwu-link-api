namespace CwuLink.Models;

public class Blog
{
    public Blog(Person person)
    {
        Person = person;
        _posts = new List<Post>();
    }

    public List<Post> _posts;

    public Person Person { get; set; }

    public IReadOnlyCollection<Post> Posts => _posts.AsReadOnly();

    public void CreateNewPost(Post post)
    {
        _posts.Add(post);
    }
}