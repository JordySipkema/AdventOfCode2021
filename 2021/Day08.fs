namespace AdventOfCode_2021

//--- Day 8: Seven Segment Search ---

//You barely reach the safety of the cave when the whale smashes into the cave mouth, collapsing it. Sensors indicate another exit to this cave at a much greater depth, so you have no choice but to press on.

//As your submarine slowly makes its way through the cave system, you notice that the four-digit seven-segment displays in your submarine are malfunctioning; they must have been damaged during the escape. You'll be in a lot of trouble without them, so you'd better figure out what's wrong.

//Each digit of a seven-segment display is rendered by turning on or off any of seven segments named a through g:

//  0:      1:      2:      3:      4:
// aaaa    ....    aaaa    aaaa    ....
//b    c  .    c  .    c  .    c  b    c
//b    c  .    c  .    c  .    c  b    c
// ....    ....    dddd    dddd    dddd
//e    f  .    f  e    .  .    f  .    f
//e    f  .    f  e    .  .    f  .    f
// gggg    ....    gggg    gggg    ....

//  5:      6:      7:      8:      9:
// aaaa    aaaa    aaaa    aaaa    aaaa
//b    .  b    .  .    c  b    c  b    c
//b    .  b    .  .    c  b    c  b    c
// dddd    dddd    ....    dddd    dddd
//.    f  e    f  .    f  e    f  .    f
//.    f  e    f  .    f  e    f  .    f
// gggg    gggg    ....    gggg    gggg

//So, to render a 1, only segments c and f would be turned on; the rest would be off. To render a 7, only segments a, c, and f would be turned on.

//The problem is that the signals which control the segments have been mixed up on each display. The submarine is still trying to display numbers by producing output on signal wires a through g, but those wires are connected to segments randomly. Worse, the wire/segment connections are mixed up separately for each four-digit display! (All of the digits within a display use the same connections, though.)

//So, you might know that only signal wires b and g are turned on, but that doesn't mean segments b and g are turned on: the only digit that uses two segments is 1, so it must mean segments c and f are meant to be on. With just that information, you still can't tell which wire (b/g) goes to which segment (c/f). For that, you'll need to collect more information.

//For each display, you watch the changing signals for a while, make a note of all ten unique signal patterns you see, and then write down a single four digit output value (your puzzle input). Using the signal patterns, you should be able to work out which pattern corresponds to which digit.

//For example, here is what you might see in a single entry in your notes:

//acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab |
//cdfeb fcadb cdfeb cdbaf

//(The entry is wrapped here to two lines so it fits; in your notes, it will all be on a single line.)

//Each entry consists of ten unique signal patterns, a | delimiter, and finally the four digit output value. Within an entry, the same wire/segment connections are used (but you don't know what the connections actually are). The unique signal patterns correspond to the ten different ways the submarine tries to render a digit using the current wire/segment connections. Because 7 is the only digit that uses three segments, dab in the above example means that to render a 7, signal lines d, a, and b are on. Because 4 is the only digit that uses four segments, eafb means that to render a 4, signal lines e, a, f, and b are on.

//Using this information, you should be able to work out which combination of signal wires corresponds to each of the ten digits. Then, you can decode the four digit output value. Unfortunately, in the above example, all of the digits in the output value (cdfeb fcadb cdfeb cdbaf) use five segments and are more difficult to deduce.

//For now, focus on the easy digits. Consider this larger example:

//be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb |
//fdgacbe cefdb cefbgd gcbe
//edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec |
//fcgedb cgb dgebacf gc
//fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef |
//cg cg fdcagb cbg
//fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega |
//efabcd cedba gadfec cb
//aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga |
//gecf egdcabf bgf bfgea
//fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf |
//gebdcfa ecba ca fadegcb
//dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf |
//cefg dcbef fcge gbcadfe
//bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd |
//ed bcgafe cdgba cbgef
//egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg |
//gbdfcae bgc cg cgb
//gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc |
//fgae cfgab fg bagce

