 |  Type   | Test case | Movies | Actors | Queries | Normal (s) | Optimization (s) | Diff (s) | Improvement |
 | :-----: | :-------: | :----: | :----: | :-----: | :--------: | :--------------: | :------: | :---------: |
 |  Small  |     1     |  193   |  2513  |   110   |   1.482    |       0.3        |  1.182   |    494%     |
 |  Small  |     2     |  187   |  8264  |   50    |   5.729    |       0.6        |  5.129   |    955%     |
 | Medium  |     3     |  967   | 13848  |   85    |   16.241   |       1.5        |  14.741  |    1083%    |
 | Medium  |     4     |  967   | 13848  |  4000   |  262.766   |        26        | 236.766  |    1011%    |
 | Medium  |     5     |  4735  | 43923  |   110   |   46.423   |        5         |  41.423  |    928%     |
 | Medium  |     6     |  4735  | 43923  |  2000   |  175.366   |        22        | 153.366  |    797%     |
 |  Large  |     7     | 14129  | 170518 |   26    |  231.683   |        26        | 205.683  |    891%     |
 |  Large  |     8     | 14129  | 170518 |   600   |   24.732   |        4         |  20.732  |    618%     |
 | Extreme |     9     | 122806 | 418451 |   22    |  1651.905  |        85        | 1566.905 |    1943%    |
 | Extreme |    10     | 122806 | 418451 |   200   |  101.333   |        8         |  93.333  |    1267%    |
 |         |           |        |        | Average |  251.766   |      17.84       | 233.926  |    999%     |


- Requirements
    - [x] without Optimization
    - [x] Optimization idea implemented
    - [x] Detailed analysis of the code
    - [x] Add Option to run the code without optimization
- bonuses:
  - [x] Frequancy of Degree of Separation from one actor/actress to all other actors/actress
  - [x] Strongest Path between two actors/actress
  - [ ] Minimum number of Movies that link all actors/actress together