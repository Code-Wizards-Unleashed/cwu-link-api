
using System.Collections.Generic;

namespace CwuLink.Models;

public class Person
{
    private List<Person> _friends;
    private List<FriendshipRequest> _friendshipRequests;

    public Person(string name, int age)
    {
        Name = name;
        Age = age;
        Blog = new Blog();
        _friends = new List<Person>();
        _friendshipRequests = new List<FriendshipRequest>();
    }

    public string Name { get; private set; }

    public int Age { get; private set; }

    public Blog Blog { get; private set; }

    public IReadOnlyCollection<Person> Friends => _friends.AsReadOnly();

    public IReadOnlyCollection<FriendshipRequest> FriendshipRequests => _friendshipRequests.AsReadOnly();

    public void SendFriendshipRequest(Person person)
    {
        person._friendshipRequests.Add(new FriendshipRequest(this));
    }
    
    public void AddFriend(FriendshipRequest friendShipRequest)
    {
        var friend = friendShipRequest.Sender;
        _friends.Add(friend);
        friend._friends.Add(this);
        _friendshipRequests.Remove(friendShipRequest);
    }
}