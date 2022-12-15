
namespace Pracrice_7_8.Forms
{
    partial class frmEditTask
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.table = new System.Windows.Forms.TableLayoutPanel();
            this.finishButton = new DevExpress.XtraEditors.SimpleButton();
            this.changeTaskButton = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // table
            // 
            this.table.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.table.AutoSize = true;
            this.table.ColumnCount = 2;
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.table.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.table.Location = new System.Drawing.Point(12, 12);
            this.table.Name = "table";
            this.table.RowCount = 1;
            this.table.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.table.Size = new System.Drawing.Size(334, 350);
            this.table.TabIndex = 0;
            // 
            // finishButton
            // 
            this.finishButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.finishButton.Location = new System.Drawing.Point(12, 432);
            this.finishButton.Name = "finishButton";
            this.finishButton.Size = new System.Drawing.Size(334, 23);
            this.finishButton.TabIndex = 1;
            this.finishButton.Text = "Завершить";
            this.finishButton.Click += new System.EventHandler(this.finishButton_Click);
            // 
            // changeTaskButton
            // 
            this.changeTaskButton.Location = new System.Drawing.Point(12, 403);
            this.changeTaskButton.Name = "changeTaskButton";
            this.changeTaskButton.Size = new System.Drawing.Size(334, 23);
            this.changeTaskButton.TabIndex = 2;
            this.changeTaskButton.Text = "Изменить";
            this.changeTaskButton.Click += new System.EventHandler(this.changeTaskButton_Click);
            // 
            // frmEditTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 467);
            this.Controls.Add(this.changeTaskButton);
            this.Controls.Add(this.finishButton);
            this.Controls.Add(this.table);
            this.Name = "frmEditTask";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmEditTask";
            this.Load += new System.EventHandler(this.frmEditTask_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel table;
        private DevExpress.XtraEditors.SimpleButton finishButton;
        private DevExpress.XtraEditors.SimpleButton changeTaskButton;
    }
}