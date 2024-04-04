
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
    }

    public string Name { get; private set; }

    public int Age { get; private set; }

    public Blog Blog { get; private set; }

    public IReadOnlyCollection<Person> Friends => _friends.AsReadOnly();

    public IReadOnlyCollection<FriendshipRequest> FriendshipRequests => _friendshipRequests.AsReadOnly();

    public void AddFriend(Person friend)
    {
        _friends.Add(friend);
        friend._friends.Add(this);
    }

    public void SendFriendshipRequest(Person person)
    {

    }
}