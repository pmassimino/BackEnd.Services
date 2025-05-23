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
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Soltec.Suscripcion.Code;
using IAfipWsService = BackEnd.Services.Services.Comun.IAfipWsService;

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
            var corsSettings = this.Configuration.GetSection("Cors");
            var allowedOrigins = corsSettings.GetSection("AllowedOrigins").Get<string[]>();

            services.AddCors(options =>
            {
                options.AddPolicy("AppPolicy",
                    policy => policy.WithOrigins(allowedOrigins)
                                   .AllowAnyHeader()
                                   .AllowAnyMethod());
            });
            // Configura la compresión para el tipo de contenido "application/json"
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
            });
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
            //Almacen
            services.AddTransient<IArticuloService, ArticuloService>();
            services.AddTransient<IFamiliaService, FamiliaService>();
            services.AddTransient<IDepositoService, DepositoService>();
            services.AddTransient<IMarcaService, MarcaService>();
            services.AddTransient<IStockService, StockService>();
            services.AddTransient<IMovStockService, MovStockService>();
            //Global
            services.AddTransient<IUnidadMedidaService, UnidadMedidaService>();
            services.AddTransient<IOrganizacionService, OrganizacionService>();
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
            //Afip
            services.AddTransient<IAfipWsService, AfipWsService>();
            services.AddTransient<ICertificadoDigitalService, CertificadoDigitalService>();

            //Ventas
            services.AddTransient<IModeloAsientoFacturaService, ModeloAsientoFacturaService>();
            services.AddTransient<IFacturaService, FacturaService>();
            services.AddTransient<IConfigFacturaService, ConfigFacturaService>();
            services.AddTransient<IPuntoEmisionService, PuntoEmisionService>();

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

            //mail
            services.AddTransient<IMailServerService, MailServerService>();
            services.AddTransient<IMailService, MailService>();


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
            app.UseCors("AppPolicy");
            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
