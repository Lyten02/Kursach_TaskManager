using System.ComponentModel;
using System.Text;

namespace Kursach
{
    public partial class MainForm : Form
    {
        private BindingList<TestUC> _taskList = new();
        public static BindingList<string> HistoryLog = new();
        public static readonly string Instructions = @"���������� �� ������������� ���������� '������ �����':

1. **���������� ������**:
   - ������� �������� ������ � ���� �����.
   - ���������� ������, ���� ������ ��� ���������.
   - ������� ������ '�������� ������', ����� ������ � � ������.

2. **���������� ��������**:
   - ����� �������� ������ ��� �����������, ���������� ������ ����� � ���.
   - ����� ������� ������, ������� ������ '�������' ����� � �������.
   - ������ ������������� �����������: ����������� ������������ ���� ������, � ������������� �������� ������. ����� ���������� ���������� �� ��������.

3. **���������� ������ �����**:
   - ������� '���������' � ����, ����� ��������� ������ ����� � ����.
   - �������� ������ ����� (.txt ��� .csv) � ������� ���� ��� ����������.

4. **�������� ������ �����**:
   - ������� '���������' � ����, ����� ��������� ����� ����������� ������ �����.
   - �������� ���� ������� .txt ��� .csv ��� ��������.

5. **����� �� ����������**:
   - ������� '�����' � ����, ����� ������� ����������. ���������, ��� ��� ������ ��������� ����� �������.

���� � ��� �������� ������� ��� ��������, ���������� � ������������.
������ ����������: 1.0";

        private HistoryForm historyForm;

        public MainForm()
        {
            InitializeComponent();
        }

