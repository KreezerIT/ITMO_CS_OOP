namespace Itmo.ObjectOrientedProgramming.Lab2.Repository;

public interface IRepository<T>
{
    public void Add(T newObject);

    public T GetById(Guid id);

    public IEnumerable<T> GetAll();
}