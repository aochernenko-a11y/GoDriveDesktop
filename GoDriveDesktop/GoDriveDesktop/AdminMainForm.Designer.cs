namespace GoDriveDesktop
{
    partial class AdminMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminMainForm));
            tableLayoutPanel1 = new TableLayoutPanel();
            roundedPanelMenu = new RoundedPanel();
            buttonChangeUser = new RoundedButton();
            flowLayoutPanel2 = new FlowLayoutPanel();
            buttonFuel = new RoundedButton();
            buttonProducts = new RoundedButton();
            buttonEmployees = new RoundedButton();
            buttonPromotions = new RoundedButton();
            buttonSalesHistory = new RoundedButton();
            buttonReports = new RoundedButton();
            buttonExit = new RoundedButton();
            roundedPanelContent = new RoundedPanel();
            tableLayoutPanel1.SuspendLayout();
            roundedPanelMenu.SuspendLayout();
            flowLayoutPanel2.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 300F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Controls.Add(roundedPanelMenu, 0, 0);
            tableLayoutPanel1.Controls.Add(roundedPanelContent, 1, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.Padding = new Padding(5);
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1006, 721);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // roundedPanelMenu
            // 
            roundedPanelMenu.BackColor = SystemColors.Window;
            roundedPanelMenu.BorderColor = Color.Silver;
            roundedPanelMenu.BorderThickness = 1;
            roundedPanelMenu.Controls.Add(buttonChangeUser);
            roundedPanelMenu.Controls.Add(flowLayoutPanel2);
            roundedPanelMenu.Controls.Add(buttonExit);
            roundedPanelMenu.Dock = DockStyle.Fill;
            roundedPanelMenu.Location = new Point(8, 8);
            roundedPanelMenu.Name = "roundedPanelMenu";
            roundedPanelMenu.Padding = new Padding(10);
            roundedPanelMenu.Size = new Size(294, 705);
            roundedPanelMenu.TabIndex = 0;
            // 
            // buttonChangeUser
            // 
            buttonChangeUser.BackColor = SystemColors.MenuHighlight;
            buttonChangeUser.Dock = DockStyle.Bottom;
            buttonChangeUser.FlatAppearance.BorderSize = 0;
            buttonChangeUser.FlatStyle = FlatStyle.Flat;
            buttonChangeUser.Font = new Font("Calibri", 16.2F, FontStyle.Bold);
            buttonChangeUser.ForeColor = Color.White;
            buttonChangeUser.Location = new Point(10, 575);
            buttonChangeUser.Name = "buttonChangeUser";
            buttonChangeUser.Size = new Size(274, 60);
            buttonChangeUser.TabIndex = 6;
            buttonChangeUser.TabStop = false;
            buttonChangeUser.Text = "Вийти з акаунта";
            buttonChangeUser.UseVisualStyleBackColor = false;
            buttonChangeUser.Click += buttonChangeUser_Click;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.Controls.Add(buttonFuel);
            flowLayoutPanel2.Controls.Add(buttonProducts);
            flowLayoutPanel2.Controls.Add(buttonEmployees);
            flowLayoutPanel2.Controls.Add(buttonPromotions);
            flowLayoutPanel2.Controls.Add(buttonSalesHistory);
            flowLayoutPanel2.Controls.Add(buttonReports);
            flowLayoutPanel2.Dock = DockStyle.Top;
            flowLayoutPanel2.Location = new Point(10, 10);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(274, 625);
            flowLayoutPanel2.TabIndex = 6;
            // 
            // buttonFuel
            // 
            buttonFuel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            buttonFuel.BackColor = SystemColors.MenuHighlight;
            buttonFuel.FlatAppearance.BorderSize = 0;
            buttonFuel.FlatStyle = FlatStyle.Flat;
            buttonFuel.Font = new Font("Calibri", 16.2F, FontStyle.Bold, GraphicsUnit.Point, 204);
            buttonFuel.ForeColor = Color.White;
            buttonFuel.Location = new Point(3, 3);
            buttonFuel.Name = "buttonFuel";
            buttonFuel.Size = new Size(268, 60);
            buttonFuel.TabIndex = 0;
            buttonFuel.TabStop = false;
            buttonFuel.Text = "Пальне";
            buttonFuel.UseVisualStyleBackColor = false;
            buttonFuel.Click += buttonFuel_Click;
            // 
            // buttonProducts
            // 
            buttonProducts.BackColor = SystemColors.MenuHighlight;
            buttonProducts.Dock = DockStyle.Top;
            buttonProducts.FlatAppearance.BorderSize = 0;
            buttonProducts.FlatStyle = FlatStyle.Flat;
            buttonProducts.Font = new Font("Calibri", 16.2F, FontStyle.Bold);
            buttonProducts.ForeColor = Color.White;
            buttonProducts.Location = new Point(3, 69);
            buttonProducts.Name = "buttonProducts";
            buttonProducts.Size = new Size(268, 60);
            buttonProducts.TabIndex = 1;
            buttonProducts.TabStop = false;
            buttonProducts.Text = "Товари";
            buttonProducts.UseVisualStyleBackColor = false;
            buttonProducts.Click += buttonProducts_Click;
            // 
            // buttonEmployees
            // 
            buttonEmployees.BackColor = SystemColors.MenuHighlight;
            buttonEmployees.Dock = DockStyle.Top;
            buttonEmployees.FlatAppearance.BorderSize = 0;
            buttonEmployees.FlatStyle = FlatStyle.Flat;
            buttonEmployees.Font = new Font("Calibri", 16.2F, FontStyle.Bold);
            buttonEmployees.ForeColor = Color.White;
            buttonEmployees.Location = new Point(3, 135);
            buttonEmployees.Name = "buttonEmployees";
            buttonEmployees.Size = new Size(268, 60);
            buttonEmployees.TabIndex = 2;
            buttonEmployees.TabStop = false;
            buttonEmployees.Text = "Працівники";
            buttonEmployees.UseVisualStyleBackColor = false;
            buttonEmployees.Click += buttonEmployees_Click;
            // 
            // buttonPromotions
            // 
            buttonPromotions.BackColor = SystemColors.MenuHighlight;
            buttonPromotions.Dock = DockStyle.Top;
            buttonPromotions.FlatAppearance.BorderSize = 0;
            buttonPromotions.FlatStyle = FlatStyle.Flat;
            buttonPromotions.Font = new Font("Calibri", 16.2F, FontStyle.Bold);
            buttonPromotions.ForeColor = Color.White;
            buttonPromotions.Location = new Point(3, 201);
            buttonPromotions.Name = "buttonPromotions";
            buttonPromotions.Size = new Size(268, 60);
            buttonPromotions.TabIndex = 3;
            buttonPromotions.TabStop = false;
            buttonPromotions.Text = "Акції";
            buttonPromotions.UseVisualStyleBackColor = false;
            buttonPromotions.Click += buttonPromotions_Click;
            // 
            // buttonSalesHistory
            // 
            buttonSalesHistory.BackColor = SystemColors.MenuHighlight;
            buttonSalesHistory.Dock = DockStyle.Top;
            buttonSalesHistory.FlatAppearance.BorderSize = 0;
            buttonSalesHistory.FlatStyle = FlatStyle.Flat;
            buttonSalesHistory.Font = new Font("Calibri", 16.2F, FontStyle.Bold);
            buttonSalesHistory.ForeColor = Color.White;
            buttonSalesHistory.Location = new Point(3, 267);
            buttonSalesHistory.Name = "buttonSalesHistory";
            buttonSalesHistory.Size = new Size(268, 60);
            buttonSalesHistory.TabIndex = 4;
            buttonSalesHistory.TabStop = false;
            buttonSalesHistory.Text = "Історія продажів";
            buttonSalesHistory.UseVisualStyleBackColor = false;
            buttonSalesHistory.Click += buttonSalesHistory_Click;
            // 
            // buttonReports
            // 
            buttonReports.BackColor = SystemColors.MenuHighlight;
            buttonReports.Dock = DockStyle.Top;
            buttonReports.FlatAppearance.BorderSize = 0;
            buttonReports.FlatStyle = FlatStyle.Flat;
            buttonReports.Font = new Font("Calibri", 16.2F, FontStyle.Bold);
            buttonReports.ForeColor = Color.White;
            buttonReports.Location = new Point(3, 333);
            buttonReports.Name = "buttonReports";
            buttonReports.Size = new Size(268, 60);
            buttonReports.TabIndex = 5;
            buttonReports.TabStop = false;
            buttonReports.Text = "Звіти";
            buttonReports.UseVisualStyleBackColor = false;
            buttonReports.Click += buttonReports_Click;
            // 
            // buttonExit
            // 
            buttonExit.BackColor = SystemColors.MenuHighlight;
            buttonExit.Dock = DockStyle.Bottom;
            buttonExit.FlatAppearance.BorderSize = 0;
            buttonExit.FlatStyle = FlatStyle.Flat;
            buttonExit.Font = new Font("Calibri", 16.2F, FontStyle.Bold);
            buttonExit.ForeColor = Color.White;
            buttonExit.Location = new Point(10, 635);
            buttonExit.Name = "buttonExit";
            buttonExit.Size = new Size(274, 60);
            buttonExit.TabIndex = 7;
            buttonExit.TabStop = false;
            buttonExit.Text = "Закрити програму";
            buttonExit.UseVisualStyleBackColor = false;
            buttonExit.Click += buttonExit_Click;
            // 
            // roundedPanelContent
            // 
            roundedPanelContent.BackColor = SystemColors.Window;
            roundedPanelContent.BorderColor = Color.Silver;
            roundedPanelContent.BorderThickness = 1;
            roundedPanelContent.Dock = DockStyle.Fill;
            roundedPanelContent.Location = new Point(308, 8);
            roundedPanelContent.Name = "roundedPanelContent";
            roundedPanelContent.Padding = new Padding(10);
            roundedPanelContent.Size = new Size(690, 705);
            roundedPanelContent.TabIndex = 1;
            // 
            // AdminMainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1006, 721);
            Controls.Add(tableLayoutPanel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(1024, 768);
            Name = "AdminMainForm";
            Text = "Панель керування";
            Load += AdminMainForm_Load;
            tableLayoutPanel1.ResumeLayout(false);
            roundedPanelMenu.ResumeLayout(false);
            flowLayoutPanel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private RoundedPanel roundedPanelMenu;
        private RoundedButton buttonReports;
        private RoundedButton buttonEmployees;
        private RoundedButton buttonProducts;
        private RoundedButton buttonFuel;
        private RoundedPanel roundedPanelContent;
        private RoundedButton buttonExit;
        private RoundedButton buttonSalesHistory;
        private FlowLayoutPanel flowLayoutPanel2;
        private RoundedButton buttonChangeUser;
        private RoundedButton buttonPromotions;
    }
}