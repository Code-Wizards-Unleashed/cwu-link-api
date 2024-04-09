using CwuLink.Models;
using FluentAssertions;
using System;

namespace CwuLink.Tests;

public class PersonTests
{
    [Fact]
    public void OnAddNewFriendFromConsumerClass_ShouldThrowException()
    {
        // Arrange
        var person = new Person("Michael", 27);
        List<Person> anotherPerson = person.Friends as List<Person>;

        // Act
        var addingFriend = () => anotherPerson.Add(new Person("Tom", 22));

        // Assert
        Assert.ThrowsAny<Exception>(addingFriend);
    }

    [Fact]
    public void AddFriend_OnAddingOneFriend_PersonFriendsCountShouldBeOne()
    {
        // Arrange
        var person = new Person("Michael", 27);
        var anoherPerson = new Person("Tom", 28);

        // Act
        person.AddFriend(anoherPerson);

        // Assert
        person.Friends.Count.Should().Be(1);
    }

    [Fact]
    public void AddFriend_OnAddingOneFriend_AnotherPersonFriendsShouldBeIncreasedByOne()
    {
        // Arrange
        var person = new Person("Michael", 27);
        var anotherPerson = new Person("Tom", 28);
        var initialAnotherPersonFriendsCount = anotherPerson.Friends.Count;

        // Act
        person.AddFriend(anotherPerson);

        // Assert
        (anotherPerson.Friends.Count - initialAnotherPersonFriendsCount).Should().Be(1);
    }

    [Fact]
    public void SendFriendshipRequest_ToAnotherPerson_ShouldAddFriendshipRequest()
    {
        // Arrange
        var person = new Person("Michael", 27);
        var anotherPerson = new Person("Tom", 28);
        var initialAnotherPersonFriendshipRequestsCount = anotherPerson.FriendshipRequests.Count;

        // Act
        person.SendFriendshipRequest(anotherPerson);

        // Assert
        (anotherPerson.FriendshipRequests.Count - initialAnotherPersonFriendshipRequestsCount).Should().Be(1);
    }



    [Fact]
    public void SendFriendshipRequest_ToAnotherPerson_ShouldAddFriendshipRequestFromCorrespondPerson()
    {
        // Arrange
        var person = new Person("Michael", 27);
        var anotherPerson = new Person("Tom", 28);

        // Act
        person.SendFriendshipRequest(anotherPerson);

        // Assert
        anotherPerson.FriendshipRequests.FirstOrDefault().Sender.Should().Be(person);
    }
}