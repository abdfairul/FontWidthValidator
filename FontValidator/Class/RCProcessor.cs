using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using StringDictionary = System.Collections.Generic.Dictionary<string, string>;

namespace FontValidator
{
    class RCProcessor
    {
        // <id<languange,value>>
        public ConcurrentDictionary<string, StringDictionary> id_stringdictionary =
            new ConcurrentDictionary<string, StringDictionary>();

        public ConcurrentDictionary<string, string> id_numdictionary =
            new ConcurrentDictionary<string, string>();

        public Regex num_rgx, text_rgx;
        private StringDictionary text_files;
        private string num_file;

        public RCProcessor(string i_num_file, StringDictionary i_text_files, Regex r_num, Regex r_text)
        {
            text_files = i_text_files;
            num_file = i_num_file;
            num_rgx = r_num;
            text_rgx = r_text;
        }

        public void process(ProgressForm pForm)
        {
            // label + content
            var alltextfiles = new Dictionary<string, List<string>>();

            foreach (KeyValuePair<string, string> file in text_files)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(file.Value))
                    {
                        string oneline = "";
                        var onefile = new List<string>();
                        while ((oneline = sr.ReadLine()) != null)
                        {
                            onefile.Add(oneline);
                        }
                        alltextfiles.Add(file.Key, onefile);
                    }
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message);
                }

            }

            int counter = 0;
            Parallel.ForEach(alltextfiles, (onefile) =>
            {
                Parallel.For(0, onefile.Value.Count, i =>
                {
                    
                    var one_line_text = onefile.Value[i];
                    Match m = text_rgx.Match(one_line_text);
                    if (m.Success)
                    {
                        // get id, then value from each files. 
                        var id = m.Groups[1].Value;
                        var value = m.Groups[2].Value;
                        var label_value_dict = new StringDictionary();
                        label_value_dict.Add(onefile.Key, value);
                        id_stringdictionary.AddOrUpdate(id, label_value_dict, (k, v) =>
                        {
                            var new_value = new StringDictionary();
                            new_value = v;
                            new_value.Add(onefile.Key, value);

                            return new_value;
                        });
                    }
                });

                Interlocked.Increment(ref counter);
                pForm.SetProgress((int)(30 + ((double)counter * 25 / alltextfiles.Count)), "Assigning String values... ");
            });


            // process num file
            var onenumfile = new List<string>();

            try
            {
                using (StreamReader sr = new StreamReader(num_file))
                {
                    string oneline = "";
                    while ((oneline = sr.ReadLine()) != null)
                    {
                        onenumfile.Add(oneline);
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }

            counter = 0;

            Parallel.For(0, onenumfile.Count, i =>
            {
                var text = onenumfile.ElementAt(i);
                Match m = num_rgx.Match(text);

                while (m.Success)
                {
                    id_numdictionary.TryAdd(m.Groups[1].Value, m.Groups[2].Value);
                    m = m.NextMatch();
                }

                Interlocked.Increment(ref counter);

                if (counter%3000 == 0)
                {
                    var percent = (double) counter*35/onenumfile.Count;
                    pForm.SetProgress((int)(55 + percent), "Assigning Num values... ");
                }
            });
        }
    }
}
