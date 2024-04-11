namespace CwuLink.Domain.Models;

public class Post
{
    private List<Comment> _сomments;

    public Person Author { get; init; }

    public string Text { get; private set; }

    public Image Image { get; private set; }

    public DateTime CreatedDateTime { get; init; }

    public IReadOnlyCollection<Comment> Comments => _сomments.AsReadOnly();

    public Post(Person author, string text) : this(author, text, null) { }

    public Post(Person author,string text, Image image)
    {
        Author = author;
        Text = text;
        Image = image;
        _сomments = new List<Comment>(); 
        CreatedDateTime = DateTime.Now;
    }

    public void AddComment(Comment comment)
    {
        _сomments.Add(comment);
    }

    public void DeleteComment(Comment comment)
    {
        _сomments.Remove(comment);
    }
}