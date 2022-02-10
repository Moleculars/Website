namespace Bb.Identity
{
    public interface IRepository<T>
        where T : class
    {

        T GetNew();

        Task<bool> SaveNew(T item);


    }

}
