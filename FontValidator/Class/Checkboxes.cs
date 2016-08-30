using System.Collections.Generic;

namespace FontValidator
{
    public class Checkboxes
    {
        public class TabCheckboxes
        {
            public Dictionary<string, bool> m_tab_checkbox =
                new Dictionary<string, bool>();
        };

        public Checkboxes(List<string> i_tab_names)
        {
            foreach (var j in i_tab_names)
            {
                var tab = new TabCheckboxes();
                m_tab_checkboxes.Add(j, tab);
            }
        }

        public string current_selected_tab_name = "";

        public Dictionary<string, bool> m_layout_checkboxes =
            new Dictionary<string, bool>();

        public Dictionary<string, TabCheckboxes> m_tab_checkboxes =
            new Dictionary<string, TabCheckboxes>();
    };
}
