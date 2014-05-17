using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;

namespace Mymdb.Core.BusinessLayer
{
    public class BusinessEntityBase : Interfaces.IBusinessEntity
    {
        [PrimaryKey]
        public int Id { get; set; }
    }
}
