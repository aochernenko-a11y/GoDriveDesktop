using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using GoDriveDesktop.Models;

namespace GoDriveDesktop
{
    public partial class CashierMainForm : FullScreenForm
    {
        //
        // Службові дані форми
        //

        // дані поточного працівника та зміни
        private int currentEmployeeId;
        private string currentEmployeeName = "";
        private int currentShiftId;

        // вибрана колонка та пальне
        private int selectedColumnId;
        private string selectedFuelName = "";

        // кольори кнопок
        private readonly Color defaultButtonColor = SystemColors.MenuHighlight;
        private readonly Color selectedButtonColor = Color.ForestGreen;

        // вибрана категорія товарів
        private int selectedProductCategoryId = 0;
        private RoundedButton selectedCategoryButton = null;

        // списки товарів і позицій чека
        private List<ProductItem> currentProducts = new List<ProductItem>();
        private readonly Dictionary<int, List<PurchaseItem>> columnPurchaseItems = new Dictionary<int, List<PurchaseItem>>();

        // вибране пальне, режими заправки та типи оплати по колонках
        private readonly Dictionary<int, string> selectedFuelByColumn = new Dictionary<int, string>();
        private readonly Dictionary<int, string> columnFuelingModes = new Dictionary<int, string>();
        private readonly Dictionary<int, string> columnPaymentTypes = new Dictionary<int, string>();

        // операції заправки по колонках
        private readonly Dictionary<int, FuelingOperation> columnOperations = new Dictionary<int, FuelingOperation>();

        // таймери інтерфейсу
        private System.Windows.Forms.Timer productResizeTimer;
        private System.Windows.Forms.Timer productSearchTimer;

        // рекламний слайдер
        private readonly List<Image> promotionImages = new List<Image>();
        private int currentPromotionIndex = 0;
        private System.Windows.Forms.Timer promotionTimer = new System.Windows.Forms.Timer();

        // випадкові значення для слайдера або інтерфейсних елементів
        private readonly Random random = new Random();

        //
        // Ініціалізація форми
        //

        public CashierMainForm() : this(0, "Касир")
        {
        }

        //
        // Ініціалізація форми касира
        //
        public CashierMainForm(int employeeId, string employeeName)
        {
            // Збереження даних поточного працівника
            currentEmployeeId = employeeId;
            currentEmployeeName = employeeName;

            // Ініціалізація компонентів форми
            InitializeComponent();

            // Налаштування заголовка форми
            Text = "Каса GoDrive";

            // Початкове розміщення основних панелей
            ArrangeFuelingModePanel();
            ArrangePurchaseListPanel();
            ArrangePaymentPanel();
            ArrangeColumnButtons();
            ConfigurePurchaseListTable();

            // Ініціалізація рекламного слайдера
            InitializePromotionSlider();
            // Налаштування панелі товарів
            productsCardsPanel.FlowDirection = FlowDirection.LeftToRight;
            productsCardsPanel.WrapContents = true;
            productsCardsPanel.AutoScroll = true;
            productsCardsPanel.Padding = new Padding(10);

            // Завантаження категорій і товарів
            LoadProductCategories();
            LoadProducts();

            // Розміщення секції товарів
            ArrangeProductsSection();

            // Таймер оновлення карток товарів після зміни розміру
            productResizeTimer = new System.Windows.Forms.Timer();
            productResizeTimer.Interval = 150;
            productResizeTimer.Tick += (s, e) =>
            {
                productResizeTimer.Stop();

                if (currentProducts != null)
                {
                    ShowProductCards(currentProducts);
                }
            };

            // Обробка зміни розміру панелі товарів
            productsCardsPanel.Resize += (s, e) =>
            {
                productResizeTimer.Stop();
                productResizeTimer.Start();
            };

            // Обробка зміни розміру секції товарів
            productsSectionPanel.Resize += (s, e) =>
            {
                ArrangeProductsSection();
                ArrangeCategoryButtons();

                productResizeTimer.Stop();
                productResizeTimer.Start();
            };

            // Таймер пошуку товарів
            productSearchTimer = new System.Windows.Forms.Timer();
            productSearchTimer.Interval = 250;
            productSearchTimer.Tick += (s, e) =>
            {
                productSearchTimer.Stop();
                LoadProducts();
            };

            // Обробка введення тексту в пошук товарів
            textBoxProductSearch.TextChanged += (s, e) =>
            {
                productSearchTimer.Stop();
                productSearchTimer.Start();
            };

            // Обробка зміни розміру панелі режимів заправки
            fuelingModePanel.Resize += (s, e) =>
            {
                ArrangeFuelingModePanel();
            };

            // Обробка зміни розміру панелі чека
            purchaseListPanel.Resize += (s, e) =>
            {
                ArrangePurchaseListPanel();
            };

            // Обробка зміни розміру панелі оплати
            paymentPanel.Resize += (s, e) =>
            {
                ArrangePaymentPanel();
            };

            // Обробка зміни розміру панелі колонок
            columnsPanel.Resize += (s, e) =>
            {
                ArrangeColumnButtons();
            };

            // Початкове відображення панелі заправки
            fuelingModePanel.Visible = true;
            SetFuelingPanelPreviewMode();

            // Оновлення кнопок пального після зміни розміру панелі
            fuelTypesPanel.Resize += (s, e) =>
            {
                if (selectedColumnId != 0)
                {
                    ShowFuelButtons(GetFuelNamesForColumn(selectedColumnId));
                }
            };

            // Ініціалізація таймера процесу заправки
            if (fuelingTimer == null)
            {
                fuelingTimer = new System.Windows.Forms.Timer();
            }

            fuelingTimer.Interval = 100;
            fuelingTimer.Tick += FuelingTimer_Tick;

            // Перевірка або створення активної зміни касира
            EnsureActiveShift();
        }

        //
        // Розміщення та оформлення елементів інтерфейсу
        //

        private void ArrangeColumnButtons()
        {
            Button[] columnButtons =
            {
                buttonColumn1,
                buttonColumn2,
                buttonColumn3,
                buttonColumn4,
                buttonColumn5
            };

            int columnsCount = columnsPanel.Width >= 300 ? 2 : 1;
            int margin = 10;
            int buttonHeight = 65;

            int buttonWidth = (columnsPanel.ClientSize.Width - margin * (columnsCount + 1)) / columnsCount;

            for (int i = 0; i < columnButtons.Length; i++)
            {
                int column = i % columnsCount;
                int row = i / columnsCount;

                columnButtons[i].Width = buttonWidth;
                columnButtons[i].Height = buttonHeight;

                columnButtons[i].Left = margin + column * (buttonWidth + margin);
                columnButtons[i].Top = margin + row * (buttonHeight + margin);
            }
        }

        private void ArrangeFuelingModePanel()
        {
            int padding = 18;
            int gap = 12;

            int panelWidth = fuelingModePanel.ClientSize.Width;
            int panelHeight = fuelingModePanel.ClientSize.Height;

            // Розмір кнопки "До повного"
            int squareSize = Math.Max(70, Math.Min(110, panelHeight / 4));

            buttonFillFull.Left = padding;
            buttonFillFull.Top = padding;
            buttonFillFull.Width = squareSize;
            buttonFillFull.Height = squareSize;

            // Права частина: TextBox + кнопка "Заправити"
            int rightBlockLeft = buttonFillFull.Right + gap;
            int rightBlockWidth = panelWidth - rightBlockLeft - padding;

            int innerGap = 8;
            int textBoxHeight = (squareSize - innerGap) / 2;
            int specificButtonHeight = squareSize - textBoxHeight - innerGap;

            textBoxSpecificAmount.Multiline = true;
            textBoxSpecificAmount.Left = rightBlockLeft;
            textBoxSpecificAmount.Top = padding;
            textBoxSpecificAmount.Width = rightBlockWidth;
            textBoxSpecificAmount.Height = textBoxHeight;

            buttonSpecificAmount.Left = rightBlockLeft;
            buttonSpecificAmount.Top = textBoxSpecificAmount.Bottom + innerGap;
            buttonSpecificAmount.Width = rightBlockWidth;
            buttonSpecificAmount.Height = specificButtonHeight;

            // Великий лічильник літрів праворуч
            labelFuelingCounter.AutoSize = false;
            labelFuelingCounter.Left = padding;
            labelFuelingCounter.Top = buttonFillFull.Bottom + 25;
            labelFuelingCounter.Width = panelWidth - padding * 2;
            labelFuelingCounter.Height = Math.Max(100, panelHeight / 3);
            labelFuelingCounter.TextAlign = ContentAlignment.MiddleRight;
            labelFuelingCounter.Font = new Font("Segoe UI", Math.Max(42, panelHeight / 7), FontStyle.Regular);

            // Кнопка "До чека" знизу
            buttonAddFuelToReceipt.Left = padding;
            buttonAddFuelToReceipt.Width = panelWidth - padding * 2;
            buttonAddFuelToReceipt.Height = Math.Max(42, panelHeight / 10);
            buttonAddFuelToReceipt.Top = panelHeight - buttonAddFuelToReceipt.Height - padding;
        }

