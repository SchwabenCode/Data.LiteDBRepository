using LiteDB;
using SchwabenCode.Data.Entity;
using SchwabenCode.Data.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace SchwabenCode.Data.LiteDBRepository
{
    /// <summary>
    /// Repository for LiteDB
    /// </summary>
    /// <typeparam name="TEntity">Entity</typeparam>
    /// <typeparam name="TIdentifier">Type of entity identifier</typeparam>
    public partial class LiteDBRepository<TEntity, TIdentifier> : IRepository<TEntity, TIdentifier>, IRepositoryAsync<TEntity, TIdentifier>
         where TEntity : IEntity<TIdentifier>, new()
        where TIdentifier : struct
    {
        private LiteCollection<TEntity> _collection;
        private string keyName = "_id";

        /// <summary>
        /// Creates a new instance of <see cref="LiteDBRepository"/> attached to the given context
        /// </summary>
        public LiteDBRepository( ILiteDBContext dbContext, String collectionName )
        {
            Contract.Requires( dbContext != null );
            Contract.Requires( !String.IsNullOrEmpty( collectionName ) );

            _collection = dbContext.Database.GetCollection<TEntity>( collectionName );
        }


        /// <summary>
        /// Checks existance of given id
        /// </summary>
        public bool Exists( TIdentifier id )
        {
            return _collection.Exists( Query.EQ( keyName, new BsonValue( id ) ) );
        }

        /// <summary>
        /// Adds given entity to the current collection.
        /// </summary>
        /// <remarks>Id must be new.</remarks>
        /// <returns>Returns passed entity.</returns>
        public TEntity Add( TEntity entity )
        {
            _collection.Insert( entity );

            return entity;
        }

        /// <summary>
        /// Adds given entity to the current collection.
        /// </summary>
        /// <remarks>Id must be new.</remarks>
        /// <returns>Returns items insered.</returns>
        public Int64 Add( IEnumerable<TEntity> entities )
        {
            return _collection.Insert( entities );
        }

        /// <summary>
        /// Adds given entity to the current collection.
        /// </summary>
        /// <remarks>Id must be new.</remarks>
        /// <returns>Returns items insered.</returns>
        public Int64 AddBulk( IEnumerable<TEntity> entity, int chunkSize = 32768 )
        {
            return _collection.InsertBulk( entity, chunkSize );
        }

        /// <summary>
        /// Returns number of entires in current collection.
        /// </summary>
        public long Count()
        {
            return _collection.Count();
        }

        /// <summary>
        /// Returns entity with given id
        /// </summary>
        public TEntity Get( TIdentifier id )
        {
            return _collection.FindById( new BsonValue( id ) );
        }

        /// <summary>
        /// Returns all entries of current collection
        /// </summary>
        public IEnumerable<TEntity> GetAll()
        {
            return _collection.FindAll();
        }

        /// <summary>
        /// Removes given entity
        /// </summary>
        public void Remove( TEntity entity )
        {
            Remove( entity.Id );
        }

        /// <summary>
        /// Removes given entity by id
        /// </summary>
        public void Remove( TIdentifier id )
        {
            _collection.Delete( new BsonValue( id ) );
        }

        /// <summary>
        /// Updates given entity
        /// </summary>
        public TEntity Update( TEntity entity )
        {
            _collection.Update( entity );

            return entity;
        }
    }
}
