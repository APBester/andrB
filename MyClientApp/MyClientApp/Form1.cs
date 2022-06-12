using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace MyClientApp
{
    public partial class Form1 : Form
    {
        localhost.WebService1 proxy = new localhost.WebService1();
        HttpClient client = new HttpClient();

        public Form1()
        {
            InitializeComponent();
        }

        private void WebServicesSettings()
        {
            client.BaseAddress = new Uri("http://localhost:56102/WebService1.asmx/");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //string countriesJson = proxy.Countries();
            //DataTable dtCountries = JsonConvert.DeserializeObject<DataTable>(countriesJson);
            //dataGridView1.DataSource = dtCountries;

            //view image
            PictureBox pb1 = new PictureBox();
            pb1.ImageLocation = "../Pictures/apb.png";
            pb1.SizeMode = PictureBoxSizeMode.AutoSize;

            WebServicesSettings();
        }

        private DataTable stringSplit(string userJson)
        {
            string[] json = userJson.Split('>');
            string[] finalJson = json[2].Split('<');

            DataTable dt = JsonConvert.DeserializeObject<DataTable>(finalJson[0]);
            return dt;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            HttpResponseMessage message = client.GetAsync("dataTableForUsers?id="+txtBoxID.Text+"").Result;
            string userJson = message.Content.ReadAsStringAsync().Result;
            //MessageBox.Show(userJson);

            dataGridView1.DataSource = stringSplit(userJson);
        }

        private void GetImage()
        {
            // open file dialog   
            OpenFileDialog open = new OpenFileDialog();
            // image filters  
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                // display image in picture box  
                pictureBox1.Image = new Bitmap(open.FileName);
                // image file path  
                textBox1.Text = open.FileName;
            }
        }

        private void txtBoxID_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnDisplay_Click(object sender, EventArgs e)
        {
            GetImage();
        }
    }
}
