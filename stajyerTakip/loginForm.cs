using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace stajyerTakip
{
    public partial class loginForm : Form
    {
        public loginForm()
        {
            InitializeComponent();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {

            string enteredUsername = userNameTextBox.Text;
            string enteredPassword = passwordTextBox.Text;
            using (var context = new Context())
            {
                var kullanici = context.interns.FirstOrDefault(i => i.UserName == enteredUsername && i.Password == enteredPassword);
                if (kullanici != null)
                {
                    MessageBox.Show("Giriş başarılı!");
                    int kullaniciId = kullanici.ID;
                    ınternPanel ınternPanel = new ınternPanel(kullaniciId);
                    ınternPanel.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı veya şifre hatalı!");
                }
            }

           /* if (userNameTextBox.Text == "stajyer" && passwordTextBox.Text == "123")
            {
                ınternPanel ınternPanel = new ınternPanel();
                ınternPanel.Show();
                this.Hide();
            }
            else if(userNameTextBox.Text == "yönetici" && passwordTextBox.Text == "123")
            {

            }
            else
            {
                MessageBox.Show("Kullanıcı adı veya şifre hatalı!");
            }
           */
        }
    }
}
