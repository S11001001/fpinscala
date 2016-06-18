namespace FPinScala.Exercises.ErrorHandling

open System

type Either<'E, 'A> = 
    | Left of 'E
    | Right of 'A

    member this.map (f: 'A -> 'B): Either<'E, 'B> =
        failwith "TODO"

    member this.flatMap (f: 'A -> Either<'E, 'B>): Either<'E, 'B> =
        failwith "TODO"

    member this.orElse (b: Either<'E, 'A>): Either<'E, 'A> =
        failwith "TODO"

    member this.map2 (b: Either<'E, 'B>) (f: ('A * 'B) -> 'c): Either<'E, 'c> =
        failwith "TODO"

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Either =
    let traverse (es: 'A list) (f: 'A -> Either<'E, 'B>): Either<'E, 'A list> =
        failwith "TODO"

    let sequence (es: Either<'E, 'A> list): Either<'E, 'A list> =
        failwith "TODO"

    let mean (xs: double list): Either<String, double> =
        match xs with
        | [] -> Left "mean of empty list!"
        | _ -> Right (List.sum xs / double (List.length xs))

    let safeDiv (x: int) (y: int): Either<Exception, int> =
        try
            Right (x / y)
        with
        | e -> Left e

    let Try (a: unit -> 'A): Either<Exception, 'A> =
        try
            Right (a ())
        with
        | e -> Left e
