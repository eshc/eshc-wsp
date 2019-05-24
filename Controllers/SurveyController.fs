namespace WSP.Controllers

open WSP.DataStore
open WSP.Models
open Microsoft.AspNetCore.Mvc
open System.Collections.Generic
open System.Collections.Generic

type SurveyController () =
    inherit Controller()

    member private this.GetRoleLookup() = 
        Dictionary<string, string>(
            Roles.getRoles () 
            |> Seq.map (fun r -> KeyValuePair(r.Role, r.Description)))

    member this.Survey() =
        let lookup = this.GetRoleLookup()
        let preferred = lookup.Keys |> Seq.toList
        let model = SurveyModel(PreferredRoles = preferred, RoleDescriptions = lookup)
        this.View(model)

    [<HttpPost>]
    member this.Survey(model : SurveyModel) =
        let preferred = 
            this.Request.Form.Item "PreferredRoles"
            |> List.ofSeq
        let ratherNot = 
            this.Request.Form.Item "RatherNotRoles"
            |> List.ofSeq
        model.PreferredRoles <- preferred
        model.RatherNotRoles <- ratherNot
        let lookup = this.GetRoleLookup()
        model.RoleDescriptions <- lookup
        if this.ModelState.IsValid then
            let preferred = preferred |> Seq.map string |> String.concat ":"
            let ratherNot = ratherNot |> Seq.map string |> String.concat ":"

            let entry = 
                Entries.Entry(
                    name = model.Name,
                    email = model.Email,
                    restrictions = model.Restrictions,
                    stayLength = model.StayLength,
                    comments = model.Comments,
                    preferredRoles = preferred,
                    ratherNotRoles = ratherNot)
            Entries.addEntry entry
            this.View("SurveySuccess") 
        else
            this.View(model)