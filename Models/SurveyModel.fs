namespace WSP.Models

open System.Collections.Generic
open System.ComponentModel.DataAnnotations

type WorkingGroupModel() =
    member val Name = "" with get, set
    member val Style = "" with get, set

type RoleModel() =
    member val Name = "" with get, set
    member val Style = "" with get, set

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

    member val Roles = Dictionary<string, RoleModel>() with get, set
    member val WorkingGroups = Dictionary<string, WorkingGroupModel>() with get, set