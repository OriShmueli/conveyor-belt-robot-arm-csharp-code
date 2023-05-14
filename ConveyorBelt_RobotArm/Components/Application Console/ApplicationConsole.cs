using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConveyorBelt_RobotArm
{
    public partial class ApplicationConsole : UserControl
    {
        private int _currentLine = 0;
        public int CurrentLine { get { return _currentLine; } }

        public ApplicationConsole()
        {
            AutoScroll = true;
            InitializeComponent();
        }

        //public void WritaeTextWithColor(string text, Color color)
        //{
        //    ApplicationConosleTextBox.Lines[_currentLine] = text;
        //    //ApplicationConosleTextBox.Find(text);
        //    //ApplicationConosleTextBox.SelectionColor = color;
        //    _currentLine++;
        //}

        private void AppendLine(string text, Color color)
        {
            if(ApplicationConosleTextBox.Text.Length == 0)
            {
                ApplicationConosleTextBox.Text = text;       
            }
            else
            {
                ApplicationConosleTextBox.AppendText("\r\n" + text);
            }

            ApplicationConosleTextBox.Find(text);
            ApplicationConosleTextBox.SelectionColor = color;
        }

        public async Task WriteLine(string text)
        {
            await Task.Run(() => {

                if (!this.IsHandleCreated)
                {
                    MessageBox.Show("err");
                    return;
                }   
                this.Invoke((MethodInvoker)delegate {
                    AppendLine(text, Color.Black);
                });
            });
        }  

        public async Task WriteLineRed(string text)
        {
            await Task.Run(() => {
                try
                {
                    this.Invoke((MethodInvoker)delegate {
                        AppendLine(text, Color.Red);
                    });
                }
                catch(InvalidOperationException ex)
                {
                    return;
                }
               
            });
        }

        public async Task WriteLineGreen(string text)
        {
            if (!this.IsHandleCreated)
            {
                MessageBox.Show("err");
                return;
            }

            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    AppendLine(text, Color.Green);
                });
            });
           
        }

        public async Task WriteLineBlue(string text)
        {
            if (!this.IsHandleCreated)
            {
                MessageBox.Show("err");
                return;
            }

            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    AppendLine(text, Color.FromArgb(8, 52, 204));
                });
            });


        }
    }
}
