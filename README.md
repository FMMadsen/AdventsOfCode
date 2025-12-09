# Advents of Code

This repository facilitates that people can work on the puzzles from the public available https://adventofcode.com.

This repository and the code within has no direct integration to Advent of Code, but has a framework solution to easy place a dataset and get it pushed into your solution code. Also UnitTests are available to ease process around the puzzles solving, so you can focus on the most fun part - to solve the challenges.

I cannot take any credit for being part of Advent Of Code, it is the good work of [Eric Wastl](http://was.tl/) and his team.

Prerequisites:
 - C# knowledge, the solution is based on that
 - Visual Studio 2022 version 17.8.0 (Community edition is good enough)
 - .NET 8

## To solve the puzzles
1) Go to https://adventofcode.com
2) Login with Google account, Twitter or GitHub
3) Go to the year you like to work puzzles for - usually current year :-)
4) Read the first puzzle for Day 1, and use any code language to figure the output string
5) Submit your answer - and Part 2 puzzle will open.
6) Every day in December a new puzzle will open - Go GAME!

## Get the code
Clone the repo, and create a branch to your own code. Keep using your own branch, and don't merge into the main branch, since that is generally containing the unit tests and foundation, but not suppose to store any actial solutions to the problems.

    > Git clone https://github.com/FMMadsen/AdventsOfCode
    > Git branch MyName
    > Git checkout MyName

## To use this repository
#### High Level: 
Use the UnitTest projects to develop your solution based on the test examples that are given every day.
Use the Puzzle Runner console application to see the result of the big datasets. Use predefined dataset files to paste dataset into.

#### Details
1) Open solution in Visual Studio
2) Ensure to use own branch
3) Add test dataset into the Unit Test project under "TestDataSets" folder
4) Go to the Solutions project and start solve a day puzzle
5) Run Unit Tests to see if your solution works
6) When it works, get the full dataset from the website
7) Paste the full dataset into the Solutions folders 'DataSet' folder ex.: AdventOfCode2023Solutions/DataSets
8) Do not paste the full dataset into the Unit Test datasets - they are only for the small tests
9) Now run the Console Application "Puzzle Runner" which will output into console, your answer. Paste answer into the website solution




