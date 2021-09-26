using WInterop.Winforms;

namespace WinFormsDirect2D
{
    partial class MainForm
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
            this.d2DPanel = new WInterop.Winforms.D2DPanel();
            this.SuspendLayout();
            // 
            // d2DPanel
            // 
            this.d2DPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.d2DPanel.Location = new System.Drawing.Point(21, 26);
            this.d2DPanel.Name = "d2DPanel";
            this.d2DPanel.Size = new System.Drawing.Size(936, 565);
            this.d2DPanel.TabIndex = 0;
            this.d2DPanel.D2DPaint += new System.EventHandler<WInterop.Winforms.D2DPaintEventArgs>(this.D2DPanel_D2DPaint);
            this.d2DPanel.CreateD2DResources += new System.EventHandler<WInterop.Winforms.D2DPaintEventArgs>(this.D2DPanel_CreateD2DResources);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(979, 629);
            this.Controls.Add(this.d2DPanel);
            this.Name = "MainForm";
            this.Text = "D2D/DWrite Demo";
            this.ResumeLayout(false);

        }

        #endregion

        private D2DPanel d2DPanel;
    }
}

