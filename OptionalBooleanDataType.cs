using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using umbraco.cms.businesslogic.datatype;
using umbraco.interfaces;
using umbraco.BusinessLogic;

namespace IMGroup.Web.Umbraco.DataTypes.OptionalBoolean {
    public class OptionalBooleanDataType : BaseDataType, IDataType {
        IData data;
        OptionalBooleanConfiguration config;
        OptionalBooleanDataEditor dataEditor;
        OptionalBooleanConfigurationEditor configEditor;

        public OptionalBooleanDataType()
            : base() {
            data = new DefaultData(this);
            config = new OptionalBooleanConfiguration(this);
            dataEditor = new OptionalBooleanDataEditor(this);
            configEditor = new OptionalBooleanConfigurationEditor(config);
        }
        
        public override string DataTypeName { get { return "Optional Boolean"; } }
        public override Guid Id { get { return new Guid("05a62d45-994b-4d29-863f-d71f7069be6f"); } }
        public override IData Data { get { return data; } }
        public override IDataEditor DataEditor { get { return dataEditor; } }
        public override IDataPrevalue PrevalueEditor { get { return configEditor; } }
        public OptionalBooleanConfiguration Config { get { return config; } }
    }
}
