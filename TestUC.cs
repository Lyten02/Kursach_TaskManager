
namespace Kursach
{
    public partial class TestUC : UserControl
    {
        private event Action _deleteTask;
        private event Action _onChangedState;

        public TestUC(Action DeleteTask, Action OnChangedState)
        {
            _deleteTask = DeleteTask;
            _onChangedState = OnChangedState;
            InitializeComponent();
            CheckTask.CheckedChanged += (sender, e) => IsFinished = CheckTask.Checked;
            DeleteTaskButton.Click += (sender, e) => _deleteTask.Invoke();
        }

        public bool IsFinished
        {
            get => CheckTask.Checked;
            set
            {
                if (value)
                {
                    Finish();
                }
                else
                {
                    UnFinish();
                }
                CheckTask.Checked = value;
            }
        }

        public string TaskName
        {
            get => TextTask.Text;
            set
            {
                TextTask.Text = value;
                _onChangedState.Invoke();
            }
        }

        public void Finish()
        {
            TextTask.Font = new Font(TextTask.Font, FontStyle.Strikeout);
            TextTask.ReadOnly = true;
            _onChangedState.Invoke();
        }

        public void UnFinish()
        {
            TextTask.Font = new Font(TextTask.Font, FontStyle.Regular);
            TextTask.ReadOnly = false;
            _onChangedState.Invoke();
        }
    }
}
