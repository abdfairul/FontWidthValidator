using System.Collections.Concurrent;
using System.Drawing;

using IDValuePair = System.Collections.Generic.KeyValuePair<string, string>;
using StringDictionary = System.Collections.Generic.Dictionary<string, string>;

namespace FontValidator
{
    public class IDData
    {
        public string filepath;
        public string textbox_id;

        public IDValuePair width;
        public IDValuePair height;
        public IDValuePair fontsize;
        public IDValuePair padding;
        public string multiline;
        public string scrollable;

        public string text;
        public ConcurrentBag<StringResult> text_values = new ConcurrentBag<StringResult>();

        public Font fontface;

        public IDData(IDValuePair i_width, IDValuePair i_height,
            string i_text, IDValuePair i_fontsize,
            IDValuePair i_padding, string i_file, string i_multiline)
        {
            width = i_width;
            height = i_height;
            text = i_text;
            fontsize = i_fontsize;
            padding = i_padding;
            filepath = i_file;
            multiline = i_multiline;
        }

        public IDData()
        {
        }

        public void numfiller(ConcurrentDictionary<string, string> id_num)
        {
            foreach (var values in id_num)
            {
                if (values.Key == width.Key)
                    width = new IDValuePair(values.Key, values.Value);
                if (values.Key == height.Key)
                    height = new IDValuePair(values.Key, values.Value);
                if (values.Key == fontsize.Key)
                    fontsize = new IDValuePair(values.Key, values.Value);
                if (values.Key == padding.Key)
                    padding = new IDValuePair(values.Key, values.Value);
            }
        }

        public void textfiller(ConcurrentDictionary<string, StringDictionary> id_text)
        {
            foreach (var values in id_text)
            {
                if (values.Key == text)
                {
                    foreach (var str_dict in values.Value)
                    {
                        var label = str_dict.Key;
                        var value = str_dict.Value;

                        var item = new StringResult();
                        item.label = label;
                        item.value_string = value;

                        text_values.Add(item);
                    }
                }
                //text = new ID_StringResult_Dictionary(values.Key, values.Value);
            }
        }
    }
}
