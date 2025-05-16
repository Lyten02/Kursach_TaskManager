namespace Kursach
{
    partial class TestUC
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            CheckTask = new CheckBox();
            TextTask = new TextBox();
            DeleteTaskButton = new Button();
            SuspendLayout();
            // 
            // CheckTask
            // 
            CheckTask.AutoSize = true;
            CheckTask.Location = new Point(2, 7);
            CheckTask.Margin = new Padding(2);
            CheckTask.Name = "CheckTask";
            CheckTask.Size = new Size(18, 17);
            CheckTask.TabIndex = 0;
            CheckTask.UseVisualStyleBackColor = true;
            // 
            // TextTask
            // 
            TextTask.Location = new Point(25, 3);
            TextTask.Margin = new Padding(2);
            TextTask.Name = "TextTask";
            TextTask.Size = new Size(372, 27);
            TextTask.TabIndex = 1;
            // 
            // DeleteTaskButton
            // 
            DeleteTaskButton.AutoSize = true;
            DeleteTaskButton.Location = new Point(401, 2);
            DeleteTaskButton.Margin = new Padding(2);
            DeleteTaskButton.Name = "DeleteTaskButton";
            DeleteTaskButton.Size = new Size(30, 30);
            DeleteTaskButton.TabIndex = 2;
            DeleteTaskButton.Text = "X";
            DeleteTaskButton.UseVisualStyleBackColor = true;
            // 
            // TestUC
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BorderStyle = BorderStyle.Fixed3D;
            Controls.Add(DeleteTaskButton);
            Controls.Add(TextTask);
            Controls.Add(CheckTask);
            Margin = new Padding(2);
            Name = "TestUC";
            Size = new Size(433, 34);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox CheckTask;
        private TextBox TextTask;
        private Button DeleteTaskButton;
    }
}
