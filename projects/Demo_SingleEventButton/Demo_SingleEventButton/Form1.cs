namespace Demo_SingleEventButton
{
    public partial class Form1 : Form
    {
        OpenFileDialog openFileDialog;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                
            }
        }
    }
}