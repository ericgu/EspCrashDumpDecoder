using Microsoft.Win32;
using System;

namespace Esp32_Crash_Dump_Decoder
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void OnLoad(EventArgs e)
        {
            _buttonProcess.Click += _buttonProcess_Click;

            RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\EspCrashDumpDecoder");

            _textBoxMapFilename.Text = (string) registryKey.GetValue("Filename");

            base.OnLoad(e);
        }

        private void _buttonProcess_Click(object sender, EventArgs e)
        {
            RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\EspCrashDumpDecoder");

            registryKey.SetValue("Filename", _textBoxMapFilename.Text);

            MapFile mapFile = new MapFile(_textBoxMapFilename.Text);

            string[] frames = _textBoxDump.Text.Split(' ');

            _textBoxStack.Text = "";

            for (int i = 1; i < frames.Length; i++)
            {
                string frame = frames[i];

                string[] frameAddresses = frame.Split(':');

                int address = Convert.ToInt32(frameAddresses[0], 16);

                Symbol symbol = mapFile.FindSymbol(address);

                string line;

                if (symbol != null)
                {
                    line = String.Format("{0:X8}: ({1:d4} / {2:d4}) {3}\r\n", address, address - symbol._address, symbol._length, symbol.FixedName);

                }
                else
                {
                    line = String.Format("{0:X8}:\r\n", address);
                }

                _textBoxStack.Text += line;
            }
        }


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this._buttonProcess = new System.Windows.Forms.Button();
            this._textBoxDump = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._textBoxStack = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this._textBoxMapFilename = new System.Windows.Forms.TextBox();
            this._buttonBrowse = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // _buttonProcess
            // 
            this._buttonProcess.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._buttonProcess.Location = new System.Drawing.Point(1546, 26);
            this._buttonProcess.Name = "_buttonProcess";
            this._buttonProcess.Size = new System.Drawing.Size(206, 59);
            this._buttonProcess.TabIndex = 0;
            this._buttonProcess.Text = "Process";
            this._buttonProcess.UseVisualStyleBackColor = true;
            // 
            // _textBoxDump
            // 
            this._textBoxDump.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._textBoxDump.Location = new System.Drawing.Point(25, 112);
            this._textBoxDump.Multiline = true;
            this._textBoxDump.Name = "_textBoxDump";
            this._textBoxDump.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._textBoxDump.Size = new System.Drawing.Size(975, 214);
            this._textBoxDump.TabIndex = 1;
            this._textBoxDump.TextChanged += new System.EventHandler(this._textBoxDump_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Backtrace from ESP";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // _textBoxStack
            // 
            this._textBoxStack.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._textBoxStack.Font = new System.Drawing.Font("Courier New", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._textBoxStack.Location = new System.Drawing.Point(25, 358);
            this._textBoxStack.Multiline = true;
            this._textBoxStack.Name = "_textBoxStack";
            this._textBoxStack.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._textBoxStack.Size = new System.Drawing.Size(1727, 443);
            this._textBoxStack.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 329);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(460, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Stack:  Address (offset in function / total size of function) Function name";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Map File:";
            // 
            // _textBoxMapFilename
            // 
            this._textBoxMapFilename.Location = new System.Drawing.Point(28, 44);
            this._textBoxMapFilename.Name = "_textBoxMapFilename";
            this._textBoxMapFilename.Size = new System.Drawing.Size(741, 22);
            this._textBoxMapFilename.TabIndex = 6;
            // 
            // _buttonBrowse
            // 
            this._buttonBrowse.Location = new System.Drawing.Point(804, 33);
            this._buttonBrowse.Name = "_buttonBrowse";
            this._buttonBrowse.Size = new System.Drawing.Size(168, 44);
            this._buttonBrowse.TabIndex = 7;
            this._buttonBrowse.Text = "Browse";
            this._buttonBrowse.UseVisualStyleBackColor = true;
            this._buttonBrowse.Click += new System.EventHandler(this._buttonBrowse_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Enabled = false;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(1033, 112);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(719, 214);
            this.textBox1.TabIndex = 8;
            this.textBox1.Text = resources.GetString("textBox1.Text");
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1793, 803);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this._buttonBrowse);
            this.Controls.Add(this._textBoxMapFilename);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._textBoxStack);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._textBoxDump);
            this.Controls.Add(this._buttonProcess);
            this.Name = "Form1";
            this.Text = "ESP Crash Dump Decoder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _buttonProcess;
        private System.Windows.Forms.TextBox _textBoxDump;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _textBoxStack;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox _textBoxMapFilename;
        private System.Windows.Forms.Button _buttonBrowse;
        private System.Windows.Forms.TextBox textBox1;
    }
}

