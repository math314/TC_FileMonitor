using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;

namespace TC_FileMonitor
{
    public class Translate
    {
        const string INIT_URL = "http://honyaku.yahoo.co.jp/transtext/";
        const string BASE_URL = "http://honyaku.yahoo.co.jp/TranslationText";
        static readonly Encoding ENC = Encoding.UTF8;
        static readonly Regex _valReg = new Regex("value=\"([^\"]+)\"", RegexOptions.Compiled);
        static readonly Regex _transReg = new Regex("\"TranslatedText\":\"([^\"]+)\"", RegexOptions.Compiled);
        static readonly Regex _utf16Reg = new Regex(@"\\u([0-9a-fA-F]{4})", RegexOptions.Compiled);

        public string EN2JA(string text)
        {
            return Trans(text, TransType.EN2JA);
        }

        public string JA2EN(string text)
        {
            return Trans(text, TransType.JA2EN);
        }

        enum TransType
        {
            EN2JA, JA2EN
        }

        /// <summary>
        /// crumbを取得
        /// </summary>
        /// <returns></returns>
        private string GetTTcrumb()
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(INIT_URL);
            using (var res = req.GetResponse())
            using (var s = res.GetResponseStream())
            {
                StreamReader sr = new StreamReader(s, ENC);
                string html = sr.ReadToEnd();
                //htmlのparseは重そうなのでゴリ押しする
                var match = _valReg.Match(html, html.IndexOf("id=\"TTcrumb\""), 256);
                return match.Groups[1].Value;
            }
        }

        private string Trans(string text, TransType tt)
        {
            //crumbの取得
            string crumb = GetTTcrumb();

            //訳
            return GetTranslatedText(text, crumb, tt);

        }

        private string GetTranslatedText(string origin, string crumb, TransType tt)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(BASE_URL);

            req.Method = "POST";
            //header
            req.Referer = INIT_URL;
            req.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            //body
            byte[] body = MakePostBody(origin, crumb, tt);
            req.ContentLength = body.Length;
            using (var rs = req.GetRequestStream())
                rs.Write(body, 0, body.Length);

            using (var res = req.GetResponse())
            using (var s = res.GetResponseStream())
            {
                StreamReader sr = new StreamReader(s, ENC);
                string json = sr.ReadToEnd();
                //やっぱりjsonのparseも面倒なのでゴリ押しする
                var matches = _transReg.Matches(json);
                return string.Join("\n",
                    matches.Cast<Match>()
                    .Select(m => _utf16Reg.Replace(m.Groups[1].Value, new MatchEvaluator(StrToChar)))
                    .ToArray()
                    );
            }
        }

        //MatchEvaluatorデリゲートメソッド
        private string StrToChar(System.Text.RegularExpressions.Match m)
        {
            //3000 ---> ' ' のように、16進数を元に文字列(UNICODE文字列)に変換する
            return new string(new char[] { (char)Convert.ToInt16(m.Groups[1].Value, 16) });
        }

        /// <summary>
        /// postするbodyを作成する
        /// </summary>
        /// <param name="tt"></param>
        /// <param name="crumb"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        private byte[] MakePostBody(string text, string crumb, TransType tt)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            //アイテムを追加
            dic.Add("ieid", tt == TransType.EN2JA ? "en" : "ja");
            dic.Add("oeid", tt == TransType.EN2JA ? "ja" : "en");
            dic.Add("results", "1000");
            dic.Add("formality", "0");
            dic.Add("output", "json");
            dic.Add("p", HttpUtility.UrlEncode(text));
            dic.Add("_crumb", crumb);
            //"key=value"を&で繋げる
            string body = string.Join("&", dic.Select(pair => pair.Key + '=' + pair.Value).ToArray());
            return ENC.GetBytes(body);
        }
    }
}