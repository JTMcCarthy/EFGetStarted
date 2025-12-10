using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

using var db = new BloggingContext();

// Note: This sample requires the database to be created before running.
Console.WriteLine($"Database path: {db.DbPath}.");

// Create
Console.WriteLine("Inserting a new blog");
db.Add(new Blog { Url = "http://blogs.msdn.com/adonet", CreatedAt=DateTime.UtcNow });
await db.SaveChangesAsync();

// Read
Console.WriteLine("Querying for a blog");

var blog = await db.Blogs.OrderBy(b => b.BlogId).FirstAsync();

// Update
Console.WriteLine("Updating the blog and adding a post");
blog.Url = "https://devblogs.microsoft.com/dotnet";
blog.Posts.Add(
    new Post { Title = DateTime.UtcNow.ToString(), Content = "I wrote an app using EF Core!" });
await db.SaveChangesAsync();

await db.Blogs
    .Include(b => b.Posts)
    .OrderBy(b => b.BlogId)
    .ForEachAsync(b => Console.WriteLine(b.ToString()));

// Delete
//Console.WriteLine("Delete the blog");
//db.Remove(blog);
await db.SaveChangesAsync();