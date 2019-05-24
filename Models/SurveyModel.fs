namespace WSP.Models

open System.Collections.Generic
open System.ComponentModel.DataAnnotations

type SurveyModel() =
    [<Required>]
    member val Name = "" with get, set

    [<Required>]
    member val Email = "" with get, set

    member val Restrictions = "" with get, set

    [<Required>]
    member val StayLength = "" with get, set

    member val Comments = "" with get, set

    member val PreferredRoles : seq<string> = Seq.empty with get, set
    member val RatherNotRoles : seq<string> = Seq.empty with get, set

    member val RoleDescriptions = Dictionary<string, string>() with get, set