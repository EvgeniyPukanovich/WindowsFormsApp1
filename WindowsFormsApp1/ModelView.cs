using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;


namespace WindowsFormsApp1
{
    class ModelView : Form
    {
        public ModelView()
        {
            CreateMenu();
        }

        private int State = 0;

        private void CreateMenu()
        {
            State = 0;
            Button button1 = new System.Windows.Forms.Button();
            Button button2 = new System.Windows.Forms.Button();
            NumericUpDown numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            TableLayoutPanel tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            TableLayoutPanel tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            Label label1 = new System.Windows.Forms.Label();
            Label label2 = new System.Windows.Forms.Label();
            ComboBox comboBox1 = new System.Windows.Forms.ComboBox();

            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(numericUpDown1)).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right);
            button1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button1.Location = new System.Drawing.Point(128, 89);
            button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            button1.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(203, 83);
            button1.TabIndex = 0;
            button1.Text = "1v1";
            button1.UseVisualStyleBackColor = false;
            button1.Click += (sender, args) =>
            {
                Controls.Clear();
                CreateGame(new GameModel(false, true, (int)numericUpDown1.Value));
            };
            // 
            // button2
            // 
            button2.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right);
            button2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            button2.Location = new System.Drawing.Point(128, 263);
            button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            button2.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(203, 83);
            button2.TabIndex = 1;
            button2.Text = "vAI";
            button2.UseVisualStyleBackColor = false;
            button2.Click += (sender, args) =>
            {
                Controls.Clear();
                if ((string)comboBox1.SelectedItem == "Юзер")
                    CreateGame(new GameModel(true, false, (int)numericUpDown1.Value));
                else
                    CreateGame(new GameModel(true, true, (int)numericUpDown1.Value));
            };
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.Anchor = AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
            | System.Windows.Forms.AnchorStyles.Left
            | System.Windows.Forms.AnchorStyles.Right;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(label1, 0, 0);
            tableLayoutPanel2.Controls.Add(numericUpDown1, 1, 0);
            tableLayoutPanel2.Controls.Add(label2, 0, 1);
            tableLayoutPanel2.Controls.Add(comboBox1, 1, 1);
            tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel2.Location = new System.Drawing.Point(592, 296);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new System.Drawing.Size(205, 77);
            tableLayoutPanel2.TabIndex = 3;
            // 
            // label1
            // 
            label1.Anchor = (System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(3, 11);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(96, 15);
            label1.TabIndex = 0;
            label1.Text = "Размер Поля";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Anchor = (System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
            numericUpDown1.Location = new System.Drawing.Point(105, 7);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new System.Drawing.Size(97, 23);
            numericUpDown1.TabIndex = 1;
            numericUpDown1.Maximum = 5;
            numericUpDown1.Minimum = 2;
            // 
            // label2
            // 
            label2.Anchor = (System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(3, 50);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(96, 15);
            label2.TabIndex = 2;
            label2.Text = "Первый ход";
            // 
            // comboBox1
            // 
            comboBox1.Anchor = (System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new System.Drawing.Point(105, 46);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new System.Drawing.Size(97, 23);
            comboBox1.TabIndex = 3;
            comboBox1.Items.AddRange(new object[] { "ИИ", "Юзер" });
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom
            | System.Windows.Forms.AnchorStyles.Left
            | System.Windows.Forms.AnchorStyles.Right;
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.27273F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.45454F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.27273F));

            tableLayoutPanel1.Controls.Add(button1, 1, 1);
            tableLayoutPanel1.Controls.Add(button2, 1, 3);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 2, 3);

            tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 5;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            tableLayoutPanel1.Size = new System.Drawing.Size(460, 437);
            tableLayoutPanel1.TabIndex = 2;
            // 
            // Menu
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(484, 461);
            Controls.Add(tableLayoutPanel1);
            Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            Name = "Menu";
            Text = "Tic Tac Toe";
            tableLayoutPanel1.ResumeLayout(true);
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(numericUpDown1)).EndInit();
            ResumeLayout(false);

        }

        private void CreateGame(GameModel model)
        {
            State = 1;
            GameModel gameModel = model;
            TableLayoutPanel table;

            table = new TableLayoutPanel();

            table.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            for (int i = 0; i < gameModel.Side; i++)
            {
                table.RowStyles.Add(new RowStyle(SizeType.Percent, 100f / gameModel.Side));
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / gameModel.Side));
            }

            table.RowStyles.Add(new RowStyle(SizeType.Percent, 7));
            Button backButton = new Button();
            backButton.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            backButton.Height = 30;
            backButton.Margin = new Padding(3, 3, 30, 3);
            backButton.Text = "Back";
            backButton.Click += (sender, args) =>
            {
                gameModel.CloseConnection();
                Controls.Clear();
                CreateMenu();
            };
            table.Controls.Add(backButton, 0, gameModel.Side);


            table.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            List<Button> buttons = new List<Button>();
            bool[] disabledButtons = new bool[gameModel.Side * gameModel.Side];


            for (int row = 0; row < gameModel.Side; row++)
            {
                for (int column = 0; column < gameModel.Side; column++)
                {
                    int iRow = row;
                    int iColumn = column;
                    Button button = new Button();
                    button.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                    button.Dock = DockStyle.Fill;
                    button.BackgroundImageLayout = ImageLayout.Zoom;
                    button.FlatStyle = FlatStyle.Flat;
                    button.Click += (sender, args) =>
                    {
                        gameModel.MakeTurn(iRow * gameModel.Side + iColumn);
                    };
                    button.Tag = iColumn * gameModel.Side + iRow + " " + iColumn + " " + iRow;
                    buttons.Add(button);
                    table.Controls.Add(button, iColumn, iRow);
                }
            }

            FormClosing += (sender, args) => gameModel.CloseConnection();

            gameModel.BeforeSendAction += () =>
            {
                foreach (Button item in buttons)
                    item.Enabled = false;
            };

            gameModel.AfterSendAction += () =>
            {
                for (int i = 0; i < gameModel.Side * gameModel.Side; i++)
                {
                    if (!disabledButtons[i])
                        buttons[i].Enabled = true;
                }
            };

            gameModel.MadeTurn += (index, state) =>
            {
                Image cross = Properties.Resources.cross;
                Image circle = Properties.Resources.circle;
                buttons[index].BackgroundImage = state == CellState.O ? circle : cross;
                buttons[index].Enabled = false;
                disabledButtons[index] = true;
            };

            gameModel.GameEnd += (result, winPosition) =>
            {
                foreach (Button item in buttons)
                    item.Enabled = false;

                Timer timer = new Timer();
                timer.Interval = 2000;
                timer.Tick += (sender, args) =>
                {
                    timer.Stop();
                    foreach (Button item in buttons)
                    {
                        item.BackColor = SystemColors.Control;
                        item.BackgroundImage = null;
                        item.Enabled = true;
                    }
                    if (State == 1)
                        gameModel.NewGameStarted();
                };

                if (winPosition == null)
                {
                    foreach (Button item in buttons)
                    {
                        item.BackColor = Color.Yellow;
                    }
                }
                else
                {
                    foreach (int item in winPosition)
                        buttons[item].BackColor = Color.Green;
                }
                for (int i = 0; i < gameModel.Side * gameModel.Side; i++)
                {
                    disabledButtons[i] = false;
                }
                timer.Start();
            };

            gameModel.ErrorOccured += (message) =>
            {
                MessageBox.Show(message);
                Controls.Clear();
                CreateMenu();
            };
            table.Dock = DockStyle.Fill;
            Controls.Add(table);
            gameModel.FirstConnect();
        }
    }
}
