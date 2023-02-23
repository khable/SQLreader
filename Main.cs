using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace occ.dmr.dataConnector
{
    public class Program
    {
        private static IConfigurationRoot configurations;

        public static void Main(string[] args)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "occ.dmr.json"));
            configurations = builder.Build();
        }

        public static List<DMRTag> ReadDMRTags()
        {
            var returnList = new List<DMRTag>();
            var connectionString = configurations.GetConnectionString("connectionString");
            var dbView = configurations["ConsolidatedTagTable"];

            using var connection = new SqlConnection(connectionString);

            connection.Open();
            using var command = new SqlCommand($"SELECT * FROM {dbView}", connection);
            using var reader = command.ExecuteReader();
            var dt = new DataTable();
            dt.Load(reader);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    returnList.Add(new DMRTag
                    {
                        Name = row["Name"],
                        UDET = row["UDET"],
                        TagList = row["TagList"],
                        GlobalClass = row["GlobalClass"],
                        SourceApplication = row["SourceApplication"],
                        Validated = (bool)row["Validated"],
                        Attributes = new List<DMRGlobalAttribute>
                        {
                            new DMRGlobalAttribute()
                            {
                                Name = row["Name"],
                                UDA = row["UDA"],
                                Value = row["Value"],
                                IsPrimary = row["IsPrimary"],
                                Uom = row["Uom"],
                                Datatype = row["Datatype"]
                            }
                        }
                    });

                }
            }

            return returnList;
        }
    }
}