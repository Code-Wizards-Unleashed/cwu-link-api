using AutoFixture;
using CwuLink.Domain.Models;
using FluentAssertions;

namespace CwuLink.Tests;

public class BlogTests
{
    [Fact]
    public void CreateNewPost_ShouldAddOnePost()
    {
        // Arrange
        var fixture = new Fixture();
        var person = fixture.Create<Person>();
        var blog = new Blog(person);
        var post = fixture.Create<Post>();
        var initialPostsCount = blog.Posts.Count();

        // Act
        blog.CreateNewPost(post);

        // Assert
        (blog.Posts.Count - initialPostsCount).Should().Be(1);
    }

    [Fact]
    public void DeletePost_OnAnyPostsInBlog_ShouldDeleteOnePost()
    {
        // Arrange
        var fixture = new Fixture();
        var person = fixture.Create<Person>();
        var blog = new Blog(person);
        var post = fixture.Create<Post>();
        blog.CreateNewPost(post);
        var initialPostsCount = blog.Posts.Count();

        // Act
        blog.DeletePost(post);

        // Assert
        (initialPostsCount - blog.Posts.Count).Should().Be(1);
    }
}
