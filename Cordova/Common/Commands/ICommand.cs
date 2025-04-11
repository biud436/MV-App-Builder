using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cordova.Commands
{
    using Core;

    interface ICommand
    {
        void Execute(Cordova cordova, object[] args);
    }
}
