namespace WSP.DataStore

open System
open FSharp.Data
open System.IO

type WspRoles = CsvProvider<"WspRoles.csv">

module Roles = 
    type Role = WspRoles.Row

    let getRoles () = 
        let wd = Directory.GetCurrentDirectory()
        let file = Path.Combine(wd, "WspRoles.csv")
        use csv = WspRoles.Load file
        csv.Rows |> Seq.toList

type WspEntries = CsvProvider<"WspResponsesTemplate.csv">

module Entries = 
    type Entry = WspEntries.Row

    let addEntry entry =
        let wd = Directory.GetCurrentDirectory()
        let bkp = Path.Combine(wd, "Backup")
        let file = Path.Combine(wd, "WspResponses.csv")
        if Directory.Exists bkp |> not then
            Directory.CreateDirectory(bkp) |> ignore
        let contents =
            use entries = 
                if File.Exists file |> not then
                    WspEntries.GetSample()
                else
                    WspEntries.Load file
            let entries' = entries.Append([entry])
            entries'.SaveToString()

        File.WriteAllText(file, contents)

        let bkpFile = 
            Path.Combine(bkp, 
                sprintf "WspResponses_%s.csv" (DateTime.Now.ToString("yyyy-MM-dd_HH-mm")))
        File.WriteAllText(bkpFile, contents)