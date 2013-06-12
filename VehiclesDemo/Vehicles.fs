module Vehicles

#if INTERACTIVE
    #r @"System.Data.dll"
    #r @"System.Data.Linq.dll"
    #r @"FSharp.Data.TypeProviders.dll"
#endif

open System
open System.Data
open System.Data.Linq
open Microsoft.FSharp.Data.TypeProviders
open Microsoft.FSharp.Linq

type dbSchema = Microsoft.FSharp.Data.TypeProviders.SqlDataConnection<"Data Source=.;Initial Catalog=VehiclesDatabase;Integrated Security=SSPI;">

let ListPeople count =
    let db = dbSchema.GetDataContext()

    db.People
    |> Seq.truncate count
    |> Seq.iter (fun person -> printfn "%s %s" person.Forename person.Surname)

let ListPeopleQuery count =
    let db = dbSchema.GetDataContext()
    query {
        for person in db.People do
        select (person.Forename, person.Surname)
    }
    |> Seq.truncate count
    |> Seq.iter (fun person -> printfn "%A" person)
    
let ListPeopleAndVehiclesQuery count =
    let db = dbSchema.GetDataContext()
    query {
        for person in db.People do
        join vk in db.VehicleKeepers on (person.Id = vk.PersonId)
        join veh in db.Vehicles on (vk.VehicleId = veh.Id)
        join vm in db.VehicleModels on (veh.VehicleModelId = vm.Id)
        join vma in db.VehicleMakes on (vm.VehicleMakeId = vma.Id)
        select (person.Forename, person.Surname, veh.Color, vm.Name, vma.Name)
    }
    |> Seq.truncate count
    |> Seq.iter (fun item -> printfn "%A" item)
