using System.Collections.Generic;
using MK.StateSpaceSearch.DataStructures;

namespace MK.StateSpaceSearch.Fringe
{
    public class DFSFringe<TCustomData> : Fringe<TCustomData>
    {
        private readonly Stack<State<TCustomData>> _fringe = new Stack<State<TCustomData>>();

        public override int Count
        {
            get { return _fringe.Count; }
        }

        public override bool IsEmpty
        {
            get { return _fringe.Count == 0; }
        }

        public override State<TCustomData> Next
        {
            get { return _fringe.Pop(); }
        }

        public override void Add(State<TCustomData> s)
        {
            _fringe.Push(s);
        }
    }
}
