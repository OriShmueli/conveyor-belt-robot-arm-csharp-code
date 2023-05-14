using ConveyorBelt_RobotArm.Components.Program_States;
using ConveyorBelt_RobotArm.Properties;
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
    public partial class ProgramStates : UserControl
    {
        List<GraphicStateComponent> _graphicStateComponents = new List<GraphicStateComponent>();
        
        private List<Image> _arrowImages = new List<Image>();
        private List<Image> _statesPath = new List<Image>();
        private List<Panel> _statesPanels = new List<Panel>();

        private GraphicStateComponent _currentComponent;
        private List<GraphicStateComponent> _currentActivatedComponents = new List<GraphicStateComponent>();

        private Panel _mainPath;

        private GraphicStateComponent _currentError = null;

        public ProgramStates()
        {
            InitializeComponent();
            // GraphicStateComponents.Add(new GraphicStateComponent())
            _mainPath = MiddleChangableColoredPanel;
           // _mainPath.BackgroundImage = Resources.gray_transparent;
           _mainPath.Visible = false;
            //compute states
            GraphicStateUtil.ArmUIWorkFlow = new GraphicStateComponent(ComputerArrowArmUIWorkFlowPictureBox, ComputerStateArmUIWorkFlowPanel, ComputerColoredArmUIWorkFlowPanel, 0, 20, ComputerStateArmUIWorkFlowLable);
            GraphicStateUtil.UpdatePackagesAmounts = new GraphicStateComponent(ComputerArrowUpdatePackagesAmountsPictureBox, ComputerStateUpdatePackagesAmountsPanel, ComputerColoredUpdatePackagesAmountsPanel, 56, 76, ComputerStateUpdatePackagesAmountsLabel);
            GraphicStateUtil.ActivatingDeactivating = new GraphicStateComponent(ComputerArrowActivatingDeactivatingPictureBox, ComputerStateActivatingDeactivatingPanel, ComputerColoredActivatingDeactivatingPanel, 112, 112+20, ComputerStateActivatingDeactivatingLabel);
            GraphicStateUtil.SendingErrorResponse = new GraphicStateComponentError(ComputerArrowSendingErrorResponsePictureBox, ComputerStateSendingErrorResponsePanel, ComputerColoredSendingErrorResponsePanel, 168, 168 + 20, ComputerStateSendingErrorResponseLabel);
            GraphicStateUtil.WritingDataToArduino = new GraphicStateComponent(ComputerArrowWritingDataToArduinoPictureBox, ComputerStateWritingDataToArduinoPanel, ComputerColoredWritingDataToArduinoPanel, 224, 224 + 20, ComputerStateWritingDataToArduinoLabel);

            //arduino states
            GraphicStateUtil.OnOff = new GraphicStateComponentOnOff(ArduinoArrowOnOffPictureBox, ArduinoStateOnOffPanel, ArduinoColoredOnOffPanel, 0, 20, ArduinoStateOnOffLabel);
            GraphicStateUtil.FirstTimeOnOff = new GraphicStateComponentFirstTimeOnOff(ArduinoArrowFirstTimeOnOffPictureBox, ArduinoStateFirstTimeOnOffPanel, ArduinoColoredFirstTimeOnOffPanel, 56, 76, ArduinoStateFirstTimeOnOffLabel);
            GraphicStateUtil.ArmMovement = new GraphicStateComponent(ArduinoArrowArmMovementPictureBox, ArduinoStateArmMovementPanel, ArduinoColoredArmMovementPanel, 112, 112 + 20, ArduinoStateArmMovementLabel);
            GraphicStateUtil.ConveyerBelt = new GraphicStateComponent(ArduinoArrowConveyerBeltPictureBox, ArduinoStateConveyerBeltPanel, ArduinoColoredConveyerBeltPanel, 168, 168 + 20, ArduinoStateConveyerBeltLabel);
            GraphicStateUtil.Sensores = new GraphicStateComponent(ArduinoArrowSensoresPictureBox, ArduinoStateSensoresPanel, ArduinoColoredSensoresPanel, 224, 224 + 20, ArduinoStateSensoresLabel);

            //editing state
            GraphicStateUtil.ModifyingArmMovement = new GraphicStateComponentEditing(ComputerArrowModifyingArmMovementPictureBox, ComputerStateModifyingArmMovementPanel, _mainPath, 224+43, 224+43, ComputerStateModifyingArmMovementLabel);
        }

        public async Task EnterState(GraphicStateComponent graphicStateComponent, bool stayActive = false)
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {

                    if (stayActive)
                    {
                        
                        if(stayActive == true)
                        {
                            _currentComponent.AddedToActivatedList(false);
                        }
                        else
                        {
                            _currentComponent.AddedToActivatedList(true);
                        }
                            
                        
                        
                    }
                    else
                    {
                        if (_currentComponent != null)
                        {
                            _currentComponent.ExitStateToNewState();
                        }
                    }                  

                    _currentComponent = graphicStateComponent;
                    if (!_currentActivatedComponents.Contains(graphicStateComponent))
                    {
                        _currentActivatedComponents.Add(graphicStateComponent);
                    }
                                        
                    int highestPosition= 1000;
                    int lowestPosition = 0;

                    for (int i = 0; i < _currentActivatedComponents.Count; i++)
                    {
                        if (_currentActivatedComponents[i].HighestPositionY < highestPosition)
                        {
                            highestPosition = _currentActivatedComponents[i].HighestPositionY;
                        }

                        if (_currentActivatedComponents[i].LowerPositionY > lowestPosition)
                        {
                            lowestPosition = _currentActivatedComponents[i].LowerPositionY;
                        }
                    }

                    _mainPath.BackColor = Color.FromArgb(0, 255, 0);
                    _mainPath.BackgroundImage = null;
                    _mainPath.Location = new Point(0, highestPosition);                    
                    _mainPath.Size = new Size(20, lowestPosition - highestPosition);
                    graphicStateComponent.EnterState();
                });
            });

            //await Task.Yield();
        }

        public async Task HideAfterExitState(GraphicStateComponent graphicStateComponent)
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    graphicStateComponent.DeactivateComponentPath();
                });
            });

            _currentActivatedComponents.Remove(graphicStateComponent);
        }

        public async Task AddActivatedComponent(GraphicStateComponent graphicStateComponent, bool off = false)
        {
            
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    if (off)
                    {
                        graphicStateComponent.AddedToActivatedList(true);
                    }
                    else
                    {
                        graphicStateComponent.AddedToActivatedList();
                    }
                    
                    _currentActivatedComponents.Add(graphicStateComponent);
                });
            });
        }
        
        public async Task ActivateStatePath(GraphicStateComponent graphicStateComponent)
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    graphicStateComponent.ActivateStatePath();
                });
            });
        }
       
        public async Task ExitState(GraphicStateComponent graphicStateComponent)
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    if (_currentActivatedComponents.Contains(graphicStateComponent))
                    {
                        _currentActivatedComponents.Remove(graphicStateComponent);
                    }
                    graphicStateComponent.ExitState();
                });
            });
        }

        public async Task ShowError(GraphicStateComponent graphicStateComponent)
        {
            
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                   
                    graphicStateComponent.AddedToActivatedList(true);
                    graphicStateComponent.ActivateStatePath();
                });
            });

            await Task.Delay(200);

            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                   
                        _currentError = graphicStateComponent;
                        graphicStateComponent.DeavtivateStatePanel();
                   
                });
            });
            await Task.Delay(200);
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                   
                        _currentError = graphicStateComponent;
                        graphicStateComponent.AddedToActivatedList(true);
                  
                });
            });

            await Task.Delay(200);

            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    
                        _currentError = graphicStateComponent;
                    //graphicStateComponent.DeavtivateStatePanel();
                    //graphicStateComponent.DeactivateStatePath();
                    if (_currentActivatedComponents.Contains(graphicStateComponent))
                    {
                        _currentActivatedComponents.Remove(graphicStateComponent);
                    }
                    graphicStateComponent.ExitState();
                    
                });
            });
        }

        public async Task FirstTimeOnChangeStateName()
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    
                    _mainPath.Visible = true;
                    if (_currentActivatedComponents.Contains(GraphicStateUtil.FirstTimeOnOff))
                    {
                        _currentActivatedComponents.Remove(GraphicStateUtil.FirstTimeOnOff);
                    }
                    GraphicStateUtil.FirstTimeOnOff.ChangeName("First Time On");
                    _currentActivatedComponents.Add(GraphicStateUtil.FirstTimeOnOff);
                });
            });
           
            await AddActivatedComponent(GraphicStateUtil.FirstTimeOnOff);
        }

        public async Task FirstTimeOffChangeStateName()
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    _mainPath.Visible = true;
                    
                    if (_currentActivatedComponents.Contains(GraphicStateUtil.FirstTimeOnOff))
                    {
                        _currentActivatedComponents.Remove(GraphicStateUtil.FirstTimeOnOff);
                    }
                    // GraphicStateUtil.FirstTimeOnOff.FirstTimeOff();
                    GraphicStateUtil.FirstTimeOnOff.ChangeName("First Time Off");
                    _currentActivatedComponents.Add(GraphicStateUtil.FirstTimeOnOff);
                });
            });
            await AddActivatedComponent(GraphicStateUtil.FirstTimeOnOff, true);
        }

        public async Task OnChangeStateName()
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    //GraphicStateUtil.OnOff.ChangeName("On");

                    if (_currentActivatedComponents.Contains(GraphicStateUtil.OnOff))
                    {
                        _currentActivatedComponents.Remove(GraphicStateUtil.OnOff);
                    }

                    GraphicStateUtil.OnOff.ChangeName("On");
                    _currentActivatedComponents.Add(GraphicStateUtil.OnOff);
                });
            });

            
            await AddActivatedComponent(GraphicStateUtil.OnOff);
        }

        public async Task OffChangeStateName()
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    //GraphicStateUtil.OnOff.ChangeName("Off");


                    if (_currentActivatedComponents.Contains(GraphicStateUtil.OnOff))
                    {
                        _currentActivatedComponents.Remove(GraphicStateUtil.OnOff);
                    }

                    GraphicStateUtil.OnOff.ChangeName("Off");
                    _currentActivatedComponents.Add(GraphicStateUtil.OnOff);
                });
            });

            
            await AddActivatedComponent(GraphicStateUtil.OnOff, true);
        }

        public async Task ActivatingChangeStateName()
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    //if (_currentActivatedComponents.Contains(GraphicStateUtil.ActivatingDeactivating))
                    //{
                    //    _currentActivatedComponents.Remove(GraphicStateUtil.ActivatingDeactivating);
                    //}

                    GraphicStateUtil.ActivatingDeactivating.ChangeName("Activating");
                });
            });
            //await AddActivatedComponent(GraphicStateUtil.ActivatingDeactivating);
        }

        public async Task DeactivatingChangeStateName()
        {
            await Task.Run(() => {
                this.Invoke((MethodInvoker)delegate {
                    if (_currentActivatedComponents.Contains(GraphicStateUtil.ActivatingDeactivating))
                    {
                        _currentActivatedComponents.Remove(GraphicStateUtil.ActivatingDeactivating);
                    }

                    GraphicStateUtil.ActivatingDeactivating.ChangeName("Deactivating");
                    GraphicStateUtil.ActivatingDeactivating.StatePanel.BackColor = Color.FromArgb(255, 0, 0);
                    _currentActivatedComponents.Add(GraphicStateUtil.ActivatingDeactivating);
                });
            });

            //await AddActivatedComponent(GraphicStateUtil.ActivatingDeactivating, true);
        }

       
    }
}
