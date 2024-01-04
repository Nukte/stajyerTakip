using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Serialization.Formatters;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using stajyerTakip;

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
        internTask internTask = new internTask();
        int getId;
        private void manPanel_Load(object sender, EventArgs e)
        {
            refresh();
            stajyerBilgiGorPanel.BringToFront();
        }
        private void görüntüleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stajyerBilgiGorPanel.BringToFront();

        }
        private void düzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
            startDater.Value = default;
            endDater.Value = default;
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
            intern.StartDate = startDater.Value;
            intern.EndDate = endDater.Value;
            intern.UserName = userNameTextBox.Text;
            intern.Password = PasswordtextBox.Text;

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
            intern.StartDate = startDater.Value;
            intern.EndDate = endDater.Value;
            intern.UserName = userNameTextBox.Text;
            intern.Password = PasswordtextBox.Text;
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
            endDater.Value = getData.StartDate;
            endDater.Value = getData.EndDate;
            userNameTextBox.Text = getData.UserName.ToString();
            PasswordtextBox.Text = getData.Password.ToString();
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
        private void deleteButton_Click(object sender, EventArgs e)
        {
            getId = Convert.ToInt32(stajyerBilgiDuzenleDataGridView.CurrentRow.Cells["id"].Value);
            var itemToDelete = dbContext.interns.FirstOrDefault(item => item.ID == getId);
            if (itemToDelete != null)
            {
                dbContext.interns.Remove(itemToDelete);
                dbContext.SaveChanges();
                refresh();
            }
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

        private void görevAtamasıYapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            taskPanel.BringToFront();
        }
        private void gorevEkleButton_Click(object sender, EventArgs e)
        {
           
        }
        private void stajyerSec()
        {
        
        }
        private void gorevSec()
        {
        
        }
        
        private void gorevAtaButton_Click(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {

        }
        task Task = new task();
        
        private void taskAddButton_Click(object sender, EventArgs e)
        {
            Task.TaskName = taskNameTextBox.Text;
            Task.TaskDescription = taskDescTextBox.Text;
            Task.TaskStatus = "Bekleniyor...";
            Task.TaskEndDate = taskEndDateTimePicker.Value;
            dbContext.tasks.Add(Task);
            dbContext.SaveChanges();

            taskDataGridView.DataSource = dbContext.tasks.ToList();
        }
        private void gorevSilButton_Click(object sender, EventArgs e)
        {

        }

        private void updateTaskButton_Click(object sender, EventArgs e)
        {
            Task.TaskName = taskNameTextBox.Text;
            Task.TaskDescription = taskDescTextBox.Text;
            Task.TaskStatus = "Bekleniyor...";
            Task.TaskEndDate = taskEndDateTimePicker.Value;

            dbContext.SaveChanges();

            taskDataGridView.DataSource = dbContext.tasks.ToList();
        }
        int getTaskId;
        private void removeTaskButton_Click(object sender, EventArgs e)
        {
            var getId = Convert.ToInt32(taskDataGridView.CurrentRow.Cells["ID"].Value);

            var silinecekveri = dbContext.tasks.Find(getId);
            dbContext.tasks.Remove(silinecekveri);
            dbContext.SaveChanges();
            taskDataGridView.DataSource = dbContext.tasks.ToList();

        }
        private void clearCompButton_Click(object sender, EventArgs e)
        {
            taskNameTextBox.Text = string.Empty;
            taskDescTextBox.Text = string.Empty;
            taskEndDateTimePicker.Value = DateTime.Now;
        }

        private void assingTask_Click(object sender, EventArgs e)
        {
            // kombobox dan seçilen stajyerin ID'si
            //var selectedIntern = (intern)internComboBox.SelectedItem;
            //var selectedTask = (task)taskComboBox.SelectedItem;
            //MessageBox.Show($"{selectedIntern.Name} adlı stajyere {selectedTask.TaskName} adlı görev atanmıştır.");

            //internTask.InternID = selectedIntern.ID;
            //internTask.InternName = selectedIntern.Name;
            //internTask.TaskID = selectedTask.ID;
            //internTask.TaskName = selectedTask.TaskName;
            //internTask.TaskStatus = "Atandı";

            //dbContext.internTasks.Add(internTask);
            //dbContext.SaveChanges();
            //atanmısGorevDataGridView.DataSource = dbContext.internTasks.ToList();



            var selectedIntern = (intern)internComboBox.SelectedItem;
            var selectedTask = (task)taskComboBox.SelectedItem;

            // Atama öncesi benzersizlik kontrolü için kullanılacak anahtar
            var uniqueTaskAssignmentKey = $"{selectedIntern.ID}_{selectedTask.ID}";

            // Önceden bu görevin atanmış olup olmadığını kontrol et
            bool alreadyAssigned = dbContext.internTasks.Any(task => task.uniqueTaskAssignmentKey == uniqueTaskAssignmentKey);

            if (!alreadyAssigned)
            {
                MessageBox.Show($"{selectedIntern.Name} adlı stajyere {selectedTask.TaskName} adlı görev atanmıştır.");

                internTask.InternID = selectedIntern.ID;
                internTask.InternName = selectedIntern.Name;
                internTask.TaskID = selectedTask.ID;
                internTask.TaskName = selectedTask.TaskName;
                internTask.TaskStatus = "Atandı";

                // Benzersiz atama anahtarını atama işlemi için kullan
                internTask.uniqueTaskAssignmentKey = uniqueTaskAssignmentKey;

                dbContext.internTasks.Add(internTask);
                dbContext.SaveChanges();
                atanmısGorevDataGridView.DataSource = dbContext.internTasks.ToList();
            }
            else
            {
                MessageBox.Show($"Bu görev zaten {selectedIntern.Name} adlı stajyere atanmış durumda.");
            }

        }

        private void deleteTaskButton_Click(object sender, EventArgs e)
        {
            var selectedInternTask = (internTask)removeTaskcomboBox.SelectedItem;

            if (selectedInternTask != null)
            {
                dbContext.internTasks.Remove(selectedInternTask);
                dbContext.SaveChanges();
                atanmısGorevDataGridView.DataSource = dbContext.internTasks.ToList();
            }
            //  var silicek = dbContext.internTasks.Find(selectedIntern);
            // dbContext.internTasks.Remove(silicek);
            /// dbContext.SaveChanges();
            var interns = dbContext.interns.ToList();

            internComboBox.DisplayMember = "Name";
            internComboBox.ValueMember = "Id";
            internComboBox.DataSource = interns;

            var tasks = dbContext.tasks.ToList();

            taskComboBox.DisplayMember = "TaskName";
            taskComboBox.ValueMember = "Id";
            taskComboBox.DataSource = tasks;

            var assingTask = dbContext.internTasks.ToList();

            removeTaskcomboBox.DisplayMember = "ID";
            removeTaskcomboBox.ValueMember = "ID";
            removeTaskcomboBox.DataSource = assingTask;
        }

        private void taskDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        
        }

        private void taskDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int getTaskId = Convert.ToInt32(taskDataGridView.CurrentRow.Cells["ID"].Value);
            var getData = dbContext.tasks.Find(getTaskId);
            taskNameTextBox.Text = getData.TaskName;
            taskEndDateTimePicker.Value = getData.TaskEndDate;
            taskDescTextBox.Text = getData.TaskDescription;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            taskDataGridView.DataSource = dbContext.tasks.ToList();
        }
        private void yenileButton_Click(object sender, EventArgs e)
        {
            var interns = dbContext.interns.ToList();

            internComboBox.DisplayMember = "Name";
            internComboBox.ValueMember = "Id";
            internComboBox.DataSource = interns;

            var tasks = dbContext.tasks.ToList();

            taskComboBox.DisplayMember = "TaskName";
            taskComboBox.ValueMember = "Id";
            taskComboBox.DataSource = tasks;

            var assingTask = dbContext.internTasks.ToList();

            removeTaskcomboBox.DisplayMember = "ID";
            removeTaskcomboBox.ValueMember = "ID";
            removeTaskcomboBox.DataSource = assingTask;

            atanmısGorevDataGridView.DataSource = dbContext.internTasks.ToList();
        }

        private void görevDurumlarınıGörToolStripMenuItem_Click(object sender, EventArgs e)
        {
            taskViewPanel.BringToFront();

            var interns = dbContext.internTasks.ToList();

            var uniqueInternNames = dbContext.internTasks.Select(internTask => internTask.InternName).Distinct().ToList();

            stajyerSecComboBox.DisplayMember = "InternName";
            stajyerSecComboBox.ValueMember = "ID";
            stajyerSecComboBox.DataSource = uniqueInternNames;
        }
        private void goreviGoruntule_Click(object sender, EventArgs e)
        {
            string selectedInternName = stajyerSecComboBox.SelectedItem.ToString(); // ComboBox'tan stajyer adını al
            intern selectedIntern = dbContext.interns.FirstOrDefault(gint => gint.Name == selectedInternName); // Seçilen stajyerin ID'sini al
            int selectedInternID = selectedIntern.ID;
            if (selectedIntern != null)
            {
                // ComboBox'ta seçilen stajyerin görevlerini veritabanından çek
                var selectedInternTasks = dbContext.internTasks.Where(task => task.InternID == selectedInternID).ToList();

                gorevleriGorDataGridView.DataSource = selectedInternTasks
                    .Select(t => new { t.InternName, t.TaskName, t.TaskStatus, taskDescription = dbContext.tasks.FirstOrDefault(td => td.ID == t.ID)?.TaskDescription, dbContext.tasks.FirstOrDefault(td => td.ID == t.ID)?.TaskEndDate })
                    .ToList();
            }
        }

        private void dosyalarVeNotlarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filePanel.BringToFront();

            var interns = dbContext.interns.ToList();

            var uniqueInternNames = dbContext.interns.Select(intern => intern.Name).Distinct().ToList();

            internFileComboBox.DisplayMember = "Name";
            internFileComboBox.ValueMember = "ID";
            internFileComboBox.DataSource = uniqueInternNames;
            
        }

        private void viewFileButton_Click(object sender, EventArgs e)
        {
            string selectIntern = internFileComboBox.SelectedItem?.ToString(); // ComboBox'tan seçili olanın null olma ihtimaline karşı ?. 
            var getInternId = dbContext.interns.FirstOrDefault(gint => gint.Name == selectIntern);

            if (getInternId != null)
            {
                int selectedInternID = getInternId.ID;

                var getInternFile = dbContext.dailyFiles.FirstOrDefault(task => task.whoId == selectedInternID);
                if (getInternFile != null)
                {
                   // List<dailyFile> fileList = new List<dailyFile> { getInternFile };
                    var files = dbContext.dailyFiles.Where(f => f.whoId == selectedInternID).ToList();

                    fileDataGridView.DataSource = files
                        .Select(f => new { f.fileName, f.description,who = dbContext.interns.FirstOrDefault(td => td.ID == f.whoId)?.Name})
                    .ToList();

                    fileDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; // Tüm sütunları hücre içeriğine göre ayarlar


                }
                else
                {
                    MessageBox.Show("bir hata oluştu");
                }
            }
            else
            {
                 MessageBox.Show("Stajyer bulunamadı");
            }




           
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void downloadButton_Click(object sender, EventArgs e)
        {

            var getFileName = fileDataGridView.CurrentRow.Cells["fileName"].Value;

            var downFile = dbContext.dailyFiles.FirstOrDefault(f => f.fileName == getFileName.ToString());
            string fileName = downFile.fileName; // Dosya adını alın
            string fileExtension = Path.GetExtension(fileName);
            MessageBox.Show($"Dosyanın uzantısı: {fileExtension}");

            MessageBox.Show(downFile.ID.ToString());


            int fileID = downFile.ID; // İndirmek istediğiniz dosyanın ID'si

            var file = dbContext.dailyFiles.FirstOrDefault(f => f.ID == fileID); // Dosyayı ID'ye göre alın

            if (file != null)
            {
                byte[] fileData = file.data; // Dosya verisini byte[] olarak alın

                // fileData byte[]'ını kullanarak dosyayı işleyin (örneğin, diske yazabilirsiniz)
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "All files (*.*)|*.*"; // Filtre olarak tüm dosya türlerini kullan
                saveFileDialog1.FilterIndex = 1; // İlk filtreyi varsayılan olarak ayarla
                saveFileDialog1.DefaultExt = fileExtension;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    // Kullanıcının seçtiği dosya yolu
                    string filePath = saveFileDialog1.FileName;

                    // Dosyayı diske yaz
                    File.WriteAllBytes(filePath, fileData);

                    MessageBox.Show("Dosya indirme başarılı!");
                }
            }
        }

        private void fileDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            fileIdLabel = 
        }

        private void internFileComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectIntern = internFileComboBox.SelectedItem?.ToString(); // ComboBox'tan seçili olanın null olma ihtimaline karşı ?. 
            var getInternId = dbContext.interns.FirstOrDefault(gint => gint.Name == selectIntern);

            if (getInternId != null)
            {
                int selectedInternID = getInternId.ID;

                var getInternFile = dbContext.dailyFiles.FirstOrDefault(task => task.whoId == selectedInternID);
                if (getInternFile != null)
                {
                    // List<dailyFile> fileList = new List<dailyFile> { getInternFile };
                    var files = dbContext.dailyFiles.Where(f => f.whoId == selectedInternID).ToList();

                    fileDataGridView.DataSource = files
                        .Select(f => new { f.fileName, f.description, who = dbContext.interns.FirstOrDefault(td => td.ID == f.whoId)?.Name })
                    .ToList();

                    fileDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells; // Tüm sütunları hücre içeriğine göre ayarlar


                }
                else
                {
                    MessageBox.Show("bir hata oluştu");
                }
            }
            else
            {
                MessageBox.Show("Stajyer bulunamadı");
            }
        }

        private void fileDataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var getFileName = fileDataGridView.CurrentRow.Cells["fileName"].Value;

            var downFile = dbContext.dailyFiles.FirstOrDefault(f => f.fileName == getFileName.ToString());


            string fileName = downFile.fileName; // Dosya adını alın
            string fileExtension = Path.GetExtension(fileName);
            MessageBox.Show($"Dosyanın uzantısı: {fileExtension}");
        }
    }
}
