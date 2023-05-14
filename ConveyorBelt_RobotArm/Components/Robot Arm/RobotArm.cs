using ConveyorBelt_RobotArm.State_Machine;
using ConveyorBelt_RobotArm.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ConveyorBelt_RobotArm.Data_Base.Sensors;
using ConveyorBelt_RobotArm.Data_Base.Packages_Properties;
using System.IO;
using ConveyorBelt_RobotArm.Components.Robot_Arm.States;
using ConveyorBelt_RobotArm.SeralPortCommunication.Send;

namespace ConveyorBelt_RobotArm
{ 
    //TODO: Fix tick frequency default is 1. currently its on 30.
    public partial class RobotArm : UserControl
    {
        public RobotArm()
        {
            InitializeComponent();
            ConveyorBeltPictureBox.Enabled = false;
            _sensor1 = new Sensor1();
            _sensor2 = new Sensor2();
            _buttonStates.Add(StartStage1Button);
            _buttonStates.Add(StartStage2Button);
            _buttonStates.Add(Path1Stage1Button);
            _buttonStates.Add(Path1Stage2Button);
            _buttonStates.Add(Path2Stage1Button);
            _buttonStates.Add(Path2Stage2Button);
            _buttonStates.Add(Path3Stage1Button);
            _buttonStates.Add(Path3Stage2Button);
        }
        private bool _isEdtingStateEnabled = false;
        public bool IsEdtingStateEnabled { get { return _isEdtingStateEnabled; } set { _isEdtingStateEnabled = value; } }

        private bool _currentbuttoneditingMovement = false;
        public bool CurrentButtonEditingMovement { get { return _currentbuttoneditingMovement; } set { _currentbuttoneditingMovement = value; } }

        #region components

        private List<Button> _buttonStates = new List<Button>();

        private Sensor1 _sensor1;
        public Sensor1 Sensor1 { get { return _sensor1; } }

        private Sensor2 _sensor2;
        public Sensor2 Sensor2 { get { return _sensor2; } }

        public void ResetSensors()
        {
            _sensor1.Reset();
            _sensor2.Reset();
        }

        #endregion

        #region Clear

        public async Task ClearForNextPackage()
        {

            List<Task> tasks = new List<Task>();

            //End
            tasks.Add(RotateFromArrowOff());

            //pathes
            if (_sensor1.GetPackage() is BlackAndYellowPackage)
            {
                tasks.Add(ClearBlackAndYellowPath());
                tasks.Add(BlackAndYellowStartPathOff());
            }

            if(_sensor2.GetPackage() is BlueAndMagneticPackage)
            {
                tasks.Add(ClearMagneticAndBluePath());
                tasks.Add(Sensor2Off());
                tasks.Add(Sensore2ArrowOff());
                tasks.Add(RotateToArrowOff());
            }

            if (_sensor2.GetPackage() is WhitePackage)
            {
                tasks.Add(ClearWhitePath());
                tasks.Add(Sensor2Off());
                tasks.Add(Sensore2ArrowOff());
                tasks.Add(RotateToArrowOff());
            }

            //pickup
            tasks.Add(PickupStateStage2Off());
            tasks.Add(PickupStateArrowOff());
            tasks.Add(PickupStateStage1Off());
            tasks.Add(PickupToArrowOff());

            //sensor 1
            tasks.Add(Sensor1Off());
            tasks.Add(Sensor1PathOff());

            tasks.Add(Task.Run(() => { ResetSensors(); }));

            try
            {
                await Task.WhenAll(tasks);
            }
            catch (ArgumentNullException ae)
            {
                MessageBox.Show(ae.Message);
            }

        }

        public async Task ClearWhitePath()
        {
            List<Task> tasks = new List<Task>();
            tasks.Add(WhiteToStagesArrowOff());
            tasks.Add(WhiteStateStage1Off());
            tasks.Add(WhiteStateArrowOff());
            tasks.Add(WhiteStateStage2Off());
            tasks.Add(WhiteToEndArrowOff());
            try
            {
                await Task.WhenAll(tasks);
            }
            catch (ArgumentNullException ae)
            {
                MessageBox.Show(ae.Message);
            }
        }

        public async Task ClearBlackAndYellowPath()
        {
            List<Task> tasks = new List<Task>();
            tasks.Add(BlackAndYellowToStagesArrowOff());
            tasks.Add(BlackAndYellowStateStage1Off());
            tasks.Add(BlackAndYellowStateArrowOff());
            tasks.Add(BlackAndYellowStateStage2Off());
            tasks.Add(BlackAndYellowToEndArrowOff());
            try
            {
                await Task.WhenAll(tasks);
            }
            catch (ArgumentNullException ae)
            {
                MessageBox.Show(ae.Message);
            }
        }

