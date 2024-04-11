using AutoFixture;
using CwuLink.Domain.Models;
using FluentAssertions;

namespace CwuLink.Tests;

public class PersonTests
{
    private readonly Fixture fixture;

    public PersonTests()
    {
        this.fixture = new Fixture();
    }

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

    [Fact]
    public void AddFriend_OnAddingOneFriend_PersonFriendsCountShouldBeOne()
    {
        // Arrange
        var person = new Person("Michael", 27);
        var anotherPerson = new Person("Tom", 28);
        person.SendFriendshipRequest(anotherPerson);
        var friendshipRequest = anotherPerson.FriendshipRequests.FirstOrDefault();

        // Act
        anotherPerson.AddFriend(friendshipRequest);

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
        person.SendFriendshipRequest(anotherPerson);
        var friendshipRequest = anotherPerson.FriendshipRequests.FirstOrDefault();

        // Act
        anotherPerson.AddFriend(friendshipRequest);

        // Assert
        (anotherPerson.Friends.Count - initialAnotherPersonFriendsCount).Should().Be(1);
    }

    [Fact]
    public void AddFriend_OnAddingOneFriend_AnotherPersonFriendshipRequestsShouldBeDecremented()
    {
        // Arrange
        var person = new Person("Michael", 27);
        var anotherPerson = new Person("Tom", 28);
        person.SendFriendshipRequest(anotherPerson);
        var friendshipRequest = anotherPerson.FriendshipRequests.FirstOrDefault();
        var initialAnotherPersonFriendshipRequests = anotherPerson.FriendshipRequests.Count;

        // Act
        anotherPerson.AddFriend(friendshipRequest);

        // Assert
        (anotherPerson.FriendshipRequests.Count - initialAnotherPersonFriendshipRequests).Should().Be(-1);
    }

    [Fact]
    public void PublishPost_OnAddingNewPost_PostsCountInBlogShouldIncremented()
    {
        // Arrange
        var person = this.fixture.Create<Person>();
        var post = this.fixture.Create<Post>();
        var initialPostsCount = person.Blog.Posts.Count;

        // Act
        person.PublishPost(post);

        // Assert
        (person.Blog.Posts.Count - initialPostsCount).Should().Be(1);
    }

    [Fact]
    public void DeletePost_OnDeletingPost_PostsCountInBlogShouldDecremeneted()
    {
        // Arrange
        var person = this.fixture.Create<Person>();
        var post = this.fixture.Build<Post>()
            .With(p => p.Author, person)
            .Create();

        person.PublishPost(post);
        var initialPostsCount = person.Blog.Posts.Count;

        // Act
        person.DeletePost(post);

        // Assert
        (initialPostsCount - person.Blog.Posts.Count).Should().Be(1);
    }

    [Fact]
    public void DeletePost_OnDeletingForeignPost_NotDeleteAnyPostsAndThrowsArgumentException()
    {
        // Arrange
        var anotherPerson = this.fixture.Create<Person>();
        var person = this.fixture.Create<Person>();
        var post = this.fixture.Create<Post>();
        anotherPerson.PublishPost(post);
        var initialPostsCount = anotherPerson.Blog.Posts.Count;

        // Act
        var action = () => person.DeletePost(post);

        // Assert
        (initialPostsCount - anotherPerson.Blog.Posts.Count).Should().Be(0);
        action.Should().Throw<ArgumentException>();

    }

    [Fact]
    public void AddComment_OnAnotherBlog_ShouldAddCommentToAnotherPersonPost()
    {
        // Arrange
        var person = this.fixture.Create<Person>();
        var anotherPerson = this.fixture.Create<Person>();

        var post = this.fixture.Create<Post>();
        anotherPerson.PublishPost(post);

        var comment = this.fixture.Create<Comment>();

        // Act
        person.AddComment(post, comment);

        // Assert
        post.Comments.Count.Should().Be(1);
    }

    [Fact]
    public void DeleteComment_OnAnotherBlog_ShouldDeleteCommentFromAnotherPersonPost()
    {
        // Arrange
        var person = this.fixture.Create<Person>();
        var anotherPerson = this.fixture.Create<Person>();

        var post = this.fixture.Create<Post>();
        anotherPerson.PublishPost(post);

        var comment = new Comment(person, "text");
        person.AddComment(post, comment);

        // Act
        person.DeleteComment(post, comment);

        // Assert
        post.Comments.Count.Should().Be(0);
    }

    [Fact]
    public void DeleteComment_OnAnotherPersonComment_ShouldThrowArgumentException()
    {
        // Arrange
        var person = this.fixture.Create<Person>();
        var anotherPerson = this.fixture.Create<Person>();

        var post = this.fixture.Create<Post>();
        anotherPerson.PublishPost(post);

        var comment = this.fixture.Create<Comment>();
        anotherPerson.AddComment(post, comment);

        // Act
        var action = () => person.DeleteComment(post, comment);

        // Assert
        post.Comments.Count.Should().Be(1);
        action.Should().Throw<ArgumentException>();
    }
}