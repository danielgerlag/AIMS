using AIMS.DomainModel.Context;
using AIMS.DomainModel.Entities;
using AIMS.DomainModel.Models;
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

        public RateResult Rate(Policy policy, IDataContext db)
        {
            RateResult result = new RateResult();
            result.Success = false;
            try
            {
                //var policy = _db.Policies.Find(policyID);
                var profile = GetEffectiveProfile(policy);

                if (profile == null)
                {
                    result.Message = "No rating profile";
                    return result;
                }

                switch (profile.Engine)
                {
                    case "InRule":
                        throw new NotImplementedException();
                    case "Script":
                        var scriptResult = _scriptEngine.Run<Policy>(policy, "policy", db, profile.Script, profile.ScriptLanguage);                        
                        result.Success = scriptResult.Success;
                        result.Log.AddRange(scriptResult.Log);
                        break;
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Log.Add(ex.Message);                
            }

            return result;   
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

            