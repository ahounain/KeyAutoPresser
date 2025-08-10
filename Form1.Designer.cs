namespace KeyAutoPresser;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    private System.Windows.Forms.Label lblKey;
    private System.Windows.Forms.TextBox txtKey;
    private System.Windows.Forms.Label lblInterval;
    private System.Windows.Forms.NumericUpDown numInterval;
    private System.Windows.Forms.Button btnToggle;
    private System.Windows.Forms.Label lblStatus;
    private System.Windows.Forms.Label lblHotkey;
    private System.Windows.Forms.CheckBox chkScanCode;
    private System.Windows.Forms.CheckBox chkHold;

    /// <summary>
    ///  Clean up any resources being used.
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

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        this.lblKey = new System.Windows.Forms.Label();
        this.txtKey = new System.Windows.Forms.TextBox();
        this.lblInterval = new System.Windows.Forms.Label();
        this.numInterval = new System.Windows.Forms.NumericUpDown();
        this.btnToggle = new System.Windows.Forms.Button();
        this.lblStatus = new System.Windows.Forms.Label();
        this.lblHotkey = new System.Windows.Forms.Label();
        this.chkScanCode = new System.Windows.Forms.CheckBox();
        this.chkHold = new System.Windows.Forms.CheckBox();
        ((System.ComponentModel.ISupportInitialize)(this.numInterval)).BeginInit();
        this.SuspendLayout();
        // 
        // lblKey
        // 
        this.lblKey.AutoSize = true;
        this.lblKey.Location = new System.Drawing.Point(16, 18);
        this.lblKey.Name = "lblKey";
        this.lblKey.Size = new System.Drawing.Size(69, 15);
        this.lblKey.TabIndex = 0;
        this.lblKey.Text = "Target key:";
        // 
        // txtKey
        // 
        this.txtKey.Location = new System.Drawing.Point(100, 14);
        this.txtKey.Name = "txtKey";
        this.txtKey.PlaceholderText = "Click here, then press a key";
        this.txtKey.ReadOnly = true;
        this.txtKey.Size = new System.Drawing.Size(220, 23);
        this.txtKey.TabIndex = 1;
        this.txtKey.TabStop = true;
        this.txtKey.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtKey_KeyDown);
        this.txtKey.Enter += new System.EventHandler(this.txtKey_Enter);
        this.txtKey.Leave += new System.EventHandler(this.txtKey_Leave);
        this.txtKey.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtKey_MouseDown);
        // 
        // lblInterval
        // 
        this.lblInterval.AutoSize = true;
        this.lblInterval.Location = new System.Drawing.Point(16, 55);
        this.lblInterval.Name = "lblInterval";
        this.lblInterval.Size = new System.Drawing.Size(72, 15);
        this.lblInterval.TabIndex = 2;
        this.lblInterval.Text = "Interval (ms):";
        // 
        // numInterval
        // 
        this.numInterval.Increment = new decimal(new int[] { 10, 0, 0, 0 });
        this.numInterval.Location = new System.Drawing.Point(100, 51);
        this.numInterval.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
        this.numInterval.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        this.numInterval.Name = "numInterval";
        this.numInterval.Size = new System.Drawing.Size(120, 23);
        this.numInterval.TabIndex = 3;
        this.numInterval.Value = new decimal(new int[] { 100, 0, 0, 0 });
        this.numInterval.ValueChanged += new System.EventHandler(this.numInterval_ValueChanged);
        // 
        // btnToggle
        // 
        this.btnToggle.Location = new System.Drawing.Point(16, 90);
        this.btnToggle.Name = "btnToggle";
        this.btnToggle.Size = new System.Drawing.Size(304, 34);
        this.btnToggle.TabIndex = 4;
        this.btnToggle.Text = "Start (or press F8)";
        this.btnToggle.UseVisualStyleBackColor = true;
        this.btnToggle.Click += new System.EventHandler(this.btnToggle_Click);
        // 
        // lblStatus
        // 
        this.lblStatus.AutoSize = true;
        this.lblStatus.Location = new System.Drawing.Point(16, 185);
        this.lblStatus.Name = "lblStatus";
        this.lblStatus.Size = new System.Drawing.Size(67, 15);
        this.lblStatus.TabIndex = 5;
        this.lblStatus.Text = "Status: Idle";
        // 
        // lblHotkey
        // 
        this.lblHotkey.AutoSize = true;
        this.lblHotkey.ForeColor = System.Drawing.SystemColors.GrayText;
        this.lblHotkey.Location = new System.Drawing.Point(16, 209);
        this.lblHotkey.Name = "lblHotkey";
        this.lblHotkey.Size = new System.Drawing.Size(214, 15);
        this.lblHotkey.TabIndex = 6;
        this.lblHotkey.Text = "Global toggle hotkey: F8 (no modifiers)";
        // 
        // chkScanCode
        // 
        this.chkScanCode.AutoSize = true;
        this.chkScanCode.Location = new System.Drawing.Point(16, 136);
        this.chkScanCode.Name = "chkScanCode";
        this.chkScanCode.Size = new System.Drawing.Size(285, 19);
        this.chkScanCode.TabIndex = 7;
        this.chkScanCode.Text = "Use scan code mode (more compatible with games)";
        this.chkScanCode.UseVisualStyleBackColor = true;
        // 
        // chkHold
        // 
        this.chkHold.AutoSize = true;
        this.chkHold.Location = new System.Drawing.Point(16, 158);
        this.chkHold.Name = "chkHold";
        this.chkHold.Size = new System.Drawing.Size(162, 19);
        this.chkHold.TabIndex = 8;
        this.chkHold.Text = "Hold key (don’t tap it)";
        this.chkHold.UseVisualStyleBackColor = true;
        // 
        // Form1
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(340, 240);
        this.Controls.Add(this.chkHold);
        this.Controls.Add(this.chkScanCode);
        this.Controls.Add(this.lblHotkey);
        this.Controls.Add(this.lblStatus);
        this.Controls.Add(this.btnToggle);
        this.Controls.Add(this.numInterval);
        this.Controls.Add(this.lblInterval);
        this.Controls.Add(this.txtKey);
        this.Controls.Add(this.lblKey);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "Form1";
        this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text = "Key Auto Presser";
        ((System.ComponentModel.ISupportInitialize)(this.numInterval)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion
}
