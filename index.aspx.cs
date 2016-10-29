using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;


namespace AP_Assignment
{
    public partial class index : System.Web.UI.Page
    {
        private HashSet<string> dict = new HashSet<string> { };

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                {
                    dictcount.Text = dict.Count.ToString() +"<br/>";

                }

            }
        }

        protected void checkwords(object sender, EventArgs e)
        {
            steps.Text = "";
                // read JSON directly from a file
                using (StreamReader file = File.OpenText(Server.MapPath("~") + "/dictionary.json"))
                using (JsonTextReader reader = new JsonTextReader(file))
                {

                    //for(int i = 0; i <= 15;reader.Read())
                    //{
                    //    i++;

                    //    if (reader.Value != null)
                    //    {

                    //        if (reader.TokenType.ToString() == "PropertyName")
                    //        {
                    //            myliteral.Text += "<tr role=\"row\" class=\"even\">";
                    //            myliteral.Text += "<td>" + reader.Value + "</td> ";
                    //        }
                    //        else
                    //        {
                    //            myliteral.Text += "<td>" + reader.Value + "</td> ";
                    //            myliteral.Text += "</tr>";
                    //        }

                    //    }

                    //}

                    while (reader.Read())
                    {
                        if (reader.Value != null)
                        {
                            if (reader.TokenType.ToString() == "PropertyName")
                            {
                                dict.Add(reader.Value.ToString().ToLower());
                            }
                        }

                    }
                    dict.Add("hit");
                    dict.Add("hot");
                    dict.Add("dot");
                    dict.Add("dog");
                    dict.Add("cog");
                }
          //  myliteral.Text = ladderlength(firstword.ToString().ToLower(), secondword.ToString().ToLower(), dict).ToString();
            myliteral.Text = ladderlength(firstword.Text.ToString().ToLower(), secondword.Text.ToString().ToLower(), dict).ToString();
            dictcount.Text = dict.Count.ToString();
              
        }
        public  int ladderlength(String beginWord, String endWord, ISet<String> wordDict )
        {
            
            if (beginWord == null || endWord == null || wordDict == null || wordDict.Count == 0)
            {
                return 0;
            }

            Queue<String> queue = new Queue<string>();

            queue.Enqueue(beginWord);
            wordDict.Remove(beginWord);

            int length = 0;

            while (queue.Count > 0)
            {
                int count = queue.Count;
                string current = "";
                // Check each adjacent string
                for (int i = 0; i < count; i++) //  BFS, each layer visit each node
                {
                    
                     current = queue.Dequeue();
                     
                    // Check if there's adjacent string
                    for (int j = 0; j < current.Length; j++)
                    {
                        for (char c = 'a'; c <= 'z'; c++)
                        {
                            if (c == current[j])
                            {
                                continue;
                            }

                            String temp = replace(current, j, c);
                            if (temp.CompareTo(endWord) == 0)
                            {
                                return length + 1;
                            }

                            if (wordDict.Contains(temp))
                            {
                                queue.Enqueue(temp);
                               
                                wordDict.Remove(temp);
                               
                            }
                        }
                    }
                }
                
                length++;
            }
            return 0;
        }
        private void SetLiteratTextSafe(string result)
        {
            steps.Text += "<br>" + result;
        }
        private static String replace(String s, int index, char c)
        {
            char[] chars = s.ToCharArray();
            chars[index] = c;
            return new String(chars);
        }
        //public static int ladderlength(String start, String end,  IList<String> dict)
        //{
        //    var preVisitedStr = new List<String> { start };
        //    var level = 1;

        //    while (preVisitedStr.Count != 0)
        //    {
        //      var nextVisitedStr = new List<String>();
        //        foreach (var visited in preVisitedStr)
        //        {
        //            if (IsOnlyOneCharDifferent(end, visited))
        //            {
        //                return level + 1;
        //            }

        //            for (var i = dict.Count() - 1; i >= 0; i--)
        //            {
        //                if (!IsOnlyOneCharDifferent(visited, dict[i]))
        //                {
        //                    continue;
        //                }

        //                nextVisitedStr.Add(dict[i]);
        //                dict.RemoveAt(i);
        //            }
        //        }

        //        preVisitedStr = nextVisitedStr;
        //        level++;
        //    }

        //    return 0;
        //}
        //private static bool IsOnlyOneCharDifferent(string str1, string str2)
        //{
        //    // all string have same length
        //    return str1.Where((t, i) => !t.Equals(str2[i])).Count() == 1;
        //}
    }
}