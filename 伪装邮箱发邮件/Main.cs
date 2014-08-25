using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 伪装邮箱发邮件
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //初始化邮件服务器地址
            string smtpServer = "127.0.0.1";//SMTP服务器IP
            //设置邮件服务器发送端口,默认邮箱SMTP服务器端口是25
            int port = 25;//SMTP服务器端口
            //初始化发送者邮箱
            string fromAddress = "ADMIN";//SMTP验证帐号
            //初始化发送者密码
            string fromPassword = "ADMIN";//SMTP验证密码
            //初始化接收者邮箱
            string[] toAddress = { textBox1.Text }; //初始化抄送的邮箱
            string[] ccAddress = { textBox2.Text };
            //初始化邮件标题
            string subject = textBox3.Text;
            //初始化邮件内容
            string mailContent = string.Format(textBox4.Text);
            //新建一封邮件
            MailMessage message = new MailMessage();
            //设置邮件发送地址
            message.From = new MailAddress(textBox2.Text);
            //设置邮件内容为HTML类型(支持网页内容排版),非HTML内容则设置为false
            message.IsBodyHtml = true;
            //设置邮件标题
            message.Subject = textBox3.Text;
            //设置邮件内容
            message.Body = string.Format(textBox4.Text);
            //设置回复地址(这个可以忽略)
            message.ReplyTo = new MailAddress(textBox2.Text);
            //设置接收者邮箱地址
            foreach (string sendTo in toAddress)
            {
                message.To.Add(new MailAddress(sendTo));
            }
            //设置抄送邮箱地址
            foreach (string copyTo in ccAddress)
            {
                message.CC.Add(new MailAddress(copyTo));
            }

            //初始化Smtp服务客户端
            SmtpClient smtp = new SmtpClient(smtpServer);
            //设置Smtp服务端口
            smtp.Port = port;
            //登陆邮箱服务器
            smtp.Credentials = new NetworkCredential(fromAddress, fromPassword);
            //发送邮件
            smtp.Send(message);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar < 97 || e.KeyChar > 122) && e.KeyChar != 8 && e.KeyChar != 64 && e.KeyChar != 46)
            {
                e.Handled = true;
                MessageBox.Show("接收邮箱地址仅允许输入英文、数字以及@","温馨提示");
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && (e.KeyChar < 97 || e.KeyChar > 122) && e.KeyChar != 8 && e.KeyChar != 64 && e.KeyChar != 46)
            {
                e.Handled = true;
                MessageBox.Show("发送邮箱地址仅允许输入英文、数字以及@", "温馨提示");
            }
        }
    }
}
