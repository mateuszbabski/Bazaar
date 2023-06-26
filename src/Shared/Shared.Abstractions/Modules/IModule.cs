namespace Shared.Abstractions.Modules
{
    public interface IModule
    {
        string Name { get; }
        //IEnumerable<string> Policies => null;
        ////void Register(IServiceCollection services);
        //void Register(IServiceCollection services, IConfiguration configuration);
        //void Use(IApplicationBuilder app);
    }
}
