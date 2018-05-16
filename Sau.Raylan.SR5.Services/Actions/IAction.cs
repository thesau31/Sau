using Sau.Raylan.SR5.Contracts.Interfaces;

namespace Sau.Raylan.SR5.Services.Actions
{
    public interface IAction
    {
        string Name { get; }
        ActionResult Do(IDiceBag bag, IHasAttributes source);
    }
}
