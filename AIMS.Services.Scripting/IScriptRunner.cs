using AIMS.DomainModel.Interface;

namespace AIMS.Services.Scripting
{
    public interface IScriptRunner
    {
        ScriptResult Run<TContext>(TContext context, string contextName, IDbContext db, string code, string language);
    }
}