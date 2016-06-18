namespace FPinScala.Answers.Datastructures

open FPinScala.Exercises.Datastructures
open System

[<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
module List2 = // `List` companion module. Contains functions for creating and working with lists.

    // Although we could return `Nil` when the input list is empty, we
    // choose to throw an exception instead. This is a somewhat
    // subjective choice. In our experience, taking the tail of an
    // empty list is often a bug, and silently returning a value just means
    // this bug will be discovered later, further from the place where
    // it was introduced.
    //
    // It's generally good practice when pattern matching to use `_`
    // for any variables you don't intend to use on the right hand
    // side of a pattern. This makes it clear the value isn't
    // relevant.
    let tail (l: 'A List): 'A List =
        match l with
        | Nil -> failwith "tail of empty list"
        | Cons (_, t) -> t

    // If a function body consists solely of a match expression, we'll
    // often put the match on the same line as the function signature,
    // rather than introducing another level of nesting.
    let setHead (l: 'A List) (h: 'A): 'A List =
        match l with
        | Nil -> failwith "setHead on empty list"
        | Cons (_, t) -> Cons (h, t)

    // Again, it's somewhat subjective whether to throw an exception
    // when asked to drop more elements than the list contains. The
    // usual default for `drop` is not to throw an exception, since
    // it's typically used in cases where this is not indicative of a
    // programming error. If you pay attention to how you use `drop`,
    // it's often in cases where the length of the input list is
    // unknown, and the number of elements to be dropped is being
    // computed from something else. If `drop` threw an exception,
    // we'd have to first compute or check the length and only drop up
    // to that many elements.
    let rec drop (l: 'A List) (n: int): 'A List =
        if (n <= 0) then l
        else match l with
             | Nil -> Nil
             | Cons (_, t) -> drop t (n-1)

    // Somewhat overkill, but to illustrate the feature we're using a
    // _pattern guard_, to only match a `Cons` whose head satisfies
    // our predicate, `f`. The syntax is to add `when <cond>` after the
    // pattern, before the `->`, where `<cond>` can use any of the
    // variables introduced by the pattern.
    let rec dropWhile (l: 'A List) (f: 'A -> bool): 'A List =
        match l with
        | Cons (h, t) when f h -> dropWhile t f
        | _ -> l

    // Note that we're copying the entire list up until the last
    // element. Besides being inefficient, the natural recursive
    // solution will use a stack frame for each element of the list,
    // which can lead to stack overflows for large lists (can you see
    // why?). With lists, it's common to use a temporary, mutable
    // buffer internal to the function (with lazy lists or streams,
    // which we discuss in chapter 5, we don't normally do this). So
    // long as the buffer is allocated internal to the function, the
    // mutation is not observable and RT is preserved.
    //
    // Another common convention is to accumulate the output list in
    // reverse order, then reverse it at the end, which doesn't
    // require even local mutation. We'll write a reverse function
    // later in this chapter.
    let rec init (l: 'A List): 'A List =
        match l with
        | Nil -> failwith "init of empty list"
        | Cons (_, Nil) -> Nil
        | Cons (h, t) -> Cons (h, init t)

    let init2 (l: 'A List): 'A List =
        let buf = new System.Collections.Generic.List<'A>()
        let rec go (cur: 'A List): 'A List = 
            match cur with
            | Nil -> failwith "init of empty list"
            | Cons (_, Nil) -> List.apply(buf.ToArray())
            | Cons (h, t) -> buf.Add h
                             go(t)
        go l

    let length (l: 'A List): int =
        List.foldRight l 0 (fun _ acc -> acc + 1)

    let rec foldLeft (l: 'A List) (z: 'B) (f: 'B -> 'A -> 'B): 'B =
        match l with
        | Nil -> z
        | Cons (h, t) -> foldLeft t (f z h) f

    let sum3 (l: int List) = foldLeft l 0 (fun x y -> x + y)
    let product3 (l: double List) = foldLeft l 1.0 (fun x y -> x * y)

    let length2 (l: 'A List): int = foldLeft l 0 (fun acc h -> acc + 1)

    let reverse (l: 'A List): 'A List = foldLeft l Nil (fun acc h -> Cons(h,acc))

    // The implementation of `foldRight` in terms of `reverse` and
    // `foldLeft` is a common trick for avoiding stack overflows when
    // implementing a strict `foldRight` function as we've done in
    // this chapter. (We'll revisit this in a later chapter, when we
    // discuss laziness).
    //
    // The other implementations build up a chain of functions which,
    // when called, results in the operations being performed with the
    // correct associativity. We are calling `foldRight` with the `'B`
    // type being instantiated to `'B -> 'B`, then calling the built
    // up function with the `z` argument. Try expanding the
    // definitions by substituting equals for equals using a simple
    // example, like `foldLeft (List.apply(1,2,3)) 0 (fun x y -> x +
    // y)` if this isn't clear. Note these implementations are more of
    // theoretical interest - they aren't stack-safe and won't work
    // for large lists.
    let foldRightViaFoldLeft (l: 'A List) (z: 'B) (f: 'A -> 'B -> 'B): 'B =
      foldLeft (reverse l) z (fun b a -> f a b)

    let foldRightViaFoldLeft_1 (l: 'A List) (z: 'B) (f: 'A -> 'B -> 'B): 'B =
      foldLeft l (fun b -> b) (fun g a b -> g (f a b)) z

    let foldLeftViaFoldRight (l: 'A List) (z: 'B) (f: 'B -> 'A -> 'B): 'B =
      List.foldRight l (fun b -> b) (fun a g b -> g (f b a)) z

  (*
  `append` simply replaces the `Nil` constructor of the first list with the second list, which is exactly the operation performed by `foldRight`.
  *)
    let appendViaFoldRight (l: 'A List) (r: 'A List): 'A List =
      List.foldRight l r (fun h t -> Cons(h, t))

   (*
  Since `append` takes time proportional to its first argument, and this first argument never grows because of the right-associativity of `foldRight`, this function is linear in the total length of all lists. You may want to try tracing the execution of the implementation on paper to convince yourself that this works.

  Note that we're simply referencing the `append` function, without writing something like `fun x y -> append x y`. In other languages there is a rather arbitrary distinction between functions defined as _methods_ and function values, which are the first-class objects we can pass to other functions, put in collections, and so on.
  *)
    let concat (l: 'A List List): 'A List =
      List.foldRight l Nil List.append

    let add1 (l: int List): int List =
      List.foldRight l Nil (fun h t -> Cons(h+1,t))

    let doubleToString(l: double List): String List =
      List.foldRight l Nil (fun h t -> Cons(h.ToString(),t))

    let map (l: 'A List) (f: 'A -> 'B): 'B List = failwith "TODO"
