using System;
using System.Collections.Generic;
using System.Linq;

namespace Sau.Raylan.SR5
{
    public class DicePoolResults
    {
        public List<int> RollResults { get; private set; }
        public int Limit { get; private set; }
        public int Total { get { return RollResults.Sum(); } }
        public int Hits
        {
            get
            {
                var hits = RollResults.Count(x => x == 5 || x == 6);
                return (Limit < 0)
                    ? hits
                    : Math.Min(hits, Limit);
            }
        }
        public bool IsGlitch { get { return RollResults.Count(x => x == 1) > RollResults.Count / 2; } }
        public bool IsCriticalGlitch { get { return IsGlitch && Hits == 0; } }

        public DicePoolResults(IEnumerable<int> rollResults)
            : this(rollResults, -1) { }

        public DicePoolResults(IEnumerable<int> rollResults, int limitToImpose)
        {
            if (rollResults == null) throw new ArgumentNullException("rollResults");

            RollResults = rollResults.ToList();
            Limit = limitToImpose;
        }
    }
}