        public async Task ClearMagneticAndBluePath()
        {
            List<Task> tasks = new List<Task>();
            tasks.Add(MagneticAndBlueToStagesArrowOff());
            tasks.Add(MagneticAndBlueStateStage1Off());
            tasks.Add(MagneticAndBlueStateArrowOff());
            tasks.Add(MagneticAndBlueStateStage2Off());
            tasks.Add(MagneticAndBlueToEndArrowOff());
            try
            {
                await Task.WhenAll(tasks);
            }
            catch (ArgumentNullException ae)
            {
                MessageBox.Show(ae.Message);
            }
        }

        #endregion

        #region End On Off Editing

        public async Task EndEditingColorOn()
        {
            await Task.Run(() =>
            {
                this.Invoke((MethodInvoker)delegate
                {                  
                    EndPanel.BackColor = Color.FromArgb(8, 52, 204);
                });
            });
        }

        public async Task EndEditingColorClear()
        {
            await Task.Run(() =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    EndPanel.BackColor = Color.White;
                });
            });
        }

        public async Task EndEditingToStartOn()
        {
            await Task.Run(() =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    EndPiece2Panel.BackColor = Color.FromArgb(8, 52, 204); 
                    EndPiece3Panel.BackColor = Color.FromArgb(8, 52, 204);
                    EndPiece4Panel.BackColor = Color.FromArgb(8, 52, 204);
                    EndPiece5PictureBox.Image = Resources.blue_arrow_up;
                    EndPanel.BackColor = Color.FromArgb(8, 52, 204);
                });
            });
        }

        public async Task EndEditingToStartclear()
        {
            await Task.Run(() =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    EndPiece2Panel.BackColor = Color.Black;
                    EndPiece3Panel.BackColor = Color.Black;
                    EndPiece4Panel.BackColor = Color.Black;
                    EndPiece5PictureBox.Image = Resources.black_arrow_up;
                });
            });
        }

        public async Task EndBackToStartOn()
        {
            await Task.Run(() =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    EndPiece2Panel.BackColor = Color.FromArgb(0, 255, 0);
                    EndPiece3Panel.BackColor = Color.FromArgb(0, 255, 0);
                    EndPiece4Panel.BackColor = Color.FromArgb(0, 255, 0);
                    EndPiece5PictureBox.Image = Resources.green_arrow_up;
                    EndPanel.BackColor = Color.FromArgb(0, 255, 0);
                });
            });
        }

        public async Task EndOffRequestedOn()
        {
            await Task.Run(() =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    OffPiece1Panel.BackColor = Color.FromArgb(255, 0, 0);
                    OffPiece2Panel.BackColor = Color.FromArgb(255, 0, 0);
                    OffPiece3PictureBox.Image = Resources.red_arrow_left;
                    EndPanel.BackColor = Color.FromArgb(255, 0, 0);
                    OnPiece1Panel.BackColor = Color.Black;
                    OnPiece2PictureBox.Image = Resources.black_arrow_down;
                });
            });
        }

        public async Task EndBackToStartClear()
        {
            await Task.Run(() =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    EndPiece2Panel.BackColor = Color.Black;
                    EndPiece3Panel.BackColor = Color.Black;
                    EndPiece4Panel.BackColor = Color.Black;
                    EndPiece5PictureBox.Image = Resources.black_arrow_up;
                });
            });
        }

        public async Task EndOffRequestedClear()
        {
            await Task.Run(() =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    OffPiece1Panel.BackColor = Color.Black;
                    OffPiece2Panel.BackColor = Color.Black;
                    OffPiece3PictureBox.Image = Resources.black_arrow_left;
                });
            });
        }

        public async Task EndClear()
        {
            await Task.Run(() =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    EndPanel.BackColor = Color.White;
                });
            });
        }

        //public async Task EndOfRequestedOff()
        //{
        //    await Task.Run(() =>
        //    {
        //        this.Invoke((MethodInvoker)delegate
        //        {
        //            OffPiece1Panel.BackColor = Color.FromArgb(255, 0, 0);
        //            OffPiece2Panel.BackColor = Color.FromArgb(255, 0, 0);
        //            OffPiece3PictureBox.Image = Resources.red_arrow_left;
        //            EndPanel.BackColor = Color.FromArgb(255, 0, 0);
        //        });
        //    });
        //}

        #endregion

        #region Rotate From

        public async Task RotateFromArrowOn()
        {
            await Task.Run(() =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    EndPiece1Panel.BackColor = Color.FromArgb(0, 255, 0);
                    EndPiece2PictureBox.Image = Resources.green_arrow_right;
                });
            });
        }

        public async Task RotateFromArrowOff()
        {
            await Task.Run(() =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    EndPiece1Panel.BackColor = Color.Black;
                    EndPiece2PictureBox.Image = Resources.black_triangle222;
                });
            });
        }

        public async Task BlinkMinus(CancellationToken token)
        {
            while (true)
            {
                await Task.Run(() =>
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        EndMinusPictureBox.Image = Resources.minus_on;
                    });
                });

                if (token.IsCancellationRequested)
                {
                    EndMinusPictureBox.Image = Resources.minus_off;
                    token.ThrowIfCancellationRequested();
                }
                
                await Task.Delay(1000);
                
                if (token.IsCancellationRequested)
                {
                    EndMinusPictureBox.Image = Resources.minus_off;
                    token.ThrowIfCancellationRequested();
                }

                await Task.Run(() =>
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        EndMinusPictureBox.Image = Resources.minus_off;
                    });
                });

                await Task.Delay(200);
            }
        }

        #endregion

        #region White Path

        public async Task WhiteToStagesArrowOn()
        {
            await Task.Run(() =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    Path3Part1Piece1Panel.BackColor = Color.FromArgb(0, 255, 0);
                    Path3Part1Piece2Panel.BackColor = Color.FromArgb(0, 255, 0);
                    Path3Part1Piece3PictureBox.Image = Resources.green_arrow_right;
                });
            });
        }

        public async Task WhiteToStagesArrowOff()
        {
            await Task.Run(() =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    Path3Part1Piece1Panel.BackColor = Color.Black;
                    Path3Part1Piece2Panel.BackColor = Color.Black;
                    Path3Part1Piece3PictureBox.Image = Resources.black_triangle222;
                });
            });
        }

        #region Stages (White)

        public async Task WhiteStateStage1Blink(CancellationToken token)
        {
            while (true)
            {
                await Task.Run(() =>
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        Path3Stage1Button.BackColor = Color.Red;
                    });
                });
                if (token.IsCancellationRequested)
                {
                    Path3Stage1Button.BackColor = Color.Red;
                    token.ThrowIfCancellationRequested();
                }
                await Task.Delay(100);
                await Task.Run(() =>
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        Path3Stage1Button.BackColor = Color.White;
                    });
                });
                if (token.IsCancellationRequested)
                {
                    Path3Stage1Button.BackColor = Color.Red;
                    token.ThrowIfCancellationRequested();
                }
                await Task.Delay(100);

            }
        }

        public async Task WhiteStateStage1On()
        {
            await Task.Run(() =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    Path3Stage1Button.BackColor = Color.Red;
                });
            });
        }

        public async Task WhiteStateStage1Off()
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    Path3Stage1Button.BackColor = Color.White;
                });
            });
        }

        public async Task WhiteStateArrowOn()
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    Path3Part2Piece1Panel.BackColor = Color.FromArgb(0, 255, 0);
                    Path3Part2Piece2PictureBox.Image = Resources.green_arrow_right;
                });
            });
        }

        public async Task WhiteStateArrowOff()
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    Path3Part2Piece1Panel.BackColor = Color.Black;
                    Path3Part2Piece2PictureBox.Image = Resources.black_triangle222;
                });
            });
        }

        public async Task WhiteStateStage2Blink(CancellationToken token)
        {
            while (true)
            {
                await Task.Run(() =>
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        Path3Stage2Button.BackColor = Color.Red;
                    });
                });
                if (token.IsCancellationRequested)
                {
                    Path3Stage2Button.BackColor = Color.Red;
                    token.ThrowIfCancellationRequested();
                }
                await Task.Delay(100);
                await Task.Run(() =>
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        Path3Stage2Button.BackColor = Color.White;
                    });
                });
                if (token.IsCancellationRequested)
                {
                    Path3Stage2Button.BackColor = Color.Red;
                    token.ThrowIfCancellationRequested();
                }
                await Task.Delay(100);

            }
        }

        public async Task WhiteStateStage2On()
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate
                {
                    Path3Stage2Button.BackColor = Color.Red;
                });
            });
        }

        public async Task WhiteStateStage2Off()
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    Path3Stage2Button.BackColor = Color.White;
                });
            });
        }

        #endregion

        public async Task WhiteToEndArrowOn()
        {
            await Task.Run(() =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    Path3Part3Piece1Panel.BackColor = Color.FromArgb(0, 255, 0);
                    Path2Part3Piece2Panel.BackColor = Color.FromArgb(0, 255, 0);
                    Path3Part3Piece3PictureBox.Image = Resources.green_arrow_up;
                });
            });
        }

        public async Task WhiteToEndArrowOff()
        {
            await Task.Run(() =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    Path3Part3Piece1Panel.BackColor = Color.Black;
                    Path2Part3Piece2Panel.BackColor = Color.Black;
                    Path3Part3Piece3PictureBox.Image = Resources.black_arrow_up;
                });
            });
        }

        #endregion

        #region Magnetic And Blue Path

        public async Task MagneticAndBlueToStagesArrowOn()
        {

            await Task.Run(() =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    Path2Part1Piece1Panel.BackColor = Color.FromArgb(0, 255, 0);
                    Path2Part1Piece2PictureBox.Image = Resources.green_arrow_right;
                });
            });
        }

        public async Task MagneticAndBlueToStagesArrowOff()
        {

            await Task.Run(() =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    Path2Part1Piece1Panel.BackColor = Color.Black;
                    Path2Part1Piece2PictureBox.Image = Resources.black_triangle222;
                });
            });
        }

        #region Stages (Blue And Magnetic)

        public async Task MagneticAndBlueStateStage1Blink(CancellationToken token)
        {
            while (true)
            {
                await Task.Run(() =>
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        Path2Stage1Button.BackColor = Color.Red;
                    });
                });
                if (token.IsCancellationRequested)
                {
                    Path2Stage1Button.BackColor = Color.Red;
                    token.ThrowIfCancellationRequested();
                }
                await Task.Delay(100);
                await Task.Run(() =>
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        Path2Stage1Button.BackColor = Color.White;
                    });
                });
                if (token.IsCancellationRequested)
                {
                    Path2Stage1Button.BackColor = Color.Red;
                    token.ThrowIfCancellationRequested();
                }
                await Task.Delay(100);

            }
        }

        public async Task MagneticAndBlueStateStage1On()
        {
            await Task.Run(() =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    Path2Stage1Button.BackColor = Color.Red;
                });
            });
        }

        public async Task MagneticAndBlueStateStage1Off()
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    Path2Stage1Button.BackColor = Color.White;
                });
            });
        }

        public async Task MagneticAndBlueStateArrowOn()
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    Path2Part2Piece1Panel.BackColor = Color.FromArgb(0, 255, 0);
                    Path2Part2Piece2PictureBox.Image = Resources.green_arrow_right;
                });
            });
        }

        public async Task MagneticAndBlueStateArrowOff()
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    Path2Part2Piece1Panel.BackColor = Color.Black;
                    Path2Part2Piece2PictureBox.Image = Resources.black_triangle222;
                });
            });
        }

        public async Task MagneticAndBlueStateStage2Blink(CancellationToken token)
        {
            while (true)
            {
                await Task.Run(() =>
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        Path2Stage2Button.BackColor = Color.Red;
                    });
                });
                if (token.IsCancellationRequested)
                {
                    Path2Stage2Button.BackColor = Color.Red;
                    token.ThrowIfCancellationRequested();
                }
                await Task.Delay(100);
                await Task.Run(() =>
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        Path2Stage2Button.BackColor = Color.White;
                    });
                });
                if (token.IsCancellationRequested)
                {
                    Path2Stage2Button.BackColor = Color.Red;
                    token.ThrowIfCancellationRequested();
                }
                await Task.Delay(100);

            }
        }

        public async Task MagneticAndBlueStateStage2On()
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate
                {
                    Path2Stage2Button.BackColor = Color.Red;
                });
            });
        }

        public async Task MagneticAndBlueStateStage2Off()
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    Path2Stage2Button.BackColor = Color.White;
                });
            });
        }

        #endregion

        public async Task MagneticAndBlueToEndArrowOn()
        {
            await Task.Run(() =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    Path2Part3Piece1Panel.BackColor = Color.FromArgb(0, 255, 0);
                    Path2Part3Piece2PictureBox.Image = Resources.green_arrow_right;
                });
            });
        }

        public async Task MagneticAndBlueToEndArrowOff()
        {
            await Task.Run(() =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    Path2Part3Piece1Panel.BackColor = Color.Black;
                    Path2Part3Piece2PictureBox.Image = Resources.black_triangle222;
                });
            });
        }

        #endregion

        #region Black And Yellow Path

        public async Task BlackAndYellowToStagesArrowOn()
        {

            await Task.Run(() =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    Path1Part1Piece1Panel.BackColor = Color.FromArgb(0, 255, 0);
                    Path1Part1Piece2Panel.BackColor = Color.FromArgb(0, 255, 0);
                    Path1Part1Piece3PictureBox.Image = Resources.green_arrow_right;
                });
            });
        }

        public async Task BlackAndYellowToStagesArrowOff()
        {

            await Task.Run(() =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    Path1Part1Piece1Panel.BackColor = Color.Black;
                    Path1Part1Piece2Panel.BackColor = Color.Black;
                    Path1Part1Piece3PictureBox.Image = Resources.black_triangle222;
                });
            });
        }

        #region Stages (Black And Yellow)
        
        public async Task BlackAndYellowStateStage1Blink(CancellationToken token)
        {
            while (true)
            {
                await Task.Run(() =>
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        Path1Stage1Button.BackColor = Color.Red;
                    });
                });
                if (token.IsCancellationRequested)
                {
                    Path1Stage1Button.BackColor = Color.Red;
                    token.ThrowIfCancellationRequested();
                }
                await Task.Delay(100);
                await Task.Run(() =>
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        Path1Stage1Button.BackColor = Color.White;
                    });
                });
                if (token.IsCancellationRequested)
                {
                    Path1Stage1Button.BackColor = Color.Red;
                    token.ThrowIfCancellationRequested();
                }
                await Task.Delay(100);

            }
        }

        public async Task BlackAndYellowStateStage1On()
        {
            await Task.Run(() =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    Path1Stage1Button.BackColor = Color.Red;
                });
            });
        }

        public async Task BlackAndYellowStateStage1Off()
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    Path1Stage1Button.BackColor = Color.White;
                });
            });
        }

        public async Task BlackAndYellowStateArrowOn()
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    Path1Part2Piece1Panel.BackColor = Color.FromArgb(0, 255, 0);
                    Path1Part2Piece2PictureBox.Image = Resources.green_arrow_right;
                });
            });
        }

        public async Task BlackAndYellowStateArrowOff()
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    Path1Part2Piece1Panel.BackColor = Color.Black;
                    Path1Part2Piece2PictureBox.Image = Resources.black_triangle222;
                });
            });
        }

        public async Task BlackAndYellowStateStage2Blink(CancellationToken token)
        {
            while (true)
            {
                await Task.Run(() =>
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        Path1Stage2Button.BackColor = Color.Red;
                    });
                });
                if (token.IsCancellationRequested)
                {
                    Path1Stage2Button.BackColor = Color.Red;
                    token.ThrowIfCancellationRequested();
                }
                await Task.Delay(100);
                await Task.Run(() =>
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        Path1Stage2Button.BackColor = Color.White;
                    });
                });
                if (token.IsCancellationRequested)
                {
                    Path1Stage2Button.BackColor = Color.Red;
                    token.ThrowIfCancellationRequested();
                }
                await Task.Delay(100);

            }
        }

        public async Task BlackAndYellowStateStage2On()
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate
                {
                    Path1Stage2Button.BackColor = Color.Red;
                });
            });            
        }

        public async Task BlackAndYellowStateStage2Off()
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    Path1Stage2Button.BackColor = Color.White;
                });
            });
        }

        #endregion

        public async Task BlackAndYellowToEndArrowOn()
        {
            await Task.Run(() =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    Path1Part3Piece1Panel.BackColor = Color.FromArgb(0, 255, 0);
                    Path1Part3Piece2Panel.BackColor = Color.FromArgb(0, 255, 0);
                    Path1Part3Piece3PictureBox.Image = Resources.green_arrow_down;
                });
            });
        }

        public async Task BlackAndYellowToEndArrowOff()
        {

            await Task.Run(() =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    Path1Part3Piece1Panel.BackColor = Color.Black;
                    Path1Part3Piece2Panel.BackColor = Color.Black;
                    Path1Part3Piece3PictureBox.Image = Resources.black_arrow_down;
                });
            });
        }

        #endregion

        #region RotateTo

        public async Task RotateToArrowOn()
        {

            await Task.Run(() =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    StartPart5Piece1Panel.BackColor = Color.FromArgb(0, 255, 0);
                    StartPart5Piece2Panel.BackColor = Color.FromArgb(0, 255, 0);
                    StartPart5Piece3PictureBox.Image = Resources.green_arrow_right;
                });
            });
        }

        public async Task RotateToArrowOff()
        {

            await Task.Run(() =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    StartPart5Piece1Panel.BackColor = Color.Black;
                    StartPart5Piece2Panel.BackColor = Color.Black;
                    StartPart5Piece3PictureBox.Image = Resources.black_triangle222;
                });
            });
        }

        public async Task BlinkPlus(CancellationToken token)
        {
            while (true)
            {
                await Task.Run(() =>
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        StartPlusPictureBox.Image = Resources.plus_on;
                    });
                });

                if (token.IsCancellationRequested)
                {
                    StartPlusPictureBox.Image = Resources.plus_off;
                    token.ThrowIfCancellationRequested();
                }
                
                await Task.Delay(1000);

                if (token.IsCancellationRequested)
                {
                    StartPlusPictureBox.Image = Resources.plus_off;
                    token.ThrowIfCancellationRequested();
                }

                await Task.Run(() =>
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        StartPlusPictureBox.Image = Resources.plus_off;
                    });
                });

                await Task.Delay(200);
            }
        }

        #endregion

        #region Black And Yellow Around Sensor 2

        public async Task BlackAndYellowStartPathOn()
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate
                {
                    StartPart3BackPiece1Panel.BackColor = Color.FromArgb(0, 255, 0);
                    StartPart3BackPiece2Panel.BackColor = Color.FromArgb(0, 255, 0);
                    StartPart3BackPiece3Panel.BackColor = Color.FromArgb(0, 255, 0);
                    StartPart3BackPiece4PictureBox.Image = Resources.green_arrow_up;
                    StartPart5Piece2Panel.BackColor = Color.FromArgb(0, 255, 0);
                    StartPart5Piece3PictureBox.Image = Resources.green_arrow_right;
                });
            });
        }

        public async Task BlackAndYellowStartPathOff()
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate
                {
                    StartPart3BackPiece1Panel.BackColor = Color.Black;
                    StartPart3BackPiece2Panel.BackColor = Color.Black;
                    StartPart3BackPiece3Panel.BackColor = Color.Black;
                    StartPart3BackPiece4PictureBox.Image = Resources.black_arrow_up;
                    StartPart5Piece2Panel.BackColor = Color.Black;
                    StartPart5Piece3PictureBox.Image = Resources.black_triangle222;
                });
            });
        }

        #endregion 

        #region Sensore2

        public async Task Sensor2Off()
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate
                {
                    Sensor2LabelText.Text = "Sensor 2";
                    Sensore2PictureBox.Image = Resources.sensore_icon;
                });
            });
        }

        public async Task Sensor2MagneticAndBlue()
        {
            //Sensore2PictureBox
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate
                {
                    Sensor2LabelText.Text = "Magnetic And Blue Package";
                    Sensore2PictureBox.Image = Resources.sensore_icon_magnetic_and_blue_package;
                });
            });
        }

        public async Task Sensor2white()
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate
                {
                    Sensor2LabelText.Text = "White Package";
                    Sensore2PictureBox.Image = Resources.sensore_icon_white_package;
                });
            });
        }

        public async Task Sensore2ArrowOn()
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate
                {
                    StartPart4Piece1Panel.BackColor = Color.FromArgb(0, 255, 0);
                    StartPart4Piece2PictureBox.Image = Resources.green_arrow_right;
                });
            });
        }

        public async Task Sensore2ArrowOff()
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate
                {
                    StartPart4Piece1Panel.BackColor = Color.Black;
                    StartPart4Piece2PictureBox.Image = Resources.black_triangle222;
                });
            });
        }
        #endregion

        #region Pickup State

        public async Task PickupToArrowOn()
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    StartPart2Piece1Panel.BackColor = Color.FromArgb(0, 255, 0);
                });
            });

            await Task.Delay(100);

            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    StartPart2Piece2PictureBox.Image = Resources.green_arrow_right;
                });
            });
        }

        public async Task PickupToArrowOff()
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    StartPart2Piece1Panel.BackColor = Color.Black;
                    StartPart2Piece2PictureBox.Image = Resources.black_triangle222;
                });
            });
        }

        public async Task PickupStateStage1Blink(CancellationToken token)
        {
            while (true)
            {
                await Task.Run(() =>
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        StartStage1Button.BackColor = Color.Red;
                    });
                });
                if (token.IsCancellationRequested)
                {
                    token.ThrowIfCancellationRequested();
                }
                await Task.Delay(100);
                await Task.Run(() =>
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        StartStage1Button.BackColor = Color.White;
                    });
                });
                if (token.IsCancellationRequested)
                {
                    StartStage1Button.BackColor = Color.Red;
                    token.ThrowIfCancellationRequested();
                }
                await Task.Delay(100);

            }

            //for (int i = 0; i < 4; i++)
            //{
            //    await Task.Run(() => {
            //        this.Invoke((MethodInvoker)delegate {
            //            StartStage1Button.BackColor = Color.Red;
            //        });
            //    });
            //    await Task.Delay(100);
            //    await Task.Run(() => {
            //        this.Invoke((MethodInvoker)delegate {
            //            StartStage1Button.BackColor = Color.White;
            //        });
            //    });
            //}
            //await Task.Delay(100);
            //await Task.Run(() => {
            //    this.Invoke((MethodInvoker)delegate {
            //        StartStage1Button.BackColor = Color.Red;
            //    });
            //});
        }

        public async Task PickupStateStage1On()
        {
            await Task.Run(() =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    StartStage1Button.BackColor = Color.Red;
                });
            });
        }

        public async Task PickupStateStage1Off()
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    StartStage1Button.BackColor = Color.White;
                });
            });
        }

        public async Task PickupStateArrowOn()
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    StartPart3Piece1Panel.BackColor = Color.FromArgb(0, 255, 0);
                    StartPart3Piece2PictureBox.Image = Resources.green_arrow_right;
                });
            });
        }

        public async Task PickupStateArrowOff()
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    StartPart3Piece1Panel.BackColor = Color.Black;
                    StartPart3Piece2PictureBox.Image = Resources.black_triangle222;
                });
            });
        }

        public async Task PickupStateStage2Blink(CancellationToken token)
        {
            while (true)
            {
                await Task.Run(() =>
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        StartStage2Button.BackColor = Color.Red;
                    });
                });
                if (token.IsCancellationRequested)
                {
                    StartStage2Button.BackColor = Color.Red;
                    token.ThrowIfCancellationRequested();
                }
                await Task.Delay(100);
                await Task.Run(() =>
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        StartStage2Button.BackColor = Color.White;
                    });
                });
                if (token.IsCancellationRequested)
                {
                    StartStage2Button.BackColor = Color.Red;
                    token.ThrowIfCancellationRequested();
                }
                await Task.Delay(100);

            }
        }

        public async Task PickupStateStage2On()
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate
                {
                    StartStage2Button.BackColor = Color.Red;
                });
            });
            //for (int i = 0; i < 4; i++)
            //{
            //    await Task.Run(() => {
            //        this.Invoke((MethodInvoker)delegate {
            //            StartStage2Button.BackColor = Color.Red;
            //        });
            //    });
            //    await Task.Delay(100);
            //    await Task.Run(() => {
            //        this.Invoke((MethodInvoker)delegate {
            //            StartStage2Button.BackColor = Color.White;
            //        });
            //    });
            //}
            //await Task.Delay(100);
            //await Task.Run(() => {
            //    this.Invoke((MethodInvoker)delegate {
            //        StartStage2Button.BackColor = Color.Red;
            //    });
            //});
        }

        public async Task PickupStateStage2Off()
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    StartStage2Button.BackColor = Color.White;
                });
            });
        }

        #endregion

        #region Sensor1

        public async Task Sensor1Off()
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate
                {
                    Sensor1LabelText.Text = "Sensor 1";
                    Sensore1PictureBox.Image = Resources.sensore_icon;
                });
            });
        }

        public async Task Sensor1BlackAndYellow()
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    Sensor1LabelText.Text = "Black And Yellow Package";
                    Sensore1PictureBox.Image = Resources.sensore_icon_black_and_yellow_package;
                    _sensor1.SetPackage(new BlackAndYellowPackage());
                });
            });
        }

        public async Task Sensor1WhiteOrMagnetic()
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    Sensor1LabelText.Text = "Magnetic And Blue / White Package";
                    Sensore1PictureBox.Image = Resources.sensore_icon_white_and_magnetic;
                    _sensor1.SetPackage(new WhiteOrMagneticPackage());
                });
            });
        }

        public async Task Sensor1PathOn()
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    StartPart1Piece1Panel.BackColor = Color.FromArgb(0, 255, 0);
                });
            });
            //await Task.Delay(50);
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    StartPart1Piece2PictureBox.Image = Properties.Resources.green_arrow_right;
                });
            });
        }

        public async Task Sensor1PathOff()
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    StartPart1Piece1Panel.BackColor = Color.Black;
                    StartPart1Piece2PictureBox.Image = Properties.Resources.black_triangle222;
                });
            });
        }

        #endregion

        #region Conveyor Belt

        public async Task ConveyorBeltPathOn()
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    StartPart2ConveyorBeltPiece1PictureBox.Image = Properties.Resources.green_arrow_up;
                });
            });
            //await Task.Delay(50);
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    StartPart2ConveyorBeltPiece2Panel.BackColor = Color.FromArgb(0, 255, 0);
                });
            });
           // await Task.Delay(50);
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    StartPart2ConveyorBeltPiece3PictureBox.Image = Properties.Resources.green_arrow_down;
                });
            });
        }

        public async Task ConveyorBeltPathOff()
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    StartPart2ConveyorBeltPiece1PictureBox.Image = Properties.Resources.black_arrow_up;
                    StartPart2ConveyorBeltPiece3PictureBox.Image = Properties.Resources.black_arrow_down;
                    StartPart2ConveyorBeltPiece2Panel.BackColor = Color.Black;
                });
            });
        }

        public async Task StartConveyorBeltGifAnimation()
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    ConveyorBeltPictureBox.Enabled = true;
                });
            });
        }

        public async Task StopConveyorBeltGifAnimation()
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    ConveyorBeltPictureBox.Enabled = false;
                });
            });
        }

        #endregion

        #region On Off

        private void ChangeOnOffIcon(Bitmap icon)
        {
            OnOffPictureBox.Image = icon;
        }

        public async Task BlinkOnOffIcon(CancellationToken token)
        {
            while (true)
            {
                await Task.Run(() => {
                    this.Invoke((MethodInvoker)delegate {
                        ChangeOnOffIcon(Properties.Resources.turn_off_icon);
                    });
                });
                await Task.Delay(1000);
                if (token.IsCancellationRequested)
                {
                    token.ThrowIfCancellationRequested();
                }
                await Task.Run(() => {
                    this.Invoke((MethodInvoker)delegate {
                        ChangeOnOffIcon(Properties.Resources.turn_on_icon);
                    });
                });
                await Task.Delay(1000);
                if (token.IsCancellationRequested)
                {
                    token.ThrowIfCancellationRequested();
                }
            }
        }

        public async Task ApplicationOn()
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    ChangeOnOffIcon(Properties.Resources.turn_on_icon);
                    OnPiece1Panel.BackColor = Color.FromArgb(0, 255, 0); //Green linke the arrow
                    OnPiece2PictureBox.Image = Properties.Resources.green_arrow_down;
                    OffPiece1Panel.BackColor = Color.Black;
                    OffPiece2Panel.BackColor = Color.Black;
                    OffPiece3PictureBox.Image = Properties.Resources.black_arrow_left;
                    //StartPanel.BackColor = Color.FromArgb(0, 255, 0);
                    //OnPiece2PictureBox
                });
                //return;
            });

        }

        public async Task ApplicationOff()
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    ChangeOnOffIcon(Properties.Resources.turn_off_icon);
                    OnPiece1Panel.BackColor = Color.Black;
                    OnPiece2PictureBox.BackColor = Color.Black;
                    OffPiece1Panel.BackColor = Color.Red;
                    OffPiece2Panel.BackColor = Color.Red;
                    OffPiece3PictureBox.BackColor = Color.Red;
                    StartPanel.BackColor = Color.FromArgb(0, 0, 0);
                });
                return;
            });

        }

        public async Task StartPanelClear()
        {
            await Task.Run(() =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    StartPanel.BackColor = Color.FromArgb(0, 0, 0);
                });
            });
        }

        public async Task StartPanelEditing()
        {            
            await Task.Run(() =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    StartPanel.BackColor = Color.FromArgb(8, 52, 204);
                });
            });
        }

        public async Task StartPanelOn()
        {
            await Task.Run(() =>
            {
                this.Invoke((MethodInvoker)delegate
                {
                    StartPanel.BackColor = Color.FromArgb(0, 255, 0);
                });
            });
        }


        #endregion

        #region Editing Button

        private Button _currentButton;
        public Button CurrentButton { get { return _currentButton; } }
        private string buttonEnterName = "";

        public async Task EnabelingCurrentEditingButton()
        {
            await ElementsUtil.ArmSettings.DisableOrEnableServoPanels(true);
            await ElementsUtil.ArmSettings.StarteditingRobotButton(false);
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    _currentButton.Enabled = true;
                });
            });
        }

        private async Task WhiteDataToArduino(char pathId, int stageId, Button button) 
        {
            if (_isEdtingStateEnabled)
            {
                if (ElementsUtil.StateMachine != null && ElementsUtil.RobotArmStateMachine != null)
                {
                    if (ElementsUtil.StateMachine.CurrentState is EditingArmSettingsState && ElementsUtil.RobotArmStateMachine.CurrentState is RobotEditingState)
                    {
                        

                        if (!_currentbuttoneditingMovement)
                        {
                            // foreach (var item in _buttonStates)
                            //{
                            //    if(item != _currentButton)
                            //    {
                            //        item.Enabled = false;
                            //    }

                            //}
                            await Task.Run(() => {
                                this.Invoke((MethodInvoker)delegate {
                                    _currentButton = button;
                                    buttonEnterName = button.Text;
                                });
                            });

                            await Task.Run(() => {
                                this.Invoke((MethodInvoker)delegate {
                                    button.BackColor = Color.FromArgb(8, 52, 204);
                                    button.Text = "Exit Editing";
                                });
                            });
                            
                            ElementsUtil.ArmSettings.PathId = pathId;
                            ElementsUtil.ArmSettings.StageID = stageId;
                            
                            foreach (var item in _buttonStates)
                            {
                               
                               item.Enabled = false;
                                

                            }
                            //start movement (enter editing mode)
                           
                           
                          
                            _currentbuttoneditingMovement = true;
                            //e#a#p#(path id)#s#(state number 1-2)
                            byte[] data = await SendBaseDataProtocol.ConvertStringToByteArray("e#a#p#" + pathId.ToString() + "#s#"+ stageId.ToString());
                            await DataProtocol.WriteDataAsync(ElementsUtil.MainSerialPort, data);

                            //e#a#p#(path id)#s#(state number 1-2)
                            //await ElementsUtil.ArmSettings.DisableOrEnableServoPanels(true);
                            //await ElementsUtil.ArmSettings.StarteditingRobotButton(false);
                        }
                        else
                        {

                            //stop editing (exiting editing mode) 
                            await Task.Run(() => {
                                this.Invoke((MethodInvoker)delegate {
                                    foreach (var item in _buttonStates)
                                    {
                                        item.Enabled = true;
                                    }
                                    button.Text = buttonEnterName;
                                    button.BackColor = Color.White;
                                });
                            });
                          
                            _currentbuttoneditingMovement = false;
                            await ElementsUtil.ArmSettings.DisableOrEnableServoPanels(false);
                            await ElementsUtil.ArmSettings.StarteditingRobotButton(true);
                        }
                    }
                }
            }
            
        }

        #region pick up
        private async void StartStage1Button_Click(object sender, EventArgs e)
        {
            await WhiteDataToArduino('u', 1, StartStage1Button);
        }

        private async void StartStage2Button_Click(object sender, EventArgs e)
        {
            await WhiteDataToArduino('u', 2, StartStage2Button);
        }
        #endregion

        #region black and yellow box
        private async void Path1Stage1Button_Click(object sender, EventArgs e)
        {
            await WhiteDataToArduino('y', 1, Path1Stage1Button);
        }

        private async void Path1Stage2Button_Click(object sender, EventArgs e)
        {
            await WhiteDataToArduino('y', 2, Path1Stage2Button);
        }
        #endregion

        #region magnetic and blue box
        private async void Path2Stage1Button_Click(object sender, EventArgs e)
        {
            await WhiteDataToArduino('m', 1, Path2Stage1Button);
        }

        private async void Path2Stage2Button_Click(object sender, EventArgs e)
        {
            await WhiteDataToArduino('m', 2, Path2Stage2Button);
        }

        #endregion

        #region white box
        private async void Path3Stage1Button_Click(object sender, EventArgs e)
        {
            await WhiteDataToArduino('w', 1, Path3Stage1Button);
        }

        private async void Path3Stage2Button_Click(object sender, EventArgs e)
        {
            await WhiteDataToArduino('w', 2, Path3Stage2Button);
        }
        #endregion

        #endregion

      
    }
}
