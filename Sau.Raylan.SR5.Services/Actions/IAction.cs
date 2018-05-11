using Sau.Raylan.SR5.Contracts;

namespace Sau.Raylan.SR5.Services.Actions
{
    public interface IAction
    {
        ActionResult Do(Character source, IDiceBag bag);
    }
}
