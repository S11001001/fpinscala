namespace FPinScala.Exercises.ErrorHandling

open System

type 'A Parser =
    | Parser of unit                 // TODO describe actual structure

type Location =
    {Input: String; Offset: int}

    member this.line =
        (this.Input.Substring(0, this.Offset + 1).ToCharArray()
         |> Seq.ofArray |> Seq.filter (fun c -> c = '\n') |> Seq.length) + 1

    member this.col =
        this.Offset + 1 - this.Input.Substring(0, this.Offset + 1).LastIndexOf('\n')

    member this.toError (msg: string): ParseError =
        {Stack = [this, msg]; OtherFailures = []}

    member this.advanceBy (n: int) =
        {Input = this.Input; Offset = this.Offset + n}

    member this.currentLine: String =
        if (this.Input.Length > 1)
        then this.Input.Split('\n') |> Seq.nth (this.line - 1)
        else ""

and ParseError = {Stack: (Location * String) list;
                  OtherFailures: ParseError list}
