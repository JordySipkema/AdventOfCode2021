namespace AdventOfCode_2021

module Day01 =
    open System

    // Shared
    let inputFile = "./input/day01.txt"

    let readInput =
        Library.ReadFile inputFile
        |> List.map (int)    // Convert String to Int


    // Part One
    let countHigherThanlast inputList =
        inputList
        |> List.pairwise     // Collect items per-pair
        |> List.choose(fun (num1,num2) -> if num2 > num1 then Some 1 else None) 
                             // return 1 if higher, discard all others
        |> List.reduce(+)    // Sum all the '1's

    let solutionPart1 =
        readInput
        |> countHigherThanlast
        |> sprintf "The answer is: %A"


    // Part Two
    let windowedSum windowSize list =
        list
        |> List.windowed windowSize  // Use native F# List.windowed func to gather all the windows.
        |> List.map (List.reduce(+)) // Count the sum of all windows and return them as a list.

    let solutionPart2 =
        readInput
        |> windowedSum 3
        |> countHigherThanlast
        |> sprintf "The answer is: %A"
