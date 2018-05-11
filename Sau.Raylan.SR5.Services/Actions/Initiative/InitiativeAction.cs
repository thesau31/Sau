using System;
using Sau.Raylan.SR5.Contracts;

namespace Sau.Raylan.SR5.Services.Actions.Initiative
{
    public class InitiativeAction : IInitiativeAction
    {
        public InitiativeCost InitiativeCost { get; }

        public ActionResult Do(Character source, IDiceBag bag)
        {
            throw new NotImplementedException();
        }
    }
}
