using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using RaptorDB;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections;
using System.Web;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TNCSearch
{
    public partial class TNCSearchMainForm : Form
    {
        public TNCSearchMainForm()
        {
            InitializeComponent();

            //books_to_json(null);

            runModeComboBox.SelectedIndex = 0;
            foreach (string dir_path in Directory.GetDirectories(Directory.GetCurrentDirectory()))
            {
                string dir_name = dir_path.Substring(dir_path.LastIndexOf(@"\") + 1);
                if (Regex.IsMatch(dir_name, @"^books?_"))
                {
                    indexStorageFolderComboBox.Items.Add(dir_name);
                }
                else
                {
                    continue;
                }
            }

            foreach (string file_name in Directory.GetFiles(Directory.GetCurrentDirectory() + @"\\$books_sources"))
            {
                string name = Regex.Match(file_name, @"\\(?<name>[^\\]+)\.zip$").Groups["name"].Value;
                booksDocumentsFolderComboBox.Items.Add(name);
            }

            bs = new Books_ser(this);
            foreach (string book_folder in booksDocumentsFolderComboBox.Items)
            {
                //serialize_book(book_folder);
                //books_to_json(book_folder);
                //
                bs.books_ht.Add(book_folder, deserialize_books(book_folder));
            }

            //webBrowser.Document.Write(); // <!DOCTYPE html>
            //webBrowser.Document.Write("<html dir='rtl'><frameset rows=\"*,*\"><frame id='res'><frameset cols=\"*,*\"><frame id='left'><frame id='right'></frameset></frameset></html>"); // " <html dir='rtl'><head><title></title><style>a.verse_id { font-size: 15px; } tr { background-color: #ffe0b4; } td { font-size: 18px; font-family: David; vertical-align: top; } span.quote { font-family: Times;} font.close_match { color: #ff6400; font-weight: bold; } font.exact_match { color: red; font-weight: bold; }</style></head><body style=\"background-color:'#FFFFD0';\"><h2 style='margin-bottom: 6px;'></h2><div dir='ltr'><span style='color:blue; font-size: 22px;'>@FaithBit </span><span id='d'></span></div><table style='width:100%; border-spacing: 6px 10px;' cellspacing='5' cellpadding='4'><tbody id='t'><col width='50%'><col width='50%'></tbody></table></body></html>"); // <!DOCTYPE html>
            //webBrowser.Refresh();
            //resFrame = webBrowser.Document.Window.Frames["res"].Document;
            //resFrame.Document.Write("<html dir='rtl'><head><title></title><style>a.verse_id { font-size: 15px; } tr { background-color: #ffe0b4; } td { font-size: 18px; font-family: David; vertical-align: top; } span.quote { font-family: Times;} font.close_match { color: #ff6400; font-weight: bold; } font.exact_match { color: red; font-weight: bold; }</style></head><body style=\"background-color:'#FFFFD0';\"><h2 style='margin-bottom: 6px;'></h2><div dir='ltr'><span style='color:blue; font-size: 22px;'>@FaithBit </span><span id='d'></span></div><table style='width:100%; border-spacing: 6px 10px;' cellspacing='5' cellpadding='4'><tbody id='t'><col width='50%'><col width='50%'></tbody></table></body></html>");
            // webBrowser.Document.RightToLeft = true;
        }

        private void TNCSearchMainForm_Load(object sender, EventArgs e)
        {
            indexStorageFolderComboBox.SelectedItem = "books_TNC_heb_ns_shrink.Index";
            booksDocumentsFolderComboBox.SelectedItem = "books_TNC_heb_puncd";
            ActiveControl = searchTextBox;
        }

        Hoot hoot;
        DateTime _indextime;

        private void button2_Click(object sender, EventArgs e)
        {
            if (!check_if_hoot_is_loaded())
            {
                throw new Exception("hOOt is NOT Loaded.");
            }

            MessageBox.Show("Words = " + hoot.WordCount.ToString("#,#") + "\r\nDocuments = " + hoot.DocumentCount.ToString("#,#"));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            hoot.Save();
        }

        [Serializable]
        private class Books_ser
        {
            public TNCSearchMainForm f;
            public Books_ser(TNCSearchMainForm f)
            {
                this.f = f;
            }
#if use_ht
            public Hashtable id_to_i_ht;
            public string[] ordered_files_ar;
#endif
            public Hashtable books_ht = new Hashtable();

            public string get_line(string books_folder, string id, bool do_inc)
            {
#if use_ht
                return ordered_files_ar[(int)id_to_i_ht[id]];
#endif
                if (do_inc)
                {
                    inc_verses_read_n(f);
                }
                int b, c, v;
                get_ids_from_filename(id, out b, out c, out v, -1);
                return ((string[][][])books_ht[books_folder])[b][c][v];
            }
        }

        static int n_verses_searched = 0;
        private static void inc_verses_read_n(TNCSearchMainForm f)
        {
            f.Invoke(new MethodInvoker(delegate
            {
                int n_versesRead = int.Parse(Regex.Match(f.avgVerPerSecTextBox.Text, @"(?<n>[,\d]+)$").Groups["n"].Value.Replace(",", ""));
                int book_n_verses = get_book_n_verses(f.searchTextBox.Text);
                f.versesReadNTextBox.Text = n_verses_searched.ToString("#,##0") + "/" + book_n_verses.ToString("#,##0") + $" ({((float)n_verses_searched / book_n_verses * 100).ToString("0.0")}%)";
                string n_versesRead_str = (n_versesRead + 1).ToString("#,##0");
                f.avgVerPerSecTextBox.Text = ((float)compare_same_book_total_time_timer.Elapsed.TotalMilliseconds * 1000 / n_versesRead).ToString(@"0.0") + " [μs] " + ((long)1000 * n_versesRead / compare_same_book_total_time_timer.Elapsed.TotalMilliseconds).ToString("#,##0") + " [n_cmp/s]. " + n_versesRead_str;
            }));
        }

        private static int get_book_n_verses(string book_n_str)
        {
            string[][] book_ar = ((string[][][])bs.books_ht["books_TNC_heb"])[int.Parse(book_n_str) - 1];
            int n_verses = 0;
            for (int c = 0; c < book_ar.Length; c++)
            {
                n_verses += book_ar[c].Length;
            }
            return n_verses;
        }

        static string file_ids_re = @"(?<b>[\d]+)\-(?<c>[\d]+)\-(?<v>[\d]+)";

        private static string get_file_id_from_filename(string filename)
        {
            return Regex.Match(filename, @"(?<=\-)[\d]+\-[\d]+\-[\d]+").Value;
        }

        void books_to_json(string books_documents_folder_name)
        {
            string json_filename = "$books_sources/$json/" + "books_TNC" + ".json";
            StreamWriter sr = new StreamWriter(json_filename, false);
            string[] files = Directory.GetFiles("$books_sources/" + "books_TNC_eng").OrderBy(p => int.Parse(p.Split('-')[1]) * 100000 + int.Parse(p.Split('-')[2]) * 1000 + int.Parse(p.Split('-')[3].Split('.')[0])).ToArray();
            List<string> ordered_files_lst = files.Cast<string>().OrderBy(p => { Match m = Regex.Match(p, file_ids_re); return int.Parse(m.Groups["b"].Value) * 100000 + int.Parse(m.Groups["c"].Value) * 1000 + int.Parse(m.Groups["v"].Value); }).ToList();
            JArray bible = new JArray();
            for (int i = 0; i < ordered_files_lst.Count; i++)
            {
                string filename = ordered_files_lst[i];
                string line_eng = File.ReadAllText(filename); // .TrimEnd('.')
                string line_heb = File.ReadAllText(filename.Replace("_eng", "_heb"));
                string line_heb_puncd = File.ReadAllText(filename.Replace("_eng\\", "_heb_puncd\\").Replace("_eng-", "_heb-"));
                string line_rom = File.ReadAllText(filename.Replace("_eng", "_rom"));
                int b, c, v;
                get_ids_from_filename(filename, out b, out c, out v, -1);
                string doc_line = (new JObject(
                    new JProperty("b", b),
                    new JProperty("c", c),
                    new JProperty("v", v),
                    new JProperty("eng", line_eng),
                    new JProperty("heb", line_heb),
                    new JProperty("heb_puncd", line_heb_puncd),
                    new JProperty("rom", line_rom)
                )).ToString();
                doc_line = Regex.Replace(doc_line, "[\r\n]+", " ");
                doc_line = Regex.Replace(doc_line, "[\\s]+", " ");
                //if (i > 0)
                //{
                //    sr.Write(", ");
                //}
                sr.WriteLine(doc_line); // (i > 0 ? "\r\n" : "") + 
            }
            sr.Close();
            //string json_str = bible.ToString();
            //File.WriteAllText(filename, json_str);
        }

        void serialize_book(string books_documents_folder_name)
        {
            Hashtable id_to_i_ht = new Hashtable();
            List<string> books_lines_lst = new List<string>();
            string[] files = Directory.GetFiles("$books_sources/" + books_documents_folder_name).OrderBy(p => int.Parse(p.Split('-')[1]) * 100000 + int.Parse(p.Split('-')[2]) * 1000 + int.Parse(p.Split('-')[3].Split('.')[0])).ToArray();
            List<string> ordered_files_lst = files.Cast<string>().OrderBy(p => { Match m = Regex.Match(p, file_ids_re); return int.Parse(m.Groups["b"].Value) * 100000 + int.Parse(m.Groups["c"].Value) * 1000 + int.Parse(m.Groups["v"].Value); }).ToList();
            List<List<List<string>>> ls = new List<List<List<string>>>();
            for (int i = 0; i < ordered_files_lst.Count; i++)
            {
                string filename = ordered_files_lst[i];
                string line = File.ReadAllText(filename);
                //
                int b, c, v;
                get_ids_from_filename(filename, out b, out c, out v, -1);
                //
                if (ls.Count < b + 1)
                {
                    ls.Add(new List<List<string>>());
                }
                if (ls[b].Count < c + 1)
                {
                    ls[b].Add(new List<string>());
                }
                if (ls[b][c].Count < v + 1)
                {
                    ls[b][c].Add(null);
                }

                ls[b][c][v] = line;
                //
                string file_id = get_file_id_from_filename(filename);
                books_lines_lst.Add(line);
                id_to_i_ht.Add(file_id, i);
            }
            //
            string[][][] files_ar = new string[ls.Count][][];
            for (int b = 0; b < ls.Count; b++)
            {
                files_ar[b] = new string[ls[b].Count][];
                for (int c = 0; c < ls[b].Count; c++)
                {
                    files_ar[b][c] = new string[ls[b][c].Count];
                    for (int v = 0; v < ls[b][c].Count; v++)
                    {
                        files_ar[b][c][v] = ls[b][c][v];
                    }
                }
            }
            //
            Books_ser book_ser = new Books_ser(this);
#if use_ht
            book_ser.id_to_i_ht = id_to_i_ht;
            book_ser.ordered_files_ar = books_lines_lst.ToArray();
#endif
            // books_ht.Add(books_documents_folder_name, files_ar);
            //
            FileStream fs = File.Open("serialized\\" + books_documents_folder_name + ".ser", FileMode.Create, FileAccess.ReadWrite);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, files_ar);
            fs.Close();
        }

        private static void get_ids_from_filename(string filename, out int b, out int c, out int v, int delta)
        {
            Match m = Regex.Match(filename, file_ids_re);
            b = int.Parse(m.Groups["b"].Value) + delta;
            c = int.Parse(m.Groups["c"].Value) + delta;
            v = int.Parse(m.Groups["v"].Value) + delta;
        }

        string[][][] deserialize_books(string books_documents_folder_name)
        {
            FileStream fs = File.Open("serialized\\" + books_documents_folder_name + ".ser", FileMode.Open, FileAccess.Read);
            string[][][] res = (string[][][])(new BinaryFormatter()).Deserialize(fs);
            fs.Close();
            return res;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (booksDocumentsFolderComboBox.SelectedItem == null)
            {
                MessageBox.Show("Please first select books_Directory.");
                return;
            }

            switch ((string)runModeComboBox.SelectedItem)
            {
                case "Search":
                    string search_txt = searchTextBox.Text;
                    if (isIndexShrinkedCheckBox.Checked && Regex.IsMatch(searchTextBox.Text, "[וי]"))
                    {
                        search_txt = shrink_txt(search_txt);
                        //MessageBox.Show("The Letters [וי] should not be used when Searching shrinked Index.");
                        //return;
                    }
                    search(search_txt);
                    break;
                case "Compare books":
                    compare_books();
                    break;
                default:
                    throw new Exception();
            }
        }

        const string search_results_filepath = "$output\\search_res.html";

        private void search(string search_str)
        {
            if (!check_if_hoot_is_loaded())
            {
                return;
            }

            //this.webBrowser.Url = new System.Uri("about:blank");
            //webBrowser.Navigate("about:blank");
            //webBrowser.Refresh();
            //Thread.Sleep(1000);
            //webBrowser.DocumentText = "";


            WebBrowser webBrowser = get_webBrowser_in_new_tabPage($"Search: {search_str}");

            HtmlDocument doc = webBrowser.Document; // resFrame
            doc.OpenNew(false);
            //doc.Body.InnerHtml = "";
            doc.Write("<html dir='rtl'><head><title></title><style>a.verse_id { font-size: 15px; } tr { background-color: #ffe0b4; } td { font-size: 18px; font-family: David; vertical-align: top; } span.quote { font-family: Times;} font.close_match { color: #ff6400; font-weight: bold; } font.exact_match { color: red; font-weight: bold; } #total { font-size: 18px; font-family: David; font-weight: bold; margin-top: 8px; } .search_result { font-family: David; font-size: 16px; padding-bottom: 3; } .quote { font-family: Times New Roman; } .ref { color: blue; } .mark { font-weight: bold; background-color: yellow; }</style></head><body style=\"background-color:'#FFFFD0';\"><div><div id='title' dir='rtl' style='padding-bottom: 6;'></div></div><div dir='ltr' style='padding-top: 3;'><span style='color:blue; font-size: 14px;'>@FaithBit </span><span id='d' style='font-size: 11;'></span></div></body></html>");
            // webBrowser.Refresh();
            doc.Title = $"Faithbit - TNC Search results for '{search_str}'";
            (doc.GetElementById("d")).InnerText = DateTime.Now.ToString("dd/MM/yyyy");
            HtmlElement div_title_el = doc.GetElementById("title");
            div_title_el.InnerHtml = "Searching...";

            // this.Cursor = Cursors.WaitCursor;

            listBox1.Items.Clear();
            listBox1.BeginUpdate();

            Invoke(new MethodInvoker(delegate
            {
                outputRichTextBox.Text = "";
            }));

            searchLableStatus.Text = "Searching...";
            // resultsTabControl.SelectTab(2);

            DateTime start_search_time = DateTime.Now;

            string search_str_;
            if (!isIndexShrinkedCheckBox.Checked)
            {
                search_str_ = search_str;
            }
            else
            {
                search_str_ = shrink_txt(search_str);
            }
            //
            //if (exactMatchCheckBox.Checked)
            //{
            //    search_str_ = $"\"{search_str_}\"";
            //}

            if (indexStorageFolderComboBox.Text.Contains("_ns"))
            {
                search_str_ = search_str_.Replace("ם", "מ");
                search_str_ = search_str_.Replace("ן", "נ");
                search_str_ = search_str_.Replace("ף", "פ");
                search_str_ = search_str_.Replace("ץ", "צ");
            }

            IEnumerable<string> res = hoot.FindDocumentFileNames(search_str_);
            string[] res_ar = res.Cast<string>().OrderBy(p => { Match m = Regex.Match(p, @"\-(?<b>[\d]+)\-(?<c>[\d]+)\-(?<v>[\d]+)\."); return int.Parse(m.Groups["b"].Value) * 100000 + int.Parse(m.Groups["c"].Value) * 1000 + int.Parse(m.Groups["v"].Value); }).ToArray();

            int skiped = 0;
            string search_regex = "\\b" + Regex.Replace(search_str, "[\\*\\?\\+]", "[\\w]$0") + "\\b";
            List<string> res_lst = new List<string>();
            string res_str = "";
            int n_total_res = 0;
            foreach (string r in res_ar)
            {
                string filename = r.Substring(r.LastIndexOf(@"\") + 1);
                string line;
                if (useFilesCheckBox.Checked)
                {
                    line = File.ReadAllText(booksDocumentsFolderComboBox.SelectedItem + @"\" + filename);
                    inc_verses_read_n(this);
                }
                else
                {
                    string file_id = get_file_id_from_filename(filename);
                    line = bs.get_line((string)booksDocumentsFolderComboBox.SelectedItem, file_id, false);
                }

                if (isPuncdBooksDocumentsCheckBox.Checked)
                {
                    search_regex = Regex.Replace(search_regex, "[" + puncd_chars_str + "]", "");
                }
                bool is_exact_match = exactMatchCheckBox.Checked;
                if (is_exact_match)
                {
                    string line_ = line;
                    if (isIndexShrinkedCheckBox.Checked)
                    {
                        line_ = shrink_txt(line_);
                    }
                    if (isPuncdBooksDocumentsCheckBox.Checked)
                    {
                        line_ = Regex.Replace(line_, "[" + puncd_chars_str + "]", "");
                    }
                    if (!Regex.IsMatch(line_, search_regex))
                    {
                        skiped++;
                        continue;
                    }
                }
                // string marked_line = mark_line(line, search_str_);
                string marked_line = line.TrimEnd(new char[] { ' ', '.' });
                mark_line(search_str_, is_exact_match, ref marked_line);
                string book_verse_id = filename.Substring(filename.IndexOf("-") + 1).Split('.')[0];
                string book_verse_id_formated = book_verse_id_formated = get_book_verse_id_formated(book_verse_id, ' ');
                int b, c, v;
                get_ids_from_filename(book_verse_id, out b, out c, out v, 0);
                string res_line = "[" + book_verse_id_formated + "] \"" + marked_line + "\".";

                res_str += res_line + "\r\n";
                n_total_res++;

                Invoke(new MethodInvoker(delegate
                {
                    string html_marked_line = Regex.Replace(marked_line, @"\*([\s\S]*?)\*", @"<span class='mark'>$1</span>");
                    HtmlElement div_el = webBrowser.Document.CreateElement("div");
                    div_el.SetAttribute("className", "search_result");
                    div_el.InnerHtml = "<span class='ref'>[" + $"<a class='verse_id' target=\"_blank\" href=\"http://www.faithbit-local.org.il/asp/TNC.aspx?b={b}&c={c}&mark_verse={v}\">" + book_verse_id_formated + "</a>" + "]</span>&nbsp\"<span class='quote'>" + html_marked_line + "</span>\".";
                    div_title_el.Parent.AppendChild(div_el);
                    div_title_el.InnerHtml = $"חיפוש '<b>{search_str}</b>' בתנ\"ך, נמצאו <b>{n_total_res}</b> תוצאות:";
                }));

                //Invoke(new MethodInvoker(delegate
                //{
                //    outputRichTextBox.Text += res_line + "\r\n";
                //    scroll_to_bottom(outputRichTextBox);
                //}));
                //
                //res_lst.Add(res_line);
            }

            if (div_title_el.Parent.Children.Count == 1)
            {
                div_title_el.InnerHtml = $"חיפוש '<b>{search_str}</b>' בתנ\"ך, לא נמצאו תוצאות.";
            }

            DateTime end_search_time = DateTime.Now;
            double search_time = end_search_time.Subtract(start_search_time).TotalSeconds;

            Invoke(new MethodInvoker(delegate
            {
                outputRichTextBox.Text = res_str;
            }));

            //foreach (var d in res_lst)
            //{
            //    listBox1.Items.Add(d.Replace(Index_Storage_Directory_ComboBox.SelectedItem + "-", ""));
            //}
            //listBox1.EndUpdate();

            searchLableStatus.Text = "Search found: " + n_total_res + " items (skiped: " + skiped + ") time: " + search_time + " s";

            File.WriteAllText(search_results_filepath, webBrowser.Document.Body.Parent.OuterHtml, Encoding.GetEncoding(webBrowser.Document.Encoding));

            // this.Cursor = Cursors.Default;
        }

        private WebBrowser get_webBrowser_in_new_tabPage(string tab_name)
        {
            WebBrowser webBrowser = null;
            Invoke(new MethodInvoker(delegate
            {
                webBrowser = new System.Windows.Forms.WebBrowser();
                webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
                webBrowser.Location = new System.Drawing.Point(3, 3);
                webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
                webBrowser.Name = "webBrowser";
                webBrowser.Size = new System.Drawing.Size(551, 158);
                webBrowser.TabIndex = 0;
                webBrowser.Url = new System.Uri("", System.UriKind.Relative);
                webBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(webBrowser_DocumentCompleted);

                TabPage new_tabPage = new System.Windows.Forms.TabPage();
                new_tabPage.SuspendLayout();
                //
                new_tabPage.Controls.Add(webBrowser);
                new_tabPage.Location = new System.Drawing.Point(4, 22);
                // new_tabPage.Name = tab_name;
                new_tabPage.Padding = new System.Windows.Forms.Padding(3);
                new_tabPage.Size = new System.Drawing.Size(557, 164);
                //new_tabPage.TabIndex = 2;
                new_tabPage.Text = tab_name;
                new_tabPage.UseVisualStyleBackColor = true;
                //new_tabPage.BackColor = System.Drawing.Color.LightCoral;
                new_tabPage.Padding = new Padding(0);
                new_tabPage.ResumeLayout(false);

                resultsTabControl.Controls.Add(new_tabPage);

                resultsTabControl.SelectedTab = new_tabPage;
                resultsTabControl.ResumeLayout();
            }));

            return webBrowser;
        }

        private bool check_if_hoot_is_loaded()
        {
            if (hoot == null)
            {
                MessageBox.Show("hOOt not loaded");
                return false;
            }
            return true;
        }

        private void mark_line(string search_str_, bool is_exact_match, ref string marked_line)
        {
            if (is_exact_match)
            {
                string full_match_str;
                marked_line = mark_shrinked_verse(marked_line, search_str_, false, out full_match_str);
            }
            else
            {
                string[] search_str_ar = search_str_.Split(' ');
                foreach (string word in search_str_ar)
                {
                    string full_match_str;
                    marked_line = mark_shrinked_verse(marked_line, word, ((string)(indexStorageFolderComboBox.SelectedItem)) == "books_TNC_heb.Index", out full_match_str);
                }
            }
        }

        private string get_book_verse_id_formated(string book_verse_id, char separator, bool book_name_bold = false)
        {
            bool heb_nums = hebNumCheckBox.Checked;
            string book_verse_id_formated;
            string[] book_verse_id_ar = book_verse_id.Split('-');
            string book_name = TNCSearchMainForm.book[int.Parse(book_verse_id_ar[0]) - 1]; // Form1.book_abrv[int.Parse(book_verse_id_ar[0]) - 1];
            book_verse_id_formated = book_name_bold ? "<b>" + book_name + "</b>" : book_name;
            book_verse_id_formated += separator;
            if (!heb_nums)
            {
                book_verse_id_formated += book_verse_id_ar[1] + separator + book_verse_id_ar[2];
            }
            else
            {
                book_verse_id_formated += heb_nums_ar[int.Parse(book_verse_id_ar[1]) - 1] + separator + heb_nums_ar[int.Parse(book_verse_id_ar[2]) - 1];
            }
            return book_verse_id_formated;
        }

        static List<string> commond_words_lst = new List<string>() { "כי", "על", "ועל", "כל", "בכל", "וכל", "את", "ואת", "אל", "ואל" };
        const string timespan_fmt = @"hh\:mm\:ss";
        static string[] book = { "בראשית", "שמות", "ויקרא", "במדבר", "דברים", "יהושע", "שופטים", "שמואל א", "שמואל ב", "מלכים א", "מלכים ב", "ישעיהו", "ירמיהו", "יחזקאל", "הושע", "יואל", "עמוס", "עובדיה", "יונה", "מיכה", "נחום", "חבקוק", "צפניה", "חגי", "זכריה", "מלאכי", "תהילים", "משלי", "איוב", "דניאל", "עזרא", "נחמיה", "שיר השירים", "רות", "איכה", "קהלת", "אסתר", "דברי הימים א", "דברי הימים ב" };
        static string[] book_abrv = { "בר'", "שמ'", "וי'", "במ'", "דב'", "יהושע", "שופ'", "שמואל א", "שמואל ב", "מלכים א", "מלכים ב", "יש'", "יר'", "יח'", "הושע", "יואל", "עמוס", "עובדיה", "יונה", "מיכה", "נחום", "חבקוק", "צפניה", "חגי", "זכריה", "מלאכי", "תה'", "מש'", "איוב", "דניאל", "עזרא", "נחמיה", @"שה""ש", "רות", "איכה", "קהלת", "אסתר", @"דה""י א", @"דה""י ב" };
        static string[] heb_nums_ar = { "א", "ב", "ג", "ד", "ה", "ו", "ז", "ח", "ט", "י", "יא", "יב", "יג", "יד", "טו", "טז", "יז", "יח", "יט", "כ", "כא", "כב", "כג", "כד", "כה", "כו", "כז", "כח", "כט", "ל", "לא", "לב", "לג", "לד", "לה", "לו", "לז", "לח", "לט", "מ", "מא", "מב", "מג", "מד", "מה", "מו", "מז", "מח", "מט", "נ", "נא", "נב", "נג", "נד", "נה", "נו", "נז", "נח", "נט", "ס", "סא", "סב", "סג", "סד", "סה", "סו", "סז", "סח", "סט", "ע", "עא", "עב", "עג", "עד", "עה", "עו", "עז", "עח", "עט", "פ", "פא", "פב", "פג", "פד", "פה", "פו", "פז", "פח", "פט", "צ", "צא", "צב", "צג", "צד", "צה", "צו", "צז", "צח", "צט", "ק", "קא", "קב", "קג", "קד", "קה", "קו", "קז", "קח", "קט", "קי", "קיא", "קיב", "קיג", "קיד", "קטו", "קטז", "קיז", "קיח", "קיט", "קכ", "קכא", "קכב", "קכג", "קכד", "קכה", "קכו", "קכז", "קכח", "קכט", "קל", "קלא", "קלב", "קלג", "קלד", "קלה", "קלו", "קלז", "קלח", "קלט", "קמ", "קמא", "קמב", "קמג", "קמד", "קמה", "קמו", "קמז", "קמח", "קמט", "קנ", "קנא", "קנב", "קנג", "קנד", "קנה", "קנו", "קנז", "קנח", "קנט", "קס", "קסא", "קסב", "קסג", "קסד", "קסה", "קסו", "קסז", "קסח", "קסט", "קע", "קעא", "קעב", "קעג", "קעד", "קעה", "קעו" };

        private void search_basic_old_unused(string search_str)
        {
            if (!check_if_hoot_is_loaded())
            {
                return;
            }

            searchLableStatus.Text = "Searching...";
            listBox1.Items.Clear();
            listBox1.BeginUpdate();
            groupBox4.Update();

            int n_skiped_results;
            Stopwatch process_result_timer, lcs_timer;
            double search_time_sec;
            List<string[]> res_lst = do_my_search(search_str, 2, out n_skiped_results, out process_result_timer, out lcs_timer, out search_time_sec);

            foreach (string[] match_res in res_lst)
            {
                listBox1.Items.Add(match_res[0] + ": " /* + mark_line(match_res[1], match_res[2]) */);
            }

            listBox1.EndUpdate();
            searchLableStatus.Text = $"Found {listBox1.Items.Count} items, skiped: {n_skiped_results}, time: {search_time_sec} s, process_results_time: {process_result_timer.Elapsed.TotalSeconds} s, lcs_time: {lcs_timer.Elapsed.TotalSeconds} s, split_time: {split_timer.Elapsed.TotalSeconds} s.";
        }

        private void compare_books()
        {
            if (!check_if_hoot_is_loaded())
            {
                return;
            }

            Thread compare_book_thread = new Thread(new ThreadStart(compare_book));
            compare_book_thread.IsBackground = true;
            compare_book_thread.Start();

            searchLableStatus.Text = "Comparing Books...";
            // resultsTabControl.SelectTab(2);
        }

        public static Stopwatch compare_same_book_total_time_timer = new Stopwatch();

        void compare_book()
        {
            if (!Regex.IsMatch(searchTextBox.Text, @"^[\d]+$"))
            {
                MessageBox.Show("the input searchTextBox must be the book number.");
                return;
            }

            WebBrowser webBrowser = null;
            int src_book_id = -1;
            int n_min_words = -1;
            HtmlElement table_el = null;
            List<string> files = null;

            Invoke(new MethodInvoker(delegate
            {
                webBrowser = get_webBrowser_in_new_tabPage("compare_book");
                webBrowser.Document.OpenNew(false);
                webBrowser.Document.Write("<html dir='rtl'><head><title></title><style>a.verse_id { font-size: 15px; } tr { background-color: #ffe0b4; } td { font-size: 18px; font-family: David; vertical-align: top; } span.quote { font-family: Times;} font.close_match { color: #ff6400; font-weight: bold; } font.exact_match { color: red; font-weight: bold; } #total { font-size: 18px; font-family: David; font-weight: bold; margin-top: 8px; }</style></head><body style=\"background-color:'#FFFFD0';\"><h2 style='margin-bottom: 0px;'></h2><div dir='ltr'><span style='color:blue; font-size: 22px;'>@FaithBit </span><span id='d'></span></div><table style='width:100%; border-spacing: 6px 10px;' cellspacing='5' cellpadding='4'><tbody id='t'><col width='50%'><col width='50%'></tbody></table><div id='total' dir='ltr'></div></body></html>");

                src_book_id = int.Parse(searchTextBox.Text);
                n_min_words = int.Parse(minWordsTextBox.Text);
                outputRichTextBox.Text = "";
                // this.Cursor = Cursors.WaitCursor;
                HtmlDocument doc = webBrowser.Document; // resFrame
                doc.GetElementsByTagName("h2")[0].InnerText = $"מציאת חפיפות של {n_min_words} מילים רצופות לפחות בין פסוקי ספר {book[src_book_id - 1]} לשאר ספרי התנ\"ך";
                (doc.GetElementById("d")).InnerText = DateTime.Now.ToString("dd/MM/yyyy");
                table_el = doc.GetElementById("t");

                files = new List<string>();
                string booksDocumentsFolder = (string)booksDocumentsFolderComboBox.SelectedItem;
                string[][][] books_ar = ((string[][][])bs.books_ht[booksDocumentsFolder]);
                for (int b = 0; b < books_ar.Length; b++)
                {
                    string[][] book_ar = books_ar[b];
                    for (int c = 0; c < book_ar.Length; c++)
                    {
                        for (int v = 0; v < book_ar[c].Length; v++)
                        {
                            files.Add($"{booksDocumentsFolder}-{b + 1}-{c + 1}-{v + 1}.txt");
                        }
                    }
                }
            }));


            //
            //
            //Invoke(new MethodInvoker(delegate
            //{
            //    files = Directory.GetFiles((string)booksDocumentsFolderComboBox.SelectedItem).OrderBy(p => int.Parse(p.Split('-')[1]) * 100000 + int.Parse(p.Split('-')[2]) * 1000 + int.Parse(p.Split('-')[3].Split('.')[0])).ToList();
            //}));
            //
            int n_matches = 0;
            Hashtable found_matches_ht = new Hashtable();
            int n_already_found_matches = 0;





            int start_at_file_i = 0;
            //start_at_file_i = files.IndexOf("books_TNC_heb_puncd\\TNC_heb-27-106-1.txt");

            for (int file_n = start_at_file_i; file_n < files.Count; file_n++)
            {
                //if (file_n == files.IndexOf("books_TNC_heb_puncd\\TNC_heb-27-107-1.txt"))
                //{
                //}
                string filename = files[file_n];
                string book1_verse_id = get_book_verse_id_str(filename);
                string[] book1_verse_id_ar = book1_verse_id.Split('-');

                if (book1_verse_id_ar[0] != src_book_id.ToString())
                {
                    continue;
                }
                //
                n_verses_searched++;
                //
                string book1_verse_id_formated = get_book_verse_id_formated(book1_verse_id, ' ', true);

                string verse = null;
                if (useFilesCheckBox.Checked)
                {
                    verse = File.ReadAllText(filename);
                    inc_verses_read_n(this);
                }
                else
                {
                    Invoke(new MethodInvoker(delegate
                    {
                        verse = bs.get_line((string)booksDocumentsFolderComboBox.SelectedItem, book1_verse_id, true);
                    }));
                }

                Invoke(new MethodInvoker(delegate
                {
                    searchLableStatus.Text = "Searching verse: " + book1_verse_id + ", found: " + n_matches + " matches, repeated: " + n_already_found_matches + " matches, run_time: " + compare_same_book_total_time_timer.Elapsed.ToString(timespan_fmt);
                }));

                //
                compare_same_book_total_time_timer.Start();
                //
                int n_skiped_results;
                Stopwatch process_result_timer, lcs_timer;
                double search_time_sec;
                //
                string verse_ = verse;
                if (isIndexShrinkedCheckBox.Checked)
                {
                    verse_ = shrink_txt(verse);
                }
                if (isPuncdBooksDocumentsCheckBox.Checked)
                {
                    verse_ = Regex.Replace(verse_, "[" + puncd_chars_str + "]", "");
                }

                //
                List<string[]> res_lst = do_my_search(verse_, n_min_words, out n_skiped_results, out process_result_timer, out lcs_timer, out search_time_sec, false);
                //
                bool found_again_first_time = false;
                foreach (string[] book2_match_res in res_lst.OrderBy(p => -(long)p[2].Length * 10000000 + int.Parse(p[0].Split('-')[1]) * 100000 + int.Parse(p[0].Split('-')[2]) * 1000 + int.Parse(p[0].Split('-')[3])))
                {
                    string books_name = book2_match_res[0].Substring(0, book2_match_res[0].IndexOf("-"));
                    string book2_verse_id = book2_match_res[0].Substring(book2_match_res[0].IndexOf("-") + 1);
                    //
                    string[] book2_verse_id_ar = book2_verse_id.Split('-');
                    if (book2_verse_id_ar[0] == src_book_id.ToString()) // book1_verse_id == book2_verse_id || verse1_id_ar[0] == verse2_id_ar[0]) // same book. && verse1_id_ar[1] == verse2_id_ar[1]) // same chap.
                    {
                        continue;
                    }
                    //
                    // string book2_verse_id_formated = book_abrv[int.Parse(book2_verse_id_ar[0]) - 1] + "-" + book2_verse_id_ar[1] + "-" + book2_verse_id_ar[2];
                    string book2_verse_id_formated = get_book_verse_id_formated(book2_verse_id, ' ', true);
                    string match_str = book2_match_res[2];

                    string book1_marked_verse;
                    string book2_marked_verse;
                    if (!isIndexShrinkedCheckBox.Checked)
                    {
                        book1_marked_verse = verse;
                        mark_line(match_str, exactMatchCheckBox.Checked, ref book1_marked_verse);
                        book1_marked_verse = book1_marked_verse.TrimEnd(new char[] { ' ', '.' });
                        book2_marked_verse = book2_match_res[1];
                        mark_line(match_str, exactMatchCheckBox.Checked, ref book2_marked_verse);
                        book2_marked_verse = book2_marked_verse.TrimEnd(new char[] { ' ', '.' });
                    }
                    else
                    {
                        string fullTextFolder = null;
                        Invoke(new MethodInvoker(delegate
                        {
                            fullTextFolder = (string)(booksDocumentsFolderComboBox.SelectedItem);
                        }));

                        string book1_full_verse;
                        if (useFilesCheckBox.Checked)
                        {
                            book1_full_verse = File.ReadAllText(fullTextFolder + @"\TNC_heb-" + book1_verse_id + ".txt");
                            inc_verses_read_n(this);
                        }
                        else
                        {
                            book1_full_verse = bs.get_line(fullTextFolder, book1_verse_id, true);
                        }

                        string book1_full_match_str;
                        book1_marked_verse = mark_shrinked_verse(book1_full_verse.TrimEnd(new char[] { ' ', '.' }), match_str, false, out book1_full_match_str);
                        string book2_full_match_str;
                        string book2_full_verse;
                        if (useFilesCheckBox.Checked)
                        {
                            book2_full_verse = File.ReadAllText(fullTextFolder + @"\" + books_name + "-" + book2_verse_id + ".txt");
                            inc_verses_read_n(this);
                        }
                        else
                        {
                            book2_full_verse = bs.get_line(fullTextFolder, book2_verse_id, true);
                        }

                        book2_marked_verse = mark_shrinked_verse(book2_full_verse.TrimEnd(new char[] { ' ', '.' }), match_str, false, out book2_full_match_str);
                        if (book1_full_match_str != book2_full_match_str)
                        {
                            book1_marked_verse = book1_marked_verse.Replace("*", "#");
                            book2_marked_verse = book2_marked_verse.Replace("*", "#");
                        }
                    }

                    int b1, c1, v1;
                    get_ids_from_filename(book1_verse_id, out b1, out c1, out v1, 0);
                    int b2, c2, v2;
                    get_ids_from_filename(book2_verse_id, out b2, out c2, out v2, 0);
                    bool already_found_in_book1_current_verse = false;
                    string[] found_already_in_book1_verse_id_ar = (string[])(found_matches_ht[match_str]);
                    if (found_already_in_book1_verse_id_ar != null)
                    {
                        if (found_already_in_book1_verse_id_ar[0] != book1_verse_id_ar[0] || found_already_in_book1_verse_id_ar[1] != book1_verse_id_ar[1] || found_already_in_book1_verse_id_ar[2] != book1_verse_id_ar[2])
                        //{
                        //    throw new Exception();
                        //}
                        //else
                        {
                            n_already_found_matches++;
                            if (!found_again_first_time)
                            {
                                found_again_first_time = true;
                                Invoke(new MethodInvoker(delegate
                                {
                                    outputRichTextBox.AppendText($"[" + book1_verse_id_formated + "] \"" + book1_marked_verse + "\".\t[?].\r\n");
                                    scroll_to_bottom(outputRichTextBox);
                                    //
                                    HtmlElement tr_el = webBrowser.Document.CreateElement("tr");
                                    HtmlElement td_el = webBrowser.Document.CreateElement("td");
                                    td_el.InnerHtml = $"<a class='verse_id' target=\"_blank\" href=\"http://www.faithbit-local.org.il/asp/TNC.aspx?b={b1}&c={c1}&mark_verse={v1}\">[" + book1_verse_id_formated + "]</a> \"<span class='quote'>" + book1_marked_verse.mark_to_html() + $"</span>\".";
                                    tr_el.AppendChild(td_el);
                                    td_el = webBrowser.Document.CreateElement("td");
                                    td_el.InnerHtml = $"<a class='verse_id' target=\"_blank\" href=\"http://www.faithbit-local.org.il/asp/TNC.aspx?b={found_already_in_book1_verse_id_ar[0]}&c={found_already_in_book1_verse_id_ar[1]}&mark_verse={found_already_in_book1_verse_id_ar[2]}\">[" + get_book_verse_id_formated(String.Join("-", found_already_in_book1_verse_id_ar), ' ', true) + "]</a>";
                                    tr_el.AppendChild(td_el);
                                    table_el.AppendChild(tr_el);

                                    // el.SetAttribute("class", "line");
                                    // webBrowser.Document.ActiveElement.AppendChild(el);
                                    scroll_to_bottom_browser(webBrowser);
                                }));
                            }
                            continue;
                        }
                        already_found_in_book1_current_verse = true;
                    }
                    else
                    {
                        found_matches_ht.Add(match_str, book1_verse_id_ar);
                    }

                    n_matches++;

                    Invoke(new MethodInvoker(delegate
                    {
                        outputRichTextBox.AppendText("[" + book1_verse_id_formated + "] \"" + book1_marked_verse + "\".\t[" + book2_verse_id_formated + "] \"" + book2_marked_verse + "\"." + "\r\n");
                        scroll_to_bottom(outputRichTextBox);
                        //
                        //HtmlElement el = webBrowser.Document.CreateElement("div");
                        //el.SetAttribute("class", "line");
                        //el.InnerHtml = ;
                        //webBrowser.Document.ActiveElement.AppendChild(el);

                        HtmlElement tr_el = webBrowser.Document.CreateElement("tr");
                        HtmlElement td_el = webBrowser.Document.CreateElement("td");
                        td_el.InnerHtml = $"<a class='verse_id' target=\"_blank\" href=\"http://www.faithbit-local.org.il/asp/TNC.aspx?b={b1}&c={c1}&mark_verse={v1}\">[" + book1_verse_id_formated + "]</a>" + (already_found_in_book1_current_verse ? "" : (" \"<span class='quote'>" + book1_marked_verse.mark_to_html() + $"</span>\"."));
                        tr_el.AppendChild(td_el);
                        td_el = webBrowser.Document.CreateElement("td");
                        if (already_found_in_book1_current_verse || found_already_in_book1_verse_id_ar == null)
                        {
                            td_el.InnerHtml = $"<a class='verse_id' target=\"_blank\" href=\"http://www.faithbit-local.org.il/asp/TNC.aspx?b={b2}&c={c2}&mark_verse={v2}\">[" + book2_verse_id_formated + "]</a> \"<span class='quote'>" + book2_marked_verse.mark_to_html() + "</span>\".";
                        }
                        else
                        {
                            td_el.InnerHtml = $"<a class='verse_id' target=\"_blank\" href=\"http://www.faithbit-local.org.il/asp/TNC.aspx?b={found_already_in_book1_verse_id_ar[0]}&c={found_already_in_book1_verse_id_ar[1]}&mark_verse={found_already_in_book1_verse_id_ar[2]}\">[" + get_book_verse_id_formated(String.Join("-", found_already_in_book1_verse_id_ar), ' ', true) + "]</a>";
                        }
                        tr_el.AppendChild(td_el);
                        table_el.AppendChild(tr_el);

                        scroll_to_bottom_browser(webBrowser);
                        // webBrowser.Refresh();
                    }));
                }

                if (this.pauseCheckBox.Checked)
                {
                    handle_pause();
                }
            }
            //
            compare_same_book_total_time_timer.Stop();
            //
            Invoke(new MethodInvoker(delegate
            {
                string final_txt = "found: " + n_matches + " matches, repeated: " + n_already_found_matches + " matches";
                searchLableStatus.Text = "Done: " + final_txt + ", total run time: " + compare_same_book_total_time_timer.Elapsed.ToString(timespan_fmt);
                webBrowser.Document.GetElementById("total").InnerText = final_txt;
                File.WriteAllText(search_results_filepath, webBrowser.Document.Body.Parent.OuterHtml, Encoding.GetEncoding(webBrowser.Document.Encoding));
            }));

            // this.Cursor = Cursors.Default;
        }

        private void scroll_to_bottom_browser(WebBrowser webBrowser)
        {
            webBrowser.Document.Window.ScrollTo(0, webBrowser.Document.Body.ScrollRectangle.Height);
        }

        private static string get_book_verse_id_str(string filename)
        {
            return Regex.Match(filename, @"-(?<id>.*?)(\.|$)").Groups["id"].Value;
        }

        private void scroll_to_bottom(RichTextBox tb)
        {
            tb.SelectionStart = tb.Text.Length;
            tb.ScrollToCaret();
        }

        static List<char> sp_chars = new List<char>() { '?', '*' };
        static string sp_chars_str = string.Join("", sp_chars);
        const string puncd_chars_str = "ְֱֲֳִֵֶַָֹֻּׁׂ";
        const string ignored_chars = @"וי";

        private static string mark_shrinked_verse(string full_verse, string match_str, bool is_word_and_not_exact_match, out string full_match_str)
        {

            if (match_str.Contains("#"))
            {
                throw new Exception();
            }
            string marked_verse;
            string ignored_chars_re = @"[" + (is_word_and_not_exact_match ? "" : ignored_chars) + puncd_chars_str + "]#"; // no puncd before first char.
            string re = ignored_chars_re;
            foreach (char ch in match_str)
            {
                string ch_str = ch.ToString();
                if (true)
                {
                    ch_str = Regex.Replace(ch_str, "[צץ]", "[צץ]");
                    ch_str = Regex.Replace(ch_str, "[פף]", "[פף]");
                    ch_str = Regex.Replace(ch_str, "[םמ]", "[םמ]");
                    ch_str = Regex.Replace(ch_str, "[נן]", "[נן]");
                }

                re += ch_str + ignored_chars_re;
            }
            re = Regex.Replace(re, $"[{sp_chars_str}]", @"[\w]$0");
            if (true || !is_word_and_not_exact_match)
            {
                re = $"(?<=(\\s|^)){re}(?=(\\s|$))";
            }
            re = re.Replace("#", "*");
            full_match_str = Regex.Match(full_verse, re).Value;
            if (full_match_str == "")
            {
                return full_verse;
                // throw new Exception(); // lilo
            }
            else
            {
                marked_verse = Regex.Replace(full_verse, re, "*$0*");
            }
            return marked_verse;
        }

        private static string shrink_txt(string txt)
        {
            string verse_ = Regex.Replace(txt, @"[וי]", ""); // @"(?<=\w)[וי](?=\w)"
            verse_ = Regex.Replace(verse_, @"ו(?!=\w)", "");
            return verse_;
        }

        protected void handle_pause()
        {
            compare_same_book_total_time_timer.Stop();
            pauseCheckBox.BackColor = System.Drawing.Color.Pink;
            while (true)
            {
                if (!pauseCheckBox.Checked)
                {
                    break;
                }
                Thread.Sleep(1000);
            }
            pauseCheckBox.BackColor = System.Drawing.SystemColors.Control;
            compare_same_book_total_time_timer.Start();
        }

        private List<string[]> do_my_search(string search_str, int n_min_words_match, out int n_skiped_results, out Stopwatch process_result_timer, out Stopwatch lcs_timer, out double search_time_sec, bool do_print_label = true)
        {
            if (do_print_label)
            {
                Invoke(new MethodInvoker(delegate
                {
                    searchLableStatus.Text = "Searching...";
                }));
            }

            DateTime start_search_time = DateTime.Now;

            search_str = Regex.Replace(search_str, @"[\.\?]", "");
            if (isIndexShrinkedCheckBox.Checked)
            {
                search_str = shrink_txt(search_str);
            }

            string[] serach_words_ar = search_str.Split(' ');
            string words_search_txt = "";
            string re7 = "";
            foreach (string word in serach_words_ar)
            {
                if (words_search_txt != "")
                {
                    words_search_txt += " ";
                    re7 += @"[\s]*";
                }
                words_search_txt += "+" + word;
                re7 += "(" + word + ")?";
            }

            IEnumerable<string> res = hoot.FindDocumentFileNames(words_search_txt);
            // string[] res_ar = res.Cast<string>().ToArray();



            n_skiped_results = 0;
            //string search_regex = "\\b" + Regex.Replace(search_str, "[\\*\\?\\+]", "[\\w]$0") + "\\b";
            //List<string> res_lst_old = new List<string>();
            int n_res = 0;
            string[] res_ar = res.Cast<string>().ToArray();

            SortedDictionary<int, List<string[]>> dict = new SortedDictionary<int, List<string[]>>();


            int max_common_match_len_found = 0;
            process_result_timer = new Stopwatch();
            process_result_timer.Start();
            lcs_timer = new Stopwatch();
            split_timer.Reset();

            foreach (string r in res_ar)
            {
                n_res++;
                //lblStatus.Text = "Searching: " + n_res;
                //groupBox4.Update();

                string filename = r.Substring(r.LastIndexOf(@"\") + 1);
                string verse_ = null;
                string booksDocumentsFolder = null;
                Invoke(new MethodInvoker(delegate
                {
                    booksDocumentsFolder = (string)booksDocumentsFolderComboBox.SelectedItem;
                }));

                if (isPuncdBooksDocumentsCheckBox.Checked)
                {
                    booksDocumentsFolder = Regex.Replace(booksDocumentsFolder, @"_puncd$", "");
                }
                if (useFilesCheckBox.Checked)
                {
                    verse_ = File.ReadAllText(booksDocumentsFolder + @"\" + filename);
                    inc_verses_read_n(this);
                }
                else
                {
                    verse_ = bs.get_line(booksDocumentsFolder, filename, true);
                }

                if (do_print_label)
                {
                    Invoke(new MethodInvoker(delegate
                    {
                        string book_verse_id = filename.Substring(filename.IndexOf("-") + 1).Split('.')[0]; // lilo: dup code.
                        searchLableStatus.Text = "Searching verse: " + book_verse_id + ", found: " + dict.Count + " matches, run_time: " + compare_same_book_total_time_timer.Elapsed.ToString(timespan_fmt);
                    }));
                }

                verse_ = Regex.Replace(verse_, @"[\.\?]", "");
                if (isIndexShrinkedCheckBox.Checked)
                {
                    verse_ = shrink_txt(verse_);
                }

                int n_words_match;
                lcs_timer.Start();
                string max_common_match_str = GFG.word_lcs(search_str, verse_, isIndexShrinkedCheckBox.Checked, out n_words_match);
                if (!exactMatchCheckBox.Checked)
                {
                    string verse__ = verse_;
                    Hashtable _ht = new Hashtable();
                    foreach (string w in search_str.Trim(new char[] { ' ', '.' }).Split(' '))
                    {
                        if (!_ht.Contains(w))
                        {
                            _ht.Add(w, null);
                        }
                    }
                    string s_s = "";
                    foreach (string tt in _ht.Keys)
                    {
                        s_s += tt + " ";
                    }
                    s_s = s_s.Trim();
                    // string s_s = string.Join(" ", new string[] { _ht.Keys });
                    mark_line(s_s, false, ref verse__);
                    n_words_match = Regex.Matches(verse__, @"[\*#]").Count / 2;
                }
                lcs_timer.Stop();
                if (max_common_match_str == null || n_words_match < n_min_words_match)
                {
                    n_skiped_results++;
                    continue;
                }
                string[] words = max_common_match_str.Split(' ').Select(s => s.Trim()).ToArray();
                int n_common_words = 0;
                foreach (string word in words)
                {
                    if (commond_words_lst.Contains(word))
                    {
                        n_common_words++;
                    }
                }
                if (n_words_match - n_common_words < n_min_words_match)
                {
                    n_skiped_results++;
                    continue;
                }
                int max_common_match_len = max_common_match_str.Length;
                int the_match_grade = n_words_match * 1000 + max_common_match_len;

                if (!dict.ContainsKey(the_match_grade))
                {
                    dict.Add(the_match_grade, new List<string[]>());
                }

                string verse = null;
                Invoke(new MethodInvoker(delegate
                {
                    verse = bs.get_line((string)booksDocumentsFolderComboBox.SelectedItem, filename, true);
                }));

                //Invoke(new MethodInvoker(delegate
                //{
                //    verse = File.ReadAllText(booksDocumentsFolderComboBox.SelectedItem + @"\" + filename); // _shrink
                //}));
                dict[the_match_grade].Add(new string[] { filename, verse, max_common_match_str });

                continue;

                //if (max_common_match_len_found <= the_match_grade) // && max_common_match_str.Split(' ').Length > 3)
                //{
                //    max_common_match_len_found = the_match_grade;
                //    string marked_line1 = Regex.Replace(verse, max_common_match_str.Trim(), "*$0*");
                //    res_lst_old.Add(filename.Split('.')[0] + ": " + marked_line1);
                //}

                //continue;

                if (false)
                {
                    int len_fiff = Math.Abs(search_str.Length - verse.Length);
                    int comp_res = CompareHelper.LevenshteinDistance(verse, search_str);
                    int dddfff = search_str.Length - comp_res;
                    continue;
                }

                // re7 = "^.*?(?<mmm>" + re7 + ").*?$";
                Regex re7_ = new Regex(re7, RegexOptions.Compiled);
                MatchCollection mr4 = re7_.Matches(verse_);
                int longest_match_len = 0;
                string longest_match_val = null;
                int longest_match_index = -1;
                foreach (Match mre in mr4)
                {
                    if (longest_match_len < mre.Value.Length)
                    {
                        longest_match_len = mre.Value.Length;
                        longest_match_val = mre.Value;
                        longest_match_index = mre.Index;
                    }
                }

                if (longest_match_val.Trim().Split(' ').Length < 3)
                {
                    continue;
                }

                //string marked_line = Regex.Replace(verse, longest_match_val.Trim(), "*$0*");
                //res_lst_old.Add(filename.Split('.')[0] + ": " + marked_line);
            }

            process_result_timer.Stop();

            DateTime end_search_time = DateTime.Now;
            search_time_sec = end_search_time.Subtract(start_search_time).TotalSeconds;

            List<string[]> res_lst = new List<string[]>();

            foreach (KeyValuePair<int, List<string[]>> dict_item in dict.OrderByDescending(p => p.Key))
            {
                if ((int)(dict_item.Key / 1000) < 2)
                {
                    break;
                }
                foreach (string[] str_ar in dict_item.Value)
                {
                    string line = str_ar[1];
                    string match_str = str_ar[2].Trim();
                    string marked_line = null;
                    if (!isIndexShrinkedCheckBox.Checked)
                    {
                        marked_line = line;
                        mark_line(match_str, exactMatchCheckBox.Checked, ref marked_line);
                    }
                    else
                    {
                        string full_match_str;
                        marked_line = mark_shrinked_verse(line, match_str, false, out full_match_str);
                    }
                    string file_id = null;
                    Invoke(new MethodInvoker(delegate
                    {
                        file_id = str_ar[0].Split('.')[0].Replace(indexStorageFolderComboBox.SelectedItem + "-", "");
                    }));
                    res_lst.Add(new string[] { file_id, line, match_str });
                }
            }

            return res_lst;
        }

        private string mark_line_unused(string line, string match_str)
        {
            //if (ignoreCheckBox.Checked)
            //{
            //    line = shrink_verse(line);
            //}
            string marked_line = Regex.Replace(line, @"(?<=\s|\b|^)(" + match_str.Replace(" ", @"\s") + @")(?=[\s\b\.]|$)", "*$1*");
            if (!marked_line.Contains("*"))
            {
                throw new Exception();
            }
            return marked_line;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = (string)indexStorageFolderComboBox.SelectedItem;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                indexStorageFolderComboBox.SelectedItem = fbd.SelectedPath;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (indexStorageFolderComboBox.SelectedItem == "" || indexStorageFolderComboBox.SelectedItem == "")
            {
                MessageBox.Show("Please supply the index storage folder and the where to start indexing from.");
                return;
            }

            btnStart.Enabled = false;
            btnStop.Enabled = true;
            if (hoot == null)
                hoot = new Hoot(Path.GetFullPath((string)indexStorageFolderComboBox.Text), "index", true);

            string[] files = Directory.GetFiles("$books_sources/" + (string)booksDocumentsFolderComboBox.Text, "*", SearchOption.AllDirectories);
            _indextime = DateTime.Now;
            backgroundWorker1.RunWorkerAsync(files);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.SelectedPath = Directory.GetCurrentDirectory();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                indexStorageFolderComboBox.SelectedItem = fbd.SelectedPath;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string[] files = e.Argument as string[];
            BackgroundWorker wrk = sender as BackgroundWorker;
            int i = 0;
            foreach (string fn in files)
            {
                if (wrk.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                backgroundWorker1.ReportProgress(1, fn);
                try
                {
                    if (hoot.IsIndexed(fn) == false)
                    {
                        TextReader tf = new EPocalipse.IFilter.FilterReader(fn);
                        string s = "";
                        if (tf != null)
                            s = tf.ReadToEnd();

                        hoot.Index(new myDoc(new FileInfo(fn), s), true);
                    }
                }
                catch { }
                i++;
                if (i > 1000)
                {
                    i = 0;
                    hoot.Save();
                }
            }
            hoot.Save();
            hoot.OptimizeIndex();
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblIndexer.Text = "" + e.UserState;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnStart.Enabled = true;
            btnStop.Enabled = false;
            lblIndexer.Text = "" + DateTime.Now.Subtract(_indextime).TotalSeconds + " sec";
            MessageBox.Show("Indexing done : " + DateTime.Now.Subtract(_indextime).TotalSeconds + " sec");
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSearch_Click(null, null);
        }

        private void listBox_DoubleClick(object sender, EventArgs e)
        {
            // Process.Start("" + listBox1.SelectedItem);
            Clipboard.SetText(((ListBox)sender).Items[((ListBox)sender).SelectedIndex].ToString());
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (hoot != null)
                hoot.Shutdown();
        }

        static Stopwatch split_timer = new Stopwatch();

        class GFG
        {
            static string unique_chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzאבגדהוזחטיכלמנסעפצקרשת0123456789" + "#$%&'()*+,-.:;<=>?@[]^_¡¢£¤¥¦§¨©ª«¬­®¯";
            //string u = "";
            //for (int i = 0; i < 256; i++)
            //{
            //    u += (char)i;
            //}
            static char[] split_chars_ar = new char[] { ' ', '.', ';', '"' };

            public static string word_lcs(String X, String Y, bool is_shrink, out int n_words_match)
            {
                if (is_shrink)
                {

                }

                Hashtable ht_word_char = new Hashtable();
                Hashtable ht_char_word = new Hashtable();
                string[] X_sep_ar;
                split_timer.Start();
                //string[] X_sep = SplitterHelper.split_br(X, new string[] { " ", @"\.", ";", "\"" }, out X_sep_ar);
                string[] X_sep = X.Split(split_chars_ar, StringSplitOptions.RemoveEmptyEntries);
                split_timer.Stop();
                int ch_i = 0;
                string X_chars_str = "";
                foreach (string word in X_sep)
                {
                    if (ht_word_char.ContainsKey(word))
                    {
                        X_chars_str += ht_word_char[word];
                        continue;
                    }
                    char ch = unique_chars[ch_i];
                    X_chars_str += ch;
                    ht_word_char.Add(word, ch);
                    ht_char_word.Add(ch, word);
                    ch_i++;
                }
                string[] Y_sep_ar;
                // string[] Y_sep = SplitterHelper.split_br(Y, new string[] { " ", @"\.", ";", @"\?" }, out Y_sep_ar);
                split_timer.Start();
                string[] Y_sep = Y.Split(split_chars_ar, StringSplitOptions.RemoveEmptyEntries);
                split_timer.Stop();
                string Y_chars_str = "";
                foreach (string word in Y_sep)
                {
                    if (ht_word_char.ContainsKey(word))
                    {
                        Y_chars_str += ht_word_char[word];
                        continue;
                    }
                    char ch = unique_chars[ch_i];
                    Y_chars_str += ch;
                    ht_word_char.Add(word, ch);
                    ht_char_word.Add(ch, word);
                    ch_i++;
                }

                string LCS_chars_res = get_LCSubStr(X_chars_str, Y_chars_str);

                if (LCS_chars_res == null)
                {
                    // warning: check ' char.
                    n_words_match = 0;
                    return null;
                }

                n_words_match = LCS_chars_res.Length;

                string res_str = "";
                foreach (char ch_ in LCS_chars_res)
                {
                    res_str += ht_char_word[ch_] + " ";
                }
                return res_str.Trim();
            }

            public static string get_LCSubStr(String X, String Y)
            {
                int m = X.Length;
                int n = Y.Length;
                // Create a table to store lengths of longest common 
                // suffixes of substrings. Note that LCSuff[i][j] 
                // contains length of longest common suffix of X[0..i-1] 
                // and Y[0..j-1]. The first row and first column entries 
                // have no logical meaning, they are used only for 
                // simplicity of program 
                int[,] LCSuff = new int[m + 1, n + 1];

                // To store length of the longest common substring 
                int len = 0;

                // To store the index of the cell which contains the 
                // maximum value. This cell's index helps in building 
                // up the longest common substring from right to left. 
                int row = 0, col = 0;

                /* Following steps build LCSuff[m+1][n+1] in bottom 
                up fashion. */
                for (int i = 0; i <= m; i++)
                {
                    for (int j = 0; j <= n; j++)
                    {
                        if (i == 0 || j == 0)
                            LCSuff[i, j] = 0;

                        else if (X[i - 1] == Y[j - 1])
                        {
                            LCSuff[i, j] = LCSuff[i - 1, j - 1] + 1;
                            if (len < LCSuff[i, j])
                            {
                                len = LCSuff[i, j];
                                row = i;
                                col = j;
                            }
                        }
                        else
                            LCSuff[i, j] = 0;
                    }
                }

                // if true, then no common substring exists 
                if (len == 0)
                {
                    Console.Write("No Common Substring");
                    return null;
                }

                // allocate space for the longest common substring 
                String resultStr = "";

                // traverse up diagonally form the (row, col) cell 
                // until LCSuff[row][col] != 0 
                while (LCSuff[row, col] != 0)
                {
                    resultStr = X[row - 1] + resultStr; // or Y[col-1] 
                    --len;

                    // move diagonally up to previous cell 
                    row--;
                    col--;
                }

                // required longest common substring 
                return resultStr;
            }

        }

        private void index_Storage_Directory_ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Uncomment to Index.
            //btnStart_Click(sender, e);
            //return;

            string index_Storage_Directory = (string)(((ComboBox)sender).SelectedItem);
            isIndexShrinkedCheckBox.Checked = Regex.IsMatch(index_Storage_Directory, @"_shrink.Index");
            hoot = new Hoot(Path.GetFullPath(index_Storage_Directory), "index", true);
            countWordsButton.Enabled = true;
        }

        static Books_ser bs;

        private void booksDocumentsFolderComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string booksDocumentsFolder = (string)(((ComboBox)sender).SelectedItem);
            isPuncdBooksDocumentsCheckBox.Checked = Regex.IsMatch(booksDocumentsFolder, @"_puncd");
        }

        private void runModeComboBox_TextChanged(object sender, EventArgs e)
        {
            if (runModeComboBox != sender)
            {
                throw new Exception("expecting to handle only runModeComboBox.");
            }
            minWordsTextBox.Enabled = (string)runModeComboBox.SelectedItem == "Compare books";
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
        }

        private void openResultsButton_Click(object sender, EventArgs e)
        {
            Process.Start(search_results_filepath);
        }

        private void resultsTabControl_MouseDown(object sender, MouseEventArgs e)
        { // right click on the Tab header will remove it from the TabControl.
            if (e.Button != MouseButtons.Right) return;
            for (int i = 0; i < resultsTabControl.TabPages.Count; i++)
            {
                if (resultsTabControl.GetTabRect(i).Contains(e.X, e.Y))
                {
                    resultsTabControl.TabPages.RemoveAt(i);
                }
            }
        }
    }

    static class SplitterHelper
    {
        public static string[] split_br(string full_text, string[] separators, out string[] separators_array)
        {
            full_text = Regex.Replace(full_text, @"[ ]+", " ");
            string[] desc_lines = SplitterHelper.split_not_inside_brackets_ToArray(full_text, separators);
            separators_array = SplitterHelper.get_separators_array(full_text, desc_lines);
            desc_lines = desc_lines.Select(s => s.ClearHTML()).Where(s => s != "").ToArray();
            /*
            //return name_el.InnerHtml.Split(new string[] { "<br>", "," }, StringSplitOptions.RemoveEmptyEntries).Select(s => RegexHelper.ClearHTML(s).Trim()).Where(s => s != "").ToArray(); // ,"(", ")" 
            string[] desc_lines = ParserHelper.split_not_inside_brackets_ToArray(pb, full_text, new string[] { "<br>", "\n", ", " }); // , "; "
            separators_array = ParserHelper.get_separators_array(full_text, desc_lines);
            */
            return desc_lines;
        }

        public static string[] get_separators_array(string full_text, string[] desc_lines)
        {
            const string sep_re = @"(?<separator{0}>[\s\S]{1})";
            string re = string.Format(sep_re, 0, "*");
            int i;
            for (i = 0; i < desc_lines.Length; i++)
            {
                string line = desc_lines[i];
                if (line != HttpUtility.HtmlDecode(line))
                {
                    throw new Exception("desc_lines array should be HtmlDecoded when calling get_separators_array(). Please contact the Programmer.");
                }
                string re_to_add = HttpUtility.HtmlDecode(line).to_regex_string() + string.Format(sep_re, i + 1, (i < desc_lines.Length - 1 ? "+" : "*") + "?");
                re += re_to_add;
            }
            re = "^" + re + "$";
            re = Regex.Replace(re, @"\s+", @"\s+?");
            Match match = Regex.Match(full_text, re, RegexOptions.Multiline);
            if (!match.Success)
            {
                throw new Exception("RegEx in get_separators_array() is invalid. Please contact the Programmer.");
            }

            List<string> separators_list = new List<string>();
            for (i = 0; i <= desc_lines.Length; i++)
            {
                string separator = match.Groups[string.Format("separator{0}", i)].Value;
                separators_list.Add(separator);
            }
            string[] separators_array = separators_list.ToArray();
            return separators_array;
        }

        public static string[] split_not_inside_brackets_ToArray(string line, string[] breaking_delimiters, string[] non_breaking_delimiters = null)
        {
            if (line == null)
            {
                return null;
            }
            string[] res = split_not_inside_brackets(line, breaking_delimiters, non_breaking_delimiters).ToArray();
            return res;
        }

        public static List<string> split_not_inside_brackets(string line, string[] breaking_delimiters, string[] non_breaking_delimiters = null)
        { // breaking_delimiters => even inside brackets.
            string[] delimiters = new string[(non_breaking_delimiters == null ? 0 : non_breaking_delimiters.Length) + (breaking_delimiters == null ? 0 : breaking_delimiters.Length)];
            int del_i = 0;
            Hashtable non_breaking_delimiters_ht = new Hashtable();
            if (non_breaking_delimiters != null)
            {
                foreach (string str in non_breaking_delimiters)
                {
                    if (str != str.ToLower())
                    {
                        throw new Exception("non_breaking_delimiters should be Lower Case.");
                    }
                    non_breaking_delimiters_ht.Add(str, null);
                    delimiters[del_i++] = str;
                }
            }
            //
            Hashtable breaking_delimiters_ht = new Hashtable();
            if (breaking_delimiters != null)
            {
                foreach (string str in breaking_delimiters)
                {
                    if (str != str.ToLower())
                    {
                        throw new Exception("breaking_delimiters should be Lower Case.");
                    }
                    breaking_delimiters_ht.Add(str, null);
                    delimiters[del_i++] = str;
                }
            }
            //
            //_line = RegexHelper.ClearHTML(_line.HtmlDecode());
            List<string> res = new List<string>();
            int start_at = 0;
            string re = string.Join("|", delimiters.Select(x => "(" + x + ")").ToArray());
            while (true)
            {
                //int index = line.ToLower().IndexOf(start_at, delimiters, ref delimiter);
                Match m = Regex.Match(line.Substring(start_at), re, RegexOptions.IgnoreCase);
                if (!m.Success)
                    break;
                int index = m.Index;
                string delimiter = m.Value;
                string sub_str = line.Substring(0, start_at + index);
                if (!Regex.IsMatch(sub_str, @"\([^\(\)]*$") // not inside opening bracket
                                                            // && !Regex.IsMatch(sub_str, @"<[^<>]*$")
                    && !re_ht_match_str(non_breaking_delimiters_ht, delimiter))
                {
                    res.Add(sub_str.Trim());
                    line = line.Substring(start_at + index + delimiter.Length).Trim();
                    start_at = 0;
                }
                else
                    start_at += index + delimiter.Length;
            }
            res.Add(line.Trim()); // last substring.
            return res.ToList().Where(s => s != "").ToList();
        }

        private static bool re_ht_match_str(Hashtable ht, string str)
        {
            foreach (string re in ht.Keys)
            {
                if (Regex.IsMatch(str, re))
                {
                    return true;
                }
            }
            return false;
        }

        private static RegexOptions regexOptions = RegexOptions.Singleline | RegexOptions.IgnoreCase;

        internal static string ClearHTML(this string Input, bool tagsToCrLf = true, bool tagsToSpace = false)
        {
            Input = HttpUtility.HtmlDecode(Input);

            if (Input == null)
            {
                return Input;
            }

            if (tagsToCrLf)
            {
                Input = Regex.Replace(Input, @"<br[^>]*>", "\n", regexOptions);
                Input = Regex.Replace(Input, @"</(p|div)>", "\n", regexOptions);
            }

            Input = Regex.Replace(Input, @"<[^>]*>", tagsToSpace ? " " : "", regexOptions);
            Input = HttpUtility.HtmlDecode(Input); // .Replace("&nbsp;", " ");
            Input = Input.TrimIfNotEmpty();

            Input = Regex.Replace(Input, @"[ ]+", " ").Trim();

            return Input;
        }

        public static string to_regex_string(this string str)
        {
            return Regex.Replace(str, @"[\\\.\(\)\?\*\+\]\[\^\$']", "\\$0");
        }

        public static string TrimIfNotEmpty(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            else
            {
                return value.Trim();
            }
        }

        public static string mark_to_html(this string str)
        {
            string res = Regex.Replace(str, @"\*([^\*]+)\*", "<font class='exact_match'>$1</font>");
            res = Regex.Replace(res, @"\#([^#]+)#", "<font class='close_match'>$1</font>");
            return res;
        }
    }
}