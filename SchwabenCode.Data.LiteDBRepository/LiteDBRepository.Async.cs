using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchwabenCode.Data.LiteDBRepository
{
    public partial class LiteDBRepository<TEntity, TIdentifier>
    {
        /// <summary>
        /// Adds given entity to the current collection.
        /// </summary>
        /// <remarks>Id must be new.</remarks>
        /// <returns>Returns passed entity.</returns>
        public Task<TEntity> AddAsync( TEntity entity )
        {
            return AsyncAll.AsyncAll.GetAsyncResult( () => Add( entity ) );
        }

        /// <summary>
        /// Returns number of entires in current collection.
        /// </summary>
        public Task<long> CountAsync()
        {
            return AsyncAll.AsyncAll.GetAsyncResult( () => Count() );
        }

        /// <summary>
        /// Returns all entries of current collection
        /// </summary>
        public Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return AsyncAll.AsyncAll.GetAsyncResult( () => GetAll() );
        }

        /// <summary>
        /// Returns entity with given id
        /// </summary>
        public Task<TEntity> GetAsync( TIdentifier id )
        {
            return AsyncAll.AsyncAll.GetAsyncResult( () => Get( id ) );
        }

        /// <summary>
        /// Removes given entity by id
        /// </summary>
        public Task RemoveAsync( TIdentifier id )
        {
            return AsyncAll.AsyncAll.ExecuteAsync( () => Remove( id ) );
        }

        /// <summary>
        /// Removes given entity
        /// </summary>
        public Task RemoveAsync( TEntity entity )
        {
            return AsyncAll.AsyncAll.ExecuteAsync( () => Remove( entity ) );
        }

        /// <summary>
        /// Updates given entity
        /// </summary>
        public Task<TEntity> UpdateAsync( TEntity entity )
        {
            return AsyncAll.AsyncAll.GetAsyncResult( () => Update( entity ) );
        }

    }
}
