[![Build status](https://travis-ci.org/S11001001/fpinscala-fsharp.svg?branch=fsharp)](https://travis-ci.org/S11001001/fpinscala-fsharp) [Join the chat at ##fsharp](https://www.irccloud.com/#!/ircs://irc.freenode.net:7000/%23%23fsharp)

This repository contains exercises ported to F#, hints, and answers
for the book
[Functional Programming in Scala](http://manning.com/bjarnason/). Along
with the book itself, it's the closest you'll get to having your own
private functional programming tutor without actually having one.

Here's how to use this repository:

Each chapter in the book develops a fully working library of functions
and data types, built up through a series of exercises and example code
given in the book text. The shell of this working library and exercise
stubs live in
`FPinScala.Exercises/<chapter-description>.*.fs`, where
`<chapter-description>` is a package name that corresponds to the
chapter title (see below). When you begin working on a chapter, we
recommend you open the exercise file(s) for that chapter, and when you
encounter exercises, implement them in the exercises file and make sure
they work.

If you get stuck on an exercise, let's say exercise 4 in the chapter,
you can find hints in `answerkey/<chapter-description>/04.hint.txt` (if
no hints are available for a problem, the file will just have a single
'-' as its contents) and the answer along with an explanation of the
answer and any variations in
`answerkey/<chapter-description>/04.answer.scala` or
`04.answer.markdown`. The finished Scala modules, with all answers for
each chapter live in
`answers/src/main/scala/fpinscala/<chapter-description>`.  There are
no F# answers written yet.  Please feel
free to submit pull requests for alternate answers, improved hints, and
so on, so we can make this repo the very best resource for people
working through the book with F#.

Chapter descriptions:

* Chapter 2: gettingstarted
* Chapter 3: Datastructures
* Chapter 4: ErrorHandling
* Chapter 5: laziness
* Chapter 6: state
* Chapter 7: parallelism
* Chapter 8: testing
* Chapter 9: Parsing
* Chapter 10: monoids
* Chapter 11: monads
* Chapter 12: applicative
* Chapter 13: iomonad
* Chapter 14: localeffects
* Chapter 15: streamingio

You will need an F# compiler and build tool.  Check the *Use* section
for your operating system on [the F# website](http://fsharp.org/).

To build the code for the first time, if you are using Mono:

    $ xbuild

If you are using Microsoft's tools:

    $ msbuild

This will compile all the code.  You can run it again when you change
the code to check your compilation again.  MonoDevelop, Visual Studio,
and Xamarin all support running this build from their *Build* menus,
so you can build directly from your IDE.

You can also do:

    > console

to get a Scala REPL with access to your exercises, and

    > run

To get a menu of possible main methods to execute.

All code in this repository is
[MIT-licensed](http://opensource.org/licenses/mit-license.php). See the
LICENSE file for details.

Have fun, and good luck! Also be sure to check out [the community
wiki](https://github.com/fpinscala/fpinscala/wiki) for the **chapter
notes**, links to more reading, and more.

_Paul and Rúnar (original version)_

F# for a Scala book?
--------------------

> This is not a book about Scala. This book is an introduction to
> *functional programming* (FP), a radical, principled approach to
> writing software. We use Scala as the vehicle to get there, but you
> can apply the lessons herein to programming in any language.
>
> — *Functional Programming in Scala*, p.xvii

As the book itself says, it is about FP in any language, and Scala is
merely a medium.  I admit that there is an added challenge to learning
functional programming and simultaneously translating prose targeted
at Scala to another programming language.  However, I expect this
challenge to be overcome readily by the intrepid FP learner.

For one thing, this book exceeds in quality of FP instruction any of
the F#-oriented materials I have examined.  That is, while there are
excellent books for F#, the material for learning FP in F# is lacking.

Additionally, I believe F# to be a better medium in certain ways for
learning FP than Scala.  Its syntax owes exceptional beauty to its
OCaml derivation.  Inference is better for most purposes, and its
standard library is focused on easily composable functions rather than
methods.  F# has syntax for FP concepts that must be simulated in F#,
like ADTs ("discriminated unions").  Subtyping, a major source of type
system confusion, is greatly deemphasized.

Were it not for the unfortunate omission of higher-kinded types from
F#, or any feature that could simulate them, F# would be an
unqualified improvement over Scala for the purposes of 

_modifications by Stephen Compall_
