# MK.StateSpaceSearch
*MK.StateSpaceSearch* is a simple implementation of [the state space search](https://en.wikipedia.org/wiki/State_space_search) approach to 
solving algorithmic problems. A given state space can be searched with [Depth-first search](https://en.wikipedia.org/wiki/Depth-first_search), 
[Breadth-first search](https://en.wikipedia.org/wiki/Breadth-first_search) or random algorithm. In order to solve a problem you have to provide a 
definition of a problem by implementing the `IProblem<TCustomData>` interface. Currently a cost function is not supported.

*MK.StateSpaceSearch.Tests* project contains an example of solving a problem that I call *Ship Problem*. 
In this problem there are N ports and a ship that travels between them.
A captain informs a port manager about a next destination before a departure. 
The next port can be the same as the current one. The port manager writes this information in the special book. 
Knowing the last port visited by the ship and the book  for each port find a itinerary of the ship.
For example if there are 3 ports (A, B, C) and the itinerary is equal to A -> B -> C -> A -> C -> B -> A then a book for the port A will contain records 
B, C, for the port B records C, A and for the port C records A, B. 
