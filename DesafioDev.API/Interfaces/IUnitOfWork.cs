namespace DesafioDev.API.Interfaces
{
    using System.Threading.Tasks;

    namespace DesafioDev.API.UoW
    {
        public interface IUnitOfWork
        {
            Task CommitAsync();
        }
    }

}