        private void ArrangeProductsSection()
        {
            int padding = 12;
            int gap = 10;

            int panelWidth = productsSectionPanel.ClientSize.Width;
            int panelHeight = productsSectionPanel.ClientSize.Height;

            int searchHeight = 40;
            int categoryWidth = 140;

            textBoxProductSearch.Left = padding;
            textBoxProductSearch.Top = padding;
            textBoxProductSearch.Width = panelWidth - padding * 2;
            textBoxProductSearch.Height = searchHeight;

            categoryPanel.Left = padding;
            categoryPanel.Top = textBoxProductSearch.Bottom + gap;
            categoryPanel.Width = categoryWidth;
            categoryPanel.Height = panelHeight - categoryPanel.Top - padding;

            productsCardsPanel.Left = categoryPanel.Right + gap;
            productsCardsPanel.Top = categoryPanel.Top;
            productsCardsPanel.Width = panelWidth - productsCardsPanel.Left - padding;
            productsCardsPanel.Height = categoryPanel.Height;
        }

        private void ArrangeCategoryButtons()
        {
            int margin = 10;
            int buttonHeight = 45;

            for (int i = 0; i < categoryPanel.Controls.Count; i++)
            {
                Control control = categoryPanel.Controls[i];

                control.Left = margin;
                control.Top = margin + i * (buttonHeight + margin);
                control.Width = categoryPanel.ClientSize.Width - margin * 2;
                control.Height = buttonHeight;
            }
        }

        private void ArrangePurchaseListPanel()
        {
            int padding = 15;
            int gap = 10;

            labelPurchaseListTitle.Left = padding;
            labelPurchaseListTitle.Top = padding;
            labelPurchaseListTitle.Width = purchaseListPanel.ClientSize.Width - padding * 2;
            labelPurchaseListTitle.Height = 25;

            buttonRemovePurchaseItem.Width = purchaseListPanel.ClientSize.Width - padding * 2;
            buttonRemovePurchaseItem.Height = 35;
            buttonRemovePurchaseItem.Left = padding;
            buttonRemovePurchaseItem.Top = purchaseListPanel.ClientSize.Height - buttonRemovePurchaseItem.Height - padding;

            dataGridViewPurchaseList.Left = padding;
            dataGridViewPurchaseList.Top = labelPurchaseListTitle.Bottom + gap;
            dataGridViewPurchaseList.Width = purchaseListPanel.ClientSize.Width - padding * 2;
            dataGridViewPurchaseList.Height = buttonRemovePurchaseItem.Top - dataGridViewPurchaseList.Top - gap;
        }
        
        private void ArrangePaymentPanel()
        {
            int padding = 15;
            int gap = 10;

            labelTotalText.Left = padding;
            labelTotalText.Top = padding;
            labelTotalText.Width = paymentPanel.ClientSize.Width - padding * 2;
            labelTotalText.Height = 25;

            labelTotalPrice.AutoSize = false;
            labelTotalPrice.Left = padding;
            labelTotalPrice.Top = labelTotalText.Bottom + 5;
            labelTotalPrice.Width = paymentPanel.ClientSize.Width - padding * 2;
            labelTotalPrice.Height = 55;
            labelTotalPrice.TextAlign = ContentAlignment.MiddleRight;
            labelTotalPrice.Font = new Font("Segoe UI", 26, FontStyle.Regular);

            int paymentButtonWidth = (paymentPanel.ClientSize.Width - padding * 2 - gap) / 2;
            int paymentButtonHeight = 45;

            buttonPaymentCash.Left = padding;
            buttonPaymentCash.Top = labelTotalPrice.Bottom + gap;
            buttonPaymentCash.Width = paymentButtonWidth;
            buttonPaymentCash.Height = paymentButtonHeight;

            buttonPaymentCard.Left = buttonPaymentCash.Right + gap;
            buttonPaymentCard.Top = buttonPaymentCash.Top;
            buttonPaymentCard.Width = paymentButtonWidth;
            buttonPaymentCard.Height = paymentButtonHeight;

            int bottomButtonWidth = (paymentPanel.ClientSize.Width - padding * 2 - gap) / 2;
            int bottomButtonHeight = 50;

            buttonCancelSale.Left = padding;
            buttonCancelSale.Top = paymentPanel.ClientSize.Height - bottomButtonHeight - padding;
            buttonCancelSale.Width = bottomButtonWidth;
            buttonCancelSale.Height = bottomButtonHeight;

            buttonConfirmSale.Left = buttonCancelSale.Right + gap;
            buttonConfirmSale.Top = buttonCancelSale.Top;
            buttonConfirmSale.Width = bottomButtonWidth;
            buttonConfirmSale.Height = bottomButtonHeight;
        }
        
        private void ConfigurePurchaseListTable()
        {
            dataGridViewPurchaseList.Columns.Clear();

            dataGridViewPurchaseList.AllowUserToAddRows = false;
            dataGridViewPurchaseList.AllowUserToDeleteRows = false;
            dataGridViewPurchaseList.ReadOnly = true;
            dataGridViewPurchaseList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewPurchaseList.MultiSelect = false;
            dataGridViewPurchaseList.RowHeadersVisible = false;
            dataGridViewPurchaseList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dataGridViewPurchaseList.Columns.Add("ItemType", "Тип");
            dataGridViewPurchaseList.Columns.Add("ItemName", "Назва");
            dataGridViewPurchaseList.Columns.Add("Quantity", "К-сть");
            dataGridViewPurchaseList.Columns.Add("Price", "Ціна");
            dataGridViewPurchaseList.Columns.Add("Total", "Сума");
            dataGridViewPurchaseList.Columns.Add("Promotion", "Акція");

            dataGridViewPurchaseList.Columns["ItemType"].FillWeight = 65;
            dataGridViewPurchaseList.Columns["ItemName"].FillWeight = 120;
            dataGridViewPurchaseList.Columns["Quantity"].FillWeight = 70;
            dataGridViewPurchaseList.Columns["Price"].FillWeight = 75;
            dataGridViewPurchaseList.Columns["Total"].FillWeight = 80;
            dataGridViewPurchaseList.Columns["Promotion"].FillWeight = 120;

            dataGridViewPurchaseList.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dataGridViewPurchaseList.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
        }

        // загальні допоміжні методи для кольору та стану кнопок

        private void SetButtonColor(Control control, Color color)
        {
            if (control is RoundedButton roundedButton)
            {
                roundedButton.NormalColor = color;
                roundedButton.BackColor = color;
                roundedButton.Invalidate();
                return;
            }

            if (control is Button button)
            {
                button.UseVisualStyleBackColor = false;
                button.BackColor = color;
                button.Invalidate();
            }
        }

        private void UpdateColumnButtonsView()
        {
            SetButtonColor(buttonColumn1, defaultButtonColor);
            SetButtonColor(buttonColumn2, defaultButtonColor);
            SetButtonColor(buttonColumn3, defaultButtonColor);
            SetButtonColor(buttonColumn4, defaultButtonColor);
            SetButtonColor(buttonColumn5, defaultButtonColor);

            if (selectedColumnId == 1)
            {
                SetButtonColor(buttonColumn1, selectedButtonColor);
            }
            else if (selectedColumnId == 2)
            {
                SetButtonColor(buttonColumn2, selectedButtonColor);
            }
            else if (selectedColumnId == 3)
            {
                SetButtonColor(buttonColumn3, selectedButtonColor);
            }
            else if (selectedColumnId == 4)
            {
                SetButtonColor(buttonColumn4, selectedButtonColor);
            }
            else if (selectedColumnId == 5)
            {
                SetButtonColor(buttonColumn5, selectedButtonColor);
            }
        }

