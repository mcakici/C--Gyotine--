using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Media;

namespace RotatePictureBox
{
    public partial class Form1 : Form
    {

        private Bitmap image = null;
        private float angle = 0.0f;
        int t2 = 6, donushizi = 5, orjdonushizi = 5, talihli = 0, eklenecekpuan = 0, tray = 1;
        string veriyolu = "data.ini";
        string[] kisiler = { "Oðuz Alp Ündeðerli", "Andaç Kýzýlýrmak", "Ayten Yýldýz", "Berkay Eþme", "Ceren Tecim", "Fatih Mehmet Demir", "Hýzlan Erpak", "Ýzzet Kaan Özdemir", "Mustafa Çakýcý", "Onur Söyleyen", "Serhat Esenkaya", "Vedat Eriþ" };
        int[] devam = new int[12];
        Random rnd = new Random();
     
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          
          image = new Bitmap(pictureBox1.Image);
          pictureBox1.Image = (Bitmap)image.Clone();
          timer1.Interval = 15;
          timer1.Start();

          button2.Enabled = false;
          button1.Enabled = true;


          SesCal(1);
          //Veri Dosyasý Varmý ? Yoksa Olusturalým bea.
          try
          {
            string data = "";
            System.IO.StreamReader file = new System.IO.StreamReader(veriyolu);
            data = file.ReadToEnd();
            string[] datadizi = data.Split(';');
            Array.Resize(ref datadizi, 12);// dizideki fazlalýgý sildim
            file.Close();
            listBox1.Items.Clear();
            for (int i = 0; i < kisiler.Length; i++)
            {
              if (datadizi[i] == "")
              {
                datadizi[i] = "0";
              }
                listBox1.Items.Add(datadizi[i] + " => " + kisiler[i]);
            }
          }
          catch // Dosya Okuma Hatalý Ýse
          {
            TextWriter dosya = new StreamWriter(veriyolu);

            try
            {
              for (int i = 0; i < kisiler.Length; i++)
              {
                dosya.Write("0;");
              }
            }
            finally
            {
              dosya.Flush();
              dosya.Close();
              VeriCek();
            }
          }

        }



        private void timer1_Tick(object sender, EventArgs e)
        {
          // her 360 döndükten sonra sýfýrlanmasý
          if (angle <= -360) { angle = 0; }
          RotateImage(pictureBox1, image, angle);
          angle -= donushizi;
          label2.Text = angle.ToString();
        }

