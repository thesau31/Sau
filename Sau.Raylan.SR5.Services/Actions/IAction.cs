using Sau.Raylan.SR5.Contracts;

namespace Sau.Raylan.SR5.Services.Actions
{
    public interface IAction
    {
        string Name { get; }
        ActionResult Do(Character source, IDiceBag bag);
    }
}
