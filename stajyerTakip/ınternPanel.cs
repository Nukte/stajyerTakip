using managerApp;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace stajyerTakip
{
    public partial class ınternPanel : Form
    {
        intern kisi = new intern();
        public ınternPanel(int id)
        {
            InitializeComponent();
               managerMessageTextBox.ReadOnly = true;
              // AllowDrop özelliğini true olarak ayarlayarak sürükle-bırak özelliğini etkinleştiriyoruz.
               filePanel.AllowDrop = true;
               // DragEnter olayı, sürüklenen dosyanın uygun olup olmadığını kontrol ediyor.
               filePanel.DragEnter += filePanel_DragEnter;
               // DragDrop olayı, sürüklenen dosyayı işliyor.
               filePanel.DragDrop += filePanel_DragDrop;

            using(var dbContext = new Context())
            {
                kisi = dbContext.interns.Find(id);
                if (kisi != null)
                {
                    dataGridView1.DataSource = new List<intern> { kisi };
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
                    uploadDate = DateTime.Now
                };

                context.dailyFiles.Add(fileData);
                context.SaveChanges();
            }
        }

        private void ınternPanel_Load(object sender, EventArgs e)
        {
            
        }
    }
}