        private void RotateImage(PictureBox pb, Image img, float angle)
        {
            if (img == null || pb.Image == null)
                return;

            Image oldImage = pb.Image;
            pb.Image = Utilities.RotateImage(img, angle);
            if (oldImage != null)
            {
                oldImage.Dispose();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
          timer1.Interval = 5;
          timer2.Interval = rnd.Next(500,1500);
          timer2.Start();
          button2.Enabled = false;
          button1.Enabled = false;
          numericUpDown1_donushizi.Enabled = false;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
          switch (t2)
          {
            case 6: donushizi = 5; break;
            case 5: donushizi = 4; break;
            case 4: donushizi = 3; break;
            case 3: donushizi = 2; break;
            case 2: donushizi = 1; break;
            case 1: donushizi = 1; break;
            case 0: timer1.Stop(); timer2.Stop(); break;
          }
          label1.Text = t2 + " sn";
          
          if (t2 == 0)
          {
            // MessageBox.Show(datadizi.Length.ToString());

            // Hangi Açý kime ait ?
            if (angle >= -29 && angle <= 0)
            {
              label1.Text = kisiler[0];
              talihli = 0;
            }
            else if (angle >= -59 && angle <= -30)
            {
              label1.Text = kisiler[1];
              talihli = 1;
            }
            else if (angle >= -89 && angle <= -60)
            {
              label1.Text = kisiler[2];
              talihli = 2;
            }
            else if (angle >= -119 && angle <= -90)
            {
              label1.Text = kisiler[3];
              talihli = 3;
            }
            else if (angle >= -149 && angle <= -120)
            {
              label1.Text = kisiler[4];
              talihli = 4;
            }
            else if (angle >= -179 && angle <= -150)
            {
              label1.Text = kisiler[5];
              talihli = 5;
            }
            else if (angle >= -209 && angle <= -180)
            {
              label1.Text = kisiler[6];
              talihli = 6;
            }
            else if (angle >= -239 && angle <= -210)
            {
              label1.Text = kisiler[7];
              talihli = 7;
            }
            else if (angle >= -269 && angle <= -240)
            {
              label1.Text = kisiler[8];
              talihli = 8;
            }
            else if (angle >= -299 && angle <= -270)
            {
              label1.Text = kisiler[9];
              talihli = 9;
            }
            else if (angle >= -329 && angle <= -300)
            {
              label1.Text = kisiler[10];
              talihli = 10;
            }
            else if (angle >= -360 && angle <= -330)
            {
              label1.Text = kisiler[11];
              talihli = 11;
            }


            if (checkBox1.Checked == true)
            {

              if (devam[talihli] == 1)
              {
                button2.Enabled = true;
                button1.Enabled = false;
                button2.PerformClick();
                VeriCek();
                return;
              }
              devam[talihli] = 1;

            }



              SesCal(0);
              button2.Enabled = true;
              button1.Enabled = false;
              groupBox1.Enabled = true;
              //VeriCek();
              listBox1.SelectedIndex = talihli;
              textBoxPuan.Focus();


          }

          
          t2 = t2 - 1;
        }



        private void button2_Click(object sender, EventArgs e)
        {
          //sýfýrlayýp yeniden baslat
          numericUpDown1_donushizi.Enabled = true;
          t2 = 6; donushizi = orjdonushizi;
          timer1.Interval = 15;
          timer1.Start();
          button2.Enabled = false;
          button1.Enabled = true;
          groupBox1.Enabled = false;
          listBox1.SelectedIndex = -1;
          SesCal(1);
        }

        private void buttonBildi_Click(object sender, EventArgs e)
        {
          string data = "";
          System.IO.StreamReader file = new System.IO.StreamReader(veriyolu);
          data = file.ReadToEnd();
          string[] datadizi = data.Split(';');
          Array.Resize(ref datadizi, 12);// dizideki fazlalýgý sildim
          file.Close();
          listBox1.Items.Clear();
          for (int i = 0; i < kisiler.Length; i++)
          {
            if (datadizi[i] == "")
            {
              datadizi[i] = "0";
            }
              listBox1.Items.Add(datadizi[i] + " => " + kisiler[i]);
          }

          
          TextWriter dosya = new StreamWriter(veriyolu);
          try
          {
            eklenecekpuan = int.Parse(textBoxPuan.Text);
          }
          catch (OverflowException)
          {
            MessageBox.Show("Girdiðiniz deðer çok büyük!", "Gyotine");
            textBoxPuan.Text = "0";
            textBoxPuan.Focus();
            eklenecekpuan = 0;
            return;
          }
          catch (FormatException)
          {
            MessageBox.Show("Bir deðer girmelisiniz!", "Gyotine");
            textBoxPuan.Text = "0";
            textBoxPuan.Focus();
            eklenecekpuan = 0;
            return;
          }
          finally
          {
            int[] toplameklenecekpuanlar = new int[12];
            for (int i = 0; i < kisiler.Length; i++)
            {
              if (talihli == i)
              {
                toplameklenecekpuanlar[i] = (int.Parse(datadizi[i]) + eklenecekpuan);
              }
              else
              {
                toplameklenecekpuanlar[i] = int.Parse(datadizi[i]);
              }
            }

            foreach (var item in toplameklenecekpuanlar)
            {
              dosya.Write(item + ";");
            }


            dosya.Flush();
            dosya.Close();
            VeriCek();
          }

          // KÝÞÝYÝ SEÇ
            listBox1.SelectedIndex = talihli;

          groupBox1.Enabled = false;
          //listBox1.SelectedIndex = -1;
          if (eklenecekpuan > 0) {
            SesCal(2);
          }
          else if (eklenecekpuan < 0)
          {
            SesCal(3);
          }
        }


        public void VeriCek()
        {
          string data = "";
          System.IO.StreamReader file = new System.IO.StreamReader(veriyolu);
          data = file.ReadToEnd();
          string[] datadizi = data.Split(';');
          Array.Resize(ref datadizi, 12);// dizideki fazlalýgý sildim
          file.Close();
          listBox1.Items.Clear();
          for (int i = 0; i < kisiler.Length; i++)
          {
            if (datadizi[i] == "")
            {
              datadizi[i] = "0";
            }
              listBox1.Items.Add(datadizi[i] + " => " + kisiler[i]);
          }
 
        }

        private void button3_Click(object sender, EventArgs e)
        {
          try
          {
            talihli = listBox1.SelectedIndex;
            if (talihli < 0)
            {
              MessageBox.Show("Listeden Birisini Seçmelisiniz!", "Gyotine");
            }
            else
            {
              groupBox1.Enabled = true;
            }
          }
          catch 
          {
            MessageBox.Show("Listeden Birisini Seçmelisiniz!", "Gyotine");
          }
        }

        private void textBoxPuan_KeyPress(object sender, KeyPressEventArgs e)
        {
          if ((e.KeyChar > 57 || e.KeyChar < 48) && e.KeyChar != 8 && e.KeyChar != 45)
          {
            e.Handled = true;
          }
        }

        private void buttonHizliDeger(object sender, EventArgs e)
        {
          if (sender == button4) { textBoxPuan.Text = "1";  }
          else if (sender == button5) { textBoxPuan.Text = "3"; }
          else if (sender == button6) { textBoxPuan.Text = "5"; }
          else if (sender == button7) { textBoxPuan.Text = "10"; }
          else if (sender == button8) { textBoxPuan.Text = "20"; }

          else if (sender == button9) { textBoxPuan.Text = "-1"; }
          else if (sender == button10) { textBoxPuan.Text = "-3"; }
          else if (sender == button11) { textBoxPuan.Text = "-5"; }
          else if (sender == button12) { textBoxPuan.Text = "-10"; }
          else if (sender == button13) { textBoxPuan.Text = "-20"; }

          buttonBildi.PerformClick();
         
        }
     

        private void SesCal(int sesid)
        {
          if (checkBoxSes.Checked == false)
          {

            string path = "sound/cin.wav";

            SoundPlayer player = new SoundPlayer();
            switch (sesid)
            {
              case 0: path = "sound/basla.wav"; break;
              case 1: path = "sound/bekleme.wav"; break;
              case 2: path = "sound/bravo.wav"; break;
              case 3: path = "sound/olmadi.wav"; break;

            }


            player.SoundLocation = path;
            player.Play();

          }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
          if (checkBox1.Checked == true)
          {
            MessageBox.Show("Program aktif olduðu sürece ayný isim gelirse devam edecektir.\n\nKapatmak için iþareti kaldýrýn.\n\nNot: Resetlemek için kapatýp tekrar açýn.", "Gyotine");
          }
          else if (checkBox1.Checked == false)
          {
            for (int i = 0; i < devam.Length; i++)
            {
              devam[i] = 0;
            }
          }
        }

        private void numericUpDown1_donushizi_ValueChanged(object sender, EventArgs e)
        {
          orjdonushizi = Convert.ToInt32(numericUpDown1_donushizi.Value);
          donushizi = Convert.ToInt32(numericUpDown1_donushizi.Value);
        }

        private void label4_Click(object sender, EventArgs e)
        {
          MessageBox.Show("Copyright 2012 All Lefts Reserved.\nMaðdurum diyorsan ücretsiz psikolojik danýþma hattý '153'ü arayabilirsiniz.\n\n\nHazýrlayan: Mustafa Çakýcý (glikoz@live.com)", "Gyotine");
        }



        private void Form1_Resize(object sender, EventArgs e)
        {
          if (checkBox2.Checked == true)
          {
            notifyIcon1.BalloonTipTitle = "Gyotine";
            notifyIcon1.BalloonTipText = "Gyotine halen çalýþmaktadýr. Ekrana geri getirmek için simgeye týklayýnýz.";

            if (FormWindowState.Minimized == this.WindowState)
            {
              notifyIcon1.Visible = true;
              notifyIcon1.ShowBalloonTip(500);
              this.Hide();
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
              notifyIcon1.Visible = false;
            }
          }
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
          this.Show();
          this.WindowState = FormWindowState.Normal;
        }







    }
}