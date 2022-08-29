namespace FrontBackEndUpdateLoop
{
    using BackEnd;
    public partial class Form1 : Form
    {
        private Storage[][] cellArray;

        public Form1()
        {
            this.InitializeComponent();

            for (int i = 0; i < 3; i++)
            {
                this.cellArray[i] = new Storage[3];
            }

            this.cellArray[1][1].Text = "NEW CONTENT";

            this.FormInit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void FormInit()
        {
            char columnHeader = (char)('A' - 1);

            for (int i = 0; i < 3; i++)
            {
                this.dataGridView1.Columns.Add(new DataGridViewTextBoxColumn());
                this.dataGridView1.Columns[i].HeaderText = (++columnHeader).ToString();
                this.dataGridView1.Rows.Add(this.cellArray[i]);
            }
        }

        private void UpdateContent()
        {
            //this.cellArray.Text = "NEW CONTENT";
            //this.dataGridView1.Rows[1].Cells[0].Value = this.cellArray.Text;
            //this.dataGridView1.Update();
        }

    }
}