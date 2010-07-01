using System;
using umbraco.BusinessLogic;
using umbraco.DataLayer;
using umbraco.interfaces;
using System.Web;
using umbraco.cms.businesslogic.datatype;

namespace IMGroup.Web.Umbraco.DataTypes.OptionalBoolean {
    public class OptionalBooleanConfiguration {
        OptionalBooleanDataType dataType;

        public string NullLabel { get; set; }
        public string FalseLabel { get; set; }
        public string TrueLabel { get; set; }

        public OptionalBooleanConfiguration(OptionalBooleanDataType dataType) {
            this.dataType = dataType;
            NullLabel = "Inherit";
            FalseLabel = "False";
            TrueLabel = "True";
        }

        public void Load() {
            string configString;
            configString = Application.SqlHelper.ExecuteScalar<string>("select value from cmsDataTypePreValues where datatypenodeid = @datatypenodeid",
                CreateDTDefIdParameter());
            if (String.IsNullOrEmpty(configString)) {
                return;
            }

            string[] items = configString.Split('&');
            foreach (string item in items) {
                string[] parts = item.Split('=');
                if (parts.Length == 2) {
                    string key = HttpUtility.UrlDecode(parts[0]);
                    string value = HttpUtility.UrlDecode(parts[1]);
                    switch (key) {
                        case "nullLabel":
                            NullLabel = value;
                            break;
                        case "falseLabel":
                            FalseLabel = value;
                            break;
                        case "trueLabel":
                            TrueLabel = value;
                            break;
                    }
                }
            }
        }

        public void Save() {
            dataType.DBType = DBTypes.Nvarchar;
            // Encode the configuration for this dataType
            string data = "nullLabel=" + HttpUtility.UrlEncode(NullLabel)
                        + "&falseLabel=" + HttpUtility.UrlEncode(FalseLabel)
                        + "&trueLabel=" + HttpUtility.UrlEncode(TrueLabel);

            IParameter[] SqlParams = new IParameter[] { CreateValueParameter(data), CreateDTDefIdParameter() };
            Application.SqlHelper.ExecuteNonQuery("delete from cmsDataTypePreValues where datatypenodeid = @datatypenodeid", SqlParams);
            Application.SqlHelper.ExecuteNonQuery("insert into cmsDataTypePreValues (datatypenodeid,[value],sortorder,alias) values (@datatypenodeid,@value,0,'')", SqlParams);
        }

        IParameter CreateDTDefIdParameter() {
            return Application.SqlHelper.CreateParameter("@datatypenodeid", dataType.DataTypeDefinitionId);
        }
        IParameter CreateValueParameter(string data) {
            return Application.SqlHelper.CreateParameter("@value", data);
        }
    }
}
