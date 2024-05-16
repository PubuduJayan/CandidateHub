using Ardalis.Specification;
using Core.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class CachedRepository<T> : IReadRepository<T> where T : class
    {
        private readonly IMemoryCache _cache;
        private readonly ILogger<CachedRepository<T>> _logger;
        private readonly IRepository<T> _sourceRepository;
        private MemoryCacheEntryOptions _cacheOptions;

        public CachedRepository(IMemoryCache cache,
            ILogger<CachedRepository<T>> logger,
            IRepository<T> sourceRepository)
        {
            _cache = cache;
            _logger = logger;
            _sourceRepository = sourceRepository;

            _cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(relative: TimeSpan.FromSeconds(20));
        }

        /// <inheritdoc/>
        public Task<bool> AnyAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
        {
            // TODO: Add Caching
            return _sourceRepository.AnyAsync(specification, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<bool> AnyAsync(CancellationToken cancellationToken = default)
        {
            // TODO: Add Caching
            return _sourceRepository.AnyAsync(cancellationToken);
        }

        public IAsyncEnumerable<T> AsAsyncEnumerable(ISpecification<T> specification)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<int> CountAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
        {
            // TODO: Add Caching
            return _sourceRepository.CountAsync(specification, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<int> CountAsync(CancellationToken cancellationToken = default)
        {
            // TODO: Add Caching
            return _sourceRepository.CountAsync(cancellationToken);
        }

        public Task<T?> FirstOrDefaultAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
        {
            if (specification.CacheEnabled)
            {
                string key = $"{specification.CacheKey}-GetBySpecAsync";
                _logger.LogInformation("Checking cache for " + key);

                return _cache.GetOrCreate(key, entry =>
                {
                    entry.SetOptions(_cacheOptions);
                    _logger.LogInformation("Fetching source data for " + key);
                    return _sourceRepository.FirstOrDefaultAsync(specification, cancellationToken);
                });
            }
            return _sourceRepository.FirstOrDefaultAsync(specification, cancellationToken);
        }

        public Task<TResult?> FirstOrDefaultAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<T> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return _sourceRepository.GetByIdAsync(id, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<T> GetByIdAsync<TId>(TId id, CancellationToken cancellationToken = default)
        {
            return _sourceRepository.GetByIdAsync(id, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<T> GetBySpecAsync<Spec>(Spec specification, CancellationToken cancellationToken = default) where Spec : ISingleResultSpecification, ISpecification<T>
        {
            if (specification.CacheEnabled)
            {
                string key = $"{specification.CacheKey}-GetBySpecAsync";
                _logger.LogInformation("Checking cache for " + key);

                return _cache.GetOrCreate(key, entry =>
                {
                    entry.SetOptions(_cacheOptions);
                    _logger.LogInformation("Fetching source data for " + key);
                    return _sourceRepository.GetBySpecAsync(specification, cancellationToken);
                });
            }
            return _sourceRepository.GetBySpecAsync(specification, cancellationToken);
        }

        /// <inheritdoc/>
        public Task<TResult> GetBySpecAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<T?> GetBySpecAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> ListAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> ListAsync(ISpecification<T> specification, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<TResult>> ListAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<T?> SingleOrDefaultAsync(ISingleResultSpecification<T> specification, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<TResult?> SingleOrDefaultAsync<TResult>(ISingleResultSpecification<T, TResult> specification, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}