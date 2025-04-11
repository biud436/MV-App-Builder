
namespace Cordova.Commands
{
    using Core;

    interface ICommand
    {
        void Execute(Cordova cordova, object[] args);
    }
}
