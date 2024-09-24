using SeendMail.Components;
using SeendMail.IServices;
using SeendMail.Services;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.
builder.Services.AddRazorComponents()
	.AddInteractiveServerComponents();
builder.Services.AddScoped<ISeenMaillServices, SeenMaillServices>();
builder.Services.AddCors(options =>
{
	options.AddPolicy(name: MyAllowSpecificOrigins,
					  policy =>
					  {
						  policy.AllowAnyOrigin();
					  });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error", createScopeForErrors: true);
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}


app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();
app.UseCors(MyAllowSpecificOrigins);
app.MapRazorComponents<App>()
	.AddInteractiveServerRenderMode();

app.Run();
