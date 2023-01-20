(in webapp directory)
dotnet aspnet-codegenerator razorpage -m Category -dc AppDbContext -udl -outDir Pages/Categories --referenceScriptLibraries
dotnet aspnet-codegenerator razorpage -m Item -dc AppDbContext -udl -outDir Pages/Items --referenceScriptLibraries
dotnet aspnet-codegenerator razorpage -m Job -dc AppDbContext -udl -outDir Pages/Jobs --referenceScriptLibraries
dotnet aspnet-codegenerator razorpage -m PerformedJob -dc AppDbContext -udl -outDir Pages/PerformedJobs --referenceScriptLibraries
dotnet aspnet-codegenerator razorpage -m ItemInStock -dc AppDbContext -udl -outDir Pages/ItemsInStock --referenceScriptLibraries
dotnet aspnet-codegenerator razorpage -m JobItem -dc AppDbContext -udl -outDir Pages/JobItems --referenceScriptLibraries
dotnet aspnet-codegenerator razorpage -m UsedItem -dc AppDbContext -udl -outDir Pages/UsedItems --referenceScriptLibraries