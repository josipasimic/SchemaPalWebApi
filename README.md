# SchemaPal Web API

Pomoćna ASP.NET Web API aplikacija za autentifikaciju i autorizaciju korisnika te upravljanje podacima u sklopu klijentske aplikacije **SchemaPal** (https://github.com/josipasimic/SchemaPal).

---

## Napomene

Aplikacija "defaultno" koristi in-memory upravljanje podacima, to jest, podaci o korisniku i njegovim shemama se spremaju u memoriju aplikacije i samim time žive dok živi i aplikacija. 

Moguće je konfigurirati aplikaciju da koristi bazu podataka. Za korištenje lokalne baze podataka, potrebno je:

1. Kreirati MSS bazu podataka koristeći skriptu u datoteci:  
   **`SqlScripts/CreateSchemaPalDb.sql`**.

2. Prilagoditi konfiguraciju u datoteci **`Program.cs`**:  

Zakomentirajte linije (41–42):  
```csharp
// builder.Services.AddSingleton<IUserRepository, InMemoryUserRepository>();
// builder.Services.AddSingleton<IDatabaseSchemaRepository, InMemoryDatabaseSchemaRepository>();
```

te otkomentirajte linije 46-49:
```csharp
builder.Services.AddDbContext<SchemaPalContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SchemaPalDb")));
builder.Services.AddScoped<IUserRepository, SqlUserRepository>();
builder.Services.AddScoped<IDatabaseSchemaRepository, SqlDatabaseSchemaRepository>();
```

Podaci za spajanje na bazu se nalaze u datoteci **`appsettings.json`** pod oznakom  **`ConnectionStrings`** (linije 8-10). Ako se koristi baza podataka na lokalnom serveru, u pravilu se ti podaci ne trebaju mijenjati.
```csharp
"ConnectionStrings": {
    "SchemaPalDb": "Server=localhost;Database=SchemaPalDb;Trusted_Connection=True;TrustServerCertificate=True;"
  },
```

Kako bi lokalni proces aplikacije SchemaPal mogao koristiti API ove aplikacije, potrebno je u datoteci **`Program.cs`** (linije 53-59) dopustiti točnu adresu s portom na kojem se osluškuje aplikacija SchemaPal. 
```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSchemaPalApp",
        builder => builder.WithOrigins("https://localhost:7135")
        .AllowAnyHeader()
        .AllowAnyMethod());
});
```
Ova adresa se nalazi u datoteci **`Properties/launchSettings.json`** aplikacije SchemaPal, pod oznakom  **`https`**.
