# Daily Coding Problem: Problem 4b

From Vas

> Joe is a prisoner who has been sentenced to hard labor for his crimes.
> Each day he is given a pile of large rocks to break into tiny rocks.
> To make matters worse, they do not provide any tools to work with. Instead, he must use the rocks themselves.
> He always picks up the largest two stones and smashes them together. If they are of equal weight, they both disintegrate entirely.
> If one is larger, the smaller one is disintegrated and the larger one is reduced by the weight of the smaller one.
> Eventually there is either one stone left that cannot be broken or all of the stones have been smashed. Determine the weight of the last stone,
> or return 0 if there is none.
> For example, there are stones of weights a = [1,2,3,6,7,7]. He always starts with the two largest stones.
> In this case, the two have equal weights of 7 so they both crumble when smashed. Next he smashes weights 3 and 6. The smaller one is destroyed
> and the larger weighs 6 - 3 = 3 units. Weights 3 and 2 are smashed together which leaves a stone of weight 1. This is smashed with the last
> remaining stone of weight 1. There are no stones left, so the remaining stone weight is 0.
> Function Description
> Complete the function lastStoneWeight in the editor below. The function must return an integer that denotes the weight of the last stone
> or 0 if all stones shattered into dust.
> lastStoneWeight has the following parameter(s):
> a[a[0],...a[n-1]]: an array of integers, the weights of each stone
