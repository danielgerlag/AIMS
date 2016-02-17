using AIMS.DomainModel.Context.Seed;
using AIMS.DomainModel.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIMS.DomainModel.Context
{
    public class DatabaseInit : CreateDatabaseIfNotExists<DataContext>
    {

        protected override void Seed(DataContext context)
        {
            //var globalArea = new DataArea() { Name = "Global" };
            //context.DataAreas.Add(globalArea);

            AttributeDataType dtText = new AttributeDataType() { Description = "Text" };
            AttributeDataType dtWholeNumber = new AttributeDataType() { Description = "WholeNumber" };
            AttributeDataType dtDecimal = new AttributeDataType() { Description = "Decimal" };
            AttributeDataType dtPercentage = new AttributeDataType() { Description = "Percentage" };
            AttributeDataType dtDate = new AttributeDataType() { Description = "Date" };
            AttributeDataType dtList = new AttributeDataType() { Description = "List" };
            AttributeDataType dtYear = new AttributeDataType() { Description = "Year" };
            AttributeDataType dtBoolean = new AttributeDataType() { Description = "Boolean" };
            AttributeDataType dtCSIOList = new AttributeDataType() { Description = "CSIOList" };

            context.AttributeDataTypes.Add(dtText);
            context.AttributeDataTypes.Add(dtWholeNumber);
            context.AttributeDataTypes.Add(dtDecimal);
            context.AttributeDataTypes.Add(dtPercentage);
            context.AttributeDataTypes.Add(dtDate);
            context.AttributeDataTypes.Add(dtList);
            context.AttributeDataTypes.Add(dtYear);
            context.AttributeDataTypes.Add(dtBoolean);
            context.AttributeDataTypes.Add(dtCSIOList);

            Region britishColumbia = new Region() { Name = "British Columbia" };
            Region alberta = new Region() { Name = "Alberta" };
            context.Regions.Add(britishColumbia);
            context.Regions.Add(alberta);

            context.AgentTypes.Add(new AgentType() { Name = "Broker" });
            context.AgentTypes.Add(new AgentType() { Name = "Underwriter" });

            context.CoverageTypes.Add(new CoverageType() { Name = "Accident Benefits", Code = "AB", Region = britishColumbia });
            context.CoverageTypes.Add(new CoverageType() { Name = "Accident Benefits (Occ. Driver)", Code = "ABOD", Region = britishColumbia });
            context.CoverageTypes.Add(new CoverageType() { Name = "Physical Damage to Insured Vehicle - Comprehensive", Code = "CMP", Region = britishColumbia });
            context.CoverageTypes.Add(new CoverageType() { Name = "Physical Damage to Insured Vehicle - Collision or Upset", Code = "COL", Region = britishColumbia });


            context.JournalTxnClasses.Add(new JournalTxnClass() { Description = "Defined Amount", IsDailyCalc = false, IsDefinedAmount = true, IsPercentage = false, OfContextParameter = false, OfCoveragePremium = false, OfLedgerAccount = false });
            context.JournalTxnClasses.Add(new JournalTxnClass() { Description = "% of Coverage Premium", IsDailyCalc = false, IsDefinedAmount = false, IsPercentage = true, OfContextParameter = false, OfCoveragePremium = true, OfLedgerAccount = false });
            context.JournalTxnClasses.Add(new JournalTxnClass() { Description = "% of Ledger Account", IsDailyCalc = false, IsDefinedAmount = false, IsPercentage = true, OfContextParameter = false, OfCoveragePremium = false, OfLedgerAccount = true });
            context.JournalTxnClasses.Add(new JournalTxnClass() { Description = "% of Contextual Parameter", IsDailyCalc = false, IsDefinedAmount = false, IsPercentage = true, OfContextParameter = true, OfCoveragePremium = false, OfLedgerAccount = false });

            context.JournalTypes.Add(new JournalType() { Name = "General", IsGeneral = true });
            context.JournalTypes.Add(new JournalType() { Name = "Invoice", IsInvoice = true });
            context.JournalTypes.Add(new JournalType() { Name = "Payment", IsPayment = true });


            context.LedgerAccountTypes.Add(new LedgerAccountType() { Name = "Current Asset", IsAsset = true, IsCurrent = true, CreditPositive = false });
            context.LedgerAccountTypes.Add(new LedgerAccountType() { Name = "Non-Current Asset", IsAsset = true, IsCurrent = false, CreditPositive = false });
            context.LedgerAccountTypes.Add(new LedgerAccountType() { Name = "Current Liability", IsLiability = true, IsCurrent = true, CreditPositive = true });
            context.LedgerAccountTypes.Add(new LedgerAccountType() { Name = "Non-Current Liability", IsLiability = true, IsCurrent = false, CreditPositive = true });
            context.LedgerAccountTypes.Add(new LedgerAccountType() { Name = "Expense", IsExpense = true, IsCurrent = true, CreditPositive = false });
            context.LedgerAccountTypes.Add(new LedgerAccountType() { Name = "Income", IsIncome = true, IsCurrent = true, CreditPositive = true });
            context.LedgerAccountTypes.Add(new LedgerAccountType() { Name = "Debtors", IsAsset = true, IsDebtor = true, IsCurrent = true, CreditPositive = false });
            context.LedgerAccountTypes.Add(new LedgerAccountType() { Name = "Creditors", IsLiability = true, IsCredior = true, IsCurrent = true, CreditPositive = true });
            context.LedgerAccountTypes.Add(new LedgerAccountType() { Name = "Equity", IsEquity = true, CreditPositive = true });


            context.PublicRequirements.Add(new PublicRequirement() { Name = "Agent", IsAgent = true });
            context.PublicRequirements.Add(new PublicRequirement() { Name = "Policy Holder", IsPolicyHolder = true });
            context.PublicRequirements.Add(new PublicRequirement() { Name = "Service Provider", IsServiceProvider = true });

            context.SequenceNumbers.Add(new SequenceNumber() { Name = "General Number", NextValue = 1, FormatMask = "G{0:00000}" });

            context.ServiceProviderTypes.Add(new ServiceProviderType() { Name = "Brokerage" });
            context.ServiceProviderTypes.Add(new ServiceProviderType() { Name = "Insurer" });
            context.ServiceProviderTypes.Add(new ServiceProviderType() { Name = "MGA" });
            context.ServiceProviderTypes.Add(new ServiceProviderType() { Name = "Reinsurer" });

            context.TransactionTriggerFrequencies.Add(new TransactionTriggerFrequency() { Name = "Once off", Code = "O" });
            context.TransactionTriggerFrequencies.Add(new TransactionTriggerFrequency() { Name = "Daily", Code = "D" });
            context.TransactionTriggerFrequencies.Add(new TransactionTriggerFrequency() { Name = "Bi-weekly", Code = "BW" });
            context.TransactionTriggerFrequencies.Add(new TransactionTriggerFrequency() { Name = "Monthly", Code = "M" });
            context.TransactionTriggerFrequencies.Add(new TransactionTriggerFrequency() { Name = "Annually", Code = "A" });

            context.TransactionTriggerStatuses.Add(new TransactionTriggerStatus() { Name = "Pending Approval", Code = "P" });
            context.TransactionTriggerStatuses.Add(new TransactionTriggerStatus() { Name = "Approved", Code = "A" });
            context.TransactionTriggerStatuses.Add(new TransactionTriggerStatus() { Name = "Suspended", Code = "S" });


            context.BusinessLines.Add(new BusinessLine() { Name = "Private Passenger Auto", CSIOCode = "AUTO" });
            context.BusinessLines.Add(new BusinessLine() { Name = "Commercial Autos Fleets Trucks", CSIOCode = "CAUTO" });
            context.BusinessLines.Add(new BusinessLine() { Name = "Miscellaneous Commercial Lines", CSIOCode = "COMM" });
            context.BusinessLines.Add(new BusinessLine() { Name = "FarmOwners Fire Liability", CSIOCode = "FARM" });
            context.BusinessLines.Add(new BusinessLine() { Name = "Habitational", CSIOCode = "HABL" });
            context.BusinessLines.Add(new BusinessLine() { Name = "Package Policy", CSIOCode = "PACK" });
            context.BusinessLines.Add(new BusinessLine() { Name = "Miscellaneous Personal Lines", CSIOCode = "PERS" });
            context.BusinessLines.Add(new BusinessLine() { Name = "Personal Lines Umbrella", CSIOCode = "PUMB" });


            /////////////////////////////////

            //context.AttributeLookupLists

            InsurableItemClass standardAuto = new InsurableItemClass() { Name = "Standard Auto" };
            InsurableItemClassAttributeGroup standardAutoGroup = new InsurableItemClassAttributeGroup() { Name = "General", Prompt = "General" };
            standardAuto.Groups.Add(standardAutoGroup);
            standardAutoGroup.Attributes.Add(new InsurableItemClassAttribute() { Name = "Make", AttributeDataType = dtList });
            standardAutoGroup.Attributes.Add(new InsurableItemClassAttribute() { Name = "VIN", AttributeDataType = dtText, Key = true });
            context.InsurableItemClasses.Add(standardAuto);


            //LoadSeedCodes(context);

            base.Seed(context);
        }


        private void LoadSeedCodes(DataContext context)
        {
            //var asm = System.Reflection.Assembly.GetExecutingAssembly();
            //var strm1 = asm.GetManifestResourceStream(@"AIMS.DomainModel.compiler.resources.seed.AL3Codes.json");            
            //System.IO.StreamReader sr = new System.IO.StreamReader(strm1);
            string sData = ASCIIEncoding.ASCII.GetString(Properties.Resources.AL3Codes); //sr.ReadToEnd();
            SeedCode[] data = Newtonsoft.Json.JsonConvert.DeserializeObject<SeedCode[]>(sData);

            foreach (SeedCode rawCode in data)
            {
                var list = context.AttributeLookupLists.Local.FirstOrDefault(x => x.CSIOCode == rawCode.ListCode);

                if (list == null)
                {
                    string listName = rawCode.ListName;
                    if (String.IsNullOrEmpty(listName))
                        listName = rawCode.ListCode;
                    list = new AttributeLookupList() { CSIOCode = rawCode.ListCode, Name = listName };
                    context.AttributeLookupLists.Add(list);
                }
                
                var subList = context.AttributeLookupSubLists.Local.FirstOrDefault(x => x.Name == rawCode.SubListName && x.AttributeLookupList == list && (rawCode.SubListName.Trim() != string.Empty));
                
                if ((rawCode.SubListName.Trim() != string.Empty) && (subList == null))
                {
                    subList = new AttributeLookupSubList() { AttributeLookupList = list, Name = rawCode.SubListName };
                    context.AttributeLookupSubLists.Add(subList);
                }

                var item = new AttributeLookupItem()
                {
                    AttributeLookupList = list,
                    AttributeLookupSubList = subList,
                    CSIOCode = rawCode.CodeValue,
                    Description = rawCode.CodeDescription.Substring(0, Math.Min(200, rawCode.CodeDescription.Length))
                };

                context.AttributeLookupItems.Add(item);             
            }

        }
    }
}
