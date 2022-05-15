|  Type   | Test case | Movies | Actors | Queries | Normal (s) | Optimization (s) | Diff (s) | Improvement |
|:-------:|:---------:|:------:|:------:|:-------:|:----------:|:----------------:|:--------:|:-----------:|
|  Small  |     1     |  193   |  2513  |   110   |   7.288    |       0.3        |  0.225   |    208%     |
|  Small  |     2     |  187   |  8264  |   50    |   14.189   |       0.6        |  0.791   |    232%     |
| Medium  |     3     |  967   | 13848  |   85    |   36.694   |       1.5        |   1.61   |    207%     |
| Medium  |     4     |  967   | 13848  |  4000   |  575.639   |        26        |  25.938  |    215%     |
| Medium  |     5     |  4735  | 43923  |   110   |  120.435   |        5         |  4.891   |    198%     |
| Medium  |     6     |  4735  | 43923  |  2000   |  390.582   |        22        |  15.578  |    171%     |
|  Large  |     7     | 14129  | 170518 |   26    |  464.134   |        26        |  14.25   |    155%     |
|  Large  |     8     | 14129  | 170518 |   600   |   53.004   |        4         |  2.438   |    161%     |
| Extreme |     9     | 122806 | 418451 |   22    |  126.422   |        85        |  41.422  |    149%     |
| Extreme |    10     | 122806 | 418451 |   200   |  242.333   |        8         |  9.656   |    221%     |
|||||Average|29.9299|17.84|11.6799|192%|


- Requirements
    - [x] without Optimization
    - [x] Optimization idea implemented
    - [ ] Detailed analysis of the code
    - [x] Add Option to run the code without optimization
- bonuses:
  - [ ] Frequancy of Degree of Separation from one actor/actress to all other actors/actress
  - [ ] Strongest Path between two actors/actress
  - [ ] Minimum number of Movies that link all actors/actress together