using System;
using MK.StateSpaceSearch.DataStructures;
using MK.StateSpaceSearch.Fringe;

namespace MK.StateSpaceSearch
{
    public class ProblemSolver<TCustomData>
    {
        public Summary<TCustomData> SolveProblem(Fringe<TCustomData> fringe, IProblem<TCustomData> problem, Config config = null)
        {
            var summary = new Summary<TCustomData>();

            if(config == null)
                config = new Config();

            var initialState = new State<TCustomData> { Data = config.DataForInitialState == null ? problem.DataForInitialState : (TCustomData)config.DataForInitialState };
            fringe.Add(initialState);

            while (!fringe.IsEmpty)
            {
                summary.MaxNumberOfStates = Math.Max(summary.MaxNumberOfStates, fringe.Count);

                var next = fringe.Next;

                if (problem.IsFinalState(next))
                {
                    summary.Results.Add(GetSolution(next, config));

                    if (config.MaxNumberOfResults != 0 && summary.Results.Count >= config.MaxNumberOfResults)
                        break;
                }
                else
                {
                    var children = problem.Expand(next);

                    if (children.Count == 0)
                    {
                        next.Parent = null;
                        continue;
                    }

                    if (config.PreserveHierarchy)
                    {
                        next.Children = children;

                        foreach (var child in next.Children)
                            child.Parent = next;
                    }

                    summary.TotalNumberOfStates += children.Count;
                    fringe.AddRange(children);
                }
            }

            return summary;
        }

        private static Result<TCustomData> GetSolution(State<TCustomData> state, Config config)
        {
            var res = new Result<TCustomData>();
            while (state != null)
            {
                res.Data.Add(state.Data);

                if (config.ReturnStates)
                    res.States.Add(state);

                if (!config.PreserveHierarchy)
                    break;

                state = state.Parent;
            }

            return res;
        }
    }
}