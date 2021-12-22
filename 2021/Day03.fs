namespace AdventOfCode_2021

module Day03 =
    open System

    // Shared
    let inputFile = "./input/day03.txt"

    let readInput =
        Library.ReadFile inputFile
        |> List.map string


    // Part One
    let getCharacterAtPostition position str:string =
        str[position]

    let selectMsbAndLsb list =
        match (List.item 0 list), (List.item 1 list) with
        (char1, count1),(char2, count2) ->
            if count1 > count2
            then char1, char2
            else char2, char1

    let transposeAndFlatten (list1, list2) =
        [list1; list2]
        |> List.map(List.map string)
        |> List.map(List.reduce(+))
        |> List.map(fun str -> Convert.ToInt32(str, 2))

    let solutionPart1 =
        readInput
        |> List.map (fun str -> str.ToCharArray() |> List.ofArray)
        |> List.transpose
        |> List.map (List.countBy id)
        |> List.map selectMsbAndLsb
        |> List.unzip
        |> transposeAndFlatten
        |> List.reduce(*)
        |> sprintf "The answer is: %i"


    // Part Two
    let lookupCharInStrAt position (str:string) =
        match str.Length with
        | length when length > position -> str.[position]
        | _ -> failwithf "Error: index %i; string '%s'" position str
    
    let mostCommonBitPredicate (char1,number1) (char2,number2) =
        match number1 with
        | number1 when number1 > number2 -> char1
        | number1 when number1 < number2 -> char2
        | number1 when number1 = number2 -> '1'
        | _ -> failwith "Error finding most common bit" 

    let leastCommonBitPredicate (char1,number1) (char2,number2) =
        match number1 with
        | number1 when number1 > number2 -> char2
        | number1 when number1 < number2 -> char1
        | number1 when number1 = number2 -> '0'
        | _ -> failwith "Error finding least common bit"

    let findCommonBits predicate list =
        list
        |> List.countBy id
        |> function 
            | [] -> failwith "Error: No values found"
            | [value] -> value |> fst
            | list -> predicate list.[0] list.[1]

    let rec filterBinaryList predicate index input =
        match input with
        | [] -> Error "Error: No values found"
        | [value] -> Ok value
        | list -> 
            let charToMatch = 
                list 
                |> List.map (lookupCharInStrAt index) 
                |> findCommonBits predicate
            list 
            |> List.filter (fun str -> lookupCharInStrAt index str = charToMatch )
            |> filterBinaryList predicate (index+1)

    let solutionPart2 =
        readInput
        |> fun list -> filterBinaryList mostCommonBitPredicate 0 list, filterBinaryList leastCommonBitPredicate 0 list
        |> function
            | Ok mostCommonList,Ok leastCommonList -> 
                [mostCommonList; leastCommonList] 
                |> List.map (fun str -> Convert.ToInt32(str,2))
                |> fun list -> sprintf "The answer is: oxygen generator rate: %i; CO2 scrubber rate: %i; life support rating: %i" list.[0] list.[1] (list.[0] * list.[1])
            | Error err1, Error err2 -> sprintf "%s; %s" err1 err2
            | Error err, _
            | _ , Error err -> sprintf "%s" err
        
