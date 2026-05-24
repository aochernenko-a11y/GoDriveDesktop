namespace GoDriveDesktop
{
    partial class CashierMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CashierMainForm));
            mainLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel1 = new TableLayoutPanel();
            buttonChangeUser = new Button();
            buttonCloseShift = new Button();
            buttonCashierSalesHistory = new Button();
            buttonShiftReport = new Button();
            buttonExit = new Button();
            tableLayoutPanel2 = new TableLayoutPanel();
            tableLayoutPanel3 = new TableLayoutPanel();
            productsSectionPanel = new RoundedPanel();
            pictureBox1 = new PictureBox();
            productsCardsPanel = new FlowLayoutPanel();
            categoryPanel = new Panel();
            textBoxProductSearch = new TextBox();
            tableLayoutPanel5 = new TableLayoutPanel();
            fuelingModePanel = new RoundedPanel();
            buttonAddFuelToReceipt = new RoundedButton();
            textBoxSpecificAmount = new TextBox();
            labelFuelingCounter = new Label();
            buttonSpecificAmount = new RoundedButton();
            buttonFillFull = new RoundedButton();
            promotionsPanel = new RoundedPanel();
            panel1 = new Panel();
            labelPromoDot3 = new Label();
            labelPromoDot2 = new Label();
            labelPromoDot1 = new Label();
            pictureBoxPromotion = new PictureBox();
            tableLayoutPanel4 = new TableLayoutPanel();
            purchaseListPanel = new RoundedPanel();
            buttonRemovePurchaseItem = new RoundedButton();
            dataGridViewPurchaseList = new DataGridView();
            labelPurchaseListTitle = new Label();
            paymentPanel = new RoundedPanel();
            buttonPaymentCard = new RoundedButton();
            buttonPaymentCash = new RoundedButton();
            labelTotalPrice = new Label();
            labelTotalText = new Label();
            buttonCancelSale = new RoundedButton();
            buttonConfirmSale = new RoundedButton();
            columnsPanel = new RoundedPanel();
            buttonColumn5 = new RoundedButton();
            buttonColumn4 = new RoundedButton();
            buttonColumn3 = new RoundedButton();
            buttonColumn2 = new RoundedButton();
            buttonColumn1 = new RoundedButton();
            fuelTypesPanel = new FlowLayoutPanel();
            mainLayoutPanel.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            productsSectionPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            tableLayoutPanel5.SuspendLayout();
            fuelingModePanel.SuspendLayout();
            promotionsPanel.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPromotion).BeginInit();
            tableLayoutPanel4.SuspendLayout();
            purchaseListPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewPurchaseList).BeginInit();
            paymentPanel.SuspendLayout();
            columnsPanel.SuspendLayout();
            SuspendLayout();
            // 
            // mainLayoutPanel
            // 
            mainLayoutPanel.ColumnCount = 1;
            mainLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainLayoutPanel.Controls.Add(tableLayoutPanel1, 0, 0);
            mainLayoutPanel.Controls.Add(tableLayoutPanel2, 0, 1);
            mainLayoutPanel.Dock = DockStyle.Fill;
            mainLayoutPanel.Location = new Point(0, 0);
            mainLayoutPanel.Name = "mainLayoutPanel";
            mainLayoutPanel.RowCount = 2;
            mainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 100F));
            mainLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayoutPanel.Size = new Size(1006, 721);
            mainLayoutPanel.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 5;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel1.Controls.Add(buttonChangeUser, 3, 0);
            tableLayoutPanel1.Controls.Add(buttonCloseShift, 2, 0);
            tableLayoutPanel1.Controls.Add(buttonCashierSalesHistory, 1, 0);
            tableLayoutPanel1.Controls.Add(buttonShiftReport, 0, 0);
            tableLayoutPanel1.Controls.Add(buttonExit, 4, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Margin = new Padding(0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1006, 100);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // buttonChangeUser
            // 
            buttonChangeUser.Dock = DockStyle.Fill;
            buttonChangeUser.Font = new Font("Calibri", 18F);
            buttonChangeUser.Location = new Point(606, 3);
            buttonChangeUser.Name = "buttonChangeUser";
            buttonChangeUser.Size = new Size(195, 94);
            buttonChangeUser.TabIndex = 14;
            buttonChangeUser.Text = "Змінити користувача";
            buttonChangeUser.UseVisualStyleBackColor = true;
            buttonChangeUser.Click += buttonChangeUser_Click;
            // 
            // buttonCloseShift
            // 
            buttonCloseShift.Dock = DockStyle.Fill;
            buttonCloseShift.Font = new Font("Calibri", 18F);
            buttonCloseShift.Location = new Point(405, 3);
            buttonCloseShift.Name = "buttonCloseShift";
            buttonCloseShift.Size = new Size(195, 94);
            buttonCloseShift.TabIndex = 13;
            buttonCloseShift.Text = "Зміна";
            buttonCloseShift.UseVisualStyleBackColor = true;
            buttonCloseShift.Click += buttonCloseShift_Click;
            // 
            // buttonCashierSalesHistory
            // 
            buttonCashierSalesHistory.Dock = DockStyle.Fill;
            buttonCashierSalesHistory.Font = new Font("Calibri", 18F);
            buttonCashierSalesHistory.Location = new Point(204, 3);
            buttonCashierSalesHistory.Name = "buttonCashierSalesHistory";
            buttonCashierSalesHistory.Size = new Size(195, 94);
            buttonCashierSalesHistory.TabIndex = 12;
            buttonCashierSalesHistory.Text = "Історія продажів\n";
            buttonCashierSalesHistory.UseVisualStyleBackColor = true;
            buttonCashierSalesHistory.Click += buttonCashierSalesHistory_Click;
            // 
            // buttonShiftReport
            // 
            buttonShiftReport.Dock = DockStyle.Fill;
            buttonShiftReport.Font = new Font("Calibri", 18F);
            buttonShiftReport.Location = new Point(3, 3);
            buttonShiftReport.Name = "buttonShiftReport";
            buttonShiftReport.Size = new Size(195, 94);
            buttonShiftReport.TabIndex = 12;
            buttonShiftReport.Text = "Звіт";
            buttonShiftReport.UseVisualStyleBackColor = true;
            buttonShiftReport.Click += buttonShiftReport_Click;
            // 
            // buttonExit
            // 
            buttonExit.BackColor = SystemColors.Window;
            buttonExit.Dock = DockStyle.Fill;
            buttonExit.Font = new Font("Calibri", 18F);
            buttonExit.Location = new Point(807, 3);
            buttonExit.Name = "buttonExit";
            buttonExit.Size = new Size(196, 94);
            buttonExit.TabIndex = 15;
            buttonExit.Text = "Закрити програму";
            buttonExit.UseVisualStyleBackColor = false;
            buttonExit.Click += buttonExit_Click;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 3;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 44F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 28F));
            tableLayoutPanel2.Controls.Add(tableLayoutPanel3, 1, 0);
            tableLayoutPanel2.Controls.Add(tableLayoutPanel4, 2, 0);
            tableLayoutPanel2.Controls.Add(columnsPanel, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(0, 100);
            tableLayoutPanel2.Margin = new Padding(0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Size = new Size(1006, 621);
            tableLayoutPanel2.TabIndex = 2;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(productsSectionPanel, 0, 1);
            tableLayoutPanel3.Controls.Add(tableLayoutPanel5, 0, 0);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(281, 0);
            tableLayoutPanel3.Margin = new Padding(0);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            tableLayoutPanel3.Size = new Size(442, 621);
            tableLayoutPanel3.TabIndex = 0;
            // 
            // productsSectionPanel
            // 
            productsSectionPanel.BackColor = SystemColors.Window;
            productsSectionPanel.BorderColor = Color.Silver;
            productsSectionPanel.BorderThickness = 1;
            productsSectionPanel.Controls.Add(pictureBox1);
            productsSectionPanel.Controls.Add(productsCardsPanel);
            productsSectionPanel.Controls.Add(categoryPanel);
            productsSectionPanel.Controls.Add(textBoxProductSearch);
            productsSectionPanel.Dock = DockStyle.Fill;
            productsSectionPanel.Location = new Point(3, 251);
            productsSectionPanel.Name = "productsSectionPanel";
            productsSectionPanel.Size = new Size(436, 367);
            productsSectionPanel.TabIndex = 1;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = Properties.Resources.IconSearch;
            pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox1.Location = new Point(18, 11);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(40, 40);
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // productsCardsPanel
            // 
            productsCardsPanel.AutoScroll = true;
            productsCardsPanel.Location = new Point(185, 57);
            productsCardsPanel.Name = "productsCardsPanel";
            productsCardsPanel.Size = new Size(241, 301);
            productsCardsPanel.TabIndex = 2;
            // 
            // categoryPanel
            // 
            categoryPanel.Location = new Point(10, 57);
            categoryPanel.Name = "categoryPanel";
            categoryPanel.Size = new Size(176, 301);
            categoryPanel.TabIndex = 1;
            // 
            // textBoxProductSearch
            // 
            textBoxProductSearch.Font = new Font("Calibri", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            textBoxProductSearch.Location = new Point(64, 11);
            textBoxProductSearch.Name = "textBoxProductSearch";
            textBoxProductSearch.Size = new Size(352, 40);
            textBoxProductSearch.TabIndex = 0;
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.ColumnCount = 2;
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 55F));
            tableLayoutPanel5.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
            tableLayoutPanel5.Controls.Add(fuelingModePanel, 0, 0);
            tableLayoutPanel5.Controls.Add(promotionsPanel, 1, 0);
            tableLayoutPanel5.Dock = DockStyle.Fill;
            tableLayoutPanel5.Location = new Point(0, 0);
            tableLayoutPanel5.Margin = new Padding(0);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 1;
            tableLayoutPanel5.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel5.Size = new Size(442, 248);
            tableLayoutPanel5.TabIndex = 2;
            // 
            // fuelingModePanel
            // 
            fuelingModePanel.BackColor = SystemColors.Window;
            fuelingModePanel.BorderColor = Color.Silver;
            fuelingModePanel.BorderThickness = 1;
            fuelingModePanel.Controls.Add(buttonAddFuelToReceipt);
            fuelingModePanel.Controls.Add(textBoxSpecificAmount);
            fuelingModePanel.Controls.Add(labelFuelingCounter);
            fuelingModePanel.Controls.Add(buttonSpecificAmount);
            fuelingModePanel.Controls.Add(buttonFillFull);
            fuelingModePanel.Dock = DockStyle.Fill;
            fuelingModePanel.Location = new Point(3, 3);
            fuelingModePanel.Name = "fuelingModePanel";
            fuelingModePanel.Size = new Size(237, 242);
            fuelingModePanel.TabIndex = 0;
            // 
            // buttonAddFuelToReceipt
            // 
            buttonAddFuelToReceipt.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            buttonAddFuelToReceipt.BackColor = SystemColors.MenuHighlight;
            buttonAddFuelToReceipt.FlatStyle = FlatStyle.Flat;
            buttonAddFuelToReceipt.Font = new Font("Calibri", 9F, FontStyle.Bold);
            buttonAddFuelToReceipt.ForeColor = Color.White;
            buttonAddFuelToReceipt.Location = new Point(10, 198);
            buttonAddFuelToReceipt.Name = "buttonAddFuelToReceipt";
            buttonAddFuelToReceipt.Size = new Size(211, 29);
            buttonAddFuelToReceipt.TabIndex = 6;
            buttonAddFuelToReceipt.TabStop = false;
            buttonAddFuelToReceipt.Text = "До чека";
            buttonAddFuelToReceipt.UseVisualStyleBackColor = true;
            buttonAddFuelToReceipt.Click += buttonAddFuelToReceipt_Click;
            // 
            // textBoxSpecificAmount
            // 
            textBoxSpecificAmount.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBoxSpecificAmount.Location = new Point(126, 13);
            textBoxSpecificAmount.Name = "textBoxSpecificAmount";
            textBoxSpecificAmount.Size = new Size(95, 27);
            textBoxSpecificAmount.TabIndex = 5;
            // 
            // labelFuelingCounter
            // 
            labelFuelingCounter.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            labelFuelingCounter.Font = new Font("Calibri", 48F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelFuelingCounter.Location = new Point(79, 98);
            labelFuelingCounter.Name = "labelFuelingCounter";
            labelFuelingCounter.Size = new Size(142, 97);
            labelFuelingCounter.TabIndex = 4;
            labelFuelingCounter.Text = "0 л";
            labelFuelingCounter.TextAlign = ContentAlignment.MiddleRight;
            // 
            // buttonSpecificAmount
            // 
            buttonSpecificAmount.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            buttonSpecificAmount.BackColor = SystemColors.MenuHighlight;
            buttonSpecificAmount.BorderRadius = 3;
            buttonSpecificAmount.FlatAppearance.BorderSize = 0;
            buttonSpecificAmount.FlatAppearance.MouseDownBackColor = SystemColors.Window;
            buttonSpecificAmount.FlatAppearance.MouseOverBackColor = Color.FromArgb(150, 210, 180);
            buttonSpecificAmount.FlatStyle = FlatStyle.Flat;
            buttonSpecificAmount.Font = new Font("Calibri", 9F, FontStyle.Bold);
            buttonSpecificAmount.ForeColor = Color.White;
            buttonSpecificAmount.Location = new Point(127, 46);
            buttonSpecificAmount.Name = "buttonSpecificAmount";
            buttonSpecificAmount.Size = new Size(94, 29);
            buttonSpecificAmount.TabIndex = 5;
            buttonSpecificAmount.TabStop = false;
            buttonSpecificAmount.Text = "Заправити";
            buttonSpecificAmount.UseVisualStyleBackColor = false;
            buttonSpecificAmount.Click += buttonSpecificAmount_Click;
            // 
            // buttonFillFull
            // 
            buttonFillFull.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            buttonFillFull.BackColor = SystemColors.MenuHighlight;
            buttonFillFull.FlatAppearance.BorderSize = 0;
            buttonFillFull.FlatAppearance.MouseDownBackColor = SystemColors.Window;
            buttonFillFull.FlatAppearance.MouseOverBackColor = Color.FromArgb(150, 210, 180);
            buttonFillFull.FlatStyle = FlatStyle.Flat;
            buttonFillFull.Font = new Font("Calibri", 9F, FontStyle.Bold, GraphicsUnit.Point, 204);
            buttonFillFull.ForeColor = Color.White;
            buttonFillFull.Location = new Point(10, 12);
            buttonFillFull.Name = "buttonFillFull";
            buttonFillFull.Size = new Size(94, 63);
            buttonFillFull.TabIndex = 5;
            buttonFillFull.TabStop = false;
            buttonFillFull.Text = "До повного";
            buttonFillFull.UseVisualStyleBackColor = false;
            buttonFillFull.Click += buttonFillFull_Click;
            // 
            // promotionsPanel
            // 
            promotionsPanel.BackColor = SystemColors.Window;
            promotionsPanel.BorderColor = Color.Silver;
            promotionsPanel.BorderThickness = 1;
            promotionsPanel.Controls.Add(panel1);
            promotionsPanel.Controls.Add(pictureBoxPromotion);
            promotionsPanel.Dock = DockStyle.Fill;
            promotionsPanel.Location = new Point(246, 3);
            promotionsPanel.Name = "promotionsPanel";
            promotionsPanel.Size = new Size(193, 242);
            promotionsPanel.TabIndex = 1;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Bottom;
            panel1.Controls.Add(labelPromoDot3);
            panel1.Controls.Add(labelPromoDot2);
            panel1.Controls.Add(labelPromoDot1);
            panel1.Location = new Point(57, 214);
            panel1.Name = "panel1";
            panel1.Size = new Size(84, 25);
            panel1.TabIndex = 1;
            // 
            // labelPromoDot3
            // 
            labelPromoDot3.AutoSize = true;
            labelPromoDot3.Location = new Point(58, 0);
            labelPromoDot3.Name = "labelPromoDot3";
            labelPromoDot3.Size = new Size(18, 20);
            labelPromoDot3.TabIndex = 2;
            labelPromoDot3.Text = "●";
            // 
            // labelPromoDot2
            // 
            labelPromoDot2.AutoSize = true;
            labelPromoDot2.Location = new Point(34, 0);
            labelPromoDot2.Name = "labelPromoDot2";
            labelPromoDot2.Size = new Size(18, 20);
            labelPromoDot2.TabIndex = 1;
            labelPromoDot2.Text = "●";
            // 
            // labelPromoDot1
            // 
            labelPromoDot1.AutoSize = true;
            labelPromoDot1.Location = new Point(10, 0);
            labelPromoDot1.Name = "labelPromoDot1";
            labelPromoDot1.Size = new Size(18, 20);
            labelPromoDot1.TabIndex = 0;
            labelPromoDot1.Text = "●";
            // 
            // pictureBoxPromotion
            // 
            pictureBoxPromotion.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            pictureBoxPromotion.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBoxPromotion.Image = Properties.Resources.Promo1;
            pictureBoxPromotion.Location = new Point(12, 9);
            pictureBoxPromotion.Name = "pictureBoxPromotion";
            pictureBoxPromotion.Size = new Size(172, 202);
            pictureBoxPromotion.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxPromotion.TabIndex = 0;
            pictureBoxPromotion.TabStop = false;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 1;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel4.Controls.Add(purchaseListPanel, 0, 0);
            tableLayoutPanel4.Controls.Add(paymentPanel, 0, 1);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(723, 0);
            tableLayoutPanel4.Margin = new Padding(0);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 2;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 60F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            tableLayoutPanel4.Size = new Size(283, 621);
            tableLayoutPanel4.TabIndex = 1;
            // 
            // purchaseListPanel
            // 
            purchaseListPanel.BackColor = SystemColors.Window;
            purchaseListPanel.BorderColor = Color.Silver;
            purchaseListPanel.BorderThickness = 1;
            purchaseListPanel.Controls.Add(buttonRemovePurchaseItem);
            purchaseListPanel.Controls.Add(dataGridViewPurchaseList);
            purchaseListPanel.Controls.Add(labelPurchaseListTitle);
            purchaseListPanel.Dock = DockStyle.Fill;
            purchaseListPanel.Location = new Point(3, 3);
            purchaseListPanel.Margin = new Padding(3, 3, 9, 3);
            purchaseListPanel.Name = "purchaseListPanel";
            purchaseListPanel.Size = new Size(271, 366);
            purchaseListPanel.TabIndex = 0;
            // 
            // buttonRemovePurchaseItem
            // 
            buttonRemovePurchaseItem.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            buttonRemovePurchaseItem.BackColor = SystemColors.MenuHighlight;
            buttonRemovePurchaseItem.BorderRadius = 4;
            buttonRemovePurchaseItem.FlatAppearance.BorderSize = 0;
            buttonRemovePurchaseItem.FlatStyle = FlatStyle.Flat;
            buttonRemovePurchaseItem.Font = new Font("Calibri", 9F, FontStyle.Bold);
            buttonRemovePurchaseItem.ForeColor = Color.White;
            buttonRemovePurchaseItem.Location = new Point(51, 324);
            buttonRemovePurchaseItem.Name = "buttonRemovePurchaseItem";
            buttonRemovePurchaseItem.Size = new Size(172, 29);
            buttonRemovePurchaseItem.TabIndex = 7;
            buttonRemovePurchaseItem.TabStop = false;
            buttonRemovePurchaseItem.Text = "Видалити позицію";
            buttonRemovePurchaseItem.UseVisualStyleBackColor = false;
            buttonRemovePurchaseItem.Click += buttonRemovePurchaseItem_Click;
            // 
            // dataGridViewPurchaseList
            // 
            dataGridViewPurchaseList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewPurchaseList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewPurchaseList.Location = new Point(11, 46);
            dataGridViewPurchaseList.Name = "dataGridViewPurchaseList";
            dataGridViewPurchaseList.RowHeadersWidth = 51;
            dataGridViewPurchaseList.Size = new Size(248, 272);
            dataGridViewPurchaseList.TabIndex = 1;
            // 
            // labelPurchaseListTitle
            // 
            labelPurchaseListTitle.AutoSize = true;
            labelPurchaseListTitle.Font = new Font("Calibri", 9F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelPurchaseListTitle.Location = new Point(13, 15);
            labelPurchaseListTitle.Name = "labelPurchaseListTitle";
            labelPurchaseListTitle.Size = new Size(109, 18);
            labelPurchaseListTitle.TabIndex = 0;
            labelPurchaseListTitle.Text = "Список покупки";
            // 
            // paymentPanel
            // 
            paymentPanel.BackColor = SystemColors.Window;
            paymentPanel.BorderColor = Color.Silver;
            paymentPanel.BorderThickness = 1;
            paymentPanel.Controls.Add(buttonPaymentCard);
            paymentPanel.Controls.Add(buttonPaymentCash);
            paymentPanel.Controls.Add(labelTotalPrice);
            paymentPanel.Controls.Add(labelTotalText);
            paymentPanel.Controls.Add(buttonCancelSale);
            paymentPanel.Controls.Add(buttonConfirmSale);
            paymentPanel.Dock = DockStyle.Fill;
            paymentPanel.Location = new Point(3, 375);
            paymentPanel.Margin = new Padding(3, 3, 9, 3);
            paymentPanel.Name = "paymentPanel";
            paymentPanel.Size = new Size(271, 243);
            paymentPanel.TabIndex = 1;
            // 
            // buttonPaymentCard
            // 
            buttonPaymentCard.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonPaymentCard.BackColor = SystemColors.MenuHighlight;
            buttonPaymentCard.FlatAppearance.BorderSize = 0;
            buttonPaymentCard.FlatStyle = FlatStyle.Flat;
            buttonPaymentCard.Font = new Font("Calibri", 9F, FontStyle.Bold);
            buttonPaymentCard.ForeColor = Color.White;
            buttonPaymentCard.Location = new Point(139, 124);
            buttonPaymentCard.Name = "buttonPaymentCard";
            buttonPaymentCard.Size = new Size(120, 49);
            buttonPaymentCard.TabIndex = 9;
            buttonPaymentCard.TabStop = false;
            buttonPaymentCard.Text = "Карта";
            buttonPaymentCard.UseVisualStyleBackColor = false;
            buttonPaymentCard.Click += buttonPaymentCard_Click;
            // 
            // buttonPaymentCash
            // 
            buttonPaymentCash.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonPaymentCash.BackColor = SystemColors.MenuHighlight;
            buttonPaymentCash.FlatAppearance.BorderSize = 0;
            buttonPaymentCash.FlatStyle = FlatStyle.Flat;
            buttonPaymentCash.Font = new Font("Calibri", 9F, FontStyle.Bold);
            buttonPaymentCash.ForeColor = Color.White;
            buttonPaymentCash.Location = new Point(13, 124);
            buttonPaymentCash.Name = "buttonPaymentCash";
            buttonPaymentCash.Size = new Size(120, 49);
            buttonPaymentCash.TabIndex = 8;
            buttonPaymentCash.TabStop = false;
            buttonPaymentCash.Text = "Готівка";
            buttonPaymentCash.UseVisualStyleBackColor = false;
            buttonPaymentCash.Click += buttonPaymentCash_Click;
            // 
            // labelTotalPrice
            // 
            labelTotalPrice.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelTotalPrice.AutoSize = true;
            labelTotalPrice.Font = new Font("Calibri", 26F);
            labelTotalPrice.Location = new Point(87, 52);
            labelTotalPrice.Name = "labelTotalPrice";
            labelTotalPrice.Size = new Size(172, 54);
            labelTotalPrice.TabIndex = 10;
            labelTotalPrice.Text = "0.00 грн";
            labelTotalPrice.TextAlign = ContentAlignment.MiddleRight;
            // 
            // labelTotalText
            // 
            labelTotalText.AutoSize = true;
            labelTotalText.Font = new Font("Calibri", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            labelTotalText.Location = new Point(13, 25);
            labelTotalText.Name = "labelTotalText";
            labelTotalText.Size = new Size(101, 24);
            labelTotalText.TabIndex = 9;
            labelTotalText.Text = "До сплати:";
            // 
            // buttonCancelSale
            // 
            buttonCancelSale.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonCancelSale.BackColor = SystemColors.MenuHighlight;
            buttonCancelSale.FlatStyle = FlatStyle.Flat;
            buttonCancelSale.Font = new Font("Microsoft Sans Serif", 8.25F);
            buttonCancelSale.ForeColor = Color.White;
            buttonCancelSale.Location = new Point(13, 179);
            buttonCancelSale.Name = "buttonCancelSale";
            buttonCancelSale.Size = new Size(89, 55);
            buttonCancelSale.TabIndex = 10;
            buttonCancelSale.TabStop = false;
            buttonCancelSale.Text = "Скасувати";
            buttonCancelSale.UseVisualStyleBackColor = true;
            buttonCancelSale.Click += buttonCancelSale_Click;
            // 
            // buttonConfirmSale
            // 
            buttonConfirmSale.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonConfirmSale.BackColor = SystemColors.MenuHighlight;
            buttonConfirmSale.FlatStyle = FlatStyle.Flat;
            buttonConfirmSale.Font = new Font("Calibri", 9F, FontStyle.Bold);
            buttonConfirmSale.ForeColor = Color.White;
            buttonConfirmSale.Location = new Point(108, 179);
            buttonConfirmSale.Name = "buttonConfirmSale";
            buttonConfirmSale.Size = new Size(151, 55);
            buttonConfirmSale.TabIndex = 11;
            buttonConfirmSale.TabStop = false;
            buttonConfirmSale.Text = "Підтвердити продаж";
            buttonConfirmSale.UseVisualStyleBackColor = false;
            buttonConfirmSale.Click += buttonConfirmSale_Click;
            // 
            // columnsPanel
            // 
            columnsPanel.BackColor = SystemColors.Window;
            columnsPanel.BorderColor = Color.Silver;
            columnsPanel.BorderThickness = 1;
            columnsPanel.Controls.Add(buttonColumn5);
            columnsPanel.Controls.Add(buttonColumn4);
            columnsPanel.Controls.Add(buttonColumn3);
            columnsPanel.Controls.Add(buttonColumn2);
            columnsPanel.Controls.Add(buttonColumn1);
            columnsPanel.Controls.Add(fuelTypesPanel);
            columnsPanel.Dock = DockStyle.Fill;
            columnsPanel.Location = new Point(9, 3);
            columnsPanel.Margin = new Padding(9, 3, 3, 3);
            columnsPanel.Name = "columnsPanel";
            columnsPanel.Size = new Size(269, 615);
            columnsPanel.TabIndex = 2;
            // 
            // buttonColumn5
            // 
            buttonColumn5.BackColor = SystemColors.MenuHighlight;
            buttonColumn5.BorderRadius = 10;
            buttonColumn5.FlatAppearance.MouseDownBackColor = SystemColors.Window;
            buttonColumn5.FlatAppearance.MouseOverBackColor = Color.FromArgb(128, 128, 255);
            buttonColumn5.FlatStyle = FlatStyle.Flat;
            buttonColumn5.Font = new Font("Calibri", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            buttonColumn5.ForeColor = Color.White;
            buttonColumn5.Location = new Point(8, 302);
            buttonColumn5.Name = "buttonColumn5";
            buttonColumn5.Size = new Size(250, 51);
            buttonColumn5.TabIndex = 4;
            buttonColumn5.TabStop = false;
            buttonColumn5.Text = "5";
            buttonColumn5.UseVisualStyleBackColor = false;
            buttonColumn5.Click += buttonColumn5_Click;
            // 
            // buttonColumn4
            // 
            buttonColumn4.BackColor = SystemColors.MenuHighlight;
            buttonColumn4.BorderRadius = 10;
            buttonColumn4.FlatAppearance.MouseDownBackColor = SystemColors.Window;
            buttonColumn4.FlatAppearance.MouseOverBackColor = Color.FromArgb(128, 128, 255);
            buttonColumn4.FlatStyle = FlatStyle.Flat;
            buttonColumn4.Font = new Font("Calibri", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            buttonColumn4.ForeColor = Color.White;
            buttonColumn4.Location = new Point(8, 224);
            buttonColumn4.Name = "buttonColumn4";
            buttonColumn4.Size = new Size(250, 51);
            buttonColumn4.TabIndex = 3;
            buttonColumn4.TabStop = false;
            buttonColumn4.Text = "4";
            buttonColumn4.UseVisualStyleBackColor = false;
            buttonColumn4.Click += buttonColumn4_Click;
            // 
            // buttonColumn3
            // 
            buttonColumn3.BackColor = SystemColors.MenuHighlight;
            buttonColumn3.BorderRadius = 10;
            buttonColumn3.FlatAppearance.MouseDownBackColor = SystemColors.Window;
            buttonColumn3.FlatAppearance.MouseOverBackColor = Color.FromArgb(128, 128, 255);
            buttonColumn3.FlatStyle = FlatStyle.Flat;
            buttonColumn3.Font = new Font("Calibri", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            buttonColumn3.ForeColor = Color.White;
            buttonColumn3.Location = new Point(8, 154);
            buttonColumn3.Name = "buttonColumn3";
            buttonColumn3.Size = new Size(250, 51);
            buttonColumn3.TabIndex = 2;
            buttonColumn3.TabStop = false;
            buttonColumn3.Text = "3";
            buttonColumn3.UseVisualStyleBackColor = false;
            buttonColumn3.Click += buttonColumn3_Click;
            // 
            // buttonColumn2
            // 
            buttonColumn2.BackColor = SystemColors.MenuHighlight;
            buttonColumn2.BorderRadius = 10;
            buttonColumn2.FlatAppearance.MouseDownBackColor = SystemColors.Window;
            buttonColumn2.FlatAppearance.MouseOverBackColor = Color.FromArgb(128, 128, 255);
            buttonColumn2.FlatStyle = FlatStyle.Flat;
            buttonColumn2.Font = new Font("Calibri", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            buttonColumn2.ForeColor = Color.White;
            buttonColumn2.Location = new Point(8, 83);
            buttonColumn2.Name = "buttonColumn2";
            buttonColumn2.Size = new Size(250, 51);
            buttonColumn2.TabIndex = 1;
            buttonColumn2.TabStop = false;
            buttonColumn2.Text = "2";
            buttonColumn2.UseVisualStyleBackColor = false;
            buttonColumn2.Click += buttonColumn2_Click;
            // 
            // buttonColumn1
            // 
            buttonColumn1.BackColor = SystemColors.MenuHighlight;
            buttonColumn1.BorderRadius = 10;
            buttonColumn1.FlatAppearance.MouseDownBackColor = SystemColors.Window;
            buttonColumn1.FlatAppearance.MouseOverBackColor = Color.FromArgb(128, 128, 255);
            buttonColumn1.FlatStyle = FlatStyle.Flat;
            buttonColumn1.Font = new Font("Calibri", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            buttonColumn1.ForeColor = Color.White;
            buttonColumn1.Location = new Point(8, 14);
            buttonColumn1.Name = "buttonColumn1";
            buttonColumn1.Size = new Size(250, 51);
            buttonColumn1.TabIndex = 0;
            buttonColumn1.TabStop = false;
            buttonColumn1.Text = "1";
            buttonColumn1.UseVisualStyleBackColor = false;
            buttonColumn1.Click += buttonColumn1_Click;
            // 
            // fuelTypesPanel
            // 
            fuelTypesPanel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            fuelTypesPanel.AutoScroll = true;
            fuelTypesPanel.Location = new Point(8, 382);
            fuelTypesPanel.Name = "fuelTypesPanel";
            fuelTypesPanel.Size = new Size(250, 224);
            fuelTypesPanel.TabIndex = 4;
            // 
            // CashierMainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Menu;
            ClientSize = new Size(1006, 721);
            Controls.Add(mainLayoutPanel);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(1024, 768);
            Name = "CashierMainForm";
            Text = "Головна сторінка";
            mainLayoutPanel.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            productsSectionPanel.ResumeLayout(false);
            productsSectionPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            tableLayoutPanel5.ResumeLayout(false);
            fuelingModePanel.ResumeLayout(false);
            fuelingModePanel.PerformLayout();
            promotionsPanel.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPromotion).EndInit();
            tableLayoutPanel4.ResumeLayout(false);
            purchaseListPanel.ResumeLayout(false);
            purchaseListPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewPurchaseList).EndInit();
            paymentPanel.ResumeLayout(false);
            paymentPanel.PerformLayout();
            columnsPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private TableLayoutPanel mainLayoutPanel;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private TableLayoutPanel tableLayoutPanel3;
        private TableLayoutPanel tableLayoutPanel4;
        private RoundedPanel columnsPanel;
        private RoundedPanel productsSectionPanel;
        private TableLayoutPanel tableLayoutPanel5;
        private RoundedPanel fuelingModePanel;
        private RoundedPanel purchaseListPanel;
        private RoundedPanel paymentPanel;
        private RoundedPanel promotionsPanel;
        private Button buttonExit;
        private Button buttonChangeUser;
        private Button buttonCloseShift;
        private Button buttonCashierSalesHistory;
        private Button buttonShiftReport;
        private FlowLayoutPanel fuelTypesPanel;
        private RoundedButton buttonColumn1;
        private RoundedButton buttonColumn5;
        private RoundedButton buttonColumn4;
        private RoundedButton buttonColumn3;
        private RoundedButton buttonColumn2;
        private RoundedButton buttonSpecificAmount;
        private RoundedButton buttonFillFull;
        private Label labelFuelingCounter;
        private System.Windows.Forms.Timer fuelingTimer;
        private TextBox textBoxSpecificAmount;
        private RoundedButton buttonAddFuelToReceipt;
        private RoundedButton buttonConfirmSale;
        private DataGridView dataGridViewPurchaseList;
        private Label labelPurchaseListTitle;
        private Label labelTotalPrice;
        private Label labelTotalText;
        private RoundedButton buttonRemovePurchaseItem;
        private RoundedButton buttonPaymentCard;
        private RoundedButton buttonPaymentCash;
        private FlowLayoutPanel productsCardsPanel;
        private Panel categoryPanel;
        private TextBox textBoxProductSearch;
        private PictureBox pictureBox1;
        private RoundedButton buttonCancelSale;
        private PictureBox pictureBoxPromotion;
        private Panel panel1;
        private Label labelPromoDot3;
        private Label labelPromoDot2;
        private Label labelPromoDot1;
    }
}