namespace AdventOfCode_2021

module Library =
    open System.IO

    let ReadFile path = 
        File.ReadLines(path)
        |> List.ofSeq        // Convert IEnumerable to F# native list
