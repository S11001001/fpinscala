namespace FPinScala.Exercises.ErrorHandling

open System

type Either<'e, 'a> = 
    | Left of 'e
    | Right of 'a

    member this.map (f: 'a -> 'b): Either<'e, 'b> =
        failwith "TODO"

    member this.flatMap (f: 'a -> Either<'e, 'b>): Either<'e, 'b> =
        failwith "TODO"

    member this.orElse (b: Either<'e, 'a>): Either<'e, 'a> =
        failwith "TODO"

    member this.map2 (b: Either<'e, 'b>) (f: ('a * 'b) -> 'c): Either<'e, 'c> =
        failwith "TODO"

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Either =
    let traverse (es: 'a list) (f: 'a -> Either<'e, 'b>): Either<'e, 'a list> =
        failwith "TODO"

    let sequence (es: Either<'e, 'a> list): Either<'e, 'a list> =
        failwith "TODO"

    let mean (xs: double list): Either<String, double> =
        match xs with
        | [] -> Left "mean of empty list!"
        | _ -> Right (List.sum xs / double (List.length xs))

    let safeDiv (x: int, y: int): Either<Exception, int> =
        try
            Right (x / y)
        with
        | e -> Left e

    let Try (a: unit -> 'a): Either<Exception, 'a> =
        try
            Right (a ())
        with
        | e -> Left e
