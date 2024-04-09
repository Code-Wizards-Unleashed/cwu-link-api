namespace CwuLink.Models;

public class FriendshipRequest
{
    public FriendshipRequest(Person sender)
    {
        Sender = sender;
    }
    public Person Sender { get; init; }
}