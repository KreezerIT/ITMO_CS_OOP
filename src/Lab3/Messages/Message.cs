using Itmo.ObjectOrientedProgramming.Lab3.Prototype;

namespace Itmo.ObjectOrientedProgramming.Lab3.Messages;

public class Message : IPrototype<Message>
{
    public string Title { get; set; }

    public string Body { get; set; }

    public ImportanceLevel? Importance { get; set; } = ImportanceLevel.Default;

    public override int GetHashCode()
    {
        return HashCode.Combine(Title, Body, Importance);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Message other)
            return false;

        return Title == other.Title &&
               Body == other.Body &&
               Importance == other.Importance;
    }

    public Message(string title, string body, ImportanceLevel? importance = null)
    {
            Title = title;
            Body = body;

            if (importance != null)
                Importance = importance;
    }

    public Message Clone()
    {
        return new Message(Title, Body, Importance);
    }
}