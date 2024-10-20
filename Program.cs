using C_Boutique.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();
        // Enregistrer le service ClientService
        builder.Services.AddScoped<ClientService>();
        builder.Services.AddScoped<ProduitService>();

        // Enregistrer d'autres services et les contrôleurs
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
