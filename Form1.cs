using System.ComponentModel;
using System.Text;

namespace Kursach
{
    public partial class MainForm : Form
    {
        private BindingList<TestUC> _taskList = new();
        public static BindingList<string> HistoryLog = new();
        public static readonly string Instructions = @"Инструкция по использованию приложения 'Список задач':

1. **Добавление задачи**:
   - Введите название задачи в поле ввода.
   - Установите флажок, если задача уже выполнена.
   - Нажмите кнопку 'Добавить задачу', чтобы внести её в список.

2. **Управление задачами**:
   - Чтобы отметить задачу как выполненную, установите флажок рядом с ней.
   - Чтобы удалить задачу, нажмите кнопку 'Удалить' рядом с задачей.
   - Задачи автоматически сортируются: выполненные перемещаются вниз списка, а невыполненные остаются сверху. Также сортировка происходит по алфавиту.

3. **Сохранение списка задач**:
   - Нажмите 'Сохранить' в меню, чтобы сохранить список задач в файл.
   - Выберите формат файла (.txt или .csv) и укажите путь для сохранения.

4. **Загрузка списка задач**:
   - Нажмите 'Загрузить' в меню, чтобы загрузить ранее сохраненный список задач.
   - Выберите файл формата .txt или .csv для загрузки.

5. **Выход из приложения**:
   - Нажмите 'Выход' в меню, чтобы закрыть приложение. Убедитесь, что все данные сохранены перед выходом.

Если у вас возникли вопросы или проблемы, обратитесь к разработчику.
Версия приложения: 1.0";

        private HistoryForm historyForm;

        public MainForm()
        {
            InitializeComponent();
        }

        private void AddTaskButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AddTaskNameValue.Text))
            {
                MessageBox.Show("Пожалуйста, введите название задачи.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            TestUC childForm = null;
            childForm = new TestUC(
                () => RemoveTask(childForm),
                () =>
                {
                    HistoryLog.Add($"Задача '{childForm.TaskName}' отмечена как {(childForm.IsFinished ? "выполненная" : "невыполненная")}.");
                    SortTaskList();
                });
            childForm.IsFinished = AddTaskCheckValue.Checked;
            childForm.TaskName = AddTaskNameValue.Text;
            _taskList.Add(childForm);
            TaskList.Controls.Add(childForm);
            childForm.Show();
            HistoryLog.Add($"Задача '{childForm.TaskName}' добавлена.");
            SortTaskList();
        }

        private void RemoveTask(TestUC task)
        {
            HistoryLog.Add($"Задача '{task.TaskName}' удалена.");
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
                        writer.WriteLine("НазваниеЗадачи;Выполнена");
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
                        writer.WriteLine("ЗаписьИстории");
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

                HistoryLog.Add($"Файл сохранён в '{filePath}', история сохранена в '{historyFilePath}'.");
                MessageBox.Show("Задачи и история успешно сохранены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось сохранить файл. Пожалуйста, проверьте путь и права доступа. Ошибка: {ex.Message}",
                    "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                                HistoryLog.Add($"Задача '{taskName}' отмечена как {(isFinished ? "выполненная" : "невыполненная")}.");
                                SortTaskList();
                            });
                        childForm.IsFinished = isFinished;
                        childForm.TaskName = taskName;
                        _taskList.Add(childForm);
                        TaskList.Controls.Add(childForm);
                        childForm.Show();
                    }
                }

                HistoryLog.Add($"Файл загружен из '{filePath}'.");
                MessageBox.Show("Задачи успешно загружены!", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось загрузить файл задач. Пожалуйста, убедитесь, что файл существует и имеет правильный формат. Ошибка: {ex.Message}",
                    "Ошибка загрузки", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                HistoryLog.Add($"История загружена из '{historyFilePath}'.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Не удалось загрузить файл истории. Пожалуйста, убедитесь, что файл существует и имеет правильный формат. Ошибка: {ex.Message}",
                    "Ошибка загрузки истории", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveStrip_Click(object sender, EventArgs e)
        {
            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Текстовые файлы (*.txt)|*.txt|CSV файлы (*.csv)|*.csv";
                saveDialog.Title = "Сохранить список задач и историю";
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
                openDialog.Filter = "Текстовые файлы (*.txt)|*.txt|CSV файлы (*.csv)|*.csv";
                openDialog.Title = "Загрузить список задач и историю";
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
            var result = MessageBox.Show("Вы уверены, что хотите выйти? Несохранённые данные будут потеряны.",
                "Выход", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void InfoStrip_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Instructions, "Инструкция по использованию", MessageBoxButtons.OK, MessageBoxIcon.Information);
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