        private void AddTaskButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AddTaskNameValue.Text))
            {
                MessageBox.Show("����������, ������� �������� ������.", "������", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TestUC childForm = null;
            childForm = new TestUC(
                () => RemoveTask(childForm),
                () =>
                {
                    HistoryLog.Add($"������ '{childForm.TaskName}' �������� ��� {(childForm.IsFinished ? "�����������" : "�������������")}.");
                    SortTaskList();
                });
            childForm.IsFinished = AddTaskCheckValue.Checked;
            childForm.TaskName = AddTaskNameValue.Text;
            _taskList.Add(childForm);
            TaskList.Controls.Add(childForm);
            childForm.Show();
            HistoryLog.Add($"������ '{childForm.TaskName}' ���������.");
            SortTaskList();
        }

        private void RemoveTask(TestUC task)
        {
            HistoryLog.Add($"������ '{task.TaskName}' �������.");
            _taskList.Remove(task);
            TaskList.Controls.Remove(task);
        }

        public void SortTaskList()
        {
            var sortedTasks = _taskList.OrderBy(tc => tc.IsFinished)
                                       .ThenBy(tc => tc.TaskName)
                                       .ToList();
            TaskList.Controls.Clear();
            _taskList.Clear();
            foreach (var task in sortedTasks)
            {
                _taskList.Add(task);
                TaskList.Controls.Add(task);
            }
        }

        private void SaveFile(string filePath, string extension)
        {
            try
            {
                using (var writer = new StreamWriter(filePath, false, new UTF8Encoding(true)))
                {
                    if (extension == ".csv")
                    {
                        writer.WriteLine("��������������;���������");
                        foreach (var task in _taskList)
                        {
                            string escapedName = task.TaskName.Replace("\"", "\"\"");
                            writer.WriteLine($"\"{escapedName}\";{task.IsFinished}");
                        }
                    }
                    else if (extension == ".txt")
                    {
                        foreach (var task in _taskList)
                        {
                            writer.WriteLine($"{task.TaskName};{task.IsFinished}");
                        }
                    }
                }

                string historyFilePath = Path.ChangeExtension(filePath, ".history" + extension);
                using (var writer = new StreamWriter(historyFilePath, false, new UTF8Encoding(true)))
                {
                    if (extension == ".csv")
                    {
                        writer.WriteLine("�������������");
                        foreach (var entry in HistoryLog)
                        {
                            string escapedEntry = entry.Replace("\"", "\"\"");
                            writer.WriteLine($"\"{escapedEntry}\"");
                        }
                    }
                    else if (extension == ".txt")
                    {
                        foreach (var entry in HistoryLog)
                        {
                            writer.WriteLine(entry);
                        }
                    }
                }

                HistoryLog.Add($"���� ������� � '{filePath}', ������� ��������� � '{historyFilePath}'.");
                MessageBox.Show("������ � ������� ������� ���������!", "�����", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"�� ������� ��������� ����. ����������, ��������� ���� � ����� �������. ������: {ex.Message}",
                    "������ ����������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadFile(string filePath, string extension)
        {
            try
            {
                if (!File.Exists(filePath)) return;
                TaskList.Controls.Clear();
                _taskList.Clear();

                using (var reader = new StreamReader(filePath))
                {
                    string line;
                    bool firstLine = true;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (extension == ".csv" && firstLine)
                        {
                            firstLine = false;
                            continue;
                        }

                        var parts = line.Split(';');
                        if (parts.Length != 2) continue;

                        var taskName = parts[0].Trim('"');
                        var isFinished = bool.Parse(parts[1]);
                        TestUC childForm = null;
                        childForm = new TestUC(
                            () => RemoveTask(childForm),
                            () =>
                            {
                                HistoryLog.Add($"������ '{taskName}' �������� ��� {(isFinished ? "�����������" : "�������������")}.");
                                SortTaskList();
                            });
                        childForm.IsFinished = isFinished;
                        childForm.TaskName = taskName;
                        _taskList.Add(childForm);
                        TaskList.Controls.Add(childForm);
                        childForm.Show();
                    }
                }

                HistoryLog.Add($"���� �������� �� '{filePath}'.");
                MessageBox.Show("������ ������� ���������!", "�����", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"�� ������� ��������� ���� �����. ����������, ���������, ��� ���� ���������� � ����� ���������� ������. ������: {ex.Message}",
                    "������ ��������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadHistoryFile(string historyFilePath, string extension)
        {
            try
            {
                if (!File.Exists(historyFilePath)) return;
                HistoryLog.Clear();

                using (var reader = new StreamReader(historyFilePath))
                {
                    string line;
                    bool firstLine = true;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (extension == ".csv" && firstLine)
                        {
                            firstLine = false;
                            continue;
                        }

                        var entry = extension == ".csv" ? line.Trim('"') : line;
                        if (!string.IsNullOrWhiteSpace(entry))
                        {
                            HistoryLog.Add(entry);
                        }
                    }
                }

                HistoryLog.Add($"������� ��������� �� '{historyFilePath}'.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"�� ������� ��������� ���� �������. ����������, ���������, ��� ���� ���������� � ����� ���������� ������. ������: {ex.Message}",
                    "������ �������� �������", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveStrip_Click(object sender, EventArgs e)
        {
            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "��������� ����� (*.txt)|*.txt|CSV ����� (*.csv)|*.csv";
                saveDialog.Title = "��������� ������ ����� � �������";
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    SaveFile(saveDialog.FileName, Path.GetExtension(saveDialog.FileName).ToLower());
                }
            }
        }

        private void LoadStrip_Click(object sender, EventArgs e)
        {
            using (var openDialog = new OpenFileDialog())
            {
                openDialog.Filter = "��������� ����� (*.txt)|*.txt|CSV ����� (*.csv)|*.csv";
                openDialog.Title = "��������� ������ ����� � �������";
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    string extension = Path.GetExtension(openDialog.FileName).ToLower();
                    LoadFile(openDialog.FileName, extension);
                    string historyFilePath = Path.ChangeExtension(openDialog.FileName, ".history" + extension);
                    LoadHistoryFile(historyFilePath, extension);
                }
            }
        }

        private void ExitStrip_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("�� �������, ��� ������ �����? ������������ ������ ����� ��������.",
                "�����", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void InfoStrip_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Instructions, "���������� �� �������������", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void HistoryStrip_Click(object sender, EventArgs e)
        {
            if (historyForm == null || historyForm.IsDisposed)
            {
                (historyForm = new HistoryForm()).Show();
            }
            else
            {
                historyForm.BringToFront();
            }
        }
    }
}