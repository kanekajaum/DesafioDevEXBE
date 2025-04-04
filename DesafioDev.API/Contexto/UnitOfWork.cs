namespace DesafioDev.API.Contexto
{
    using System.Threading.Tasks;
    using global::DesafioDev.API.Interfaces.DesafioDev.API.UoW;

    namespace DesafioDev.API.UoW
    {
        public class UnitOfWork : IUnitOfWork
        {
            private readonly ApplicationDbContext _context;

            public UnitOfWork(ApplicationDbContext context)
            {
                _context = context;
            }

            public async Task CommitAsync()
            {
                await _context.SaveChangesAsync();
            }
        }
    }
}