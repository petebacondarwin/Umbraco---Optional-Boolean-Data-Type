using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using umbraco.editorControls.userControlGrapper;
using umbraco.interfaces;

namespace IMGroup.Web.Umbraco.DataTypes.OptionalBoolean {
    public class OptionalBooleanDataEditor : RadioButtonList, IDataEditor {
        OptionalBooleanDataType dataType;
        OptionalBooleanConfiguration config;
        IData data;

        public OptionalBooleanDataEditor(OptionalBooleanDataType dataType) {
            this.dataType = dataType;
            this.config = dataType.Config;
            this.data = dataType.Data;
        }

        protected override void OnInit(EventArgs e) {
            base.OnInit(e);
            config.Load();
            Items.Add(new ListItem(config.NullLabel, "null"));
            Items.Add(new ListItem(config.FalseLabel, "false"));
            Items.Add(new ListItem(config.TrueLabel, "true"));
            if ((data != null) && (data.Value != null) && data.Value.ToString().Trim() != "") {
                SelectedValue = data.Value.ToString();
            } else {
                SelectedValue = "null";
            }
        }

        #region IDataEditor Members

        public Control Editor {
            get { return this; }
        }

        public void Save() {
            if (SelectedValue == "null") {
                data.Value = null;
            } else {
                data.Value = SelectedValue;
            }
        }

        public bool ShowLabel {
            get { return true; }
        }

        public bool TreatAsRichTextEditor {
            get { return false; }
        }

        #endregion
    }
}
