namespace CarDealer.Repository.Interfaces
{
    public interface IUnitOfWork
    {

        IMakeRepository Make { get; }

        void Save();

    }
}
