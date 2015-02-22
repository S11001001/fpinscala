namespace FPinScala.Exercises.Datastructures

open System

type 'a List = // `List` data type, parameterized on a type, `A`
    | Nil      // A `List` data constructor representing the empty list
    | Cons of 'a * 'a List // Another data constructor, representing nonempty lists. Note that it contains another `'a list`, which may be `Nil` or another `Cons`.

    static member apply ([<ParamArray>] xs: 'a[]): 'a List = // Variadic member syntax
        failwith "TODO s11"

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module List = // `List` companion module. Contains functions for creating and working with lists.
    let rec sum (ints: int List): int = // `rec` declares a recursive function
        match ints with // A function that uses pattern matching to add up a list of integers
        | Nil -> 0 // The sum of the empty list is 0.
        | Cons (x,xs) -> x + sum xs // The sum of a list starting with `x` is `x` plus the sum of the rest of the list.
  
    let rec product (ds: double List): double = 
        match ds with
        | Nil -> 1.0
        | Cons (0.0, _) -> 0.0
        | Cons (x,xs) -> x * product xs
  
    let x = match List.apply(1,2,3,4,5) with
            | Cons(x, Cons(2, Cons(4, _))) -> x
            | Nil -> 42 
            | Cons(x, Cons(y, Cons(3, Cons(4, _)))) -> x + y
            | Cons(h, t) -> h + sum(t)
            | _ -> 101 

    let rec append (a1: 'a List, a2: 'a List): 'a List =
        match a1 with
        | Nil -> a2
        | Cons(h, t) -> Cons (h, append (t, a2))

    let rec foldRight (elts: 'a List, z: 'b) (f: ('a * 'b) -> 'b): 'b = // Utility functions
        match elts with
        | Nil -> z
        | Cons(x, xs) -> f (x, foldRight (xs, z) f)
  
    let sum2 (ns: int List) = 
        foldRight (ns, 0) (fun (x,y) -> x + y)
  
    let product2 (ns: double List) =
        foldRight (ns, 1.0) (fun (x,y) -> x * y) // no shorthand syntax for lambdas exists

    let tail (l: 'a List): 'a List = failwith "TODO"

    let setHead (l: 'a List, h: 'a): 'a List = failwith "TODO"

    let drop (l: 'a List, n: int): 'a List = failwith "TODO"

    let dropWhile (l: 'a List, f: 'a -> bool): 'a List = failwith "TODO"

    let init (l: 'a List): 'a List = failwith "TODO"

    let length (l: 'a List): int = failwith "TODO"

    let foldLeft (l: 'a List, z: 'b) (f: ('b * 'a) -> 'b): 'b = failwith "TODO"

    let map (l: 'a List) (f: 'a -> 'b): 'b List = failwith "TODO"
