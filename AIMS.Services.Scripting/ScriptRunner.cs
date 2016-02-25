using AIMS.DomainModel.Interface;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.Services.Scripting
{
    public class ScriptRunner : IScriptRunner
    {


        public ScriptResult Run<TContext>(TContext context, IDbContext db, string code, string language)
        {
            ScriptResult result = new ScriptResult();
            try
            {
                var engine = IoC.Container.ResolveKeyed<ScriptEngine>(language);
                var scope = engine.CreateScope();
                scope.SetVariable("context", context);
                scope.SetVariable("db", db);
                scope.SetVariable("log", result.Log);
                engine.Execute(code, scope);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Log.Add(ex.Message);
            }
            return result;
        }

    }
}
