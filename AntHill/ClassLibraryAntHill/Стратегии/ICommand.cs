using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibraryAntHill
{
    interface ICommand
    {
        IObjectField place { get; set; }
        bool Execute();
    }
}