        private void UpdateFuelingModeButtonsView()
        {
            SetButtonColor(buttonFillFull, defaultButtonColor);
            SetButtonColor(buttonSpecificAmount, defaultButtonColor);

            if (selectedColumnId == 0)
            {
                return;
            }

            if (!columnFuelingModes.ContainsKey(selectedColumnId))
            {
                return;
            }

            if (columnFuelingModes[selectedColumnId] == "Full")
            {
                SetButtonColor(buttonFillFull, selectedButtonColor);
            }
            else if (columnFuelingModes[selectedColumnId] == "Specific")
            {
                SetButtonColor(buttonSpecificAmount, selectedButtonColor);
            }
        }

        private void UpdatePaymentButtonsView()
        {
            SetButtonColor(buttonPaymentCash, defaultButtonColor);
            SetButtonColor(buttonPaymentCard, defaultButtonColor);

            if (selectedColumnId == 0)
            {
                return;
            }

            if (!columnPaymentTypes.ContainsKey(selectedColumnId))
            {
                return;
            }

            if (columnPaymentTypes[selectedColumnId] == "Готівка")
            {
                SetButtonColor(buttonPaymentCash, selectedButtonColor);
            }
            else if (columnPaymentTypes[selectedColumnId] == "Картка")
            {
                SetButtonColor(buttonPaymentCard, selectedButtonColor);
            }
        }

        private void UpdateTotalPriceLabel()
        {
            if (selectedColumnId == 0 || !columnPurchaseItems.ContainsKey(selectedColumnId))
            {
                labelTotalPrice.Text = "0.00 грн";
                return;
            }

            decimal total = columnPurchaseItems[selectedColumnId].Sum(item => item.Total);

            labelTotalPrice.Text = total.ToString("0.00") + " грн";
        }

        //
        // Вибір колонки та пального
        //

        // Вибір колонки

        private void SelectColumn(int columnId)
        {
            selectedColumnId = columnId;

            if (selectedFuelByColumn.ContainsKey(selectedColumnId))
            {
                selectedFuelName = selectedFuelByColumn[selectedColumnId];
            }
            else
            {
                selectedFuelName = "";
            }

            UpdateFuelingPanelForSelectedColumn();
            ShowFuelButtons(GetFuelNamesForColumn(selectedColumnId));
            ShowPurchaseListForSelectedColumn();
            UpdateColumnButtonsView();
            UpdateFuelingModeButtonsView();
            UpdatePaymentButtonsView();
        }

        private void UpdateFuelingPanelForSelectedColumn()
        {
            textBoxSpecificAmount.Clear();
            labelFuelingCounter.Text = "0 л";

            if (selectedColumnId == 0)
            {
                SetFuelingPanelPreviewMode();
                return;
            }

            if (columnOperations.ContainsKey(selectedColumnId))
            {
                FuelingOperation operation = columnOperations[selectedColumnId];

                buttonFillFull.Enabled = false;
                textBoxSpecificAmount.Enabled = false;
                buttonSpecificAmount.Enabled = false;
                buttonAddFuelToReceipt.Enabled = operation.State == ColumnState.WaitingNozzleBack;

                if (operation.State == ColumnState.Fueling)
                {
                    labelFuelingCounter.Text = operation.CurrentValue + " л";
                }

                return;
            }

            if (selectedFuelByColumn.ContainsKey(selectedColumnId))
            {
                SetFuelingPanelActiveMode();
            }
            else
            {
                SetFuelingPanelPreviewMode();
            }
        }

        private void buttonColumn1_Click(object sender, EventArgs e)
        {
            SelectColumn(1);
        }

        private void buttonColumn2_Click(object sender, EventArgs e)
        {
            SelectColumn(2);
        }

        private void buttonColumn3_Click(object sender, EventArgs e)
        {
            SelectColumn(3);
        }

        private void buttonColumn4_Click(object sender, EventArgs e)
        {
            SelectColumn(4);
        }

        private void buttonColumn5_Click(object sender, EventArgs e)
        {
            SelectColumn(5);
        }

        // робота зі списком пального
        private string[] GetFuelNamesForColumn(int columnId)
        {
            if (columnId == 1)
            {
                return new string[] { "А-95", "А-95 Turbo", "А-92" };
            }

            if (columnId == 2)
            {
                return new string[] { "А-95", "ДП" };
            }

            if (columnId == 3)
            {
                return new string[] { "А-92", "ДП" };
            }

            if (columnId == 4)
            {
                return new string[] { "А-95", "А-95 Turbo", "ДП" };
            }

            if (columnId == 5)
            {
                return new string[] { "Газ" };
            }

            return Array.Empty<string>();
        }

        private void ShowFuelButtons(string[] fuelNames)
        {
            fuelTypesPanel.Controls.Clear();

            int margin = 10;
            int buttonHeight = 50;
            int columnsCount = 1;

            int buttonWidth =
                (fuelTypesPanel.ClientSize.Width - margin * (columnsCount + 1)) / columnsCount;

            for (int i = 0; i < fuelNames.Length; i++)
            {
                string fuelName = fuelNames[i];

                int column = i % columnsCount;
                int row = i / columnsCount;

                RoundedButton fuelButton = new RoundedButton();

                fuelButton.Text = fuelName;
                fuelButton.Width = buttonWidth;
                fuelButton.Height = buttonHeight;

                fuelButton.Left = margin + column * (buttonWidth + margin);
                fuelButton.Top = margin + row * (buttonHeight + margin);

                if (columnOperations.ContainsKey(selectedColumnId)
                    && columnOperations[selectedColumnId].FuelName == fuelName)
                {
                    FuelingOperation operation = columnOperations[selectedColumnId];
                    operation.Button = fuelButton;

                    if (operation.State == ColumnState.Fueling)
                    {
                        SetFuelButtonColor(fuelButton, Color.FromArgb(220, 70, 70));
                    }
                    else if (operation.State == ColumnState.WaitingNozzleBack)
                    {
                        SetFuelButtonColor(fuelButton, Color.FromArgb(80, 180, 100));
                    }
                    else if (operation.State == ColumnState.WaitingPayment)
                    {
                        SetFuelButtonColor(fuelButton, Color.FromArgb(230, 170, 60));
                    }
                }
                else if (selectedFuelByColumn.ContainsKey(selectedColumnId)
                         && selectedFuelByColumn[selectedColumnId] == fuelName)
                {
                    SetFuelButtonColor(fuelButton, selectedButtonColor);
                }

                fuelButton.Click += (sender, e) =>
                {
                    SelectFuel(fuelName, fuelButton);
                };

                fuelTypesPanel.Controls.Add(fuelButton);
            }
        }

        private void SelectFuel(string fuelName, RoundedButton fuelButton)
        {
            if (selectedColumnId == 0)
            {
                MessageBox.Show("Спочатку оберіть колонку");
                return;
            }

            if (IsColumnBlocked(selectedColumnId))
            {
                MessageBox.Show("Ця колонка вже зайнята. Спочатку завершіть заправку та оплату.");
                return;
            }

            selectedFuelName = fuelName;

            selectedFuelByColumn[selectedColumnId] = fuelName;

            decimal fuelAmount = GetFuelAmountFromDatabase(fuelName);

            if (fuelAmount > 100)
            {
                SetFuelingPanelActiveMode();
                ShowFuelButtons(GetFuelNamesForColumn(selectedColumnId));
            }
            else
            {
                MessageBox.Show("Недостатньо пального для заправки");
            }
        }

        private RoundedButton FindVisibleFuelButton(string fuelName)
        {
            foreach (Control control in fuelTypesPanel.Controls)
            {
                if (control is RoundedButton button && button.Text == fuelName)
                {
                    return button;
                }
            }

            return null;
        }

