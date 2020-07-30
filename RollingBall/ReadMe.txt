RollingBall is game WPF project developed according to MVVM architectural pattern.

Rules are simple and kind of similar to billiard - you must pocket a ball. 
Diferences are: only one pocket, ball always moves with stable speed and instead of strike a ball with cue you should draw walls with mouse to change it direction.
You can chill out watching at ball go or try to beat the challenge mode. Custom levels is upcoming.

Main classes are: Base_game_page, Ball, Wall.

Base_game_page is View-Model part of MVVM pattern. It create other calsses objects, define their interaction, control GUI and processes user imput.
It is separated to provide page inheritance to add challenge mode functional in the OOP maner.

Ball object change its position and notify Base_game_page when level is completed.

Wall object control its existence time and notify Base_game_page when its must be deleted.

Concurrent computing, events, data binding, LINQ were used in the developing process.
