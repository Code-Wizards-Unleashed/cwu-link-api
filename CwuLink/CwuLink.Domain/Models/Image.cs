namespace CwuLink.Domain.Models;

public class Image
{
    public string Url { get; init; }
    public Post Post { get; init; }

    public Image(string url, Post post)
    {
        Url = url;
        Post = post;
    }
}