//Because the digits 1, 4, 7, and 8 each use a unique number of segments, you should be able to tell which combinations of signals correspond to those digits. Counting only digits in the output values (the part after | on each line), in the above example, there are 26 instances of digits that use a unique number of segments (highlighted above).

//In the output values, how many times do digits 1, 4, 7, or 8 appear?

//--- Part Two ---

//Through a little deduction, you should now be able to determine the remaining digits. Consider again the first example above:

//acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab |
//cdfeb fcadb cdfeb cdbaf

//After some careful analysis, the mapping between signal wires and segments only make sense in the following configuration:

//   dddd
//  e    a
//  e    a
//   ffff
//  g    b
//  g    b
//   cccc

//So, the unique signal patterns would correspond to the following digits:

//    acedgfb: 8
//    cdfbe: 5
//    gcdfa: 2
//    fbcad: 3
//    dab: 7
//    cefabd: 9
//    cdfgeb: 6
//    eafb: 4
//    cagedb: 0
//    ab: 1

//Then, the four digits of the output value can be decoded:

//    cdfeb: 5
//    fcadb: 3
//    cdfeb: 5
//    cdbaf: 3

//Therefore, the output value for this entry is 5353.

//Following this same process for each entry in the second, larger example above, the output value of each entry can be determined:

//    fdgacbe cefdb cefbgd gcbe: 8394
//    fcgedb cgb dgebacf gc: 9781
//    cg cg fdcagb cbg: 1197
//    efabcd cedba gadfec cb: 9361
//    gecf egdcabf bgf bfgea: 4873
//    gebdcfa ecba ca fadegcb: 8418
//    cefg dcbef fcge gbcadfe: 4548
//    ed bcgafe cdgba cbgef: 1625
//    gbdfcae bgc cg cgb: 8717
//    fgae cfgab fg bagce: 4315

//Adding all of the output values in this larger example produces 61229.

//For each entry, determine all of the wire/segment connections and decode the four-digit output values. What do you get if you add up all of the output values?


module Day08 =
    open System

    type Segment = char list
    type Segments = Segment list
    type SegmentInfo = Segments * Segments

    // Shared
    let inputFile = "./input/day08.txt"

    let ParseSegments (str:string) : Segments =
        str.Trim().Split(' ')
        |> Seq.map Seq.toList
        |> Seq.toList

    let ParseInput (str:string) =
        match str.Split("|") with
        | [|l;r|] -> ParseSegments l, ParseSegments r
        | _ -> failwith "Incompatible input: Line"

    let readInput =
        Library.ReadFile inputFile
        |> List.map ParseInput

    let isOneFourSevenOrEight (segment:Segment) =
        match segment.Length with
        | 2 | 3 | 4 | 7 ->  true
        | _ -> false

    let solutionPart1 =
        readInput
        |> List.map snd
        |> List.concat
        |> List.filter isOneFourSevenOrEight
        |> List.length
        |> sprintf "The answer is: %A"

//--- Part Two ---

// Display used:
//    aaaa 
//   f    b         0 = abcdef (6)    5 = acdfg    (5)  
//   f    b         1 = bc     (2)    6 = acdefg   (6)   
//    gggg          2 = abedg  (5)    7 = abc      (3)
//   e    c         3 = abcdg  (5)    8 = abcdefg  (7)    
//   e    c         4 = bcfg   (4)    9 = abcdfg   (6)   
//    dddd     digit^    ^segments                  ^number of segments   

// Example numbers:
//    aaaa                  aaaa       aaaa               
//   f    b          b          b          b     f    b   
//   f    b          b          b          b     f    b   
//                          gggg       gggg       gggg    
//   e    c          c     e               c          c   
//   e    c          c     e               c          c   
//    dddd                  dddd       dddd             
//
//    aaaa       aaaa       aaaa       aaaa       aaaa    
//   f          f               b     f    b     f    b   
//   f          f               b     f    b     f    b   
//    gggg       gggg                  gggg       gggg    
//        c     e    c          c     e    c          c   
//        c     e    c          c     e    c          c   
//    dddd       dddd                  dddd       dddd     

