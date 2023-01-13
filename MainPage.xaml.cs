using MauiSqliteClassLibrary.Models;
using MauiSqliteClassLibrary;
using Microsoft.EntityFrameworkCore;

namespace MauiSqlite;

public partial class MainPage : ContentPage
{
	int counter = 0;
    private readonly ApplicationDbContext _context;

    public MainPage(ApplicationDbContext context)
	{
		InitializeComponent();
        _context = context;
    }

	private async void OnAddToDbClicked(object sender, EventArgs e)
    {
		counter++;
        var newPost = new Post
        {
            Title = $"Post Number {counter}",
            Content = $"Content for Post: {counter}"
        };

        try
        {
            await _context.Posts.AddAsync(newPost);
            await _context.SaveChangesAsync();
            ResultEditor.Text = $"Post {counter} Added successfully";
        }
        catch (Exception ex)
        {
            ResultEditor.Text = ex.Message;
        }
    }
    private async void OnReadFromDbClicked(object sender, EventArgs e)
    {

        try
        {

            var posts = await _context.Posts.ToListAsync();
            var text = "";
            foreach (var post in posts)
            {
                text += $"Id:{post.Id} {post.Title} Content: {post.Content}" + Environment.NewLine;
            }
            ResultEditor.Text = text;
        }
        catch (Exception ex)
        {
            ResultEditor.Text = ex.Message;
        }
    }
    private void OnClearTableClicked(object sender, EventArgs e)
    {

        try
        {

            _context.Posts.RemoveRange(_context.Posts.ToList());
            _context.SaveChanges();
            ResultEditor.Text = "Table Cleared";
        }
        catch (Exception ex)
        {
            ResultEditor.Text = ex.Message;
        }
    }
}

