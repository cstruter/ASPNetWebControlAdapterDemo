using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls.Adapters;
using System.Web.UI.WebControls;

namespace CSTruter
{
    public class DropDownListAdapter : WebControlAdapter
    {
        private Object _ViewState;

        protected override void OnLoad(EventArgs e)
        {
            if (Page.IsPostBack)
            {
                if (_ViewState != null)
                {
                    Object[] groups = (Object[])_ViewState;
                    DropDownList dropDownList = (DropDownList)Control;
                    // Add saved optgroups to ListItems
                    for (Int32 i = 0; i < groups.Length; i++)
                    {
                        if (groups[i] != null)
                        {
                            dropDownList.Items[i].Attributes["Group"] = groups[i].ToString();
                        }
                    }
                }
            }
            base.OnLoad(e);
        }

        protected override void LoadAdapterViewState(object state)
        {
            // Retrieve existing state
            _ViewState = state;
        }

        protected override object SaveAdapterViewState()
        {
            DropDownList dropDownList = (DropDownList)Control;
            Int32 count = dropDownList.Items.Count;
            Object[] values = new Object[count];

            // Retrieve Optgrouping from ListItem 
            for (int i = 0; i < count; i++)
            {
                values[i] = dropDownList.Items[i].Attributes["Group"];
            }
            return values;
        }

        protected override void RenderContents(System.Web.UI.HtmlTextWriter writer)
        {
            // The current control being "adaptered" is available within context from the Control property
            DropDownList dropDownList = (DropDownList)Control;
            ListItemCollection items = dropDownList.Items;

            // Retrieve Optgrouping using LinQ
            var groups = (from p in items.OfType<ListItem>()
                          group p by p.Attributes["Group"] into g
                          select new { Label = g.Key, Items = g.ToList<ListItem>() });

            foreach (var group in groups)
            {
                if (!String.IsNullOrEmpty(group.Label))
                {
                    writer.WriteBeginTag("optgroup");
                    writer.WriteAttribute("label", group.Label);
                    writer.Write(">");
                }

                int count = group.Items.Count();
                if (count > 0)
                {
                    bool flag = false;
                    for (int i = 0; i < count; i++)
                    {
                        ListItem item = group.Items[i];

                        writer.WriteBeginTag("option");
                        if (item.Selected)
                        {
                            if (flag)
                            {
                                throw new HttpException("Multiple selected items not allowed");
                            }
                            flag = true;

                            writer.WriteAttribute("selected", "selected");
                        }

                        if (!item.Enabled)
                        {
                            writer.WriteAttribute("disabled", "true");
                        }

                        writer.WriteAttribute("value", item.Value, true);

                        if (this.Page != null)
                        {
                            this.Page.ClientScript.RegisterForEventValidation(dropDownList.UniqueID, item.Value);
                        }
                        writer.Write('>');
                        HttpUtility.HtmlEncode(item.Text, writer);
                        writer.WriteEndTag("option");
                        writer.WriteLine();
                    }
                }
                if (!String.IsNullOrEmpty(group.Label))
                {
                    writer.WriteEndTag("optgroup");
                }
            }
        }
    }
}