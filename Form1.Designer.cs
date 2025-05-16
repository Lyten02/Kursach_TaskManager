namespace Kursach
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            TaskList = new FlowLayoutPanel();
            AddTaskButton = new Button();
            AddTaskCheckValue = new CheckBox();
            AddTaskNameValue = new TextBox();
            toolStrip1 = new ToolStrip();
            InfoStrip = new ToolStripButton();
            LoadStrip = new ToolStripButton();
            SaveStrip = new ToolStripButton();
            HistoryStrip = new ToolStripButton();
            ExitStrip = new ToolStripButton();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // TaskList
            // 
            TaskList.AutoScroll = true;
            TaskList.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TaskList.BorderStyle = BorderStyle.FixedSingle;
            TaskList.Location = new Point(0, 122);
            TaskList.Margin = new Padding(2);
            TaskList.Name = "TaskList";
            TaskList.Size = new Size(439, 465);
            TaskList.TabIndex = 0;
            // 
            // AddTaskButton
            // 
            AddTaskButton.Location = new Point(11, 91);
            AddTaskButton.Margin = new Padding(2);
            AddTaskButton.Name = "AddTaskButton";
            AddTaskButton.Size = new Size(162, 27);
            AddTaskButton.TabIndex = 1;
            AddTaskButton.Text = "Добавить Задачу";
            AddTaskButton.UseVisualStyleBackColor = true;
            AddTaskButton.Click += AddTaskButton_Click;
            // 
            // AddTaskCheckValue
            // 
            AddTaskCheckValue.AutoSize = true;
            AddTaskCheckValue.Location = new Point(11, 29);
            AddTaskCheckValue.Margin = new Padding(2);
            AddTaskCheckValue.Name = "AddTaskCheckValue";
            AddTaskCheckValue.Size = new Size(175, 24);
            AddTaskCheckValue.TabIndex = 3;
            AddTaskCheckValue.Text = "Выполнена или нет?";
            AddTaskCheckValue.TextImageRelation = TextImageRelation.ImageBeforeText;
            AddTaskCheckValue.UseVisualStyleBackColor = true;
            // 
            // AddTaskNameValue
            // 
            AddTaskNameValue.Location = new Point(11, 57);
            AddTaskNameValue.Margin = new Padding(2);
            AddTaskNameValue.Name = "AddTaskNameValue";
            AddTaskNameValue.Size = new Size(162, 27);
            AddTaskNameValue.TabIndex = 2;
            // 
            // toolStrip1
            // 
            toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { InfoStrip, LoadStrip, SaveStrip, HistoryStrip, ExitStrip });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(439, 27);
            toolStrip1.TabIndex = 8;
            toolStrip1.Text = "toolStrip1";
            // 
            // InfoStrip
            // 
            InfoStrip.DisplayStyle = ToolStripItemDisplayStyle.Text;
            InfoStrip.Image = (Image)resources.GetObject("InfoStrip.Image");
            InfoStrip.ImageTransparentColor = Color.Magenta;
            InfoStrip.Name = "InfoStrip";
            InfoStrip.Size = new Size(95, 24);
            InfoStrip.Text = "Инструкция";
            InfoStrip.Click += InfoStrip_Click;
            // 
            // LoadStrip
            // 
            LoadStrip.DisplayStyle = ToolStripItemDisplayStyle.Text;
            LoadStrip.Image = (Image)resources.GetObject("LoadStrip.Image");
            LoadStrip.ImageTransparentColor = Color.Magenta;
            LoadStrip.Name = "LoadStrip";
            LoadStrip.Size = new Size(81, 24);
            LoadStrip.Text = "Загрузить";
            LoadStrip.Click += LoadStrip_Click;
            // 
            // SaveStrip
            // 
            SaveStrip.DisplayStyle = ToolStripItemDisplayStyle.Text;
            SaveStrip.Image = (Image)resources.GetObject("SaveStrip.Image");
            SaveStrip.ImageTransparentColor = Color.Magenta;
            SaveStrip.Name = "SaveStrip";
            SaveStrip.Size = new Size(87, 24);
            SaveStrip.Text = "Сохранить";
            SaveStrip.Click += SaveStrip_Click;
            // 
            // HistoryStrip
            // 
            HistoryStrip.DisplayStyle = ToolStripItemDisplayStyle.Text;
            HistoryStrip.Image = (Image)resources.GetObject("HistoryStrip.Image");
            HistoryStrip.ImageTransparentColor = Color.Magenta;
            HistoryStrip.Name = "HistoryStrip";
            HistoryStrip.Size = new Size(72, 24);
            HistoryStrip.Text = "История";
            HistoryStrip.Click += HistoryStrip_Click;
            // 
            // ExitStrip
            // 
            ExitStrip.DisplayStyle = ToolStripItemDisplayStyle.Text;
            ExitStrip.Image = (Image)resources.GetObject("ExitStrip.Image");
            ExitStrip.ImageTransparentColor = Color.Magenta;
            ExitStrip.Name = "ExitStrip";
            ExitStrip.Size = new Size(57, 24);
            ExitStrip.Text = "Выход";
            ExitStrip.Click += ExitStrip_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(439, 587);
            Controls.Add(toolStrip1);
            Controls.Add(AddTaskNameValue);
            Controls.Add(AddTaskCheckValue);
            Controls.Add(AddTaskButton);
            Controls.Add(TaskList);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(2);
            Name = "MainForm";
            Text = "Задачник. Выполнил: Мокан Константин.";
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private FlowLayoutPanel TaskList;
        private Button AddTaskButton;
        private CheckBox AddTaskCheckValue;
        private TextBox AddTaskNameValue;
        private ToolStrip toolStrip1;
        private ToolStripButton LoadStrip;
        private ToolStripButton SaveStrip;
        private ToolStripButton ExitStrip;
        private ToolStripButton InfoStrip;
        private ToolStripButton HistoryStrip;
    }
}
