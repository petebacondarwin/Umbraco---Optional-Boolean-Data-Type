using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using umbraco.interfaces;
using System.Web.UI.WebControls;
using umbraco.BusinessLogic;
using System.Web;
using umbraco.cms.businesslogic.datatype;
using umbraco.DataLayer;

namespace IMGroup.Web.Umbraco.DataTypes.OptionalBoolean {
    class OptionalBooleanConfigurationEditor : CompositeControl, IDataPrevalue {
        OptionalBooleanConfiguration config;
        TextBox nullLabel = new TextBox { ID = "NullLabel" };
        TextBox falseLabel = new TextBox { ID = "FalseLabel" };
        TextBox trueLabel = new TextBox { ID = "TrueLabel" };

        public OptionalBooleanConfigurationEditor(OptionalBooleanConfiguration config) : base() {
            this.config = config;
        }

        public System.Web.UI.Control Editor { get { return this; } }

        protected override void Render(System.Web.UI.HtmlTextWriter writer) {
            writer.WriteLine("<table>");
            
            writer.WriteLine("<tr><th>Null Label:</th><td>");
            this.nullLabel.RenderControl(writer);
            writer.Write("</td></tr>");

            writer.WriteLine("<tr><th>False Label:</th><td>");
            this.falseLabel.RenderControl(writer);
            writer.Write("</td></tr>");

            writer.WriteLine("<tr><th>True Label:</th><td>");
            this.trueLabel.RenderControl(writer);
            writer.Write("</td></tr>");

            writer.Write("</table>");
        }

        public void Save() {
            config.NullLabel = nullLabel.Text;
            config.FalseLabel = falseLabel.Text;
            config.TrueLabel = trueLabel.Text;
            config.Save();
        }

        protected override void CreateChildControls() {
            Controls.Clear();
            Controls.Add(nullLabel);
            Controls.Add(falseLabel);
            Controls.Add(trueLabel);
        }

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            config.Load();
            if (!Page.IsPostBack) {
                nullLabel.Text = config.NullLabel;
                falseLabel.Text = config.FalseLabel;
                trueLabel.Text = config.TrueLabel;
            }
        }
    }
}
