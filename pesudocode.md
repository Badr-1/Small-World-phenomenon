|  Type   | Test case | Movies | Actors | Queries | Old (s) | New (s) | Diff (s) | Improvement |
|:-------:|:---------:|:------:|:------:|:-------:|:-------:|:-------:|:--------:|:-----------:|
|  Small  |     1     |  193   |  2513  |   110   |  0.625  |   0.3   |  0.225   |    208%     |
|  Small  |     2     |  187   |  8264  |   50    |  1.391  |   0.6   |  0.791   |    232%     |
| Medium  |     3     |  967   | 13848  |   85    |  3.11   |   1.5   |   1.61   |    207%     |
| Medium  |     4     |  967   | 13848  |  4000   | 55.938  |   26    |  25.938  |    215%     |
| Medium  |     5     |  4735  | 43923  |   110   |  9.891  |    5    |  4.891   |    198%     |
| Medium  |     6     |  4735  | 43923  |  2000   | 37.578  |   22    |  15.578  |    171%     |
|  Large  |     7     | 14129  | 170518 |   26    |  40.25  |   26    |  14.25   |    155%     |
|  Large  |     8     | 14129  | 170518 |   600   |  6.438  |    4    |  2.438   |    161%     |
| Extreme |     9     | 122806 | 418451 |   22    | 126.422 |   85    |  41.422  |    149%     |
| Extreme |    10     | 122806 | 418451 |   200   | 17.656  |    8    |  9.656   |    221%     |
|||||Average|29.9299|17.84|11.6799|192%|

- Requirements
    - [ ] without Optimization
    - [x] Optimization idea implemented
    - [ ] Detailed analysis of the code
    - [ ] Add Option to run the code without optimization
- bonuses:
  - [ ] Frequancy of Degree of Separation from one actor/actress to all other actors/actress
  - [ ] Strongest Path between two actors/actress
  - [ ] Minimum number of Movies that link all actors/actress together