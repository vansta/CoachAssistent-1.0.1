dotnet ef migrations add NameFix --project .\CoachAssistent.Data --startup-project .\CoachAssistent.Api\

dotnet ef migrations remove --project .\CoachAssistent.Data --startup-project .\CoachAssistent.Api\

dotnet ef database update --project .\CoachAssistent.Data --startup-project .\CoachAssistent.Api\