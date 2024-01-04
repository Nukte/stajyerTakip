using managerApp;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace stajyerTakip
{
    public partial class ınternPanel : Form
    {
        int idGet;
        intern kisi = new intern();
        public ınternPanel(int id)
        {
            InitializeComponent();
             //  managerMessageTextBox.ReadOnly = true;
              // AllowDrop özelliğini true olarak ayarlayarak sürükle-bırak özelliğini etkinleştiriyoruz.
               filePanel.AllowDrop = true;
               // DragEnter olayı, sürüklenen dosyanın uygun olup olmadığını kontrol ediyor.
               filePanel.DragEnter += filePanel_DragEnter;
               // DragDrop olayı, sürüklenen dosyayı işliyor.
               filePanel.DragDrop += filePanel_DragDrop;
            idGet = id;
            using(var dbContext = new Context())
            {
                kisi = dbContext.interns.Find(id);
                if (kisi != null)
                {
                   // dataGridView1.DataSource = new List<intern> { kisi };
                }

                var internWithId = dbContext.internTasks.FirstOrDefault(internTask => internTask.ID == id);
               
                if (internWithId != null)
                {
                    // internName değerini kullanabilirsiniz
                    // ComboBox'tan stajyer adını al  asdasdas  dasdasd

                     // Seçilen stajyerin ID'sini al

                    // ComboBox'ta seçilen stajyerin görevlerini tüm veritabanından çek

                    var selectedInternTasks = dbContext.internTasks.Where(task => task.InternID == internWithId.ID).ToList();
                    taskDataGridView.DataSource = selectedInternTasks
                          .Where(t => t.InternID == internWithId.ID)
                    .Select(t => new { t.TaskName, t.TaskStatus, taskDescription = dbContext.tasks.FirstOrDefault(td => td.ID == t.ID)?.TaskDescription, dbContext.tasks.FirstOrDefault(td => td.ID == t.ID)?.TaskEndDate })
                    .ToList();

                }


            }

       

        }
        private void filePanel_DragEnter(object sender, DragEventArgs e)
        {
            // sürüklenen dosyanın fileDrop türünde olup olmadığını sorgular
               if (e.Data.GetDataPresent(DataFormats.FileDrop))
               {
                // Dosya doğru formattaysa dosya yolunu temsil eden diziyi alır
                   e.Effect = DragDropEffects.Copy;
               }
               else
               {
                   e.Effect = DragDropEffects.None;
               }
            
        }

        private void filePanel_DragDrop(object sender, DragEventArgs e)
        {
            // yukarıda elde ettiğimiz diziyi files değişkenine atıyoruz.
              string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            // Dizi uzunluğunu kontrol edip dosyanın yolunu alıyoruz.
              if (files.Length > 0)
              {
                  string FilePath = files[0]; 

                  MessageBox.Show("Sürüklenen dosya: " + FilePath);
                // Dosyayı Veritabanına kaydeden metotu çağırıyoruz.
                  SaveFileToDatabase(FilePath);

              }
        }
        private void SaveFileToDatabase(string FilePath)
        {
            byte[] fileBytes;

            // Excel dosyasını okuma işlemi
            try
            {
                fileBytes = File.ReadAllBytes(FilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Dosya okuma hatası: " + ex.Message);
                return;
            }

            // Veritabanına kaydetme işlemi
            using (var context = new Context())
            {
                var fileData = new dailyFile
                {
                    fileName = Path.GetFileName(FilePath),
                    description = descriptionTextBox.Text,
                    data = fileBytes,
                    uploadDate = DateTime.Now,
                    whoId = idGet
                };

                context.dailyFiles.Add(fileData);
                context.SaveChanges();
            }
        }

        private void ınternPanel_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var dbContext = new Context())
            {
                var result = dbContext.internTasks
                 .Where(task => task.ID == idGet)
                 .Select(task => new { task.TaskName, task.TaskStatus/* Diğer sütunlar buraya gelecek */ })
                 .ToList();

                taskDataGridView.DataSource = result;
            }

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void saveAndCloseButton_Click(object sender, EventArgs e)
        {

        }

        private void filePanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}