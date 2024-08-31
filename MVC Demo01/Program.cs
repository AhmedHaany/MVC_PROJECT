using Microsoft.AspNetCore.Routing.Constraints;

namespace MVC_Demo01
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            

            //builder.Services.AddControllers(); //API
            builder.Services.AddControllersWithViews(); //MVC
                                                        //builder.Services.AddRazorPages(); //Razor
                                                        //builder.Services.AddMvc (); //

            var app = builder.Build();

            //app.MapGet("/Products/Get/2", () => "Hello World!");
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endPoints =>
                endPoints.MapControllerRoute(
                    name: "Default",
                    pattern: "{Controller=Home}/{Action=Index}/{id?}"  
                    //constraints: new {id = new IntRouteConstraint  }
                   // defaults : new {Controller = "Home" , Action ="Index" }
                    )
            );
            
            app.Run();
        }
    }
}
