namespace FPinScala.Exercises.ErrorHandling

type 'a Option =
    | None
    | Some of 'a

    member this.map (f: 'a -> 'b): 'b Option =
        failwith "todo"

    member this.getOrElse (deflt: 'a): 'a =
        failwith "todo"

    member this.flatMap (f: 'a -> 'b Option): 'b Option =
        failwith "todo"

    member this.orElse (ob: 'a Option): 'a Option =
        failwith "todo"

    member this.filter (f: 'a -> bool): 'a Option =
        failwith "todo"

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module Option =
    let mean (xs: double list): double Option =
        match xs with
        | [] -> None
        | _  -> Some (List.sum xs / double (List.length xs))

    let variance (xs: double list): double Option =
        failwith "todo"

    let map2 (a: 'a Option, b: 'b Option) (f: ('a * 'b) -> 'c): 'c Option =
        failwith "todo"

    let sequence (a: 'a Option list): 'a list Option =
        failwith "todo"

    let traverse (a: 'a list) (f: 'a -> 'b Option): 'b list Option =
        failwith "todo"
