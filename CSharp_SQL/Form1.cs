using System;
using System.Drawing;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace CSharp_SQL
{

    public partial class Form1 : Form
    {
        private Label lblNo;
        TextBox tbNo = new TextBox();
        private Label lblNumber;
        private Button btnSearch;
        NumericUpDown nudNumber = new NumericUpDown();
        string providerName = null;

        public Form1()
        {
            this.Size = new Size(300, 300);
            this.StartPosition = FormStartPosition.CenterScreen;

            lblNo = new Label();
            lblNo.Text = "工單號碼";
            lblNo.Location = new Point(30, 20);
            lblNo.AutoSize = true;
            lblNo.Font = new Font(TextBox.DefaultFont.FontFamily, 11);
            this.Controls.Add(lblNo);

            tbNo.Size = new Size(120, 00);
            tbNo.Location = new Point(30, 40);
            tbNo.Text = "請輸入工單號碼";
            this.Controls.Add(tbNo);

            lblNumber = new Label();
            lblNumber.AutoSize = true;
            lblNumber.Text = "工單生產數量";
            lblNumber.Location = new Point(30, 120);
            lblNumber.Font = new Font(TextBox.DefaultFont.FontFamily, 11);
            this.Controls.Add(lblNumber);
                        
            nudNumber.Size = new Size(140, 00);
            nudNumber.Location = new Point(30, 140);
            nudNumber.BackColor = SystemColors.MenuBar;
            nudNumber.Minimum = 0;
            nudNumber.Maximum = 100000;
            nudNumber.ReadOnly = true;
            this.Controls.Add(nudNumber);

            btnSearch = new Button();
            btnSearch.Text = "查詢";
            btnSearch.Size = new Size(70, 26);
            btnSearch.Location = new Point(180, 190);
            btnSearch.Click += btnSearch_Click;
            this.Controls.Add(btnSearch);

        }
        

        private void btnSearch_Click(object sender, EventArgs e)
        {

            string connectionString = "DATA SOURCE=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.0.0.1)(PORT=1521)))(CONNECT_DATA=(SID = MIS)));PERSIST SECURITY INFO=True;USER ID=MIS;PASSWORD=OracleMis;";
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                try
                {
                    connection.Open();
                   
                    string sqlQuery = "SELECT F002 FROM MSE0017 WHERE F003 = :WorkOrderNumber";
                    OracleCommand command = new OracleCommand(sqlQuery, connection);
                    command.Parameters.Add(new OracleParameter(":WorkOrderNumber", tbNo.Text));

                    int result = Convert.ToInt32(command.ExecuteScalar());
                    if (result > 0)
                    {
                        nudNumber.Value = result;
                    }
                    else
                    {
                        MessageBox.Show("目前查詢的工單不存在");
                    }
                    }
                catch (Exception ex)
                {
                    MessageBox.Show("連線或查詢過程中發生錯誤 : " + ex.Message);
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
