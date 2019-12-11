# Block-Breaker v.1
This repository features a simple Brick Breaker game made in Unity, which conceptually was made by GameDev.tv

### Temporary Link to Game (Active for 30 days)
https://www.sharemygame.com/share/1768f794-9bf4-458d-92be-055106cc1d9c

### TODO (Most likely will not be completed)
- [BUG] Launch Arrow loses distance from ball vector after repetitive scrolling; This is caused by UnityEngine.Cos/Sin which has slight issues with roundoff error; Must implement floor or ceiling function to prevent this roundoff or keep track of original direction vector
- [BUG] Ball can get trapped between two collision bodies; This can be solved by calculating the local direction vector of the ball and varying the launch angles
- [DESIGN] Game is boring; Could be thematically changed to Brick Breaker made by blackberry or a space game with ships as blocks etc.
- [FEATURE] Abilities and upgrades should be available after breaking blocks