using Application;
using Application.Posts.Commands;
using DataAccess;
using DataAccess.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MinimalApi.Abstractions;

namespace MinimalApi.Extensions;

public static class MinimalApiExtensions
{
    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        var cs = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<SocialDbContext>(opt => opt.UseSqlServer(cs));
        builder.Services.AddScoped<IPostRepository, PostRepository>();
        builder.Services.AddMediatR(typeof(CreatePost));
    }

    public static void RegisterEndpointDefinitions(this WebApplication app)
    {
        var endpointDefinitons = typeof(Program).Assembly
                .GetTypes()
                .Where( t => t.IsAssignableTo(typeof(IEndpointDefinition))
                    && !t.IsAbstract && !t.IsInterface)
                .Select(Activator.CreateInstance)
                .Cast<IEndpointDefinition>();
        
        foreach (var endpointDef in endpointDefinitons)
        {
            endpointDef.RegisterEndpoints(app);
        }
    }
}