        // стан панелі заправки
        private void SetFuelingPanelPreviewMode()
        {
            buttonFillFull.Enabled = false;
            textBoxSpecificAmount.Enabled = false;
            buttonSpecificAmount.Enabled = false;
            buttonAddFuelToReceipt.Enabled = false;

            textBoxSpecificAmount.Clear();
            labelFuelingCounter.Text = "0 л";
        }
        private void SetFuelingPanelActiveMode()
        {
            buttonFillFull.Enabled = true;
            textBoxSpecificAmount.Enabled = true;
            buttonSpecificAmount.Enabled = true;
            buttonAddFuelToReceipt.Enabled = true;

            labelFuelingCounter.Text = "0 л";
        }
        private void SetFuelButtonColor(RoundedButton button, Color color)
        {
            button.NormalColor = color;
            button.HoverColor = color;
            button.BackColor = color;
            button.Invalidate();
        }
        private bool IsColumnBlocked(int columnId)
        {
            if (!columnOperations.ContainsKey(columnId))
            {
                return false;
            }

            ColumnState state = columnOperations[columnId].State;

            return state == ColumnState.Fueling
                || state == ColumnState.WaitingNozzleBack
                || state == ColumnState.WaitingPayment;
        }
        //
        // Заправка автомобіля
        //
        private void buttonFillFull_Click(object sender, EventArgs e)
        {
            columnFuelingModes[selectedColumnId] = "Full";
            UpdateFuelingModeButtonsView();

            if (selectedColumnId == 0 || selectedFuelName == "")
            {
                MessageBox.Show("Спочатку оберіть колонку та пальне");
                return;
            }

            if (IsColumnBlocked(selectedColumnId))
            {
                MessageBox.Show("Ця колонка зайнята або очікує оплату");
                return;
            }

            decimal fuelAmount = GetFuelAmountFromDatabase(selectedFuelName);

            if (fuelAmount <= 100)
            {
                MessageBox.Show("Недостатньо пального");
                return;
            }

            int randomLiters = random.Next(20, 81);

            FuelingOperation operation = new FuelingOperation
            {
                ColumnId = selectedColumnId,
                FuelName = selectedFuelName,
                CurrentValue = 0,
                TargetValue = randomLiters,
                FinalLiters = randomLiters,
                IsCountdownMode = false,
                State = ColumnState.Fueling,
                Button = FindVisibleFuelButton(selectedFuelName)
            };

            columnOperations[selectedColumnId] = operation;

            if (operation.Button != null)
            {
                SetFuelButtonColor(operation.Button, Color.FromArgb(220, 70, 70));
            }

            fuelingTimer.Start();
        }
        
        private void buttonSpecificAmount_Click(object sender, EventArgs e)
        {
            columnFuelingModes[selectedColumnId] = "Specific";
            UpdateFuelingModeButtonsView();

            if (selectedColumnId == 0 || selectedFuelName == "")
            {
                MessageBox.Show("Спочатку оберіть колонку та пальне");
                return;
            }

            if (IsColumnBlocked(selectedColumnId))
            {
                MessageBox.Show("Ця колонка зайнята або очікує оплату");
                return;
            }

            if (!decimal.TryParse(textBoxSpecificAmount.Text, out decimal liters))
            {
                MessageBox.Show("Введіть коректну кількість літрів");
                return;
            }

            if (liters <= 0)
            {
                MessageBox.Show("Кількість літрів має бути більшою за 0");
                return;
            }

            decimal fuelAmount = GetFuelAmountFromDatabase(selectedFuelName);

            if (fuelAmount < liters)
            {
                MessageBox.Show("Недостатньо пального");
                return;
            }


            FuelingOperation operation = new FuelingOperation
            {
                ColumnId = selectedColumnId,
                FuelName = selectedFuelName,
                CurrentValue = liters,
                TargetValue = 0,
                FinalLiters = liters,
                IsCountdownMode = true,
                State = ColumnState.Fueling,
                Button = FindVisibleFuelButton(selectedFuelName)
            };

            columnOperations[selectedColumnId] = operation;

            if (operation.Button != null)
            {
                SetFuelButtonColor(operation.Button, Color.FromArgb(220, 70, 70));
            }

            fuelingTimer.Start();
        }
        
        // процес заправки
        
        private void FuelingTimer_Tick(object sender, EventArgs e)
        {
            bool hasActiveOperations = false;

            foreach (FuelingOperation operation in columnOperations.Values)
            {
                if (operation.State != ColumnState.Fueling)
                {
                    continue;
                }

                hasActiveOperations = true;

                if (operation.IsCountdownMode)
                {
                    operation.CurrentValue--;

                    if (operation.CurrentValue <= operation.TargetValue)
                    {
                        operation.CurrentValue = operation.TargetValue;
                        CompleteFueling(operation);
                    }
                }
                else
                {
                    operation.CurrentValue++;

                    if (operation.CurrentValue >= operation.TargetValue)
                    {
                        operation.CurrentValue = operation.TargetValue;
                        CompleteFueling(operation);
                    }
                }

                if (operation.ColumnId == selectedColumnId)
                {
                    labelFuelingCounter.Text = operation.CurrentValue + " л";
                }
            }

            if (!hasActiveOperations)
            {
                fuelingTimer.Stop();
            }
        }
        
        private void CompleteFueling(FuelingOperation operation)
        {
            operation.State = ColumnState.WaitingNozzleBack;

            DecreaseFuelAmountInDatabase(operation.FuelName, operation.FinalLiters);

            if (operation.Button != null)
            {
                SetFuelButtonColor(operation.Button, Color.FromArgb(80, 180, 100));
            }

            if (operation.ColumnId == selectedColumnId)
            {
                labelFuelingCounter.Text = "0 л";
            }

        }
        
