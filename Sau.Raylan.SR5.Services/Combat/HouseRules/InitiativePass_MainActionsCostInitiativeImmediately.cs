using System.Linq;

namespace Sau.Raylan.SR5.Services.Combat.HouseRules
{
    public class InitiativePass_MainActionsCostInitiativeImmediately : BaseInitiativePass
    {
        public override InitiativePassSlot Next()
        {
            InitiativeOrder.Sort();
            var slot = InitiativeOrder.FirstOrDefault(_leftToAct);
            if (slot != null)
                slot.CurrentInitiative -= 10;
            return slot;
        }

        public override void Reset()
        {
            InitiativeOrder.ForEach(x => x.HasActed = false);
            InitiativeOrder = InitiativeOrder.Where(_leftToAct).ToList();
        }
    }
}
