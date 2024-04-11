using AutoFixture;
using CwuLink.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CwuLink.Tests;

public class BlogTests
{
    [Fact]
    public void CreateNewPost_OnNoPostsInBlog_ShouldAddOnePost()
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
}
