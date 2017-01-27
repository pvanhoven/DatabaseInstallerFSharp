open System
open Argu

type CliArguments =
    | Export
    | Install
    | Listener of host:string * port:int
with
    interface IArgParserTemplate with
        member s.Usage =
            match s with
            | Export _ -> "specify if the database should be exported into scripts."
            | Install _ -> "specify if a new database instance should be created."
            | Listener _ -> "specify a listener (hostname : port)."

[<EntryPoint>]
let main argv = 
    printfn "%A" argv

    let parser = ArgumentParser.Create<CliArguments>(programName = "databaseinstallerfsharp.exe")
    let result = parser.Parse(argv)
    let all = result.GetAllResults()

    let export = result.Contains<@ Export @>
    if export then Console.WriteLine("Exporting")

    if result.Contains<@ Install @> then Console.WriteLine("Installing")

    if result.Contains<@ Listener @> then Console.WriteLine("Listener defined")
    let listener = result.GetResult<@ Listener @>
    if result.Contains<@ Listener @> then Console.WriteLine(listener);

    Console.ReadLine() |> ignore

    0 // return an integer exit code
