namespace WrocTram.Lib

open FSharp.Data

type Boards = JsonProvider<Sample="Boards.json", Encoding="Utf-8", EmbeddedResource="WrocTram.Lib, boards.json", InferTypesFromValues=false>
type BoardDetails = JsonProvider<Sample="BoardDetails.json", Encoding="Utf-8", EmbeddedResource="WrocTram.Lib, boardDetails.json", InferTypesFromValues=false>
type Lines = JsonProvider<Sample="Lines.json", Encoding="Utf-8", EmbeddedResource="WrocTram.Lib, lines.json", InferTypesFromValues=false>

type DataProvider() = 
    [<Literal>]
    let boardsAddress = """http://tram.wroclaw.pl/ws/board/post?term="""
    [<Literal>]
    let boardDetailsAddress = """http://tram.wroclaw.pl/ws/board/show/"""

    let getDirection (lines : Boards.Line[]) =
        let direction = ""
        let join t1 t2 = sprintf "%s %s" t1 t2
        lines |> Array.map (fun line -> sprintf "%s -> %s" (line.JsonValue.GetProperty("line").AsString()) line.Direction) |> Array.fold join ""

    let getBoardsAsync address =
        async {
            let! boards = Boards.AsyncLoad address
            return boards |> Array.map (fun board -> new BoardData(board.Symbol, board.Name, getDirection board.Lines))
        }

    let getBoardDetailsAsync boardSymbol = 
        async {
            let! boardDetails = BoardDetails.AsyncLoad(boardDetailsAddress + boardSymbol)
            let details = boardDetails.JsonValue.GetProperty(boardSymbol).GetProperty("board")
            let lines = Lines.Parse(details.ToString())
            return lines |> Array.map (fun line -> new LineData(line.Line, line.Direction, line.MinuteCount))
        }

    member this.GetBoards (boardName : string) = 
        let address = boardsAddress + boardName
        Async.RunSynchronously(getBoardsAsync address)

    member this.GetLines boardSymbol =
        Async.RunSynchronously (getBoardDetailsAsync boardSymbol)