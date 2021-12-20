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
    let OperationFromString str =
        match str with
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
    type Submarine = 
        {
            position : int
            depth : int
            aim : int
        }
        static member init = { position = 0; depth = 0; aim = 0 }

    
    let rec dive submarine instructions =
        match instructions with
        | [] -> submarine  // If array is empty
        | instruction::remainingInstructions -> // Pop the first instruction off and keep the remaining list
        (
            match instruction with
            | Forward, x -> { 
                submarine with 
                    position = submarine.position + x
                    depth = submarine.depth + ( submarine.aim * x)
                }
            | Up, x -> { submarine with aim = submarine.aim - x }
            | Down, x -> { submarine with aim = submarine.aim + x }
        ) 
        |> fun submarine -> dive submarine remainingInstructions // After applying the current instruction; recursively call this function again.

    let submarineToString (sub:Submarine) =
        sprintf "Position: %A * Depth %A = %A" sub.position sub.depth (sub.position * sub.depth)

    let solutionPart2 =
        readInput
        |> dive Submarine.init
        |> submarineToString
        |> Console.WriteLine
