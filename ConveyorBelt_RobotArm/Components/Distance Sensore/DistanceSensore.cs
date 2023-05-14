using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConveyorBelt_RobotArm
{
    public partial class DistanceSensore : UserControl
    {
        //TODO: fix timers animations and create funtion for future code
        //distance betwin the panels is 20
        private int _maxDistance;
        private int _minDistance;
        private int _currentMaxDistance;
        private int _currentMinDistance;
        private int _newDistance;
        private int _lastDistance;
        private int _currentMaxOffsetValue;
        private int _currentMinOffsetValue;
        private int _lastIndicatorPosition;
        private int _sensoreNumber;
        private string _sensorName;
        private int _currentIndicatorPosition;
        private float _indicatorPositionThresholdPercent;
        private int _indicatorStartPosition;
        private int _indicatorEndPosition;
        private int _correctDistance;

        public int CurrentMaxOffsetValue { get { return _currentMaxOffsetValue; } set { } }
        public int CurrentMinOffsetValue { get { return _currentMinOffsetValue; } set { } }
        public int CurrentMaxDistance { get { return _currentMaxDistance; } set { } }
        public int CurrentMinDistance { get { return _currentMinDistance; } set { } }
        public int MinDistance { get { return _minDistance; } set { } }
        public int MaxDistance { get { return _maxDistance; } set { } }
        public int SensoreNumber { get { return _sensoreNumber; } set { _sensoreNumber = value; } }
        public string SensorName { get { return _sensorName; } set { _sensorName = value; } }
        public SerialPort serialPort { get; set; }
        public int NewDistance { get { return _newDistance; } set { _newDistance = value; } }
        public int LastDistance { get { return _lastDistance; } set { _lastDistance = value; } }
        public float IndicatorPositionThresholdPercent { get { return _indicatorPositionThresholdPercent; } set { _indicatorPositionThresholdPercent = value; } }
        public int IndicatorStartPosition { get { return _indicatorStartPosition; } set { _indicatorStartPosition = value; } }
        public int IndicatorEndPosition { get { return _indicatorEndPosition; } set { _indicatorEndPosition = value; } }
        public int CurrectDistance { get { return _correctDistance; } set {} }

        private bool _isIndicatorColorChanged = false;

        Timer ImageBoxMoveTimer = new Timer();
        public DistanceSensore()
        {
            InitializeComponent();
            //ImageBoxMoveTimer.Tick += ImageBoxMoveTimer_Tick;
            _maxDistance = int.Parse(MaxDomainUpDown.Text);
            _minDistance = int.Parse(MinDomainUpDown.Text);
            _currentMaxDistance = ConvertRange(MaxOffsetTrackBar.Maximum, MaxOffsetTrackBar.Minimum, _maxDistance, _minDistance, MaxOffsetTrackBar.Value);
            _currentMinDistance = ConvertRange(MinOffsetTrackBar.Maximum, MinOffsetTrackBar.Minimum, _maxDistance, _minDistance, MinOffsetTrackBar.Value);
            _lastIndicatorPosition = 0;
            _currentIndicatorPosition = 0;
            _currentMaxOffsetValue = MaxOffsetTrackBar.Value;
            _currentMinOffsetValue = MinOffsetTrackBar.Value;
            CurrentMaxValueInfoLabel.Text = _currentMaxDistance.ToString();
            CurrentMinValueInfoLabel.Text = _currentMinDistance.ToString();
            _indicatorStartPosition = 0;
            _indicatorEndPosition = 218;
            //ShowDistance(0);
            _newDistance = 0;
            _lastDistance = 0;
            _indicatorPositionThresholdPercent = 1.1f;
        }

        //need to be fixed (the sensores panel at this pint are still not functional using timers)
        private void ImageBoxMoveTimer_Tick(object sender, EventArgs e)
        {
            if ((_currentIndicatorPosition - _lastIndicatorPosition) < 0)
            {
                DistanceIndecatorPictureBox.Location = new Point(0, _currentIndicatorPosition - 1);
            }
            else
            {
                DistanceIndecatorPictureBox.Location = new Point(0, _currentIndicatorPosition + 1);
            }
        }

        public void SetSensoreName()
        {
            SensoreTitleLabel.Text = "Sensore: " + _sensorName + ". (" +_sensoreNumber.ToString() + ")";
        }

        public async Task ShowDistance()
        {
            await ShowDistance(_newDistance);
        }

        public async Task ShowDistance(int distance)
        {
            await Task.Run(() => {
                if (distance > _currentMaxDistance)
                {
                    distance = _currentMaxDistance;
                }

                if (distance < _currentMinDistance)
                {
                    //show err
                    distance = _currentMinDistance;
                }

                if (distance <= _currentMaxDistance / 6)
                {
                    if(DistanceInfoLabel.ForeColor == Color.Red)
                    {
                        _isIndicatorColorChanged = false;
                    }
                    else
                    {
                        _isIndicatorColorChanged = true;
                    }
                    DistanceInfoLabel.ForeColor = Color.Red;
                   
                }

                if (distance > _currentMaxDistance / 6 && distance <= _currentMaxDistance / 2)
                {
                    if (DistanceInfoLabel.ForeColor == Color.DarkGoldenrod)
                    {
                        _isIndicatorColorChanged = false;
                    }
                    else
                    {
                        _isIndicatorColorChanged = true;
                    }

                    DistanceInfoLabel.ForeColor = Color.DarkGoldenrod;
                }

                if (distance > _currentMaxDistance / 2)
                {
                    if (DistanceInfoLabel.ForeColor == Color.Green)
                    {
                        _isIndicatorColorChanged = false;
                    }
                    else
                    {
                        _isIndicatorColorChanged = true;
                    }

                    DistanceInfoLabel.ForeColor = Color.Green;
                }

                _lastDistance = distance;
                _correctDistance = distance;
            });
            
            
            this.Invoke((MethodInvoker)delegate {
                //UpdateSliderPosition(distance);

                _currentIndicatorPosition = ConvertRange(_currentMaxDistance, _currentMinDistance, _indicatorStartPosition, _indicatorEndPosition, distance);
                DistanceInfoLabel.Text = distance.ToString();
                if (_isValuePassingThresholdPercentage(_currentIndicatorPosition))
                {
                    DistanceIndecatorPictureBox.Location = new Point(0, _currentIndicatorPosition);
                    _lastIndicatorPosition = _currentIndicatorPosition;
                }
            });

        }

        private bool _isValuePassingThresholdPercentage(int newIndicatorPosition)
        {
            if(newIndicatorPosition == _lastIndicatorPosition)
            {
                return false;
            }

            if(_isIndicatorColorChanged)
            {
                return true;
            }

            /// <summary>
            /// taking the max distance that the indicator can move in.
            /// And then get the perstage put of it with the threshold value example threshold: 2% out of 218 (distance) -> 4.  
            /// </summary>
            int newPercentagePosition = (int)((_indicatorEndPosition*_indicatorPositionThresholdPercent/100));
            //int currentPositionMinuxPercentagePosition = _lastIndicatorPosition - newPercentagePosition;
            //int maxPercentagePosition = currentPositionMinuxPercentagePosition + _lastIndicatorPosition;
            //int minPercentagePosition = _lastIndicatorPosition - currentPositionMinuxPercentagePosition;
            int maxPercentagePosition = newPercentagePosition + _lastIndicatorPosition;
            int minPercentagePosition = _lastIndicatorPosition - newPercentagePosition;
            if (newIndicatorPosition < minPercentagePosition || newIndicatorPosition > maxPercentagePosition)
            {
                return true;
            }

            return false;
        }

        public bool IsDistanceChanged(int distance)
        {
            if(distance == _lastDistance)
            {
                return false;
            }

            return true;
        }

        private void UpdateSliderPosition(int distance)
        {
            DistanceInfoLabel.Text = distance.ToString();
            _currentIndicatorPosition = ConvertRange(_currentMaxDistance, _currentMinDistance, 0, 180, distance);
            if (_currentIndicatorPosition == _lastIndicatorPosition)
            {

            }
            else
            {
                int deltaImagePosition = _currentIndicatorPosition - _lastIndicatorPosition;
                if (deltaImagePosition < 0)
                {
                    deltaImagePosition *= -1;
                }
                ImageBoxMoveTimer.Interval = deltaImagePosition;
                ImageBoxMoveTimer.Start();
            }

            _lastIndicatorPosition = _currentIndicatorPosition;
        }

        private int ConvertRange(int originalStart, int originalEnd, int newStart, int newEnd, int value)
        {
            double scale = (double)(newEnd - newStart) / (originalEnd - originalStart);
            return (int)(newStart + ((value - originalStart) * scale));
        }

        private bool CheckMinMaxValues(int newMax, int newMin)
        {
            if (_maxDistance <= newMin)
            {
                //TODO: Show err for alitel bit of time
                ChangeBackMinMaxTextBox();
                //MaxDomainUpDown.Text = _maxDistance.ToString();
                return false;
            }

            if (newMax == 0 || newMax <= _minDistance)
            {
                ChangeBackMinMaxTextBox();
                return false;
            }

            return true;
        }

        private void ChangeBackMinMaxTextBox()
        {
            MaxDomainUpDown.Text = _maxDistance.ToString();
            MinDomainUpDown.Text = _minDistance.ToString();
        }

        private void ValueChanged(DomainUpDown domainUpDown)
        {
           
            if (domainUpDown.Text == null || domainUpDown.Text == "")
            {
                ChangeBackMinMaxTextBox();
                //show error
                return;
            }

            try
            {
                if (CheckMinMaxValues(int.Parse(MaxDomainUpDown.Text), int.Parse(MinDomainUpDown.Text)))
                {

                    UpdatePrivateMinMaxValues(int.Parse(MaxDomainUpDown.Text), int.Parse(MinDomainUpDown.Text));
                    UpdateMinMaxOffsetLabels();
                    return;
                }
            }
            catch
            {
            }
            finally
            {
                ChangeBackMinMaxTextBox();
            }
        }

        private void UpdateMinMaxOffsetLabels()
        {
            MaxSensitivityLabel1.Text = _maxDistance.ToString();
            MaxSensitivityLabel2.Text = _maxDistance.ToString();
            MinSensitivityLabel1.Text = _minDistance.ToString();
            MinSensitivityLabel2.Text = _minDistance.ToString();
        }

        private void UpdatePrivateMinMaxValues(int newMax, int newMin)
        {
            _maxDistance = newMax;
            _minDistance = newMin;
            _currentMaxDistance = ConvertRange(MaxOffsetTrackBar.Maximum, MaxOffsetTrackBar.Minimum, _maxDistance, _minDistance, MaxOffsetTrackBar.Value);
            _currentMinDistance = ConvertRange(MinOffsetTrackBar.Maximum, MinOffsetTrackBar.Minimum, _maxDistance, _minDistance, MinOffsetTrackBar.Value);
            CurrentMaxValueInfoLabel.Text = _currentMaxDistance.ToString();
            CurrentMinValueInfoLabel.Text = _currentMinDistance.ToString();
        }

        private void MinDomainUpDown_SelectedItemChanged(object sender, EventArgs e)
        {
            ValueChanged(MinDomainUpDown);
        }

        private void MaxDomainUpDown_SelectedItemChanged(object sender, EventArgs e)
        {
            ValueChanged(MaxDomainUpDown);
        }

        private void MinOffsetTrackBar_MouseUp(object sender, MouseEventArgs e)
        {
            if (MinOffsetTrackBar.Value >= _currentMaxOffsetValue)
            {
                //show err
                MessageBox.Show("Current Min Value: " + _currentMinOffsetValue);
                MinOffsetTrackBar.Value = _currentMinOffsetValue;
                return;
            }
            else
            {
                _currentMinOffsetValue = MinOffsetTrackBar.Value;
                _currentMinDistance = ConvertRange(MinOffsetTrackBar.Maximum, MinOffsetTrackBar.Minimum, _maxDistance, _minDistance, MinOffsetTrackBar.Value);
                CurrentMinValueInfoLabel.Text = _currentMinDistance.ToString();
            }
        }

        private void MaxOffsetTrackBar_MouseUp(object sender, MouseEventArgs e)
        {
            if (MaxOffsetTrackBar.Value <= _currentMinOffsetValue)
            {
                //show err
                MessageBox.Show("current max value: " + MaxOffsetTrackBar.Value);
                MaxOffsetTrackBar.Value = _currentMaxOffsetValue;
                return;
            }
            else
            {
                _currentMaxOffsetValue = MaxOffsetTrackBar.Value;
                _currentMaxDistance = ConvertRange(MaxOffsetTrackBar.Maximum, MaxOffsetTrackBar.Minimum, _maxDistance, _minDistance, MaxOffsetTrackBar.Value);
                CurrentMaxValueInfoLabel.Text = _currentMaxDistance.ToString();
            }
        }
    }
}

