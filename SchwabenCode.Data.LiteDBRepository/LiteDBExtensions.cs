using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LiteDB;
using System.IO;

namespace SchwabenCode.Data.LiteDBRepository
{
    public static class LiteDBExtensions
    {
        public static TFile ToStorageFile<TFile>( this LiteFileInfo fileInfo ) where TFile : class, IStorageFile, new()
        {
            TFile file = new TFile
            {
                Id = fileInfo.Id
            };

            // Read
            using( LiteFileStream fs = fileInfo.OpenRead() )
            using( MemoryStream ms = new MemoryStream() )
            {
                fs.CopyTo( ms );

                file.Content = ms.ToArray();
            }

            return file;
        }
    }
}
