Pomoćna ASP.NET Web API aplikacija za autentifikaciju i autorizaciju korisnika te upravljanje podacima u sklopu klijentske aplikacije SchemaPal

Aplikacija "defaultno" koristi in-memory upravljanje podacima, to jest, podaci o korisniku i njegovim shemama se spremaju u memoriju aplikacije i samim time žive dok živi i aplikacija. 
Moguće je i konfigurirati aplikaciju da koristi bazu podataka. Za korištenje lokalne baze podataka, potrebno je kreirati MSS bazu podataka koristeći skriptu u datoteci SqlScripts/CreateSchemaPalDb.sql.

Zatim je u datoteci Program.cs potrebno zakomentirati linije 41-42:
// builder.Services.AddSingleton<IUserRepository, InMemoryUserRepository>();
// builder.Services.AddSingleton<IDatabaseSchemaRepository, InMemoryDatabaseSchemaRepository>();

te otkomentirati linije 46-49:
builder.Services.AddDbContext<SchemaPalContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SchemaPalDb")));
builder.Services.AddScoped<IUserRepository, SqlUserRepository>();
builder.Services.AddScoped<IDatabaseSchemaRepository, SqlDatabaseSchemaRepository>();

Pri pokretanju, aplikacija dodaje defaultnog korisnika s korisničkim imenom "demo.korisnik" i lozinkom "demo123!", ako takav korisnik već ne postoji.
U slučaju da želje za uklanjanjem dodavanja defaultnog korisnika, potrebno je zakomentirati liniju 51 u datoteci Program.cs:
// builder.Services.AddHostedService<DataSeederService>();
