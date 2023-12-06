using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace managerApp
{
    public partial class manPanel : Form
    {
        public manPanel()
        {
            InitializeComponent();
        }
        Context dbContext = new Context();
        intern intern = new intern();
        int getId;
        private void manPanel_Load(object sender, EventArgs e)
        {
            refresh();
            stajyerBilgiDuzenlePanel.SendToBack();

        }
        private void görüntüleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stajyerBilgiGorPanel.BringToFront();
            stajyerBilgiDuzenlePanel.SendToBack();

        }
        private void düzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stajyerBilgiGorPanel.SendToBack();
            stajyerBilgiDuzenlePanel.BringToFront();

        }
        #region methods
        private void componentClear()
        {
            nameTextbox.Text = string.Empty;
            SurnameTextbox.Text = string.Empty;
            companyNameTextbox.Text = string.Empty;
            departmentTextBox.Text = string.Empty;
            companyManTextBox.Text = string.Empty;
            startDateTextBox.Text = string.Empty;
            endDateTextBox.Text = string.Empty;
            userNameTextBox.Text = string.Empty;
            PasswordtextBox.Text = string.Empty;
        } // Komponentleri Temizler
        private void registerIntern()
        {
            intern.Name = nameTextbox.Text;
            intern.Surname = SurnameTextbox.Text;
            intern.CompanyName = companyNameTextbox.Text;
            intern.Department = departmentTextBox.Text;
            intern.CompanyMan = companyManTextBox.Text;
            intern.startDate = startDateTextBox.Text;
            intern.endDate = endDateTextBox.Text;
            intern.userName = userNameTextBox.Text;
            intern.password = PasswordtextBox.Text;

            dbContext.interns.Add(intern);
            dbContext.SaveChanges();
        } // Yeni Stajyer Ekler
        private void refresh()
        {
            stajyerBilgiDuzenleDataGridView.DataSource = dbContext.interns.ToList();
            stajyerBilgiGoruntuleDataGridView.DataSource = dbContext.interns.ToList();
            stajyerBilgiGoruntuleDataGridView.Columns["ID"].Visible = false;
        } // Tabloyu Veritabanına göre günceller
        private void updateınternDatas()
        {
            intern = dbContext.interns.Find(getId);
            intern.Name = nameTextbox.Text;
            intern.Surname = SurnameTextbox.Text;
            intern.CompanyName = companyNameTextbox.Text;
            intern.Department = departmentTextBox.Text;
            intern.CompanyMan = companyManTextBox.Text;
            intern.startDate = startDateTextBox.Text;
            intern.endDate = endDateTextBox.Text;
            intern.userName = userNameTextBox.Text;
            intern.password = PasswordtextBox.Text;

            dbContext.SaveChanges();
        } // Seçilen Stajyerin bilgilerini Günceller
        private void getInternDataOnTable()
        {
            getId = Convert.ToInt32(stajyerBilgiDuzenleDataGridView.CurrentRow.Cells["id"].Value);
            var getData = dbContext.interns.Find(getId);
            nameTextbox.Text = getData.Name;
            SurnameTextbox.Text = getData.Surname;
            companyNameTextbox.Text = getData.CompanyName.ToString();
            departmentTextBox.Text = getData.Department.ToString();
            companyManTextBox.Text = getData.CompanyMan.ToString();
            startDateTextBox.Text = getData.startDate.ToString();
            endDateTextBox.Text = getData.endDate.ToString();
            userNameTextBox.Text = getData.userName.ToString();
            PasswordtextBox.Text = getData.password.ToString();
        } // Tabloda seçilen stajyerin bilgilerini getirir
        #endregion
       
        private void button1_Click(object sender, EventArgs e)
        {
            componentClear();
        }
        
        private void addButton_Click(object sender, EventArgs e)
        {
            registerIntern();
            refresh();
        }
        private void stajyerBilgiDuzenleDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            getInternDataOnTable();
        }
        private void updateButton_Click(object sender, EventArgs e)
        {
            updateınternDatas();
            refresh();
        }
    }
}
