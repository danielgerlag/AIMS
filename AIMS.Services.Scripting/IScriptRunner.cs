using System.Collections.Generic;
using AIMS.DomainModel.Interface;

namespace AIMS.Services.Scripting
{
    public interface IScriptRunner
    {
        ScriptResult Run<TContext>(TContext context, string contextName, IDbContext db, string code, string language);
                
        ScriptResult Run(Dictionary<string, object> parameters, IDbContext db, string code, string language);

        object ResolveExpression<TContext>(TContext context, string contextName, IDbContext db, string code, string language);

        TResult ResolveExpression<TResult>(Dictionary<string, object> parameters, IDbContext db, string code, string language);
    }

    
}