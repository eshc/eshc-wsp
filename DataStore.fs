namespace WSP.DataStore

open System
open FSharp.Data
open System.IO

module Helpers =
    let csvPath file =
        let wd = Directory.GetCurrentDirectory()
        let file = Path.Combine(wd, file)
        file

module WorkingGroups = 
    type Provider = CsvProvider<"WspWorkingGroups.csv">
    type WorkingGroup = Provider.Row

    let getWorkingGroups () =
        use csv = Helpers.csvPath "WspWorkingGroups.csv" |> Provider.Load
        csv.Rows |> Seq.toList

module Roles = 
    type Provider = CsvProvider<"WspRoles.csv">
    type Role = Provider.Row

    let getRoles () = 
        use csv = Helpers.csvPath "WspRoles.csv" |> Provider.Load
        csv.Rows |> Seq.toList

module Entries = 
    type Provider = CsvProvider<"WspResponsesTemplate.csv">
    type Entry = Provider.Row

    let addEntry entry =
        let wd = Directory.GetCurrentDirectory()
        let bkp = Path.Combine(wd, "Backup")
        let file = Path.Combine(wd, "WspResponses.csv")
        if Directory.Exists bkp |> not then
            Directory.CreateDirectory(bkp) |> ignore
        let contents =
            use entries = 
                if File.Exists file |> not then
                    Provider.GetSample()
                else
                    Provider.Load file
            let entries' = entries.Append([entry])
            entries'.SaveToString()

        File.WriteAllText(file, contents)

        let bkpFile = 
            Path.Combine(bkp, 
                sprintf "WspResponses_%s.csv" (DateTime.Now.ToString("yyyy-MM-dd_HH-mm")))
        File.WriteAllText(bkpFile, contents)