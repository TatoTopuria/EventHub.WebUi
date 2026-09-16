// Minimal static-file host used only to back the F8 Selenium smoke test. Everything dynamic is
// the SPA in wwwroot, which talks to the gateway directly. The host itself is intentionally tiny —
// adding routing, auth, or DI here would invite the smoke test to grow into a parallel
// integration-test stack, which is not the goal.
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
app.MapGet("/healthz", () => Results.Text("ok", "text/plain"));

app.Run();

public partial class Program;
