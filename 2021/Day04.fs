namespace AdventOfCode_2021

module Day04 =
    open System
    open Library
    
    let inputFile = "./input/day04.txt"

    type Column = int
    type Row = int
    type Location = Row * Column
    type BingoNumber = int
    type BingoCard =
        {
            fields: (Location * BingoNumber option) list
        }
        member this.column number =
            this.fields
            |> List.filter (fun ((_,col),_) -> col = number)
            |> List.choose snd
        member this.row number =
            this.fields
            |> List.filter (fun ((row,_),_) -> row = number)
            |> List.choose snd
        member this.sumOfCard : int=
            this.fields 
            |> List.sumBy (
                fun (_,bingoNumberOpt) -> 
                    match bingoNumberOpt with
                    | Some number -> number
                    | None -> 0 )
        static member init (listOfrows:BingoNumber list list) =
            {
                fields = 
                    listOfrows
                    |> List.mapi ( fun rowNum listofNumbers -> 
                        listofNumbers 
                        |> List.mapi ( fun colNum bingonumber -> 
                            let location = (rowNum + 1, colNum + 1)
                            location, Some bingonumber ))
                    |> List.concat
            }
        member this.removeNumber numberPlayed =
            let newFields = 
                this.fields
                |> List.map (fun (location, number) ->
                    match number with
                    | Some number when number = numberPlayed -> location, None
                    | _ -> location, number)
            {this with fields = newFields}
            
        member this.hasBingo =
            let rows = [1..5] |> List.map(fun i -> this.row i)
            let cols = [1..5] |> List.map(fun i -> this.column i)
            
            List.append rows cols
            |> List.map(List.isEmpty)
            |> List.contains true

            

    type BingoGame =
        {
            bingoNumbersToDraw : BingoNumber list
            cards : BingoCard list
        }
    
    let lineToBingoNumbers (separator:char) (str:string) = 
        str.Split(separator, StringSplitOptions.RemoveEmptyEntries ||| StringSplitOptions.TrimEntries)
        |> List.ofArray
        |> List.map int

    let ReadBingoGame path =
        let input = ReadFile path

        let bingoNumbersToDraw = lineToBingoNumbers ',' input[0]
        let cards = (input[2..] 
        |> splitListBy ""
        |> List.map (List.map(lineToBingoNumbers ' '))
        |> List.map BingoCard.init)
                
        { bingoNumbersToDraw = bingoNumbersToDraw; cards = cards }

    let stopOnFirstBingo (bingoCards:BingoCard list) =
        bingoCards
        |> List.choose (fun card -> if card.hasBingo then card.sumOfCard |> Some else None)
        |> List.tryHead
        |> fun opt ->  (opt, bingoCards)
        
    let stopOnLastBingo (bingoCards:BingoCard list) =
        match bingoCards with
        | [] -> None, []
        | [lastCard] ->
            if lastCard.hasBingo
            then lastCard.sumOfCard |> Some, [lastCard]
            else None, [lastCard]
        | cards -> None, cards |> List.filter (fun card -> not card.hasBingo)


    let rec playBingo winningCondition (bingoGame:BingoGame) : int =
        match bingoGame.bingoNumbersToDraw with
        | [] -> failwith "No remaining bingo numbers"
        | current::remaining ->
            let updateCards =
                bingoGame.cards
                |> List.map (fun card -> card.removeNumber current)

            updateCards
            |> winningCondition
            |> function
                | Some value, _ -> value * current
                | None, cards -> playBingo winningCondition { bingoNumbersToDraw = remaining; cards = cards}

    let solutionPart1 =
        ReadBingoGame inputFile
        |> playBingo stopOnFirstBingo
        |> sprintf "The answer is: %A"


    let solutionPart2 =
        ReadBingoGame inputFile
        |> playBingo stopOnLastBingo
        |> sprintf "The answer is: %A"
