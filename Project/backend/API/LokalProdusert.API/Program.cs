using Sales.Application;
using Sales.Infrastructure;
using Sales.API;
using Microsoft.OpenApi.Models;
using Quartz;
using LokalProdusert.Shared;
using LokalProdusert.Shared.BackgroundJobs;
using TransportHub.API;
using TransportHub.Application;
using TransportHub.Infrastructure;
using Sales.IntegrationEvents;
using LokalProdusert.Shared.EventBus;
using Payments.API;
using Payments.Application;
using Payments.Infrastructure;
using Users.API;
using Users.Application;
using Users.Infrastructure;
using Administration.API;
using Administration.Application;
using Administration.Infrastructure;



var builder = WebApplication.CreateBuilder(args);
{
    
    builder.Services
        .AddSalesApplication()
        .AddSalesAPI()
        .AddSalesInfrastructure(builder.Configuration)
        .AddSalesIntegrationEvents()
        .AddShared(builder.Configuration)
        .AddTransportHubAPI()
        .AddTransportHubApplication()
        .AddTransportHubInfrastructure(builder.Configuration)
        .AddPaymentsAPI()
        .AddPaymentsApplication()
        .AddPaymentsInfrastructure(builder.Configuration)
        .AddUsersAPI()
        .AddUsersApplication()
        .AddUsersInfrastructure(builder.Configuration)
        .AddAdministrationAPI()
        .AddAdministrationApplication()
        .AddAdminstrationInfrastructure(builder.Configuration);
        
    builder.Services.AddControllers();


    // Register the Swagger generator, defining one or more Swagger documents
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    });

}

builder.Services.AddQuartz(configure =>{
    var jobKey = new JobKey(nameof(ProcessOutboxMessagesJob));
    configure
    .AddJob<ProcessOutboxMessagesJob>(jobKey)
    .AddTrigger(
        trigger => trigger.ForJob(jobKey)
            .WithSimpleSchedule(schedule => 
            schedule.WithInterval(TimeSpan.FromSeconds(10)).RepeatForever()
    ));

    var jobKey2 = new JobKey(nameof(ProcessIntegrationEventsJob));
    configure
    .AddJob<ProcessIntegrationEventsJob>(jobKey2)
    .AddTrigger(
        trigger => trigger.ForJob(jobKey2)
            .WithSimpleSchedule(schedule => 
            schedule.WithInterval(TimeSpan.FromSeconds(20)).RepeatForever()
    ));    
});


builder.Services.AddQuartzHostedService();

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        // Enable middleware to serve generated Swagger as a JSON endpoint.
        app.UseSwagger();
        // Enable middleware to serve swagger-ui
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
    }

    app.UseExceptionHandler("/error");

    
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.UseSalesAPI();
    app.UseTransportHubAPI();
    app.UsePaymentsAPI();
    app.UseUsersAPI();
    app.UseAdministrationAPI();
    app.Run();
}


