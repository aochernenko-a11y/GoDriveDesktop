using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoDriveDesktop
{
    public partial class AdminMainForm : FullScreenForm
    {
        //
        // Поля класса
        //
        private string currentSection = "Fuel";

        private Panel panelCrud;
        private Panel panelReports;
        private Panel panelSalesHistory;

        private Label labelCrudTitle;
        private TextBox textBoxCrudSearch;
        private DataGridView dataGridViewCrud;

        private RoundedButton buttonAdd;
        private RoundedButton buttonEdit;
        private RoundedButton buttonDelete;
        private RoundedButton buttonRefresh;

        private DateTimePicker dateTimePickerReportFrom;
        private DateTimePicker dateTimePickerReportTo;
        private ComboBox comboBoxReportType;
        private RoundedButton buttonGenerateReport;
        private DataGridView dataGridViewReports;

        private DateTimePicker dateTimePickerHistoryFrom;
        private DateTimePicker dateTimePickerHistoryTo;
        private ComboBox comboBoxHistoryPaymentType;
        private RoundedButton buttonFilterHistory;
        private RoundedButton buttonSaleDetails;
        private DataGridView dataGridViewSalesHistory;

        private readonly Color menuButtonColor = Color.FromArgb(0, 126, 210);
        private readonly Color activeButtonColor = Color.FromArgb(0, 90, 160);
        private readonly Color buttonTextColor = Color.White;

        public AdminMainForm()
        {
            InitializeComponent();

            CreateInterface();
            ShowFuelSection();
            SetupExistingMenuButtons();
        }

        //
        // Створення та налаштування інтерфейсу
        //

        private void CreateInterface()
        {
            Text = "AdminMainForm";
            BackColor = Color.WhiteSmoke;

            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.RowCount = 1;

            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.RowStyles.Clear();

            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 330F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            roundedPanelMenu.Dock = DockStyle.Fill;
            roundedPanelMenu.Padding = new Padding(12);
            roundedPanelMenu.BackColor = Color.White;

            roundedPanelContent.Dock = DockStyle.Fill;
            roundedPanelContent.Padding = new Padding(15);
            roundedPanelContent.BackColor = Color.White;

            flowLayoutPanel2.Dock = DockStyle.Top;
            flowLayoutPanel2.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel2.WrapContents = false;
            flowLayoutPanel2.AutoScroll = false;
            flowLayoutPanel2.Height = 410;

            SetupExistingMenuButtons();

            flowLayoutPanel2.Resize += (sender, e) =>
            {
                UpdateMenuButtonsWidth();
            };

            roundedPanelMenu.Resize += (sender, e) =>
            {
                UpdateMenuButtonsWidth();
            };

            CreateContentPanels();
        }

        // Головне меню

        private void SetupExistingMenuButtons()
        {
            SetupMenuButton(buttonFuel);
            SetupMenuButton(buttonProducts);
            SetupMenuButton(buttonEmployees);
            SetupMenuButton(buttonPromotions);
            SetupMenuButton(buttonSalesHistory);
            SetupMenuButton(buttonReports);

            SetupBottomMenuButton(buttonChangeUser);
            SetupBottomMenuButton(buttonExit);
        }

        private void SetupBottomMenuButton(Button button)
        {
            button.AutoSize = false;
            button.Dock = DockStyle.Bottom;

            button.Height = 60;
            button.Margin = new Padding(0);

            button.BackColor = menuButtonColor;
            button.ForeColor = buttonTextColor;
            button.Font = new Font("Calibri", 16.2F, FontStyle.Bold, GraphicsUnit.Point);

            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;

            button.Cursor = Cursors.Hand;
            button.TextAlign = ContentAlignment.MiddleCenter;
            button.UseVisualStyleBackColor = false;
        }

        private void SetupMenuButton(Button button)
        {
            button.AutoSize = false;
            button.Dock = DockStyle.None;
            button.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            button.Height = 60;
            button.Margin = new Padding(3, 4, 3, 4);

            button.BackColor = menuButtonColor;
            button.ForeColor = buttonTextColor;

            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Cursor = Cursors.Hand;
            button.TextAlign = ContentAlignment.MiddleCenter;
            button.UseVisualStyleBackColor = false;
        }

        private void UpdateMenuButtonsWidth()
        {
            int menuButtonWidth = flowLayoutPanel2.ClientSize.Width - 8;

            if (menuButtonWidth < 200)
            {
                menuButtonWidth = 200;
            }

            buttonFuel.Width = menuButtonWidth;
            buttonProducts.Width = menuButtonWidth;
            buttonEmployees.Width = menuButtonWidth;
            buttonPromotions.Width = menuButtonWidth;
            buttonSalesHistory.Width = menuButtonWidth;
            buttonReports.Width = menuButtonWidth;

            int bottomButtonWidth = roundedPanelMenu.ClientSize.Width
                - roundedPanelMenu.Padding.Left
                - roundedPanelMenu.Padding.Right;

            if (bottomButtonWidth < 200)
            {
                bottomButtonWidth = 200;
            }

            buttonChangeUser.Width = bottomButtonWidth;
            buttonExit.Width = bottomButtonWidth;
        }

        // Панелі вмісту

        private void CreateContentPanels()
        {
            roundedPanelContent.Controls.Clear();

            panelCrud = new Panel
            {
                Dock = DockStyle.Fill,
                Visible = false
            };

            panelReports = new Panel
            {
                Dock = DockStyle.Fill,
                Visible = false
            };

            panelSalesHistory = new Panel
            {
                Dock = DockStyle.Fill,
                Visible = false
            };

            CreateCrudPanel();
            CreateReportsPanel();
            CreateSalesHistoryPanel();

            roundedPanelContent.Controls.Add(panelCrud);
            roundedPanelContent.Controls.Add(panelReports);
            roundedPanelContent.Controls.Add(panelSalesHistory);
        }

        private void CreateCrudPanel()
        {
            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4
            };

            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));

            labelCrudTitle = new Label
            {
                Text = "Керування",
                Dock = DockStyle.Fill,
                Font = new Font("Calibri", 18F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            };

            Panel searchPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(0, 8, 0, 8)
            };

            Label labelSearch = new Label
            {
                Text = "Пошук:",
                Dock = DockStyle.Left,
                Width = 80,
                Font = new Font("Calibri", 11F),
                TextAlign = ContentAlignment.MiddleLeft
            };

            textBoxCrudSearch = new TextBox
            {
                Dock = DockStyle.Fill,
                Font = new Font("Calibri", 11F)
            };

            textBoxCrudSearch.TextChanged += textBoxCrudSearch_TextChanged;

            searchPanel.Controls.Add(textBoxCrudSearch);
            searchPanel.Controls.Add(labelSearch);

            dataGridViewCrud = CreateDataGridView();

            FlowLayoutPanel actionsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                Padding = new Padding(0, 10, 0, 0),
                WrapContents = false
            };

            buttonAdd = CreateActionButton("Додати");
            buttonEdit = CreateActionButton("Редагувати");
            buttonDelete = CreateActionButton("Видалити");
            buttonRefresh = CreateActionButton("Оновити");

            buttonAdd.Click += buttonAdd_Click;
            buttonEdit.Click += buttonEdit_Click;
            buttonDelete.Click += buttonDelete_Click;
            buttonRefresh.Click += buttonRefresh_Click;

            actionsPanel.Controls.Add(buttonRefresh);
            actionsPanel.Controls.Add(buttonDelete);
            actionsPanel.Controls.Add(buttonEdit);
            actionsPanel.Controls.Add(buttonAdd);

            layout.Controls.Add(labelCrudTitle, 0, 0);
            layout.Controls.Add(searchPanel, 0, 1);
            layout.Controls.Add(dataGridViewCrud, 0, 2);
            layout.Controls.Add(actionsPanel, 0, 3);

            panelCrud.Controls.Add(layout);
        }

        private void CreateReportsPanel()
        {
            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3
            };

            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            Label labelTitle = new Label
            {
                Text = "Формування звітів",
                Dock = DockStyle.Fill,
                Font = new Font("Calibri", 18F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            };

            FlowLayoutPanel filterPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(0, 10, 0, 0),
                WrapContents = false
            };

            dateTimePickerReportFrom = new DateTimePicker
            {
                Width = 160,
                Format = DateTimePickerFormat.Short
            };

            dateTimePickerReportTo = new DateTimePicker
            {
                Width = 160,
                Format = DateTimePickerFormat.Short
            };

            comboBoxReportType = new ComboBox
            {
                Width = 220,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            comboBoxReportType.Items.Add("Усі продажі");
            comboBoxReportType.Items.Add("Продаж пального");
            comboBoxReportType.Items.Add("Продаж товарів");
            comboBoxReportType.Items.Add("Зміни касирів");
            comboBoxReportType.SelectedIndex = 0;

            buttonGenerateReport = CreateActionButton("Сформувати");
            buttonGenerateReport.Click += buttonGenerateReport_Click;

            filterPanel.Controls.Add(CreateSmallLabel("Дата від:"));
            filterPanel.Controls.Add(dateTimePickerReportFrom);
            filterPanel.Controls.Add(CreateSmallLabel("Дата до:"));
            filterPanel.Controls.Add(dateTimePickerReportTo);
            filterPanel.Controls.Add(CreateSmallLabel("Тип:"));
            filterPanel.Controls.Add(comboBoxReportType);
            filterPanel.Controls.Add(buttonGenerateReport);

            dataGridViewReports = CreateDataGridView();

            layout.Controls.Add(labelTitle, 0, 0);
            layout.Controls.Add(filterPanel, 0, 1);
            layout.Controls.Add(dataGridViewReports, 0, 2);

            panelReports.Controls.Add(layout);
        }

        private void CreateSalesHistoryPanel()
        {
            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3
            };

            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 55F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            Label labelTitle = new Label
            {
                Text = "Історія продажів",
                Dock = DockStyle.Fill,
                Font = new Font("Calibri", 18F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft
            };

            FlowLayoutPanel filterPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(0, 10, 0, 0),
                WrapContents = false
            };

            dateTimePickerHistoryFrom = new DateTimePicker
            {
                Width = 160,
                Format = DateTimePickerFormat.Short
            };

            dateTimePickerHistoryTo = new DateTimePicker
            {
                Width = 160,
                Format = DateTimePickerFormat.Short
            };

            comboBoxHistoryPaymentType = new ComboBox
            {
                Width = 160,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            comboBoxHistoryPaymentType.Items.Add("Усі");
            comboBoxHistoryPaymentType.Items.Add("Готівка");
            comboBoxHistoryPaymentType.Items.Add("Картка");
            comboBoxHistoryPaymentType.SelectedIndex = 0;

            buttonFilterHistory = CreateActionButton("Фільтрувати");
            buttonSaleDetails = CreateActionButton("Деталі");

            buttonFilterHistory.Click += buttonFilterHistory_Click;
            buttonSaleDetails.Click += buttonSaleDetails_Click;

            filterPanel.Controls.Add(CreateSmallLabel("Дата від:"));
            filterPanel.Controls.Add(dateTimePickerHistoryFrom);
            filterPanel.Controls.Add(CreateSmallLabel("Дата до:"));
            filterPanel.Controls.Add(dateTimePickerHistoryTo);
            filterPanel.Controls.Add(CreateSmallLabel("Оплата:"));
            filterPanel.Controls.Add(comboBoxHistoryPaymentType);
            filterPanel.Controls.Add(buttonFilterHistory);
            filterPanel.Controls.Add(buttonSaleDetails);

            dataGridViewSalesHistory = CreateDataGridView();

            layout.Controls.Add(labelTitle, 0, 0);
            layout.Controls.Add(filterPanel, 0, 1);
            layout.Controls.Add(dataGridViewSalesHistory, 0, 2);

            panelSalesHistory.Controls.Add(layout);
        }

        // Створення загальних елементів інтерфейсу

        private DataGridView CreateDataGridView()
        {
            DataGridView dataGridView = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                MultiSelect = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Calibri", 10F)
            };

            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Calibri", 10F, FontStyle.Bold);
            dataGridView.RowHeadersVisible = false;

            return dataGridView;
        }

        private RoundedButton CreateActionButton(string text)
        {
            RoundedButton button = new RoundedButton
            {
                Text = text,
                Width = 140,
                Height = 40,
                Margin = new Padding(8, 0, 0, 0),
                BackColor = menuButtonColor,
                ForeColor = buttonTextColor,
                Font = new Font("Calibri", 10F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };

            button.FlatAppearance.BorderSize = 0;

            return button;
        }

        private Label CreateSmallLabel(string text)
        {
            return new Label
            {
                Text = text,
                Width = 75,
                Height = 30,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Calibri", 10F)
            };
        }

        // Перемикання видимих панелей

        private void ShowContentPanel(Panel panel)
        {
            panelCrud.Visible = false;
            panelReports.Visible = false;
            panelSalesHistory.Visible = false;

            panel.Visible = true;
            panel.BringToFront();
        }

        private void SetActiveButton(Button activeButton)
        {
            Button[] buttons =
            {
        buttonFuel,
        buttonProducts,
        buttonEmployees,
        buttonPromotions,
        buttonReports,
        buttonSalesHistory
    };

            foreach (Button button in buttons)
            {
                button.BackColor = menuButtonColor;
            }

            activeButton.BackColor = activeButtonColor;
        }

        //
        // Обробники форми та кнопок меню
        //

        private void AdminMainForm_Load(object sender, EventArgs e)
        {

        }


        private void buttonFuel_Click(object sender, EventArgs e)
        {
            ShowFuelSection();
        }

        private void buttonProducts_Click(object sender, EventArgs e)
        {
            ShowProductsSection();
        }

        private void buttonEmployees_Click(object sender, EventArgs e)
        {
            ShowEmployeesSection();
        }

        private void buttonPromotions_Click(object sender, EventArgs e)
        {
            ShowPromotionsSection();
        }

        private void buttonReports_Click(object sender, EventArgs e)
        {
            currentSection = "Reports";
            SetActiveButton(buttonReports);
            ShowContentPanel(panelReports);
            GenerateReport();
        }

        private void buttonSalesHistory_Click(object sender, EventArgs e)
        {
            currentSection = "SalesHistory";
            SetActiveButton(buttonSalesHistory);
            ShowContentPanel(panelSalesHistory);
            LoadSalesHistory();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //
        // Перемикання CRUD-розділів
        //

        private void ShowFuelSection()
        {
            currentSection = "Fuel";
            labelCrudTitle.Text = "Керування пальним";
            textBoxCrudSearch.Clear();

            SetActiveButton(buttonFuel);
            ShowContentPanel(panelCrud);
            LoadFuel();
        }

        private void ShowProductsSection()
        {
            currentSection = "Products";
            labelCrudTitle.Text = "Керування товарами";
            textBoxCrudSearch.Clear();

            SetActiveButton(buttonProducts);
            ShowContentPanel(panelCrud);
            LoadProducts();
        }

        private void ShowEmployeesSection()
        {
            currentSection = "Employees";
            labelCrudTitle.Text = "Керування працівниками";
            textBoxCrudSearch.Clear();

            SetActiveButton(buttonEmployees);
            ShowContentPanel(panelCrud);
            LoadEmployees();
        }

        private void ShowPromotionsSection()
        {
            currentSection = "Promotions";
            labelCrudTitle.Text = "Акції";
            textBoxCrudSearch.Clear();

            SetActiveButton(buttonPromotions);
            ShowContentPanel(panelCrud);
            LoadPromotions();
        }

        // Пошук та оновлення поточного CRUD-розділу
        
        private void textBoxCrudSearch_TextChanged(object sender, EventArgs e)
        {
            if (!panelCrud.Visible)
            {
                return;
            }

            LoadCurrentCrudData();
        }

        private void LoadCurrentCrudData()
        {
            if (currentSection == "Fuel")
            {
                LoadFuel();
            }
            else if (currentSection == "Products")
            {
                LoadProducts();
            }
            else if (currentSection == "Employees")
            {
                LoadEmployees();
            }
            else if (currentSection == "Promotions")
            {
                LoadPromotions();
            }
        }

        //
        // Загальні CRUD-кнопки
        //

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadCurrentCrudData();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (currentSection == "Fuel")
            {
                AddFuel();
            }
            else if (currentSection == "Products")
            {
                AddProduct();
            }
            else if (currentSection == "Employees")
            {
                AddEmployee();
            }
            else if (currentSection == "Promotions")
            {
                AddPromotion();
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (currentSection == "Fuel")
            {
                EditFuel();
            }
            else if (currentSection == "Products")
            {
                EditProduct();
            }
            else if (currentSection == "Employees")
            {
                EditEmployee();
            }
            else if (currentSection == "Promotions")
            {
                EditPromotion();
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (!TryGetSelectedId(dataGridViewCrud, out int id))
            {
                return;
            }

            DialogResult result = MessageBox.Show(
                "Видалити обраний запис?",
                "Підтвердження",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                return;
            }

            try
            {
                if (currentSection == "Fuel")
                {
                    ExecuteNonQuery("DELETE FROM Fuel WHERE FuelId = @Id", command =>
                    {
                        command.Parameters.AddWithValue("@Id", id);
                    });
                }
                else if (currentSection == "Products")
                {
                    ExecuteNonQuery("DELETE FROM Products WHERE ProductId = @Id", command =>
                    {
                        command.Parameters.AddWithValue("@Id", id);
                    });
                }
                else if (currentSection == "Employees")
                {
                    ExecuteNonQuery("DELETE FROM Employees WHERE EmployeeId = @Id", command =>
                    {
                        command.Parameters.AddWithValue("@Id", id);
                    });
                }
                else if (currentSection == "Promotions")
                {
                    ExecuteNonQuery("DELETE FROM Promotions WHERE PromotionId = @Id", command =>
                    {
                        command.Parameters.AddWithValue("@Id", id);
                    });
                }

                LoadCurrentCrudData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Не вдалося видалити запис. Можливо, він уже використовується в продажах або змінах.\n\n" + ex.Message,
                    "Помилка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }


        //
        // Розділ "Пальне"
        //
        private void LoadFuel()
        {
            string search = "%" + textBoxCrudSearch.Text.Trim() + "%";

            string query = @"
                SELECT 
                    FuelId AS 'ID',
                    Name AS 'Назва',
                    FuelType AS 'Тип',
                    PricePerLiter AS 'Ціна за літр',
                    AmountLiters AS 'Залишок літрів'
                FROM Fuel
                WHERE Name LIKE @Search OR FuelType LIKE @Search
                ORDER BY FuelId;";

            LoadTable(query, command =>
            {
                command.Parameters.AddWithValue("@Search", search);
            }, dataGridViewCrud);
        }

        // Додавання та редагування пального

        private void AddFuel()
        {
            Dictionary<string, object> values = ShowFuelDialog(null);

            if (values == null)
            {
                return;
            }

            try
            {
                ExecuteNonQuery(@"
                    INSERT INTO Fuel (Name, FuelType, PricePerLiter, AmountLiters)
                    VALUES (@Name, @FuelType, @PricePerLiter, @AmountLiters);",
                    command =>
                    {
                        command.Parameters.AddWithValue("@Name", values["Name"]);
                        command.Parameters.AddWithValue("@FuelType", values["FuelType"]);
                        command.Parameters.AddWithValue("@PricePerLiter", values["PricePerLiter"]);
                        command.Parameters.AddWithValue("@AmountLiters", values["AmountLiters"]);
                    });

                LoadFuel();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void EditFuel()
        {
            if (!TryGetSelectedId(dataGridViewCrud, out int id))
            {
                return;
            }

            DataRow row = GetDataRow("SELECT * FROM Fuel WHERE FuelId = @Id", id);

            if (row == null)
            {
                return;
            }

            Dictionary<string, object> values = ShowFuelDialog(row);

            if (values == null)
            {
                return;
            }

            try
            {
                ExecuteNonQuery(@"
                    UPDATE Fuel 
                    SET Name = @Name,
                        FuelType = @FuelType,
                        PricePerLiter = @PricePerLiter,
                        AmountLiters = @AmountLiters
                    WHERE FuelId = @Id;",
                    command =>
                    {
                        command.Parameters.AddWithValue("@Name", values["Name"]);
                        command.Parameters.AddWithValue("@FuelType", values["FuelType"]);
                        command.Parameters.AddWithValue("@PricePerLiter", values["PricePerLiter"]);
                        command.Parameters.AddWithValue("@AmountLiters", values["AmountLiters"]);
                        command.Parameters.AddWithValue("@Id", id);
                    });

                LoadFuel();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        // Діалогове вікно пального

        private Dictionary<string, object> ShowFuelDialog(DataRow row)
        {
            Form form = CreateEditForm(row == null ? "Додати пальне" : "Редагувати пальне");

            TextBox textBoxName = CreateTextBox();
            TextBox textBoxFuelType = CreateTextBox();
            TextBox textBoxPrice = CreateTextBox();
            TextBox textBoxAmount = CreateTextBox();

            if (row != null)
            {
                textBoxName.Text = row["Name"].ToString();
                textBoxFuelType.Text = row["FuelType"].ToString();
                textBoxPrice.Text = row["PricePerLiter"].ToString();
                textBoxAmount.Text = row["AmountLiters"].ToString();
            }

            AddDialogRow(form, "Назва:", textBoxName, 0);
            AddDialogRow(form, "Тип:", textBoxFuelType, 1);
            AddDialogRow(form, "Ціна за літр:", textBoxPrice, 2);
            AddDialogRow(form, "Залишок літрів:", textBoxAmount, 3);

            if (form.ShowDialog() != DialogResult.OK)
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(textBoxName.Text) ||
                string.IsNullOrWhiteSpace(textBoxFuelType.Text) ||
                !TryParseDecimal(textBoxPrice.Text, out decimal price) ||
                !TryParseDecimal(textBoxAmount.Text, out decimal amount) ||
                price <= 0 ||
                amount < 0)
            {
                MessageBox.Show("Вкажіть коректні дані для пального.");
                return null;
            }

            return new Dictionary<string, object>
            {
                { "Name", textBoxName.Text.Trim() },
                { "FuelType", textBoxFuelType.Text.Trim() },
                { "PricePerLiter", price },
                { "AmountLiters", amount }
            };
        }

        //
        // Розділ "Товари"
        //

        private void LoadProducts()
        {
            string search = "%" + textBoxCrudSearch.Text.Trim() + "%";

            string query = @"
                SELECT 
                    p.ProductId AS 'ID',
                    p.Name AS 'Назва',
                    c.Name AS 'Категорія',
                    p.Price AS 'Ціна',
                    p.Quantity AS 'Кількість'
                FROM Products p
                INNER JOIN ProductCategories c ON p.CategoryId = c.CategoryId
                WHERE p.Name LIKE @Search OR c.Name LIKE @Search
                ORDER BY p.ProductId;";

            LoadTable(query, command =>
            {
                command.Parameters.AddWithValue("@Search", search);
            }, dataGridViewCrud);
        }

        // Додавання та редагування товару

        private void AddProduct()
        {
            Dictionary<string, object> values = ShowProductDialog(null);

            if (values == null)
            {
                return;
            }

            try
            {
                ExecuteNonQuery(@"
                    INSERT INTO Products (Name, CategoryId, Price, Quantity)
                    VALUES (@Name, @CategoryId, @Price, @Quantity);",
                    command =>
                    {
                        command.Parameters.AddWithValue("@Name", values["Name"]);
                        command.Parameters.AddWithValue("@CategoryId", values["CategoryId"]);
                        command.Parameters.AddWithValue("@Price", values["Price"]);
                        command.Parameters.AddWithValue("@Quantity", values["Quantity"]);
                    });

                LoadProducts();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void EditProduct()
        {
            if (!TryGetSelectedId(dataGridViewCrud, out int id))
            {
                return;
            }

            DataRow row = GetDataRow("SELECT * FROM Products WHERE ProductId = @Id", id);

            if (row == null)
            {
                return;
            }

            Dictionary<string, object> values = ShowProductDialog(row);

            if (values == null)
            {
                return;
            }

            try
            {
                ExecuteNonQuery(@"
                    UPDATE Products
                    SET Name = @Name,
                        CategoryId = @CategoryId,
                        Price = @Price,
                        Quantity = @Quantity
                    WHERE ProductId = @Id;",
                    command =>
                    {
                        command.Parameters.AddWithValue("@Name", values["Name"]);
                        command.Parameters.AddWithValue("@CategoryId", values["CategoryId"]);
                        command.Parameters.AddWithValue("@Price", values["Price"]);
                        command.Parameters.AddWithValue("@Quantity", values["Quantity"]);
                        command.Parameters.AddWithValue("@Id", id);
                    });

                LoadProducts();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        // Діалогове вікно товару

        private Dictionary<string, object> ShowProductDialog(DataRow row)
        {
            Form form = CreateEditForm(row == null ? "Додати товар" : "Редагувати товар");

            TextBox textBoxName = CreateTextBox();
            ComboBox comboBoxCategory = CreateComboBox();
            TextBox textBoxPrice = CreateTextBox();
            TextBox textBoxQuantity = CreateTextBox();

            LoadOptions(comboBoxCategory, "SELECT CategoryId AS Id, Name FROM ProductCategories ORDER BY Name;");

            if (row != null)
            {
                textBoxName.Text = row["Name"].ToString();
                textBoxPrice.Text = row["Price"].ToString();
                textBoxQuantity.Text = row["Quantity"].ToString();
                SelectComboOption(comboBoxCategory, Convert.ToInt32(row["CategoryId"]));
            }

            AddDialogRow(form, "Назва:", textBoxName, 0);
            AddDialogRow(form, "Категорія:", comboBoxCategory, 1);
            AddDialogRow(form, "Ціна:", textBoxPrice, 2);
            AddDialogRow(form, "Кількість:", textBoxQuantity, 3);

            if (form.ShowDialog() != DialogResult.OK)
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(textBoxName.Text) ||
                comboBoxCategory.SelectedItem == null ||
                !TryParseDecimal(textBoxPrice.Text, out decimal price) ||
                !int.TryParse(textBoxQuantity.Text, out int quantity) ||
                price <= 0 ||
                quantity < 0)
            {
                MessageBox.Show("Вкажіть коректні дані для товару.");
                return null;
            }

            ComboOption category = (ComboOption)comboBoxCategory.SelectedItem;

            return new Dictionary<string, object>
            {
                { "Name", textBoxName.Text.Trim() },
                { "CategoryId", category.Id },
                { "Price", price },
                { "Quantity", quantity }
            };
        }

        //
        // Розділ "Працівники"
        //

        private void LoadEmployees()
        {
            string search = "%" + textBoxCrudSearch.Text.Trim() + "%";

            string query = @"
                SELECT 
                    e.EmployeeId AS 'ID',
                    e.FullName AS 'ПІБ',
                    e.Login AS 'Логін',
                    p.Name AS 'Посада'
                FROM Employees e
                INNER JOIN Positions p ON e.PositionId = p.PositionId
                WHERE e.FullName LIKE @Search OR e.Login LIKE @Search OR p.Name LIKE @Search
                ORDER BY e.EmployeeId;";

            LoadTable(query, command =>
            {
                command.Parameters.AddWithValue("@Search", search);
            }, dataGridViewCrud);
        }

        // Додавання та редагування працівника

        private void AddEmployee()
        {
            Dictionary<string, object> values = ShowEmployeeDialog(null);

            if (values == null)
            {
                return;
            }

            try
            {
                ExecuteNonQuery(@"
                    INSERT INTO Employees (FullName, Login, Password, PositionId)
                    VALUES (@FullName, @Login, @Password, @PositionId);",
                    command =>
                    {
                        command.Parameters.AddWithValue("@FullName", values["FullName"]);
                        command.Parameters.AddWithValue("@Login", values["Login"]);
                        command.Parameters.AddWithValue("@Password", values["Password"]);
                        command.Parameters.AddWithValue("@PositionId", values["PositionId"]);
                    });

                LoadEmployees();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void EditEmployee()
        {
            if (!TryGetSelectedId(dataGridViewCrud, out int id))
            {
                return;
            }

            DataRow row = GetDataRow("SELECT * FROM Employees WHERE EmployeeId = @Id", id);

            if (row == null)
            {
                return;
            }

            Dictionary<string, object> values = ShowEmployeeDialog(row);

            if (values == null)
            {
                return;
            }

            try
            {
                ExecuteNonQuery(@"
                    UPDATE Employees
                    SET FullName = @FullName,
                        Login = @Login,
                        Password = @Password,
                        PositionId = @PositionId
                    WHERE EmployeeId = @Id;",
                    command =>
                    {
                        command.Parameters.AddWithValue("@FullName", values["FullName"]);
                        command.Parameters.AddWithValue("@Login", values["Login"]);
                        command.Parameters.AddWithValue("@Password", values["Password"]);
                        command.Parameters.AddWithValue("@PositionId", values["PositionId"]);
                        command.Parameters.AddWithValue("@Id", id);
                    });

                LoadEmployees();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        // Діалогове вікно працівника

        private Dictionary<string, object> ShowEmployeeDialog(DataRow row)
        {
            Form form = CreateEditForm(row == null ? "Додати працівника" : "Редагувати працівника");

            TextBox textBoxFullName = CreateTextBox();
            TextBox textBoxLogin = CreateTextBox();
            TextBox textBoxPassword = CreateTextBox();
            ComboBox comboBoxPosition = CreateComboBox();

            LoadOptions(comboBoxPosition, "SELECT PositionId AS Id, Name FROM Positions ORDER BY Name;");

            if (row != null)
            {
                textBoxFullName.Text = row["FullName"].ToString();
                textBoxLogin.Text = row["Login"].ToString();
                textBoxPassword.Text = row["Password"].ToString();
                SelectComboOption(comboBoxPosition, Convert.ToInt32(row["PositionId"]));
            }

            AddDialogRow(form, "ПІБ:", textBoxFullName, 0);
            AddDialogRow(form, "Логін:", textBoxLogin, 1);
            AddDialogRow(form, "Пароль:", textBoxPassword, 2);
            AddDialogRow(form, "Посада:", comboBoxPosition, 3);

            if (form.ShowDialog() != DialogResult.OK)
            {
                return null;
            }

            if (string.IsNullOrWhiteSpace(textBoxFullName.Text) ||
                string.IsNullOrWhiteSpace(textBoxLogin.Text) ||
                string.IsNullOrWhiteSpace(textBoxPassword.Text) ||
                comboBoxPosition.SelectedItem == null)
            {
                MessageBox.Show("Заповніть усі обов’язкові поля працівника.");
                return null;
            }

            ComboOption position = (ComboOption)comboBoxPosition.SelectedItem;

            return new Dictionary<string, object>
            {
                { "FullName", textBoxFullName.Text.Trim() },
                { "Login", textBoxLogin.Text.Trim() },
                { "Password", textBoxPassword.Text.Trim() },
                { "PositionId", position.Id }
            };
        }

        //
        // Розділ "Акції"
        //

        private void LoadPromotions()
        {
            string search = textBoxCrudSearch.Text.Trim();

            string query = @"
        SELECT
            pr.PromotionId AS 'ID',
            pr.Name AS 'Назва',

            CASE
                WHEN pr.ConditionType = 'FuelLiters' AND pr.TargetType = 'Product'
                    THEN 'Знижка на товар при заправці'
                WHEN pr.ConditionType = 'FuelLiters' AND pr.TargetType = 'Category'
                    THEN 'Знижка на категорію при заправці'

                WHEN pr.TargetType = 'Product' AND pr.DiscountType = 'Percent'
                    THEN 'Знижка % на товар'
                WHEN pr.TargetType = 'Product' AND pr.DiscountType IN ('Fixed', 'Amount')
                    THEN 'Знижка грн на товар'

                WHEN pr.TargetType = 'Category' AND pr.DiscountType = 'Percent'
                    THEN 'Знижка % на категорію'
                WHEN pr.TargetType = 'Category' AND pr.DiscountType IN ('Fixed', 'Amount')
                    THEN 'Знижка грн на категорію'

                WHEN pr.TargetType = 'Fuel' AND pr.DiscountType = 'Percent'
                    THEN 'Знижка % на пальне'
                WHEN pr.TargetType = 'Fuel' AND pr.DiscountType IN ('Fixed', 'Amount')
                    THEN 'Знижка грн/л на пальне'

                ELSE 'Інша акція'
            END AS 'Тип акції',

            COALESCE(p.Name, pc.Name, f.Name) AS 'На що діє',

            CASE
                WHEN pr.ConditionType = 'FuelLiters'
                    THEN CONCAT('Від ', pr.ConditionMinLiters, ' л: ', COALESCE(cf.Name, 'будь-яке пальне'))
                WHEN pr.TargetType = 'Fuel'
                    THEN CONCAT('Від ', pr.MinQuantity, ' л')
                ELSE 'Без умови'
            END AS 'Умова',

            CASE
                WHEN pr.DiscountType = 'Percent'
                    THEN CONCAT(pr.DiscountValue, ' %')
                WHEN pr.TargetType = 'Fuel'
                    THEN CONCAT(pr.DiscountValue, ' грн/л')
                ELSE CONCAT(pr.DiscountValue, ' грн')
            END AS 'Знижка',

            pr.StartDate AS 'Початок',
            pr.EndDate AS 'Кінець',

            CASE 
                WHEN pr.IsActive = 1 THEN 'Активна'
                ELSE 'Вимкнена'
            END AS 'Статус'

        FROM Promotions pr
        LEFT JOIN Products p ON pr.ProductId = p.ProductId
        LEFT JOIN ProductCategories pc ON pr.CategoryId = pc.CategoryId
        LEFT JOIN Fuel f ON pr.FuelId = f.FuelId
        LEFT JOIN Fuel cf ON pr.ConditionFuelId = cf.FuelId

        WHERE @Search = ''
           OR pr.Name LIKE CONCAT('%', @Search, '%')
           OR p.Name LIKE CONCAT('%', @Search, '%')
           OR pc.Name LIKE CONCAT('%', @Search, '%')
           OR f.Name LIKE CONCAT('%', @Search, '%')
           OR cf.Name LIKE CONCAT('%', @Search, '%')

        ORDER BY pr.PromotionId DESC;
    ";

            LoadTable(query, command =>
            {
                command.Parameters.AddWithValue("@Search", search);
            }, dataGridViewCrud);
        }
        
        // Параметри акції

        private void AddPromotion()
        {
            Dictionary<string, object> values = ShowPromotionDialog(null);

            if (values == null)
            {
                return;
            }

            try
            {
                ExecuteNonQuery(@"
            INSERT INTO Promotions
            (
                Name,
                TargetType,
                ProductId,
                CategoryId,
                FuelId,
                MinQuantity,
                ConditionType,
                ConditionFuelId,
                ConditionMinLiters,
                DiscountType,
                DiscountValue,
                StartDate,
                EndDate,
                IsActive
            )
            VALUES
            (
                @Name,
                @TargetType,
                @ProductId,
                @CategoryId,
                @FuelId,
                @MinQuantity,
                @ConditionType,
                @ConditionFuelId,
                @ConditionMinLiters,
                @DiscountType,
                @DiscountValue,
                @StartDate,
                @EndDate,
                @IsActive
            );",
                    command =>
                    {
                        AddPromotionParameters(command, values);
                    });

                LoadPromotions();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void EditPromotion()
        {
            if (!TryGetSelectedId(dataGridViewCrud, out int id))
            {
                return;
            }

            DataRow row = GetDataRow("SELECT * FROM Promotions WHERE PromotionId = @Id", id);

            if (row == null)
            {
                return;
            }

            Dictionary<string, object> values = ShowPromotionDialog(row);

            if (values == null)
            {
                return;
            }

            try
            {
                ExecuteNonQuery(@"
            UPDATE Promotions
            SET Name = @Name,
                TargetType = @TargetType,
                ProductId = @ProductId,
                CategoryId = @CategoryId,
                FuelId = @FuelId,
                MinQuantity = @MinQuantity,
                ConditionType = @ConditionType,
                ConditionFuelId = @ConditionFuelId,
                ConditionMinLiters = @ConditionMinLiters,
                DiscountType = @DiscountType,
                DiscountValue = @DiscountValue,
                StartDate = @StartDate,
                EndDate = @EndDate,
                IsActive = @IsActive
            WHERE PromotionId = @Id;",
                    command =>
                    {
                        AddPromotionParameters(command, values);
                        command.Parameters.AddWithValue("@Id", id);
                    });

                LoadPromotions();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        // Параметри акції

        private void AddPromotionParameters(MySqlCommand command, Dictionary<string, object> values)
        {
            command.Parameters.AddWithValue("@Name", values["Name"]);
            command.Parameters.AddWithValue("@TargetType", values["TargetType"]);
            command.Parameters.AddWithValue("@ProductId", values["ProductId"] ?? DBNull.Value);
            command.Parameters.AddWithValue("@CategoryId", values["CategoryId"] ?? DBNull.Value);
            command.Parameters.AddWithValue("@FuelId", values["FuelId"] ?? DBNull.Value);
            command.Parameters.AddWithValue("@MinQuantity", values["MinQuantity"]);

            command.Parameters.AddWithValue("@ConditionType", values["ConditionType"]);
            command.Parameters.AddWithValue("@ConditionFuelId", values["ConditionFuelId"] ?? DBNull.Value);
            command.Parameters.AddWithValue("@ConditionMinLiters", values["ConditionMinLiters"] ?? DBNull.Value);

            command.Parameters.AddWithValue("@DiscountType", values["DiscountType"]);
            command.Parameters.AddWithValue("@DiscountValue", values["DiscountValue"]);
            command.Parameters.AddWithValue("@StartDate", values["StartDate"]);
            command.Parameters.AddWithValue("@EndDate", values["EndDate"]);
            command.Parameters.AddWithValue("@IsActive", values["IsActive"]);
        }

        private void buttonChangeUser_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        // Діалогове вікно акції

        private Dictionary<string, object> ShowPromotionDialog(DataRow row)
        {
            const string actionProductFixed = "Знижка грн на конкретний товар";
            const string actionCategoryFixed = "Знижка грн на категорію товарів";
            const string actionProductPercent = "Знижка % на конкретний товар";
            const string actionCategoryPercent = "Знижка % на категорію товарів";
            const string actionFuelFixed = "Знижка грн/л на пальне від X літрів";
            const string actionFuelPercent = "Знижка % на пальне від X літрів";
            const string actionProductFuelCondition = "Знижка на товар при заправці від X літрів";
            const string actionCategoryFuelCondition = "Знижка на категорію при заправці від X літрів";

            Form form = new Form
            {
                Text = row == null ? "Додати акцію" : "Редагувати акцію",
                Width = 620,
                Height = 610,
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 12,
                Padding = new Padding(15)
            };

            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 200F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            for (int i = 0; i < 11; i++)
            {
                layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42F));
            }

            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));
            form.Controls.Add(layout);

            Label AddRow(string labelText, Control inputControl, int rowIndex)
            {
                Label label = new Label
                {
                    Text = labelText,
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleLeft,
                    Font = new Font("Calibri", 10F)
                };

                inputControl.Dock = DockStyle.Fill;

                layout.Controls.Add(label, 0, rowIndex);
                layout.Controls.Add(inputControl, 1, rowIndex);

                return label;
            }

            void SetRowVisible(Label label, Control control, int rowIndex, bool visible)
            {
                label.Visible = visible;
                control.Visible = visible;

                layout.RowStyles[rowIndex].Height = visible ? 42F : 0F;
                layout.RowStyles[rowIndex].SizeType = SizeType.Absolute;
            }

            bool HasColumn(string columnName)
            {
                return row != null && row.Table.Columns.Contains(columnName);
            }

            string GetStringValue(string columnName, string defaultValue)
            {
                if (!HasColumn(columnName) || row[columnName] == DBNull.Value)
                {
                    return defaultValue;
                }

                return row[columnName].ToString();
            }

            string GetSelectedTargetType(string action)
            {
                if (action == actionProductFixed ||
                    action == actionProductPercent ||
                    action == actionProductFuelCondition)
                {
                    return "Product";
                }

                if (action == actionCategoryFixed ||
                    action == actionCategoryPercent ||
                    action == actionCategoryFuelCondition)
                {
                    return "Category";
                }

                return "Fuel";
            }

            string GetSelectedDiscountType(string action)
            {
                if (action == actionProductPercent ||
                    action == actionCategoryPercent ||
                    action == actionFuelPercent)
                {
                    return "Percent";
                }

                if (action == actionProductFuelCondition ||
                    action == actionCategoryFuelCondition)
                {
                    return "Manual";
                }

                return "Fixed";
            }
            bool UsesLiters(string action)
            {
                return action == actionFuelFixed ||
                       action == actionFuelPercent ||
                       action == actionProductFuelCondition ||
                       action == actionCategoryFuelCondition;
            }

            bool UsesFuelCondition(string action)
            {
                return action == actionProductFuelCondition ||
                       action == actionCategoryFuelCondition;
            }

            bool UsesDiscountTypeChoice(string action)
            {
                return action == actionProductFuelCondition ||
                       action == actionCategoryFuelCondition;
            }

            TextBox textBoxName = CreateTextBox();

            ComboBox comboBoxActionType = CreateComboBox();
            comboBoxActionType.Items.Add(actionProductFixed);
            comboBoxActionType.Items.Add(actionCategoryFixed);
            comboBoxActionType.Items.Add(actionProductPercent);
            comboBoxActionType.Items.Add(actionCategoryPercent);
            comboBoxActionType.Items.Add(actionFuelFixed);
            comboBoxActionType.Items.Add(actionFuelPercent);
            comboBoxActionType.Items.Add(actionProductFuelCondition);
            comboBoxActionType.Items.Add(actionCategoryFuelCondition);

            ComboBox comboBoxProduct = CreateComboBox();
            ComboBox comboBoxCategory = CreateComboBox();
            ComboBox comboBoxFuel = CreateComboBox();
            ComboBox comboBoxConditionFuel = CreateComboBox();

            LoadOptions(comboBoxProduct, "SELECT ProductId AS Id, Name FROM Products ORDER BY Name;");
            LoadOptions(comboBoxCategory, "SELECT CategoryId AS Id, Name FROM ProductCategories ORDER BY Name;");
            LoadOptions(comboBoxFuel, "SELECT FuelId AS Id, Name FROM Fuel ORDER BY Name;");
            LoadOptions(comboBoxConditionFuel, "SELECT FuelId AS Id, Name FROM Fuel ORDER BY Name;");

            comboBoxConditionFuel.Items.Insert(0, new ComboOption
            {
                Id = 0,
                Name = "Будь-яке пальне"
            });

            comboBoxConditionFuel.SelectedIndex = 0;

            TextBox textBoxLiters = CreateTextBox();
            textBoxLiters.Text = "25";

            ComboBox comboBoxDiscountType = CreateComboBox();
            comboBoxDiscountType.Items.Add("Відсоток (%)");
            comboBoxDiscountType.Items.Add("Гривні (грн)");
            comboBoxDiscountType.SelectedIndex = 0;

            TextBox textBoxDiscountValue = CreateTextBox();

            DateTimePicker dateStart = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Dock = DockStyle.Fill
            };

            DateTimePicker dateEnd = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Dock = DockStyle.Fill
            };

            CheckBox checkBoxIsActive = new CheckBox
            {
                Text = "Активна",
                Checked = true,
                Dock = DockStyle.Fill
            };

            Label labelName = AddRow("Назва:", textBoxName, 0);
            Label labelActionType = AddRow("Тип акції:", comboBoxActionType, 1);
            Label labelProduct = AddRow("Товар:", comboBoxProduct, 2);
            Label labelCategory = AddRow("Категорія:", comboBoxCategory, 3);
            Label labelFuel = AddRow("Пальне:", comboBoxFuel, 4);
            Label labelConditionFuel = AddRow("Умова по пальному:", comboBoxConditionFuel, 5);
            Label labelLiters = AddRow("Від літрів:", textBoxLiters, 6);
            Label labelDiscountType = AddRow("Тип знижки:", comboBoxDiscountType, 7);
            Label labelDiscountValue = AddRow("Значення знижки:", textBoxDiscountValue, 8);
            Label labelStart = AddRow("Початок:", dateStart, 9);
            Label labelEnd = AddRow("Кінець:", dateEnd, 10);

            FlowLayoutPanel buttonsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false
            };

            Button buttonSave = CreateActionButton("Зберегти");
            Button buttonCancel = CreateActionButton("Скасувати");

            buttonSave.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            buttonsPanel.Controls.Add(buttonSave);
            buttonsPanel.Controls.Add(buttonCancel);
            buttonsPanel.Controls.Add(checkBoxIsActive);

            layout.Controls.Add(buttonsPanel, 0, 11);
            layout.SetColumnSpan(buttonsPanel, 2);

            form.AcceptButton = buttonSave;
            form.CancelButton = buttonCancel;

            void UpdateVisibleFields()
            {
                if (comboBoxActionType.SelectedItem == null)
                {
                    return;
                }

                string action = comboBoxActionType.SelectedItem.ToString();
                string targetType = GetSelectedTargetType(action);
                string discountType = GetSelectedDiscountType(action);
                if (discountType == "Manual")
                {
                    if (comboBoxDiscountType.SelectedItem != null &&
                        comboBoxDiscountType.SelectedItem.ToString() == "Гривні (грн)")
                    {
                        discountType = "Fixed";
                    }
                    else
                    {
                        discountType = "Percent";
                    }
                }

                SetRowVisible(labelProduct, comboBoxProduct, 2, targetType == "Product");
                SetRowVisible(labelCategory, comboBoxCategory, 3, targetType == "Category");
                SetRowVisible(labelFuel, comboBoxFuel, 4, targetType == "Fuel");
                SetRowVisible(labelConditionFuel, comboBoxConditionFuel, 5, UsesFuelCondition(action));
                SetRowVisible(labelLiters, textBoxLiters, 6, UsesLiters(action));
                SetRowVisible(labelDiscountType, comboBoxDiscountType, 7, UsesDiscountTypeChoice(action));

                if (targetType == "Fuel" && discountType == "Fixed")
                {
                    labelDiscountValue.Text = "Знижка грн/л:";
                }
                else if (discountType == "Percent")
                {
                    labelDiscountValue.Text = "Знижка %:";
                }
                else
                {
                    labelDiscountValue.Text = "Знижка грн:";
                }
            }

            comboBoxActionType.SelectedIndexChanged += (sender, e) =>
            {
                UpdateVisibleFields();
            };

            comboBoxDiscountType.SelectedIndexChanged += (sender, e) =>
            {
                UpdateVisibleFields();
            };

            if (row != null)
            {
                textBoxName.Text = row["Name"].ToString();
                textBoxDiscountValue.Text = row["DiscountValue"].ToString();
                dateStart.Value = Convert.ToDateTime(row["StartDate"]);
                dateEnd.Value = Convert.ToDateTime(row["EndDate"]);
                checkBoxIsActive.Checked = Convert.ToBoolean(row["IsActive"]);

                string targetType = GetStringValue("TargetType", "Product");
                string discountType = GetStringValue("DiscountType", "Percent");
                string conditionType = GetStringValue("ConditionType", "None");

                if (discountType == "Amount")
                {
                    discountType = "Fixed";
                }

                if (conditionType == "FuelLiters" && targetType == "Product")
                {
                    comboBoxActionType.SelectedItem = actionProductFuelCondition;
                }
                else if (conditionType == "FuelLiters" && targetType == "Category")
                {
                    comboBoxActionType.SelectedItem = actionCategoryFuelCondition;
                }
                else if (targetType == "Product" && discountType == "Percent")
                {
                    comboBoxActionType.SelectedItem = actionProductPercent;
                }
                else if (targetType == "Product")
                {
                    comboBoxActionType.SelectedItem = actionProductFixed;
                }
                else if (targetType == "Category" && discountType == "Percent")
                {
                    comboBoxActionType.SelectedItem = actionCategoryPercent;
                }
                else if (targetType == "Category")
                {
                    comboBoxActionType.SelectedItem = actionCategoryFixed;
                }
                else if (targetType == "Fuel" && discountType == "Percent")
                {
                    comboBoxActionType.SelectedItem = actionFuelPercent;
                }
                else
                {
                    comboBoxActionType.SelectedItem = actionFuelFixed;
                }

                if (discountType == "Fixed")
                {
                    comboBoxDiscountType.SelectedItem = "Гривні (грн)";
                }
                else
                {
                    comboBoxDiscountType.SelectedItem = "Відсоток (%)";
                }

                if (row["ProductId"] != DBNull.Value)
                {
                    SelectComboOption(comboBoxProduct, Convert.ToInt32(row["ProductId"]));
                }

                if (row["CategoryId"] != DBNull.Value)
                {
                    SelectComboOption(comboBoxCategory, Convert.ToInt32(row["CategoryId"]));
                }

                if (row["FuelId"] != DBNull.Value)
                {
                    SelectComboOption(comboBoxFuel, Convert.ToInt32(row["FuelId"]));
                }

                if (HasColumn("ConditionFuelId") && row["ConditionFuelId"] != DBNull.Value)
                {
                    SelectComboOption(comboBoxConditionFuel, Convert.ToInt32(row["ConditionFuelId"]));
                }

                if (HasColumn("ConditionMinLiters") && row["ConditionMinLiters"] != DBNull.Value)
                {
                    textBoxLiters.Text = row["ConditionMinLiters"].ToString();
                }
                else if (targetType == "Fuel")
                {
                    textBoxLiters.Text = row["MinQuantity"].ToString();
                }
                else
                {
                    textBoxLiters.Text = "25";
                }
            }
            else
            {
                comboBoxActionType.SelectedItem = actionProductPercent;
                comboBoxDiscountType.SelectedItem = "Відсоток (%)";
                textBoxDiscountValue.Text = "";
                dateStart.Value = DateTime.Today;
                dateEnd.Value = DateTime.Today.AddMonths(1);
            }

            UpdateVisibleFields();

            if (form.ShowDialog() != DialogResult.OK)
            {
                return null;
            }

            if (comboBoxActionType.SelectedItem == null)
            {
                MessageBox.Show("Оберіть тип акції.");
                return null;
            }

            if (string.IsNullOrWhiteSpace(textBoxName.Text))
            {
                MessageBox.Show("Вкажіть назву акції.");
                return null;
            }

            if (!TryParseDecimal(textBoxDiscountValue.Text, out decimal discountValue) || discountValue <= 0)
            {
                MessageBox.Show("Вкажіть коректне значення знижки.");
                return null;
            }

            string selectedAction = comboBoxActionType.SelectedItem.ToString();
            string selectedTargetType = GetSelectedTargetType(selectedAction);
            string selectedDiscountType = GetSelectedDiscountType(selectedAction);

            if (selectedDiscountType == "Percent" && discountValue > 100)
            {
                MessageBox.Show("Відсоткова знижка не може бути більшою за 100%.");
                return null;
            }

            if (dateStart.Value.Date > dateEnd.Value.Date)
            {
                MessageBox.Show("Дата початку не може бути більшою за дату завершення.");
                return null;
            }

            object productId = null;
            object categoryId = null;
            object fuelId = null;

            if (selectedTargetType == "Product")
            {
                if (comboBoxProduct.SelectedItem == null)
                {
                    MessageBox.Show("Оберіть товар.");
                    return null;
                }

                productId = ((ComboOption)comboBoxProduct.SelectedItem).Id;
            }
            else if (selectedTargetType == "Category")
            {
                if (comboBoxCategory.SelectedItem == null)
                {
                    MessageBox.Show("Оберіть категорію.");
                    return null;
                }

                categoryId = ((ComboOption)comboBoxCategory.SelectedItem).Id;
            }
            else if (selectedTargetType == "Fuel")
            {
                if (comboBoxFuel.SelectedItem == null)
                {
                    MessageBox.Show("Оберіть пальне.");
                    return null;
                }

                fuelId = ((ComboOption)comboBoxFuel.SelectedItem).Id;
            }

            decimal minQuantity = 1;
            string conditionTypeResult = "None";
            object conditionFuelId = null;
            object conditionMinLiters = null;

            if (UsesLiters(selectedAction))
            {
                if (!TryParseDecimal(textBoxLiters.Text, out decimal liters) || liters <= 0)
                {
                    MessageBox.Show("Вкажіть коректну кількість літрів.");
                    return null;
                }

                if (selectedTargetType == "Fuel")
                {
                    minQuantity = liters;
                }
                else
                {
                    conditionTypeResult = "FuelLiters";
                    conditionMinLiters = liters;

                    ComboOption conditionFuel = comboBoxConditionFuel.SelectedItem as ComboOption;

                    if (conditionFuel != null && conditionFuel.Id > 0)
                    {
                        conditionFuelId = conditionFuel.Id;
                    }
                }
            }

            return new Dictionary<string, object>
    {
        { "Name", textBoxName.Text.Trim() },
        { "TargetType", selectedTargetType },
        { "ProductId", productId },
        { "CategoryId", categoryId },
        { "FuelId", fuelId },
        { "MinQuantity", minQuantity },
        { "ConditionType", conditionTypeResult },
        { "ConditionFuelId", conditionFuelId },
        { "ConditionMinLiters", conditionMinLiters },
        { "DiscountType", selectedDiscountType },
        { "DiscountValue", discountValue },
        { "StartDate", dateStart.Value.Date },
        { "EndDate", dateEnd.Value.Date },
        { "IsActive", checkBoxIsActive.Checked ? 1 : 0 }
    };
        }

        // Допоміжні методи діалогового вікна акції

        private Label AddPromotionDialogRow(TableLayoutPanel layout, string labelText, Control inputControl, int row)
        {
            Label label = new Label
            {
                Text = labelText,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Calibri", 10F)
            };

            inputControl.Dock = DockStyle.Fill;

            layout.Controls.Add(label, 0, row);
            layout.Controls.Add(inputControl, 1, row);

            return label;
        }

        private void SetPromotionRowVisible(TableLayoutPanel layout, Label label, Control control, int row, bool visible)
        {
            label.Visible = visible;
            control.Visible = visible;

            if (row < layout.RowStyles.Count)
            {
                layout.RowStyles[row].Height = visible ? 42F : 0F;
                layout.RowStyles[row].SizeType = SizeType.Absolute;
            }
        }

        private void SelectTextValueOption(ComboBox comboBox, string value)
        {
            foreach (object item in comboBox.Items)
            {
                TextValueOption option = item as TextValueOption;

                if (option != null && option.Value == value)
                {
                    comboBox.SelectedItem = option;
                    return;
                }
            }
        }

        private void SelectPromotionActionOption(ComboBox comboBox, string code)
        {
            foreach (object item in comboBox.Items)
            {
                PromotionActionOption option = item as PromotionActionOption;

                if (option != null && option.Code == code)
                {
                    comboBox.SelectedItem = option;
                    return;
                }
            }
        }

        private string GetPromotionActionCode(DataRow row)
        {
            string targetType = row["TargetType"].ToString();
            string discountType = row["DiscountType"].ToString();
            string conditionType = row["ConditionType"].ToString();

            if (conditionType == "FuelLiters" && targetType == "Product")
            {
                return "ProductFuelCondition";
            }

            if (conditionType == "FuelLiters" && targetType == "Category")
            {
                return "CategoryFuelCondition";
            }

            if (targetType == "Product" && discountType == "Percent")
            {
                return "ProductPercent";
            }

            if (targetType == "Product")
            {
                return "ProductFixed";
            }

            if (targetType == "Category" && discountType == "Percent")
            {
                return "CategoryPercent";
            }

            if (targetType == "Category")
            {
                return "CategoryFixed";
            }

            if (targetType == "Fuel" && discountType == "Percent")
            {
                return "FuelPercent";
            }

            if (targetType == "Fuel")
            {
                return "FuelFixed";
            }

            return "ProductPercent";
        }

        //
        // Розділ "Звіти"
        //

        private void buttonGenerateReport_Click(object sender, EventArgs e)
        {
            GenerateReport();
        }

        private void GenerateReport()
        {
            if (!TryGetDateRange(dateTimePickerReportFrom, dateTimePickerReportTo, out DateTime dateFrom, out DateTime dateTo))
            {
                return;
            }

            string reportType = comboBoxReportType.SelectedItem.ToString();

            if (reportType == "Усі продажі")
            {
                LoadAllSalesReport(dateFrom, dateTo);
            }
            else if (reportType == "Продаж пального")
            {
                LoadFuelSalesReport(dateFrom, dateTo);
            }
            else if (reportType == "Продаж товарів")
            {
                LoadProductSalesReport(dateFrom, dateTo);
            }
            else if (reportType == "Зміни касирів")
            {
                LoadShiftsReport(dateFrom, dateTo);
            }
        }

        // Види звітів

        private void LoadAllSalesReport(DateTime dateFrom, DateTime dateTo)
        {
            string query = @"
                SELECT
                    s.SaleId AS 'ID',
                    s.SaleDate AS 'Дата',
                    e.FullName AS 'Працівник',
                    s.TotalPrice AS 'Сума',
                    s.PaymentType AS 'Оплата',
                    s.Status AS 'Статус'
                FROM Sales s
                INNER JOIN Employees e ON s.EmployeeId = e.EmployeeId
                WHERE s.SaleDate >= @DateFrom AND s.SaleDate < @DateTo
                ORDER BY s.SaleDate DESC;";

            LoadTableWithDateRange(query, dateFrom, dateTo, dataGridViewReports);
        }

        private void LoadFuelSalesReport(DateTime dateFrom, DateTime dateTo)
        {
            string query = @"
                SELECT
                    s.SaleId AS 'ID продажу',
                    s.SaleDate AS 'Дата',
                    e.FullName AS 'Працівник',
                    f.Name AS 'Пальне',
                    si.Quantity AS 'Літри',
                    si.Price AS 'Ціна',
                    (si.Quantity * si.Price) AS 'Сума'
                FROM SaleItems si
                INNER JOIN Sales s ON si.SaleId = s.SaleId
                INNER JOIN Employees e ON s.EmployeeId = e.EmployeeId
                INNER JOIN Fuel f ON si.FuelId = f.FuelId
                WHERE si.FuelId IS NOT NULL
                  AND s.SaleDate >= @DateFrom 
                  AND s.SaleDate < @DateTo
                ORDER BY s.SaleDate DESC;";

            LoadTableWithDateRange(query, dateFrom, dateTo, dataGridViewReports);
        }

        private void LoadProductSalesReport(DateTime dateFrom, DateTime dateTo)
        {
            string query = @"
                SELECT
                    s.SaleId AS 'ID продажу',
                    s.SaleDate AS 'Дата',
                    e.FullName AS 'Працівник',
                    p.Name AS 'Товар',
                    si.Quantity AS 'Кількість',
                    si.Price AS 'Ціна',
                    (si.Quantity * si.Price) AS 'Сума'
                FROM SaleItems si
                INNER JOIN Sales s ON si.SaleId = s.SaleId
                INNER JOIN Employees e ON s.EmployeeId = e.EmployeeId
                INNER JOIN Products p ON si.ProductId = p.ProductId
                WHERE si.ProductId IS NOT NULL
                  AND s.SaleDate >= @DateFrom 
                  AND s.SaleDate < @DateTo
                ORDER BY s.SaleDate DESC;";

            LoadTableWithDateRange(query, dateFrom, dateTo, dataGridViewReports);
        }

        private void LoadShiftsReport(DateTime dateFrom, DateTime dateTo)
        {
            string query = @"
                SELECT
                    sh.ShiftId AS 'ID',
                    e.FullName AS 'Працівник',
                    sh.StartTime AS 'Початок',
                    sh.EndTime AS 'Кінець',
                    sh.TotalMoney AS 'Сума'
                FROM Shifts sh
                INNER JOIN Employees e ON sh.EmployeeId = e.EmployeeId
                WHERE sh.StartTime >= @DateFrom 
                  AND sh.StartTime < @DateTo
                ORDER BY sh.StartTime DESC;";

            LoadTableWithDateRange(query, dateFrom, dateTo, dataGridViewReports);
        }

        //
        // Розділ "Історія продажів"
        //

        private void buttonFilterHistory_Click(object sender, EventArgs e)
        {
            LoadSalesHistory();
        }

        private void LoadSalesHistory()
        {
            if (!TryGetDateRange(dateTimePickerHistoryFrom, dateTimePickerHistoryTo, out DateTime dateFrom, out DateTime dateTo))
            {
                return;
            }

            string paymentType = comboBoxHistoryPaymentType.SelectedItem.ToString();

            string query = @"
                SELECT
                    s.SaleId AS 'ID',
                    s.SaleDate AS 'Дата',
                    e.FullName AS 'Касир',
                    s.TotalPrice AS 'Сума',
                    s.PaymentType AS 'Оплата',
                    s.Status AS 'Статус'
                FROM Sales s
                INNER JOIN Employees e ON s.EmployeeId = e.EmployeeId
                WHERE s.SaleDate >= @DateFrom
                  AND s.SaleDate < @DateTo";

            if (paymentType != "Усі")
            {
                query += " AND s.PaymentType = @PaymentType";
            }

            query += " ORDER BY s.SaleDate DESC;";

            LoadTable(query, command =>
            {
                command.Parameters.AddWithValue("@DateFrom", dateFrom.Date);
                command.Parameters.AddWithValue("@DateTo", dateTo.Date.AddDays(1));

                if (paymentType != "Усі")
                {
                    command.Parameters.AddWithValue("@PaymentType", paymentType);
                }
            }, dataGridViewSalesHistory);
        }

        // Деталі продажу

        private void buttonSaleDetails_Click(object sender, EventArgs e)
        {
            if (!TryGetSelectedId(dataGridViewSalesHistory, out int saleId))
            {
                return;
            }

            string query = @"
                SELECT
                    COALESCE(p.Name, f.Name) AS ItemName,
                    CASE 
                        WHEN p.Name IS NOT NULL THEN 'Товар'
                        ELSE 'Пальне'
                    END AS ItemType,
                    si.Quantity,
                    si.Price,
                    (si.Quantity * si.Price) AS Total
                FROM SaleItems si
                LEFT JOIN Products p ON si.ProductId = p.ProductId
                LEFT JOIN Fuel f ON si.FuelId = f.FuelId
                WHERE si.SaleId = @SaleId;";

            DataTable table = new DataTable();

            using (MySqlConnection connection = DBConnection.GetConnection())
            using (MySqlCommand command = new MySqlCommand(query, connection))
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@SaleId", saleId);
                adapter.Fill(table);
            }

            if (table.Rows.Count == 0)
            {
                MessageBox.Show("У продажі немає позицій.");
                return;
            }

            string text = "Деталі продажу №" + saleId + "\n\n";

            foreach (DataRow row in table.Rows)
            {
                text += row["ItemType"] + ": " + row["ItemName"] +
                        " | Кількість: " + row["Quantity"] +
                        " | Ціна: " + row["Price"] +
                        " | Сума: " + row["Total"] + "\n";
            }

            MessageBox.Show(text, "Деталі продажу", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //
        // Загальні методи для діалогових вікон
        //

        private Form CreateEditForm(string title)
        {
            Form form = new Form
            {
                Text = title,
                Width = 430,
                Height = 320,
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 5,
                Padding = new Padding(15)
            };

            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 130F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            for (int i = 0; i < 4; i++)
            {
                layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 45F));
            }

            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60F));

            Button buttonSave = CreateActionButton("Зберегти");
            Button buttonCancel = CreateActionButton("Скасувати");

            buttonSave.DialogResult = DialogResult.OK;
            buttonCancel.DialogResult = DialogResult.Cancel;

            FlowLayoutPanel buttonsPanel = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false
            };

            buttonsPanel.Controls.Add(buttonSave);
            buttonsPanel.Controls.Add(buttonCancel);

            layout.Controls.Add(buttonsPanel, 0, 4);
            layout.SetColumnSpan(buttonsPanel, 2);

            form.Controls.Add(layout);
            form.AcceptButton = buttonSave;
            form.CancelButton = buttonCancel;

            return form;
        }

        private void AddDialogRow(Form form, string labelText, Control inputControl, int row)
        {
            TableLayoutPanel layout = form.Controls.OfType<TableLayoutPanel>().First();

            Label label = new Label
            {
                Text = labelText,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Calibri", 10F)
            };

            inputControl.Dock = DockStyle.Fill;

            layout.Controls.Add(label, 0, row);
            layout.Controls.Add(inputControl, 1, row);
        }

        private TextBox CreateTextBox()
        {
            return new TextBox
            {
                Font = new Font("Calibri", 10F)
            };
        }

        private ComboBox CreateComboBox()
        {
            return new ComboBox
            {
                Font = new Font("Calibri", 10F),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
        }

        // Робота з ComboBox

        private void LoadOptions(ComboBox comboBox, string query)
        {
            comboBox.Items.Clear();

            using (MySqlConnection connection = DBConnection.GetConnection())
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                connection.Open();

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        comboBox.Items.Add(new ComboOption
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString()
                        });
                    }
                }
            }

            if (comboBox.Items.Count > 0)
            {
                comboBox.SelectedIndex = 0;
            }
        }

        private void SelectComboOption(ComboBox comboBox, int id)
        {
            foreach (object item in comboBox.Items)
            {
                ComboOption option = item as ComboOption;

                if (option != null && option.Id == id)
                {
                    comboBox.SelectedItem = option;
                    return;
                }
            }
        }

        //
        // Загальні методи роботи з базою даних
        //
        private void LoadTableWithDateRange(string query, DateTime dateFrom, DateTime dateTo, DataGridView dataGridView)
        {
            LoadTable(query, command =>
            {
                command.Parameters.AddWithValue("@DateFrom", dateFrom.Date);
                command.Parameters.AddWithValue("@DateTo", dateTo.Date.AddDays(1));
            }, dataGridView);
        }

        private void LoadTable(string query, Action<MySqlCommand> configureCommand, DataGridView dataGridView)
        {
            try
            {
                if (dataGridView == null)
                {
                    MessageBox.Show("Помилка: таблиця для виведення даних не створена.");
                    return;
                }

                using (MySqlConnection connection = DBConnection.GetConnection())
                using (MySqlCommand command = new MySqlCommand(query, connection))
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    configureCommand?.Invoke(command);

                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    dataGridView.DataSource = table;
                    dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dataGridView.MultiSelect = false;
                    dataGridView.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        private void ExecuteNonQuery(string query, Action<MySqlCommand> configureCommand)
        {
            using (MySqlConnection connection = DBConnection.GetConnection())
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                configureCommand?.Invoke(command);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private DataRow GetDataRow(string query, int id)
        {
            DataTable table = new DataTable();

            using (MySqlConnection connection = DBConnection.GetConnection())
            using (MySqlCommand command = new MySqlCommand(query, connection))
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@Id", id);
                adapter.Fill(table);
            }

            if (table.Rows.Count == 0)
            {
                return null;
            }

            return table.Rows[0];
        }

        //
        // Перевірки та допоміжні методи
        //

        private bool TryGetDateRange(DateTimePicker dateFromPicker, DateTimePicker dateToPicker, out DateTime dateFrom, out DateTime dateTo)
        {
            dateFrom = dateFromPicker.Value.Date;
            dateTo = dateToPicker.Value.Date;

            if (dateFrom > dateTo)
            {
                MessageBox.Show("Дата початку не може бути більшою за дату завершення.");
                return false;
            }

            return true;
        }

        private bool TryGetSelectedId(DataGridView dataGridView, out int id)
        {
            id = 0;

            if (dataGridView.CurrentRow == null)
            {
                MessageBox.Show("Оберіть запис у таблиці.");
                return false;
            }

            if (!dataGridView.Columns.Contains("ID"))
            {
                MessageBox.Show("У таблиці не знайдено стовпець ID.");
                return false;
            }

            object value = dataGridView.CurrentRow.Cells["ID"].Value;

            if (value == null || !int.TryParse(value.ToString(), out id))
            {
                MessageBox.Show("Не вдалося визначити ID запису.");
                return false;
            }

            return true;
        }

        private bool TryParseDecimal(string text, out decimal value)
        {
            text = text.Trim().Replace(',', '.');

            return decimal.TryParse(
                text,
                NumberStyles.Number,
                CultureInfo.InvariantCulture,
                out value);
        }

        private void ShowError(Exception ex)
        {
            MessageBox.Show(
                "Сталася помилка:\n\n" + ex.ToString(),
                "Помилка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        //
        // Внутрішні класи для ComboBox
        //

        private class ComboOption
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public override string ToString()
            {
                return Name;
            }
        }

        private class TextValueOption
        {
            public string Text { get; set; }
            public string Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        private class PromotionActionOption
        {
            public string Code { get; set; }
            public string Text { get; set; }
            public string TargetType { get; set; }
            public string DiscountType { get; set; }
            public bool AllowDiscountTypeChoice { get; set; }
            public bool UsesFuelCondition { get; set; }
            public bool UsesLiters { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }
    }
}