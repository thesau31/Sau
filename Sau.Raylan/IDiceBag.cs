using System.Collections.Generic;

namespace Sau.Raylan
{
    public interface IDiceBag
    {
        int d6();
        IEnumerable<int> d6(int times);
    }
}
