using AutoFixture;
using CwuLink.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CwuLink.Tests;

public class PostTests
{
    private readonly Fixture fixture;

    public PostTests()
    {
        this.fixture = new Fixture();
    }

    [Fact]
    public void AddComment_ShouldIncreaseCommentsCount()
    {
        // Arrange
        var comment = this.fixture.Create<Comment>();
        var post = this.fixture.Create<Post>();
        var initialCommentsCount = post.Comments.Count;

        // Act 
        post.AddComment(comment);

        // Assert
        (post.Comments.Count - initialCommentsCount).Should().Be(1);
    }

    [Fact]
    public void DeleteComment_ShouldDecreaseCommentsCount()
    {
        // Arrange
        var comment = this.fixture.Create<Comment>();
        var post = this.fixture.Create<Post>();
        post.AddComment(comment);
        var initialCommentsCount = post.Comments.Count;

        // Act 
        post.DeleteComment(comment);

        // Assert
        (initialCommentsCount - post.Comments.Count).Should().Be(1);
    }
}
