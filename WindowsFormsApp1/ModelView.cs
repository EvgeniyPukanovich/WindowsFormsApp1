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

        private void CreateMenu()
        {
            Button button1 = new System.Windows.Forms.Button();
            Button button2 = new System.Windows.Forms.Button();
            TableLayoutPanel tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            tableLayoutPanel1.SuspendLayout();
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
                CreateGame(new GameModel(CellState.X, false));
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
                CreateGame(new GameModel(CellState.X, true));
            };
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right);
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.27273F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.45454F));
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 27.27273F));
            tableLayoutPanel1.Controls.Add(button1, 1, 1);
            tableLayoutPanel1.Controls.Add(button2, 1, 3);
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
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);

        }

        private void CreateGame(GameModel model)
        {
            GameModel gameModel = model;
            TableLayoutPanel table;

            table = new TableLayoutPanel();

            table.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            for (int i = 0; i < gameModel.side; i++)
            {
                table.RowStyles.Add(new RowStyle(SizeType.Percent, 100f/gameModel.side));
                table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f /gameModel.side ));
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
            table.Controls.Add(backButton, 0, gameModel.side);


            table.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            List<Button> buttons = new List<Button>();
            bool[] disabledButtons = new bool[gameModel.side * gameModel.side];

            for (int column = 0; column < gameModel.side; column++)
            {
                for (int row = 0; row < gameModel.side; row++)
                {
                    var iRow = row;
                    var iColumn = column;
                    var button = new Button();
                    button.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                    button.Dock = DockStyle.Fill;
                    button.BackgroundImageLayout = ImageLayout.Zoom;
                    button.FlatStyle = FlatStyle.Flat;
                    button.Click += (sender, args) =>
                    {
                        gameModel.MakeTurn(iColumn * gameModel.side + iRow);
                    };
                    buttons.Add(button);
                    table.Controls.Add(button, iColumn, iRow);
                }
            }

            FormClosing += (sender, args) => gameModel.CloseConnection();

            gameModel.BeforeSendAction += () =>
            {
                foreach (var item in buttons)
                    item.Enabled = false;
            };

            gameModel.AfterSendAction += () =>
            {
                for (int i = 0; i < gameModel.side*gameModel.side; i++)
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
                foreach (var item in buttons)
                    item.Enabled = false;

                Timer timer = new Timer();
                timer.Interval = 2000;
                timer.Tick += (sender, args) =>
                {
                    timer.Stop();
                    foreach (var item in buttons)
                    {
                        item.BackColor = SystemColors.Control;
                        item.BackgroundImage = null;
                        item.Enabled = true;
                    }
                };

                if (winPosition == null)
                {
                    foreach (var item in buttons)
                    {
                        //item.BackColor = Color.Yellow;
                    }
                }
                else
                {
                    //foreach (var item in winPosition)
                        //buttons[item].BackColor = Color.Green;
                }
                for (int i = 0; i < gameModel.side*gameModel.side; i++)
                {
                    disabledButtons[i] = false;
                }
                timer.Start();
            };

            gameModel.ErrorOccured += (message) =>
            {
                MessageBox.Show(message);
                //Invoke((MethodInvoker)(() => { Controls.Clear(); CreateMenu(); }));
                Controls.Clear();
                CreateMenu();
            };
            table.Dock = DockStyle.Fill;
            Controls.Add(table);
        }
    }
}
