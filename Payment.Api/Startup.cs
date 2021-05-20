using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Payment.Api.MiddleWare;
using Payment.Core.Features.Commands;
using Payment.Core.Features.Queries;
using Payment.Core.PipelineBehaviours;
using Payment.Core.Validators;
using Payment.Data;
using Payment.Data.Context;
using System.Reflection;

namespace Payment.Api
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

            var connectionString = Configuration.GetConnectionString("DBConnection");
            var keepAliveConnection = new SqliteConnection(connectionString);
            keepAliveConnection.Open();

            services.AddDbContext<PaymentContext>(options =>
            {
                options.UseSqlite(connectionString);
            });

            services.AddLogging();

            services.AddScoped<IUow, Uow>();
            services.AddScoped<IPaymentContext, PaymentContext>();

            services.AddSingleton<ICommissionMapper, CommissionMapper>();
            services.AddSingleton<ICommissionApplier, VisaCommissionApplier>();
            services.AddSingleton<ICommissionApplier, MasterCommissionApplier>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Payment.Api", Version = "v1" });
            });

            services.AddMediatR(typeof(GetBalancesQuery).GetTypeInfo().Assembly);
            services.AddValidatorsFromAssembly(typeof(UpdateAccountCommandValidator).GetTypeInfo().Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, PaymentContext paymentContext)
        {
            paymentContext.Database.Migrate();
            paymentContext.Database.EnsureCreated();

            app.UsePaymentExceptionMiddleware();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Payment.Api v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
