using Ardalis.Specification.EntityFrameworkCore;
using Core.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Infrastructure.Data
{
    public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class
    {
        private readonly IMemoryCache _cache;
        private readonly MemoryCacheEntryOptions _cacheOptions;

        public EfRepository(AppDbContext dbContext, IMemoryCache cache) : base(dbContext)
        {
            _cache = cache;
            _cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(relative: TimeSpan.FromSeconds(20));
        }

        public override Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            var key = $"{entity.GetType().FullName}-bust";
            _cache.Set(key, "bust", _cacheOptions);
            return base.AddAsync(entity, cancellationToken);
        }

        public override Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            var key = $"{entity.GetType().FullName}-bust";
            _cache.Set(key, "bust", _cacheOptions);
            return base.UpdateAsync(entity, cancellationToken);
        }

        public override Task DeleteAsync(T entity, CancellationToken cancellationToken = default)
        {
            var key = $"{entity.GetType().FullName}-bust";
            _cache.Set(key, "bust", _cacheOptions);
            return base.DeleteAsync(entity, cancellationToken);
        }
    }
}