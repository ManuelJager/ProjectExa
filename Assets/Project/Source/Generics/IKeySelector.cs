using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exa.Generics
{
    public interface IKeySelector<T>
    {
        T Key { get; }
    }
}
