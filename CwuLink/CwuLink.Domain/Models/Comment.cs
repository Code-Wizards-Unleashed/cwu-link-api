namespace CwuLink.Domain.Models;

public class Comment
{
    public Comment(Person author, string text)
    {
        Author = author;
        Text = text;
        CreatedDateTime = DateTime.Now;
    }

    public Person Author { get; init; }

    public string Text { get; private set; }

    public DateTime CreatedDateTime { get; init; }
}