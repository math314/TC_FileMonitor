namespace TC_FileMonitor
{
    partial class FormMain
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxInfo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxProjectPath = new System.Windows.Forms.TextBox();
            this.textBoxDirectory = new System.Windows.Forms.TextBox();
            this.fileSystemWatcher = new System.IO.FileSystemWatcher();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(372, 35);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 23);
            this.buttonSave.TabIndex = 1;
            this.buttonSave.Text = "設定する";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // textBoxInfo
            // 
            this.textBoxInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.textBoxInfo.Location = new System.Drawing.Point(0, 75);
            this.textBoxInfo.Multiline = true;
            this.textBoxInfo.Name = "textBoxInfo";
            this.textBoxInfo.Size = new System.Drawing.Size(459, 187);
            this.textBoxInfo.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "監視するディレクトリ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "プロジェクトファイルへのパス";
            // 
            // textBoxProjectPath
            // 
            this.textBoxProjectPath.AllowDrop = true;
            this.textBoxProjectPath.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::TC_FileMonitor.Properties.Settings.Default, "ProjectPath", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation));
            this.textBoxProjectPath.Location = new System.Drawing.Point(147, 37);
            this.textBoxProjectPath.Name = "textBoxProjectPath";
            this.textBoxProjectPath.Size = new System.Drawing.Size(212, 19);
            this.textBoxProjectPath.TabIndex = 5;
            this.textBoxProjectPath.Text = global::TC_FileMonitor.Properties.Settings.Default.ProjectPath;
            this.textBoxProjectPath.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox_DragDrop);
            this.textBoxProjectPath.DragOver += new System.Windows.Forms.DragEventHandler(this.textBox_DragOver);
            // 
            // textBoxDirectory
            // 
            this.textBoxDirectory.AllowDrop = true;
            this.textBoxDirectory.DataBindings.Add(new System.Windows.Forms.Binding("Text", global::TC_FileMonitor.Properties.Settings.Default, "Directory", true, System.Windows.Forms.DataSourceUpdateMode.OnValidation));
            this.textBoxDirectory.Location = new System.Drawing.Point(147, 12);
            this.textBoxDirectory.Name = "textBoxDirectory";
            this.textBoxDirectory.Size = new System.Drawing.Size(212, 19);
            this.textBoxDirectory.TabIndex = 0;
            this.textBoxDirectory.Text = global::TC_FileMonitor.Properties.Settings.Default.Directory;
            this.textBoxDirectory.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox_DragDrop);
            this.textBoxDirectory.DragOver += new System.Windows.Forms.DragEventHandler(this.textBox_DragOver);
            // 
            // fileSystemWatcher
            // 
            this.fileSystemWatcher.EnableRaisingEvents = true;
            this.fileSystemWatcher.Filter = "*.cpp";
            this.fileSystemWatcher.Path = global::TC_FileMonitor.Properties.Settings.Default.Directory;
            this.fileSystemWatcher.SynchronizingObject = this;
            this.fileSystemWatcher.Created += new System.IO.FileSystemEventHandler(this.fileSystemWatcher_Created);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(459, 262);
            this.Controls.Add(this.textBoxProjectPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxInfo);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.textBoxDirectory);
            this.Name = "FormMain";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.FileSystemWatcher fileSystemWatcher;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TextBox textBoxDirectory;
        private System.Windows.Forms.TextBox textBoxInfo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxProjectPath;
        private System.Windows.Forms.Label label2;
    }
}

