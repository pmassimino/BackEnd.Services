using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using BackEnd.Api.Core;
using BackEnd.Services.Comun;
using BackEnd.Services.Core;
using BackEnd.Services.Data;
using BackEnd.Services.Services.Afip;
using BackEnd.Services.Services.Almacen;
using BackEnd.Services.Services.Comun;
using BackEnd.Services.Services.Contable;
using BackEnd.Services.Services.Global;
using BackEnd.Services.Services.Tesoreria;
using BackEnd.Services.Services.Venta;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace BackEnd.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddControllers();
            services.AddCors();
            services.AddCors(o => o.AddPolicy("CorePolicy", builder =>
            {
                builder.AllowAnyMethod();builder.AllowAnyOrigin();builder.AllowAnyHeader();
            }));
            var databaseCn = Configuration.GetConnectionString("DatabaseCN");
            var globalCn = Configuration.GetConnectionString("GlobalCN");
            //services.AddDbContext<GestionDBContext>(c => c.UseSqlServer(databaseCn));
            //services.AddDbContext<GlobalDBContext>(c => c.UseSqlServer(globalCn));
            //services.AddDbContext<GestionDBContext>(c => { c.UseSqlite("Data Source=Gestion.db"); c.EnableSensitiveDataLogging(true); }) ;
            
            services.AddDbContext<GlobalDBContext>(c => c.UseSqlite("Data Source=Global.db"));
            services.AddTransient<UnitOfWorkGlobalDb>();           
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IDataBaseManager, DataBaseManager>();
            services.AddTransient<ISessionService, SessionService>();
            services.AddTransient<IContextFactory, ContextFactory>();
            services.AddTransient<UnitOfWorkGestionDb>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IArticuloService, ArticuloService>();
            services.AddTransient<IFamiliaService, FamiliaService>();
            //Global
            services.AddTransient<IUnidadMedidaService, UnidadMedidaService>();
            services.AddTransient<IEmpresaService, EmpresaService>();
            services.AddTransient<ICondIvaOperacionService, CondIvaOperacionService>();
            services.AddTransient<ITipoDocumentoService, TipoDocumentoService>();
            services.AddTransient<ICondIvaService, CondIvaService>();
            services.AddTransient<ITipoFacturaService, TipoFacturaService>();
            services.AddTransient<IComprobanteService, ComprobanteService>();
            services.AddTransient<IGlobalService, GlobalService>();
            services.AddTransient<ISettingGlobalService, SettingGlobalService>();
            services.AddTransient<ILocalidadService, LocalidadService>();
            services.AddTransient<IProvinciaService, ProvinciaService>();
            services.AddTransient<IMonedaService, MonedaService>();
            services.AddTransient<IPermisoService, PermisoService>();
            services.AddTransient<BackEnd.Services.Services.Global.IRolService, BackEnd.Services.Services.Global.RolService>();
            //Comun
            services.AddTransient<ITipoRolService, TipoRolService>();
            services.AddTransient<ISujetoService, SujetoService>();
            services.AddTransient<ISeccionService, SeccionService>();
            services.AddTransient<IAreaService, AreaService>();
            services.AddTransient<INumeradorDocumentoService, NumeradorDocumentoService>();
            services.AddTransient<ISettingService, SettingService>();
            services.AddTransient<ITransaccionService, TransaccionService>();

            //Ventas
            services.AddTransient<IModeloAsientoFacturaService, ModeloAsientoFacturaService>();
            services.AddTransient<IFacturaService, FacturaService>();
            services.AddTransient<IConfigFacturaService, ConfigFacturaService>();

            //Core
            services.AddTransient<ICoreServices, CoreServices>();
            services.AddTransient<ITransaccionService, TransaccionService>();
            //Contabilidad
            services.AddTransient<ITipoCuentaMayorService, TipoCuentaMayorService>();
            services.AddTransient<IUsoCuentaMayorService, UsoCuentaMayorService>();
            services.AddTransient<IComprobanteMayorService, ComprobanteMayorService>();
            services.AddTransient<ICuentaMayorService, CuentaMayorService>();
            services.AddTransient<IMayorService, MayorService>();
            services.AddTransient<IMovCtaCteService, MovCtaCteService>();
            services.AddTransient<ILibroIvaService, LibroIvaService>();
            services.AddTransient<IContableService, ContableService>();




            //Tesoreria
            services.AddTransient<IConfigReciboService, ConfigReciboService>();
            services.AddTransient<IReciboCtaCteService, ReciboCtaCteService>();
            services.AddTransient<ICarteraValorService, CarteraValorService>();
            //Afip Services
            services.AddTransient<IAFIPHelperService, AFIPHelperService>();



            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["Jwt:Audience"],
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });
            services.AddControllersWithViews()
            .AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                // Ignore circular references
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                // do not use camel case
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                // Set the time format
                //options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                // If the field is a null value, the field will not be returned to the front end
                // options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("CorePolicy");
            app.UseHttpsRedirection();

            app.UseRouting();

            //app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
         
            //app.UseCors(
            //    options => options.SetIsOriginAllowed(x => _ = true).AllowAnyMethod().AllowAnyHeader().AllowCredentials()
            //);

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
