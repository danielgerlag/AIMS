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


            context.BusinessLines.Add(new BusinessLine() { Name = "Private Passenger Auto", CSIOCode = "AUTO" });
            context.BusinessLines.Add(new BusinessLine() { Name = "Commercial Autos Fleets Trucks", CSIOCode = "CAUTO" });
            context.BusinessLines.Add(new BusinessLine() { Name = "Miscellaneous Commercial Lines", CSIOCode = "COMM" });
            context.BusinessLines.Add(new BusinessLine() { Name = "FarmOwners Fire Liability", CSIOCode = "FARM" });
            context.BusinessLines.Add(new BusinessLine() { Name = "Habitational", CSIOCode = "HABL" });
            context.BusinessLines.Add(new BusinessLine() { Name = "Package Policy", CSIOCode = "PACK" });
            context.BusinessLines.Add(new BusinessLine() { Name = "Miscellaneous Personal Lines", CSIOCode = "PERS" });
            context.BusinessLines.Add(new BusinessLine() { Name = "Personal Lines Umbrella", CSIOCode = "PUMB" });


            //context.AttributeLookupLists

            InsurableItemClass standardAuto = new InsurableItemClass() { Name = "Standard Auto" };
            standardAuto.Attributes.Add(new InsurableItemClassAttribute() { Name = "Make", AttributeDataType = dtList });
            standardAuto.Attributes.Add(new InsurableItemClassAttribute() { Name = "VIN", AttributeDataType = dtText });
            context.InsurableItemClasses.Add(standardAuto);


            LoadSeedCodes(context);

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
