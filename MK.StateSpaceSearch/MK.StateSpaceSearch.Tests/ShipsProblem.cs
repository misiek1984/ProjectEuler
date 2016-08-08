using System.Linq;
using System.Text;
using System.Collections.Generic;

using MK.StateSpaceSearch.DataStructures;
using MK.StateSpaceSearch.Fringe;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MK.StateSpaceSearch.Tests
{
    public static class Ports
    {
        public const string Port1 = "A";
        public const string Port2 = "B";
        public const string Port3 = "C";
    }

    public class ShipProblemData
    {
        public Dictionary<string, Stack<string>> Ports { get; private set; }

        public Stack<string> CurrentPath { get; private set; }

        public ShipProblemData()
        {
            Ports = new Dictionary<string, Stack<string>>();
            CurrentPath = new Stack<string>();
        }

        public ShipProblemData Clone()
        {
            var state = new ShipProblemData();

            foreach (var pair in Ports)
            {
                var stack = new Stack<string>();
                foreach (var target in pair.Value.Reverse())
                    stack.Push(target);

                state.Ports[pair.Key] = stack;
            }

            foreach (var item in CurrentPath.Reverse())
                state.CurrentPath.Push(item);

            return state;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            var max = Ports.Max(p => p.Value.Count);

            foreach (var p in Ports)
                sb.Append(p.Key + ";");

            sb.AppendLine();

            foreach (var p in Ports)
                sb.Append(new string('-', p.Key.Length).PadRight(20) + ";");

            sb.AppendLine();

            for (var i = 0; i < max; ++i)
            {
                foreach (var p in Ports)
                    if (i < p.Value.Count)
                        sb.Append(p.Value.Reverse().Skip(i).First() + ";");
                    else
                        sb.Append(";");

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }

    /// <summary>
    /// MK.StateSpaceSearch.Tests project contains an example of solving a problem that I call Ship Problem. 
    /// In this problem there are N ports and a ship that travels between them. A captain informs a port manager 
    /// about a next destination before a departure. The next port can be the same as the current one. 
    /// The port manager writes this information in the special book. Knowing the last port visited by the ship 
    /// and the book for each port find a itinerary of the ship. For example if there are 3 ports (A, B, C) and 
    /// the itinerary is equal to A -> B -> C -> A -> C -> B -> A then a book for the port A will contain records B, C, 
    /// for the port B records C, A and for the port C records A, B.
    /// </summary>
    public class ShipProblem : IProblem<ShipProblemData>
    {
        private bool _withoutLastPort;
  
        public ShipProblem(bool withoutLastPort = false)
        {
            _withoutLastPort = withoutLastPort;
        }

        public ShipProblemData DataForInitialState
        {
            get
            {
                var myData = new ShipProblemData();

                if (!_withoutLastPort)
                    myData.CurrentPath.Push(Ports.Port1);

                var book = new Stack<string>();

                book.Push(Ports.Port2);
                book.Push(Ports.Port1);
                book.Push(Ports.Port3);
                book.Push(Ports.Port2);
                book.Push(Ports.Port1);
                book.Push(Ports.Port3);
                myData.Ports[Ports.Port1] = book;

                book = new Stack<string>();

                book.Push(Ports.Port3);
                book.Push(Ports.Port1);
                book.Push(Ports.Port1);
                book.Push(Ports.Port3);
                book.Push(Ports.Port1);
                book.Push(Ports.Port1);
                myData.Ports[Ports.Port2] = book;

                book = new Stack<string>();

                book.Push(Ports.Port2);
                book.Push(Ports.Port3);
                book.Push(Ports.Port2);
                book.Push(Ports.Port2);
                book.Push(Ports.Port3);
                book.Push(Ports.Port2);
                myData.Ports[Ports.Port3] = book;

                return myData;
            }
        }

        public bool IsFinalState(State<ShipProblemData> state)
        {
            return state.Data.Ports.All(kvp => !kvp.Value.Any());
        }

        public IList<State<ShipProblemData>> Expand(State<ShipProblemData> state)
        {
            var newStates = new List<State<ShipProblemData>>();

            var currentHost = state.Data.CurrentPath.Count == 0 ? null : state.Data.CurrentPath.Peek();
            foreach (var host in state.Data.Ports.Keys)
            {
                if (state.Data.Ports[host].Count > 0 && (currentHost == null || state.Data.Ports[host].Peek() == currentHost))
                {
                    var copy = state.Data.Clone();
                    var p = copy.Ports[host].Pop();
                    if (currentHost == null)
                        copy.CurrentPath.Push(p);
                    copy.CurrentPath.Push(host);

                    newStates.Add(new State<ShipProblemData> { Data = copy });
                }
            }

            return newStates;
        }
    }

    [TestClass]
    public class Tests
    {
        private Dictionary<string, List<string>> _solutions;
        public Dictionary<string, List<string>> Solutions
        {
            get
            {
                if (_solutions == null)
                    _solutions = new Dictionary<string, List<string>>()
                        {
                            {
                                Ports.Port1,
                                new List<string>
                                    {
                                        Ports.Port1,
                                        Ports.Port2,
                                        Ports.Port3,
                                        Ports.Port2,
                                        Ports.Port1,
                                        Ports.Port1,
                                        Ports.Port3,
                                        Ports.Port3,
                                        Ports.Port2,
                                        Ports.Port1,
                                        Ports.Port2,
                                        Ports.Port3,
                                        Ports.Port2,
                                        Ports.Port1,
                                        Ports.Port1,
                                        Ports.Port3,
                                        Ports.Port3,
                                        Ports.Port2,
                                        Ports.Port1
                                    }
                            },
                            {
                                Ports.Port2,
                                new List<string>
                                    {
                                        Ports.Port2,
                                        Ports.Port3,
                                        Ports.Port2,
                                        Ports.Port1,
                                        Ports.Port2,
                                        Ports.Port1,
                                        Ports.Port1,
                                        Ports.Port3,
                                        Ports.Port3,
                                        Ports.Port2,
                                        Ports.Port3,
                                        Ports.Port2,
                                        Ports.Port1,
                                        Ports.Port2,
                                        Ports.Port1,
                                        Ports.Port1,
                                        Ports.Port3,
                                        Ports.Port3,
                                        Ports.Port2
                                    }
                            },
                            {
                                Ports.Port3,
                                new List<string>
                                    {
                                        Ports.Port3,
                                        Ports.Port2,
                                        Ports.Port3,
                                        Ports.Port3,
                                        Ports.Port2,
                                        Ports.Port1,
                                        Ports.Port2,
                                        Ports.Port1,
                                        Ports.Port1,
                                        Ports.Port3,
                                        Ports.Port2,
                                        Ports.Port3,
                                        Ports.Port3,
                                        Ports.Port2,
                                        Ports.Port1,
                                        Ports.Port2,
                                        Ports.Port1,
                                        Ports.Port1,
                                        Ports.Port3,
                                    }
                            }
                        };

                return _solutions;
            }
        }

        [TestMethod]
        public void SolverProblem_DFSFringeWithoutLastPort_CorrectResult()
        {
            var solver = new ProblemSolver<ShipProblemData>();
            var summary = solver.SolveProblem(new DFSFringe<ShipProblemData>(), new ShipProblem(true));

            Assert.IsTrue(summary.MaxNumberOfStates == 7);
            Assert.IsTrue(summary.TotalNumberOfStates == 112);

            Assert.IsNotNull(summary);
            Assert.IsTrue(summary.Results.Count == 3);
            foreach (var res in summary.Results)
            {
                Assert.IsTrue(res.Data != null);
                Assert.IsTrue(res.Data.Count == 19);
                Assert.IsTrue(res.Data[0].CurrentPath != null);
                Assert.IsTrue(res.Data[0].CurrentPath.Count == 19);
                Assert.IsTrue(res.Data[0].Ports.All(p => p.Value.Count == 0));

                var pathAsList = res.Data[0].CurrentPath.ToList();
                var lastPort = pathAsList.Last();
                for (var i = 0; i < pathAsList.Count; ++i)
                    Assert.IsTrue(pathAsList[i] == Solutions[lastPort][i]);
            }
        }

        [TestMethod]
        public void SolverProblem_DFSFringe_CorrectResult()
        {
            var solver = new ProblemSolver<ShipProblemData>();
            var summary = solver.SolveProblem(new DFSFringe<ShipProblemData>(), new ShipProblem());

            Assert.IsTrue(summary.MaxNumberOfStates == 5);
            Assert.IsTrue(summary.TotalNumberOfStates == 40);

            Assert.IsNotNull(summary);
            Assert.IsTrue(summary.Results.Count == 1);
            Assert.IsTrue(summary.Results[0].Data != null);
            Assert.IsTrue(summary.Results[0].Data.Count == 19);
            Assert.IsTrue(summary.Results[0].Data[0].CurrentPath != null);
            Assert.IsTrue(summary.Results[0].Data[0].CurrentPath.Count == 19);

            Assert.IsTrue(summary.Results[0].Data[0].Ports.All(p => p.Value.Count == 0));
            var pathAsList = summary.Results[0].Data[0].CurrentPath.ToList();
            var lastPort = pathAsList.Last();
            for (var i = 0; i < pathAsList.Count; ++i)
                Assert.IsTrue(pathAsList[i] == Solutions[lastPort][i]);
        }

        [TestMethod]
        public void SolverProblem_DFSFringeOnly1Result_CorrectResult()
        {
            var solver = new ProblemSolver<ShipProblemData>();
            var summary = solver.SolveProblem(new DFSFringe<ShipProblemData>(), new ShipProblem(), new Config { MaxNumberOfResults = 1 });

            Assert.IsTrue(summary.MaxNumberOfStates == 5);
            Assert.IsTrue(summary.TotalNumberOfStates == 32);

            Assert.IsNotNull(summary);
            Assert.IsTrue(summary.Results.Count == 1);
            Assert.IsTrue(summary.Results[0].Data != null);
            Assert.IsTrue(summary.Results[0].Data.Count == 19);
            Assert.IsTrue(summary.Results[0].Data[0].CurrentPath != null);
            Assert.IsTrue(summary.Results[0].Data[0].CurrentPath.Count == 19);

            Assert.IsTrue(summary.Results[0].Data[0].Ports.All(p => p.Value.Count == 0));
            var pathAsList = summary.Results[0].Data[0].CurrentPath.ToList();
            var lastPort = pathAsList.Last();
            for (var i = 0; i < pathAsList.Count; ++i)
                Assert.IsTrue(pathAsList[i] == Solutions[lastPort][i]);
        }

        [TestMethod]
        public void SolverProblem_DFSFringeDoNotPreserveHierarchy_CorrectResult()
        {
            var solver = new ProblemSolver<ShipProblemData>();
            var summary = solver.SolveProblem(new DFSFringe<ShipProblemData>(), new ShipProblem(), new Config { PreserveHierarchy = false});

            Assert.IsTrue(summary.MaxNumberOfStates == 5);
            Assert.IsTrue(summary.TotalNumberOfStates == 40);

            Assert.IsNotNull(summary);
            Assert.IsTrue(summary.Results.Count == 1);
            Assert.IsTrue(summary.Results[0].Data != null);
            Assert.IsTrue(summary.Results[0].Data.Count == 1);
            Assert.IsTrue(summary.Results[0].Data[0].CurrentPath != null);
            Assert.IsTrue(summary.Results[0].Data[0].CurrentPath.Count == 19);

            Assert.IsTrue(summary.Results[0].Data[0].Ports.All(p => p.Value.Count == 0));
            var pathAsList = summary.Results[0].Data[0].CurrentPath.ToList();
            var lastPort = pathAsList.Last();
            for (var i = 0; i < pathAsList.Count; ++i)
                Assert.IsTrue(pathAsList[i] == Solutions[lastPort][i]);
        }

        [TestMethod]
        public void SolverProblem_BFSFringeWithoutLastPort_CorrectResult()
        {
            var solver = new ProblemSolver<ShipProblemData>();
            var summary = solver.SolveProblem(new BFSFringe<ShipProblemData>(), new ShipProblem(true));

            Assert.IsTrue(summary.MaxNumberOfStates == 9);
            Assert.IsTrue(summary.TotalNumberOfStates == 112);

            Assert.IsNotNull(summary);
            Assert.IsTrue(summary.Results.Count == 3);
            foreach (var res in summary.Results)
            {
                Assert.IsTrue(res.Data != null);
                Assert.IsTrue(res.Data.Count == 19);
                Assert.IsTrue(res.Data[0].CurrentPath != null);
                Assert.IsTrue(res.Data[0].CurrentPath.Count == 19);
                Assert.IsTrue(res.Data[0].Ports.All(p => p.Value.Count == 0));

                var pathAsList = res.Data[0].CurrentPath.ToList();
                var lastPort = pathAsList.Last();
                for (var i = 0; i < pathAsList.Count; ++i)
                    Assert.IsTrue(pathAsList[i] == Solutions[lastPort][i]);
            }
        }

        [TestMethod]
        public void SolverProblem_BFSFringe_CorrectResult()
        {
            var solver = new ProblemSolver<ShipProblemData>();
            var summary = solver.SolveProblem(new BFSFringe<ShipProblemData>(), new ShipProblem());

            Assert.IsNotNull(summary);
            Assert.IsTrue(summary.Results.Count == 1);
            Assert.IsTrue(summary.Results[0].Data != null);
            Assert.IsTrue(summary.Results[0].Data.Count == 19);
            Assert.IsTrue(summary.Results[0].Data[0].CurrentPath != null);
            Assert.IsTrue(summary.Results[0].Data[0].CurrentPath.Count == 19);

            Assert.IsTrue(summary.MaxNumberOfStates == 3);
            Assert.IsTrue(summary.TotalNumberOfStates == 40);

            Assert.IsTrue(summary.Results[0].Data[0].Ports.All(p => p.Value.Count == 0));
            var pathAsList = summary.Results[0].Data[0].CurrentPath.ToList();
            var lastPort = pathAsList.Last();
            for (var i = 0; i < pathAsList.Count; ++i)
                Assert.IsTrue(pathAsList[i] == Solutions[lastPort][i]);
        }

        [TestMethod]
        public void SolverProblem_BFSFringeOnly1Result_CorrectResult()
        {
            var solver = new ProblemSolver<ShipProblemData>();
            var summary = solver.SolveProblem(new BFSFringe<ShipProblemData>(), new ShipProblem(), new Config { MaxNumberOfResults = 1 });

            Assert.IsTrue(summary.MaxNumberOfStates == 3);
            Assert.IsTrue(summary.TotalNumberOfStates == 40);

            Assert.IsNotNull(summary);
            Assert.IsTrue(summary.Results.Count == 1);
            Assert.IsTrue(summary.Results[0].Data != null);
            Assert.IsTrue(summary.Results[0].Data.Count == 19);
            Assert.IsTrue(summary.Results[0].Data[0].CurrentPath != null);
            Assert.IsTrue(summary.Results[0].Data[0].CurrentPath.Count == 19);

            Assert.IsTrue(summary.Results[0].Data[0].Ports.All(p => p.Value.Count == 0));
            var pathAsList = summary.Results[0].Data[0].CurrentPath.ToList();
            var lastPort = pathAsList.Last();
            for (var i = 0; i < pathAsList.Count; ++i)
                Assert.IsTrue(pathAsList[i] == Solutions[lastPort][i]);
        }

        [TestMethod]
        public void SolverProblem_DFSFringeOverrideInitialState_CorrectResult()
        {
            var solver = new ProblemSolver<ShipProblemData>();

            //This problem is a sub-problem of the original problem
            var myData = new ShipProblemData();

            myData.CurrentPath.Push(Ports.Port1);

            var book = new Stack<string>();

            book.Push(Ports.Port2);
            book.Push(Ports.Port1);
            book.Push(Ports.Port3);
            myData.Ports[Ports.Port1] = book;

            book = new Stack<string>();

            book.Push(Ports.Port3);
            book.Push(Ports.Port1);
            book.Push(Ports.Port1);
            myData.Ports[Ports.Port2] = book;

            book = new Stack<string>();

            book.Push(Ports.Port2);
            book.Push(Ports.Port3);
            book.Push(Ports.Port2);
            myData.Ports[Ports.Port3] = book;

            var summary = solver.SolveProblem(new DFSFringe<ShipProblemData>(), new ShipProblem(),
                                              new Config {DataForInitialState = myData});

            Assert.IsTrue(summary.MaxNumberOfStates == 3);
            Assert.IsTrue(summary.TotalNumberOfStates == 18);

            Assert.IsNotNull(summary);
            Assert.IsTrue(summary.Results.Count == 1);
            Assert.IsTrue(summary.Results[0].Data != null);
            Assert.IsTrue(summary.Results[0].Data.Count == 10);
            Assert.IsTrue(summary.Results[0].Data[0].CurrentPath != null);
            Assert.IsTrue(summary.Results[0].Data[0].CurrentPath.Count == 10);

            Assert.IsTrue(summary.Results[0].Data[0].Ports.All(p => p.Value.Count == 0));
            var pathAsList = summary.Results[0].Data[0].CurrentPath.ToList();
            var lastPort = pathAsList.Last();
            for (var i = 0; i < pathAsList.Count; ++i)
                Assert.IsTrue(pathAsList[i] == Solutions[lastPort][i]);
        }
    }
}
