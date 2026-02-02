namespace CustomPages
{
    partial class MotionRemoteControl
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.xtraTabControlPLCControl = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabConnectPage = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControl3 = new DevExpress.XtraLayout.LayoutControl();
            this.textEditTcpPort = new DevExpress.XtraEditors.TextEdit();
            this.PLCDisConnect = new DevExpress.XtraEditors.SimpleButton();
            this.ConnectPLCButton = new DevExpress.XtraEditors.SimpleButton();
            this.textEditIpAddress = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.textEditSendPLCData = new DevExpress.XtraEditors.TextEdit();
            this.memoEditMessageLog = new DevExpress.XtraEditors.MemoEdit();
            this.PLCSendDataButton = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.textEdit = new DevExpress.XtraLayout.LayoutControlItem();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.xtraTabControlPage = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl2 = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControl5 = new DevExpress.XtraLayout.LayoutControl();
            this.groupControlPCLControl = new DevExpress.XtraEditors.GroupControl();
            this.layoutControl6 = new DevExpress.XtraLayout.LayoutControl();
            this.radioGroupCalibration = new DevExpress.XtraEditors.RadioGroup();
            this.checkEditCalibration = new DevExpress.XtraEditors.CheckEdit();
            this.textEditTargetAcceleration = new DevExpress.XtraEditors.TextEdit();
            this.textEditTargetVelocity = new DevExpress.XtraEditors.TextEdit();
            this.textEditTargetPosFR = new DevExpress.XtraEditors.TextEdit();
            this.textEditTargetPosFZ = new DevExpress.XtraEditors.TextEdit();
            this.textEditTargetPosZ = new DevExpress.XtraEditors.TextEdit();
            this.textEditTargetPosY2 = new DevExpress.XtraEditors.TextEdit();
            this.textEditTargetPosY1 = new DevExpress.XtraEditors.TextEdit();
            this.textEditTargetPosX = new DevExpress.XtraEditors.TextEdit();
            this.SendCmdHommingButton = new DevExpress.XtraEditors.SimpleButton();
            this.SendCommandMoveStopButton = new DevExpress.XtraEditors.SimpleButton();
            this.ErrorResetButton = new DevExpress.XtraEditors.SimpleButton();
            this.EmergencyStopButton = new DevExpress.XtraEditors.SimpleButton();
            this.RobotEnableButton = new DevExpress.XtraEditors.SimpleButton();
            this.SendCmdPositionButton = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControl8 = new DevExpress.XtraLayout.LayoutControl();
            this.textEditUserDefineValue = new DevExpress.XtraEditors.TextEdit();
            this.CheckButtonFRPlusControlCommand = new DevExpress.XtraEditors.CheckButton();
            this.CheckButtonFRStopControlCommand = new DevExpress.XtraEditors.CheckButton();
            this.CheckButtonFRMinusControlCommand = new DevExpress.XtraEditors.CheckButton();
            this.CheckButtonFZPlusControlCommand = new DevExpress.XtraEditors.CheckButton();
            this.CheckButtonFZStopControlCommand = new DevExpress.XtraEditors.CheckButton();
            this.CheckButtonFZMinusControlCommand = new DevExpress.XtraEditors.CheckButton();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.CheckButtonZPlusControlCommand = new DevExpress.XtraEditors.CheckButton();
            this.CheckButtonZStopControlCommand = new DevExpress.XtraEditors.CheckButton();
            this.CheckButtonZMinusControlCommand = new DevExpress.XtraEditors.CheckButton();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.CheckButtonY2PlusControlCommand = new DevExpress.XtraEditors.CheckButton();
            this.CheckButtonY2StopControlCommand = new DevExpress.XtraEditors.CheckButton();
            this.CheckButtonY2MinusControlCommand = new DevExpress.XtraEditors.CheckButton();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.CheckButtonY1PlusControlCommand = new DevExpress.XtraEditors.CheckButton();
            this.CheckButtonY1StopControlCommand = new DevExpress.XtraEditors.CheckButton();
            this.CheckButtonY1MinusControlCommand = new DevExpress.XtraEditors.CheckButton();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.CheckButtonXPlusControlCommand = new DevExpress.XtraEditors.CheckButton();
            this.CheckButtonXStopControlCommand = new DevExpress.XtraEditors.CheckButton();
            this.CheckButtonXMinusControlCommand = new DevExpress.XtraEditors.CheckButton();
            this.checkButtonHighValue = new DevExpress.XtraEditors.CheckButton();
            this.checkButtonMiddleValue = new DevExpress.XtraEditors.CheckButton();
            this.radioGroupMenualValueMode = new DevExpress.XtraEditors.RadioGroup();
            this.checkButtonLowValue = new DevExpress.XtraEditors.CheckButton();
            this.layoutControlGroup9 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem32 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem29 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem30 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem31 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem37 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem40 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem38 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem39 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem41 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem42 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem43 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem44 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem45 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem46 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem47 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem48 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem49 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem50 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem51 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem52 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem53 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem54 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem55 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem56 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem57 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem58 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem59 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem60 = new DevExpress.XtraLayout.LayoutControlItem();
            this.textBoxUserDefineValue = new DevExpress.XtraLayout.LayoutControlItem();
            this.radioGroupMenualControlMode = new DevExpress.XtraEditors.RadioGroup();
            this.radioGroupMenualMode = new DevExpress.XtraEditors.RadioGroup();
            this.checkButtonMenualMode = new DevExpress.XtraEditors.CheckButton();
            this.layoutControlGroup7 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem27 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem26 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem28 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem35 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem61 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem153 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem154 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem155 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem33 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem19 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem20 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem118 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem21 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem22 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem23 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem24 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem34 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem36 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem156 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup5 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem18 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControl4 = new DevExpress.XtraLayout.LayoutControl();
            this.groupControlPresentPosition = new DevExpress.XtraEditors.GroupControl();
            this.layoutControl7 = new DevExpress.XtraLayout.LayoutControl();
            this.textEditPresentPosFR = new DevExpress.XtraEditors.TextEdit();
            this.textEditPresentPosFZ = new DevExpress.XtraEditors.TextEdit();
            this.textEditPresentPosZ = new DevExpress.XtraEditors.TextEdit();
            this.textEditPresentPosY2 = new DevExpress.XtraEditors.TextEdit();
            this.textEditPresentPosY1 = new DevExpress.XtraEditors.TextEdit();
            this.textEditPresentPosX = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup8 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem15 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem16 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem17 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup4 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem25 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.xtraTabStatusPage = new DevExpress.XtraTab.XtraTabPage();
            this.layoutControl9 = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControl10 = new DevExpress.XtraLayout.LayoutControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.layoutControl14 = new DevExpress.XtraLayout.LayoutControl();
            this.layoutControl15 = new DevExpress.XtraLayout.LayoutControl();
            this.groupControl6 = new DevExpress.XtraEditors.GroupControl();
            this.layoutControl17 = new DevExpress.XtraLayout.LayoutControl();
            this.labelControlDIn16 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlDIn15 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlDIn14 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlDIn12 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlDIn11 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlDIn10 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlDIn8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlDIn7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlDIn6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlDIn13 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlDIn9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlDIn5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlDIn4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlDIn3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlDIn2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlDIn1 = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlGroup18 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem119 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem137 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem138 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem139 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem140 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem141 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem142 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem143 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem144 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem145 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem146 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem147 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem148 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem149 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem150 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem151 = new DevExpress.XtraLayout.LayoutControlItem();
            this.groupControl5 = new DevExpress.XtraEditors.GroupControl();
            this.layoutControl16 = new DevExpress.XtraLayout.LayoutControl();
            this.labelControlDOut16 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlDOut15 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlDOut14 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlDOut12 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlDOut11 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlDOut10 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlDOut8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlDOut7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlDOut6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlDOut13 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlDOut9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlDOut5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlDOut4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlDOut3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlDOut2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlDOut1 = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlGroup17 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem120 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem121 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem122 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem123 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem130 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem131 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem132 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem133 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem124 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem127 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem128 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem129 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem125 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem134 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem135 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem136 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup16 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem126 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem152 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup15 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem117 = new DevExpress.XtraLayout.LayoutControlItem();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.layoutControl11 = new DevExpress.XtraLayout.LayoutControl();
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.layoutControl13 = new DevExpress.XtraLayout.LayoutControl();
            this.labelControlPLCStatus32 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlPLCStatus31 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlPLCStatus30 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlPLCStatus28 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlPLCStatus27 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlPLCStatus26 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlPLCStatus24 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlPLCStatus23 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlPLCStatus22 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlPLCStatus20 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlPLCStatus19 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlPLCStatus18 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlPLCStatus16 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlPLCStatus15 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlPLCStatus14 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlPLCStatus12 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlPLCStatus11 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlPLCStatus10 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlPLCStatus8 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlPLCStatus7 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlPLCStatus6 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlPLCStatus29 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlPLCStatus25 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlPLCStatus21 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlPLCStatus17 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlPLCStatus13 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlPLCStatus9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlPLCStatus5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlPLCStatus4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlPLCStatus3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlPLCStatus2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlPLCStatus1 = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlGroup14 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem85 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem86 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem87 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem88 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem89 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem91 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem92 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem93 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem94 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem95 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem96 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem97 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem98 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem101 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem90 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem99 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem100 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem102 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem103 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem104 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem105 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem106 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem107 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem108 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem109 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem110 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem111 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem112 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem113 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem114 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem115 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem116 = new DevExpress.XtraLayout.LayoutControlItem();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.layoutControl12 = new DevExpress.XtraLayout.LayoutControl();
            this.labelControlPLimitFR = new DevExpress.XtraEditors.LabelControl();
            this.labelControlHomeFR = new DevExpress.XtraEditors.LabelControl();
            this.labelControlNLimit_FR = new DevExpress.XtraEditors.LabelControl();
            this.labelControlPLimitFZ = new DevExpress.XtraEditors.LabelControl();
            this.labelControlHomeFZ = new DevExpress.XtraEditors.LabelControl();
            this.labelControlNLimit_FZ = new DevExpress.XtraEditors.LabelControl();
            this.labelControlPLimitZ = new DevExpress.XtraEditors.LabelControl();
            this.labelControlHomeZ = new DevExpress.XtraEditors.LabelControl();
            this.labelControlNLimit_Z = new DevExpress.XtraEditors.LabelControl();
            this.labelControlPLimitY2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlHomeY2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlNLimit_Y2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlPLimitY1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlHomeY1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlNLimit_Y1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControlPLimitX = new DevExpress.XtraEditors.LabelControl();
            this.labelControlHomeX = new DevExpress.XtraEditors.LabelControl();
            this.labelControlNLimit_X = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlGroup13 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem67 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem68 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem69 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem70 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem73 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem76 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem71 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem72 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem74 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem75 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem77 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem78 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem79 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem80 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem81 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem82 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem83 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem84 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup12 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem65 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem66 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup11 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem63 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem64 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup10 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem62 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup6 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.maskedTextBox2 = new System.Windows.Forms.MaskedTextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.checkButton51 = new DevExpress.XtraEditors.CheckButton();
            this.checkButton41 = new DevExpress.XtraEditors.CheckButton();
            this.checkButton61 = new DevExpress.XtraEditors.CheckButton();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControlPLCControl)).BeginInit();
            this.xtraTabControlPLCControl.SuspendLayout();
            this.xtraTabConnectPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).BeginInit();
            this.layoutControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditTcpPort.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditIpAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditSendPLCData.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditMessageLog.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.xtraTabControlPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).BeginInit();
            this.layoutControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl5)).BeginInit();
            this.layoutControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlPCLControl)).BeginInit();
            this.groupControlPCLControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl6)).BeginInit();
            this.layoutControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupCalibration.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditCalibration.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditTargetAcceleration.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditTargetVelocity.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditTargetPosFR.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditTargetPosFZ.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditTargetPosZ.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditTargetPosY2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditTargetPosY1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditTargetPosX.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl8)).BeginInit();
            this.layoutControl8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditUserDefineValue.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupMenualValueMode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem32)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem29)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem30)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem31)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem37)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem40)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem38)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem39)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem41)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem42)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem43)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem44)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem45)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem46)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem47)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem48)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem49)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem50)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem51)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem52)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem53)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem54)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem55)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem56)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem57)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem58)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem59)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem60)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxUserDefineValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupMenualControlMode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupMenualMode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem27)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem26)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem28)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem35)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem61)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem153)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem154)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem155)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem33)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem118)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem22)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem24)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem34)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem36)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem156)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl4)).BeginInit();
            this.layoutControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlPresentPosition)).BeginInit();
            this.groupControlPresentPosition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl7)).BeginInit();
            this.layoutControl7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textEditPresentPosFR.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditPresentPosFZ.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditPresentPosZ.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditPresentPosY2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditPresentPosY1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditPresentPosX.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem25)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            this.xtraTabStatusPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl9)).BeginInit();
            this.layoutControl9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl10)).BeginInit();
            this.layoutControl10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl14)).BeginInit();
            this.layoutControl14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl15)).BeginInit();
            this.layoutControl15.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl6)).BeginInit();
            this.groupControl6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl17)).BeginInit();
            this.layoutControl17.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem119)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem137)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem138)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem139)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem140)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem141)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem142)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem143)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem144)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem145)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem146)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem147)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem148)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem149)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem150)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem151)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).BeginInit();
            this.groupControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl16)).BeginInit();
            this.layoutControl16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem120)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem121)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem122)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem123)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem130)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem131)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem132)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem133)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem124)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem127)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem128)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem129)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem125)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem134)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem135)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem136)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem126)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem152)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem117)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl11)).BeginInit();
            this.layoutControl11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.groupControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl13)).BeginInit();
            this.layoutControl13.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem85)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem86)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem87)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem88)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem89)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem91)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem92)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem93)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem94)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem95)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem96)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem97)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem98)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem101)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem90)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem99)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem100)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem102)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem103)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem104)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem105)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem106)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem107)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem108)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem109)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem110)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem111)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem112)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem113)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem114)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem115)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem116)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl12)).BeginInit();
            this.layoutControl12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem67)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem68)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem69)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem70)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem73)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem76)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem71)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem72)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem74)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem75)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem77)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem78)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem79)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem80)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem81)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem82)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem83)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem84)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem65)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem66)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem63)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem64)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem62)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).BeginInit();
            this.SuspendLayout();
            // 
            // xtraTabControlPLCControl
            // 
            this.xtraTabControlPLCControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xtraTabControlPLCControl.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Left;
            this.xtraTabControlPLCControl.Location = new System.Drawing.Point(0, -4);
            this.xtraTabControlPLCControl.Margin = new System.Windows.Forms.Padding(2);
            this.xtraTabControlPLCControl.Name = "xtraTabControlPLCControl";
            this.xtraTabControlPLCControl.SelectedTabPage = this.xtraTabConnectPage;
            this.xtraTabControlPLCControl.Size = new System.Drawing.Size(548, 637);
            this.xtraTabControlPLCControl.TabIndex = 0;
            this.xtraTabControlPLCControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabConnectPage,
            this.xtraTabControlPage,
            this.xtraTabStatusPage});
            // 
            // xtraTabConnectPage
            // 
            this.xtraTabConnectPage.Controls.Add(this.layoutControl1);
            this.xtraTabConnectPage.Name = "xtraTabConnectPage";
            this.xtraTabConnectPage.Size = new System.Drawing.Size(518, 631);
            this.xtraTabConnectPage.Text = "연결";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.layoutControl3);
            this.layoutControl1.Controls.Add(this.dataLayoutControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(518, 631);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // layoutControl3
            // 
            this.layoutControl3.Controls.Add(this.textEditTcpPort);
            this.layoutControl3.Controls.Add(this.PLCDisConnect);
            this.layoutControl3.Controls.Add(this.ConnectPLCButton);
            this.layoutControl3.Controls.Add(this.textEditIpAddress);
            this.layoutControl3.Location = new System.Drawing.Point(0, 0);
            this.layoutControl3.Name = "layoutControl3";
            this.layoutControl3.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl3.Root = this.layoutControlGroup3;
            this.layoutControl3.Size = new System.Drawing.Size(518, 115);
            this.layoutControl3.TabIndex = 6;
            this.layoutControl3.Text = "layoutControl3";
            // 
            // textEditTcpPort
            // 
            this.textEditTcpPort.EditValue = "1000";
            this.textEditTcpPort.Location = new System.Drawing.Point(137, 36);
            this.textEditTcpPort.Name = "textEditTcpPort";
            this.textEditTcpPort.Size = new System.Drawing.Size(369, 20);
            this.textEditTcpPort.StyleController = this.layoutControl3;
            this.textEditTcpPort.TabIndex = 9;
            // 
            // PLCDisConnect
            // 
            this.PLCDisConnect.Location = new System.Drawing.Point(261, 63);
            this.PLCDisConnect.Name = "PLCDisConnect";
            this.PLCDisConnect.Size = new System.Drawing.Size(245, 22);
            this.PLCDisConnect.StyleController = this.layoutControl3;
            this.PLCDisConnect.TabIndex = 8;
            this.PLCDisConnect.Text = "PLC 연결해제";
            this.PLCDisConnect.Click += new System.EventHandler(this.PLCDisConnect_Click);
            // 
            // ConnectPLCButton
            // 
            this.ConnectPLCButton.Location = new System.Drawing.Point(12, 63);
            this.ConnectPLCButton.Name = "ConnectPLCButton";
            this.ConnectPLCButton.Size = new System.Drawing.Size(245, 22);
            this.ConnectPLCButton.StyleController = this.layoutControl3;
            this.ConnectPLCButton.TabIndex = 7;
            this.ConnectPLCButton.Text = "PLC  연결하기";
            this.ConnectPLCButton.Click += new System.EventHandler(this.ConnectPLCButton_Click);
            // 
            // textEditIpAddress
            // 
            this.textEditIpAddress.EditValue = "192.168.0.130";
            this.textEditIpAddress.Location = new System.Drawing.Point(137, 12);
            this.textEditIpAddress.Name = "textEditIpAddress";
            this.textEditIpAddress.Size = new System.Drawing.Size(369, 20);
            this.textEditIpAddress.StyleController = this.layoutControl3;
            this.textEditIpAddress.TabIndex = 5;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup3.GroupBordersVisible = false;
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlItem1});
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Size = new System.Drawing.Size(518, 115);
            this.layoutControlGroup3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.textEditIpAddress;
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(498, 24);
            this.layoutControlItem4.Text = "Ip 주소";
            this.layoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(120, 16);
            this.layoutControlItem4.TextToControlDistance = 5;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.ConnectPLCButton;
            this.layoutControlItem5.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 51);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(249, 44);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.PLCDisConnect;
            this.layoutControlItem6.ControlAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.layoutControlItem6.Location = new System.Drawing.Point(249, 51);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(249, 44);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.textEditTcpPort;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(498, 27);
            this.layoutControlItem1.Text = "Port";
            this.layoutControlItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem1.TextLocation = DevExpress.Utils.Locations.Left;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(120, 23);
            this.layoutControlItem1.TextToControlDistance = 5;
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.Controls.Add(this.textEditSendPLCData);
            this.dataLayoutControl1.Controls.Add(this.memoEditMessageLog);
            this.dataLayoutControl1.Controls.Add(this.PLCSendDataButton);
            this.dataLayoutControl1.Location = new System.Drawing.Point(0, 115);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.OptionsView.UseDefaultDragAndDropRendering = false;
            this.dataLayoutControl1.Root = this.layoutControlGroup2;
            this.dataLayoutControl1.Size = new System.Drawing.Size(518, 631);
            this.dataLayoutControl1.TabIndex = 5;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // textEditSendPLCData
            // 
            this.textEditSendPLCData.Location = new System.Drawing.Point(137, 12);
            this.textEditSendPLCData.Name = "textEditSendPLCData";
            this.textEditSendPLCData.Size = new System.Drawing.Size(246, 20);
            this.textEditSendPLCData.StyleController = this.dataLayoutControl1;
            this.textEditSendPLCData.TabIndex = 7;
            // 
            // memoEditMessageLog
            // 
            this.memoEditMessageLog.Location = new System.Drawing.Point(87, 40);
            this.memoEditMessageLog.Name = "memoEditMessageLog";
            this.memoEditMessageLog.Properties.ReadOnly = true;
            this.memoEditMessageLog.Size = new System.Drawing.Size(419, 579);
            this.memoEditMessageLog.StyleController = this.dataLayoutControl1;
            this.memoEditMessageLog.TabIndex = 6;
            // 
            // PLCSendDataButton
            // 
            this.PLCSendDataButton.Location = new System.Drawing.Point(387, 12);
            this.PLCSendDataButton.Name = "PLCSendDataButton";
            this.PLCSendDataButton.Size = new System.Drawing.Size(119, 22);
            this.PLCSendDataButton.StyleController = this.dataLayoutControl1;
            this.PLCSendDataButton.TabIndex = 5;
            this.PLCSendDataButton.Text = "전송 하기";
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem8,
            this.layoutControlItem9,
            this.textEdit});
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Size = new System.Drawing.Size(518, 631);
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.PLCSendDataButton;
            this.layoutControlItem8.Location = new System.Drawing.Point(375, 0);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(123, 28);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.memoEditMessageLog;
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 28);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(498, 583);
            this.layoutControlItem9.Text = "통신 내역";
            this.layoutControlItem9.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem9.TextSize = new System.Drawing.Size(70, 23);
            this.layoutControlItem9.TextToControlDistance = 5;
            // 
            // textEdit
            // 
            this.textEdit.Control = this.textEditSendPLCData;
            this.textEdit.Location = new System.Drawing.Point(0, 0);
            this.textEdit.MaxSize = new System.Drawing.Size(0, 28);
            this.textEdit.MinSize = new System.Drawing.Size(176, 28);
            this.textEdit.Name = "textEdit";
            this.textEdit.Size = new System.Drawing.Size(375, 28);
            this.textEdit.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.textEdit.Text = "전송 메시지";
            this.textEdit.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.textEdit.TextSize = new System.Drawing.Size(120, 16);
            this.textEdit.TextToControlDistance = 5;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.False;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(518, 631);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.dataLayoutControl1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 115);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.OptionsPrint.AppearanceItemCaption.BorderColor = System.Drawing.Color.Black;
            this.layoutControlItem2.OptionsPrint.AppearanceItemCaption.Options.UseBorderColor = true;
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem2.Size = new System.Drawing.Size(518, 631);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // xtraTabControlPage
            // 
            this.xtraTabControlPage.Controls.Add(this.layoutControl2);
            this.xtraTabControlPage.Name = "xtraTabControlPage";
            this.xtraTabControlPage.Size = new System.Drawing.Size(518, 631);
            this.xtraTabControlPage.Text = "제어";
            // 
            // layoutControl2
            // 
            this.layoutControl2.Controls.Add(this.layoutControl5);
            this.layoutControl2.Controls.Add(this.layoutControl4);
            this.layoutControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl2.Location = new System.Drawing.Point(0, 0);
            this.layoutControl2.Name = "layoutControl2";
            this.layoutControl2.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl2.Root = this.layoutControlGroup1;
            this.layoutControl2.Size = new System.Drawing.Size(518, 631);
            this.layoutControl2.TabIndex = 0;
            this.layoutControl2.Text = "layoutControl2";
            // 
            // layoutControl5
            // 
            this.layoutControl5.Controls.Add(this.groupControlPCLControl);
            this.layoutControl5.Location = new System.Drawing.Point(12, 137);
            this.layoutControl5.Name = "layoutControl5";
            this.layoutControl5.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl5.Root = this.layoutControlGroup5;
            this.layoutControl5.Size = new System.Drawing.Size(494, 482);
            this.layoutControl5.TabIndex = 5;
            this.layoutControl5.Text = "layoutControl5";
            // 
            // groupControlPCLControl
            // 
            this.groupControlPCLControl.Controls.Add(this.layoutControl6);
            this.groupControlPCLControl.Location = new System.Drawing.Point(6, 7);
            this.groupControlPCLControl.Name = "groupControlPCLControl";
            this.groupControlPCLControl.Size = new System.Drawing.Size(482, 468);
            this.groupControlPCLControl.TabIndex = 4;
            this.groupControlPCLControl.Text = "모션 위치 제어";
            // 
            // layoutControl6
            // 
            this.layoutControl6.Controls.Add(this.radioGroupCalibration);
            this.layoutControl6.Controls.Add(this.checkEditCalibration);
            this.layoutControl6.Controls.Add(this.textEditTargetAcceleration);
            this.layoutControl6.Controls.Add(this.textEditTargetVelocity);
            this.layoutControl6.Controls.Add(this.textEditTargetPosFR);
            this.layoutControl6.Controls.Add(this.textEditTargetPosFZ);
            this.layoutControl6.Controls.Add(this.textEditTargetPosZ);
            this.layoutControl6.Controls.Add(this.textEditTargetPosY2);
            this.layoutControl6.Controls.Add(this.textEditTargetPosY1);
            this.layoutControl6.Controls.Add(this.textEditTargetPosX);
            this.layoutControl6.Controls.Add(this.SendCmdHommingButton);
            this.layoutControl6.Controls.Add(this.SendCommandMoveStopButton);
            this.layoutControl6.Controls.Add(this.ErrorResetButton);
            this.layoutControl6.Controls.Add(this.EmergencyStopButton);
            this.layoutControl6.Controls.Add(this.RobotEnableButton);
            this.layoutControl6.Controls.Add(this.SendCmdPositionButton);
            this.layoutControl6.Controls.Add(this.layoutControl8);
            this.layoutControl6.Controls.Add(this.radioGroupMenualControlMode);
            this.layoutControl6.Controls.Add(this.radioGroupMenualMode);
            this.layoutControl6.Controls.Add(this.checkButtonMenualMode);
            this.layoutControl6.Location = new System.Drawing.Point(2, 26);
            this.layoutControl6.Name = "layoutControl6";
            this.layoutControl6.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl6.Root = this.layoutControlGroup7;
            this.layoutControl6.Size = new System.Drawing.Size(477, 401);
            this.layoutControl6.TabIndex = 0;
            this.layoutControl6.Text = "layoutControl6";
            // 
            // radioGroupCalibration
            // 
            this.radioGroupCalibration.Location = new System.Drawing.Point(183, 298);
            this.radioGroupCalibration.Name = "radioGroupCalibration";
            this.radioGroupCalibration.Properties.Columns = 2;
            this.radioGroupCalibration.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "투광부 보정"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "검사부 보정")});
            this.radioGroupCalibration.Size = new System.Drawing.Size(290, 25);
            this.radioGroupCalibration.StyleController = this.layoutControl6;
            this.radioGroupCalibration.TabIndex = 38;
            // 
            // checkEditCalibration
            // 
            this.checkEditCalibration.Location = new System.Drawing.Point(4, 298);
            this.checkEditCalibration.Name = "checkEditCalibration";
            this.checkEditCalibration.Properties.Caption = "위치보정 활성화";
            this.checkEditCalibration.Size = new System.Drawing.Size(175, 19);
            this.checkEditCalibration.StyleController = this.layoutControl6;
            this.checkEditCalibration.TabIndex = 37;
            // 
            // textEditTargetAcceleration
            // 
            this.textEditTargetAcceleration.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditTargetAcceleration.Location = new System.Drawing.Point(234, 375);
            this.textEditTargetAcceleration.Margin = new System.Windows.Forms.Padding(1);
            this.textEditTargetAcceleration.Name = "textEditTargetAcceleration";
            this.textEditTargetAcceleration.Properties.Appearance.Options.UseTextOptions = true;
            this.textEditTargetAcceleration.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.textEditTargetAcceleration.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.textEditTargetAcceleration.Size = new System.Drawing.Size(81, 20);
            this.textEditTargetAcceleration.StyleController = this.layoutControl6;
            this.textEditTargetAcceleration.TabIndex = 36;
            // 
            // textEditTargetVelocity
            // 
            this.textEditTargetVelocity.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textEditTargetVelocity.Location = new System.Drawing.Point(76, 375);
            this.textEditTargetVelocity.Margin = new System.Windows.Forms.Padding(1);
            this.textEditTargetVelocity.Name = "textEditTargetVelocity";
            this.textEditTargetVelocity.Properties.Appearance.Options.UseTextOptions = true;
            this.textEditTargetVelocity.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.textEditTargetVelocity.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.textEditTargetVelocity.Size = new System.Drawing.Size(82, 20);
            this.textEditTargetVelocity.StyleController = this.layoutControl6;
            this.textEditTargetVelocity.TabIndex = 35;
            // 
            // textEditTargetPosFR
            // 
            this.textEditTargetPosFR.Location = new System.Drawing.Point(391, 351);
            this.textEditTargetPosFR.Name = "textEditTargetPosFR";
            this.textEditTargetPosFR.Properties.Appearance.Options.UseTextOptions = true;
            this.textEditTargetPosFR.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.textEditTargetPosFR.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.textEditTargetPosFR.Size = new System.Drawing.Size(82, 20);
            this.textEditTargetPosFR.StyleController = this.layoutControl6;
            this.textEditTargetPosFR.TabIndex = 34;
            // 
            // textEditTargetPosFZ
            // 
            this.textEditTargetPosFZ.Location = new System.Drawing.Point(234, 351);
            this.textEditTargetPosFZ.Name = "textEditTargetPosFZ";
            this.textEditTargetPosFZ.Properties.Appearance.Options.UseTextOptions = true;
            this.textEditTargetPosFZ.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.textEditTargetPosFZ.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.textEditTargetPosFZ.Size = new System.Drawing.Size(81, 20);
            this.textEditTargetPosFZ.StyleController = this.layoutControl6;
            this.textEditTargetPosFZ.TabIndex = 33;
            // 
            // textEditTargetPosZ
            // 
            this.textEditTargetPosZ.Location = new System.Drawing.Point(76, 351);
            this.textEditTargetPosZ.Name = "textEditTargetPosZ";
            this.textEditTargetPosZ.Properties.Appearance.Options.UseTextOptions = true;
            this.textEditTargetPosZ.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.textEditTargetPosZ.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.textEditTargetPosZ.Size = new System.Drawing.Size(82, 20);
            this.textEditTargetPosZ.StyleController = this.layoutControl6;
            this.textEditTargetPosZ.TabIndex = 32;
            // 
            // textEditTargetPosY2
            // 
            this.textEditTargetPosY2.Location = new System.Drawing.Point(391, 327);
            this.textEditTargetPosY2.Name = "textEditTargetPosY2";
            this.textEditTargetPosY2.Properties.Appearance.Options.UseTextOptions = true;
            this.textEditTargetPosY2.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.textEditTargetPosY2.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.textEditTargetPosY2.Size = new System.Drawing.Size(82, 20);
            this.textEditTargetPosY2.StyleController = this.layoutControl6;
            this.textEditTargetPosY2.TabIndex = 31;
            // 
            // textEditTargetPosY1
            // 
            this.textEditTargetPosY1.Location = new System.Drawing.Point(234, 327);
            this.textEditTargetPosY1.Name = "textEditTargetPosY1";
            this.textEditTargetPosY1.Properties.Appearance.Options.UseTextOptions = true;
            this.textEditTargetPosY1.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.textEditTargetPosY1.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.textEditTargetPosY1.Size = new System.Drawing.Size(81, 20);
            this.textEditTargetPosY1.StyleController = this.layoutControl6;
            this.textEditTargetPosY1.TabIndex = 30;
            // 
            // textEditTargetPosX
            // 
            this.textEditTargetPosX.Location = new System.Drawing.Point(76, 327);
            this.textEditTargetPosX.Name = "textEditTargetPosX";
            this.textEditTargetPosX.Properties.Appearance.Options.UseTextOptions = true;
            this.textEditTargetPosX.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.textEditTargetPosX.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.textEditTargetPosX.Size = new System.Drawing.Size(82, 20);
            this.textEditTargetPosX.StyleController = this.layoutControl6;
            this.textEditTargetPosX.TabIndex = 29;
            // 
            // SendCmdHommingButton
            // 
            this.SendCmdHommingButton.Location = new System.Drawing.Point(122, 4);
            this.SendCmdHommingButton.Name = "SendCmdHommingButton";
            this.SendCmdHommingButton.Size = new System.Drawing.Size(114, 22);
            this.SendCmdHommingButton.StyleController = this.layoutControl6;
            this.SendCmdHommingButton.TabIndex = 28;
            this.SendCmdHommingButton.Text = "원점 복귀";
            this.SendCmdHommingButton.Click += new System.EventHandler(this.SendCmdHommingButton_Click);
            // 
            // SendCommandMoveStopButton
            // 
            this.SendCommandMoveStopButton.Location = new System.Drawing.Point(397, 375);
            this.SendCommandMoveStopButton.Margin = new System.Windows.Forms.Padding(1);
            this.SendCommandMoveStopButton.Name = "SendCommandMoveStopButton";
            this.SendCommandMoveStopButton.Size = new System.Drawing.Size(76, 22);
            this.SendCommandMoveStopButton.StyleController = this.layoutControl6;
            this.SendCommandMoveStopButton.TabIndex = 27;
            this.SendCommandMoveStopButton.Text = "정지 하기";
            this.SendCommandMoveStopButton.Click += new System.EventHandler(this.SendCommandMoveStopButton_Click);
            // 
            // ErrorResetButton
            // 
            this.ErrorResetButton.Location = new System.Drawing.Point(357, 4);
            this.ErrorResetButton.Name = "ErrorResetButton";
            this.ErrorResetButton.Size = new System.Drawing.Size(116, 22);
            this.ErrorResetButton.StyleController = this.layoutControl6;
            this.ErrorResetButton.TabIndex = 26;
            this.ErrorResetButton.Text = "오류 복구";
            this.ErrorResetButton.Click += new System.EventHandler(this.ErrorResetButton_Click);
            // 
            // EmergencyStopButton
            // 
            this.EmergencyStopButton.Location = new System.Drawing.Point(240, 4);
            this.EmergencyStopButton.Name = "EmergencyStopButton";
            this.EmergencyStopButton.Size = new System.Drawing.Size(113, 22);
            this.EmergencyStopButton.StyleController = this.layoutControl6;
            this.EmergencyStopButton.TabIndex = 25;
            this.EmergencyStopButton.Text = "응급 정지";
            this.EmergencyStopButton.Click += new System.EventHandler(this.EmergencyStopButton_Click);
            // 
            // RobotEnableButton
            // 
            this.RobotEnableButton.Location = new System.Drawing.Point(4, 4);
            this.RobotEnableButton.Name = "RobotEnableButton";
            this.RobotEnableButton.Size = new System.Drawing.Size(114, 22);
            this.RobotEnableButton.StyleController = this.layoutControl6;
            this.RobotEnableButton.TabIndex = 24;
            this.RobotEnableButton.Text = "모션제어 활성화";
            this.RobotEnableButton.Click += new System.EventHandler(this.RobotEnableButton_Click);
            // 
            // SendCmdPositionButton
            // 
            this.SendCmdPositionButton.Location = new System.Drawing.Point(319, 375);
            this.SendCmdPositionButton.Margin = new System.Windows.Forms.Padding(1);
            this.SendCmdPositionButton.Name = "SendCmdPositionButton";
            this.SendCmdPositionButton.Size = new System.Drawing.Size(74, 22);
            this.SendCmdPositionButton.StyleController = this.layoutControl6;
            this.SendCmdPositionButton.TabIndex = 22;
            this.SendCmdPositionButton.Text = "이동 하기";
            this.SendCmdPositionButton.Click += new System.EventHandler(this.SendCmdPositionButton_Click);
            // 
            // layoutControl8
            // 
            this.layoutControl8.Controls.Add(this.textEditUserDefineValue);
            this.layoutControl8.Controls.Add(this.CheckButtonFRPlusControlCommand);
            this.layoutControl8.Controls.Add(this.CheckButtonFRStopControlCommand);
            this.layoutControl8.Controls.Add(this.CheckButtonFRMinusControlCommand);
            this.layoutControl8.Controls.Add(this.CheckButtonFZPlusControlCommand);
            this.layoutControl8.Controls.Add(this.CheckButtonFZStopControlCommand);
            this.layoutControl8.Controls.Add(this.CheckButtonFZMinusControlCommand);
            this.layoutControl8.Controls.Add(this.labelControl6);
            this.layoutControl8.Controls.Add(this.labelControl5);
            this.layoutControl8.Controls.Add(this.CheckButtonZPlusControlCommand);
            this.layoutControl8.Controls.Add(this.CheckButtonZStopControlCommand);
            this.layoutControl8.Controls.Add(this.CheckButtonZMinusControlCommand);
            this.layoutControl8.Controls.Add(this.labelControl4);
            this.layoutControl8.Controls.Add(this.CheckButtonY2PlusControlCommand);
            this.layoutControl8.Controls.Add(this.CheckButtonY2StopControlCommand);
            this.layoutControl8.Controls.Add(this.CheckButtonY2MinusControlCommand);
            this.layoutControl8.Controls.Add(this.labelControl3);
            this.layoutControl8.Controls.Add(this.CheckButtonY1PlusControlCommand);
            this.layoutControl8.Controls.Add(this.CheckButtonY1StopControlCommand);
            this.layoutControl8.Controls.Add(this.CheckButtonY1MinusControlCommand);
            this.layoutControl8.Controls.Add(this.labelControl2);
            this.layoutControl8.Controls.Add(this.labelControl1);
            this.layoutControl8.Controls.Add(this.CheckButtonXPlusControlCommand);
            this.layoutControl8.Controls.Add(this.CheckButtonXStopControlCommand);
            this.layoutControl8.Controls.Add(this.CheckButtonXMinusControlCommand);
            this.layoutControl8.Controls.Add(this.checkButtonHighValue);
            this.layoutControl8.Controls.Add(this.checkButtonMiddleValue);
            this.layoutControl8.Controls.Add(this.radioGroupMenualValueMode);
            this.layoutControl8.Controls.Add(this.checkButtonLowValue);
            this.layoutControl8.Location = new System.Drawing.Point(4, 85);
            this.layoutControl8.Name = "layoutControl8";
            this.layoutControl8.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(914, 163, 650, 400);
            this.layoutControl8.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl8.Root = this.layoutControlGroup9;
            this.layoutControl8.Size = new System.Drawing.Size(469, 209);
            this.layoutControl8.TabIndex = 19;
            this.layoutControl8.Text = "layoutControl8";
            // 
            // textEditUserDefineValue
            // 
            this.textEditUserDefineValue.Location = new System.Drawing.Point(229, 29);
            this.textEditUserDefineValue.Name = "textEditUserDefineValue";
            this.textEditUserDefineValue.Properties.Appearance.Options.UseTextOptions = true;
            this.textEditUserDefineValue.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.textEditUserDefineValue.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.textEditUserDefineValue.Size = new System.Drawing.Size(237, 20);
            this.textEditUserDefineValue.StyleController = this.layoutControl8;
            this.textEditUserDefineValue.TabIndex = 46;
            this.textEditUserDefineValue.EditValueChanged += new System.EventHandler(this.textEditUserDefineValue_EditValueChanged);
            // 
            // CheckButtonFRPlusControlCommand
            // 
            this.CheckButtonFRPlusControlCommand.Location = new System.Drawing.Point(327, 183);
            this.CheckButtonFRPlusControlCommand.Name = "CheckButtonFRPlusControlCommand";
            this.CheckButtonFRPlusControlCommand.Size = new System.Drawing.Size(139, 22);
            this.CheckButtonFRPlusControlCommand.StyleController = this.layoutControl8;
            this.CheckButtonFRPlusControlCommand.TabIndex = 45;
            this.CheckButtonFRPlusControlCommand.Text = "》(+)";
            this.CheckButtonFRPlusControlCommand.CheckedChanged += new System.EventHandler(this.CheckButtonStateControlCommand_CheckedChanged);
            this.CheckButtonFRPlusControlCommand.Click += new System.EventHandler(this.checkButtonJogMove_Click);
            // 
            // CheckButtonFRStopControlCommand
            // 
            this.CheckButtonFRStopControlCommand.Location = new System.Drawing.Point(181, 183);
            this.CheckButtonFRStopControlCommand.Name = "CheckButtonFRStopControlCommand";
            this.CheckButtonFRStopControlCommand.Size = new System.Drawing.Size(142, 22);
            this.CheckButtonFRStopControlCommand.StyleController = this.layoutControl8;
            this.CheckButtonFRStopControlCommand.TabIndex = 44;
            this.CheckButtonFRStopControlCommand.Text = "■(정지)";
            this.CheckButtonFRStopControlCommand.CheckedChanged += new System.EventHandler(this.ButtonStopControlCommand_CheckedChanged);
            this.CheckButtonFRStopControlCommand.Click += new System.EventHandler(this.checkButtonJogMove_Click);
            // 
            // CheckButtonFRMinusControlCommand
            // 
            this.CheckButtonFRMinusControlCommand.Location = new System.Drawing.Point(34, 183);
            this.CheckButtonFRMinusControlCommand.Name = "CheckButtonFRMinusControlCommand";
            this.CheckButtonFRMinusControlCommand.Size = new System.Drawing.Size(143, 22);
            this.CheckButtonFRMinusControlCommand.StyleController = this.layoutControl8;
            this.CheckButtonFRMinusControlCommand.TabIndex = 43;
            this.CheckButtonFRMinusControlCommand.Text = "(-)《";
            this.CheckButtonFRMinusControlCommand.CheckedChanged += new System.EventHandler(this.CheckButtonStateControlCommand_CheckedChanged);
            this.CheckButtonFRMinusControlCommand.Click += new System.EventHandler(this.checkButtonJogMove_Click);
            // 
            // CheckButtonFZPlusControlCommand
            // 
            this.CheckButtonFZPlusControlCommand.Location = new System.Drawing.Point(327, 157);
            this.CheckButtonFZPlusControlCommand.Name = "CheckButtonFZPlusControlCommand";
            this.CheckButtonFZPlusControlCommand.Size = new System.Drawing.Size(139, 22);
            this.CheckButtonFZPlusControlCommand.StyleController = this.layoutControl8;
            this.CheckButtonFZPlusControlCommand.TabIndex = 42;
            this.CheckButtonFZPlusControlCommand.Text = "》(+)";
            this.CheckButtonFZPlusControlCommand.CheckedChanged += new System.EventHandler(this.CheckButtonStateControlCommand_CheckedChanged);
            this.CheckButtonFZPlusControlCommand.Click += new System.EventHandler(this.checkButtonJogMove_Click);
            // 
            // CheckButtonFZStopControlCommand
            // 
            this.CheckButtonFZStopControlCommand.Location = new System.Drawing.Point(181, 157);
            this.CheckButtonFZStopControlCommand.Name = "CheckButtonFZStopControlCommand";
            this.CheckButtonFZStopControlCommand.Size = new System.Drawing.Size(142, 22);
            this.CheckButtonFZStopControlCommand.StyleController = this.layoutControl8;
            this.CheckButtonFZStopControlCommand.TabIndex = 41;
            this.CheckButtonFZStopControlCommand.Text = "■(정지)";
            this.CheckButtonFZStopControlCommand.CheckedChanged += new System.EventHandler(this.ButtonStopControlCommand_CheckedChanged);
            this.CheckButtonFZStopControlCommand.Click += new System.EventHandler(this.checkButtonJogMove_Click);
            // 
            // CheckButtonFZMinusControlCommand
            // 
            this.CheckButtonFZMinusControlCommand.Location = new System.Drawing.Point(34, 157);
            this.CheckButtonFZMinusControlCommand.Name = "CheckButtonFZMinusControlCommand";
            this.CheckButtonFZMinusControlCommand.Size = new System.Drawing.Size(143, 22);
            this.CheckButtonFZMinusControlCommand.StyleController = this.layoutControl8;
            this.CheckButtonFZMinusControlCommand.TabIndex = 40;
            this.CheckButtonFZMinusControlCommand.Text = "(-)《";
            this.CheckButtonFZMinusControlCommand.CheckedChanged += new System.EventHandler(this.CheckButtonStateControlCommand_CheckedChanged);
            this.CheckButtonFZMinusControlCommand.Click += new System.EventHandler(this.checkButtonJogMove_Click);
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Options.UseTextOptions = true;
            this.labelControl6.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl6.Location = new System.Drawing.Point(3, 183);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(27, 23);
            this.labelControl6.StyleController = this.layoutControl8;
            this.labelControl6.TabIndex = 39;
            this.labelControl6.Text = "FR 축";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Options.UseTextOptions = true;
            this.labelControl5.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl5.Location = new System.Drawing.Point(3, 157);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(27, 14);
            this.labelControl5.StyleController = this.layoutControl8;
            this.labelControl5.TabIndex = 38;
            this.labelControl5.Text = "FZ 축";
            // 
            // CheckButtonZPlusControlCommand
            // 
            this.CheckButtonZPlusControlCommand.Location = new System.Drawing.Point(327, 131);
            this.CheckButtonZPlusControlCommand.Name = "CheckButtonZPlusControlCommand";
            this.CheckButtonZPlusControlCommand.Size = new System.Drawing.Size(139, 22);
            this.CheckButtonZPlusControlCommand.StyleController = this.layoutControl8;
            this.CheckButtonZPlusControlCommand.TabIndex = 37;
            this.CheckButtonZPlusControlCommand.Text = "》(+)";
            this.CheckButtonZPlusControlCommand.CheckedChanged += new System.EventHandler(this.CheckButtonStateControlCommand_CheckedChanged);
            this.CheckButtonZPlusControlCommand.Click += new System.EventHandler(this.checkButtonJogMove_Click);
            // 
            // CheckButtonZStopControlCommand
            // 
            this.CheckButtonZStopControlCommand.Location = new System.Drawing.Point(181, 131);
            this.CheckButtonZStopControlCommand.Name = "CheckButtonZStopControlCommand";
            this.CheckButtonZStopControlCommand.Size = new System.Drawing.Size(142, 22);
            this.CheckButtonZStopControlCommand.StyleController = this.layoutControl8;
            this.CheckButtonZStopControlCommand.TabIndex = 36;
            this.CheckButtonZStopControlCommand.Text = "■(정지)";
            this.CheckButtonZStopControlCommand.CheckedChanged += new System.EventHandler(this.ButtonStopControlCommand_CheckedChanged);
            this.CheckButtonZStopControlCommand.Click += new System.EventHandler(this.checkButtonJogMove_Click);
            // 
            // CheckButtonZMinusControlCommand
            // 
            this.CheckButtonZMinusControlCommand.Location = new System.Drawing.Point(36, 131);
            this.CheckButtonZMinusControlCommand.Name = "CheckButtonZMinusControlCommand";
            this.CheckButtonZMinusControlCommand.Size = new System.Drawing.Size(141, 22);
            this.CheckButtonZMinusControlCommand.StyleController = this.layoutControl8;
            this.CheckButtonZMinusControlCommand.TabIndex = 35;
            this.CheckButtonZMinusControlCommand.Text = "(-)《";
            this.CheckButtonZMinusControlCommand.CheckedChanged += new System.EventHandler(this.CheckButtonStateControlCommand_CheckedChanged);
            this.CheckButtonZMinusControlCommand.Click += new System.EventHandler(this.checkButtonJogMove_Click);
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Options.UseTextOptions = true;
            this.labelControl4.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl4.Location = new System.Drawing.Point(3, 131);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(29, 14);
            this.labelControl4.StyleController = this.layoutControl8;
            this.labelControl4.TabIndex = 34;
            this.labelControl4.Text = "Z   축";
            // 
            // CheckButtonY2PlusControlCommand
            // 
            this.CheckButtonY2PlusControlCommand.Location = new System.Drawing.Point(327, 105);
            this.CheckButtonY2PlusControlCommand.Name = "CheckButtonY2PlusControlCommand";
            this.CheckButtonY2PlusControlCommand.Size = new System.Drawing.Size(139, 22);
            this.CheckButtonY2PlusControlCommand.StyleController = this.layoutControl8;
            this.CheckButtonY2PlusControlCommand.TabIndex = 33;
            this.CheckButtonY2PlusControlCommand.Text = "》(+)";
            this.CheckButtonY2PlusControlCommand.CheckedChanged += new System.EventHandler(this.CheckButtonStateControlCommand_CheckedChanged);
            this.CheckButtonY2PlusControlCommand.Click += new System.EventHandler(this.checkButtonJogMove_Click);
            // 
            // CheckButtonY2StopControlCommand
            // 
            this.CheckButtonY2StopControlCommand.Location = new System.Drawing.Point(181, 105);
            this.CheckButtonY2StopControlCommand.Name = "CheckButtonY2StopControlCommand";
            this.CheckButtonY2StopControlCommand.Size = new System.Drawing.Size(142, 22);
            this.CheckButtonY2StopControlCommand.StyleController = this.layoutControl8;
            this.CheckButtonY2StopControlCommand.TabIndex = 32;
            this.CheckButtonY2StopControlCommand.Text = "■(정지)";
            this.CheckButtonY2StopControlCommand.CheckedChanged += new System.EventHandler(this.ButtonStopControlCommand_CheckedChanged);
            this.CheckButtonY2StopControlCommand.Click += new System.EventHandler(this.checkButtonJogMove_Click);
            // 
            // CheckButtonY2MinusControlCommand
            // 
            this.CheckButtonY2MinusControlCommand.Location = new System.Drawing.Point(36, 105);
            this.CheckButtonY2MinusControlCommand.Name = "CheckButtonY2MinusControlCommand";
            this.CheckButtonY2MinusControlCommand.Size = new System.Drawing.Size(141, 22);
            this.CheckButtonY2MinusControlCommand.StyleController = this.layoutControl8;
            this.CheckButtonY2MinusControlCommand.TabIndex = 31;
            this.CheckButtonY2MinusControlCommand.Text = "(-)《";
            this.CheckButtonY2MinusControlCommand.CheckedChanged += new System.EventHandler(this.CheckButtonStateControlCommand_CheckedChanged);
            this.CheckButtonY2MinusControlCommand.Click += new System.EventHandler(this.checkButtonJogMove_Click);
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Options.UseTextOptions = true;
            this.labelControl3.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl3.Location = new System.Drawing.Point(3, 105);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(29, 14);
            this.labelControl3.StyleController = this.layoutControl8;
            this.labelControl3.TabIndex = 30;
            this.labelControl3.Text = "Y2 축";
            // 
            // CheckButtonY1PlusControlCommand
            // 
            this.CheckButtonY1PlusControlCommand.Location = new System.Drawing.Point(327, 79);
            this.CheckButtonY1PlusControlCommand.Name = "CheckButtonY1PlusControlCommand";
            this.CheckButtonY1PlusControlCommand.Size = new System.Drawing.Size(139, 22);
            this.CheckButtonY1PlusControlCommand.StyleController = this.layoutControl8;
            this.CheckButtonY1PlusControlCommand.TabIndex = 29;
            this.CheckButtonY1PlusControlCommand.Text = "》(+)";
            this.CheckButtonY1PlusControlCommand.CheckedChanged += new System.EventHandler(this.CheckButtonStateControlCommand_CheckedChanged);
            this.CheckButtonY1PlusControlCommand.Click += new System.EventHandler(this.checkButtonJogMove_Click);
            // 
            // CheckButtonY1StopControlCommand
            // 
            this.CheckButtonY1StopControlCommand.Location = new System.Drawing.Point(181, 79);
            this.CheckButtonY1StopControlCommand.Name = "CheckButtonY1StopControlCommand";
            this.CheckButtonY1StopControlCommand.Size = new System.Drawing.Size(142, 22);
            this.CheckButtonY1StopControlCommand.StyleController = this.layoutControl8;
            this.CheckButtonY1StopControlCommand.TabIndex = 28;
            this.CheckButtonY1StopControlCommand.Text = "■(정지)";
            this.CheckButtonY1StopControlCommand.CheckedChanged += new System.EventHandler(this.ButtonStopControlCommand_CheckedChanged);
            this.CheckButtonY1StopControlCommand.Click += new System.EventHandler(this.checkButtonJogMove_Click);
            // 
            // CheckButtonY1MinusControlCommand
            // 
            this.CheckButtonY1MinusControlCommand.Location = new System.Drawing.Point(36, 79);
            this.CheckButtonY1MinusControlCommand.Name = "CheckButtonY1MinusControlCommand";
            this.CheckButtonY1MinusControlCommand.Size = new System.Drawing.Size(141, 22);
            this.CheckButtonY1MinusControlCommand.StyleController = this.layoutControl8;
            this.CheckButtonY1MinusControlCommand.TabIndex = 27;
            this.CheckButtonY1MinusControlCommand.Text = "(-)《";
            this.CheckButtonY1MinusControlCommand.CheckedChanged += new System.EventHandler(this.CheckButtonStateControlCommand_CheckedChanged);
            this.CheckButtonY1MinusControlCommand.Click += new System.EventHandler(this.checkButtonJogMove_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Options.UseTextOptions = true;
            this.labelControl2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl2.Location = new System.Drawing.Point(3, 79);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(29, 14);
            this.labelControl2.StyleController = this.layoutControl8;
            this.labelControl2.TabIndex = 26;
            this.labelControl2.Text = "Y1 축";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Options.UseTextOptions = true;
            this.labelControl1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControl1.Location = new System.Drawing.Point(3, 53);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(29, 14);
            this.labelControl1.StyleController = this.layoutControl8;
            this.labelControl1.TabIndex = 25;
            this.labelControl1.Text = "X   축";
            // 
            // CheckButtonXPlusControlCommand
            // 
            this.CheckButtonXPlusControlCommand.Location = new System.Drawing.Point(327, 53);
            this.CheckButtonXPlusControlCommand.Name = "CheckButtonXPlusControlCommand";
            this.CheckButtonXPlusControlCommand.Size = new System.Drawing.Size(139, 22);
            this.CheckButtonXPlusControlCommand.StyleController = this.layoutControl8;
            this.CheckButtonXPlusControlCommand.TabIndex = 24;
            this.CheckButtonXPlusControlCommand.Text = "》(+)";
            this.CheckButtonXPlusControlCommand.CheckedChanged += new System.EventHandler(this.CheckButtonStateControlCommand_CheckedChanged);
            this.CheckButtonXPlusControlCommand.Click += new System.EventHandler(this.checkButtonJogMove_Click);
            // 
            // CheckButtonXStopControlCommand
            // 
            this.CheckButtonXStopControlCommand.Location = new System.Drawing.Point(181, 53);
            this.CheckButtonXStopControlCommand.Name = "CheckButtonXStopControlCommand";
            this.CheckButtonXStopControlCommand.Size = new System.Drawing.Size(142, 22);
            this.CheckButtonXStopControlCommand.StyleController = this.layoutControl8;
            this.CheckButtonXStopControlCommand.TabIndex = 23;
            this.CheckButtonXStopControlCommand.Text = "■(정지)";
            this.CheckButtonXStopControlCommand.CheckedChanged += new System.EventHandler(this.ButtonStopControlCommand_CheckedChanged);
            this.CheckButtonXStopControlCommand.Click += new System.EventHandler(this.checkButtonJogMove_Click);
            // 
            // CheckButtonXMinusControlCommand
            // 
            this.CheckButtonXMinusControlCommand.Location = new System.Drawing.Point(36, 53);
            this.CheckButtonXMinusControlCommand.Name = "CheckButtonXMinusControlCommand";
            this.CheckButtonXMinusControlCommand.Size = new System.Drawing.Size(141, 22);
            this.CheckButtonXMinusControlCommand.StyleController = this.layoutControl8;
            this.CheckButtonXMinusControlCommand.TabIndex = 22;
            this.CheckButtonXMinusControlCommand.Text = "(-)《";
            this.CheckButtonXMinusControlCommand.CheckedChanged += new System.EventHandler(this.CheckButtonStateControlCommand_CheckedChanged);
            this.CheckButtonXMinusControlCommand.Click += new System.EventHandler(this.checkButtonJogMove_Click);
            // 
            // checkButtonHighValue
            // 
            this.checkButtonHighValue.Location = new System.Drawing.Point(349, 3);
            this.checkButtonHighValue.Name = "checkButtonHighValue";
            this.checkButtonHighValue.Size = new System.Drawing.Size(117, 22);
            this.checkButtonHighValue.StyleController = this.layoutControl8;
            this.checkButtonHighValue.TabIndex = 16;
            this.checkButtonHighValue.Text = "큰값";
            this.checkButtonHighValue.CheckedChanged += new System.EventHandler(this.checkButtonHighValue_CheckedChanged);
            this.checkButtonHighValue.Click += new System.EventHandler(this.checkButtonHighValue_Click);
            // 
            // checkButtonMiddleValue
            // 
            this.checkButtonMiddleValue.Location = new System.Drawing.Point(225, 3);
            this.checkButtonMiddleValue.Name = "checkButtonMiddleValue";
            this.checkButtonMiddleValue.Size = new System.Drawing.Size(120, 22);
            this.checkButtonMiddleValue.StyleController = this.layoutControl8;
            this.checkButtonMiddleValue.TabIndex = 15;
            this.checkButtonMiddleValue.Text = "중간값";
            this.checkButtonMiddleValue.CheckedChanged += new System.EventHandler(this.checkButtonMiddleValue_CheckedChanged);
            this.checkButtonMiddleValue.Click += new System.EventHandler(this.checkButtonMiddleValue_Click);
            // 
            // radioGroupMenualValueMode
            // 
            this.radioGroupMenualValueMode.Location = new System.Drawing.Point(3, 3);
            this.radioGroupMenualValueMode.Name = "radioGroupMenualValueMode";
            this.radioGroupMenualValueMode.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "기본값 모드"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "사용자 모드")});
            this.radioGroupMenualValueMode.Size = new System.Drawing.Size(97, 46);
            this.radioGroupMenualValueMode.StyleController = this.layoutControl8;
            this.radioGroupMenualValueMode.TabIndex = 1;
            this.radioGroupMenualValueMode.SelectedIndexChanged += new System.EventHandler(this.radioGroupMenualValueMode_SelectedIndexChanged);
            // 
            // checkButtonLowValue
            // 
            this.checkButtonLowValue.Location = new System.Drawing.Point(104, 3);
            this.checkButtonLowValue.Name = "checkButtonLowValue";
            this.checkButtonLowValue.Size = new System.Drawing.Size(117, 22);
            this.checkButtonLowValue.StyleController = this.layoutControl8;
            this.checkButtonLowValue.TabIndex = 14;
            this.checkButtonLowValue.Text = "작은값";
            this.checkButtonLowValue.CheckedChanged += new System.EventHandler(this.checkButtonLowValue_CheckedChanged);
            this.checkButtonLowValue.Click += new System.EventHandler(this.checkButtonLowValue_Click);
            // 
            // layoutControlGroup9
            // 
            this.layoutControlGroup9.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup9.GroupBordersVisible = false;
            this.layoutControlGroup9.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem32,
            this.layoutControlItem29,
            this.layoutControlItem30,
            this.layoutControlItem31,
            this.layoutControlItem37,
            this.layoutControlItem40,
            this.layoutControlItem38,
            this.layoutControlItem39,
            this.layoutControlItem41,
            this.layoutControlItem42,
            this.layoutControlItem43,
            this.layoutControlItem44,
            this.layoutControlItem45,
            this.layoutControlItem46,
            this.layoutControlItem47,
            this.layoutControlItem48,
            this.layoutControlItem49,
            this.layoutControlItem50,
            this.layoutControlItem51,
            this.layoutControlItem52,
            this.layoutControlItem53,
            this.layoutControlItem54,
            this.layoutControlItem55,
            this.layoutControlItem56,
            this.layoutControlItem57,
            this.layoutControlItem58,
            this.layoutControlItem59,
            this.layoutControlItem60,
            this.textBoxUserDefineValue});
            this.layoutControlGroup9.Name = "Root";
            this.layoutControlGroup9.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.layoutControlGroup9.Size = new System.Drawing.Size(469, 209);
            this.layoutControlGroup9.TextVisible = false;
            // 
            // layoutControlItem32
            // 
            this.layoutControlItem32.Control = this.radioGroupMenualValueMode;
            this.layoutControlItem32.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem32.Name = "layoutControlItem32";
            this.layoutControlItem32.Size = new System.Drawing.Size(101, 50);
            this.layoutControlItem32.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem32.TextVisible = false;
            // 
            // layoutControlItem29
            // 
            this.layoutControlItem29.Control = this.checkButtonLowValue;
            this.layoutControlItem29.Location = new System.Drawing.Point(101, 0);
            this.layoutControlItem29.Name = "layoutControlItem29";
            this.layoutControlItem29.Size = new System.Drawing.Size(121, 26);
            this.layoutControlItem29.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem29.TextVisible = false;
            // 
            // layoutControlItem30
            // 
            this.layoutControlItem30.Control = this.checkButtonMiddleValue;
            this.layoutControlItem30.Location = new System.Drawing.Point(222, 0);
            this.layoutControlItem30.Name = "layoutControlItem30";
            this.layoutControlItem30.Size = new System.Drawing.Size(124, 26);
            this.layoutControlItem30.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem30.TextVisible = false;
            // 
            // layoutControlItem31
            // 
            this.layoutControlItem31.Control = this.checkButtonHighValue;
            this.layoutControlItem31.Location = new System.Drawing.Point(346, 0);
            this.layoutControlItem31.Name = "layoutControlItem31";
            this.layoutControlItem31.Size = new System.Drawing.Size(121, 26);
            this.layoutControlItem31.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem31.TextVisible = false;
            // 
            // layoutControlItem37
            // 
            this.layoutControlItem37.Control = this.CheckButtonXMinusControlCommand;
            this.layoutControlItem37.Location = new System.Drawing.Point(33, 50);
            this.layoutControlItem37.Name = "layoutControlItem37";
            this.layoutControlItem37.Size = new System.Drawing.Size(145, 26);
            this.layoutControlItem37.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem37.TextVisible = false;
            // 
            // layoutControlItem40
            // 
            this.layoutControlItem40.Control = this.labelControl1;
            this.layoutControlItem40.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.layoutControlItem40.Location = new System.Drawing.Point(0, 50);
            this.layoutControlItem40.Name = "layoutControlItem40";
            this.layoutControlItem40.Size = new System.Drawing.Size(33, 26);
            this.layoutControlItem40.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem40.TextVisible = false;
            // 
            // layoutControlItem38
            // 
            this.layoutControlItem38.Control = this.CheckButtonXStopControlCommand;
            this.layoutControlItem38.Location = new System.Drawing.Point(178, 50);
            this.layoutControlItem38.Name = "layoutControlItem38";
            this.layoutControlItem38.Size = new System.Drawing.Size(146, 26);
            this.layoutControlItem38.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem38.TextVisible = false;
            // 
            // layoutControlItem39
            // 
            this.layoutControlItem39.Control = this.CheckButtonXPlusControlCommand;
            this.layoutControlItem39.Location = new System.Drawing.Point(324, 50);
            this.layoutControlItem39.Name = "layoutControlItem39";
            this.layoutControlItem39.Size = new System.Drawing.Size(143, 26);
            this.layoutControlItem39.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem39.TextVisible = false;
            // 
            // layoutControlItem41
            // 
            this.layoutControlItem41.Control = this.labelControl2;
            this.layoutControlItem41.Location = new System.Drawing.Point(0, 76);
            this.layoutControlItem41.Name = "layoutControlItem41";
            this.layoutControlItem41.Size = new System.Drawing.Size(33, 26);
            this.layoutControlItem41.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem41.TextVisible = false;
            // 
            // layoutControlItem42
            // 
            this.layoutControlItem42.Control = this.CheckButtonY1MinusControlCommand;
            this.layoutControlItem42.Location = new System.Drawing.Point(33, 76);
            this.layoutControlItem42.Name = "layoutControlItem42";
            this.layoutControlItem42.Size = new System.Drawing.Size(145, 26);
            this.layoutControlItem42.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem42.TextVisible = false;
            // 
            // layoutControlItem43
            // 
            this.layoutControlItem43.Control = this.CheckButtonY1StopControlCommand;
            this.layoutControlItem43.Location = new System.Drawing.Point(178, 76);
            this.layoutControlItem43.Name = "layoutControlItem43";
            this.layoutControlItem43.Size = new System.Drawing.Size(146, 26);
            this.layoutControlItem43.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem43.TextVisible = false;
            // 
            // layoutControlItem44
            // 
            this.layoutControlItem44.Control = this.CheckButtonY1PlusControlCommand;
            this.layoutControlItem44.Location = new System.Drawing.Point(324, 76);
            this.layoutControlItem44.Name = "layoutControlItem44";
            this.layoutControlItem44.Size = new System.Drawing.Size(143, 26);
            this.layoutControlItem44.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem44.TextVisible = false;
            // 
            // layoutControlItem45
            // 
            this.layoutControlItem45.Control = this.labelControl3;
            this.layoutControlItem45.Location = new System.Drawing.Point(0, 102);
            this.layoutControlItem45.Name = "layoutControlItem45";
            this.layoutControlItem45.Size = new System.Drawing.Size(33, 26);
            this.layoutControlItem45.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem45.TextVisible = false;
            // 
            // layoutControlItem46
            // 
            this.layoutControlItem46.Control = this.CheckButtonY2MinusControlCommand;
            this.layoutControlItem46.Location = new System.Drawing.Point(33, 102);
            this.layoutControlItem46.Name = "layoutControlItem46";
            this.layoutControlItem46.Size = new System.Drawing.Size(145, 26);
            this.layoutControlItem46.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem46.TextVisible = false;
            // 
            // layoutControlItem47
            // 
            this.layoutControlItem47.Control = this.CheckButtonY2StopControlCommand;
            this.layoutControlItem47.Location = new System.Drawing.Point(178, 102);
            this.layoutControlItem47.Name = "layoutControlItem47";
            this.layoutControlItem47.Size = new System.Drawing.Size(146, 26);
            this.layoutControlItem47.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem47.TextVisible = false;
            // 
            // layoutControlItem48
            // 
            this.layoutControlItem48.Control = this.CheckButtonY2PlusControlCommand;
            this.layoutControlItem48.Location = new System.Drawing.Point(324, 102);
            this.layoutControlItem48.Name = "layoutControlItem48";
            this.layoutControlItem48.Size = new System.Drawing.Size(143, 26);
            this.layoutControlItem48.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem48.TextVisible = false;
            // 
            // layoutControlItem49
            // 
            this.layoutControlItem49.Control = this.labelControl4;
            this.layoutControlItem49.Location = new System.Drawing.Point(0, 128);
            this.layoutControlItem49.Name = "layoutControlItem49";
            this.layoutControlItem49.Size = new System.Drawing.Size(33, 26);
            this.layoutControlItem49.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem49.TextVisible = false;
            // 
            // layoutControlItem50
            // 
            this.layoutControlItem50.Control = this.CheckButtonZMinusControlCommand;
            this.layoutControlItem50.Location = new System.Drawing.Point(33, 128);
            this.layoutControlItem50.Name = "layoutControlItem50";
            this.layoutControlItem50.Size = new System.Drawing.Size(145, 26);
            this.layoutControlItem50.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem50.TextVisible = false;
            // 
            // layoutControlItem51
            // 
            this.layoutControlItem51.Control = this.CheckButtonZStopControlCommand;
            this.layoutControlItem51.Location = new System.Drawing.Point(178, 128);
            this.layoutControlItem51.Name = "layoutControlItem51";
            this.layoutControlItem51.Size = new System.Drawing.Size(146, 26);
            this.layoutControlItem51.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem51.TextVisible = false;
            // 
            // layoutControlItem52
            // 
            this.layoutControlItem52.Control = this.CheckButtonZPlusControlCommand;
            this.layoutControlItem52.Location = new System.Drawing.Point(324, 128);
            this.layoutControlItem52.Name = "layoutControlItem52";
            this.layoutControlItem52.Size = new System.Drawing.Size(143, 26);
            this.layoutControlItem52.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem52.TextVisible = false;
            // 
            // layoutControlItem53
            // 
            this.layoutControlItem53.Control = this.labelControl5;
            this.layoutControlItem53.Location = new System.Drawing.Point(0, 154);
            this.layoutControlItem53.Name = "layoutControlItem53";
            this.layoutControlItem53.Size = new System.Drawing.Size(31, 26);
            this.layoutControlItem53.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem53.TextVisible = false;
            // 
            // layoutControlItem54
            // 
            this.layoutControlItem54.Control = this.labelControl6;
            this.layoutControlItem54.Location = new System.Drawing.Point(0, 180);
            this.layoutControlItem54.MaxSize = new System.Drawing.Size(31, 30);
            this.layoutControlItem54.MinSize = new System.Drawing.Size(1, 1);
            this.layoutControlItem54.Name = "layoutControlItem54";
            this.layoutControlItem54.Size = new System.Drawing.Size(31, 27);
            this.layoutControlItem54.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem54.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem54.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem54.TextToControlDistance = 0;
            this.layoutControlItem54.TextVisible = false;
            // 
            // layoutControlItem55
            // 
            this.layoutControlItem55.Control = this.CheckButtonFZMinusControlCommand;
            this.layoutControlItem55.Location = new System.Drawing.Point(31, 154);
            this.layoutControlItem55.Name = "layoutControlItem55";
            this.layoutControlItem55.Size = new System.Drawing.Size(147, 26);
            this.layoutControlItem55.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem55.TextVisible = false;
            // 
            // layoutControlItem56
            // 
            this.layoutControlItem56.Control = this.CheckButtonFZStopControlCommand;
            this.layoutControlItem56.Location = new System.Drawing.Point(178, 154);
            this.layoutControlItem56.Name = "layoutControlItem56";
            this.layoutControlItem56.Size = new System.Drawing.Size(146, 26);
            this.layoutControlItem56.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem56.TextVisible = false;
            // 
            // layoutControlItem57
            // 
            this.layoutControlItem57.Control = this.CheckButtonFZPlusControlCommand;
            this.layoutControlItem57.Location = new System.Drawing.Point(324, 154);
            this.layoutControlItem57.Name = "layoutControlItem57";
            this.layoutControlItem57.Size = new System.Drawing.Size(143, 26);
            this.layoutControlItem57.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem57.TextVisible = false;
            // 
            // layoutControlItem58
            // 
            this.layoutControlItem58.Control = this.CheckButtonFRMinusControlCommand;
            this.layoutControlItem58.Location = new System.Drawing.Point(31, 180);
            this.layoutControlItem58.Name = "layoutControlItem58";
            this.layoutControlItem58.Size = new System.Drawing.Size(147, 27);
            this.layoutControlItem58.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem58.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem58.TextToControlDistance = 0;
            this.layoutControlItem58.TextVisible = false;
            // 
            // layoutControlItem59
            // 
            this.layoutControlItem59.Control = this.CheckButtonFRStopControlCommand;
            this.layoutControlItem59.Location = new System.Drawing.Point(178, 180);
            this.layoutControlItem59.Name = "layoutControlItem59";
            this.layoutControlItem59.Size = new System.Drawing.Size(146, 27);
            this.layoutControlItem59.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem59.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem59.TextToControlDistance = 0;
            this.layoutControlItem59.TextVisible = false;
            // 
            // layoutControlItem60
            // 
            this.layoutControlItem60.Control = this.CheckButtonFRPlusControlCommand;
            this.layoutControlItem60.Location = new System.Drawing.Point(324, 180);
            this.layoutControlItem60.Name = "layoutControlItem60";
            this.layoutControlItem60.Size = new System.Drawing.Size(143, 27);
            this.layoutControlItem60.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem60.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem60.TextToControlDistance = 0;
            this.layoutControlItem60.TextVisible = false;
            // 
            // textBoxUserDefineValue
            // 
            this.textBoxUserDefineValue.Control = this.textEditUserDefineValue;
            this.textBoxUserDefineValue.Location = new System.Drawing.Point(101, 26);
            this.textBoxUserDefineValue.Name = "textBoxUserDefineValue";
            this.textBoxUserDefineValue.Size = new System.Drawing.Size(366, 24);
            this.textBoxUserDefineValue.Text = "사용자 정의값";
            this.textBoxUserDefineValue.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.textBoxUserDefineValue.TextLocation = DevExpress.Utils.Locations.Left;
            this.textBoxUserDefineValue.TextSize = new System.Drawing.Size(120, 20);
            this.textBoxUserDefineValue.TextToControlDistance = 5;
            // 
            // radioGroupMenualControlMode
            // 
            this.radioGroupMenualControlMode.Location = new System.Drawing.Point(171, 56);
            this.radioGroupMenualControlMode.Name = "radioGroupMenualControlMode";
            this.radioGroupMenualControlMode.Properties.Columns = 3;
            this.radioGroupMenualControlMode.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "속도 제어"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "위치 제어"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "좌표 제어")});
            this.radioGroupMenualControlMode.Size = new System.Drawing.Size(302, 25);
            this.radioGroupMenualControlMode.StyleController = this.layoutControl6;
            this.radioGroupMenualControlMode.TabIndex = 13;
            this.radioGroupMenualControlMode.SelectedIndexChanged += new System.EventHandler(this.radioGroupMenualControlMode_SelectedIndexChanged);
            // 
            // radioGroupMenualMode
            // 
            this.radioGroupMenualMode.Location = new System.Drawing.Point(4, 56);
            this.radioGroupMenualMode.Name = "radioGroupMenualMode";
            this.radioGroupMenualMode.Properties.Columns = 2;
            this.radioGroupMenualMode.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "조그 모드"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "좌표 모드")});
            this.radioGroupMenualMode.Size = new System.Drawing.Size(163, 25);
            this.radioGroupMenualMode.StyleController = this.layoutControl6;
            this.radioGroupMenualMode.TabIndex = 12;
            this.radioGroupMenualMode.SelectedIndexChanged += new System.EventHandler(this.radioGroupMenualMode_SelectedIndexChanged);
            // 
            // checkButtonMenualMode
            // 
            this.checkButtonMenualMode.ImageOptions.Image = global::atPhotoInspection.Properties.Resources.touchmode_16x16;
            this.checkButtonMenualMode.Location = new System.Drawing.Point(4, 30);
            this.checkButtonMenualMode.Name = "checkButtonMenualMode";
            this.checkButtonMenualMode.Size = new System.Drawing.Size(469, 22);
            this.checkButtonMenualMode.StyleController = this.layoutControl6;
            this.checkButtonMenualMode.TabIndex = 11;
            this.checkButtonMenualMode.Text = "수동 모드";
            this.checkButtonMenualMode.CheckedChanged += new System.EventHandler(this.checkButtonMenualMode_CheckedChanged);
            // 
            // layoutControlGroup7
            // 
            this.layoutControlGroup7.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup7.GroupBordersVisible = false;
            this.layoutControlGroup7.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem27,
            this.layoutControlItem26,
            this.layoutControlItem28,
            this.layoutControlItem35,
            this.layoutControlItem61,
            this.layoutControlItem153,
            this.layoutControlItem154,
            this.layoutControlItem155,
            this.layoutControlItem7,
            this.layoutControlItem33,
            this.layoutControlItem19,
            this.layoutControlItem20,
            this.layoutControlItem118,
            this.layoutControlItem21,
            this.layoutControlItem22,
            this.layoutControlItem23,
            this.layoutControlItem24,
            this.layoutControlItem34,
            this.layoutControlItem36,
            this.layoutControlItem156});
            this.layoutControlGroup7.Name = "layoutControlGroup7";
            this.layoutControlGroup7.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.layoutControlGroup7.Size = new System.Drawing.Size(477, 401);
            this.layoutControlGroup7.TextVisible = false;
            // 
            // layoutControlItem27
            // 
            this.layoutControlItem27.Control = this.checkButtonMenualMode;
            this.layoutControlItem27.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem27.Name = "layoutControlItem27";
            this.layoutControlItem27.Size = new System.Drawing.Size(473, 26);
            this.layoutControlItem27.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem27.TextVisible = false;
            // 
            // layoutControlItem26
            // 
            this.layoutControlItem26.Control = this.radioGroupMenualMode;
            this.layoutControlItem26.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem26.Name = "layoutControlItem26";
            this.layoutControlItem26.Size = new System.Drawing.Size(167, 29);
            this.layoutControlItem26.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem26.TextVisible = false;
            // 
            // layoutControlItem28
            // 
            this.layoutControlItem28.Control = this.radioGroupMenualControlMode;
            this.layoutControlItem28.Location = new System.Drawing.Point(167, 52);
            this.layoutControlItem28.Name = "layoutControlItem28";
            this.layoutControlItem28.Size = new System.Drawing.Size(306, 29);
            this.layoutControlItem28.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem28.TextVisible = false;
            // 
            // layoutControlItem35
            // 
            this.layoutControlItem35.Control = this.layoutControl8;
            this.layoutControlItem35.Location = new System.Drawing.Point(0, 81);
            this.layoutControlItem35.MinSize = new System.Drawing.Size(50, 29);
            this.layoutControlItem35.Name = "layoutControlItem35";
            this.layoutControlItem35.Size = new System.Drawing.Size(473, 213);
            this.layoutControlItem35.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem35.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem35.TextVisible = false;
            // 
            // layoutControlItem61
            // 
            this.layoutControlItem61.Control = this.SendCmdPositionButton;
            this.layoutControlItem61.Location = new System.Drawing.Point(315, 371);
            this.layoutControlItem61.Name = "layoutControlItem61";
            this.layoutControlItem61.Size = new System.Drawing.Size(78, 26);
            this.layoutControlItem61.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem61.TextVisible = false;
            // 
            // layoutControlItem153
            // 
            this.layoutControlItem153.Control = this.RobotEnableButton;
            this.layoutControlItem153.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem153.Name = "layoutControlItem153";
            this.layoutControlItem153.Size = new System.Drawing.Size(118, 26);
            this.layoutControlItem153.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem153.TextVisible = false;
            // 
            // layoutControlItem154
            // 
            this.layoutControlItem154.Control = this.EmergencyStopButton;
            this.layoutControlItem154.Location = new System.Drawing.Point(236, 0);
            this.layoutControlItem154.Name = "layoutControlItem154";
            this.layoutControlItem154.Size = new System.Drawing.Size(117, 26);
            this.layoutControlItem154.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem154.TextVisible = false;
            // 
            // layoutControlItem155
            // 
            this.layoutControlItem155.Control = this.ErrorResetButton;
            this.layoutControlItem155.Location = new System.Drawing.Point(353, 0);
            this.layoutControlItem155.Name = "layoutControlItem155";
            this.layoutControlItem155.Size = new System.Drawing.Size(120, 26);
            this.layoutControlItem155.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem155.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.SendCommandMoveStopButton;
            this.layoutControlItem7.Location = new System.Drawing.Point(393, 371);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(80, 26);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem33
            // 
            this.layoutControlItem33.Control = this.SendCmdHommingButton;
            this.layoutControlItem33.Location = new System.Drawing.Point(118, 0);
            this.layoutControlItem33.Name = "layoutControlItem33";
            this.layoutControlItem33.Size = new System.Drawing.Size(118, 26);
            this.layoutControlItem33.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem33.TextVisible = false;
            // 
            // layoutControlItem19
            // 
            this.layoutControlItem19.Control = this.textEditTargetPosX;
            this.layoutControlItem19.Location = new System.Drawing.Point(0, 323);
            this.layoutControlItem19.Name = "layoutControlItem19";
            this.layoutControlItem19.Size = new System.Drawing.Size(158, 24);
            this.layoutControlItem19.Text = "목표 위치(X)";
            this.layoutControlItem19.TextSize = new System.Drawing.Size(69, 14);
            // 
            // layoutControlItem20
            // 
            this.layoutControlItem20.Control = this.textEditTargetPosY1;
            this.layoutControlItem20.Location = new System.Drawing.Point(158, 323);
            this.layoutControlItem20.Name = "layoutControlItem20";
            this.layoutControlItem20.Size = new System.Drawing.Size(157, 24);
            this.layoutControlItem20.Text = "목표 위치(Y1)";
            this.layoutControlItem20.TextSize = new System.Drawing.Size(69, 14);
            // 
            // layoutControlItem118
            // 
            this.layoutControlItem118.Control = this.textEditTargetPosY2;
            this.layoutControlItem118.Location = new System.Drawing.Point(315, 323);
            this.layoutControlItem118.Name = "layoutControlItem118";
            this.layoutControlItem118.Size = new System.Drawing.Size(158, 24);
            this.layoutControlItem118.Text = "목표 위치(Y2)";
            this.layoutControlItem118.TextSize = new System.Drawing.Size(69, 14);
            // 
            // layoutControlItem21
            // 
            this.layoutControlItem21.Control = this.textEditTargetPosZ;
            this.layoutControlItem21.Location = new System.Drawing.Point(0, 347);
            this.layoutControlItem21.Name = "layoutControlItem21";
            this.layoutControlItem21.Size = new System.Drawing.Size(158, 24);
            this.layoutControlItem21.Text = "목표 위치(Z)";
            this.layoutControlItem21.TextSize = new System.Drawing.Size(69, 14);
            // 
            // layoutControlItem22
            // 
            this.layoutControlItem22.Control = this.textEditTargetPosFZ;
            this.layoutControlItem22.Location = new System.Drawing.Point(158, 347);
            this.layoutControlItem22.Name = "layoutControlItem22";
            this.layoutControlItem22.Size = new System.Drawing.Size(157, 24);
            this.layoutControlItem22.Text = "목표 위치(FZ)";
            this.layoutControlItem22.TextSize = new System.Drawing.Size(69, 14);
            // 
            // layoutControlItem23
            // 
            this.layoutControlItem23.Control = this.textEditTargetPosFR;
            this.layoutControlItem23.Location = new System.Drawing.Point(315, 347);
            this.layoutControlItem23.Name = "layoutControlItem23";
            this.layoutControlItem23.Size = new System.Drawing.Size(158, 24);
            this.layoutControlItem23.Text = "목표 위치(FR)";
            this.layoutControlItem23.TextSize = new System.Drawing.Size(69, 14);
            // 
            // layoutControlItem24
            // 
            this.layoutControlItem24.Control = this.textEditTargetVelocity;
            this.layoutControlItem24.Location = new System.Drawing.Point(0, 371);
            this.layoutControlItem24.Name = "layoutControlItem24";
            this.layoutControlItem24.Size = new System.Drawing.Size(158, 26);
            this.layoutControlItem24.Text = "목표 속도";
            this.layoutControlItem24.TextSize = new System.Drawing.Size(69, 14);
            // 
            // layoutControlItem34
            // 
            this.layoutControlItem34.Control = this.textEditTargetAcceleration;
            this.layoutControlItem34.Location = new System.Drawing.Point(158, 371);
            this.layoutControlItem34.Name = "layoutControlItem34";
            this.layoutControlItem34.Size = new System.Drawing.Size(157, 26);
            this.layoutControlItem34.Text = "목표 가속도";
            this.layoutControlItem34.TextSize = new System.Drawing.Size(69, 14);
            // 
            // layoutControlItem36
            // 
            this.layoutControlItem36.Control = this.checkEditCalibration;
            this.layoutControlItem36.Location = new System.Drawing.Point(0, 294);
            this.layoutControlItem36.Name = "layoutControlItem36";
            this.layoutControlItem36.Size = new System.Drawing.Size(179, 29);
            this.layoutControlItem36.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem36.TextVisible = false;
            // 
            // layoutControlItem156
            // 
            this.layoutControlItem156.Control = this.radioGroupCalibration;
            this.layoutControlItem156.Location = new System.Drawing.Point(179, 294);
            this.layoutControlItem156.Name = "layoutControlItem156";
            this.layoutControlItem156.Size = new System.Drawing.Size(294, 29);
            this.layoutControlItem156.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem156.TextVisible = false;
            // 
            // layoutControlGroup5
            // 
            this.layoutControlGroup5.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup5.GroupBordersVisible = false;
            this.layoutControlGroup5.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem18});
            this.layoutControlGroup5.Name = "layoutControlGroup5";
            this.layoutControlGroup5.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 4, 5, 5);
            this.layoutControlGroup5.Size = new System.Drawing.Size(494, 482);
            this.layoutControlGroup5.TextVisible = false;
            // 
            // layoutControlItem18
            // 
            this.layoutControlItem18.Control = this.groupControlPCLControl;
            this.layoutControlItem18.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem18.Name = "layoutControlItem18";
            this.layoutControlItem18.Size = new System.Drawing.Size(486, 472);
            this.layoutControlItem18.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem18.TextVisible = false;
            // 
            // layoutControl4
            // 
            this.layoutControl4.Controls.Add(this.groupControlPresentPosition);
            this.layoutControl4.Location = new System.Drawing.Point(12, 12);
            this.layoutControl4.Name = "layoutControl4";
            this.layoutControl4.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl4.Root = this.layoutControlGroup4;
            this.layoutControl4.Size = new System.Drawing.Size(494, 121);
            this.layoutControl4.TabIndex = 4;
            this.layoutControl4.Text = "layoutControl4";
            // 
            // groupControlPresentPosition
            // 
            this.groupControlPresentPosition.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupControlPresentPosition.Controls.Add(this.layoutControl7);
            this.groupControlPresentPosition.Location = new System.Drawing.Point(6, 7);
            this.groupControlPresentPosition.Name = "groupControlPresentPosition";
            this.groupControlPresentPosition.Size = new System.Drawing.Size(482, 107);
            this.groupControlPresentPosition.TabIndex = 5;
            this.groupControlPresentPosition.Text = "모션 현재 위치";
            // 
            // layoutControl7
            // 
            this.layoutControl7.Controls.Add(this.textEditPresentPosFR);
            this.layoutControl7.Controls.Add(this.textEditPresentPosFZ);
            this.layoutControl7.Controls.Add(this.textEditPresentPosZ);
            this.layoutControl7.Controls.Add(this.textEditPresentPosY2);
            this.layoutControl7.Controls.Add(this.textEditPresentPosY1);
            this.layoutControl7.Controls.Add(this.textEditPresentPosX);
            this.layoutControl7.Location = new System.Drawing.Point(2, 26);
            this.layoutControl7.Name = "layoutControl7";
            this.layoutControl7.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl7.Root = this.layoutControlGroup8;
            this.layoutControl7.Size = new System.Drawing.Size(477, 77);
            this.layoutControl7.TabIndex = 0;
            this.layoutControl7.Text = "layoutControl7";
            // 
            // textEditPresentPosFR
            // 
            this.textEditPresentPosFR.Location = new System.Drawing.Point(388, 36);
            this.textEditPresentPosFR.Name = "textEditPresentPosFR";
            this.textEditPresentPosFR.Properties.ReadOnly = true;
            this.textEditPresentPosFR.Size = new System.Drawing.Size(77, 20);
            this.textEditPresentPosFR.StyleController = this.layoutControl7;
            this.textEditPresentPosFR.TabIndex = 15;
            // 
            // textEditPresentPosFZ
            // 
            this.textEditPresentPosFZ.Location = new System.Drawing.Point(236, 36);
            this.textEditPresentPosFZ.Name = "textEditPresentPosFZ";
            this.textEditPresentPosFZ.Properties.ReadOnly = true;
            this.textEditPresentPosFZ.Size = new System.Drawing.Size(76, 20);
            this.textEditPresentPosFZ.StyleController = this.layoutControl7;
            this.textEditPresentPosFZ.TabIndex = 14;
            // 
            // textEditPresentPosZ
            // 
            this.textEditPresentPosZ.Location = new System.Drawing.Point(84, 36);
            this.textEditPresentPosZ.Name = "textEditPresentPosZ";
            this.textEditPresentPosZ.Properties.ReadOnly = true;
            this.textEditPresentPosZ.Size = new System.Drawing.Size(76, 20);
            this.textEditPresentPosZ.StyleController = this.layoutControl7;
            this.textEditPresentPosZ.TabIndex = 13;
            // 
            // textEditPresentPosY2
            // 
            this.textEditPresentPosY2.Location = new System.Drawing.Point(388, 12);
            this.textEditPresentPosY2.Name = "textEditPresentPosY2";
            this.textEditPresentPosY2.Properties.ReadOnly = true;
            this.textEditPresentPosY2.Size = new System.Drawing.Size(77, 20);
            this.textEditPresentPosY2.StyleController = this.layoutControl7;
            this.textEditPresentPosY2.TabIndex = 12;
            // 
            // textEditPresentPosY1
            // 
            this.textEditPresentPosY1.Location = new System.Drawing.Point(236, 12);
            this.textEditPresentPosY1.Name = "textEditPresentPosY1";
            this.textEditPresentPosY1.Properties.ReadOnly = true;
            this.textEditPresentPosY1.Size = new System.Drawing.Size(76, 20);
            this.textEditPresentPosY1.StyleController = this.layoutControl7;
            this.textEditPresentPosY1.TabIndex = 11;
            // 
            // textEditPresentPosX
            // 
            this.textEditPresentPosX.Location = new System.Drawing.Point(84, 12);
            this.textEditPresentPosX.Name = "textEditPresentPosX";
            this.textEditPresentPosX.Properties.ReadOnly = true;
            this.textEditPresentPosX.Size = new System.Drawing.Size(76, 20);
            this.textEditPresentPosX.StyleController = this.layoutControl7;
            this.textEditPresentPosX.TabIndex = 10;
            // 
            // layoutControlGroup8
            // 
            this.layoutControlGroup8.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup8.GroupBordersVisible = false;
            this.layoutControlGroup8.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem12,
            this.layoutControlItem13,
            this.layoutControlItem14,
            this.layoutControlItem15,
            this.layoutControlItem16,
            this.layoutControlItem17});
            this.layoutControlGroup8.Name = "layoutControlGroup7";
            this.layoutControlGroup8.Size = new System.Drawing.Size(477, 77);
            this.layoutControlGroup8.TextVisible = false;
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.textEditPresentPosX;
            this.layoutControlItem12.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(152, 24);
            this.layoutControlItem12.Text = "현재 위치(X)";
            this.layoutControlItem12.TextSize = new System.Drawing.Size(69, 14);
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.Control = this.textEditPresentPosY1;
            this.layoutControlItem13.Location = new System.Drawing.Point(152, 0);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Size = new System.Drawing.Size(152, 24);
            this.layoutControlItem13.Text = "현재 위치(Y1)";
            this.layoutControlItem13.TextSize = new System.Drawing.Size(69, 14);
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.Control = this.textEditPresentPosY2;
            this.layoutControlItem14.Location = new System.Drawing.Point(304, 0);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Size = new System.Drawing.Size(153, 24);
            this.layoutControlItem14.Text = "현재 위치(Y2)";
            this.layoutControlItem14.TextSize = new System.Drawing.Size(69, 14);
            // 
            // layoutControlItem15
            // 
            this.layoutControlItem15.Control = this.textEditPresentPosZ;
            this.layoutControlItem15.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem15.Name = "layoutControlItem15";
            this.layoutControlItem15.Size = new System.Drawing.Size(152, 33);
            this.layoutControlItem15.Text = "현재 위치(Z)";
            this.layoutControlItem15.TextSize = new System.Drawing.Size(69, 14);
            // 
            // layoutControlItem16
            // 
            this.layoutControlItem16.Control = this.textEditPresentPosFZ;
            this.layoutControlItem16.Location = new System.Drawing.Point(152, 24);
            this.layoutControlItem16.Name = "layoutControlItem16";
            this.layoutControlItem16.Size = new System.Drawing.Size(152, 33);
            this.layoutControlItem16.Text = "현재 위치(FZ)";
            this.layoutControlItem16.TextSize = new System.Drawing.Size(69, 14);
            // 
            // layoutControlItem17
            // 
            this.layoutControlItem17.Control = this.textEditPresentPosFR;
            this.layoutControlItem17.Location = new System.Drawing.Point(304, 24);
            this.layoutControlItem17.Name = "layoutControlItem17";
            this.layoutControlItem17.Size = new System.Drawing.Size(153, 33);
            this.layoutControlItem17.Text = "현재 위치(FR)";
            this.layoutControlItem17.TextSize = new System.Drawing.Size(69, 14);
            // 
            // layoutControlGroup4
            // 
            this.layoutControlGroup4.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup4.GroupBordersVisible = false;
            this.layoutControlGroup4.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem25});
            this.layoutControlGroup4.Name = "Root";
            this.layoutControlGroup4.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 4, 5, 5);
            this.layoutControlGroup4.Size = new System.Drawing.Size(494, 121);
            this.layoutControlGroup4.TextVisible = false;
            // 
            // layoutControlItem25
            // 
            this.layoutControlItem25.Control = this.groupControlPresentPosition;
            this.layoutControlItem25.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem25.Name = "layoutControlItem25";
            this.layoutControlItem25.Size = new System.Drawing.Size(486, 111);
            this.layoutControlItem25.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem25.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem10,
            this.layoutControlItem11});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(518, 631);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.layoutControl4;
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(498, 125);
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextVisible = false;
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.layoutControl5;
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 125);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(498, 486);
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextVisible = false;
            // 
            // xtraTabStatusPage
            // 
            this.xtraTabStatusPage.Controls.Add(this.layoutControl9);
            this.xtraTabStatusPage.Margin = new System.Windows.Forms.Padding(0);
            this.xtraTabStatusPage.Name = "xtraTabStatusPage";
            this.xtraTabStatusPage.Size = new System.Drawing.Size(518, 631);
            this.xtraTabStatusPage.Text = "상태정보";
            // 
            // layoutControl9
            // 
            this.layoutControl9.Controls.Add(this.layoutControl10);
            this.layoutControl9.Location = new System.Drawing.Point(4, 5);
            this.layoutControl9.Margin = new System.Windows.Forms.Padding(10, 12, 10, 12);
            this.layoutControl9.Name = "layoutControl9";
            this.layoutControl9.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl9.Root = this.layoutControlGroup10;
            this.layoutControl9.Size = new System.Drawing.Size(513, 619);
            this.layoutControl9.TabIndex = 0;
            this.layoutControl9.Text = "layoutControl9";
            // 
            // layoutControl10
            // 
            this.layoutControl10.Controls.Add(this.groupControl2);
            this.layoutControl10.Controls.Add(this.groupControl1);
            this.layoutControl10.Location = new System.Drawing.Point(10, 10);
            this.layoutControl10.Name = "layoutControl10";
            this.layoutControl10.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl10.Root = this.layoutControlGroup11;
            this.layoutControl10.Size = new System.Drawing.Size(493, 599);
            this.layoutControl10.TabIndex = 4;
            this.layoutControl10.Text = "layoutControl10";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.layoutControl14);
            this.groupControl2.Location = new System.Drawing.Point(6, 351);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(481, 241);
            this.groupControl2.TabIndex = 5;
            this.groupControl2.Text = "PLC I/O 정보";
            // 
            // layoutControl14
            // 
            this.layoutControl14.Controls.Add(this.layoutControl15);
            this.layoutControl14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl14.Location = new System.Drawing.Point(2, 21);
            this.layoutControl14.Name = "layoutControl14";
            this.layoutControl14.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl14.Root = this.layoutControlGroup15;
            this.layoutControl14.Size = new System.Drawing.Size(477, 218);
            this.layoutControl14.TabIndex = 0;
            this.layoutControl14.Text = "layoutControl14";
            // 
            // layoutControl15
            // 
            this.layoutControl15.Controls.Add(this.groupControl6);
            this.layoutControl15.Controls.Add(this.groupControl5);
            this.layoutControl15.Location = new System.Drawing.Point(12, 12);
            this.layoutControl15.Name = "layoutControl15";
            this.layoutControl15.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl15.Root = this.layoutControlGroup16;
            this.layoutControl15.Size = new System.Drawing.Size(453, 194);
            this.layoutControl15.TabIndex = 4;
            this.layoutControl15.Text = "layoutControl15";
            // 
            // groupControl6
            // 
            this.groupControl6.Controls.Add(this.layoutControl17);
            this.groupControl6.Location = new System.Drawing.Point(4, 4);
            this.groupControl6.Name = "groupControl6";
            this.groupControl6.Size = new System.Drawing.Size(445, 89);
            this.groupControl6.TabIndex = 9;
            this.groupControl6.Text = "Digital Input";
            // 
            // layoutControl17
            // 
            this.layoutControl17.Controls.Add(this.labelControlDIn16);
            this.layoutControl17.Controls.Add(this.labelControlDIn15);
            this.layoutControl17.Controls.Add(this.labelControlDIn14);
            this.layoutControl17.Controls.Add(this.labelControlDIn12);
            this.layoutControl17.Controls.Add(this.labelControlDIn11);
            this.layoutControl17.Controls.Add(this.labelControlDIn10);
            this.layoutControl17.Controls.Add(this.labelControlDIn8);
            this.layoutControl17.Controls.Add(this.labelControlDIn7);
            this.layoutControl17.Controls.Add(this.labelControlDIn6);
            this.layoutControl17.Controls.Add(this.labelControlDIn13);
            this.layoutControl17.Controls.Add(this.labelControlDIn9);
            this.layoutControl17.Controls.Add(this.labelControlDIn5);
            this.layoutControl17.Controls.Add(this.labelControlDIn4);
            this.layoutControl17.Controls.Add(this.labelControlDIn3);
            this.layoutControl17.Controls.Add(this.labelControlDIn2);
            this.layoutControl17.Controls.Add(this.labelControlDIn1);
            this.layoutControl17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl17.Location = new System.Drawing.Point(2, 21);
            this.layoutControl17.Name = "layoutControl17";
            this.layoutControl17.Root = this.layoutControlGroup18;
            this.layoutControl17.Size = new System.Drawing.Size(441, 66);
            this.layoutControl17.TabIndex = 0;
            this.layoutControl17.Text = "layoutControl17";
            // 
            // labelControlDIn16
            // 
            this.labelControlDIn16.Appearance.Options.UseTextOptions = true;
            this.labelControlDIn16.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlDIn16.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlDIn16.Location = new System.Drawing.Point(384, 34);
            this.labelControlDIn16.Name = "labelControlDIn16";
            this.labelControlDIn16.Size = new System.Drawing.Size(53, 28);
            this.labelControlDIn16.StyleController = this.layoutControl17;
            this.labelControlDIn16.TabIndex = 23;
            this.labelControlDIn16.Text = "16";
            // 
            // labelControlDIn15
            // 
            this.labelControlDIn15.Appearance.Options.UseTextOptions = true;
            this.labelControlDIn15.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlDIn15.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlDIn15.Location = new System.Drawing.Point(329, 34);
            this.labelControlDIn15.Name = "labelControlDIn15";
            this.labelControlDIn15.Size = new System.Drawing.Size(51, 28);
            this.labelControlDIn15.StyleController = this.layoutControl17;
            this.labelControlDIn15.TabIndex = 22;
            this.labelControlDIn15.Text = "15";
            // 
            // labelControlDIn14
            // 
            this.labelControlDIn14.Appearance.Options.UseTextOptions = true;
            this.labelControlDIn14.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlDIn14.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlDIn14.Location = new System.Drawing.Point(274, 34);
            this.labelControlDIn14.Name = "labelControlDIn14";
            this.labelControlDIn14.Size = new System.Drawing.Size(51, 28);
            this.labelControlDIn14.StyleController = this.layoutControl17;
            this.labelControlDIn14.TabIndex = 21;
            this.labelControlDIn14.Text = "14";
            // 
            // labelControlDIn12
            // 
            this.labelControlDIn12.Appearance.Options.UseTextOptions = true;
            this.labelControlDIn12.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlDIn12.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlDIn12.Location = new System.Drawing.Point(166, 34);
            this.labelControlDIn12.Name = "labelControlDIn12";
            this.labelControlDIn12.Size = new System.Drawing.Size(51, 28);
            this.labelControlDIn12.StyleController = this.layoutControl17;
            this.labelControlDIn12.TabIndex = 20;
            this.labelControlDIn12.Text = "12";
            // 
            // labelControlDIn11
            // 
            this.labelControlDIn11.Appearance.Options.UseTextOptions = true;
            this.labelControlDIn11.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlDIn11.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlDIn11.Location = new System.Drawing.Point(113, 34);
            this.labelControlDIn11.Name = "labelControlDIn11";
            this.labelControlDIn11.Size = new System.Drawing.Size(49, 28);
            this.labelControlDIn11.StyleController = this.layoutControl17;
            this.labelControlDIn11.TabIndex = 19;
            this.labelControlDIn11.Text = "11";
            // 
            // labelControlDIn10
            // 
            this.labelControlDIn10.Appearance.Options.UseTextOptions = true;
            this.labelControlDIn10.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlDIn10.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlDIn10.Location = new System.Drawing.Point(58, 34);
            this.labelControlDIn10.Name = "labelControlDIn10";
            this.labelControlDIn10.Size = new System.Drawing.Size(51, 28);
            this.labelControlDIn10.StyleController = this.layoutControl17;
            this.labelControlDIn10.TabIndex = 18;
            this.labelControlDIn10.Text = "10";
            // 
            // labelControlDIn8
            // 
            this.labelControlDIn8.Appearance.Options.UseTextOptions = true;
            this.labelControlDIn8.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlDIn8.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlDIn8.Location = new System.Drawing.Point(384, 4);
            this.labelControlDIn8.Name = "labelControlDIn8";
            this.labelControlDIn8.Size = new System.Drawing.Size(53, 26);
            this.labelControlDIn8.StyleController = this.layoutControl17;
            this.labelControlDIn8.TabIndex = 17;
            this.labelControlDIn8.Text = "8";
            // 
            // labelControlDIn7
            // 
            this.labelControlDIn7.Appearance.Options.UseTextOptions = true;
            this.labelControlDIn7.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlDIn7.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlDIn7.Location = new System.Drawing.Point(329, 4);
            this.labelControlDIn7.Name = "labelControlDIn7";
            this.labelControlDIn7.Size = new System.Drawing.Size(51, 26);
            this.labelControlDIn7.StyleController = this.layoutControl17;
            this.labelControlDIn7.TabIndex = 16;
            this.labelControlDIn7.Text = "7";
            // 
            // labelControlDIn6
            // 
            this.labelControlDIn6.Appearance.Options.UseTextOptions = true;
            this.labelControlDIn6.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlDIn6.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlDIn6.Location = new System.Drawing.Point(274, 4);
            this.labelControlDIn6.Name = "labelControlDIn6";
            this.labelControlDIn6.Size = new System.Drawing.Size(51, 26);
            this.labelControlDIn6.StyleController = this.layoutControl17;
            this.labelControlDIn6.TabIndex = 15;
            this.labelControlDIn6.Text = "6";
            // 
            // labelControlDIn13
            // 
            this.labelControlDIn13.Appearance.Options.UseTextOptions = true;
            this.labelControlDIn13.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlDIn13.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlDIn13.Location = new System.Drawing.Point(221, 34);
            this.labelControlDIn13.Name = "labelControlDIn13";
            this.labelControlDIn13.Size = new System.Drawing.Size(49, 28);
            this.labelControlDIn13.StyleController = this.layoutControl17;
            this.labelControlDIn13.TabIndex = 10;
            this.labelControlDIn13.Text = "13";
            // 
            // labelControlDIn9
            // 
            this.labelControlDIn9.Appearance.Options.UseTextOptions = true;
            this.labelControlDIn9.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlDIn9.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlDIn9.Location = new System.Drawing.Point(4, 34);
            this.labelControlDIn9.Name = "labelControlDIn9";
            this.labelControlDIn9.Size = new System.Drawing.Size(50, 28);
            this.labelControlDIn9.StyleController = this.layoutControl17;
            this.labelControlDIn9.TabIndex = 9;
            this.labelControlDIn9.Text = "9";
            // 
            // labelControlDIn5
            // 
            this.labelControlDIn5.Appearance.Options.UseTextOptions = true;
            this.labelControlDIn5.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlDIn5.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlDIn5.Location = new System.Drawing.Point(221, 4);
            this.labelControlDIn5.Name = "labelControlDIn5";
            this.labelControlDIn5.Size = new System.Drawing.Size(49, 26);
            this.labelControlDIn5.StyleController = this.layoutControl17;
            this.labelControlDIn5.TabIndex = 8;
            this.labelControlDIn5.Text = "5";
            // 
            // labelControlDIn4
            // 
            this.labelControlDIn4.Appearance.Options.UseTextOptions = true;
            this.labelControlDIn4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlDIn4.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlDIn4.Location = new System.Drawing.Point(166, 4);
            this.labelControlDIn4.Name = "labelControlDIn4";
            this.labelControlDIn4.Size = new System.Drawing.Size(51, 26);
            this.labelControlDIn4.StyleController = this.layoutControl17;
            this.labelControlDIn4.TabIndex = 7;
            this.labelControlDIn4.Text = "4";
            // 
            // labelControlDIn3
            // 
            this.labelControlDIn3.Appearance.Options.UseTextOptions = true;
            this.labelControlDIn3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlDIn3.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlDIn3.Location = new System.Drawing.Point(113, 4);
            this.labelControlDIn3.Name = "labelControlDIn3";
            this.labelControlDIn3.Size = new System.Drawing.Size(49, 26);
            this.labelControlDIn3.StyleController = this.layoutControl17;
            this.labelControlDIn3.TabIndex = 6;
            this.labelControlDIn3.Text = "3";
            // 
            // labelControlDIn2
            // 
            this.labelControlDIn2.Appearance.Options.UseTextOptions = true;
            this.labelControlDIn2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlDIn2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlDIn2.Location = new System.Drawing.Point(58, 4);
            this.labelControlDIn2.Name = "labelControlDIn2";
            this.labelControlDIn2.Size = new System.Drawing.Size(51, 26);
            this.labelControlDIn2.StyleController = this.layoutControl17;
            this.labelControlDIn2.TabIndex = 5;
            this.labelControlDIn2.Text = "2";
            // 
            // labelControlDIn1
            // 
            this.labelControlDIn1.Appearance.Options.UseTextOptions = true;
            this.labelControlDIn1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlDIn1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlDIn1.Location = new System.Drawing.Point(4, 4);
            this.labelControlDIn1.Name = "labelControlDIn1";
            this.labelControlDIn1.Size = new System.Drawing.Size(50, 26);
            this.labelControlDIn1.StyleController = this.layoutControl17;
            this.labelControlDIn1.TabIndex = 4;
            this.labelControlDIn1.Text = "1";
            // 
            // layoutControlGroup18
            // 
            this.layoutControlGroup18.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup18.GroupBordersVisible = false;
            this.layoutControlGroup18.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem119,
            this.layoutControlItem137,
            this.layoutControlItem138,
            this.layoutControlItem139,
            this.layoutControlItem140,
            this.layoutControlItem141,
            this.layoutControlItem142,
            this.layoutControlItem143,
            this.layoutControlItem144,
            this.layoutControlItem145,
            this.layoutControlItem146,
            this.layoutControlItem147,
            this.layoutControlItem148,
            this.layoutControlItem149,
            this.layoutControlItem150,
            this.layoutControlItem151});
            this.layoutControlGroup18.Name = "layoutControlGroup14";
            this.layoutControlGroup18.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.layoutControlGroup18.Size = new System.Drawing.Size(441, 66);
            this.layoutControlGroup18.TextVisible = false;
            // 
            // layoutControlItem119
            // 
            this.layoutControlItem119.Control = this.labelControlDIn1;
            this.layoutControlItem119.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem119.MaxSize = new System.Drawing.Size(80, 58);
            this.layoutControlItem119.MinSize = new System.Drawing.Size(53, 23);
            this.layoutControlItem119.Name = "layoutControlItem85";
            this.layoutControlItem119.Size = new System.Drawing.Size(54, 30);
            this.layoutControlItem119.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem119.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem119.TextVisible = false;
            // 
            // layoutControlItem137
            // 
            this.layoutControlItem137.Control = this.labelControlDIn2;
            this.layoutControlItem137.Location = new System.Drawing.Point(54, 0);
            this.layoutControlItem137.MaxSize = new System.Drawing.Size(80, 58);
            this.layoutControlItem137.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem137.Name = "layoutControlItem86";
            this.layoutControlItem137.Size = new System.Drawing.Size(55, 30);
            this.layoutControlItem137.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem137.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem137.TextVisible = false;
            // 
            // layoutControlItem138
            // 
            this.layoutControlItem138.Control = this.labelControlDIn3;
            this.layoutControlItem138.Location = new System.Drawing.Point(109, 0);
            this.layoutControlItem138.MaxSize = new System.Drawing.Size(80, 58);
            this.layoutControlItem138.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem138.Name = "layoutControlItem87";
            this.layoutControlItem138.Size = new System.Drawing.Size(53, 30);
            this.layoutControlItem138.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem138.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem138.TextVisible = false;
            // 
            // layoutControlItem139
            // 
            this.layoutControlItem139.Control = this.labelControlDIn4;
            this.layoutControlItem139.Location = new System.Drawing.Point(162, 0);
            this.layoutControlItem139.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem139.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem139.Name = "layoutControlItem88";
            this.layoutControlItem139.Size = new System.Drawing.Size(55, 30);
            this.layoutControlItem139.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem139.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem139.TextVisible = false;
            // 
            // layoutControlItem140
            // 
            this.layoutControlItem140.Control = this.labelControlDIn12;
            this.layoutControlItem140.Location = new System.Drawing.Point(162, 30);
            this.layoutControlItem140.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem140.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem140.Name = "layoutControlItem101";
            this.layoutControlItem140.Size = new System.Drawing.Size(55, 32);
            this.layoutControlItem140.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem140.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem140.TextVisible = false;
            // 
            // layoutControlItem141
            // 
            this.layoutControlItem141.Control = this.labelControlDIn9;
            this.layoutControlItem141.Location = new System.Drawing.Point(0, 30);
            this.layoutControlItem141.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem141.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem141.Name = "layoutControlItem90";
            this.layoutControlItem141.Size = new System.Drawing.Size(54, 32);
            this.layoutControlItem141.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem141.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem141.TextVisible = false;
            // 
            // layoutControlItem142
            // 
            this.layoutControlItem142.Control = this.labelControlDIn10;
            this.layoutControlItem142.Location = new System.Drawing.Point(54, 30);
            this.layoutControlItem142.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem142.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem142.Name = "layoutControlItem99";
            this.layoutControlItem142.Size = new System.Drawing.Size(55, 32);
            this.layoutControlItem142.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem142.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem142.TextVisible = false;
            // 
            // layoutControlItem143
            // 
            this.layoutControlItem143.Control = this.labelControlDIn11;
            this.layoutControlItem143.Location = new System.Drawing.Point(109, 30);
            this.layoutControlItem143.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem143.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem143.Name = "layoutControlItem100";
            this.layoutControlItem143.Size = new System.Drawing.Size(53, 32);
            this.layoutControlItem143.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem143.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem143.TextVisible = false;
            // 
            // layoutControlItem144
            // 
            this.layoutControlItem144.Control = this.labelControlDIn5;
            this.layoutControlItem144.Location = new System.Drawing.Point(217, 0);
            this.layoutControlItem144.MaxSize = new System.Drawing.Size(80, 58);
            this.layoutControlItem144.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem144.Name = "layoutControlItem89";
            this.layoutControlItem144.Size = new System.Drawing.Size(53, 30);
            this.layoutControlItem144.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem144.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem144.TextVisible = false;
            // 
            // layoutControlItem145
            // 
            this.layoutControlItem145.Control = this.labelControlDIn6;
            this.layoutControlItem145.Location = new System.Drawing.Point(270, 0);
            this.layoutControlItem145.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem145.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem145.Name = "layoutControlItem96";
            this.layoutControlItem145.Size = new System.Drawing.Size(55, 30);
            this.layoutControlItem145.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem145.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem145.TextVisible = false;
            // 
            // layoutControlItem146
            // 
            this.layoutControlItem146.Control = this.labelControlDIn7;
            this.layoutControlItem146.Location = new System.Drawing.Point(325, 0);
            this.layoutControlItem146.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem146.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem146.Name = "layoutControlItem97";
            this.layoutControlItem146.Size = new System.Drawing.Size(55, 30);
            this.layoutControlItem146.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem146.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem146.TextVisible = false;
            // 
            // layoutControlItem147
            // 
            this.layoutControlItem147.Control = this.labelControlDIn8;
            this.layoutControlItem147.Location = new System.Drawing.Point(380, 0);
            this.layoutControlItem147.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem147.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem147.Name = "layoutControlItem98";
            this.layoutControlItem147.Size = new System.Drawing.Size(57, 30);
            this.layoutControlItem147.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem147.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem147.TextVisible = false;
            // 
            // layoutControlItem148
            // 
            this.layoutControlItem148.Control = this.labelControlDIn13;
            this.layoutControlItem148.Location = new System.Drawing.Point(217, 30);
            this.layoutControlItem148.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem148.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem148.Name = "layoutControlItem91";
            this.layoutControlItem148.Size = new System.Drawing.Size(53, 32);
            this.layoutControlItem148.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem148.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem148.TextVisible = false;
            // 
            // layoutControlItem149
            // 
            this.layoutControlItem149.Control = this.labelControlDIn14;
            this.layoutControlItem149.Location = new System.Drawing.Point(270, 30);
            this.layoutControlItem149.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem149.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem149.Name = "layoutControlItem102";
            this.layoutControlItem149.Size = new System.Drawing.Size(55, 32);
            this.layoutControlItem149.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem149.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem149.TextVisible = false;
            // 
            // layoutControlItem150
            // 
            this.layoutControlItem150.Control = this.labelControlDIn15;
            this.layoutControlItem150.Location = new System.Drawing.Point(325, 30);
            this.layoutControlItem150.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem150.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem150.Name = "layoutControlItem103";
            this.layoutControlItem150.Size = new System.Drawing.Size(55, 32);
            this.layoutControlItem150.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem150.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem150.TextVisible = false;
            // 
            // layoutControlItem151
            // 
            this.layoutControlItem151.Control = this.labelControlDIn16;
            this.layoutControlItem151.Location = new System.Drawing.Point(380, 30);
            this.layoutControlItem151.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem151.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem151.Name = "layoutControlItem104";
            this.layoutControlItem151.Size = new System.Drawing.Size(57, 32);
            this.layoutControlItem151.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem151.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem151.TextVisible = false;
            // 
            // groupControl5
            // 
            this.groupControl5.Controls.Add(this.layoutControl16);
            this.groupControl5.Location = new System.Drawing.Point(4, 97);
            this.groupControl5.Name = "groupControl5";
            this.groupControl5.Size = new System.Drawing.Size(445, 93);
            this.groupControl5.TabIndex = 8;
            this.groupControl5.Text = "Digital Output";
            // 
            // layoutControl16
            // 
            this.layoutControl16.Controls.Add(this.labelControlDOut16);
            this.layoutControl16.Controls.Add(this.labelControlDOut15);
            this.layoutControl16.Controls.Add(this.labelControlDOut14);
            this.layoutControl16.Controls.Add(this.labelControlDOut12);
            this.layoutControl16.Controls.Add(this.labelControlDOut11);
            this.layoutControl16.Controls.Add(this.labelControlDOut10);
            this.layoutControl16.Controls.Add(this.labelControlDOut8);
            this.layoutControl16.Controls.Add(this.labelControlDOut7);
            this.layoutControl16.Controls.Add(this.labelControlDOut6);
            this.layoutControl16.Controls.Add(this.labelControlDOut13);
            this.layoutControl16.Controls.Add(this.labelControlDOut9);
            this.layoutControl16.Controls.Add(this.labelControlDOut5);
            this.layoutControl16.Controls.Add(this.labelControlDOut4);
            this.layoutControl16.Controls.Add(this.labelControlDOut3);
            this.layoutControl16.Controls.Add(this.labelControlDOut2);
            this.layoutControl16.Controls.Add(this.labelControlDOut1);
            this.layoutControl16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl16.Location = new System.Drawing.Point(2, 21);
            this.layoutControl16.Name = "layoutControl16";
            this.layoutControl16.Root = this.layoutControlGroup17;
            this.layoutControl16.Size = new System.Drawing.Size(441, 70);
            this.layoutControl16.TabIndex = 0;
            this.layoutControl16.Text = "layoutControl16";
            // 
            // labelControlDOut16
            // 
            this.labelControlDOut16.Appearance.Options.UseTextOptions = true;
            this.labelControlDOut16.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlDOut16.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlDOut16.Location = new System.Drawing.Point(384, 35);
            this.labelControlDOut16.Name = "labelControlDOut16";
            this.labelControlDOut16.Size = new System.Drawing.Size(53, 31);
            this.labelControlDOut16.StyleController = this.layoutControl16;
            this.labelControlDOut16.TabIndex = 23;
            this.labelControlDOut16.Text = "16";
            this.labelControlDOut16.Click += new System.EventHandler(this.OutputControl_Click);
            // 
            // labelControlDOut15
            // 
            this.labelControlDOut15.Appearance.Options.UseTextOptions = true;
            this.labelControlDOut15.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlDOut15.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlDOut15.Location = new System.Drawing.Point(329, 35);
            this.labelControlDOut15.Name = "labelControlDOut15";
            this.labelControlDOut15.Size = new System.Drawing.Size(51, 31);
            this.labelControlDOut15.StyleController = this.layoutControl16;
            this.labelControlDOut15.TabIndex = 22;
            this.labelControlDOut15.Text = "15";
            this.labelControlDOut15.Click += new System.EventHandler(this.OutputControl_Click);
            // 
            // labelControlDOut14
            // 
            this.labelControlDOut14.Appearance.Options.UseTextOptions = true;
            this.labelControlDOut14.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlDOut14.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlDOut14.Location = new System.Drawing.Point(275, 35);
            this.labelControlDOut14.Name = "labelControlDOut14";
            this.labelControlDOut14.Size = new System.Drawing.Size(50, 31);
            this.labelControlDOut14.StyleController = this.layoutControl16;
            this.labelControlDOut14.TabIndex = 21;
            this.labelControlDOut14.Text = "14";
            this.labelControlDOut14.Click += new System.EventHandler(this.OutputControl_Click);
            // 
            // labelControlDOut12
            // 
            this.labelControlDOut12.Appearance.Options.UseTextOptions = true;
            this.labelControlDOut12.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlDOut12.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlDOut12.Location = new System.Drawing.Point(166, 35);
            this.labelControlDOut12.Name = "labelControlDOut12";
            this.labelControlDOut12.Size = new System.Drawing.Size(52, 31);
            this.labelControlDOut12.StyleController = this.layoutControl16;
            this.labelControlDOut12.TabIndex = 20;
            this.labelControlDOut12.Text = "12";
            this.labelControlDOut12.Click += new System.EventHandler(this.OutputControl_Click);
            // 
            // labelControlDOut11
            // 
            this.labelControlDOut11.Appearance.Options.UseTextOptions = true;
            this.labelControlDOut11.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlDOut11.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlDOut11.Location = new System.Drawing.Point(113, 35);
            this.labelControlDOut11.Name = "labelControlDOut11";
            this.labelControlDOut11.Size = new System.Drawing.Size(49, 31);
            this.labelControlDOut11.StyleController = this.layoutControl16;
            this.labelControlDOut11.TabIndex = 19;
            this.labelControlDOut11.Text = "11";
            this.labelControlDOut11.Click += new System.EventHandler(this.OutputControl_Click);
            // 
            // labelControlDOut10
            // 
            this.labelControlDOut10.Appearance.Options.UseTextOptions = true;
            this.labelControlDOut10.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlDOut10.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlDOut10.Location = new System.Drawing.Point(58, 35);
            this.labelControlDOut10.Name = "labelControlDOut10";
            this.labelControlDOut10.Size = new System.Drawing.Size(51, 31);
            this.labelControlDOut10.StyleController = this.layoutControl16;
            this.labelControlDOut10.TabIndex = 18;
            this.labelControlDOut10.Text = "10";
            this.labelControlDOut10.Click += new System.EventHandler(this.OutputControl_Click);
            // 
            // labelControlDOut8
            // 
            this.labelControlDOut8.Appearance.Options.UseTextOptions = true;
            this.labelControlDOut8.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlDOut8.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlDOut8.Location = new System.Drawing.Point(384, 4);
            this.labelControlDOut8.Name = "labelControlDOut8";
            this.labelControlDOut8.Size = new System.Drawing.Size(53, 27);
            this.labelControlDOut8.StyleController = this.layoutControl16;
            this.labelControlDOut8.TabIndex = 17;
            this.labelControlDOut8.Text = "8";
            this.labelControlDOut8.Click += new System.EventHandler(this.OutputControl_Click);
            // 
            // labelControlDOut7
            // 
            this.labelControlDOut7.Appearance.Options.UseTextOptions = true;
            this.labelControlDOut7.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlDOut7.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlDOut7.Location = new System.Drawing.Point(329, 4);
            this.labelControlDOut7.Name = "labelControlDOut7";
            this.labelControlDOut7.Size = new System.Drawing.Size(51, 27);
            this.labelControlDOut7.StyleController = this.layoutControl16;
            this.labelControlDOut7.TabIndex = 16;
            this.labelControlDOut7.Text = "7";
            this.labelControlDOut7.Click += new System.EventHandler(this.OutputControl_Click);
            // 
            // labelControlDOut6
            // 
            this.labelControlDOut6.Appearance.Options.UseTextOptions = true;
            this.labelControlDOut6.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlDOut6.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlDOut6.Location = new System.Drawing.Point(275, 4);
            this.labelControlDOut6.Name = "labelControlDOut6";
            this.labelControlDOut6.Size = new System.Drawing.Size(50, 27);
            this.labelControlDOut6.StyleController = this.layoutControl16;
            this.labelControlDOut6.TabIndex = 15;
            this.labelControlDOut6.Text = "6";
            this.labelControlDOut6.Click += new System.EventHandler(this.OutputControl_Click);
            // 
            // labelControlDOut13
            // 
            this.labelControlDOut13.Appearance.Options.UseTextOptions = true;
            this.labelControlDOut13.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlDOut13.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlDOut13.Location = new System.Drawing.Point(222, 35);
            this.labelControlDOut13.Name = "labelControlDOut13";
            this.labelControlDOut13.Size = new System.Drawing.Size(49, 31);
            this.labelControlDOut13.StyleController = this.layoutControl16;
            this.labelControlDOut13.TabIndex = 10;
            this.labelControlDOut13.Text = "13";
            this.labelControlDOut13.Click += new System.EventHandler(this.OutputControl_Click);
            // 
            // labelControlDOut9
            // 
            this.labelControlDOut9.Appearance.Options.UseTextOptions = true;
            this.labelControlDOut9.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlDOut9.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlDOut9.Location = new System.Drawing.Point(4, 35);
            this.labelControlDOut9.Name = "labelControlDOut9";
            this.labelControlDOut9.Size = new System.Drawing.Size(50, 31);
            this.labelControlDOut9.StyleController = this.layoutControl16;
            this.labelControlDOut9.TabIndex = 9;
            this.labelControlDOut9.Text = "9";
            this.labelControlDOut9.Click += new System.EventHandler(this.OutputControl_Click);
            // 
            // labelControlDOut5
            // 
            this.labelControlDOut5.Appearance.Options.UseTextOptions = true;
            this.labelControlDOut5.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlDOut5.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlDOut5.Location = new System.Drawing.Point(222, 4);
            this.labelControlDOut5.Name = "labelControlDOut5";
            this.labelControlDOut5.Size = new System.Drawing.Size(49, 27);
            this.labelControlDOut5.StyleController = this.layoutControl16;
            this.labelControlDOut5.TabIndex = 8;
            this.labelControlDOut5.Text = "5";
            this.labelControlDOut5.Click += new System.EventHandler(this.OutputControl_Click);
            // 
            // labelControlDOut4
            // 
            this.labelControlDOut4.Appearance.Options.UseTextOptions = true;
            this.labelControlDOut4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlDOut4.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlDOut4.Location = new System.Drawing.Point(166, 4);
            this.labelControlDOut4.Name = "labelControlDOut4";
            this.labelControlDOut4.Size = new System.Drawing.Size(52, 27);
            this.labelControlDOut4.StyleController = this.layoutControl16;
            this.labelControlDOut4.TabIndex = 7;
            this.labelControlDOut4.Text = "4";
            this.labelControlDOut4.Click += new System.EventHandler(this.OutputControl_Click);
            // 
            // labelControlDOut3
            // 
            this.labelControlDOut3.Appearance.Options.UseTextOptions = true;
            this.labelControlDOut3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlDOut3.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlDOut3.Location = new System.Drawing.Point(113, 4);
            this.labelControlDOut3.Name = "labelControlDOut3";
            this.labelControlDOut3.Size = new System.Drawing.Size(49, 27);
            this.labelControlDOut3.StyleController = this.layoutControl16;
            this.labelControlDOut3.TabIndex = 6;
            this.labelControlDOut3.Text = "3";
            this.labelControlDOut3.Click += new System.EventHandler(this.OutputControl_Click);
            // 
            // labelControlDOut2
            // 
            this.labelControlDOut2.Appearance.Options.UseTextOptions = true;
            this.labelControlDOut2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlDOut2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlDOut2.Location = new System.Drawing.Point(58, 4);
            this.labelControlDOut2.Name = "labelControlDOut2";
            this.labelControlDOut2.Size = new System.Drawing.Size(51, 27);
            this.labelControlDOut2.StyleController = this.layoutControl16;
            this.labelControlDOut2.TabIndex = 5;
            this.labelControlDOut2.Text = "2";
            this.labelControlDOut2.Click += new System.EventHandler(this.OutputControl_Click);
            // 
            // labelControlDOut1
            // 
            this.labelControlDOut1.Appearance.Options.UseTextOptions = true;
            this.labelControlDOut1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlDOut1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlDOut1.Location = new System.Drawing.Point(4, 4);
            this.labelControlDOut1.Name = "labelControlDOut1";
            this.labelControlDOut1.Size = new System.Drawing.Size(50, 27);
            this.labelControlDOut1.StyleController = this.layoutControl16;
            this.labelControlDOut1.TabIndex = 4;
            this.labelControlDOut1.Text = "1";
            this.labelControlDOut1.Click += new System.EventHandler(this.OutputControl_Click);
            // 
            // layoutControlGroup17
            // 
            this.layoutControlGroup17.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup17.GroupBordersVisible = false;
            this.layoutControlGroup17.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem120,
            this.layoutControlItem121,
            this.layoutControlItem122,
            this.layoutControlItem123,
            this.layoutControlItem130,
            this.layoutControlItem131,
            this.layoutControlItem132,
            this.layoutControlItem133,
            this.layoutControlItem124,
            this.layoutControlItem127,
            this.layoutControlItem128,
            this.layoutControlItem129,
            this.layoutControlItem125,
            this.layoutControlItem134,
            this.layoutControlItem135,
            this.layoutControlItem136});
            this.layoutControlGroup17.Name = "layoutControlGroup14";
            this.layoutControlGroup17.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.layoutControlGroup17.Size = new System.Drawing.Size(441, 70);
            this.layoutControlGroup17.TextVisible = false;
            // 
            // layoutControlItem120
            // 
            this.layoutControlItem120.Control = this.labelControlDOut1;
            this.layoutControlItem120.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem120.MaxSize = new System.Drawing.Size(80, 58);
            this.layoutControlItem120.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem120.Name = "layoutControlItem85";
            this.layoutControlItem120.Size = new System.Drawing.Size(54, 31);
            this.layoutControlItem120.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem120.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem120.TextVisible = false;
            // 
            // layoutControlItem121
            // 
            this.layoutControlItem121.Control = this.labelControlDOut2;
            this.layoutControlItem121.Location = new System.Drawing.Point(54, 0);
            this.layoutControlItem121.MaxSize = new System.Drawing.Size(80, 58);
            this.layoutControlItem121.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem121.Name = "layoutControlItem86";
            this.layoutControlItem121.Size = new System.Drawing.Size(55, 31);
            this.layoutControlItem121.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem121.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem121.TextVisible = false;
            // 
            // layoutControlItem122
            // 
            this.layoutControlItem122.Control = this.labelControlDOut3;
            this.layoutControlItem122.Location = new System.Drawing.Point(109, 0);
            this.layoutControlItem122.MaxSize = new System.Drawing.Size(80, 58);
            this.layoutControlItem122.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem122.Name = "layoutControlItem87";
            this.layoutControlItem122.Size = new System.Drawing.Size(53, 31);
            this.layoutControlItem122.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem122.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem122.TextVisible = false;
            // 
            // layoutControlItem123
            // 
            this.layoutControlItem123.Control = this.labelControlDOut4;
            this.layoutControlItem123.Location = new System.Drawing.Point(162, 0);
            this.layoutControlItem123.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem123.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem123.Name = "layoutControlItem88";
            this.layoutControlItem123.Size = new System.Drawing.Size(56, 31);
            this.layoutControlItem123.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem123.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem123.TextVisible = false;
            // 
            // layoutControlItem130
            // 
            this.layoutControlItem130.Control = this.labelControlDOut12;
            this.layoutControlItem130.Location = new System.Drawing.Point(162, 31);
            this.layoutControlItem130.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem130.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem130.Name = "layoutControlItem101";
            this.layoutControlItem130.Size = new System.Drawing.Size(56, 35);
            this.layoutControlItem130.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem130.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem130.TextVisible = false;
            // 
            // layoutControlItem131
            // 
            this.layoutControlItem131.Control = this.labelControlDOut9;
            this.layoutControlItem131.Location = new System.Drawing.Point(0, 31);
            this.layoutControlItem131.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem131.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem131.Name = "layoutControlItem90";
            this.layoutControlItem131.Size = new System.Drawing.Size(54, 35);
            this.layoutControlItem131.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem131.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem131.TextVisible = false;
            // 
            // layoutControlItem132
            // 
            this.layoutControlItem132.Control = this.labelControlDOut10;
            this.layoutControlItem132.Location = new System.Drawing.Point(54, 31);
            this.layoutControlItem132.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem132.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem132.Name = "layoutControlItem99";
            this.layoutControlItem132.Size = new System.Drawing.Size(55, 35);
            this.layoutControlItem132.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem132.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem132.TextVisible = false;
            // 
            // layoutControlItem133
            // 
            this.layoutControlItem133.Control = this.labelControlDOut11;
            this.layoutControlItem133.Location = new System.Drawing.Point(109, 31);
            this.layoutControlItem133.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem133.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem133.Name = "layoutControlItem100";
            this.layoutControlItem133.Size = new System.Drawing.Size(53, 35);
            this.layoutControlItem133.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem133.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem133.TextVisible = false;
            // 
            // layoutControlItem124
            // 
            this.layoutControlItem124.Control = this.labelControlDOut5;
            this.layoutControlItem124.Location = new System.Drawing.Point(218, 0);
            this.layoutControlItem124.MaxSize = new System.Drawing.Size(80, 58);
            this.layoutControlItem124.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem124.Name = "layoutControlItem89";
            this.layoutControlItem124.Size = new System.Drawing.Size(53, 31);
            this.layoutControlItem124.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem124.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem124.TextVisible = false;
            // 
            // layoutControlItem127
            // 
            this.layoutControlItem127.Control = this.labelControlDOut6;
            this.layoutControlItem127.Location = new System.Drawing.Point(271, 0);
            this.layoutControlItem127.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem127.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem127.Name = "layoutControlItem96";
            this.layoutControlItem127.Size = new System.Drawing.Size(54, 31);
            this.layoutControlItem127.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem127.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem127.TextVisible = false;
            // 
            // layoutControlItem128
            // 
            this.layoutControlItem128.Control = this.labelControlDOut7;
            this.layoutControlItem128.Location = new System.Drawing.Point(325, 0);
            this.layoutControlItem128.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem128.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem128.Name = "layoutControlItem97";
            this.layoutControlItem128.Size = new System.Drawing.Size(55, 31);
            this.layoutControlItem128.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem128.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem128.TextVisible = false;
            // 
            // layoutControlItem129
            // 
            this.layoutControlItem129.Control = this.labelControlDOut8;
            this.layoutControlItem129.Location = new System.Drawing.Point(380, 0);
            this.layoutControlItem129.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem129.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem129.Name = "layoutControlItem98";
            this.layoutControlItem129.Size = new System.Drawing.Size(57, 31);
            this.layoutControlItem129.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem129.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem129.TextVisible = false;
            // 
            // layoutControlItem125
            // 
            this.layoutControlItem125.Control = this.labelControlDOut13;
            this.layoutControlItem125.Location = new System.Drawing.Point(218, 31);
            this.layoutControlItem125.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem125.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem125.Name = "layoutControlItem91";
            this.layoutControlItem125.Size = new System.Drawing.Size(53, 35);
            this.layoutControlItem125.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem125.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem125.TextVisible = false;
            // 
            // layoutControlItem134
            // 
            this.layoutControlItem134.Control = this.labelControlDOut14;
            this.layoutControlItem134.Location = new System.Drawing.Point(271, 31);
            this.layoutControlItem134.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem134.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem134.Name = "layoutControlItem102";
            this.layoutControlItem134.Size = new System.Drawing.Size(54, 35);
            this.layoutControlItem134.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem134.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem134.TextVisible = false;
            // 
            // layoutControlItem135
            // 
            this.layoutControlItem135.Control = this.labelControlDOut15;
            this.layoutControlItem135.Location = new System.Drawing.Point(325, 31);
            this.layoutControlItem135.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem135.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem135.Name = "layoutControlItem103";
            this.layoutControlItem135.Size = new System.Drawing.Size(55, 35);
            this.layoutControlItem135.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem135.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem135.TextVisible = false;
            // 
            // layoutControlItem136
            // 
            this.layoutControlItem136.Control = this.labelControlDOut16;
            this.layoutControlItem136.Location = new System.Drawing.Point(380, 31);
            this.layoutControlItem136.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem136.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem136.Name = "layoutControlItem104";
            this.layoutControlItem136.Size = new System.Drawing.Size(57, 35);
            this.layoutControlItem136.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem136.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem136.TextVisible = false;
            // 
            // layoutControlGroup16
            // 
            this.layoutControlGroup16.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup16.GroupBordersVisible = false;
            this.layoutControlGroup16.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem126,
            this.layoutControlItem152});
            this.layoutControlGroup16.Name = "layoutControlGroup16";
            this.layoutControlGroup16.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.layoutControlGroup16.Size = new System.Drawing.Size(453, 194);
            this.layoutControlGroup16.TextVisible = false;
            // 
            // layoutControlItem126
            // 
            this.layoutControlItem126.Control = this.groupControl5;
            this.layoutControlItem126.Location = new System.Drawing.Point(0, 93);
            this.layoutControlItem126.Name = "layoutControlItem126";
            this.layoutControlItem126.Size = new System.Drawing.Size(449, 97);
            this.layoutControlItem126.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem126.TextVisible = false;
            // 
            // layoutControlItem152
            // 
            this.layoutControlItem152.Control = this.groupControl6;
            this.layoutControlItem152.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem152.Name = "layoutControlItem152";
            this.layoutControlItem152.Size = new System.Drawing.Size(449, 93);
            this.layoutControlItem152.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem152.TextVisible = false;
            // 
            // layoutControlGroup15
            // 
            this.layoutControlGroup15.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup15.GroupBordersVisible = false;
            this.layoutControlGroup15.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem117});
            this.layoutControlGroup15.Name = "layoutControlGroup15";
            this.layoutControlGroup15.Size = new System.Drawing.Size(477, 218);
            this.layoutControlGroup15.TextVisible = false;
            // 
            // layoutControlItem117
            // 
            this.layoutControlItem117.Control = this.layoutControl15;
            this.layoutControlItem117.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem117.Name = "layoutControlItem117";
            this.layoutControlItem117.Size = new System.Drawing.Size(457, 198);
            this.layoutControlItem117.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem117.TextVisible = false;
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.layoutControl11);
            this.groupControl1.Location = new System.Drawing.Point(6, 7);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(481, 340);
            this.groupControl1.TabIndex = 4;
            this.groupControl1.Text = "PLC 상태 정보";
            // 
            // layoutControl11
            // 
            this.layoutControl11.Controls.Add(this.groupControl4);
            this.layoutControl11.Controls.Add(this.groupControl3);
            this.layoutControl11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl11.Location = new System.Drawing.Point(2, 21);
            this.layoutControl11.Name = "layoutControl11";
            this.layoutControl11.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl11.Root = this.layoutControlGroup12;
            this.layoutControl11.Size = new System.Drawing.Size(477, 317);
            this.layoutControl11.TabIndex = 0;
            this.layoutControl11.Text = "layoutControl11";
            // 
            // groupControl4
            // 
            this.groupControl4.Controls.Add(this.layoutControl13);
            this.groupControl4.Location = new System.Drawing.Point(232, 7);
            this.groupControl4.Name = "groupControl4";
            this.groupControl4.Size = new System.Drawing.Size(239, 303);
            this.groupControl4.TabIndex = 5;
            this.groupControl4.Text = "PLC 상태";
            // 
            // layoutControl13
            // 
            this.layoutControl13.Controls.Add(this.labelControlPLCStatus32);
            this.layoutControl13.Controls.Add(this.labelControlPLCStatus31);
            this.layoutControl13.Controls.Add(this.labelControlPLCStatus30);
            this.layoutControl13.Controls.Add(this.labelControlPLCStatus28);
            this.layoutControl13.Controls.Add(this.labelControlPLCStatus27);
            this.layoutControl13.Controls.Add(this.labelControlPLCStatus26);
            this.layoutControl13.Controls.Add(this.labelControlPLCStatus24);
            this.layoutControl13.Controls.Add(this.labelControlPLCStatus23);
            this.layoutControl13.Controls.Add(this.labelControlPLCStatus22);
            this.layoutControl13.Controls.Add(this.labelControlPLCStatus20);
            this.layoutControl13.Controls.Add(this.labelControlPLCStatus19);
            this.layoutControl13.Controls.Add(this.labelControlPLCStatus18);
            this.layoutControl13.Controls.Add(this.labelControlPLCStatus16);
            this.layoutControl13.Controls.Add(this.labelControlPLCStatus15);
            this.layoutControl13.Controls.Add(this.labelControlPLCStatus14);
            this.layoutControl13.Controls.Add(this.labelControlPLCStatus12);
            this.layoutControl13.Controls.Add(this.labelControlPLCStatus11);
            this.layoutControl13.Controls.Add(this.labelControlPLCStatus10);
            this.layoutControl13.Controls.Add(this.labelControlPLCStatus8);
            this.layoutControl13.Controls.Add(this.labelControlPLCStatus7);
            this.layoutControl13.Controls.Add(this.labelControlPLCStatus6);
            this.layoutControl13.Controls.Add(this.labelControlPLCStatus29);
            this.layoutControl13.Controls.Add(this.labelControlPLCStatus25);
            this.layoutControl13.Controls.Add(this.labelControlPLCStatus21);
            this.layoutControl13.Controls.Add(this.labelControlPLCStatus17);
            this.layoutControl13.Controls.Add(this.labelControlPLCStatus13);
            this.layoutControl13.Controls.Add(this.labelControlPLCStatus9);
            this.layoutControl13.Controls.Add(this.labelControlPLCStatus5);
            this.layoutControl13.Controls.Add(this.labelControlPLCStatus4);
            this.layoutControl13.Controls.Add(this.labelControlPLCStatus3);
            this.layoutControl13.Controls.Add(this.labelControlPLCStatus2);
            this.layoutControl13.Controls.Add(this.labelControlPLCStatus1);
            this.layoutControl13.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl13.Location = new System.Drawing.Point(2, 21);
            this.layoutControl13.Name = "layoutControl13";
            this.layoutControl13.Root = this.layoutControlGroup14;
            this.layoutControl13.Size = new System.Drawing.Size(235, 280);
            this.layoutControl13.TabIndex = 0;
            this.layoutControl13.Text = "layoutControl13";
            // 
            // labelControlPLCStatus32
            // 
            this.labelControlPLCStatus32.Appearance.Font = new System.Drawing.Font("Tahoma", 6F);
            this.labelControlPLCStatus32.Appearance.Options.UseFont = true;
            this.labelControlPLCStatus32.Appearance.Options.UseTextOptions = true;
            this.labelControlPLCStatus32.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlPLCStatus32.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlPLCStatus32.Location = new System.Drawing.Point(173, 235);
            this.labelControlPLCStatus32.Name = "labelControlPLCStatus32";
            this.labelControlPLCStatus32.Size = new System.Drawing.Size(50, 33);
            this.labelControlPLCStatus32.StyleController = this.layoutControl13;
            this.labelControlPLCStatus32.TabIndex = 35;
            this.labelControlPLCStatus32.Text = "32";
            // 
            // labelControlPLCStatus31
            // 
            this.labelControlPLCStatus31.Appearance.Font = new System.Drawing.Font("Tahoma", 6F);
            this.labelControlPLCStatus31.Appearance.Options.UseFont = true;
            this.labelControlPLCStatus31.Appearance.Options.UseTextOptions = true;
            this.labelControlPLCStatus31.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlPLCStatus31.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlPLCStatus31.Location = new System.Drawing.Point(120, 235);
            this.labelControlPLCStatus31.Name = "labelControlPLCStatus31";
            this.labelControlPLCStatus31.Size = new System.Drawing.Size(49, 33);
            this.labelControlPLCStatus31.StyleController = this.layoutControl13;
            this.labelControlPLCStatus31.TabIndex = 34;
            this.labelControlPLCStatus31.Text = "31";
            // 
            // labelControlPLCStatus30
            // 
            this.labelControlPLCStatus30.Appearance.Font = new System.Drawing.Font("Tahoma", 6F);
            this.labelControlPLCStatus30.Appearance.Options.UseFont = true;
            this.labelControlPLCStatus30.Appearance.Options.UseTextOptions = true;
            this.labelControlPLCStatus30.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlPLCStatus30.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlPLCStatus30.Location = new System.Drawing.Point(66, 235);
            this.labelControlPLCStatus30.Name = "labelControlPLCStatus30";
            this.labelControlPLCStatus30.Size = new System.Drawing.Size(50, 33);
            this.labelControlPLCStatus30.StyleController = this.layoutControl13;
            this.labelControlPLCStatus30.TabIndex = 33;
            this.labelControlPLCStatus30.Text = "30";
            // 
            // labelControlPLCStatus28
            // 
            this.labelControlPLCStatus28.Appearance.Font = new System.Drawing.Font("Tahoma", 6F);
            this.labelControlPLCStatus28.Appearance.Options.UseFont = true;
            this.labelControlPLCStatus28.Appearance.Options.UseTextOptions = true;
            this.labelControlPLCStatus28.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlPLCStatus28.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlPLCStatus28.Location = new System.Drawing.Point(173, 204);
            this.labelControlPLCStatus28.Name = "labelControlPLCStatus28";
            this.labelControlPLCStatus28.Size = new System.Drawing.Size(50, 27);
            this.labelControlPLCStatus28.StyleController = this.layoutControl13;
            this.labelControlPLCStatus28.TabIndex = 32;
            this.labelControlPLCStatus28.Text = "28";
            // 
            // labelControlPLCStatus27
            // 
            this.labelControlPLCStatus27.Appearance.Font = new System.Drawing.Font("Tahoma", 6F);
            this.labelControlPLCStatus27.Appearance.Options.UseFont = true;
            this.labelControlPLCStatus27.Appearance.Options.UseTextOptions = true;
            this.labelControlPLCStatus27.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlPLCStatus27.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlPLCStatus27.Location = new System.Drawing.Point(120, 204);
            this.labelControlPLCStatus27.Name = "labelControlPLCStatus27";
            this.labelControlPLCStatus27.Size = new System.Drawing.Size(49, 27);
            this.labelControlPLCStatus27.StyleController = this.layoutControl13;
            this.labelControlPLCStatus27.TabIndex = 31;
            this.labelControlPLCStatus27.Text = "27";
            // 
            // labelControlPLCStatus26
            // 
            this.labelControlPLCStatus26.Appearance.Font = new System.Drawing.Font("Tahoma", 6F);
            this.labelControlPLCStatus26.Appearance.Options.UseFont = true;
            this.labelControlPLCStatus26.Appearance.Options.UseTextOptions = true;
            this.labelControlPLCStatus26.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlPLCStatus26.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlPLCStatus26.Location = new System.Drawing.Point(66, 204);
            this.labelControlPLCStatus26.Name = "labelControlPLCStatus26";
            this.labelControlPLCStatus26.Size = new System.Drawing.Size(50, 27);
            this.labelControlPLCStatus26.StyleController = this.layoutControl13;
            this.labelControlPLCStatus26.TabIndex = 30;
            this.labelControlPLCStatus26.Text = "26";
            // 
            // labelControlPLCStatus24
            // 
            this.labelControlPLCStatus24.Appearance.Font = new System.Drawing.Font("Tahoma", 6F);
            this.labelControlPLCStatus24.Appearance.Options.UseFont = true;
            this.labelControlPLCStatus24.Appearance.Options.UseTextOptions = true;
            this.labelControlPLCStatus24.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlPLCStatus24.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlPLCStatus24.Location = new System.Drawing.Point(173, 170);
            this.labelControlPLCStatus24.Name = "labelControlPLCStatus24";
            this.labelControlPLCStatus24.Size = new System.Drawing.Size(50, 30);
            this.labelControlPLCStatus24.StyleController = this.layoutControl13;
            this.labelControlPLCStatus24.TabIndex = 29;
            this.labelControlPLCStatus24.Text = "24";
            // 
            // labelControlPLCStatus23
            // 
            this.labelControlPLCStatus23.Appearance.Font = new System.Drawing.Font("Tahoma", 6F);
            this.labelControlPLCStatus23.Appearance.Options.UseFont = true;
            this.labelControlPLCStatus23.Appearance.Options.UseTextOptions = true;
            this.labelControlPLCStatus23.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlPLCStatus23.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlPLCStatus23.Location = new System.Drawing.Point(120, 170);
            this.labelControlPLCStatus23.Name = "labelControlPLCStatus23";
            this.labelControlPLCStatus23.Size = new System.Drawing.Size(49, 30);
            this.labelControlPLCStatus23.StyleController = this.layoutControl13;
            this.labelControlPLCStatus23.TabIndex = 28;
            this.labelControlPLCStatus23.Text = "23";
            // 
            // labelControlPLCStatus22
            // 
            this.labelControlPLCStatus22.Appearance.Font = new System.Drawing.Font("Tahoma", 6F);
            this.labelControlPLCStatus22.Appearance.Options.UseFont = true;
            this.labelControlPLCStatus22.Appearance.Options.UseTextOptions = true;
            this.labelControlPLCStatus22.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlPLCStatus22.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlPLCStatus22.Location = new System.Drawing.Point(66, 170);
            this.labelControlPLCStatus22.Name = "labelControlPLCStatus22";
            this.labelControlPLCStatus22.Size = new System.Drawing.Size(50, 30);
            this.labelControlPLCStatus22.StyleController = this.layoutControl13;
            this.labelControlPLCStatus22.TabIndex = 27;
            this.labelControlPLCStatus22.Text = "22";
            // 
            // labelControlPLCStatus20
            // 
            this.labelControlPLCStatus20.Appearance.Font = new System.Drawing.Font("Tahoma", 6F);
            this.labelControlPLCStatus20.Appearance.Options.UseFont = true;
            this.labelControlPLCStatus20.Appearance.Options.UseTextOptions = true;
            this.labelControlPLCStatus20.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlPLCStatus20.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlPLCStatus20.Location = new System.Drawing.Point(173, 140);
            this.labelControlPLCStatus20.Name = "labelControlPLCStatus20";
            this.labelControlPLCStatus20.Size = new System.Drawing.Size(50, 26);
            this.labelControlPLCStatus20.StyleController = this.layoutControl13;
            this.labelControlPLCStatus20.TabIndex = 26;
            this.labelControlPLCStatus20.Text = "20";
            // 
            // labelControlPLCStatus19
            // 
            this.labelControlPLCStatus19.Appearance.Font = new System.Drawing.Font("Tahoma", 6F);
            this.labelControlPLCStatus19.Appearance.Options.UseFont = true;
            this.labelControlPLCStatus19.Appearance.Options.UseTextOptions = true;
            this.labelControlPLCStatus19.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlPLCStatus19.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlPLCStatus19.Location = new System.Drawing.Point(120, 140);
            this.labelControlPLCStatus19.Name = "labelControlPLCStatus19";
            this.labelControlPLCStatus19.Size = new System.Drawing.Size(49, 26);
            this.labelControlPLCStatus19.StyleController = this.layoutControl13;
            this.labelControlPLCStatus19.TabIndex = 25;
            this.labelControlPLCStatus19.Text = "19";
            // 
            // labelControlPLCStatus18
            // 
            this.labelControlPLCStatus18.Appearance.Font = new System.Drawing.Font("Tahoma", 6F);
            this.labelControlPLCStatus18.Appearance.Options.UseFont = true;
            this.labelControlPLCStatus18.Appearance.Options.UseTextOptions = true;
            this.labelControlPLCStatus18.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlPLCStatus18.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlPLCStatus18.Location = new System.Drawing.Point(66, 140);
            this.labelControlPLCStatus18.Name = "labelControlPLCStatus18";
            this.labelControlPLCStatus18.Size = new System.Drawing.Size(50, 26);
            this.labelControlPLCStatus18.StyleController = this.layoutControl13;
            this.labelControlPLCStatus18.TabIndex = 24;
            this.labelControlPLCStatus18.Text = "18";
            // 
            // labelControlPLCStatus16
            // 
            this.labelControlPLCStatus16.Appearance.Font = new System.Drawing.Font("Tahoma", 6F);
            this.labelControlPLCStatus16.Appearance.Options.UseFont = true;
            this.labelControlPLCStatus16.Appearance.Options.UseTextOptions = true;
            this.labelControlPLCStatus16.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlPLCStatus16.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlPLCStatus16.Location = new System.Drawing.Point(173, 108);
            this.labelControlPLCStatus16.Name = "labelControlPLCStatus16";
            this.labelControlPLCStatus16.Size = new System.Drawing.Size(50, 28);
            this.labelControlPLCStatus16.StyleController = this.layoutControl13;
            this.labelControlPLCStatus16.TabIndex = 23;
            this.labelControlPLCStatus16.Text = "Error";
            // 
            // labelControlPLCStatus15
            // 
            this.labelControlPLCStatus15.Appearance.Font = new System.Drawing.Font("Tahoma", 6F);
            this.labelControlPLCStatus15.Appearance.Options.UseFont = true;
            this.labelControlPLCStatus15.Appearance.Options.UseTextOptions = true;
            this.labelControlPLCStatus15.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlPLCStatus15.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlPLCStatus15.Location = new System.Drawing.Point(120, 108);
            this.labelControlPLCStatus15.Name = "labelControlPLCStatus15";
            this.labelControlPLCStatus15.Size = new System.Drawing.Size(49, 28);
            this.labelControlPLCStatus15.StyleController = this.layoutControl13;
            this.labelControlPLCStatus15.TabIndex = 22;
            this.labelControlPLCStatus15.Text = "S/W EMR";
            // 
            // labelControlPLCStatus14
            // 
            this.labelControlPLCStatus14.Appearance.Font = new System.Drawing.Font("Tahoma", 6F);
            this.labelControlPLCStatus14.Appearance.Options.UseFont = true;
            this.labelControlPLCStatus14.Appearance.Options.UseTextOptions = true;
            this.labelControlPLCStatus14.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlPLCStatus14.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlPLCStatus14.Location = new System.Drawing.Point(66, 108);
            this.labelControlPLCStatus14.Name = "labelControlPLCStatus14";
            this.labelControlPLCStatus14.Size = new System.Drawing.Size(50, 28);
            this.labelControlPLCStatus14.StyleController = this.layoutControl13;
            this.labelControlPLCStatus14.TabIndex = 21;
            this.labelControlPLCStatus14.Text = "H/W EMR";
            // 
            // labelControlPLCStatus12
            // 
            this.labelControlPLCStatus12.Appearance.Font = new System.Drawing.Font("Tahoma", 6F);
            this.labelControlPLCStatus12.Appearance.Options.UseFont = true;
            this.labelControlPLCStatus12.Appearance.Options.UseTextOptions = true;
            this.labelControlPLCStatus12.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlPLCStatus12.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlPLCStatus12.Location = new System.Drawing.Point(173, 76);
            this.labelControlPLCStatus12.Name = "labelControlPLCStatus12";
            this.labelControlPLCStatus12.Size = new System.Drawing.Size(50, 28);
            this.labelControlPLCStatus12.StyleController = this.layoutControl13;
            this.labelControlPLCStatus12.TabIndex = 20;
            this.labelControlPLCStatus12.Text = "12";
            // 
            // labelControlPLCStatus11
            // 
            this.labelControlPLCStatus11.Appearance.Font = new System.Drawing.Font("Tahoma", 6F);
            this.labelControlPLCStatus11.Appearance.Options.UseFont = true;
            this.labelControlPLCStatus11.Appearance.Options.UseTextOptions = true;
            this.labelControlPLCStatus11.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlPLCStatus11.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlPLCStatus11.Location = new System.Drawing.Point(120, 76);
            this.labelControlPLCStatus11.Name = "labelControlPLCStatus11";
            this.labelControlPLCStatus11.Size = new System.Drawing.Size(49, 28);
            this.labelControlPLCStatus11.StyleController = this.layoutControl13;
            this.labelControlPLCStatus11.TabIndex = 19;
            this.labelControlPLCStatus11.Text = "Velocity Mode";
            // 
            // labelControlPLCStatus10
            // 
            this.labelControlPLCStatus10.Appearance.Font = new System.Drawing.Font("Tahoma", 6F);
            this.labelControlPLCStatus10.Appearance.Options.UseFont = true;
            this.labelControlPLCStatus10.Appearance.Options.UseTextOptions = true;
            this.labelControlPLCStatus10.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlPLCStatus10.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlPLCStatus10.Location = new System.Drawing.Point(66, 76);
            this.labelControlPLCStatus10.Name = "labelControlPLCStatus10";
            this.labelControlPLCStatus10.Size = new System.Drawing.Size(50, 28);
            this.labelControlPLCStatus10.StyleController = this.layoutControl13;
            this.labelControlPLCStatus10.TabIndex = 18;
            this.labelControlPLCStatus10.Text = "Jog Mode";
            // 
            // labelControlPLCStatus8
            // 
            this.labelControlPLCStatus8.Appearance.Font = new System.Drawing.Font("Tahoma", 6F);
            this.labelControlPLCStatus8.Appearance.Options.UseFont = true;
            this.labelControlPLCStatus8.Appearance.Options.UseTextOptions = true;
            this.labelControlPLCStatus8.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlPLCStatus8.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlPLCStatus8.Location = new System.Drawing.Point(173, 44);
            this.labelControlPLCStatus8.Name = "labelControlPLCStatus8";
            this.labelControlPLCStatus8.Size = new System.Drawing.Size(50, 28);
            this.labelControlPLCStatus8.StyleController = this.layoutControl13;
            this.labelControlPLCStatus8.TabIndex = 17;
            this.labelControlPLCStatus8.Text = "8";
            // 
            // labelControlPLCStatus7
            // 
            this.labelControlPLCStatus7.Appearance.Font = new System.Drawing.Font("Tahoma", 6F);
            this.labelControlPLCStatus7.Appearance.Options.UseFont = true;
            this.labelControlPLCStatus7.Appearance.Options.UseTextOptions = true;
            this.labelControlPLCStatus7.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlPLCStatus7.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlPLCStatus7.Location = new System.Drawing.Point(120, 44);
            this.labelControlPLCStatus7.Name = "labelControlPLCStatus7";
            this.labelControlPLCStatus7.Size = new System.Drawing.Size(49, 28);
            this.labelControlPLCStatus7.StyleController = this.layoutControl13;
            this.labelControlPLCStatus7.TabIndex = 16;
            this.labelControlPLCStatus7.Text = "Inposition";
            // 
            // labelControlPLCStatus6
            // 
            this.labelControlPLCStatus6.Appearance.Font = new System.Drawing.Font("Tahoma", 6F);
            this.labelControlPLCStatus6.Appearance.Options.UseFont = true;
            this.labelControlPLCStatus6.Appearance.Options.UseTextOptions = true;
            this.labelControlPLCStatus6.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlPLCStatus6.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlPLCStatus6.Location = new System.Drawing.Point(66, 44);
            this.labelControlPLCStatus6.Name = "labelControlPLCStatus6";
            this.labelControlPLCStatus6.Size = new System.Drawing.Size(50, 28);
            this.labelControlPLCStatus6.StyleController = this.layoutControl13;
            this.labelControlPLCStatus6.TabIndex = 15;
            this.labelControlPLCStatus6.Text = "Moving";
            // 
            // labelControlPLCStatus29
            // 
            this.labelControlPLCStatus29.Appearance.Font = new System.Drawing.Font("Tahoma", 6F);
            this.labelControlPLCStatus29.Appearance.Options.UseFont = true;
            this.labelControlPLCStatus29.Appearance.Options.UseTextOptions = true;
            this.labelControlPLCStatus29.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlPLCStatus29.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlPLCStatus29.Location = new System.Drawing.Point(12, 235);
            this.labelControlPLCStatus29.Name = "labelControlPLCStatus29";
            this.labelControlPLCStatus29.Size = new System.Drawing.Size(50, 33);
            this.labelControlPLCStatus29.StyleController = this.layoutControl13;
            this.labelControlPLCStatus29.TabIndex = 14;
            this.labelControlPLCStatus29.Text = "29";
            // 
            // labelControlPLCStatus25
            // 
            this.labelControlPLCStatus25.Appearance.Font = new System.Drawing.Font("Tahoma", 6F);
            this.labelControlPLCStatus25.Appearance.Options.UseFont = true;
            this.labelControlPLCStatus25.Appearance.Options.UseTextOptions = true;
            this.labelControlPLCStatus25.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlPLCStatus25.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlPLCStatus25.Location = new System.Drawing.Point(12, 204);
            this.labelControlPLCStatus25.Name = "labelControlPLCStatus25";
            this.labelControlPLCStatus25.Size = new System.Drawing.Size(50, 27);
            this.labelControlPLCStatus25.StyleController = this.layoutControl13;
            this.labelControlPLCStatus25.TabIndex = 13;
            this.labelControlPLCStatus25.Text = "25";
            // 
            // labelControlPLCStatus21
            // 
            this.labelControlPLCStatus21.Appearance.Font = new System.Drawing.Font("Tahoma", 6F);
            this.labelControlPLCStatus21.Appearance.Options.UseFont = true;
            this.labelControlPLCStatus21.Appearance.Options.UseTextOptions = true;
            this.labelControlPLCStatus21.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlPLCStatus21.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlPLCStatus21.Location = new System.Drawing.Point(12, 170);
            this.labelControlPLCStatus21.Name = "labelControlPLCStatus21";
            this.labelControlPLCStatus21.Size = new System.Drawing.Size(50, 30);
            this.labelControlPLCStatus21.StyleController = this.layoutControl13;
            this.labelControlPLCStatus21.TabIndex = 12;
            this.labelControlPLCStatus21.Text = "21";
            // 
            // labelControlPLCStatus17
            // 
            this.labelControlPLCStatus17.Appearance.Font = new System.Drawing.Font("Tahoma", 6F);
            this.labelControlPLCStatus17.Appearance.Options.UseFont = true;
            this.labelControlPLCStatus17.Appearance.Options.UseTextOptions = true;
            this.labelControlPLCStatus17.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlPLCStatus17.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlPLCStatus17.Location = new System.Drawing.Point(12, 140);
            this.labelControlPLCStatus17.Name = "labelControlPLCStatus17";
            this.labelControlPLCStatus17.Size = new System.Drawing.Size(50, 26);
            this.labelControlPLCStatus17.StyleController = this.layoutControl13;
            this.labelControlPLCStatus17.TabIndex = 11;
            this.labelControlPLCStatus17.Text = "17";
            // 
            // labelControlPLCStatus13
            // 
            this.labelControlPLCStatus13.Appearance.Font = new System.Drawing.Font("Tahoma", 6F);
            this.labelControlPLCStatus13.Appearance.Options.UseFont = true;
            this.labelControlPLCStatus13.Appearance.Options.UseTextOptions = true;
            this.labelControlPLCStatus13.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlPLCStatus13.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlPLCStatus13.Location = new System.Drawing.Point(12, 108);
            this.labelControlPLCStatus13.Name = "labelControlPLCStatus13";
            this.labelControlPLCStatus13.Size = new System.Drawing.Size(50, 28);
            this.labelControlPLCStatus13.StyleController = this.layoutControl13;
            this.labelControlPLCStatus13.TabIndex = 10;
            this.labelControlPLCStatus13.Text = "13";
            // 
            // labelControlPLCStatus9
            // 
            this.labelControlPLCStatus9.Appearance.Font = new System.Drawing.Font("Tahoma", 6F);
            this.labelControlPLCStatus9.Appearance.Options.UseFont = true;
            this.labelControlPLCStatus9.Appearance.Options.UseTextOptions = true;
            this.labelControlPLCStatus9.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlPLCStatus9.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlPLCStatus9.Location = new System.Drawing.Point(12, 76);
            this.labelControlPLCStatus9.Name = "labelControlPLCStatus9";
            this.labelControlPLCStatus9.Size = new System.Drawing.Size(50, 28);
            this.labelControlPLCStatus9.StyleController = this.layoutControl13;
            this.labelControlPLCStatus9.TabIndex = 9;
            this.labelControlPLCStatus9.Text = "Auto Mode";
            // 
            // labelControlPLCStatus5
            // 
            this.labelControlPLCStatus5.Appearance.Font = new System.Drawing.Font("Tahoma", 6F);
            this.labelControlPLCStatus5.Appearance.Options.UseFont = true;
            this.labelControlPLCStatus5.Appearance.Options.UseTextOptions = true;
            this.labelControlPLCStatus5.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlPLCStatus5.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlPLCStatus5.Location = new System.Drawing.Point(12, 44);
            this.labelControlPLCStatus5.Name = "labelControlPLCStatus5";
            this.labelControlPLCStatus5.Size = new System.Drawing.Size(50, 28);
            this.labelControlPLCStatus5.StyleController = this.layoutControl13;
            this.labelControlPLCStatus5.TabIndex = 8;
            this.labelControlPLCStatus5.Text = "Move Stop";
            // 
            // labelControlPLCStatus4
            // 
            this.labelControlPLCStatus4.Appearance.Font = new System.Drawing.Font("Tahoma", 6F);
            this.labelControlPLCStatus4.Appearance.Options.UseFont = true;
            this.labelControlPLCStatus4.Appearance.Options.UseTextOptions = true;
            this.labelControlPLCStatus4.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlPLCStatus4.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlPLCStatus4.Location = new System.Drawing.Point(173, 12);
            this.labelControlPLCStatus4.Name = "labelControlPLCStatus4";
            this.labelControlPLCStatus4.Size = new System.Drawing.Size(50, 28);
            this.labelControlPLCStatus4.StyleController = this.layoutControl13;
            this.labelControlPLCStatus4.TabIndex = 7;
            this.labelControlPLCStatus4.Text = "Ready";
            // 
            // labelControlPLCStatus3
            // 
            this.labelControlPLCStatus3.Appearance.Font = new System.Drawing.Font("Tahoma", 6F);
            this.labelControlPLCStatus3.Appearance.Options.UseFont = true;
            this.labelControlPLCStatus3.Appearance.Options.UseTextOptions = true;
            this.labelControlPLCStatus3.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlPLCStatus3.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlPLCStatus3.Location = new System.Drawing.Point(120, 12);
            this.labelControlPLCStatus3.Name = "labelControlPLCStatus3";
            this.labelControlPLCStatus3.Size = new System.Drawing.Size(49, 28);
            this.labelControlPLCStatus3.StyleController = this.layoutControl13;
            this.labelControlPLCStatus3.TabIndex = 6;
            this.labelControlPLCStatus3.Text = "Homming";
            // 
            // labelControlPLCStatus2
            // 
            this.labelControlPLCStatus2.Appearance.Font = new System.Drawing.Font("Tahoma", 6F);
            this.labelControlPLCStatus2.Appearance.Options.UseFont = true;
            this.labelControlPLCStatus2.Appearance.Options.UseTextOptions = true;
            this.labelControlPLCStatus2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlPLCStatus2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlPLCStatus2.Location = new System.Drawing.Point(66, 12);
            this.labelControlPLCStatus2.Name = "labelControlPLCStatus2";
            this.labelControlPLCStatus2.Size = new System.Drawing.Size(50, 28);
            this.labelControlPLCStatus2.StyleController = this.layoutControl13;
            this.labelControlPLCStatus2.TabIndex = 5;
            this.labelControlPLCStatus2.Text = "Servo On";
            // 
            // labelControlPLCStatus1
            // 
            this.labelControlPLCStatus1.Appearance.Font = new System.Drawing.Font("Tahoma", 6F);
            this.labelControlPLCStatus1.Appearance.Options.UseFont = true;
            this.labelControlPLCStatus1.Appearance.Options.UseTextOptions = true;
            this.labelControlPLCStatus1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlPLCStatus1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlPLCStatus1.Location = new System.Drawing.Point(12, 12);
            this.labelControlPLCStatus1.Name = "labelControlPLCStatus1";
            this.labelControlPLCStatus1.Size = new System.Drawing.Size(50, 28);
            this.labelControlPLCStatus1.StyleController = this.layoutControl13;
            this.labelControlPLCStatus1.TabIndex = 4;
            this.labelControlPLCStatus1.Text = "EtherCAT OP";
            // 
            // layoutControlGroup14
            // 
            this.layoutControlGroup14.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup14.GroupBordersVisible = false;
            this.layoutControlGroup14.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem85,
            this.layoutControlItem86,
            this.layoutControlItem87,
            this.layoutControlItem88,
            this.layoutControlItem89,
            this.layoutControlItem91,
            this.layoutControlItem92,
            this.layoutControlItem93,
            this.layoutControlItem94,
            this.layoutControlItem95,
            this.layoutControlItem96,
            this.layoutControlItem97,
            this.layoutControlItem98,
            this.layoutControlItem101,
            this.layoutControlItem90,
            this.layoutControlItem99,
            this.layoutControlItem100,
            this.layoutControlItem102,
            this.layoutControlItem103,
            this.layoutControlItem104,
            this.layoutControlItem105,
            this.layoutControlItem106,
            this.layoutControlItem107,
            this.layoutControlItem108,
            this.layoutControlItem109,
            this.layoutControlItem110,
            this.layoutControlItem111,
            this.layoutControlItem112,
            this.layoutControlItem113,
            this.layoutControlItem114,
            this.layoutControlItem115,
            this.layoutControlItem116});
            this.layoutControlGroup14.Name = "layoutControlGroup14";
            this.layoutControlGroup14.Size = new System.Drawing.Size(235, 280);
            this.layoutControlGroup14.TextVisible = false;
            // 
            // layoutControlItem85
            // 
            this.layoutControlItem85.Control = this.labelControlPLCStatus1;
            this.layoutControlItem85.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem85.MaxSize = new System.Drawing.Size(80, 58);
            this.layoutControlItem85.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem85.Name = "layoutControlItem85";
            this.layoutControlItem85.Size = new System.Drawing.Size(54, 32);
            this.layoutControlItem85.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem85.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem85.TextVisible = false;
            // 
            // layoutControlItem86
            // 
            this.layoutControlItem86.Control = this.labelControlPLCStatus2;
            this.layoutControlItem86.Location = new System.Drawing.Point(54, 0);
            this.layoutControlItem86.MaxSize = new System.Drawing.Size(80, 58);
            this.layoutControlItem86.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem86.Name = "layoutControlItem86";
            this.layoutControlItem86.Size = new System.Drawing.Size(54, 32);
            this.layoutControlItem86.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem86.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem86.TextVisible = false;
            // 
            // layoutControlItem87
            // 
            this.layoutControlItem87.Control = this.labelControlPLCStatus3;
            this.layoutControlItem87.Location = new System.Drawing.Point(108, 0);
            this.layoutControlItem87.MaxSize = new System.Drawing.Size(80, 58);
            this.layoutControlItem87.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem87.Name = "layoutControlItem87";
            this.layoutControlItem87.Size = new System.Drawing.Size(53, 32);
            this.layoutControlItem87.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem87.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem87.TextVisible = false;
            // 
            // layoutControlItem88
            // 
            this.layoutControlItem88.Control = this.labelControlPLCStatus4;
            this.layoutControlItem88.Location = new System.Drawing.Point(161, 0);
            this.layoutControlItem88.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem88.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem88.Name = "layoutControlItem88";
            this.layoutControlItem88.Size = new System.Drawing.Size(54, 32);
            this.layoutControlItem88.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem88.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem88.TextVisible = false;
            // 
            // layoutControlItem89
            // 
            this.layoutControlItem89.Control = this.labelControlPLCStatus5;
            this.layoutControlItem89.Location = new System.Drawing.Point(0, 32);
            this.layoutControlItem89.MaxSize = new System.Drawing.Size(80, 58);
            this.layoutControlItem89.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem89.Name = "layoutControlItem89";
            this.layoutControlItem89.Size = new System.Drawing.Size(54, 32);
            this.layoutControlItem89.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem89.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem89.TextVisible = false;
            // 
            // layoutControlItem91
            // 
            this.layoutControlItem91.Control = this.labelControlPLCStatus13;
            this.layoutControlItem91.Location = new System.Drawing.Point(0, 96);
            this.layoutControlItem91.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem91.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem91.Name = "layoutControlItem91";
            this.layoutControlItem91.Size = new System.Drawing.Size(54, 32);
            this.layoutControlItem91.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem91.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem91.TextVisible = false;
            // 
            // layoutControlItem92
            // 
            this.layoutControlItem92.Control = this.labelControlPLCStatus17;
            this.layoutControlItem92.Location = new System.Drawing.Point(0, 128);
            this.layoutControlItem92.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem92.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem92.Name = "layoutControlItem92";
            this.layoutControlItem92.Size = new System.Drawing.Size(54, 30);
            this.layoutControlItem92.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem92.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem92.TextVisible = false;
            // 
            // layoutControlItem93
            // 
            this.layoutControlItem93.Control = this.labelControlPLCStatus21;
            this.layoutControlItem93.Location = new System.Drawing.Point(0, 158);
            this.layoutControlItem93.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem93.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem93.Name = "layoutControlItem93";
            this.layoutControlItem93.Size = new System.Drawing.Size(54, 34);
            this.layoutControlItem93.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem93.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem93.TextVisible = false;
            // 
            // layoutControlItem94
            // 
            this.layoutControlItem94.Control = this.labelControlPLCStatus25;
            this.layoutControlItem94.Location = new System.Drawing.Point(0, 192);
            this.layoutControlItem94.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem94.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem94.Name = "layoutControlItem94";
            this.layoutControlItem94.Size = new System.Drawing.Size(54, 31);
            this.layoutControlItem94.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem94.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem94.TextVisible = false;
            // 
            // layoutControlItem95
            // 
            this.layoutControlItem95.Control = this.labelControlPLCStatus29;
            this.layoutControlItem95.Location = new System.Drawing.Point(0, 223);
            this.layoutControlItem95.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem95.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem95.Name = "layoutControlItem95";
            this.layoutControlItem95.Size = new System.Drawing.Size(54, 37);
            this.layoutControlItem95.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem95.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem95.TextVisible = false;
            // 
            // layoutControlItem96
            // 
            this.layoutControlItem96.Control = this.labelControlPLCStatus6;
            this.layoutControlItem96.Location = new System.Drawing.Point(54, 32);
            this.layoutControlItem96.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem96.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem96.Name = "layoutControlItem96";
            this.layoutControlItem96.Size = new System.Drawing.Size(54, 32);
            this.layoutControlItem96.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem96.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem96.TextVisible = false;
            // 
            // layoutControlItem97
            // 
            this.layoutControlItem97.Control = this.labelControlPLCStatus7;
            this.layoutControlItem97.Location = new System.Drawing.Point(108, 32);
            this.layoutControlItem97.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem97.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem97.Name = "layoutControlItem97";
            this.layoutControlItem97.Size = new System.Drawing.Size(53, 32);
            this.layoutControlItem97.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem97.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem97.TextVisible = false;
            // 
            // layoutControlItem98
            // 
            this.layoutControlItem98.Control = this.labelControlPLCStatus8;
            this.layoutControlItem98.Location = new System.Drawing.Point(161, 32);
            this.layoutControlItem98.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem98.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem98.Name = "layoutControlItem98";
            this.layoutControlItem98.Size = new System.Drawing.Size(54, 32);
            this.layoutControlItem98.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem98.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem98.TextVisible = false;
            // 
            // layoutControlItem101
            // 
            this.layoutControlItem101.Control = this.labelControlPLCStatus12;
            this.layoutControlItem101.Location = new System.Drawing.Point(161, 64);
            this.layoutControlItem101.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem101.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem101.Name = "layoutControlItem101";
            this.layoutControlItem101.Size = new System.Drawing.Size(54, 32);
            this.layoutControlItem101.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem101.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem101.TextVisible = false;
            // 
            // layoutControlItem90
            // 
            this.layoutControlItem90.Control = this.labelControlPLCStatus9;
            this.layoutControlItem90.Location = new System.Drawing.Point(0, 64);
            this.layoutControlItem90.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem90.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem90.Name = "layoutControlItem90";
            this.layoutControlItem90.Size = new System.Drawing.Size(54, 32);
            this.layoutControlItem90.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem90.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem90.TextVisible = false;
            // 
            // layoutControlItem99
            // 
            this.layoutControlItem99.Control = this.labelControlPLCStatus10;
            this.layoutControlItem99.Location = new System.Drawing.Point(54, 64);
            this.layoutControlItem99.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem99.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem99.Name = "layoutControlItem99";
            this.layoutControlItem99.Size = new System.Drawing.Size(54, 32);
            this.layoutControlItem99.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem99.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem99.TextVisible = false;
            // 
            // layoutControlItem100
            // 
            this.layoutControlItem100.Control = this.labelControlPLCStatus11;
            this.layoutControlItem100.Location = new System.Drawing.Point(108, 64);
            this.layoutControlItem100.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem100.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem100.Name = "layoutControlItem100";
            this.layoutControlItem100.Size = new System.Drawing.Size(53, 32);
            this.layoutControlItem100.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem100.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem100.TextVisible = false;
            // 
            // layoutControlItem102
            // 
            this.layoutControlItem102.Control = this.labelControlPLCStatus14;
            this.layoutControlItem102.Location = new System.Drawing.Point(54, 96);
            this.layoutControlItem102.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem102.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem102.Name = "layoutControlItem102";
            this.layoutControlItem102.Size = new System.Drawing.Size(54, 32);
            this.layoutControlItem102.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem102.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem102.TextVisible = false;
            // 
            // layoutControlItem103
            // 
            this.layoutControlItem103.Control = this.labelControlPLCStatus15;
            this.layoutControlItem103.Location = new System.Drawing.Point(108, 96);
            this.layoutControlItem103.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem103.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem103.Name = "layoutControlItem103";
            this.layoutControlItem103.Size = new System.Drawing.Size(53, 32);
            this.layoutControlItem103.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem103.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem103.TextVisible = false;
            // 
            // layoutControlItem104
            // 
            this.layoutControlItem104.Control = this.labelControlPLCStatus16;
            this.layoutControlItem104.Location = new System.Drawing.Point(161, 96);
            this.layoutControlItem104.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem104.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem104.Name = "layoutControlItem104";
            this.layoutControlItem104.Size = new System.Drawing.Size(54, 32);
            this.layoutControlItem104.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem104.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem104.TextVisible = false;
            // 
            // layoutControlItem105
            // 
            this.layoutControlItem105.Control = this.labelControlPLCStatus18;
            this.layoutControlItem105.Location = new System.Drawing.Point(54, 128);
            this.layoutControlItem105.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem105.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem105.Name = "layoutControlItem105";
            this.layoutControlItem105.Size = new System.Drawing.Size(54, 30);
            this.layoutControlItem105.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem105.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem105.TextVisible = false;
            // 
            // layoutControlItem106
            // 
            this.layoutControlItem106.Control = this.labelControlPLCStatus19;
            this.layoutControlItem106.Location = new System.Drawing.Point(108, 128);
            this.layoutControlItem106.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem106.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem106.Name = "layoutControlItem106";
            this.layoutControlItem106.Size = new System.Drawing.Size(53, 30);
            this.layoutControlItem106.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem106.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem106.TextVisible = false;
            // 
            // layoutControlItem107
            // 
            this.layoutControlItem107.Control = this.labelControlPLCStatus20;
            this.layoutControlItem107.Location = new System.Drawing.Point(161, 128);
            this.layoutControlItem107.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem107.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem107.Name = "layoutControlItem107";
            this.layoutControlItem107.Size = new System.Drawing.Size(54, 30);
            this.layoutControlItem107.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem107.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem107.TextVisible = false;
            // 
            // layoutControlItem108
            // 
            this.layoutControlItem108.Control = this.labelControlPLCStatus22;
            this.layoutControlItem108.Location = new System.Drawing.Point(54, 158);
            this.layoutControlItem108.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem108.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem108.Name = "layoutControlItem108";
            this.layoutControlItem108.Size = new System.Drawing.Size(54, 34);
            this.layoutControlItem108.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem108.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem108.TextVisible = false;
            // 
            // layoutControlItem109
            // 
            this.layoutControlItem109.Control = this.labelControlPLCStatus23;
            this.layoutControlItem109.Location = new System.Drawing.Point(108, 158);
            this.layoutControlItem109.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem109.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem109.Name = "layoutControlItem109";
            this.layoutControlItem109.Size = new System.Drawing.Size(53, 34);
            this.layoutControlItem109.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem109.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem109.TextVisible = false;
            // 
            // layoutControlItem110
            // 
            this.layoutControlItem110.Control = this.labelControlPLCStatus24;
            this.layoutControlItem110.Location = new System.Drawing.Point(161, 158);
            this.layoutControlItem110.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem110.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem110.Name = "layoutControlItem110";
            this.layoutControlItem110.Size = new System.Drawing.Size(54, 34);
            this.layoutControlItem110.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem110.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem110.TextVisible = false;
            // 
            // layoutControlItem111
            // 
            this.layoutControlItem111.Control = this.labelControlPLCStatus26;
            this.layoutControlItem111.Location = new System.Drawing.Point(54, 192);
            this.layoutControlItem111.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem111.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem111.Name = "layoutControlItem111";
            this.layoutControlItem111.Size = new System.Drawing.Size(54, 31);
            this.layoutControlItem111.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem111.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem111.TextVisible = false;
            // 
            // layoutControlItem112
            // 
            this.layoutControlItem112.Control = this.labelControlPLCStatus27;
            this.layoutControlItem112.Location = new System.Drawing.Point(108, 192);
            this.layoutControlItem112.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem112.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem112.Name = "layoutControlItem112";
            this.layoutControlItem112.Size = new System.Drawing.Size(53, 31);
            this.layoutControlItem112.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem112.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem112.TextVisible = false;
            // 
            // layoutControlItem113
            // 
            this.layoutControlItem113.Control = this.labelControlPLCStatus28;
            this.layoutControlItem113.Location = new System.Drawing.Point(161, 192);
            this.layoutControlItem113.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem113.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem113.Name = "layoutControlItem113";
            this.layoutControlItem113.Size = new System.Drawing.Size(54, 31);
            this.layoutControlItem113.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem113.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem113.TextVisible = false;
            // 
            // layoutControlItem114
            // 
            this.layoutControlItem114.Control = this.labelControlPLCStatus30;
            this.layoutControlItem114.Location = new System.Drawing.Point(54, 223);
            this.layoutControlItem114.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem114.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem114.Name = "layoutControlItem114";
            this.layoutControlItem114.Size = new System.Drawing.Size(54, 37);
            this.layoutControlItem114.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem114.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem114.TextVisible = false;
            // 
            // layoutControlItem115
            // 
            this.layoutControlItem115.Control = this.labelControlPLCStatus31;
            this.layoutControlItem115.Location = new System.Drawing.Point(108, 223);
            this.layoutControlItem115.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem115.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem115.Name = "layoutControlItem115";
            this.layoutControlItem115.Size = new System.Drawing.Size(53, 37);
            this.layoutControlItem115.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem115.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem115.TextVisible = false;
            // 
            // layoutControlItem116
            // 
            this.layoutControlItem116.Control = this.labelControlPLCStatus32;
            this.layoutControlItem116.Location = new System.Drawing.Point(161, 223);
            this.layoutControlItem116.MaxSize = new System.Drawing.Size(81, 58);
            this.layoutControlItem116.MinSize = new System.Drawing.Size(30, 23);
            this.layoutControlItem116.Name = "layoutControlItem116";
            this.layoutControlItem116.Size = new System.Drawing.Size(54, 37);
            this.layoutControlItem116.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem116.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem116.TextVisible = false;
            // 
            // groupControl3
            // 
            this.groupControl3.Controls.Add(this.layoutControl12);
            this.groupControl3.Location = new System.Drawing.Point(6, 7);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(222, 303);
            this.groupControl3.TabIndex = 4;
            this.groupControl3.Text = "드라이버 센서상태";
            // 
            // layoutControl12
            // 
            this.layoutControl12.Controls.Add(this.labelControlPLimitFR);
            this.layoutControl12.Controls.Add(this.labelControlHomeFR);
            this.layoutControl12.Controls.Add(this.labelControlNLimit_FR);
            this.layoutControl12.Controls.Add(this.labelControlPLimitFZ);
            this.layoutControl12.Controls.Add(this.labelControlHomeFZ);
            this.layoutControl12.Controls.Add(this.labelControlNLimit_FZ);
            this.layoutControl12.Controls.Add(this.labelControlPLimitZ);
            this.layoutControl12.Controls.Add(this.labelControlHomeZ);
            this.layoutControl12.Controls.Add(this.labelControlNLimit_Z);
            this.layoutControl12.Controls.Add(this.labelControlPLimitY2);
            this.layoutControl12.Controls.Add(this.labelControlHomeY2);
            this.layoutControl12.Controls.Add(this.labelControlNLimit_Y2);
            this.layoutControl12.Controls.Add(this.labelControlPLimitY1);
            this.layoutControl12.Controls.Add(this.labelControlHomeY1);
            this.layoutControl12.Controls.Add(this.labelControlNLimit_Y1);
            this.layoutControl12.Controls.Add(this.labelControlPLimitX);
            this.layoutControl12.Controls.Add(this.labelControlHomeX);
            this.layoutControl12.Controls.Add(this.labelControlNLimit_X);
            this.layoutControl12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl12.Location = new System.Drawing.Point(2, 21);
            this.layoutControl12.Name = "layoutControl12";
            this.layoutControl12.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(685, 22, 650, 400);
            this.layoutControl12.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl12.Root = this.layoutControlGroup13;
            this.layoutControl12.Size = new System.Drawing.Size(218, 280);
            this.layoutControl12.TabIndex = 0;
            this.layoutControl12.Text = "layoutControl12";
            // 
            // labelControlPLimitFR
            // 
            this.labelControlPLimitFR.Appearance.Options.UseTextOptions = true;
            this.labelControlPLimitFR.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlPLimitFR.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlPLimitFR.Location = new System.Drawing.Point(146, 226);
            this.labelControlPLimitFR.Name = "labelControlPLimitFR";
            this.labelControlPLimitFR.Size = new System.Drawing.Size(60, 42);
            this.labelControlPLimitFR.StyleController = this.layoutControl12;
            this.labelControlPLimitFR.TabIndex = 21;
            this.labelControlPLimitFR.Text = "Limti+(FR)";
            // 
            // labelControlHomeFR
            // 
            this.labelControlHomeFR.Appearance.Options.UseTextOptions = true;
            this.labelControlHomeFR.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlHomeFR.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlHomeFR.Location = new System.Drawing.Point(76, 226);
            this.labelControlHomeFR.Name = "labelControlHomeFR";
            this.labelControlHomeFR.Size = new System.Drawing.Size(66, 42);
            this.labelControlHomeFR.StyleController = this.layoutControl12;
            this.labelControlHomeFR.TabIndex = 20;
            this.labelControlHomeFR.Text = "Home(FR)";
            // 
            // labelControlNLimit_FR
            // 
            this.labelControlNLimit_FR.Appearance.Options.UseTextOptions = true;
            this.labelControlNLimit_FR.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlNLimit_FR.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlNLimit_FR.Location = new System.Drawing.Point(12, 226);
            this.labelControlNLimit_FR.Name = "labelControlNLimit_FR";
            this.labelControlNLimit_FR.Size = new System.Drawing.Size(60, 42);
            this.labelControlNLimit_FR.StyleController = this.layoutControl12;
            this.labelControlNLimit_FR.TabIndex = 19;
            this.labelControlNLimit_FR.Text = "-Limit(FR)";
            // 
            // labelControlPLimitFZ
            // 
            this.labelControlPLimitFZ.Appearance.Options.UseTextOptions = true;
            this.labelControlPLimitFZ.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlPLimitFZ.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlPLimitFZ.Location = new System.Drawing.Point(146, 183);
            this.labelControlPLimitFZ.Name = "labelControlPLimitFZ";
            this.labelControlPLimitFZ.Size = new System.Drawing.Size(60, 39);
            this.labelControlPLimitFZ.StyleController = this.layoutControl12;
            this.labelControlPLimitFZ.TabIndex = 18;
            this.labelControlPLimitFZ.Text = "Limti+(FZ)";
            // 
            // labelControlHomeFZ
            // 
            this.labelControlHomeFZ.Appearance.Options.UseTextOptions = true;
            this.labelControlHomeFZ.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlHomeFZ.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlHomeFZ.Location = new System.Drawing.Point(76, 183);
            this.labelControlHomeFZ.Name = "labelControlHomeFZ";
            this.labelControlHomeFZ.Size = new System.Drawing.Size(66, 39);
            this.labelControlHomeFZ.StyleController = this.layoutControl12;
            this.labelControlHomeFZ.TabIndex = 17;
            this.labelControlHomeFZ.Text = "Home(FZ)";
            // 
            // labelControlNLimit_FZ
            // 
            this.labelControlNLimit_FZ.Appearance.Options.UseTextOptions = true;
            this.labelControlNLimit_FZ.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlNLimit_FZ.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlNLimit_FZ.Location = new System.Drawing.Point(12, 183);
            this.labelControlNLimit_FZ.Name = "labelControlNLimit_FZ";
            this.labelControlNLimit_FZ.Size = new System.Drawing.Size(60, 39);
            this.labelControlNLimit_FZ.StyleController = this.layoutControl12;
            this.labelControlNLimit_FZ.TabIndex = 16;
            this.labelControlNLimit_FZ.Text = "-Limit(FZ)";
            // 
            // labelControlPLimitZ
            // 
            this.labelControlPLimitZ.Appearance.Options.UseTextOptions = true;
            this.labelControlPLimitZ.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlPLimitZ.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlPLimitZ.Location = new System.Drawing.Point(146, 140);
            this.labelControlPLimitZ.Name = "labelControlPLimitZ";
            this.labelControlPLimitZ.Size = new System.Drawing.Size(60, 39);
            this.labelControlPLimitZ.StyleController = this.layoutControl12;
            this.labelControlPLimitZ.TabIndex = 15;
            this.labelControlPLimitZ.Text = "Limti+(Z)";
            // 
            // labelControlHomeZ
            // 
            this.labelControlHomeZ.Appearance.Options.UseTextOptions = true;
            this.labelControlHomeZ.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlHomeZ.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlHomeZ.Location = new System.Drawing.Point(76, 140);
            this.labelControlHomeZ.Name = "labelControlHomeZ";
            this.labelControlHomeZ.Size = new System.Drawing.Size(66, 39);
            this.labelControlHomeZ.StyleController = this.layoutControl12;
            this.labelControlHomeZ.TabIndex = 14;
            this.labelControlHomeZ.Text = "Home(Z)";
            // 
            // labelControlNLimit_Z
            // 
            this.labelControlNLimit_Z.Appearance.Options.UseTextOptions = true;
            this.labelControlNLimit_Z.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlNLimit_Z.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlNLimit_Z.Location = new System.Drawing.Point(12, 140);
            this.labelControlNLimit_Z.Name = "labelControlNLimit_Z";
            this.labelControlNLimit_Z.Size = new System.Drawing.Size(60, 39);
            this.labelControlNLimit_Z.StyleController = this.layoutControl12;
            this.labelControlNLimit_Z.TabIndex = 13;
            this.labelControlNLimit_Z.Text = "-Limit(Z)";
            // 
            // labelControlPLimitY2
            // 
            this.labelControlPLimitY2.Appearance.Options.UseTextOptions = true;
            this.labelControlPLimitY2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlPLimitY2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlPLimitY2.Location = new System.Drawing.Point(146, 97);
            this.labelControlPLimitY2.Name = "labelControlPLimitY2";
            this.labelControlPLimitY2.Size = new System.Drawing.Size(60, 39);
            this.labelControlPLimitY2.StyleController = this.layoutControl12;
            this.labelControlPLimitY2.TabIndex = 12;
            this.labelControlPLimitY2.Text = "Limti+(Y2)";
            // 
            // labelControlHomeY2
            // 
            this.labelControlHomeY2.Appearance.Options.UseTextOptions = true;
            this.labelControlHomeY2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlHomeY2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlHomeY2.Location = new System.Drawing.Point(76, 97);
            this.labelControlHomeY2.Name = "labelControlHomeY2";
            this.labelControlHomeY2.Size = new System.Drawing.Size(66, 39);
            this.labelControlHomeY2.StyleController = this.layoutControl12;
            this.labelControlHomeY2.TabIndex = 11;
            this.labelControlHomeY2.Text = "Home(Y2)";
            // 
            // labelControlNLimit_Y2
            // 
            this.labelControlNLimit_Y2.Appearance.Options.UseTextOptions = true;
            this.labelControlNLimit_Y2.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlNLimit_Y2.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlNLimit_Y2.Location = new System.Drawing.Point(12, 97);
            this.labelControlNLimit_Y2.Name = "labelControlNLimit_Y2";
            this.labelControlNLimit_Y2.Size = new System.Drawing.Size(60, 39);
            this.labelControlNLimit_Y2.StyleController = this.layoutControl12;
            this.labelControlNLimit_Y2.TabIndex = 10;
            this.labelControlNLimit_Y2.Text = "-Limit(Y2)";
            // 
            // labelControlPLimitY1
            // 
            this.labelControlPLimitY1.Appearance.Options.UseTextOptions = true;
            this.labelControlPLimitY1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlPLimitY1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlPLimitY1.Location = new System.Drawing.Point(146, 55);
            this.labelControlPLimitY1.Name = "labelControlPLimitY1";
            this.labelControlPLimitY1.Size = new System.Drawing.Size(60, 38);
            this.labelControlPLimitY1.StyleController = this.layoutControl12;
            this.labelControlPLimitY1.TabIndex = 9;
            this.labelControlPLimitY1.Text = "Limti+(Y1)";
            // 
            // labelControlHomeY1
            // 
            this.labelControlHomeY1.Appearance.Options.UseTextOptions = true;
            this.labelControlHomeY1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlHomeY1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlHomeY1.Location = new System.Drawing.Point(76, 55);
            this.labelControlHomeY1.Name = "labelControlHomeY1";
            this.labelControlHomeY1.Size = new System.Drawing.Size(66, 38);
            this.labelControlHomeY1.StyleController = this.layoutControl12;
            this.labelControlHomeY1.TabIndex = 8;
            this.labelControlHomeY1.Text = "Home(Y1)";
            // 
            // labelControlNLimit_Y1
            // 
            this.labelControlNLimit_Y1.Appearance.Options.UseTextOptions = true;
            this.labelControlNLimit_Y1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlNLimit_Y1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlNLimit_Y1.Location = new System.Drawing.Point(12, 55);
            this.labelControlNLimit_Y1.Name = "labelControlNLimit_Y1";
            this.labelControlNLimit_Y1.Size = new System.Drawing.Size(60, 38);
            this.labelControlNLimit_Y1.StyleController = this.layoutControl12;
            this.labelControlNLimit_Y1.TabIndex = 7;
            this.labelControlNLimit_Y1.Text = "-Limit(Y1)";
            // 
            // labelControlPLimitX
            // 
            this.labelControlPLimitX.Appearance.Options.UseTextOptions = true;
            this.labelControlPLimitX.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlPLimitX.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlPLimitX.Location = new System.Drawing.Point(146, 12);
            this.labelControlPLimitX.Name = "labelControlPLimitX";
            this.labelControlPLimitX.Size = new System.Drawing.Size(60, 39);
            this.labelControlPLimitX.StyleController = this.layoutControl12;
            this.labelControlPLimitX.TabIndex = 6;
            this.labelControlPLimitX.Text = "Limti+(X)";
            // 
            // labelControlHomeX
            // 
            this.labelControlHomeX.Appearance.Options.UseTextOptions = true;
            this.labelControlHomeX.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlHomeX.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlHomeX.Location = new System.Drawing.Point(76, 12);
            this.labelControlHomeX.Name = "labelControlHomeX";
            this.labelControlHomeX.Size = new System.Drawing.Size(66, 39);
            this.labelControlHomeX.StyleController = this.layoutControl12;
            this.labelControlHomeX.TabIndex = 5;
            this.labelControlHomeX.Text = "Home(X)";
            // 
            // labelControlNLimit_X
            // 
            this.labelControlNLimit_X.Appearance.Options.UseTextOptions = true;
            this.labelControlNLimit_X.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.labelControlNLimit_X.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlNLimit_X.Location = new System.Drawing.Point(12, 12);
            this.labelControlNLimit_X.Name = "labelControlNLimit_X";
            this.labelControlNLimit_X.Size = new System.Drawing.Size(60, 39);
            this.labelControlNLimit_X.StyleController = this.layoutControl12;
            this.labelControlNLimit_X.TabIndex = 4;
            this.labelControlNLimit_X.Text = "-Limit(X)";
            // 
            // layoutControlGroup13
            // 
            this.layoutControlGroup13.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup13.GroupBordersVisible = false;
            this.layoutControlGroup13.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem67,
            this.layoutControlItem68,
            this.layoutControlItem69,
            this.layoutControlItem70,
            this.layoutControlItem73,
            this.layoutControlItem76,
            this.layoutControlItem71,
            this.layoutControlItem72,
            this.layoutControlItem74,
            this.layoutControlItem75,
            this.layoutControlItem77,
            this.layoutControlItem78,
            this.layoutControlItem79,
            this.layoutControlItem80,
            this.layoutControlItem81,
            this.layoutControlItem82,
            this.layoutControlItem83,
            this.layoutControlItem84});
            this.layoutControlGroup13.Name = "Root";
            this.layoutControlGroup13.Size = new System.Drawing.Size(218, 280);
            this.layoutControlGroup13.TextVisible = false;
            // 
            // layoutControlItem67
            // 
            this.layoutControlItem67.Control = this.labelControlNLimit_X;
            this.layoutControlItem67.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem67.MaxSize = new System.Drawing.Size(80, 58);
            this.layoutControlItem67.MinSize = new System.Drawing.Size(54, 26);
            this.layoutControlItem67.Name = "layoutControlItem67";
            this.layoutControlItem67.Size = new System.Drawing.Size(64, 43);
            this.layoutControlItem67.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem67.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem67.TextVisible = false;
            // 
            // layoutControlItem68
            // 
            this.layoutControlItem68.Control = this.labelControlHomeX;
            this.layoutControlItem68.Location = new System.Drawing.Point(64, 0);
            this.layoutControlItem68.MaxSize = new System.Drawing.Size(80, 58);
            this.layoutControlItem68.MinSize = new System.Drawing.Size(70, 21);
            this.layoutControlItem68.Name = "layoutControlItem68";
            this.layoutControlItem68.Size = new System.Drawing.Size(70, 43);
            this.layoutControlItem68.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem68.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem68.TextVisible = false;
            // 
            // layoutControlItem69
            // 
            this.layoutControlItem69.Control = this.labelControlPLimitX;
            this.layoutControlItem69.Location = new System.Drawing.Point(134, 0);
            this.layoutControlItem69.MaxSize = new System.Drawing.Size(82, 58);
            this.layoutControlItem69.MinSize = new System.Drawing.Size(54, 21);
            this.layoutControlItem69.Name = "layoutControlItem69";
            this.layoutControlItem69.Size = new System.Drawing.Size(64, 43);
            this.layoutControlItem69.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem69.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem69.TextVisible = false;
            // 
            // layoutControlItem70
            // 
            this.layoutControlItem70.Control = this.labelControlNLimit_Y1;
            this.layoutControlItem70.Location = new System.Drawing.Point(0, 43);
            this.layoutControlItem70.MaxSize = new System.Drawing.Size(80, 58);
            this.layoutControlItem70.MinSize = new System.Drawing.Size(58, 21);
            this.layoutControlItem70.Name = "layoutControlItem70";
            this.layoutControlItem70.Size = new System.Drawing.Size(64, 42);
            this.layoutControlItem70.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem70.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem70.TextVisible = false;
            // 
            // layoutControlItem73
            // 
            this.layoutControlItem73.Control = this.labelControlNLimit_Y2;
            this.layoutControlItem73.Location = new System.Drawing.Point(0, 85);
            this.layoutControlItem73.MaxSize = new System.Drawing.Size(80, 58);
            this.layoutControlItem73.MinSize = new System.Drawing.Size(58, 21);
            this.layoutControlItem73.Name = "layoutControlItem73";
            this.layoutControlItem73.Size = new System.Drawing.Size(64, 43);
            this.layoutControlItem73.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem73.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem73.TextVisible = false;
            // 
            // layoutControlItem76
            // 
            this.layoutControlItem76.Control = this.labelControlNLimit_Z;
            this.layoutControlItem76.Location = new System.Drawing.Point(0, 128);
            this.layoutControlItem76.MaxSize = new System.Drawing.Size(80, 58);
            this.layoutControlItem76.MinSize = new System.Drawing.Size(50, 21);
            this.layoutControlItem76.Name = "layoutControlItem76";
            this.layoutControlItem76.Size = new System.Drawing.Size(64, 43);
            this.layoutControlItem76.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem76.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem76.TextVisible = false;
            // 
            // layoutControlItem71
            // 
            this.layoutControlItem71.Control = this.labelControlHomeY1;
            this.layoutControlItem71.Location = new System.Drawing.Point(64, 43);
            this.layoutControlItem71.MaxSize = new System.Drawing.Size(80, 58);
            this.layoutControlItem71.MinSize = new System.Drawing.Size(61, 21);
            this.layoutControlItem71.Name = "layoutControlItem71";
            this.layoutControlItem71.Size = new System.Drawing.Size(70, 42);
            this.layoutControlItem71.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem71.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem71.TextVisible = false;
            // 
            // layoutControlItem72
            // 
            this.layoutControlItem72.Control = this.labelControlPLimitY1;
            this.layoutControlItem72.Location = new System.Drawing.Point(134, 43);
            this.layoutControlItem72.MaxSize = new System.Drawing.Size(80, 58);
            this.layoutControlItem72.MinSize = new System.Drawing.Size(62, 21);
            this.layoutControlItem72.Name = "layoutControlItem72";
            this.layoutControlItem72.Size = new System.Drawing.Size(64, 42);
            this.layoutControlItem72.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem72.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem72.TextVisible = false;
            // 
            // layoutControlItem74
            // 
            this.layoutControlItem74.Control = this.labelControlHomeY2;
            this.layoutControlItem74.Location = new System.Drawing.Point(64, 85);
            this.layoutControlItem74.MaxSize = new System.Drawing.Size(80, 58);
            this.layoutControlItem74.MinSize = new System.Drawing.Size(61, 21);
            this.layoutControlItem74.Name = "layoutControlItem74";
            this.layoutControlItem74.Size = new System.Drawing.Size(70, 43);
            this.layoutControlItem74.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem74.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem74.TextVisible = false;
            // 
            // layoutControlItem75
            // 
            this.layoutControlItem75.Control = this.labelControlPLimitY2;
            this.layoutControlItem75.Location = new System.Drawing.Point(134, 85);
            this.layoutControlItem75.MaxSize = new System.Drawing.Size(80, 58);
            this.layoutControlItem75.MinSize = new System.Drawing.Size(62, 21);
            this.layoutControlItem75.Name = "layoutControlItem75";
            this.layoutControlItem75.Size = new System.Drawing.Size(64, 43);
            this.layoutControlItem75.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem75.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem75.TextVisible = false;
            // 
            // layoutControlItem77
            // 
            this.layoutControlItem77.Control = this.labelControlHomeZ;
            this.layoutControlItem77.Location = new System.Drawing.Point(64, 128);
            this.layoutControlItem77.MaxSize = new System.Drawing.Size(80, 58);
            this.layoutControlItem77.MinSize = new System.Drawing.Size(53, 21);
            this.layoutControlItem77.Name = "layoutControlItem77";
            this.layoutControlItem77.Size = new System.Drawing.Size(70, 43);
            this.layoutControlItem77.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem77.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem77.TextVisible = false;
            // 
            // layoutControlItem78
            // 
            this.layoutControlItem78.Control = this.labelControlPLimitZ;
            this.layoutControlItem78.Location = new System.Drawing.Point(134, 128);
            this.layoutControlItem78.MaxSize = new System.Drawing.Size(80, 58);
            this.layoutControlItem78.MinSize = new System.Drawing.Size(54, 21);
            this.layoutControlItem78.Name = "layoutControlItem78";
            this.layoutControlItem78.Size = new System.Drawing.Size(64, 43);
            this.layoutControlItem78.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem78.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem78.TextVisible = false;
            // 
            // layoutControlItem79
            // 
            this.layoutControlItem79.Control = this.labelControlNLimit_FZ;
            this.layoutControlItem79.Location = new System.Drawing.Point(0, 171);
            this.layoutControlItem79.MaxSize = new System.Drawing.Size(80, 58);
            this.layoutControlItem79.MinSize = new System.Drawing.Size(56, 21);
            this.layoutControlItem79.Name = "layoutControlItem79";
            this.layoutControlItem79.Size = new System.Drawing.Size(64, 43);
            this.layoutControlItem79.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem79.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem79.TextVisible = false;
            // 
            // layoutControlItem80
            // 
            this.layoutControlItem80.Control = this.labelControlHomeFZ;
            this.layoutControlItem80.Location = new System.Drawing.Point(64, 171);
            this.layoutControlItem80.MaxSize = new System.Drawing.Size(80, 58);
            this.layoutControlItem80.MinSize = new System.Drawing.Size(59, 21);
            this.layoutControlItem80.Name = "layoutControlItem80";
            this.layoutControlItem80.Size = new System.Drawing.Size(70, 43);
            this.layoutControlItem80.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem80.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem80.TextVisible = false;
            // 
            // layoutControlItem81
            // 
            this.layoutControlItem81.Control = this.labelControlPLimitFZ;
            this.layoutControlItem81.Location = new System.Drawing.Point(134, 171);
            this.layoutControlItem81.MaxSize = new System.Drawing.Size(80, 58);
            this.layoutControlItem81.MinSize = new System.Drawing.Size(60, 21);
            this.layoutControlItem81.Name = "layoutControlItem81";
            this.layoutControlItem81.Size = new System.Drawing.Size(64, 43);
            this.layoutControlItem81.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem81.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem81.TextVisible = false;
            // 
            // layoutControlItem82
            // 
            this.layoutControlItem82.Control = this.labelControlNLimit_FR;
            this.layoutControlItem82.Location = new System.Drawing.Point(0, 214);
            this.layoutControlItem82.MaxSize = new System.Drawing.Size(80, 58);
            this.layoutControlItem82.MinSize = new System.Drawing.Size(56, 21);
            this.layoutControlItem82.Name = "layoutControlItem82";
            this.layoutControlItem82.Size = new System.Drawing.Size(64, 46);
            this.layoutControlItem82.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem82.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem82.TextVisible = false;
            // 
            // layoutControlItem83
            // 
            this.layoutControlItem83.Control = this.labelControlHomeFR;
            this.layoutControlItem83.Location = new System.Drawing.Point(64, 214);
            this.layoutControlItem83.MaxSize = new System.Drawing.Size(80, 58);
            this.layoutControlItem83.MinSize = new System.Drawing.Size(59, 21);
            this.layoutControlItem83.Name = "layoutControlItem83";
            this.layoutControlItem83.Size = new System.Drawing.Size(70, 46);
            this.layoutControlItem83.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem83.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem83.TextVisible = false;
            // 
            // layoutControlItem84
            // 
            this.layoutControlItem84.Control = this.labelControlPLimitFR;
            this.layoutControlItem84.Location = new System.Drawing.Point(134, 214);
            this.layoutControlItem84.MaxSize = new System.Drawing.Size(80, 58);
            this.layoutControlItem84.MinSize = new System.Drawing.Size(60, 21);
            this.layoutControlItem84.Name = "layoutControlItem84";
            this.layoutControlItem84.Size = new System.Drawing.Size(64, 46);
            this.layoutControlItem84.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem84.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem84.TextVisible = false;
            // 
            // layoutControlGroup12
            // 
            this.layoutControlGroup12.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup12.GroupBordersVisible = false;
            this.layoutControlGroup12.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem65,
            this.layoutControlItem66});
            this.layoutControlGroup12.Name = "layoutControlGroup12";
            this.layoutControlGroup12.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 4, 5, 5);
            this.layoutControlGroup12.Size = new System.Drawing.Size(477, 317);
            this.layoutControlGroup12.TextVisible = false;
            // 
            // layoutControlItem65
            // 
            this.layoutControlItem65.Control = this.groupControl3;
            this.layoutControlItem65.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem65.Name = "layoutControlItem65";
            this.layoutControlItem65.Size = new System.Drawing.Size(226, 307);
            this.layoutControlItem65.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem65.TextVisible = false;
            // 
            // layoutControlItem66
            // 
            this.layoutControlItem66.Control = this.groupControl4;
            this.layoutControlItem66.Location = new System.Drawing.Point(226, 0);
            this.layoutControlItem66.Name = "layoutControlItem66";
            this.layoutControlItem66.Size = new System.Drawing.Size(243, 307);
            this.layoutControlItem66.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem66.TextVisible = false;
            // 
            // layoutControlGroup11
            // 
            this.layoutControlGroup11.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup11.GroupBordersVisible = false;
            this.layoutControlGroup11.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem63,
            this.layoutControlItem64});
            this.layoutControlGroup11.Name = "layoutControlGroup11";
            this.layoutControlGroup11.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 4, 5, 5);
            this.layoutControlGroup11.Size = new System.Drawing.Size(493, 599);
            this.layoutControlGroup11.TextVisible = false;
            // 
            // layoutControlItem63
            // 
            this.layoutControlItem63.Control = this.groupControl1;
            this.layoutControlItem63.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem63.Name = "layoutControlItem63";
            this.layoutControlItem63.Size = new System.Drawing.Size(485, 344);
            this.layoutControlItem63.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem63.TextVisible = false;
            // 
            // layoutControlItem64
            // 
            this.layoutControlItem64.Control = this.groupControl2;
            this.layoutControlItem64.Location = new System.Drawing.Point(0, 344);
            this.layoutControlItem64.Name = "layoutControlItem64";
            this.layoutControlItem64.Size = new System.Drawing.Size(485, 245);
            this.layoutControlItem64.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem64.TextVisible = false;
            // 
            // layoutControlGroup10
            // 
            this.layoutControlGroup10.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup10.GroupBordersVisible = false;
            this.layoutControlGroup10.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem62});
            this.layoutControlGroup10.Name = "layoutControlGroup10";
            this.layoutControlGroup10.Size = new System.Drawing.Size(513, 619);
            this.layoutControlGroup10.TextVisible = false;
            // 
            // layoutControlItem62
            // 
            this.layoutControlItem62.Control = this.layoutControl10;
            this.layoutControlItem62.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem62.Name = "layoutControlItem62";
            this.layoutControlItem62.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem62.Size = new System.Drawing.Size(493, 599);
            this.layoutControlItem62.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem62.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.layoutControl3;
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.OptionsPrint.AppearanceItemCaption.BorderColor = System.Drawing.Color.Black;
            this.layoutControlItem3.OptionsPrint.AppearanceItemCaption.Options.UseBorderColor = true;
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem3.Size = new System.Drawing.Size(518, 115);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlGroup6
            // 
            this.layoutControlGroup6.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup6.GroupBordersVisible = false;
            this.layoutControlGroup6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup6.Name = "layoutControlGroup6";
            this.layoutControlGroup6.Size = new System.Drawing.Size(474, 92);
            this.layoutControlGroup6.TextVisible = false;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(85, 125);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(75, 21);
            this.textBox6.TabIndex = 4;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(237, 125);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(75, 21);
            this.textBox7.TabIndex = 5;
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(389, 125);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(76, 21);
            this.textBox8.TabIndex = 6;
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(85, 149);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(75, 21);
            this.textBox9.TabIndex = 7;
            // 
            // maskedTextBox2
            // 
            this.maskedTextBox2.Location = new System.Drawing.Point(237, 149);
            this.maskedTextBox2.Name = "maskedTextBox2";
            this.maskedTextBox2.Size = new System.Drawing.Size(75, 21);
            this.maskedTextBox2.TabIndex = 8;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(85, 173);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(151, 21);
            this.textBox2.TabIndex = 20;
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.Location = new System.Drawing.Point(313, 173);
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.Size = new System.Drawing.Size(152, 21);
            this.maskedTextBox1.TabIndex = 21;
            // 
            // checkButton51
            // 
            this.checkButton51.Location = new System.Drawing.Point(175, 79);
            this.checkButton51.Name = "checkButton51";
            this.checkButton51.Size = new System.Drawing.Size(135, 22);
            this.checkButton51.TabIndex = 28;
            this.checkButton51.Text = "■(정지)";
            // 
            // checkButton41
            // 
            this.checkButton41.Location = new System.Drawing.Point(36, 79);
            this.checkButton41.Name = "checkButton41";
            this.checkButton41.Size = new System.Drawing.Size(135, 22);
            this.checkButton41.TabIndex = 27;
            this.checkButton41.Text = "(-)《";
            // 
            // checkButton61
            // 
            this.checkButton61.Location = new System.Drawing.Point(314, 79);
            this.checkButton61.Name = "checkButton61";
            this.checkButton61.Size = new System.Drawing.Size(136, 22);
            this.checkButton61.TabIndex = 29;
            this.checkButton61.Text = "》(+)";
            // 
            // PLCRemoteControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.xtraTabControlPLCControl);
            this.Name = "MotionRemoteControl";
            this.Size = new System.Drawing.Size(548, 637);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControlPLCControl)).EndInit();
            this.xtraTabControlPLCControl.ResumeLayout(false);
            this.xtraTabConnectPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl3)).EndInit();
            this.layoutControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEditTcpPort.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditIpAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEditSendPLCData.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.memoEditMessageLog.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.xtraTabControlPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl2)).EndInit();
            this.layoutControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl5)).EndInit();
            this.layoutControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlPCLControl)).EndInit();
            this.groupControlPCLControl.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl6)).EndInit();
            this.layoutControl6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupCalibration.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditCalibration.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditTargetAcceleration.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditTargetVelocity.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditTargetPosFR.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditTargetPosFZ.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditTargetPosZ.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditTargetPosY2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditTargetPosY1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditTargetPosX.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl8)).EndInit();
            this.layoutControl8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEditUserDefineValue.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupMenualValueMode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem32)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem29)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem30)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem31)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem37)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem40)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem38)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem39)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem41)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem42)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem43)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem44)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem45)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem46)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem47)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem48)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem49)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem50)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem51)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem52)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem53)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem54)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem55)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem56)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem57)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem58)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem59)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem60)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxUserDefineValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupMenualControlMode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroupMenualMode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem27)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem26)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem28)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem35)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem61)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem153)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem154)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem155)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem33)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem20)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem118)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem22)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem24)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem34)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem36)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem156)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl4)).EndInit();
            this.layoutControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlPresentPosition)).EndInit();
            this.groupControlPresentPosition.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl7)).EndInit();
            this.layoutControl7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textEditPresentPosFR.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditPresentPosFZ.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditPresentPosZ.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditPresentPosY2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditPresentPosY1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEditPresentPosX.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem25)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            this.xtraTabStatusPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl9)).EndInit();
            this.layoutControl9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl10)).EndInit();
            this.layoutControl10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl14)).EndInit();
            this.layoutControl14.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl15)).EndInit();
            this.layoutControl15.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl6)).EndInit();
            this.groupControl6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl17)).EndInit();
            this.layoutControl17.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem119)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem137)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem138)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem139)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem140)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem141)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem142)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem143)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem144)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem145)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem146)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem147)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem148)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem149)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem150)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem151)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).EndInit();
            this.groupControl5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl16)).EndInit();
            this.layoutControl16.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem120)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem121)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem122)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem123)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem130)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem131)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem132)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem133)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem124)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem127)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem128)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem129)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem125)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem134)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem135)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem136)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem126)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem152)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem117)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl11)).EndInit();
            this.layoutControl11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
            this.groupControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl13)).EndInit();
            this.layoutControl13.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem85)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem86)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem87)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem88)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem89)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem91)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem92)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem93)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem94)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem95)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem96)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem97)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem98)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem101)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem90)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem99)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem100)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem102)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem103)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem104)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem105)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem106)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem107)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem108)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem109)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem110)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem111)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem112)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem113)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem114)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem115)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem116)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl12)).EndInit();
            this.layoutControl12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem67)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem68)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem69)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem70)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem73)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem76)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem71)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem72)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem74)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem75)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem77)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem78)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem79)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem80)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem81)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem82)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem83)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem84)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem65)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem66)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem63)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem64)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem62)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup6)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControlPLCControl;
        private DevExpress.XtraTab.XtraTabPage xtraTabConnectPage;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControl layoutControl3;
        private DevExpress.XtraEditors.SimpleButton PLCDisConnect;
        private DevExpress.XtraEditors.SimpleButton ConnectPLCButton;
        private DevExpress.XtraEditors.TextEdit textEditIpAddress;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraEditors.MemoEdit memoEditMessageLog;
        private DevExpress.XtraEditors.SimpleButton PLCSendDataButton;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraTab.XtraTabPage xtraTabControlPage;
        private DevExpress.XtraLayout.LayoutControl layoutControl2;
        private DevExpress.XtraLayout.LayoutControl layoutControl5;
        private DevExpress.XtraEditors.GroupControl groupControlPCLControl;
        private DevExpress.XtraLayout.LayoutControl layoutControl6;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup7;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem18;
        private DevExpress.XtraLayout.LayoutControl layoutControl4;
        private DevExpress.XtraEditors.GroupControl groupControlPresentPosition;
        private DevExpress.XtraLayout.LayoutControl layoutControl7;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup8;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem25;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup6;
        private DevExpress.XtraEditors.RadioGroup radioGroupMenualControlMode;
        private DevExpress.XtraEditors.RadioGroup radioGroupMenualMode;
        private DevExpress.XtraEditors.CheckButton checkButtonMenualMode;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem27;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem26;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem28;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.MaskedTextBox maskedTextBox2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.MaskedTextBox maskedTextBox1;
        private DevExpress.XtraEditors.SimpleButton SendCmdPositionButton;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem61;
        private DevExpress.XtraEditors.CheckButton checkButton51;
        private DevExpress.XtraEditors.CheckButton checkButton41;
        private DevExpress.XtraEditors.CheckButton checkButton61;
        private DevExpress.XtraLayout.LayoutControl layoutControl8;
        private DevExpress.XtraEditors.CheckButton CheckButtonFRPlusControlCommand;
        private DevExpress.XtraEditors.CheckButton CheckButtonFRStopControlCommand;
        private DevExpress.XtraEditors.CheckButton CheckButtonFRMinusControlCommand;
        private DevExpress.XtraEditors.CheckButton CheckButtonFZPlusControlCommand;
        private DevExpress.XtraEditors.CheckButton CheckButtonFZStopControlCommand;
        private DevExpress.XtraEditors.CheckButton CheckButtonFZMinusControlCommand;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.CheckButton CheckButtonZPlusControlCommand;
        private DevExpress.XtraEditors.CheckButton CheckButtonZStopControlCommand;
        private DevExpress.XtraEditors.CheckButton CheckButtonZMinusControlCommand;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.CheckButton CheckButtonY2PlusControlCommand;
        private DevExpress.XtraEditors.CheckButton CheckButtonY2StopControlCommand;
        private DevExpress.XtraEditors.CheckButton CheckButtonY2MinusControlCommand;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.CheckButton CheckButtonY1PlusControlCommand;
        private DevExpress.XtraEditors.CheckButton CheckButtonY1StopControlCommand;
        private DevExpress.XtraEditors.CheckButton CheckButtonY1MinusControlCommand;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.CheckButton CheckButtonXPlusControlCommand;
        private DevExpress.XtraEditors.CheckButton CheckButtonXStopControlCommand;
        private DevExpress.XtraEditors.CheckButton CheckButtonXMinusControlCommand;
        private DevExpress.XtraEditors.CheckButton checkButtonHighValue;
        private DevExpress.XtraEditors.CheckButton checkButtonMiddleValue;
        private DevExpress.XtraEditors.RadioGroup radioGroupMenualValueMode;
        private DevExpress.XtraEditors.CheckButton checkButtonLowValue;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem32;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem29;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem30;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem31;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem37;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem40;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem38;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem39;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem41;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem42;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem43;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem44;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem45;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem46;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem47;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem48;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem49;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem50;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem51;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem52;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem53;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem54;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem55;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem56;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem57;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem58;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem59;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem60;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem35;
        private DevExpress.XtraEditors.SimpleButton ErrorResetButton;
        private DevExpress.XtraEditors.SimpleButton EmergencyStopButton;
        private DevExpress.XtraEditors.SimpleButton RobotEnableButton;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem153;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem154;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem155;
        private DevExpress.XtraEditors.TextEdit textEditTcpPort;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.TextEdit textEditSendPLCData;
        private DevExpress.XtraLayout.LayoutControlItem textEdit;
        private DevExpress.XtraTab.XtraTabPage xtraTabStatusPage;
        private DevExpress.XtraLayout.LayoutControl layoutControl9;
        private DevExpress.XtraLayout.LayoutControl layoutControl10;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraLayout.LayoutControl layoutControl14;
        private DevExpress.XtraLayout.LayoutControl layoutControl15;
        private DevExpress.XtraEditors.GroupControl groupControl6;
        private DevExpress.XtraLayout.LayoutControl layoutControl17;
        private DevExpress.XtraEditors.LabelControl labelControlDIn16;
        private DevExpress.XtraEditors.LabelControl labelControlDIn15;
        private DevExpress.XtraEditors.LabelControl labelControlDIn14;
        private DevExpress.XtraEditors.LabelControl labelControlDIn12;
        private DevExpress.XtraEditors.LabelControl labelControlDIn11;
        private DevExpress.XtraEditors.LabelControl labelControlDIn10;
        private DevExpress.XtraEditors.LabelControl labelControlDIn8;
        private DevExpress.XtraEditors.LabelControl labelControlDIn7;
        private DevExpress.XtraEditors.LabelControl labelControlDIn6;
        private DevExpress.XtraEditors.LabelControl labelControlDIn13;
        private DevExpress.XtraEditors.LabelControl labelControlDIn9;
        private DevExpress.XtraEditors.LabelControl labelControlDIn5;
        private DevExpress.XtraEditors.LabelControl labelControlDIn4;
        private DevExpress.XtraEditors.LabelControl labelControlDIn3;
        private DevExpress.XtraEditors.LabelControl labelControlDIn2;
        private DevExpress.XtraEditors.LabelControl labelControlDIn1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup18;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem119;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem137;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem138;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem139;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem140;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem141;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem142;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem143;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem144;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem145;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem146;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem147;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem148;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem149;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem150;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem151;
        private DevExpress.XtraEditors.GroupControl groupControl5;
        private DevExpress.XtraLayout.LayoutControl layoutControl16;
        private DevExpress.XtraEditors.LabelControl labelControlDOut16;
        private DevExpress.XtraEditors.LabelControl labelControlDOut15;
        private DevExpress.XtraEditors.LabelControl labelControlDOut14;
        private DevExpress.XtraEditors.LabelControl labelControlDOut12;
        private DevExpress.XtraEditors.LabelControl labelControlDOut11;
        private DevExpress.XtraEditors.LabelControl labelControlDOut10;
        private DevExpress.XtraEditors.LabelControl labelControlDOut8;
        private DevExpress.XtraEditors.LabelControl labelControlDOut7;
        private DevExpress.XtraEditors.LabelControl labelControlDOut6;
        private DevExpress.XtraEditors.LabelControl labelControlDOut13;
        private DevExpress.XtraEditors.LabelControl labelControlDOut9;
        private DevExpress.XtraEditors.LabelControl labelControlDOut5;
        private DevExpress.XtraEditors.LabelControl labelControlDOut4;
        private DevExpress.XtraEditors.LabelControl labelControlDOut3;
        private DevExpress.XtraEditors.LabelControl labelControlDOut2;
        private DevExpress.XtraEditors.LabelControl labelControlDOut1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup17;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem120;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem121;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem122;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem123;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem130;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem131;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem132;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem133;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem124;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem127;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem128;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem129;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem125;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem134;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem135;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem136;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup16;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem126;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem152;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup15;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem117;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraLayout.LayoutControl layoutControl11;
        private DevExpress.XtraEditors.GroupControl groupControl4;
        private DevExpress.XtraLayout.LayoutControl layoutControl13;
        private DevExpress.XtraEditors.LabelControl labelControlPLCStatus32;
        private DevExpress.XtraEditors.LabelControl labelControlPLCStatus31;
        private DevExpress.XtraEditors.LabelControl labelControlPLCStatus30;
        private DevExpress.XtraEditors.LabelControl labelControlPLCStatus28;
        private DevExpress.XtraEditors.LabelControl labelControlPLCStatus27;
        private DevExpress.XtraEditors.LabelControl labelControlPLCStatus26;
        private DevExpress.XtraEditors.LabelControl labelControlPLCStatus24;
        private DevExpress.XtraEditors.LabelControl labelControlPLCStatus23;
        private DevExpress.XtraEditors.LabelControl labelControlPLCStatus22;
        private DevExpress.XtraEditors.LabelControl labelControlPLCStatus20;
        private DevExpress.XtraEditors.LabelControl labelControlPLCStatus19;
        private DevExpress.XtraEditors.LabelControl labelControlPLCStatus18;
        private DevExpress.XtraEditors.LabelControl labelControlPLCStatus16;
        private DevExpress.XtraEditors.LabelControl labelControlPLCStatus15;
        private DevExpress.XtraEditors.LabelControl labelControlPLCStatus14;
        private DevExpress.XtraEditors.LabelControl labelControlPLCStatus12;
        private DevExpress.XtraEditors.LabelControl labelControlPLCStatus11;
        private DevExpress.XtraEditors.LabelControl labelControlPLCStatus10;
        private DevExpress.XtraEditors.LabelControl labelControlPLCStatus8;
        private DevExpress.XtraEditors.LabelControl labelControlPLCStatus7;
        private DevExpress.XtraEditors.LabelControl labelControlPLCStatus6;
        private DevExpress.XtraEditors.LabelControl labelControlPLCStatus29;
        private DevExpress.XtraEditors.LabelControl labelControlPLCStatus25;
        private DevExpress.XtraEditors.LabelControl labelControlPLCStatus21;
        private DevExpress.XtraEditors.LabelControl labelControlPLCStatus17;
        private DevExpress.XtraEditors.LabelControl labelControlPLCStatus13;
        private DevExpress.XtraEditors.LabelControl labelControlPLCStatus9;
        private DevExpress.XtraEditors.LabelControl labelControlPLCStatus5;
        private DevExpress.XtraEditors.LabelControl labelControlPLCStatus4;
        private DevExpress.XtraEditors.LabelControl labelControlPLCStatus3;
        private DevExpress.XtraEditors.LabelControl labelControlPLCStatus2;
        private DevExpress.XtraEditors.LabelControl labelControlPLCStatus1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup14;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem85;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem86;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem87;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem88;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem89;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem91;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem92;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem93;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem94;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem95;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem96;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem97;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem98;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem101;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem90;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem99;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem100;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem102;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem103;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem104;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem105;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem106;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem107;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem108;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem109;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem110;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem111;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem112;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem113;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem114;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem115;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem116;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraLayout.LayoutControl layoutControl12;
        private DevExpress.XtraEditors.LabelControl labelControlPLimitFR;
        private DevExpress.XtraEditors.LabelControl labelControlHomeFR;
        private DevExpress.XtraEditors.LabelControl labelControlNLimit_FR;
        private DevExpress.XtraEditors.LabelControl labelControlPLimitFZ;
        private DevExpress.XtraEditors.LabelControl labelControlHomeFZ;
        private DevExpress.XtraEditors.LabelControl labelControlNLimit_FZ;
        private DevExpress.XtraEditors.LabelControl labelControlPLimitZ;
        private DevExpress.XtraEditors.LabelControl labelControlHomeZ;
        private DevExpress.XtraEditors.LabelControl labelControlNLimit_Z;
        private DevExpress.XtraEditors.LabelControl labelControlPLimitY2;
        private DevExpress.XtraEditors.LabelControl labelControlHomeY2;
        private DevExpress.XtraEditors.LabelControl labelControlNLimit_Y2;
        private DevExpress.XtraEditors.LabelControl labelControlPLimitY1;
        private DevExpress.XtraEditors.LabelControl labelControlHomeY1;
        private DevExpress.XtraEditors.LabelControl labelControlNLimit_Y1;
        private DevExpress.XtraEditors.LabelControl labelControlPLimitX;
        private DevExpress.XtraEditors.LabelControl labelControlHomeX;
        private DevExpress.XtraEditors.LabelControl labelControlNLimit_X;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup13;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem67;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem68;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem69;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem70;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem73;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem76;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem71;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem72;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem74;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem75;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem77;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem78;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem79;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem80;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem81;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem82;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem83;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem84;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup12;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem65;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem66;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup11;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem63;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem64;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup10;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem62;
        private DevExpress.XtraEditors.SimpleButton SendCommandMoveStopButton;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraEditors.TextEdit textEditUserDefineValue;
        private DevExpress.XtraLayout.LayoutControlItem textBoxUserDefineValue;
        private DevExpress.XtraEditors.SimpleButton SendCmdHommingButton;
        private DevExpress.XtraEditors.TextEdit textEditTargetAcceleration;
        private DevExpress.XtraEditors.TextEdit textEditTargetVelocity;
        private DevExpress.XtraEditors.TextEdit textEditTargetPosFR;
        private DevExpress.XtraEditors.TextEdit textEditTargetPosFZ;
        private DevExpress.XtraEditors.TextEdit textEditTargetPosZ;
        private DevExpress.XtraEditors.TextEdit textEditTargetPosY2;
        private DevExpress.XtraEditors.TextEdit textEditTargetPosY1;
        private DevExpress.XtraEditors.TextEdit textEditTargetPosX;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem19;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem20;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem118;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem21;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem22;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem23;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem24;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem34;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem33;
        private DevExpress.XtraEditors.RadioGroup radioGroupCalibration;
        private DevExpress.XtraEditors.CheckEdit checkEditCalibration;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem36;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem156;
        private DevExpress.XtraEditors.TextEdit textEditPresentPosX;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraEditors.TextEdit textEditPresentPosFR;
        private DevExpress.XtraEditors.TextEdit textEditPresentPosFZ;
        private DevExpress.XtraEditors.TextEdit textEditPresentPosZ;
        private DevExpress.XtraEditors.TextEdit textEditPresentPosY2;
        private DevExpress.XtraEditors.TextEdit textEditPresentPosY1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem15;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem16;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem17;
    }
}
