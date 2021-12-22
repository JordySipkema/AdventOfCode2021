namespace AdventOfCode_2021

module Library =
    open System.IO

    let ReadFile path = 
        File.ReadLines(path)
        |> List.ofSeq        // Convert IEnumerable to F# native list

    /// Split a list into chunks using the specified separator
    /// This takes a list and returns a list of lists (chunks)
    /// that represent individual groups, separated by the given
    /// separator 'v'
    let splitListBy v list =
        let yieldRevNonEmpty list = 
            if list = [] then []
            else [List.rev list]
   
        let rec loop groupSoFar list = seq { 
            match list with
            | [] -> yield! yieldRevNonEmpty groupSoFar
            | head::tail when head = v ->
                yield! yieldRevNonEmpty groupSoFar
                yield! loop [] tail
            | head::tail ->
                yield! loop (head::groupSoFar) tail }
        loop [] list |> List.ofSeq
