namespace AdventOfCode2021

module Day01 = 
    open System.IO

    let inputFile = "./input/input.txt"

    let readInput =
        File.ReadLines(inputFile)
        |> List.ofSeq        // Convert IEnumerable to F# native list
        |> List.map (int)    // Convert String to Int

    // Part One
    let countHigherThanlast inputList =
        inputList
        |> List.pairwise     // Collect items per-pair
        |> List.choose(fun (num1,num2) -> if num2 > num1 then Some 1 else None)
        |> List.reduce(+)

    let solutionPart1 =
        readInput
        |> countHigherThanlast
        |> sprintf "The answer is: %A"
        |> System.Console.WriteLine

    // Part Two

    let windowedSum windowSize list =
        list
        |> List.windowed windowSize
        |> List.map (List.reduce(+))

    let solutionPart2 =
        readInput
        |> windowedSum 3
        |> countHigherThanlast
        |> sprintf "The answer is: %A"
        |> System.Console.WriteLine


    solutionPart1 |> ignore
    solutionPart2 |> ignore