        // робота з кількістю пального в базі даних
        private decimal GetFuelAmountFromDatabase(string fuelName)
        {
            using (MySqlConnection connection = DBConnection.GetConnection())
            {
                connection.Open();

                string query = @"
            SELECT AmountLiters
            FROM Fuel
            WHERE Name = @fuelName;
        ";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@fuelName", fuelName);

                    object result = command.ExecuteScalar();

                    if (result == null)
                    {
                        return 0;
                    }

                    return Convert.ToDecimal(result);
                }
            }
        }

        private bool DecreaseFuelAmountInDatabase(string fuelName, decimal liters)
        {
            using (MySqlConnection connection = DBConnection.GetConnection())
            {
                connection.Open();

                string query = @"
            UPDATE Fuel
            SET AmountLiters = AmountLiters - @liters
            WHERE Name = @fuelName AND AmountLiters >= @liters;
        ";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@liters", liters);
                    command.Parameters.AddWithValue("@fuelName", fuelName);

                    int affectedRows = command.ExecuteNonQuery();

                    return affectedRows > 0;
                }
            }
        }
        
        private bool ReturnFuelAmountToDatabase(string fuelName, decimal liters)
        {
            using (MySqlConnection connection = DBConnection.GetConnection())
            {
                connection.Open();

                string query = @"
            UPDATE Fuel
            SET AmountLiters = AmountLiters + @Liters
            WHERE Name = @FuelName;
        ";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Liters", liters);
                    command.Parameters.AddWithValue("@FuelName", fuelName);

                    return command.ExecuteNonQuery() > 0;
                }
            }
        }

        //
        // Товари магазину
        //

        // категорії товарів

        private void LoadProductCategories()
        {
            categoryPanel.Controls.Clear();

            AddCategoryButton("Все", 0, 0);

            using (MySqlConnection connection = DBConnection.GetConnection())
            {
                connection.Open();

                string query = @"
            SELECT CategoryId, Name
            FROM ProductCategories
            ORDER BY Name;
        ";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    int index = 1;

                    while (reader.Read())
                    {
                        int categoryId = reader.GetInt32("CategoryId");
                        string categoryName = reader.GetString("Name");

                        AddCategoryButton(categoryName, categoryId, index);

                        index++;
                    }
                }
            }
            ArrangeCategoryButtons();
        }

        private void AddCategoryButton(string text, int categoryId, int index)
        {
            int margin = 10;
            int buttonHeight = 45;

            RoundedButton button = new RoundedButton();

            button.Text = text;
            button.Width = categoryPanel.ClientSize.Width - margin * 2;
            button.Height = buttonHeight;
            button.Left = margin;
            button.Top = margin + index * (buttonHeight + margin);

            button.Click += (sender, e) =>
            {
                selectedProductCategoryId = categoryId;

                if (selectedCategoryButton != null)
                {
                    SetButtonColor(selectedCategoryButton, defaultButtonColor);
                }

                selectedCategoryButton = button;
                SetButtonColor(selectedCategoryButton, selectedButtonColor);

                LoadProducts();
            };

            categoryPanel.Controls.Add(button);
        }

        // завантаження та відображення товарів

        private void LoadProducts()
        {
            currentProducts.Clear();

            string searchText = textBoxProductSearch.Text.Trim();

            using (MySqlConnection connection = DBConnection.GetConnection())
            {
                connection.Open();

                string query = @"
            SELECT 
                p.ProductId,
                p.CategoryId,
                p.Name,
                p.Price,
                p.Quantity,
                c.Name AS CategoryName
            FROM Products p
            INNER JOIN ProductCategories c ON p.CategoryId = c.CategoryId
            WHERE 
                (@categoryId = 0 OR p.CategoryId = @categoryId)
                AND
                (
                    @searchText = ''
                    OR p.Name LIKE CONCAT('%', @searchText, '%')
                    OR CAST(p.ProductId AS CHAR) = @searchText
                )
            ORDER BY p.Name;
        ";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@categoryId", selectedProductCategoryId);
                    command.Parameters.AddWithValue("@searchText", searchText);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ProductItem product = new ProductItem
                            {
                                ProductId = reader.GetInt32("ProductId"),
                                CategoryId = reader.GetInt32("CategoryId"),
                                Name = reader.GetString("Name"),
                                Price = reader.GetDecimal("Price"),
                                Quantity = reader.GetInt32("Quantity"),
                                CategoryName = reader.GetString("CategoryName")
                            };

                            currentProducts.Add(product);
                        }
                    }
                }
            }

            ShowProductCards(currentProducts);
        }

        private void ShowProductCards(List<ProductItem> products)
        {
            productsCardsPanel.SuspendLayout();
            productsCardsPanel.Visible = false;

            productsCardsPanel.Controls.Clear();

            int gap = 10;
            int minCardWidth = 220;
            int cardHeight = 130;

            int panelWidth = productsCardsPanel.ClientSize.Width - productsCardsPanel.Padding.Horizontal;

            if (panelWidth <= 0)
            {
                productsCardsPanel.Visible = true;
                productsCardsPanel.ResumeLayout();
                return;
            }

            int columnsCount = Math.Max(1, panelWidth / (minCardWidth + gap));
            int cardWidth = (panelWidth - gap * columnsCount) / columnsCount;

            if (cardWidth < minCardWidth)
            {
                columnsCount = 1;
                cardWidth = panelWidth - gap;
            }

            foreach (ProductItem product in products)
            {
                Panel card = CreateProductCard(product, cardWidth, cardHeight);
                card.Margin = new Padding(5);
                productsCardsPanel.Controls.Add(card);
            }

            productsCardsPanel.Visible = true;
            productsCardsPanel.ResumeLayout();
        }

        private Panel CreateProductCard(ProductItem product, int width, int height)
        {
            Panel card = new Panel();

            card.Width = width;
            card.Height = height;
            card.Margin = new Padding(10);
            card.BackColor = Color.White;
            card.BorderStyle = BorderStyle.FixedSingle;
            card.Cursor = Cursors.Hand;

            PictureBox pictureBox = new PictureBox();
            pictureBox.Left = 10;
            pictureBox.Top = 10;
            pictureBox.Width = 70;
            pictureBox.Height = 70;
            pictureBox.BackColor = Color.Gainsboro;
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

            Label labelName = new Label();
            labelName.Text = product.Name;
            labelName.Left = 90;
            labelName.Top = 10;
            labelName.Width = width - 100;
            labelName.Height = 40;
            labelName.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            Label labelPrice = new Label();
            labelPrice.Text = product.Price.ToString("0.00") + " грн";
            labelPrice.Left = 90;
            labelPrice.Top = 55;
            labelPrice.Width = width - 100;
            labelPrice.Height = 25;
            labelPrice.Font = new Font("Segoe UI", 10);

            Label labelQuantity = new Label();
            labelQuantity.Text = "Залишок: " + product.Quantity;
            labelQuantity.Left = 10;
            labelQuantity.Top = 95;
            labelQuantity.Width = width - 20;
            labelQuantity.Height = 25;

            card.Controls.Add(pictureBox);
            card.Controls.Add(labelName);
            card.Controls.Add(labelPrice);
            card.Controls.Add(labelQuantity);

            if (product.Quantity <= 0)
            {
                card.BackColor = Color.LightGray;
                card.Cursor = Cursors.Default;
            }
            else
            {
                ConnectProductCardClick(card, product);
            }

            return card;
        }

        private void ConnectProductCardClick(Control control, ProductItem product)
        {
            control.Click += (sender, e) =>
            {
                AddProductToSelectedColumnReceipt(product);
            };

            foreach (Control childControl in control.Controls)
            {
                ConnectProductCardClick(childControl, product);
            }
        }

        // додавання товару до чека

        private void AddProductToSelectedColumnReceipt(ProductItem product)
        {
            if (selectedColumnId == 0)
            {
                MessageBox.Show("Спочатку оберіть колонку");
                return;
            }

            if (product.Quantity <= 0)
            {
                MessageBox.Show("Товару немає в наявності");
                return;
            }

            if (!columnPurchaseItems.ContainsKey(selectedColumnId))
            {
                columnPurchaseItems[selectedColumnId] = new List<PurchaseItem>();
            }

            List<PurchaseItem> purchaseItems = columnPurchaseItems[selectedColumnId];

            decimal alreadyAddedQuantity = purchaseItems
                .Where(item => item.ItemType == "Товар" && item.ProductId == product.ProductId)
                .Sum(item => item.Quantity);

            if (alreadyAddedQuantity + 1 > product.Quantity)
            {
                MessageBox.Show("Недостатня кількість товару на складі");
                return;
            }

            string promotionName;
            decimal finalPrice = ApplyPromotionToProductPrice(product, out promotionName);

            PurchaseItem existingItem = purchaseItems.FirstOrDefault(item =>
                item.ItemType == "Товар"
                && item.ProductId == product.ProductId
                && item.Price == finalPrice
                && item.PromotionName == promotionName
            );

            if (existingItem != null)
            {
                existingItem.Quantity += 1;
                existingItem.Total = existingItem.Quantity * existingItem.Price;
            }
            else
            {
                purchaseItems.Add(new PurchaseItem
                {
                    ProductId = product.ProductId,
                    ItemType = "Товар",
                    ItemName = product.Name,
                    Quantity = 1,
                    Unit = "шт",
                    Price = finalPrice,
                    Total = finalPrice,
                    PromotionName = promotionName
                });
            }

            if (!string.IsNullOrWhiteSpace(promotionName))
            {
                MessageBox.Show("Застосовано акцію: " + promotionName);
            }

            ShowPurchaseListForSelectedColumn();
        }

        //
        // Акції та знижки
        //

        private decimal ApplyPromotionToProductPrice(ProductItem product, out string promotionName)
        {
            promotionName = "";

            if (selectedColumnId == 0)
            {
                return product.Price;
            }

            using (MySqlConnection connection = DBConnection.GetConnection())
            {
                connection.Open();

                string query = @"
            SELECT
                PromotionId,
                Name,
                TargetType,
                ConditionType,
                ConditionFuelId,
                ConditionMinLiters,
                DiscountType,
                DiscountValue,
                ConditionProductId,
                ConditionMinProductQuantity,
                MaxDiscountQuantity
            FROM Promotions
            WHERE IsActive = 1
              AND CURDATE() BETWEEN StartDate AND EndDate
              AND MinQuantity <= 1
              AND
              (
                    (TargetType = 'Product' AND ProductId = @ProductId)
                    OR
                    (TargetType = 'Category' AND CategoryId = @CategoryId)
              )
            ORDER BY 
                CASE WHEN ConditionType = 'FuelLiters' THEN 1 ELSE 0 END DESC,
                DiscountValue DESC;
        ";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ProductId", product.ProductId);
                    command.Parameters.AddWithValue("@CategoryId", product.CategoryId);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string currentPromotionName = reader.GetString("Name");
                            string conditionType = reader.GetString("ConditionType");

                            if (conditionType == "FuelLiters")
                            {
                                decimal conditionMinLiters = 0;

                                if (reader["ConditionMinLiters"] != DBNull.Value)
                                {
                                    conditionMinLiters = reader.GetDecimal("ConditionMinLiters");
                                }

                                decimal fuelLitersInCheck;

                                if (reader["ConditionFuelId"] == DBNull.Value)
                                {
                                    fuelLitersInCheck = GetFuelLitersInCurrentCheck(selectedColumnId, null);
                                }
                                else
                                {
                                    int conditionFuelId = reader.GetInt32("ConditionFuelId");
                                    fuelLitersInCheck = GetFuelLitersInCurrentCheck(selectedColumnId, conditionFuelId);
                                }

                                if (fuelLitersInCheck < conditionMinLiters)
                                {
                                    continue;
                                }
                            }

                            if (conditionType == "ProductCombo")
                            {
                                if (reader["ConditionProductId"] == DBNull.Value)
                                {
                                    continue;
                                }

                                int conditionProductId = reader.GetInt32("ConditionProductId");

                                decimal conditionMinProductQuantity = 1;

                                if (reader["ConditionMinProductQuantity"] != DBNull.Value)
                                {
                                    conditionMinProductQuantity = reader.GetDecimal("ConditionMinProductQuantity");
                                }

                                decimal productQuantityInCheck = GetProductQuantityInCurrentCheck(
                                    selectedColumnId,
                                    conditionProductId
                                );

                                if (productQuantityInCheck < conditionMinProductQuantity)
                                {
                                    continue;
                                }
                            }

                            if (reader["MaxDiscountQuantity"] != DBNull.Value)
                            {
                                decimal maxDiscountQuantity = reader.GetDecimal("MaxDiscountQuantity");

                                decimal alreadyDiscountedQuantity = GetProductQuantityWithPromotion(
                                    selectedColumnId,
                                    product.ProductId,
                                    currentPromotionName
                                );

                                if (alreadyDiscountedQuantity >= maxDiscountQuantity)
                                {
                                    continue;
                                }
                            }

                            string discountType = reader.GetString("DiscountType");
                            decimal discountValue = reader.GetDecimal("DiscountValue");

                            decimal newPrice = product.Price;

                            if (discountType == "Percent")
                            {
                                newPrice = product.Price - product.Price * discountValue / 100m;
                            }
                            else if (discountType == "Fixed")
                            {
                                newPrice = product.Price - discountValue;
                            }

                            if (newPrice < 0)
                            {
                                newPrice = 0;
                            }

                            promotionName = currentPromotionName;
                            return newPrice;
                        }
                    }
                }
            }

            return product.Price;
        }

        // допоміжні методи для перевірки умов акцій

        private decimal GetProductQuantityInCurrentCheck(int columnId, int productId)
        {
            if (!columnPurchaseItems.ContainsKey(columnId))
            {
                return 0;
            }

            return columnPurchaseItems[columnId]
                .Where(item =>
                    item.ItemType == "Товар"
                    && item.ProductId == productId)
                .Sum(item => item.Quantity);
        }

        private decimal GetFuelLitersInCurrentCheck(int columnId, int? fuelId)
        {
            if (!columnPurchaseItems.ContainsKey(columnId))
            {
                return 0;
            }

            decimal liters = 0;

            foreach (PurchaseItem item in columnPurchaseItems[columnId])
            {
                if (item.ItemType != "Паливо")
                {
                    continue;
                }

                if (fuelId == null)
                {
                    liters += item.Quantity;
                }
                else
                {
                    int currentFuelId = GetFuelIdByName(item.ItemName);

                    if (currentFuelId == fuelId.Value)
                    {
                        liters += item.Quantity;
                    }
                }
            }

            return liters;
        }

        private decimal GetProductQuantityWithPromotion(int columnId, int productId, string promotionName)
        {
            if (!columnPurchaseItems.ContainsKey(columnId))
            {
                return 0;
            }

            return columnPurchaseItems[columnId]
                .Where(item =>
                    item.ItemType == "Товар"
                    && item.ProductId == productId
                    && item.PromotionName == promotionName)
                .Sum(item => item.Quantity);
        }

        private int GetFuelIdByName(string fuelName)
        {
            using (MySqlConnection connection = DBConnection.GetConnection())
            {
                connection.Open();

                string query = @"
            SELECT FuelId
            FROM Fuel
            WHERE Name = @FuelName
            LIMIT 1;
        ";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FuelName", fuelName);

                    object result = command.ExecuteScalar();

                    if (result == null)
                    {
                        return 0;
                    }

                    return Convert.ToInt32(result);
                }
            }
        }

        //
        // Рекламний слайдер
        //

        private void InitializePromotionSlider()
        {
            promotionImages.Add(Properties.Resources.Promo1);
            promotionImages.Add(Properties.Resources.Promo2);
            promotionImages.Add(Properties.Resources.Promo3);

            promotionTimer.Interval = 20000;
            promotionTimer.Tick += (s, e) =>
            {
                ShowNextPromotion();
            };

            ShowPromotion(0);
            promotionTimer.Start();
        }

        private void ShowPromotion(int index)
        {
            if (promotionImages.Count == 0)
            {
                return;
            }

            currentPromotionIndex = index;
            pictureBoxPromotion.Image = promotionImages[currentPromotionIndex];

            UpdatePromotionDots();
        }

        private void ShowNextPromotion()
        {
            currentPromotionIndex++;

            if (currentPromotionIndex >= promotionImages.Count)
            {
                currentPromotionIndex = 0;
            }

            ShowPromotion(currentPromotionIndex);
        }

        private void UpdatePromotionDots()
        {
            labelPromoDot1.ForeColor = SystemColors.MenuHighlight;
            labelPromoDot2.ForeColor = SystemColors.MenuHighlight;
            labelPromoDot3.ForeColor = SystemColors.MenuHighlight;

            if (currentPromotionIndex == 0)
            {
                labelPromoDot1.ForeColor = Color.ForestGreen;
            }
            else if (currentPromotionIndex == 1)
            {
                labelPromoDot2.ForeColor = Color.ForestGreen;
            }
            else if (currentPromotionIndex == 2)
            {
                labelPromoDot3.ForeColor = Color.ForestGreen;
            }
        }

        //
        // Чек покупки
        //

        // додавання пального до чека

        private void buttonAddFuelToReceipt_Click(object sender, EventArgs e)
        {
            if (selectedColumnId == 0)
            {
                MessageBox.Show("Спочатку оберіть колонку");
                return;
            }

            if (!columnOperations.ContainsKey(selectedColumnId))
            {
                MessageBox.Show("На цій колонці немає завершеної заправки");
                return;
            }

            FuelingOperation operation = columnOperations[selectedColumnId];

            if (operation.State != ColumnState.WaitingNozzleBack)
            {
                MessageBox.Show("Спочатку потрібно завершити заправку");
                return;
            }

            decimal pricePerLiter = GetFuelPriceFromDatabase(operation.FuelName);
            decimal itemTotal = operation.FinalLiters * pricePerLiter;

            if (!columnPurchaseItems.ContainsKey(selectedColumnId))
            {
                columnPurchaseItems[selectedColumnId] = new List<PurchaseItem>();
            }

            columnPurchaseItems[selectedColumnId].Add(new PurchaseItem
            {
                ItemType = "Паливо",
                ItemName = operation.FuelName,
                Quantity = operation.FinalLiters,
                Unit = "л",
                Price = pricePerLiter,
                Total = itemTotal
            });

            operation.State = ColumnState.WaitingPayment;

            if (operation.Button != null)
            {
                SetFuelButtonColor(operation.Button, Color.FromArgb(230, 170, 60));
            }

            ShowPurchaseListForSelectedColumn();

        }

        private decimal GetFuelPriceFromDatabase(string fuelName)
        {
            using (MySqlConnection connection = DBConnection.GetConnection())
            {
                connection.Open();

                string query = @"
            SELECT PricePerLiter
            FROM Fuel
            WHERE Name = @fuelName;
        ";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@fuelName", fuelName);

                    object result = command.ExecuteScalar();

                    if (result == null)
                    {
                        return 0;
                    }

                    return Convert.ToDecimal(result);
                }
            }
        }

        // відображення та зміна чека

        private void ShowPurchaseListForSelectedColumn()
        {
            dataGridViewPurchaseList.Rows.Clear();

            if (selectedColumnId == 0)
            {
                labelTotalPrice.Text = "0.00 грн";
                return;
            }

            if (!columnPurchaseItems.ContainsKey(selectedColumnId))
            {
                columnPurchaseItems[selectedColumnId] = new List<PurchaseItem>();
            }

            foreach (PurchaseItem item in columnPurchaseItems[selectedColumnId])
            {
                dataGridViewPurchaseList.Rows.Add(
                    item.ItemType,
                    item.ItemName,
                    item.Quantity.ToString("0.##") + " " + item.Unit,
                    item.Price.ToString("0.00") + " грн",
                    item.Total.ToString("0.00") + " грн",
                    item.PromotionName
                );
            }

            UpdateTotalPriceLabel();
            UpdatePaymentButtonsView();
        }

        private void buttonRemovePurchaseItem_Click(object sender, EventArgs e)
        {
            if (selectedColumnId == 0)
            {
                MessageBox.Show("Спочатку оберіть колонку");
                return;
            }

            if (!columnPurchaseItems.ContainsKey(selectedColumnId))
            {
                MessageBox.Show("Список покупки порожній");
                return;
            }

            if (dataGridViewPurchaseList.SelectedRows.Count == 0)
            {
                MessageBox.Show("Оберіть позицію для видалення");
                return;
            }

            int rowIndex = dataGridViewPurchaseList.SelectedRows[0].Index;

            if (rowIndex < 0 || rowIndex >= columnPurchaseItems[selectedColumnId].Count)
            {
                return;
            }

            PurchaseItem removedItem = columnPurchaseItems[selectedColumnId][rowIndex];

            if (removedItem.ItemType == "Паливо")
            {
                ReturnFuelAmountToDatabase(removedItem.ItemName, removedItem.Quantity);

                if (columnOperations.ContainsKey(selectedColumnId))
                {
                    columnOperations.Remove(selectedColumnId);
                }

                ShowFuelButtons(GetFuelNamesForColumn(selectedColumnId));
            }

            columnPurchaseItems[selectedColumnId].RemoveAt(rowIndex);

            ShowPurchaseListForSelectedColumn();
        }

        private void buttonCancelSale_Click(object sender, EventArgs e)
        {
            if (selectedColumnId == 0)
            {
                MessageBox.Show("Спочатку оберіть колонку");
                return;
            }

            bool hasPurchaseItems = columnPurchaseItems.ContainsKey(selectedColumnId)
                && columnPurchaseItems[selectedColumnId].Count > 0;

            bool hasFuelingOperation = columnOperations.ContainsKey(selectedColumnId);

            if (!hasPurchaseItems && !hasFuelingOperation)
            {
                MessageBox.Show("Для цієї колонки немає замовлення для скасування");
                return;
            }

            FuelingOperation currentOperation = null;

            if (hasFuelingOperation)
            {
                currentOperation = columnOperations[selectedColumnId];

                if (currentOperation.State == ColumnState.Fueling)
                {
                    MessageBox.Show("Не можна скасувати замовлення під час заправки. Дочекайтесь завершення наливу.");
                    return;
                }
            }

            DialogResult result = MessageBox.Show(
                "Скасувати замовлення для колонки №" + selectedColumnId + "?",
                "Підтвердження",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes)
            {
                return;
            }

            bool fuelReturnedFromPurchaseList = false;

            if (hasPurchaseItems)
            {
                foreach (PurchaseItem item in columnPurchaseItems[selectedColumnId].ToList())
                {
                    if (item.ItemType == "Паливо")
                    {
                        ReturnFuelAmountToDatabase(item.ItemName, item.Quantity);
                        fuelReturnedFromPurchaseList = true;
                    }
                }

                columnPurchaseItems[selectedColumnId].Clear();
            }

            if (currentOperation != null
                && currentOperation.State == ColumnState.WaitingNozzleBack
                && !fuelReturnedFromPurchaseList)
            {
                ReturnFuelAmountToDatabase(currentOperation.FuelName, currentOperation.FinalLiters);
            }

            columnPaymentTypes.Remove(selectedColumnId);
            columnOperations.Remove(selectedColumnId);
            selectedFuelByColumn.Remove(selectedColumnId);
            columnFuelingModes.Remove(selectedColumnId);

            labelFuelingCounter.Text = "0 л";

            ShowFuelButtons(GetFuelNamesForColumn(selectedColumnId));
            UpdateFuelingModeButtonsView();
            ShowPurchaseListForSelectedColumn();
            LoadProducts();

            MessageBox.Show("Замовлення скасовано");
        }

        //
        // Оплата продажу
        //

        // вибір способу оплати

        private void buttonPaymentCash_Click(object sender, EventArgs e)
        {
            if (selectedColumnId == 0)
            {
                MessageBox.Show("Спочатку оберіть колонку");
                return;
            }

            columnPaymentTypes[selectedColumnId] = "Готівка";
            UpdatePaymentButtonsView();
        }

        private void buttonPaymentCard_Click(object sender, EventArgs e)
        {
            if (selectedColumnId == 0)
            {
                MessageBox.Show("Спочатку оберіть колонку");
                return;
            }

            columnPaymentTypes[selectedColumnId] = "Картка";
            UpdatePaymentButtonsView();
        }

        // підтвердження продажу

        private void buttonConfirmSale_Click(object sender, EventArgs e)
        {
            if (selectedColumnId == 0)
            {
                MessageBox.Show("Спочатку оберіть колонку");
                return;
            }

            if (!columnPurchaseItems.ContainsKey(selectedColumnId) || columnPurchaseItems[selectedColumnId].Count == 0)
            {
                MessageBox.Show("Список покупки для цієї колонки порожній");
                return;
            }

            if (!columnPaymentTypes.ContainsKey(selectedColumnId) || columnPaymentTypes[selectedColumnId] == "")
            {
                MessageBox.Show("Оберіть спосіб оплати");
                return;
            }

            EnsureActiveShift();

            if (currentEmployeeId <= 0 || currentShiftId <= 0)
            {
                MessageBox.Show("Не вдалося визначити касира або активну зміну.");
                return;
            }

            List<PurchaseItem> items = columnPurchaseItems[selectedColumnId];
            decimal total = items.Sum(item => item.Total);
            string paymentType = columnPaymentTypes[selectedColumnId];

            try
            {
                int saleId = SaveSaleToDatabase(items, total, paymentType);

                MessageBox.Show(
                    "Продаж успішно збережено.\n" +
                    "Номер продажу: " + saleId + "\n" +
                    "Колонка: №" + selectedColumnId + "\n" +
                    "Сума: " + total.ToString("0.00") + " грн\n" +
                    "Оплата: " + paymentType
                );

                columnPurchaseItems[selectedColumnId].Clear();
                columnPaymentTypes.Remove(selectedColumnId);

                if (columnOperations.ContainsKey(selectedColumnId))
                {
                    columnOperations.Remove(selectedColumnId);
                }

                selectedFuelByColumn.Remove(selectedColumnId);

                ShowFuelButtons(GetFuelNamesForColumn(selectedColumnId));
                columnFuelingModes.Remove(selectedColumnId);
                UpdateFuelingModeButtonsView();

                ShowPurchaseListForSelectedColumn();
                LoadProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Продаж не було збережено.\n\n" + ex.Message,
                    "Помилка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // збереження продажу в базу даних

        private int SaveSaleToDatabase(List<PurchaseItem> items, decimal total, string paymentType)
        {
            using (MySqlConnection connection = DBConnection.GetConnection())
            {
                connection.Open();

                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int shiftId = EnsureActiveShift(connection, transaction);

                        string saleQuery = @"
                    INSERT INTO Sales (EmployeeId, ShiftId, TotalPrice, PaymentType, Status)
                    VALUES (@EmployeeId, @ShiftId, @TotalPrice, @PaymentType, 'Завершено');
                ";

                        int saleId;

                        using (MySqlCommand command = new MySqlCommand(saleQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@EmployeeId", currentEmployeeId);
                            command.Parameters.AddWithValue("@ShiftId", shiftId);
                            command.Parameters.AddWithValue("@TotalPrice", total);
                            command.Parameters.AddWithValue("@PaymentType", paymentType);
                            command.ExecuteNonQuery();

                            saleId = Convert.ToInt32(command.LastInsertedId);
                        }

                        foreach (PurchaseItem item in items)
                        {
                            if (item.ItemType == "Товар")
                            {
                                int productQuantity = Convert.ToInt32(item.Quantity);

                                string updateProductQuery = @"
                            UPDATE Products
                            SET Quantity = Quantity - @Quantity
                            WHERE ProductId = @ProductId AND Quantity >= @Quantity;
                        ";

                                using (MySqlCommand command = new MySqlCommand(updateProductQuery, connection, transaction))
                                {
                                    command.Parameters.AddWithValue("@Quantity", productQuantity);
                                    command.Parameters.AddWithValue("@ProductId", item.ProductId);

                                    int affectedRows = command.ExecuteNonQuery();

                                    if (affectedRows == 0)
                                    {
                                        throw new Exception("Недостатня кількість товару: " + item.ItemName);
                                    }
                                }

                                InsertSaleItem(connection, transaction, saleId, item.ProductId, null, item.Quantity, item.Price);
                            }
                            else if (item.ItemType == "Паливо")
                            {
                                int fuelId = GetFuelIdByName(connection, transaction, item.ItemName);
                                InsertSaleItem(connection, transaction, saleId, null, fuelId, item.Quantity, item.Price);
                            }
                        }

                        string updateShiftQuery = @"
                    UPDATE Shifts
                    SET TotalMoney = TotalMoney + @Total
                    WHERE ShiftId = @ShiftId;
                ";

                        using (MySqlCommand command = new MySqlCommand(updateShiftQuery, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@Total", total);
                            command.Parameters.AddWithValue("@ShiftId", shiftId);
                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();
                        currentShiftId = shiftId;

                        return saleId;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        private void InsertSaleItem(MySqlConnection connection, MySqlTransaction transaction, int saleId, int? productId, int? fuelId, decimal quantity, decimal price)
        {
            string query = @"
                    INSERT INTO SaleItems (SaleId, ProductId, FuelId, Quantity, Price)
                    VALUES (@SaleId, @ProductId, @FuelId, @Quantity, @Price);
                    ";

            using (MySqlCommand command = new MySqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@SaleId", saleId);
                command.Parameters.AddWithValue("@ProductId", productId.HasValue ? (object)productId.Value : DBNull.Value);
                command.Parameters.AddWithValue("@FuelId", fuelId.HasValue ? (object)fuelId.Value : DBNull.Value);
                command.Parameters.AddWithValue("@Quantity", quantity);
                command.Parameters.AddWithValue("@Price", price);
                command.ExecuteNonQuery();
            }
        }

        private int GetFuelIdByName(MySqlConnection connection, MySqlTransaction transaction, string fuelName)
        {
            string query = @"
                SELECT FuelId
                FROM Fuel
                WHERE Name = @FuelName
                LIMIT 1;
                ";

            using (MySqlCommand command = new MySqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@FuelName", fuelName);

                object result = command.ExecuteScalar();

                if (result == null)
                {
                    throw new Exception("Пальне не знайдено в базі: " + fuelName);
                }

                return Convert.ToInt32(result);
            }
        }

        //
        // Зміна касира
        //

        // активна зміна

        private void EnsureActiveShift()
        {
            try
            {
                using (MySqlConnection connection = DBConnection.GetConnection())
                {
                    connection.Open();
                    currentShiftId = EnsureActiveShift(connection, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Не вдалося відкрити зміну касира.\n\n" + ex.Message,
                    "Помилка зміни",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private int EnsureActiveShift(MySqlConnection connection, MySqlTransaction transaction)
        {
            if (currentEmployeeId <= 0)
            {
                string employeeQuery = @"
            SELECT e.EmployeeId, e.FullName
            FROM Employees e
            INNER JOIN Positions p ON e.PositionId = p.PositionId
            WHERE p.Name = 'Касир'
            ORDER BY e.EmployeeId
            LIMIT 1;
        ";

                using (MySqlCommand command = new MySqlCommand(employeeQuery, connection, transaction))
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        currentEmployeeId = reader.GetInt32("EmployeeId");
                        currentEmployeeName = reader.GetString("FullName");
                    }
                }
            }

            if (currentEmployeeId <= 0)
            {
                throw new Exception("У базі не знайдено касира.");
            }

            string openShiftQuery = @"
        SELECT ShiftId
        FROM Shifts
        WHERE EmployeeId = @EmployeeId AND EndTime IS NULL
        ORDER BY ShiftId DESC
        LIMIT 1;
    ";

            using (MySqlCommand command = new MySqlCommand(openShiftQuery, connection, transaction))
            {
                command.Parameters.AddWithValue("@EmployeeId", currentEmployeeId);
                object result = command.ExecuteScalar();

                if (result != null)
                {
                    return Convert.ToInt32(result);
                }
            }

            string insertShiftQuery = @"
        INSERT INTO Shifts (EmployeeId, StartTime, TotalMoney)
        VALUES (@EmployeeId, NOW(), 0);
    ";

            using (MySqlCommand command = new MySqlCommand(insertShiftQuery, connection, transaction))
            {
                command.Parameters.AddWithValue("@EmployeeId", currentEmployeeId);
                command.ExecuteNonQuery();

                return Convert.ToInt32(command.LastInsertedId);
            }
        }

        // звіти та історія

        private void buttonShiftReport_Click(object sender, EventArgs e)
        {
            EnsureActiveShift();

            if (currentShiftId <= 0)
            {
                MessageBox.Show("Активну зміну не знайдено.");
                return;
            }

            MessageBox.Show(GetShiftReportText(currentShiftId), "Звіт зміни", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonCashierSalesHistory_Click(object sender, EventArgs e)
        {
            EnsureActiveShift();

            if (currentEmployeeId <= 0)
            {
                MessageBox.Show("Касира не знайдено.");
                return;
            }

            DataTable table = new DataTable();

            using (MySqlConnection connection = DBConnection.GetConnection())
            using (MySqlCommand command = new MySqlCommand(@"
        SELECT
            s.SaleId AS 'ID',
            s.SaleDate AS 'Дата',
            s.TotalPrice AS 'Сума',
            s.PaymentType AS 'Оплата',
            s.Status AS 'Статус'
        FROM Sales s
        WHERE s.EmployeeId = @EmployeeId
        ORDER BY s.SaleDate DESC
        LIMIT 50;
    ", connection))
            using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@EmployeeId", currentEmployeeId);
                adapter.Fill(table);
            }

            ShowTableDialog("Історія продажів касира", table);
        }

        private void buttonCloseShift_Click(object sender, EventArgs e)
        {
            EnsureActiveShift();

            if (currentShiftId <= 0)
            {
                MessageBox.Show("Активну зміну не знайдено.");
                return;
            }

            bool hasUnfinishedOperations = columnOperations.Any() ||
                columnPurchaseItems.Any(pair => pair.Value.Count > 0);

            if (hasUnfinishedOperations)
            {
                MessageBox.Show("Не можна завершити зміну: є незавершені продажі або заправки.");
                return;
            }

            DialogResult result = MessageBox.Show(
                "Завершити поточну зміну?",
                "Підтвердження",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result != DialogResult.Yes)
            {
                return;
            }

            string reportText = GetShiftReportText(currentShiftId);

            using (MySqlConnection connection = DBConnection.GetConnection())
            using (MySqlCommand command = new MySqlCommand(@"
        UPDATE Shifts
        SET EndTime = NOW()
        WHERE ShiftId = @ShiftId AND EndTime IS NULL;
    ", connection))
            {
                command.Parameters.AddWithValue("@ShiftId", currentShiftId);
                connection.Open();
                command.ExecuteNonQuery();
            }

            currentShiftId = 0;

            MessageBox.Show(reportText + "\n\nЗміну завершено.", "Звіт зміни", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string GetShiftReportText(int shiftId)
        {
            using (MySqlConnection connection = DBConnection.GetConnection())
            {
                connection.Open();

                string header = "Касир: " + currentEmployeeName + "\n";
                header += "Зміна №" + shiftId + "\n\n";

                using (MySqlCommand command = new MySqlCommand(@"
            SELECT
                COUNT(*) AS SalesCount,
                COALESCE(SUM(TotalPrice), 0) AS TotalMoney
            FROM Sales
            WHERE ShiftId = @ShiftId;
        ", connection))
                {
                    command.Parameters.AddWithValue("@ShiftId", shiftId);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            header += "Кількість продажів: " + reader.GetInt32("SalesCount") + "\n";
                            header += "Сума продажів: " + reader.GetDecimal("TotalMoney").ToString("0.00") + " грн\n";
                        }
                    }
                }

                using (MySqlCommand command = new MySqlCommand(@"
            SELECT COALESCE(SUM(si.Quantity), 0)
            FROM SaleItems si
            INNER JOIN Sales s ON si.SaleId = s.SaleId
            WHERE s.ShiftId = @ShiftId AND si.FuelId IS NOT NULL;
        ", connection))
                {
                    command.Parameters.AddWithValue("@ShiftId", shiftId);
                    decimal liters = Convert.ToDecimal(command.ExecuteScalar());
                    header += "Продано пального: " + liters.ToString("0.##") + " л\n";
                }

                using (MySqlCommand command = new MySqlCommand(@"
            SELECT COALESCE(SUM(si.Quantity), 0)
            FROM SaleItems si
            INNER JOIN Sales s ON si.SaleId = s.SaleId
            WHERE s.ShiftId = @ShiftId AND si.ProductId IS NOT NULL;
        ", connection))
                {
                    command.Parameters.AddWithValue("@ShiftId", shiftId);
                    decimal products = Convert.ToDecimal(command.ExecuteScalar());
                    header += "Продано товарів: " + products.ToString("0.##") + " шт";
                }

                return header;
            }
        }

        private void ShowTableDialog(string title, DataTable table)
        {
            Form form = new Form();
            form.Text = title;
            form.Width = 850;
            form.Height = 500;
            form.StartPosition = FormStartPosition.CenterParent;

            DataGridView dataGridView = new DataGridView();
            dataGridView.Dock = DockStyle.Fill;
            dataGridView.ReadOnly = true;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.DataSource = table;

            form.Controls.Add(dataGridView);
            form.ShowDialog();
        }

        //
        // Навігація та вихід
        //

        private void buttonChangeUser_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
