using SchwabenCode.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchwabenCode.Data.LiteDBRepository
{
    /// <summary>
    /// Storage file
    /// </summary>
    /// <typeparam name="TIdentifier"></typeparam>
    public interface IStorageFile
    {
        /// <summary>
        /// Id and Name
        /// </summary>
        String Id { get; set; }

        /// <summary>
        /// File contents
        /// </summary>
        Byte[ ] Content { get; set; }
    }

}