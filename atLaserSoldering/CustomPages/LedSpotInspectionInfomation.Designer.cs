namespace CustomPages
{
    partial class LedSpotInspectionInfomation
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.dataLayoutControl1 = new DevExpress.XtraDataLayout.DataLayoutControl();
            this.vGridControl1 = new DevExpress.XtraVerticalGrid.VGridControl();
            this.categoryImageProcessResult = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            this.rowProductSeries = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowProductModelName = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowLedSpotBlobInspectHSize = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowLedSpotBlobInspectVSize = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowLedSpotImageBright = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowAlignmentDistance = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.categoryOpticalEccentricAngle = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            this.rowOpticalEccentricAngle = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowOpticalEccentricHorizonAngle = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowOpticalEccentricVirticalAngle = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowDivergenceAngle = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowNDReduceRatio = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowNDFilterAngle = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowOperateMinDistance = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowOperateMaxDistance = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowInspectResult = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).BeginInit();
            this.dataLayoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowOnly;
            this.layoutControl1.Controls.Add(this.dataLayoutControl1);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Margin = new System.Windows.Forms.Padding(1);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(327, 326);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // dataLayoutControl1
            // 
            this.dataLayoutControl1.Controls.Add(this.vGridControl1);
            this.dataLayoutControl1.Location = new System.Drawing.Point(0, 0);
            this.dataLayoutControl1.Name = "dataLayoutControl1";
            this.dataLayoutControl1.OptionsView.UseDefaultDragAndDropRendering = false;
            this.dataLayoutControl1.Root = this.layoutControlGroup2;
            this.dataLayoutControl1.Size = new System.Drawing.Size(327, 326);
            this.dataLayoutControl1.TabIndex = 5;
            this.dataLayoutControl1.Text = "dataLayoutControl1";
            // 
            // vGridControl1
            // 
            this.vGridControl1.Appearance.BandBorder.BackColor = System.Drawing.Color.Transparent;
            this.vGridControl1.Appearance.BandBorder.Options.UseBackColor = true;
            this.vGridControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.vGridControl1.Cursor = System.Windows.Forms.Cursors.SizeNS;
            this.vGridControl1.LayoutStyle = DevExpress.XtraVerticalGrid.LayoutViewStyle.SingleRecordView;
            this.vGridControl1.Location = new System.Drawing.Point(3, 3);
            this.vGridControl1.Margin = new System.Windows.Forms.Padding(0);
            this.vGridControl1.Name = "vGridControl1";
            this.vGridControl1.RecordWidth = 72;
            this.vGridControl1.RowHeaderWidth = 128;
            this.vGridControl1.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.categoryImageProcessResult});
            this.vGridControl1.Size = new System.Drawing.Size(321, 320);
            this.vGridControl1.TabIndex = 4;
            // 
            // categoryImageProcessResult
            // 
            this.categoryImageProcessResult.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rowProductSeries,
            this.rowProductModelName,
            this.rowLedSpotBlobInspectHSize,
            this.rowLedSpotBlobInspectVSize,
            this.rowLedSpotImageBright,
            this.rowAlignmentDistance,
            this.categoryOpticalEccentricAngle,
            this.rowDivergenceAngle,
            this.rowNDReduceRatio,
            this.rowNDFilterAngle,
            this.rowOperateMinDistance,
            this.rowOperateMaxDistance,
            this.rowInspectResult});
            this.categoryImageProcessResult.Height = 19;
            this.categoryImageProcessResult.Name = "categoryImageProcessResult";
            this.categoryImageProcessResult.Properties.Caption = "투광 편심각 검사 결과";
            // 
            // rowProductSeries
            // 
            this.rowProductSeries.Appearance.Options.UseFont = true;
            this.rowProductSeries.Appearance.Options.UseTextOptions = true;
            this.rowProductSeries.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.rowProductSeries.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.rowProductSeries.Name = "rowProductSeries";
            this.rowProductSeries.Properties.Caption = "제품 시리즈";
            this.rowProductSeries.Properties.ReadOnly = true;
            // 
            // rowProductModelName
            // 
            this.rowProductModelName.Appearance.Options.UseFont = true;
            this.rowProductModelName.Appearance.Options.UseTextOptions = true;
            this.rowProductModelName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.rowProductModelName.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.rowProductModelName.Name = "rowProductModelName";
            this.rowProductModelName.Properties.Caption = "제품 모델명";
            this.rowProductModelName.Properties.ReadOnly = true;
            // 
            // rowLedSpotBlobInspectHSize
            // 
            this.rowLedSpotBlobInspectHSize.Appearance.Options.UseFont = true;
            this.rowLedSpotBlobInspectHSize.Appearance.Options.UseTextOptions = true;
            this.rowLedSpotBlobInspectHSize.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.rowLedSpotBlobInspectHSize.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.rowLedSpotBlobInspectHSize.Name = "rowLedSpotBlobInspectHSize";
            this.rowLedSpotBlobInspectHSize.Properties.Caption = "Spot 1 크기[mm]";
            this.rowLedSpotBlobInspectHSize.Properties.ReadOnly = true;
            // 
            // rowLedSpotBlobInspectVSize
            // 
            this.rowLedSpotBlobInspectVSize.Appearance.Options.UseTextOptions = true;
            this.rowLedSpotBlobInspectVSize.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.rowLedSpotBlobInspectVSize.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.rowLedSpotBlobInspectVSize.Name = "rowLedSpotBlobInspectVSize";
            this.rowLedSpotBlobInspectVSize.Properties.Caption = "Spot 2 크기[mm]";
            this.rowLedSpotBlobInspectVSize.Properties.ReadOnly = true;
            // 
            // rowLedSpotImageBright
            // 
            this.rowLedSpotImageBright.Appearance.Options.UseFont = true;
            this.rowLedSpotImageBright.Appearance.Options.UseTextOptions = true;
            this.rowLedSpotImageBright.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.rowLedSpotImageBright.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.rowLedSpotImageBright.Name = "rowLedSpotImageBright";
            this.rowLedSpotImageBright.Properties.Caption = "광원 이미지 밝기";
            this.rowLedSpotImageBright.Properties.ReadOnly = true;
            // 
            // rowAlignmentDistance
            // 
            this.rowAlignmentDistance.Appearance.Options.UseFont = true;
            this.rowAlignmentDistance.Appearance.Options.UseTextOptions = true;
            this.rowAlignmentDistance.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.rowAlignmentDistance.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.rowAlignmentDistance.Name = "rowAlignmentDistance";
            this.rowAlignmentDistance.Properties.Caption = "편심 거리[mm]";
            this.rowAlignmentDistance.Properties.ReadOnly = true;
            // 
            // categoryOpticalEccentricAngle
            // 
            this.categoryOpticalEccentricAngle.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rowOpticalEccentricAngle,
            this.rowOpticalEccentricHorizonAngle,
            this.rowOpticalEccentricVirticalAngle});
            this.categoryOpticalEccentricAngle.Name = "categoryOpticalEccentricAngle";
            this.categoryOpticalEccentricAngle.Properties.Caption = "편심각도";
            // 
            // rowOpticalEccentricAngle
            // 
            this.rowOpticalEccentricAngle.Appearance.Options.UseTextOptions = true;
            this.rowOpticalEccentricAngle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.rowOpticalEccentricAngle.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.rowOpticalEccentricAngle.Name = "rowOpticalEccentricAngle";
            this.rowOpticalEccentricAngle.Properties.Caption = "편심 각도[˚]";
            this.rowOpticalEccentricAngle.Properties.ReadOnly = true;
            // 
            // rowOpticalEccentricHorizoneAngle
            // 
            this.rowOpticalEccentricHorizonAngle.Appearance.Options.UseTextOptions = true;
            this.rowOpticalEccentricHorizonAngle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.rowOpticalEccentricHorizonAngle.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.rowOpticalEccentricHorizonAngle.Name = "rowOpticalEccentricHorizoneAngle";
            this.rowOpticalEccentricHorizonAngle.Properties.Caption = "편심 수평각도";
            this.rowOpticalEccentricHorizonAngle.Properties.ReadOnly = true;
            // 
            // rowOpticalEccentricVirticalAngle
            // 
            this.rowOpticalEccentricVirticalAngle.Appearance.Options.UseTextOptions = true;
            this.rowOpticalEccentricVirticalAngle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.rowOpticalEccentricVirticalAngle.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.rowOpticalEccentricVirticalAngle.Name = "rowOpticalEccentricVirticalAngle";
            this.rowOpticalEccentricVirticalAngle.Properties.Caption = "편심 수직각도";
            this.rowOpticalEccentricVirticalAngle.Properties.ReadOnly = true;
            // 
            // rowDivergenceAngle
            // 
            this.rowDivergenceAngle.Appearance.Options.UseFont = true;
            this.rowDivergenceAngle.Appearance.Options.UseTextOptions = true;
            this.rowDivergenceAngle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.rowDivergenceAngle.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.rowDivergenceAngle.Name = "rowDivergenceAngle";
            this.rowDivergenceAngle.Properties.Caption = "광원 발산 각도[˚]";
            this.rowDivergenceAngle.Properties.ReadOnly = true;
            // 
            // rowNDReduceRatio
            // 
            this.rowNDReduceRatio.Appearance.Options.UseFont = true;
            this.rowNDReduceRatio.Appearance.Options.UseTextOptions = true;
            this.rowNDReduceRatio.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.rowNDReduceRatio.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.rowNDReduceRatio.Name = "rowNDReduceRatio";
            this.rowNDReduceRatio.Properties.Caption = "ND 필터감쇄율";
            this.rowNDReduceRatio.Properties.ReadOnly = true;
            // 
            // rowNDFilterAngle
            // 
            this.rowNDFilterAngle.Appearance.Options.UseFont = true;
            this.rowNDFilterAngle.Appearance.Options.UseTextOptions = true;
            this.rowNDFilterAngle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.rowNDFilterAngle.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.rowNDFilterAngle.Name = "rowNDFilterAngle";
            this.rowNDFilterAngle.Properties.Caption = "ND 필터 예측각도";
            this.rowNDFilterAngle.Properties.ReadOnly = true;
            // 
            // rowOperateMinDistance
            // 
            this.rowOperateMinDistance.Appearance.Options.UseFont = true;
            this.rowOperateMinDistance.Appearance.Options.UseTextOptions = true;
            this.rowOperateMinDistance.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.rowOperateMinDistance.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.rowOperateMinDistance.Height = 17;
            this.rowOperateMinDistance.Name = "rowOperateMinDistance";
            this.rowOperateMinDistance.Properties.Caption = "동작 거리[ND 필터 각도]";
            this.rowOperateMinDistance.Properties.ReadOnly = true;
            // 
            // rowOperateMaxDistance
            // 
            this.rowOperateMaxDistance.Appearance.Options.UseFont = true;
            this.rowOperateMaxDistance.Appearance.Options.UseTextOptions = true;
            this.rowOperateMaxDistance.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.rowOperateMaxDistance.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.rowOperateMaxDistance.Name = "rowOperateMaxDistance";
            this.rowOperateMaxDistance.Properties.Caption = "최대 거리[ND 필터 각도]";
            this.rowOperateMaxDistance.Properties.ReadOnly = true;
            // 
            // rowInspectResult
            // 
            this.rowInspectResult.Appearance.Options.UseFont = true;
            this.rowInspectResult.Appearance.Options.UseTextOptions = true;
            this.rowInspectResult.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.rowInspectResult.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.rowInspectResult.Height = 18;
            this.rowInspectResult.Name = "rowInspectResult";
            this.rowInspectResult.OptionsRow.AllowFocus = false;
            this.rowInspectResult.Properties.Caption = "투광 검사 결과";
            this.rowInspectResult.Properties.ReadOnly = true;
            this.rowInspectResult.Properties.Value = "";
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup2.GroupBordersVisible = false;
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.layoutControlGroup2.Size = new System.Drawing.Size(327, 326);
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.vGridControl1;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(325, 324);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.Root.Size = new System.Drawing.Size(327, 326);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.dataLayoutControl1;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem2.Size = new System.Drawing.Size(327, 326);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // LedSpotInspectionInfomation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.layoutControl1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "LedSpotInspectionInfomation";
            this.Size = new System.Drawing.Size(327, 326);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataLayoutControl1)).EndInit();
            this.dataLayoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.vGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
                
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraDataLayout.DataLayoutControl dataLayoutControl1;
        private DevExpress.XtraVerticalGrid.VGridControl vGridControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraVerticalGrid.Rows.CategoryRow categoryImageProcessResult;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowProductSeries;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowProductModelName;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowLedSpotBlobInspectHSize;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowLedSpotBlobInspectVSize;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowAlignmentDistance;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowDivergenceAngle;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowInspectResult;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowNDReduceRatio;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowLedSpotImageBright;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowOperateMinDistance;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowOperateMaxDistance;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowNDFilterAngle;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowOpticalEccentricAngle;
        private DevExpress.XtraVerticalGrid.Rows.CategoryRow categoryOpticalEccentricAngle;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowOpticalEccentricHorizonAngle;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowOpticalEccentricVirticalAngle;
    }
}
