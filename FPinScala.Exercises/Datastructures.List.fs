﻿namespace FPinScala.Exercises.Datastructures

open System

type 'A List = // `List` data type, parameterized on a type, `A`
    | Nil      // A `List` data constructor representing the empty list
    // Another data constructor, representing nonempty lists. Note
    // that it contains another `'A List`, which may be `Nil` or
    // another `Cons`.
    | Cons of 'A * 'A List

    static member apply ([<ParamArray>] xs: 'A[]): 'A List = // Variadic member syntax
        Array.foldBack (fun x xs -> Cons (x, xs)) xs Nil

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

    let rec append (a1: 'A List) (a2: 'A List): 'A List =
        match a1 with
        | Nil -> a2
        | Cons(h, t) -> Cons (h, append t a2)

    let rec foldRight (elts: 'A List) (z: 'B) (f: 'A -> 'B -> 'B): 'B = // Utility functions
        match elts with
        | Nil -> z
        | Cons(x, xs) -> f x (foldRight xs z f)
  
    let sum2 (ns: int List) = 
        foldRight ns 0 (fun x y -> x + y)
  
    let product2 (ns: double List) =
        foldRight ns 1.0 (fun x y -> x * y) // no shorthand syntax for lambdas exists

    let tail (l: 'A List): 'A List = failwith "TODO"

    let setHead (l: 'A List) (h: 'A): 'A List = failwith "TODO"

    let drop (l: 'A List) (n: int): 'A List = failwith "TODO"

    let dropWhile (l: 'A List) (f: 'A -> bool): 'A List = failwith "TODO"

    let init (l: 'A List): 'A List = failwith "TODO"

    let length (l: 'A List): int = failwith "TODO"

    let foldLeft (l: 'A List) (z: 'B) (f: 'B -> 'A -> 'B): 'B = failwith "TODO"

    let map (l: 'A List) (f: 'A -> 'B): 'B List = failwith "TODO"
