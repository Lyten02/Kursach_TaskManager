namespace Kursach
{
    public partial class HistoryForm : Form
    {
        private BindingSource bindingSource = new BindingSource();

        public HistoryForm()
        {
            InitializeComponent();
            this.Text = "История действий";
            listBox1.Dock = DockStyle.Fill;
            bindingSource.DataSource = MainForm.HistoryLog;
            listBox1.DataSource = bindingSource;
        }
    }
}