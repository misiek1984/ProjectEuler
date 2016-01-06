using System.Collections.Generic;
using MK.StateSpaceSearch.DataStructures;

namespace MK.StateSpaceSearch
{
    public interface IProblem<TCustomData>
    {
        TCustomData DataForInitialState { get; }

        bool IsFinalState(State<TCustomData> state);

        IList<State<TCustomData>> Expand(State<TCustomData> state);
    }
}
