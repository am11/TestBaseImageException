var builder = WebApplication
    .CreateBuilder(args);


throw new Exception("This wont show up");

var app = builder.Build();

app.Run();
