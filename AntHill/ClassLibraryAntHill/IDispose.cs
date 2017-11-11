using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryAntHill
{
    public delegate void DisposeMethod(IDispose _this);
    public interface IDispose
    {
        DisposeMethod Dispose { get; set; }
    }
}
