using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace getapkinfo
{
    public partial class StartWinForm : Form
    {
        public StartWinForm()
        {
            InitializeComponent();
        }
        /// <summary>  
        /// 执行DOS命令，返回DOS命令的输出  
        /// </summary>  
        /// <param name="dosCommand">dos命令</param>  
        /// <param name="milliseconds">等待命令执行的时间（单位：毫秒），  
        /// 如果设定为0，则无限等待</param>  
        /// <returns>返回DOS命令的输出</returns>  
        public static string Execute(string command, int seconds)
        {
            string output = ""; //输出字符串  
            if (command != null && !command.Equals(""))
            {
                Process process = new Process();//创建进程对象  
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "cmd.exe";//设定需要执行的命令  
                startInfo.Arguments = "/C " + command;//“/C”表示执行完命令后马上退出  
                                                      //  MessageBox.Show(command);
                startInfo.UseShellExecute = false;//不使用系统外壳程序启动  
                startInfo.RedirectStandardInput = false;//不重定向输入  
                startInfo.RedirectStandardOutput = true; //重定向输出  
                startInfo.CreateNoWindow = true;//不创建窗口  
                startInfo.RedirectStandardError = false;
                startInfo.StandardOutputEncoding = Encoding.UTF8;
                process.StartInfo = startInfo;
                try
                {
                    if (process.Start())//开始进程  
                    {
                        if (seconds == 0)
                        {
                            process.WaitForExit();//这里无限等待进程结束  
                        }
                        else
                        {
                            process.WaitForExit(seconds); //等待进程结束，等待时间为指定的毫秒  
                        }
                        output = process.StandardOutput.ReadToEnd();//读取进程的输出  
                    }
                }
                catch
                {
                }
                finally
                {
                    if (process != null)
                        process.Close();
                }
            }
            StreamWriter sw = File.AppendText(Application.StartupPath + "\\log.txt");
            string logStr = System.DateTime.Now.ToString("G") + ":Run\\’" + command + "\\'  Result:\\'" + output + "\n";
            sw.Write(logStr);
            sw.Close();
            return output;
        }
        private void KillProcessExists(string appName)
        {
            Process[] processes = Process.GetProcessesByName(appName);
            foreach (Process p in processes)
            {
               // MessageBox.Show(p.MainModule.FileName);
                if (System.IO.Path.Combine(Application.StartupPath, appName+"exe") == p.MainModule.FileName)
                {
                    p.Kill();
                    p.Close();
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string tmpStr = "";
            string Ip = "";
            Ip = this.textBox1.Text;
            KillProcessExists("adb");
            if (Ip != "")
            {
                tmpStr = Execute("adb connect " + this.textBox1.Text, 10);
                if (tmpStr.Contains("connected"))
                {
                    MessageBox.Show(tmpStr);
                    mainForm mf = new mainForm();
                    mf.Show();
                    this.Visible = false;

                }
                else
                {
                    MessageBox.Show(tmpStr);

                }
            }
            else
            {
                MessageBox.Show("请输入Ip地址");
            }

           

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }
    }
}
