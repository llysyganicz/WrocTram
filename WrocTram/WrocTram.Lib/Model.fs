namespace WrocTram.Lib

type public BoardData(symbol : string, name : string, direction : string) =
    member this.Symbol = symbol
    member this.Name = name
    member this.Direction = direction

type public LineData (line : string, direction : string, minuteCount: decimal)=
    member this.Line = line
    member this.Direction = direction
    member this.MinuteCount = minuteCount