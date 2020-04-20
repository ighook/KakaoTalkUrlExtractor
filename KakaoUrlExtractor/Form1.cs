using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Runtime.Serialization;
using System.Windows.Documents;
using System.Diagnostics;

namespace KakaoUrlExtractor
{
    public partial class MainForm : Form
    {
        //List<URL> urlList = new List<URL>();
        List<string> strList = new List<string>();
        string url = " ";

        public MainForm()
        {
            InitializeComponent();
        }
        private void Open_Click(object sender, EventArgs e)
        {
            OpenTxetFile();
        }
        private void OpenTxetFile()
        {
            OpenFileDialog txt = new OpenFileDialog();
            txt.InitialDirectory = "C://";
            txt.Filter = "텍스트 파일 (*.txt) | *.txt";

            if (txt.ShowDialog() == DialogResult.OK)
            {
                string filePath = txt.FileName;
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        string text = " ";
                        string firstText = sr.ReadLine();
                        
                        if (!firstText.Contains("카카오톡"))
                        {
                            MessageBox.Show("카카오톡 대화 파일이 아닙니다.", "ERROR!", MessageBoxButtons.OK);
                            return;
                        }
                        while ((text = sr.ReadLine()) != null)
                        {
                            
                            if (text.Length > 0 && text.StartsWith("["))
                            {
                                string[] temp = text.Split(new string[] { "] " }, StringSplitOptions.None);
                                //MessageBox.Show(temp[0] + "\n" + temp[1], "ㅋㅋ", MessageBoxButtons.OK);
                                if (temp[2].StartsWith("https://"))
                                {
                                    url = temp[2];
                                    strList.Add(url);
                                    listBox1.Items.Add(url);
                                    //MessageBox.Show(url, "ㅋㅋ", MessageBoxButtons.OK);
                                }
                                else continue;
                            }
                            
                        }
                        
                    }
                }
                
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void SaveFile()
        {
            using (FileStream fs = new FileStream("URL.txt", FileMode.Create))
            {
                StreamWriter sw = new StreamWriter(fs);

                for(int i = 0; i < strList.Count; i++)
                {
                    sw.WriteLine(strList[i], "\n");
                }
                sw.Close();
            }
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedUrl = listBox1.SelectedItem.ToString();
            Clipboard.SetData(DataFormats.Text, (Object)selectedUrl);
            Process.Start(selectedUrl);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("카카토오톡 PC 버전에서 대화 내보내기를 이용해 대화 내용을 추출합니다.\n" +
                "텍스트 파일을 불러온 뒤 선택한 URL이 등록된 브라우저를 통해 열립니다.", "Help", MessageBoxButtons.OK);
        }
    }
}
