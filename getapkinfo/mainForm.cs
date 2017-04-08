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
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

   

        /// <summary>
        /// 存储apk启动信息package/activity
        /// </summary>
        string[] apkinfos = new string[8];
        /// <summary>
        /// 存储遥控码
        /// </summary>
        string[] rmcodes = new string[8];
       
       
        /// <summary>
        /// 获取配置文件中rmcode设置情况
        /// </summary>
        /// <param name="filepath">配置文件路径</param>
        /// <returns>返回rmcode数组</returns>
        public  string [] LoadRmset(string filepath)
        {
            string[] resultStr = new string[8];
            resultStr[0] = RWINI.ReadString("rm","rm1","", filepath);
            resultStr[1] = RWINI.ReadString("rm", "rm2", "", filepath);
            resultStr[2] = RWINI.ReadString("rm", "rm3", "", filepath);
            resultStr[3] = RWINI.ReadString("rm", "rm4", "", filepath);
            resultStr[4] = RWINI.ReadString("rm", "rm5", "", filepath);
            resultStr[5] = RWINI.ReadString("rm", "rm6", "", filepath);
            resultStr[6] = RWINI.ReadString("rm", "rm7", "", filepath);
            resultStr[7] = RWINI.ReadString("rm", "rm8", "", filepath);
            
            return resultStr;

        }
        /// <summary>
        /// 获取配置文件中app设置情况
        /// </summary>
        /// <param name="filepath">配置文件路径</param>
        /// <returns>返回app设置信息数组</returns>
        public string[] LoadAppset(string filepath)
        {
            string[] resultStr = new string[8];

            resultStr[0] = RWINI.ReadString("app", "app1", "", filepath);
         //   MessageBox.Show(resultStr[0]);
            resultStr[1] = RWINI.ReadString("app", "app2", "", filepath);
            resultStr[2] = RWINI.ReadString("app", "app3", "", filepath);
            resultStr[3] = RWINI.ReadString("app", "app4", "", filepath);
            resultStr[4] = RWINI.ReadString("app", "app5", "", filepath);
            resultStr[5] = RWINI.ReadString("app", "app6", "", filepath);
            resultStr[6] = RWINI.ReadString("app", "app7", "", filepath);
            resultStr[7] = RWINI.ReadString("app", "app8", "", filepath);
            return resultStr;

        }

        /// <summary>
        /// 获取配置文件中rmcode设置情况
        /// </summary>
        /// <param name="filepath">配置文件路径</param>
        /// <returns></returns>
        public bool Saveini(string filepath,string [] rmcodes,string [] apkinfos)
        {
            bool resultFlag = false;

            rmcodes[0]=this.rmcTbox1.Text;
            rmcodes[1]=this.rmcTbox2.Text ;
            rmcodes[2]=this.rmcTbox3.Text;
            rmcodes[3] = this.rmcTbox4.Text;
            rmcodes[4] = this.rmcTbox5.Text;
            rmcodes[5] = this.rmcTbox6.Text;
            rmcodes[6] = this.rmcTbox7.Text;
            rmcodes[7] = this.rmcTbox8.Text;
            apkinfos[0]=this.apkinfoTbox1.Text ;
            apkinfos[1] = this.apkinfoTbox2.Text ;
            apkinfos[2] = this.apkinfoTbox3.Text ;
            apkinfos[3] = this.apkinfoTbox4.Text ;
            apkinfos[4] = this.apkinfoTbox5.Text ;
            apkinfos[5] = this.apkinfoTbox6.Text ;
            apkinfos[6] = this.apkinfoTbox7.Text;
            apkinfos[7] = this.apkinfoTbox8.Text;
            for (int i = 1; i <= 8; i++)
            {
               // MessageBox.Show("rm" +  i.ToString()+"="+ rmcodes[i - 1]);
                resultFlag = RWINI.WriteString("app", "app" + i.ToString(), apkinfos[i-1], filepath);
                resultFlag = RWINI.WriteString("rm", "rm" + i.ToString(), rmcodes[i - 1], filepath);

            }
           
            return resultFlag;

        }

    
        /// <summary> 
        /// 生成进程 
        /// </summary> 
        /// <param name="filename"></param> 
        /// <returns></returns> 
        public static Process CreateProcess(string filename, string dir)
        {
            Process p = new Process();//进程 
            p.StartInfo.FileName = filename;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.CreateNoWindow = true;
            //下面二句不可少，不然会出错 
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardError = true;
            if (!string.IsNullOrEmpty(dir))
                p.StartInfo.WorkingDirectory = dir;
            return p;
        }
        /// <summary>  
        /// 执行DOS命令，返回DOS命令的输出  
        /// </summary>  
        /// <param name="dosCommand">dos命令</param>  
        /// <param name="milliseconds">等待命令执行的时间（单位：毫秒），  
        /// 如果设定为0，则无限等待</param>  
        /// <returns>返回DOS命令的输出</returns>  
        public static string ExecuteCMD(string command, int seconds)
        {
            StringBuilder result = new StringBuilder();
            if (command != null && !command.Equals(""))
            {
                Process p = CreateProcess(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), "cmd.exe"), Application.StartupPath);
               
                //这里就是我们用来异步读取输出的二个委托事件 
                //一个是正确的信息，另一个是错误的信息输出 
                p.ErrorDataReceived += new DataReceivedEventHandler(delegate (object sender1, DataReceivedEventArgs e1)
                {
                    //也可以是你自义定的其它处理,比如用console.write打印出来等。ShowErrorInfo(e.Data); 
                    result.AppendLine(e1.Data);   //收集所有的出错信息                  
                });
                p.OutputDataReceived += new DataReceivedEventHandler(delegate (object sender1, DataReceivedEventArgs e1)
                {
                    //  ShowNormalInfo(e1.Data);
                    result.AppendLine(e1.Data);
                });
                //try
                //{
                //    if (p.Start())//开始进程  
                //    {

                //        if (seconds == 0)
                //        {
                //            p.WaitForExit();//这里无限等待进程结束  
                //        }
                //        else
                //        {
                //            p.WaitForExit(seconds); //等待进程结束，等待时间为指定的毫秒  
                //        }

                //       // output = process.StandardOutput.ReadToEnd(); ;
                //    }
                //}
                //catch
                //{
                //}
                //finally
                //{
                //    if (p != null)
                //        p.Close();
                //}
                p.Start();
            //这二句别忘了，不然不会触发上面的事件 
            p.BeginErrorReadLine();
            p.BeginOutputReadLine();
            //可以做你要的操作，执行批处理或其它控制台程序 
            p.StandardInput.WriteLine(command);
          //  p.StandardInput.WriteLine("adb push .\\exitiptv.sh /system/etc/exitiptv.sh");
            //p.StandardInput.WriteLine(input); 
            /////////////// 
            p.StandardInput.WriteLine("exit");//最后打入退出命令 
            p.WaitForExit();
            p.Close();
            p.Dispose();

             }
            StreamWriter sw = File.AppendText(Application.StartupPath + "\\log.txt");
            string logStr = System.DateTime.Now.ToString("G") + ":Run\\’" + command + "\\'  Result:\\'" + result.ToString() + "\n";
            sw.Write(logStr);
            sw.Close();
            return result.ToString();
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
           // StringBuilder result = new StringBuilder();
            if (command != null && !command.Equals(""))
            {
              //  command = command + " >>";
                Process process = new Process();//创建进程对象  
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "cmd.exe";//设定需要执行的命令  
                startInfo.Arguments = "/C " + command+ "&exit";//“/C”表示执行完命令后马上退出  
              //  MessageBox.Show(command);
                startInfo.UseShellExecute = false;//不使用系统外壳程序启动  
                startInfo.RedirectStandardInput = false;//不重定向输入  
                startInfo.RedirectStandardOutput = true; //重定向输出  
                startInfo.CreateNoWindow = true;//不创建窗口  
             //   startInfo.RedirectStandardError = false;
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

                         output = process.StandardOutput.ReadToEnd(); ;
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
            StreamWriter sw = File.AppendText(Application.StartupPath+ "\\log.txt");
            string logStr = System.DateTime.Now.ToString("G") + ":Run\\’" + command + "\\'  Result:\\'" + output + "\n";
            sw.Write(logStr);
            sw.Close();
            return output;
        }
      
        /// <summary>
        /// 获取APK程序的package/activity
        /// </summary>
        /// <param name="apkpath">APK应用的路径</param>
        /// <returns></returns>
        private string getapkinfo(string apkpath)
        {
            string cmdstr = "";
            string packStr = "";
            string activityStr = "";
            string result = "";
            string[] tmpStr;
            cmdstr = "aapt dump badging " + apkpath + " | findstr \"package\"";
            packStr = Execute(cmdstr,10);
            cmdstr = "aapt dump badging " + apkpath + " | findstr \"launchable\"";
            activityStr = Execute(cmdstr, 10);
            if (packStr.Contains("package")&& activityStr.Contains("launchable-activity"))
            {
                tmpStr = packStr.Split('\'');
                packStr = tmpStr[1];
                tmpStr = activityStr.Split('\'');
                activityStr = tmpStr[1];
                result = packStr + "/" + activityStr;
            }
            else
            {
                result = null;
                
            }                             
            return result;


        }
        /// <summary>
        /// 获取遥控器按键码
        /// </summary>
        /// <returns>遥控按键按下后，返回遥控码</returns>
        private string getrmcode()
        {
            string tmpStr = "";
            string resultStr = "";
            tmpStr= Execute("adb shell getevent -c 1 /dev/input/event0", 0);
            resultStr = tmpStr.Replace(" ","-");
           // MessageBox.Show(resultStr.Length.ToString());
            //获取的遥控码不正确
            if(resultStr.Length<=18)
            {
                return null;
            }
            return resultStr.Substring(0,18);
        }
        /// <summary>
        /// 获取遥控器按键码
        /// </summary>
        /// <returns>遥控按键按下后，返回遥控码</returns>
        private bool pushFile()
        {
            string cmdStr = "";
            string result = "";
            cmdStr = "adb shell \"mount -o remount,rw /system\"";
            result = ExecuteCMD(cmdStr, 50);
            if (result.Contains("error")|| result.Contains("failed"))
            {
                MessageBox.Show(result);
                return false;
            }
            //  
            cmdStr = "adb push .\\exitiptv.sh /system/etc/exitiptv.sh";
            result = ExecuteCMD(cmdStr, 50);
            if (result.Contains("error") || result.Contains("failed"))
            {
                MessageBox.Show(result);
                return false;
            }
            cmdStr = "adb shell \"chmod 0755 /system/etc/exitiptv.sh\"";
            result = ExecuteCMD(cmdStr, 50);
            if (result.Contains("error") || result.Contains("failed"))
            {
                MessageBox.Show(result);
                return false;
            }
            cmdStr = "adb push .\\exitiptvcfg.ini  /system/etc/exitiptvcfg.ini ";
            result = ExecuteCMD(cmdStr, 50);
            if (result.Contains("error") || result.Contains("failed"))
            {
                MessageBox.Show(result);
                return false;
            }
            cmdStr = "adb pull /system/etc/install-recovery-2.sh .\\install-recovery-2.sh";
            result = ExecuteCMD(cmdStr, 50);
            if (result.Contains("error") || result.Contains("failed"))
            {
                MessageBox.Show(result);
                return false;
            }
            //判断是否install-recovery-2.sh文件中是否存在 /system/etc/exitiptv.sh^&

            cmdStr = "find  \"/system/etc/exitiptv.sh&\" install-recovery-2.sh";
            result = Execute(cmdStr, 50);
            if (!result.Contains("/system/etc/exitiptv.sh"))
            {
                //cmdStr = "echo /system/etc/exitiptv.sh^& >>install-recovery-2.sh";
                cmdStr = "echo /system/etc/exitiptv.sh^& >>install-recovery-2.sh";
                result = Execute(cmdStr, 50);
                if (result.Contains("error") || result.Contains("failed"))
                {
                    MessageBox.Show(result);
                    return false;
                }
                //     MessageBox.Show(result);
            }
            cmdStr = "adb push .\\install-recovery-2.sh /system/etc/install-recovery-2.sh";

             result = ExecuteCMD(cmdStr, 50);
            if (result.Contains("error") || result.Contains("failed"))
            {
                MessageBox.Show(result);
                return false;
            }
            //     MessageBox.Show(result);
            cmdStr = "adb shell \"chmod 0755 /system/etc/install-recovery-2.sh\"";
            result = ExecuteCMD(cmdStr, 50);
            if (result.Contains("error") || result.Contains("failed"))
            {
                MessageBox.Show(result);
                return false;
            }
            //   MessageBox.Show(result);
            return true;
        }




        private void getapktBtn1_Click(object sender, EventArgs e)
        {
            //string apkpath = "";


            //if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    apkpath = openFileDialog1.FileName;
            //    apkinfos[0] = getapkinfo(apkpath);
            //    if (apkinfos[0] != null)
            //    {
            //        this.apkinfoTbox1.Text = apkinfos[0];
            //        MessageBox.Show(openFileDialog1.SafeFileName + "启动信息获取成功！");
            //    }
            //    else
            //    {
            //        MessageBox.Show(openFileDialog1.SafeFileName + "启动信息获取失败！");
            //        MessageBox.Show("请将APK文件存放至较短目录，如d:\\apk\\");
            //    }
            //}
            string cmdStr = "";
            string result = "";
            string[] results = new string[10];
            MessageBox.Show("请在机顶盒上打开想设置的APP后，单击“确定”按钮");
            cmdStr = "adb shell dumpsys activity |find \"mFocusedActivity\"";
            result = Execute(cmdStr, 20);
            results = result.Split(' ');
            apkinfos[0] = results[5];
            if (apkinfos[0] != null)
            {
                this.apkinfoTbox1.Text = apkinfos[0];
                MessageBox.Show("启动信息获取成功！");
            }
            else
            {
                MessageBox.Show( "启动信息获取失败！");
             
            }

        }


        private void getapktBtn2_Click(object sender, EventArgs e)
        {
            string cmdStr = "";
            string result = "";
            string[] results = new string[10];
            MessageBox.Show("请在机顶盒上打开想设置的APP后，单击“确定”按钮");
            cmdStr = "adb shell dumpsys activity |find \"mFocusedActivity\"";
            result = Execute(cmdStr, 20);
            results = result.Split(' ');
            apkinfos[1] = results[5];
            if (apkinfos[1] != null)
            {
                this.apkinfoTbox2.Text = apkinfos[1];
                MessageBox.Show("启动信息获取成功！");
            }
            else
            {
                MessageBox.Show("启动信息获取失败！");
            }
               
           
        }

        private void getapktBtn3_Click(object sender, EventArgs e)
        {
            string cmdStr = "";
            string result = "";
            string[] results = new string[10];
            MessageBox.Show("请在机顶盒上打开想设置的APP后，单击“确定”按钮");
            cmdStr = "adb shell dumpsys activity |find \"mFocusedActivity\"";
            result = Execute(cmdStr, 20);
            results = result.Split(' ');
            apkinfos[2] = results[5];
            if (apkinfos[2] != null)
                {
                    this.apkinfoTbox3.Text = apkinfos[2];
                MessageBox.Show("启动信息获取成功！");
            }
                else
                {
                MessageBox.Show("启动信息获取失败！");
            }
               
                    
        }

        private void getapktBtn4_Click(object sender, EventArgs e)
        {
            string cmdStr = "";
            string result = "";
            string[] results = new string[10];
            MessageBox.Show("请在机顶盒上打开想设置的APP后，单击“确定”按钮");
            cmdStr = "adb shell dumpsys activity |find \"mFocusedActivity\"";
            result = Execute(cmdStr, 20);
            results = result.Split(' ');
            apkinfos[3] = results[5];
            if (apkinfos[3] != null)
                {
                    this.apkinfoTbox4.Text = apkinfos[3];
                MessageBox.Show("启动信息获取成功！");
            }
                else
                {
                MessageBox.Show("启动信息获取失败！");
            }
              
          
        }

        private void getapktBtn5_Click(object sender, EventArgs e)
        {
            string cmdStr = "";
            string result = "";
            string[] results = new string[10];
            MessageBox.Show("请在机顶盒上打开想设置的APP后，单击“确定”按钮");
            cmdStr = "adb shell dumpsys activity |find \"mFocusedActivity\"";
            result = Execute(cmdStr, 20);
            results = result.Split(' ');
            apkinfos[4] = results[5];
            if (apkinfos[4] != null)
                {
                    this.apkinfoTbox5.Text = apkinfos[4];
                    MessageBox.Show("启动信息获取成功！");
            }
                else
                {
                MessageBox.Show("启动信息获取失败！");
            }
           
        }

        private void getapktBtn6_Click(object sender, EventArgs e)
        {
            string cmdStr = "";
            string result = "";
            string[] results = new string[10];
            MessageBox.Show("请在机顶盒上打开想设置的APP后，单击“确定”按钮");
            cmdStr = "adb shell dumpsys activity |find \"mFocusedActivity\"";
            result = Execute(cmdStr, 20);
            results = result.Split(' ');
            apkinfos[5] = results[5];
            if (apkinfos[5] != null)
                {
                    this.apkinfoTbox6.Text = apkinfos[5];
                MessageBox.Show("启动信息获取成功！");
            }
                else
                {
                MessageBox.Show("启动信息获取失败！");
            }              
           
        }

        private void getapktBtn7_Click(object sender, EventArgs e)
        {
            string cmdStr = "";
            string result = "";
            string[] results = new string[10];
            MessageBox.Show("请在机顶盒上打开想设置的APP后，单击“确定”按钮");
            cmdStr = "adb shell dumpsys activity |find \"mFocusedActivity\"";
            result = Execute(cmdStr, 20);
            results = result.Split(' ');
            apkinfos[6] = results[5];
            if (apkinfos[6] != null)
                {
                    this.apkinfoTbox7.Text = apkinfos[6];
                MessageBox.Show("启动信息获取成功！");
            }
                else
                {
                MessageBox.Show("启动信息获取失败！");
            }
              
           
        }

        private void getapktBtn8_Click(object sender, EventArgs e)
        {
            string cmdStr = "";
            string result = "";
            string[] results = new string[10];
            MessageBox.Show("请在机顶盒上打开想设置的APP后，单击“确定”按钮");
            cmdStr = "adb shell dumpsys activity |find \"mFocusedActivity\"";
            result = Execute(cmdStr, 20);
            results = result.Split(' ');
            apkinfos[7] = results[5];
            if (apkinfos[7] != null)
                {
                    this.apkinfoTbox8.Text = apkinfos[7];
                MessageBox.Show("启动信息获取成功！");
            }
                else
                {
                    MessageBox.Show( "启动信息获取失败！");
                   
                }

         
           
        }

        private void getrmBtn1_Click(object sender, EventArgs e)
        {
            string tmpRmcode = "";
            MessageBox.Show("单击确定按钮后，按下遥控器按键！");
            tmpRmcode = getrmcode();
            if(tmpRmcode!=null)
            {
                rmcodes[0] = tmpRmcode;
             this.rmcTbox1.Text = rmcodes[0];
               MessageBox.Show("遥控码获取成功！");

            }
            else
            {
                MessageBox.Show("遥控码获取失败！");
            }
              
        }
        private void getrmBtn2_Click(object sender, EventArgs e)
        {
            string tmpRmcode = "";
            MessageBox.Show("单击确定按钮后，按下遥控器按键！");
            tmpRmcode = getrmcode();
            if (tmpRmcode != null)
            {
                rmcodes[1] = tmpRmcode;
            this.rmcTbox2.Text = rmcodes[1];
            MessageBox.Show("遥控码获取成功！");

            }
            else
            {
                MessageBox.Show("遥控码获取失败！");
            }
        }

        private void getrmBtn3_Click(object sender, EventArgs e)
        {
            string tmpRmcode = "";
            MessageBox.Show("单击确定按钮后，按下遥控器按键！");
            tmpRmcode = getrmcode();
            if (tmpRmcode != null)
            {
                rmcodes[2] = tmpRmcode;
            this.rmcTbox3.Text = rmcodes[2];
            MessageBox.Show("遥控码获取成功！");
            }
            else
            {
                MessageBox.Show("遥控码获取失败！");
            }

        }
        private void getrmBtn4_Click(object sender, EventArgs e)
        {
            string tmpRmcode = "";
            MessageBox.Show("单击确定按钮后，按下遥控器按键！");
            tmpRmcode = getrmcode();
            if (tmpRmcode != null)
            {
                rmcodes[3] = tmpRmcode;
                this.rmcTbox4.Text = rmcodes[3];
                MessageBox.Show("遥控码获取成功！");
            }
            else
            {
                MessageBox.Show("遥控码获取失败！");
            }

        }

        private void getrmBtn5_Click(object sender, EventArgs e)
        {
            string tmpRmcode = "";
            MessageBox.Show("单击确定按钮后，按下遥控器按键！");
            tmpRmcode = getrmcode();
            if (tmpRmcode != null)
            {
                rmcodes[4] = tmpRmcode;
                this.rmcTbox5.Text = rmcodes[4];
                MessageBox.Show("遥控码获取成功！");
            }
            else
            {
                MessageBox.Show("遥控码获取失败！");
            }

        }

        private void getrmBtn6_Click(object sender, EventArgs e)
        {
            string tmpRmcode = "";
            MessageBox.Show("单击确定按钮后，按下遥控器按键！");
            tmpRmcode = getrmcode();
            if (tmpRmcode != null)
            {
                rmcodes[5] = tmpRmcode;
            this.rmcTbox6.Text = rmcodes[5];
            MessageBox.Show("遥控码获取成功！");
            }
            else
            {
                MessageBox.Show("遥控码获取失败！");
            }
        }

        private void getrmBtn7_Click(object sender, EventArgs e)
        {
            string tmpRmcode = "";
            MessageBox.Show("单击确定按钮后，按下遥控器按键！");
            tmpRmcode = getrmcode();
            if (tmpRmcode != null)
            {
                rmcodes[6] = tmpRmcode;
            this.rmcTbox7.Text = rmcodes[6];
            MessageBox.Show("遥控码获取成功！");
            }
            else
            {
                MessageBox.Show("遥控码获取失败！");
            }
        }

        private void getrmBtn8_Click(object sender, EventArgs e)
        {
            string tmpRmcode = "";
            MessageBox.Show("单击确定按钮后，按下遥控器按键！");
            tmpRmcode = getrmcode();
            if (tmpRmcode != null)
            {
                rmcodes[7] = tmpRmcode;
            this.rmcTbox8.Text = rmcodes[7];
            MessageBox.Show("遥控码获取成功！");
            }
            else
            {
                MessageBox.Show("遥控码获取失败！");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            rmcodes = LoadRmset(Application.StartupPath+ "\\exitiptvcfg.ini");
            apkinfos=LoadAppset(Application.StartupPath + "\\exitiptvcfg.ini");
            this.rmcTbox1.Text = rmcodes[0];
            this.rmcTbox2.Text = rmcodes[1];
            this.rmcTbox3.Text = rmcodes[2];
            this.rmcTbox4.Text = rmcodes[3];
            this.rmcTbox5.Text = rmcodes[4];
            this.rmcTbox6.Text = rmcodes[5];
            this.rmcTbox7.Text = rmcodes[6];
            this.rmcTbox8.Text = rmcodes[7];
            this.apkinfoTbox1.Text = apkinfos[0];
            this.apkinfoTbox2.Text = apkinfos[1];
            this.apkinfoTbox3.Text = apkinfos[2];
            this.apkinfoTbox4.Text = apkinfos[3];
            this.apkinfoTbox5.Text = apkinfos[4];
            this.apkinfoTbox6.Text = apkinfos[5];
            this.apkinfoTbox7.Text = apkinfos[6];
            this.apkinfoTbox8.Text = apkinfos[7];
            //获取连接设备信息
            string cmdStr = "";
            string result = "";
            cmdStr = "adb shell getprop ro.product.device";
            result = Execute(cmdStr, 1);
            this.Text = "已连接至：" + result+"     作者：一〇一二  QQ：64652178  支付宝：18604430620";

        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            bool saveFlag = false;
            saveFlag = Saveini(Application.StartupPath + "\\exitiptvcfg.ini", rmcodes, apkinfos);
            if(saveFlag==true)
            {
                MessageBox.Show("配置文件保存成功!");

            }
            else
            {
                MessageBox.Show("配置文件保存失败!");
            }


        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pushBtn_Click(object sender, EventArgs e)
        {

            bool pushFlag = false;
            pushFlag = pushFile();
          //  if(MessageBox.Show("此功能为测试版，"))
            if (pushFlag == true)
            {
                MessageBox.Show("文件已推送至机顶盒，请重启机顶盒检验是否成功!");

            }
            else
            {
                MessageBox.Show("文件推送失败!,请检查ADB链接状态");
            }

           
        }

        private void rebootBtn_Click(object sender, EventArgs e)
        {
            //string cmdStr = "";
            //string result = "";
            //cmdStr = "adb shell reboot";
            //result = ExecuteCMD(cmdStr, 10);
            //MessageBox.Show(result);
            this.backgroundWorker1.RunWorkerAsync();
            MessageBox.Show("机顶盒已重启");
        }

        private void aboutBtn_Click(object sender, EventArgs e)
        {
            AboutBox1 ab = new AboutBox1();
            ab.Show();
         

        }

        private void LoadiniBtn_Click(object sender, EventArgs e)
        {
            rmcodes = LoadRmset(Application.StartupPath + "\\exitiptvcfg.ini");
            apkinfos = LoadAppset(Application.StartupPath + "\\exitiptvcfg.ini");
            this.rmcTbox1.Text = rmcodes[0];
            this.rmcTbox2.Text = rmcodes[1];
            this.rmcTbox3.Text = rmcodes[2];
            this.rmcTbox4.Text = rmcodes[3];
            this.rmcTbox5.Text = rmcodes[4];
            this.rmcTbox6.Text = rmcodes[5];
            this.rmcTbox7.Text = rmcodes[6];
            this.rmcTbox8.Text = rmcodes[7];
            this.apkinfoTbox1.Text = apkinfos[0];
            this.apkinfoTbox2.Text = apkinfos[1];
            this.apkinfoTbox3.Text = apkinfos[2];
            this.apkinfoTbox4.Text = apkinfos[3];
            this.apkinfoTbox5.Text = apkinfos[4];
            this.apkinfoTbox6.Text = apkinfos[5];
            this.apkinfoTbox7.Text = apkinfos[6];
            this.apkinfoTbox8.Text = apkinfos[7];
            MessageBox.Show("载入配置成功");
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void apkinfoTbox1_TextChanged(object sender, EventArgs e)
        {
          //  apkinfos[0]= apkinfoTbox1
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            string cmdStr = "";
            string result = "";
            cmdStr = "adb shell reboot";
            result = ExecuteCMD(cmdStr, 10);
            MessageBox.Show(result);
        }

        private void GetLogBtn_Click(object sender, EventArgs e)
        {
            string cmdStr = "";
            string result = "";
            cmdStr = "adb pull /system/etc/exitiptv.log .\\exitiptv.log";
            result = ExecuteCMD(cmdStr, 10);
            if (result.Contains("error") || result.Contains("failed"))
            {
                MessageBox.Show("日志获取失败！请检查是否已正确连接机顶盒！");
                
            }
            else
            {
                    MessageBox.Show("日志exitiptv.log文件已保存至程序运行目录！");

            }

        }

        private void RunShellBtn_Click(object sender, EventArgs e)
        {
            // this.backgroundWorker2.RunWorkerAsync();
            string command = Application.StartupPath + "\\runshell.bat";
           // MessageBox.Show(command);
            System.Diagnostics.Process.Start(command);
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {

            string command = "adb shell /system/etc/exitiptv.sh";
            string output = ""; //输出字符串  
            int seconds = 20;
                                // StringBuilder result = new StringBuilder();
            if (command != null && !command.Equals(""))
            {
                //  command = command + " >>";
                Process process = new Process();//创建进程对象  
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "cmd.exe";//设定需要执行的命令  
                startInfo.Arguments = "/C " + command + "&exit";//“/C”表示执行完命令后马上退出  
                                                                //  MessageBox.Show(command);
                startInfo.UseShellExecute = true;//不使用系统外壳程序启动  
                startInfo.RedirectStandardInput = false;//不重定向输入  
                startInfo.RedirectStandardOutput = true; //重定向输出  
                startInfo.CreateNoWindow = true;//不创建窗口  
                                                //   startInfo.RedirectStandardError = false;
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

                        output = process.StandardOutput.ReadToEnd(); ;
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
            //return output;
        }
    }

    
}
