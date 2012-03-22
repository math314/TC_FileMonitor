using System;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace TC_FileMonitor
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void textBox_DragOver(object sender, DragEventArgs e)
        {
            if(!e.Data.GetDataPresent(DataFormats.FileDrop)) return;

            e.Effect = DragDropEffects.Copy;
        }

        private void textBox_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop)) return;

            var fileNames = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (textBoxDirectory == sender)
                textBoxDirectory.Text = fileNames[0];
            else if( textBoxProjectPath == sender)
                textBoxProjectPath.Text = fileNames[0];
        }

        private void fileSystemWatcher_Created(object sender, System.IO.FileSystemEventArgs e)
        {
            textBoxInfo.AppendText(e.ChangeType.ToString() + " : " + e.Name + "\r\n" );

            //プロジェクトファイルを書き換える
            ChangeProjectFile(e.Name);

            //VCExpressの起動
            CreateVCProcessIfProcessNonExisted();

            //説明の部分を翻訳する
            TranslateDesctiption(Path.ChangeExtension(e.Name, "html"));

            //説明のtxtファイルを開く
            OpenDescriptionText(Path.ChangeExtension(e.Name,"html"));
        }

        private void TranslateDesctiption(string html)
        {
            //htmlをロード
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.Load(Properties.Settings.Default.Directory + "\\" + html,Encoding.UTF8);

            var ProblemStatementNode = doc.DocumentNode.SelectSingleNode("./html/body/table/tr[2]/td[2]");
            Translate tl = new Translate();
            try
            {
                ProblemStatementNode.InnerHtml += "<br>" + tl.EN2JA(ProblemStatementNode.InnerText);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            doc.Save(Properties.Settings.Default.Directory + "\\" + html);
        }

        private void OpenDescriptionText(string html)
        {
            Process.Start(Properties.Settings.Default.Directory + "\\" + html);
            textBoxInfo.AppendText(html + " : descriptionを表示。\r\n");
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            fileSystemWatcher.Path = Properties.Settings.Default.Directory;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// vsxprojを書き換える
        /// </summary>
        private void ChangeProjectFile(string cppName)
        {
            string projectPath = Properties.Settings.Default.ProjectPath;
            //変更する
            ChangeProjectIncludeFile(projectPath, cppName);
            ChangeProjectIncludeFile(projectPath + ".filters", cppName);
            textBoxInfo.AppendText( cppName + "をプロジェクトに設定\r\n");

        }

        private void ChangeProjectIncludeFile(string filePath,string cppName)
        {
            //ファイルを開く
            var x = XDocument.Load(filePath);
            XmlNamespaceManager xnm;
            using (var reader = x.CreateReader())
            {
                xnm = new XmlNamespaceManager(reader.NameTable);
            }
            xnm.AddNamespace("ns", x.Root.Name.NamespaceName);

            //cppファイルを見つけ、includeを書き換える
            x.XPathSelectElements("./ns:Project/ns:ItemGroup/ns:ClCompile", xnm)
                .Where(e =>
                {
                    string include = e.Attribute("Include").Value;
                    return include != null && include.EndsWith(".cpp");
                })
                .FirstOrDefault()
                .Attribute("Include").Value = cppName;

            //保存
            x.Save(filePath);
        }

        private void CreateVCProcessIfProcessNonExisted()
        {
            Process p = Process.GetProcessesByName("VCExpress").FirstOrDefault();
            if (p == null)
            {
                p = Process.Start(Properties.Settings.Default.ProjectPath); //関連付けを利用してVCExpressを起動
                p.WaitForInputIdle();
                textBoxInfo.AppendText("visual studio を起動しました\r\n");
            }
        }
    }
}
