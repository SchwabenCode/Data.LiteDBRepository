using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchwabenCode.Data.LiteDBRepository
{
    /// <summary>
    /// Context of LiteDB
    /// </summary>
    public interface ILiteDBContext
    {
        /// <summary>
        /// Current used Database
        /// </summary>
        LiteDatabase Database { get; set; }
    }
}
