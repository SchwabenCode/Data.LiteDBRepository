using LiteDB;
using SchwabenCode.Data.Entity;
using SchwabenCode.Data.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;

namespace SchwabenCode.Data.LiteDBRepository
{
    public class LiteDBStorageRepository<TFile> where TFile : class, IStorageFile, new()
    {
        private LiteFileStorage _fileStorage;

        /// <summary>
        /// Creates a new instance of <see cref="LiteDBStorageRepository"/> attached to the given context
        /// </summary>
        /// <remarks>Limit per file is 2 GB</remarks>
        public LiteDBStorageRepository( ILiteDBContext dbContext )
        {
            Contract.Requires( dbContext != null );

            _fileStorage = dbContext.Database.FileStorage;
        }

        /// <summary>
        /// Adds given file and uses Id property as key
        /// </summary>
        public TFile Add( TFile file )
        {
            _fileStorage.Upload( file.Id, new MemoryStream( file.Content ) );
            return file;
        }

        /// <summary>
        /// Reads file from storage
        /// </summary>
        /// <param name="idOrName">Name or Id</param>
        /// <returns>Null if not found otherwise filled <see cref="TFile"/></returns>
        public TFile Get( String idOrName )
        {
            LiteFileInfo fileInfo = _fileStorage.FindById( idOrName );

            if( fileInfo == null )
            {
                return null;
            }

            return fileInfo.ToStorageFile<TFile>();
        }

        /// <summary>
        /// Returns all files in storage
        /// </summary>
        public IEnumerable<TFile> GetAll()
        {
            foreach( LiteFileInfo fileInfo in _fileStorage.FindAll() )
            {
                yield return fileInfo.ToStorageFile<TFile>();
            }
        }

        /// <summary>
        /// Removes file with given id
        /// </summary>
        /// <return>true of remove succeded</return>
        public bool Remove( String id )
        {
            return _fileStorage.Delete( id );
        }

        /// <summary>
        /// Removes file
        /// </summary>
        /// <return>true of remove succeded</return>
        public bool Remove( TFile file )
        {
            return Remove( file.Id );
        }

        /// <summary>
        /// Updates given file
        /// </summary>
        public TFile Update( TFile file )
        {
            return Add( file );
        }
    }
}
