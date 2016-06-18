namespace FPinScala.Answers.ErrorHandling

open System

type 'A Option =
    | None
    | Some of 'A

    member this.map (f: 'A -> 'B): 'B Option =
        failwith "todo"

    member this.getOrElse (deflt: 'A): 'A =
        failwith "todo"

    member this.flatMap (f: 'A -> 'B Option): 'B Option =
        failwith "todo"

    member this.orElse (ob: 'A Option): 'A Option =
        failwith "todo"

    member this.filter (f: 'A -> bool): 'A Option =
        failwith "todo"

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Option =
    let failingFn (i: int): int =
        let y: int = raise (Exception "fail!") // `let y: int = ...` declares `y` as having type `int`, and sets it equal to the right hand side of the `=`.
        try
            let x = 42 + 5
            x + y
        with
        | _ -> 43 // A `catch` block is just a pattern matching block like the ones we've seen. `_` is a pattern that matches anything (which must be an `Exception` in this case), and it binds this value to the identifier `e`. The match returns the value 43.

    let failingFn2 (i: int): int =
        try
            let x = 42 + 5
            x + (raise (Exception "fail!"): int) // A thrown Exception can be given any type; here we're annotating it with the type `int`
        with
        | _ -> 43

    let mean (xs: double list): double Option =
        match xs with
        | [] -> None
        | _  -> Some (List.sum xs / double (List.length xs))

    let variance (xs: double list): double Option =
        failwith "todo"

    let map2 (a: 'A Option) (b: 'B Option) (f: 'A -> 'B -> 'C): 'C Option =
        failwith "todo"

    let sequence (a: 'A Option list): 'A list Option =
        failwith "todo"

    let traverse (a: 'A list) (f: 'A -> 'B Option): 'B list Option =
        failwith "todo"
