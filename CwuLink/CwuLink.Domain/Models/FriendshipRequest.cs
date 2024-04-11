namespace CwuLink.Domain.Models;

public class FriendshipRequest
{
    public FriendshipRequest(Person sender)
    {
        Sender = sender;
    }
    public Person Sender { get; init; }
}