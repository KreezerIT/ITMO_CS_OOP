namespace Itmo.ObjectOrientedProgramming.Lab2.Repository;

public abstract class BaseRepository<T> : IRepository<T>
{
    private readonly List<T> items = new();

    public virtual void Add(T newObject)
    {
        if (newObject == null)
        {
            throw new ArgumentNullException(nameof(newObject), $"{typeof(T).Name} is null.");
        }

        items.Add(newObject);
    }

    public virtual T GetById(Guid id)
    {
        T? item = items.FirstOrDefault(item => GetId(item) == id);

        return item ?? throw new KeyNotFoundException($"{typeof(T).Name} not found in repository.");
    }

    public virtual IEnumerable<T> GetAll()
    {
        return items;
    }

    protected abstract Guid GetId(T item);
}