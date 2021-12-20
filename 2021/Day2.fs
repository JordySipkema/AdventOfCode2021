namespace AdventOfCode_2021

module Day02 = 
    open System

    let inputFile = "./input/day02.txt"   

    // Shared
    // Types
    type Operation =
      | Forward
      | Up
      | Down

    type Movement = Operation * int

    // Helper functions for Types
    let OperationFromString someString =
        match someString with
        | "forward" -> Forward
        | "up" -> Up
        | "down" -> Down
        | _ -> failwith "Incompatible input: Operation"

    let MovementFromString (str:string) =
        match str.Split(' ') with
        | [|a;b|] -> Movement(OperationFromString a, int b)
        | _ -> failwith "Incompatible input: Movement"

    // Helper Function for input
    let readInput =
        Library.ReadFile inputFile
        |> List.map MovementFromString

    // Part One
    let Horizontal movement =
        match movement with
            | Forward, pos -> Some pos
            | _, _ -> None

    let Depth movement =
        match movement with
            | Up, pos -> Some -pos   // Depth decreases
            | Down, pos -> Some pos  // Depth increases
            | _, _ -> None

    /// Generic function to read the position; first argument is the filter function ('a -> int option)
    /// Returns the sum of the list after the filter function has been applied.
    let GetPosition filter list =
        list
        |> List.choose filter
        |> List.reduce(+)

    let solutionPart1 =
        let horPos = 
            readInput
            |> GetPosition Horizontal   

        let depth =
            readInput
            |> GetPosition Depth

        sprintf "Horizontal Postition %A * Depth: %A = %A" horPos depth (horPos*depth)
        |> Console.WriteLine

        

    // Part Two


    let solutionPart2 =
        "TBD"
        |> sprintf "The answer is: %A"
        |> Console.WriteLine
