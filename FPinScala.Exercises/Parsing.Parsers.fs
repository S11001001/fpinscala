namespace FPinScala.Exercises.ErrorHandling

open System

type 'a Parser =
    | Parser of unit                 // TODO describe actual structure

type Location =
    {Input: String; Offset: int}

    member this.line = failwith "TODO"

    member this.col = failwith "TODO"

    member this.toError (msg: string): ParseError =
        {Stack = [this, msg]; OtherFailures = []}

    member this.advanceBy (n: int) =
        {Input = this.Input; Offset = this.Offset + n}

    member this.currentLine: String =
        if (this.Input.Length > 1) then failwith "TODO" 
       else ""

and ParseError = {Stack: (Location * String) list;
                  OtherFailures: ParseError list}
