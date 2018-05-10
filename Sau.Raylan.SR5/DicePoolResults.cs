using System;
using System.Collections.Generic;
using System.Linq;

namespace Sau.Raylan.SR5
{
    public class DicePoolResults
    {
        public List<int> RollResults { get; private set; }
        public int Total { get { return RollResults.Sum(); } }
        public int Hits { get { return RollResults.Count(x => x == 5 || x == 6); } }
        public bool IsGlitch { get { return RollResults.Count(x => x == 1) > RollResults.Count / 2; } }
        public bool IsCriticalGlitch { get { return IsGlitch && Hits == 0; } }

        public DicePoolResults(IEnumerable<int> rollResults)
        {
            if (rollResults == null) throw new ArgumentNullException("rollResults");

            RollResults = rollResults.ToList();
        }
    }
}
