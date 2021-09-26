using WInterop.Winforms;

namespace WinFormsDirect2D
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.dxControl = new WInterop.Winforms.Direct2DControl();
            this.SuspendLayout();
            // 
            // dxControl
            // 
            this.dxControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dxControl.Location = new System.Drawing.Point(21, 26);
            this.dxControl.Name = "dxControl";
            this.dxControl.Size = new System.Drawing.Size(757, 386);
            this.dxControl.TabIndex = 0;
            this.dxControl.DirectXPaint += new System.EventHandler<WInterop.Winforms.DirectXPaintEventArgs>(this.dxControl_DirectXEvent);
            this.dxControl.CreateDirectXResources += new System.EventHandler<WInterop.Winforms.DirectXPaintEventArgs>(this.dxControl_CreateDirectXResources);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dxControl);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Direct2DControl dxControl;
    }
}

