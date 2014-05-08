using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mymdb.Core.BusinessLayer
{
    public class BusinessEntityBase : Interfaces.IBusinessEntity
    {
        public int Id { get; set; }
    }
}
