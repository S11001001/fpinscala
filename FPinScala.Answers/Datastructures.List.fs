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
    let tail (l: 'a List): 'a List =
        match l with
        | Nil -> failwith "tail of empty list"
        | Cons (_, t) -> t

    // If a function body consists solely of a match expression, we'll
    // often put the match on the same line as the function signature,
    // rather than introducing another level of nesting.
    let setHead (l: 'a List) (h: 'a): 'a List =
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
    let rec drop (l: 'a List) (n: int): 'a List =
        if (n <= 0) then l
        else match l with
             | Nil -> Nil
             | Cons (_, t) -> drop t (n-1)

    // Somewhat overkill, but to illustrate the feature we're using a
    // _pattern guard_, to only match a `Cons` whose head satisfies
    // our predicate, `f`. The syntax is to add `when <cond>` after the
    // pattern, before the `->`, where `<cond>` can use any of the
    // variables introduced by the pattern.
    let rec dropWhile (l: 'a List) (f: 'a -> bool): 'a List =
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
    let rec init (l: 'a List): 'a List =
        match l with
        | Nil -> failwith "init of empty list"
        | Cons (_, Nil) -> Nil
        | Cons (h, t) -> Cons (h, init t)

    let init2 (l: 'a List): 'a List =
        let buf = new System.Collections.Generic.List<'a>()
        let rec go (cur: 'a List): 'a List = 
            match cur with
            | Nil -> failwith "init of empty list"
            | Cons (_, Nil) -> List.apply(buf.ToArray())
            | Cons (h, t) -> buf.Add h
                             go(t)
        go l

    let length (l: 'a List): int =
        List.foldRight l 0 (fun _ acc -> acc + 1)

    let rec foldLeft (l: 'a List) (z: 'b) (f: 'b -> 'a -> 'b): 'b =
        match l with
        | Nil -> z
        | Cons (h, t) -> foldLeft t (f z h) f

    let map (l: 'a List) (f: 'a -> 'b): 'b List = failwith "TODO"
