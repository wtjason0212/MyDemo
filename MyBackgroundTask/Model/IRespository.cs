using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBackgroundTask.Model
{
    public interface IRespository <T> where T:class
    {
       // IUnitOfWork UnitOfWork { get; }
    }
}
