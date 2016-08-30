using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FontValidator.Class
{
    internal class DRMProcessor
    {
        private string m_drm_file;

        public ConcurrentDictionary<int, string> indexLine =
            new ConcurrentDictionary<int, string>();

        public DRMProcessor(string i_drm_file)
        {
            m_drm_file = i_drm_file;
        }

        public void process()
        {
            var longestString = string.Empty;

            var onefile = new List<string>();
            try
            {
                using (StreamReader sr = new StreamReader(m_drm_file))
                {
                    string oneline = "";

                    while ((oneline = sr.ReadLine()) != null)
                    {
                        onefile.Add(oneline);
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }

            var rgx = new Regex(@"^""([\s\S]*?)"",?$");

            Parallel.For(0, onefile.Count, i =>
            {
                var one_line_text = onefile[i];

                Match m = rgx.Match(one_line_text);

                if (m.Success)
                {
                    // get id, then value from each files. 
                    var text = m.Groups[1].Value;

                    if (!Regex.IsMatch(text, @"---"))
                    {
                        indexLine.TryAdd(i + 1, text);
                    }

                }

            });
        }
    }
}
