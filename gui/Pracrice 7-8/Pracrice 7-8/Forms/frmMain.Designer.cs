
namespace Pracrice_7_8.Forms
{
    partial class frmMain
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
            this.nameLabel = new DevExpress.XtraEditors.LabelControl();
            this.edit_button = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.finish_button = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.idColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.executorColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.authorColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.contactColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.typeColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.priorityColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dataColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.contractColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dtCreatedColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dtDeadlineColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dtFinishedColumn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.editButton = new DevExpress.XtraGrid.Columns.GridColumn();
            this.finishButton = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            ((System.ComponentModel.ISupportInitialize)(this.edit_button)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.finish_button)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            this.nameLabel.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nameLabel.Appearance.Options.UseFont = true;
            this.nameLabel.Location = new System.Drawing.Point(14, 14);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(0, 21);
            this.nameLabel.TabIndex = 1;
            // 
            // edit_button
            // 
            this.edit_button.AccessibleDescription = "Изменить";
            this.edit_button.Appearance.BackColor = System.Drawing.SystemColors.Highlight;
            this.edit_button.Appearance.Options.UseBackColor = true;
            this.edit_button.AutoHeight = false;
            this.edit_button.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.edit_button.Name = "edit_button";
            // 
            // finish_button
            // 
            this.finish_button.AutoHeight = false;
            this.finish_button.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.finish_button.Name = "finish_button";
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.idColumn,
            this.executorColumn,
            this.authorColumn,
            this.contactColumn,
            this.typeColumn,
            this.priorityColumn,
            this.dataColumn,
            this.contractColumn,
            this.dtCreatedColumn,
            this.dtDeadlineColumn,
            this.dtFinishedColumn,
            this.editButton,
            this.finishButton});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsCustomization.AllowSort = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // idColumn
            // 
            this.idColumn.Caption = "Номер задания";
            this.idColumn.FieldName = "id";
            this.idColumn.Name = "idColumn";
            this.idColumn.OptionsColumn.ReadOnly = true;
            this.idColumn.Visible = true;
            this.idColumn.VisibleIndex = 0;
            // 
            // executorColumn
            // 
            this.executorColumn.Caption = "Исполнитель";
            this.executorColumn.Name = "executorColumn";
            this.executorColumn.OptionsColumn.ReadOnly = true;
            this.executorColumn.Visible = true;
            this.executorColumn.VisibleIndex = 1;
            // 
            // authorColumn
            // 
            this.authorColumn.Caption = "Автор";
            this.authorColumn.Name = "authorColumn";
            this.authorColumn.OptionsColumn.ReadOnly = true;
            this.authorColumn.Visible = true;
            this.authorColumn.VisibleIndex = 2;
            // 
            // contactColumn
            // 
            this.contactColumn.Caption = "Контактное лицо";
            this.contactColumn.Name = "contactColumn";
            this.contactColumn.OptionsColumn.ReadOnly = true;
            this.contactColumn.Visible = true;
            this.contactColumn.VisibleIndex = 3;
            // 
            // typeColumn
            // 
            this.typeColumn.Caption = "Тип задания";
            this.typeColumn.Name = "typeColumn";
            this.typeColumn.OptionsColumn.ReadOnly = true;
            this.typeColumn.Visible = true;
            this.typeColumn.VisibleIndex = 4;
            // 
            // priorityColumn
            // 
            this.priorityColumn.Caption = "Приоритет";
            this.priorityColumn.Name = "priorityColumn";
            this.priorityColumn.OptionsColumn.ReadOnly = true;
            this.priorityColumn.Visible = true;
            this.priorityColumn.VisibleIndex = 5;
            // 
            // dataColumn
            // 
            this.dataColumn.Caption = "Описание";
            this.dataColumn.Name = "dataColumn";
            this.dataColumn.OptionsColumn.ReadOnly = true;
            this.dataColumn.Visible = true;
            this.dataColumn.VisibleIndex = 6;
            // 
            // contractColumn
            // 
            this.contractColumn.Caption = "Контракт";
            this.contractColumn.Name = "contractColumn";
            this.contractColumn.OptionsColumn.ReadOnly = true;
            this.contractColumn.Visible = true;
            this.contractColumn.VisibleIndex = 7;
            // 
            // dtCreatedColumn
            // 
            this.dtCreatedColumn.Caption = "Дата создания";
            this.dtCreatedColumn.Name = "dtCreatedColumn";
            this.dtCreatedColumn.OptionsColumn.ReadOnly = true;
            this.dtCreatedColumn.Visible = true;
            this.dtCreatedColumn.VisibleIndex = 8;
            // 
            // dtDeadlineColumn
            // 
            this.dtDeadlineColumn.Caption = "Дедлайн";
            this.dtDeadlineColumn.Name = "dtDeadlineColumn";
            this.dtDeadlineColumn.OptionsColumn.ReadOnly = true;
            this.dtDeadlineColumn.Visible = true;
            this.dtDeadlineColumn.VisibleIndex = 9;
            // 
            // dtFinishedColumn
            // 
            this.dtFinishedColumn.Caption = "Дата выполнения";
            this.dtFinishedColumn.Name = "dtFinishedColumn";
            this.dtFinishedColumn.Visible = true;
            this.dtFinishedColumn.VisibleIndex = 10;
            // 
            // editButton
            // 
            this.editButton.ColumnEdit = this.edit_button;
            this.editButton.Name = "editButton";
            this.editButton.Visible = true;
            this.editButton.VisibleIndex = 11;
            // 
            // finishButton
            // 
            this.finishButton.ColumnEdit = this.finish_button;
            this.finishButton.Name = "finishButton";
            this.finishButton.Visible = true;
            this.finishButton.VisibleIndex = 12;
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(14, 201);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.edit_button,
            this.finish_button});
            this.gridControl1.Size = new System.Drawing.Size(876, 354);
            this.gridControl1.TabIndex = 2;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // frmMain
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(902, 567);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.nameLabel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "frmMain";
            this.Text = "Главное окно";
            ((System.ComponentModel.ISupportInitialize)(this.edit_button)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.finish_button)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl nameLabel;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit edit_button;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit finish_button;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn idColumn;
        private DevExpress.XtraGrid.Columns.GridColumn executorColumn;
        private DevExpress.XtraGrid.Columns.GridColumn authorColumn;
        private DevExpress.XtraGrid.Columns.GridColumn contactColumn;
        private DevExpress.XtraGrid.Columns.GridColumn typeColumn;
        private DevExpress.XtraGrid.Columns.GridColumn priorityColumn;
        private DevExpress.XtraGrid.Columns.GridColumn dataColumn;
        private DevExpress.XtraGrid.Columns.GridColumn contractColumn;
        private DevExpress.XtraGrid.Columns.GridColumn dtCreatedColumn;
        private DevExpress.XtraGrid.Columns.GridColumn dtDeadlineColumn;
        private DevExpress.XtraGrid.Columns.GridColumn dtFinishedColumn;
        private DevExpress.XtraGrid.Columns.GridColumn editButton;
        private DevExpress.XtraGrid.Columns.GridColumn finishButton;
        private DevExpress.XtraGrid.GridControl gridControl1;
    }
}