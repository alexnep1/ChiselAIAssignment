The current solution includes 2 projects: 
1. LRUCache project - basic implementation of LRU Cache.
2. LRUCacheTest project - unit tests to cover the LRUCache implementation.
Note: there is no runner project to include Main(), only library.

LRUCache implementation includes:
1. Generic interface ILRUCache with exposed API: Put, Get, Delete and Reset.
2. Class LRUCache - implementation of the above interface. 

Assumptions and decisions:
1. Getting non-existing item will return empty and will not update the priority.
2. ILRUCache was added for decoupling purposes.
3. For Unit tests purposes, cache keys are ulong for maximum paging capacity and values are string for flexibility.
4. Unit tests have a good coverage, but are far from exhaustive.
