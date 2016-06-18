namespace FPinScala.Answers.ErrorHandling

open System

type 'A Option =
    | None
    | Some of 'A

    member this.map (f: 'A -> 'B): 'B Option =
        match this with
        | None -> None
        | Some a -> Some (f a)

    member this.getOrElse (deflt: 'A): 'A =
        match this with
        | None -> deflt
        | Some a -> a

    member this.flatMap (f: 'A -> 'B Option): 'B Option =
        this.map(f).getOrElse(None)

    // Of course, we can also implement `flatMap` with explicit pattern matching.
    member this.flatMap_1 (f: 'A -> 'B Option): 'B Option =
        match this with
        | None -> None
        | Some a -> f(a)

    member this.orElse (ob: 'A Option): 'A Option =
        this.map(Some).getOrElse(ob)

    // Again, we can implement this with explicit pattern matching.
    member this.orElse_1 (ob: 'A Option): 'A Option =
        match this with
        | None -> ob
        | _ -> this

    member this.filter (f: 'A -> bool): 'A Option =
        match this with
        | Some a when f a -> this
        | _ -> None

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
        (mean xs).flatMap(fun m -> xs |> List.map (fun x -> Math.Pow(x - m, 2.0)) |> mean)

    let map2 (a: 'A Option) (b: 'B Option) (f: 'A -> 'B -> 'C): 'C Option =
        a.flatMap(fun aa -> b.map(fun bb -> f aa bb))

    // Here's an explicit recursive version:
    let rec sequence (a: 'A Option list): 'A list Option =
        match a with
        | [] -> Some []
        | h :: t -> h.flatMap(fun hh -> sequence(t).map(fun z -> hh :: z))

    // It can also be implemented using `foldBack` and `map2`.
    let sequence_1 (a: 'A Option list): 'A list Option =
        List.foldBack (fun x y -> map2 x y (fun v vs -> v :: vs)) a (Some [])

    let rec traverse (a: 'A list) (f: 'A -> 'B Option): 'B list Option =
        match a with
        | [] -> Some []
        | h :: t -> map2 (f h) (traverse t f) (fun v vs -> v :: vs)

    let traverse_1 (a: 'A list) (f: 'A -> 'B Option): 'B list Option =
        List.foldBack (fun h t -> map2 (f h) t (fun v vs -> v :: vs))
                      a (Some [])

    let sequenceViaTraverse (a: 'A Option list): 'A list Option =
        traverse a id
