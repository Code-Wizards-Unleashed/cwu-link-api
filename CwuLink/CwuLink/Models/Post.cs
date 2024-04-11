namespace CwuLink.Models;

public class Post
{
    private List<Comment> _сomments;

    public string Text { get; private set; }

    public Image Image { get; private set; }

    public IReadOnlyCollection<Comment> Comments => _сomments.AsReadOnly();

    public Post(string text)
    {
        Text = text;
        _сomments = new List<Comment>();
    }

    public Post(string text, Image image)
    {
        Text = text;
        Image = image;
        _сomments = new List<Comment>();
    }
}