// Occurence of segments in digits:
// Segment a is in 8 numbers      // Segment e is in 4 numbers !
// Segment b is in 8 numbers      // Segment f is in 6 numbers !
// Segment c is in 9 numbers !    // Segment g is in 7 numbers
// Segment d is in 7 numbers

// Known numbers: 
// One    (2 segments)             // Four   (4 segments)
// Seven  (3 segments)             // Eight  (all seven segments)

// Conclusion:
// a = Seven - One            // e = Is in 4 numbers
// b = One - 'c'              // f = Is in 6 numbers
// c = Is in 9 numbers        // g = Four - 'bcf'
// d = Eight - 'abcefg'


    let numberOfSegments test (segment:Segment) = (segment.Length = test)
    let numberOfOccurences test (_,occurences) = (occurences = test)
    let charNotIn list char = (list |> List.contains char |> not)

    let decodeSegments (segments:Segments) =
        let occurences = segments |> List.concat |> List.countBy id
        
        // Numbers based on segment count
        let one   = segments |> List.find (numberOfSegments 2)
        let four  = segments |> List.find (numberOfSegments 4)
        let seven = segments |> List.find (numberOfSegments 3)
        let eight = segments |> List.find (numberOfSegments 7)

        // Individual segments
        let c = occurences |> List.find (numberOfOccurences 9) |> fst
        let e = occurences |> List.find (numberOfOccurences 4) |> fst
        let f = occurences |> List.find (numberOfOccurences 6) |> fst
        let a = seven |> List.find (charNotIn one)
        let b = one   |> List.find (charNotIn [c])
        let g = four  |> List.find (charNotIn [b;c;f])
        let d = eight |> List.find (charNotIn [a;b;c;e;f;g])
        
        // Create remaining numbers:
        let zero  = [a; b; c; d; e; f   ]
        let two   = [a; b;    d; e;    g]
        let three = [a; b; c; d;       g]
        let five  = [a;    c; d;    f; g]
        let six   = [a;    c; d; e; f; g]
        let nine  = [a; b; c; d;    f; g]

        // Return the mapping in the form of Key=char list, Value=int
        Map.empty
        |> Map.add (zero  |> List.sort)  0
        |> Map.add (one   |> List.sort)  1
        |> Map.add (two   |> List.sort)  2
        |> Map.add (three |> List.sort)  3
        |> Map.add (four  |> List.sort)  4
        |> Map.add (five  |> List.sort)  5
        |> Map.add (six   |> List.sort)  6
        |> Map.add (seven |> List.sort)  7
        |> Map.add (eight |> List.sort)  8
        |> Map.add (nine  |> List.sort)  9

    let digitListToNumber list =
        // Recursive number builder using powers of 10.
        // Starting with idx = 0 ; 10^0=1, 10^1=10, 10^2=100...
        let rec numberBuilder exp list =
            match list with
            | [] -> 0    // done
            | digit::tail -> (pown 10 exp)*digit + numberBuilder (exp+1) tail

        list
        |> List.rev          // Start with the least significant number first.
        |> numberBuilder 0   // Start recursive loop with 10^0=1.

    let decodeReading (observation, reading) =
        let decodingMap = decodeSegments observation
        reading
        |> List.map List.sort // sort characters alphabetically
        |> List.map (fun segment ->
            match decodingMap.TryFind segment with
            | Some number -> number
            | None -> failwith "Segment not found in decoding map"
        )
        |> digitListToNumber

    let solutionPart2 =
        readInput
        |> List.map decodeReading
        |> List.reduce(+)
        |> sprintf "The answer is: %A"
