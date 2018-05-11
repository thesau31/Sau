using Sau.Raylan.SR5.Contracts;

namespace Sau.Raylan.SR5.Services.Actions.Initiative
{
    public class BlockAction : IInitiativeAction
    {
        private readonly InitiativeCost _cost = new InitiativeCost() { Cost = 5, IsCostRequired = true };
        public InitiativeCost InitiativeCost { get { return _cost; } }

        public ActionResult Do(Character source, IDiceBag bag)
        {
            var times = source.Reaction + source.Intuition; // + GYMNASTICS
            var pool = new DicePool(bag, times);
            var results = pool.Roll(); // limited by physical

            return new ActionResult("REA + INT + Gymnastics [Physical]", results);
        }
    }
}
