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


        public ScriptResult Run<TContext>(TContext context, string contextName, IDbContext db, string code, string language)
        {
            ScriptResult result = new ScriptResult();
            try
            {
                var engine = IoC.Container.ResolveKeyed<ScriptEngine>(language);                
                var scope = engine.CreateScope();
                scope.SetVariable(contextName, context);
                scope.SetVariable("db", db);
                scope.SetVariable("ioc", new IoC.Adaptor());
                scope.SetVariable("log", result.Log);
                engine.Execute(PrepareScript(code, language), scope);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Log.Add(ex.Message);
            }
            return result;
        }



        private string PrepareScript(string script, string language)
        {
            StringBuilder sb = new StringBuilder();
            if (language == "Python")
            {
                sb.Append("import clr\r\n");
                sb.Append("import System\r\n");
                sb.Append("clr.AddReference(\"System.Core\")\r\n");
                sb.Append("clr.ImportExtensions(System.Linq)\r\n");
            }
            sb.Append(script);
            return sb.ToString();

        }
    }
}
