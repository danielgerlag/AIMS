using AIMS.DomainModel.Context;
using AIMS.DomainModel.Entities;
using AIMS.Services.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Services
{
    public class PolicyRater : IPolicyRater
    {        
        IScriptRunner _scriptEngine;

        public PolicyRater(IScriptRunner scriptEngine)
        {
            _scriptEngine = scriptEngine;
        }

        public bool Rate(Policy policy, IDataContext db)
        {
            try
            {
                //var policy = _db.Policies.Find(policyID);
                var profile = GetEffectiveProfile(policy);

                if (profile == null)
                    return false;

                switch (profile.Engine)
                {
                    case "InRule":
                        throw new NotImplementedException();
                    case "Script":
                        var scriptResult = _scriptEngine.Run<Policy>(policy, "policy", db, profile.Script, profile.ScriptLanguage);
                        //if (scriptResult.Success)
                        //    _db.SaveChanges();
                        return scriptResult.Success;
                    default:
                        return false;
                }
            }
            catch (Exception ex)
            {
                //todo: logs / return msg
                return false;
            }
            
        }

        private RatingProfile GetEffectiveProfile(Policy policy)
        {
            DateTime effectiveDate = (policy.RatesDate ?? DateTime.Now);
            var rating = policy.PolicySubType.RatingProfiles
                .Where(x => x.EffectiveDate <= effectiveDate)
                .OrderByDescending(x => x.EffectiveDate)
                .FirstOrDefault();

            if (rating == null)
                return null;

            return rating.RatingProfile;
        }
    }
}

            