namespace WinFormsSample
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            groupBox_Preview = new GroupBox();
            label_Preview_Min = new Label();
            label_Preview_Avrg = new Label();
            label_Preview_Max = new Label();
            label_Preview_MinName = new Label();
            label_Preview_AvrgName = new Label();
            label_Preview_MaxName = new Label();
            pictureBox_CmosPreview = new PictureBox();
            pictureBox_ThermalPreview = new PictureBox();
            groupBox_Broadcast = new GroupBox();
            label_Broadcast_Count = new Label();
            label_Broadcast_CountName = new Label();
            listView_Broadcast = new ListView();
            groupBox_SelectedDevice = new GroupBox();
            button_SelectedDevice_Connect = new Button();
            labell_SelectedDevice_Gateway = new Label();
            labell_SelectedDevice_Port = new Label();
            labell_SelectedDevice_Netmask = new Label();
            labell_SelectedDevice_Ip = new Label();
            label_SelectedDevice_Version = new Label();
            groupBox_AddManual = new GroupBox();
            comboBox1 = new ComboBox();
            button_AddManual_Connect = new Button();
            textBox_AddManual_Port = new TextBox();
            textBox_AddManual_Ip = new TextBox();
            groupBox_Control = new GroupBox();
            button_Control_Disconnect = new Button();
            button_Control_ActiveOnceShutter = new Button();
            button_Control_Offset_Set = new Button();
            button_Control_Offset_Get = new Button();
            textBox_Control_Offset = new TextBox();
            comboBox_Control_PseudoColor = new ComboBox();
            label_Control_Offset = new Label();
            label_Control_PseudoColor = new Label();
            groupBox_Preview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox_CmosPreview).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_ThermalPreview).BeginInit();
            groupBox_Broadcast.SuspendLayout();
            groupBox_SelectedDevice.SuspendLayout();
            groupBox_AddManual.SuspendLayout();
            groupBox_Control.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox_Preview
            // 
            groupBox_Preview.Controls.Add(label_Preview_Min);
            groupBox_Preview.Controls.Add(label_Preview_Avrg);
            groupBox_Preview.Controls.Add(label_Preview_Max);
            groupBox_Preview.Controls.Add(label_Preview_MinName);
            groupBox_Preview.Controls.Add(label_Preview_AvrgName);
            groupBox_Preview.Controls.Add(label_Preview_MaxName);
            groupBox_Preview.Controls.Add(pictureBox_CmosPreview);
            groupBox_Preview.Controls.Add(pictureBox_ThermalPreview);
            groupBox_Preview.Location = new Point(12, 12);
            groupBox_Preview.Name = "groupBox_Preview";
            groupBox_Preview.Size = new Size(369, 413);
            groupBox_Preview.TabIndex = 0;
            groupBox_Preview.TabStop = false;
            groupBox_Preview.Text = "Preview";
            // 
            // label_Preview_Min
            // 
            label_Preview_Min.AutoSize = true;
            label_Preview_Min.Location = new Point(211, 22);
            label_Preview_Min.Name = "label_Preview_Min";
            label_Preview_Min.Size = new Size(14, 15);
            label_Preview_Min.TabIndex = 9;
            label_Preview_Min.Text = "0";
            // 
            // label_Preview_Avrg
            // 
            label_Preview_Avrg.AutoSize = true;
            label_Preview_Avrg.Location = new Point(135, 22);
            label_Preview_Avrg.Name = "label_Preview_Avrg";
            label_Preview_Avrg.Size = new Size(14, 15);
            label_Preview_Avrg.TabIndex = 8;
            label_Preview_Avrg.Text = "0";
            // 
            // label_Preview_Max
            // 
            label_Preview_Max.AutoSize = true;
            label_Preview_Max.Location = new Point(53, 22);
            label_Preview_Max.Name = "label_Preview_Max";
            label_Preview_Max.Size = new Size(14, 15);
            label_Preview_Max.TabIndex = 7;
            label_Preview_Max.Text = "0";
            // 
            // label_Preview_MinName
            // 
            label_Preview_MinName.AutoSize = true;
            label_Preview_MinName.Location = new Point(166, 22);
            label_Preview_MinName.Name = "label_Preview_MinName";
            label_Preview_MinName.Size = new Size(39, 15);
            label_Preview_MinName.TabIndex = 6;
            label_Preview_MinName.Text = "Min : ";
            // 
            // label_Preview_AvrgName
            // 
            label_Preview_AvrgName.AutoSize = true;
            label_Preview_AvrgName.Location = new Point(86, 22);
            label_Preview_AvrgName.Name = "label_Preview_AvrgName";
            label_Preview_AvrgName.Size = new Size(43, 15);
            label_Preview_AvrgName.TabIndex = 5;
            label_Preview_AvrgName.Text = "Avrg : ";
            // 
            // label_Preview_MaxName
            // 
            label_Preview_MaxName.AutoSize = true;
            label_Preview_MaxName.Location = new Point(6, 22);
            label_Preview_MaxName.Name = "label_Preview_MaxName";
            label_Preview_MaxName.Size = new Size(41, 15);
            label_Preview_MaxName.TabIndex = 4;
            label_Preview_MaxName.Text = "Max : ";
            // 
            // pictureBox_CmosPreview
            // 
            pictureBox_CmosPreview.Location = new Point(6, 228);
            pictureBox_CmosPreview.Name = "pictureBox_CmosPreview";
            pictureBox_CmosPreview.Size = new Size(357, 178);
            pictureBox_CmosPreview.TabIndex = 1;
            pictureBox_CmosPreview.TabStop = false;
            // 
            // pictureBox_ThermalPreview
            // 
            pictureBox_ThermalPreview.Location = new Point(6, 47);
            pictureBox_ThermalPreview.Name = "pictureBox_ThermalPreview";
            pictureBox_ThermalPreview.Size = new Size(357, 175);
            pictureBox_ThermalPreview.TabIndex = 0;
            pictureBox_ThermalPreview.TabStop = false;
            // 
            // groupBox_Broadcast
            // 
            groupBox_Broadcast.Controls.Add(label_Broadcast_Count);
            groupBox_Broadcast.Controls.Add(label_Broadcast_CountName);
            groupBox_Broadcast.Controls.Add(listView_Broadcast);
            groupBox_Broadcast.Location = new Point(387, 12);
            groupBox_Broadcast.Name = "groupBox_Broadcast";
            groupBox_Broadcast.Size = new Size(211, 274);
            groupBox_Broadcast.TabIndex = 1;
            groupBox_Broadcast.TabStop = false;
            groupBox_Broadcast.Text = "Broadcast";
            // 
            // label_Broadcast_Count
            // 
            label_Broadcast_Count.AutoSize = true;
            label_Broadcast_Count.Location = new Point(184, 22);
            label_Broadcast_Count.Name = "label_Broadcast_Count";
            label_Broadcast_Count.Size = new Size(14, 15);
            label_Broadcast_Count.TabIndex = 4;
            label_Broadcast_Count.Text = "0";
            // 
            // label_Broadcast_CountName
            // 
            label_Broadcast_CountName.AutoSize = true;
            label_Broadcast_CountName.Location = new Point(127, 22);
            label_Broadcast_CountName.Name = "label_Broadcast_CountName";
            label_Broadcast_CountName.Size = new Size(51, 15);
            label_Broadcast_CountName.TabIndex = 3;
            label_Broadcast_CountName.Text = "Count : ";
            // 
            // listView_Broadcast
            // 
            listView_Broadcast.Location = new Point(6, 47);
            listView_Broadcast.Name = "listView_Broadcast";
            listView_Broadcast.Size = new Size(199, 221);
            listView_Broadcast.TabIndex = 0;
            listView_Broadcast.UseCompatibleStateImageBehavior = false;
            // 
            // groupBox_SelectedDevice
            // 
            groupBox_SelectedDevice.Controls.Add(button_SelectedDevice_Connect);
            groupBox_SelectedDevice.Controls.Add(labell_SelectedDevice_Gateway);
            groupBox_SelectedDevice.Controls.Add(labell_SelectedDevice_Port);
            groupBox_SelectedDevice.Controls.Add(labell_SelectedDevice_Netmask);
            groupBox_SelectedDevice.Controls.Add(labell_SelectedDevice_Ip);
            groupBox_SelectedDevice.Controls.Add(label_SelectedDevice_Version);
            groupBox_SelectedDevice.Location = new Point(604, 12);
            groupBox_SelectedDevice.Name = "groupBox_SelectedDevice";
            groupBox_SelectedDevice.Size = new Size(168, 130);
            groupBox_SelectedDevice.TabIndex = 2;
            groupBox_SelectedDevice.TabStop = false;
            groupBox_SelectedDevice.Text = "Selected Device";
            // 
            // button_SelectedDevice_Connect
            // 
            button_SelectedDevice_Connect.Location = new Point(6, 100);
            button_SelectedDevice_Connect.Name = "button_SelectedDevice_Connect";
            button_SelectedDevice_Connect.Size = new Size(156, 23);
            button_SelectedDevice_Connect.TabIndex = 7;
            button_SelectedDevice_Connect.Text = "Connect";
            button_SelectedDevice_Connect.UseVisualStyleBackColor = true;
            // 
            // labell_SelectedDevice_Gateway
            // 
            labell_SelectedDevice_Gateway.AutoSize = true;
            labell_SelectedDevice_Gateway.Location = new Point(6, 82);
            labell_SelectedDevice_Gateway.Name = "labell_SelectedDevice_Gateway";
            labell_SelectedDevice_Gateway.Size = new Size(52, 15);
            labell_SelectedDevice_Gateway.TabIndex = 6;
            labell_SelectedDevice_Gateway.Text = "Gateway";
            // 
            // labell_SelectedDevice_Port
            // 
            labell_SelectedDevice_Port.AutoSize = true;
            labell_SelectedDevice_Port.Location = new Point(6, 67);
            labell_SelectedDevice_Port.Name = "labell_SelectedDevice_Port";
            labell_SelectedDevice_Port.Size = new Size(29, 15);
            labell_SelectedDevice_Port.TabIndex = 5;
            labell_SelectedDevice_Port.Text = "Port";
            // 
            // labell_SelectedDevice_Netmask
            // 
            labell_SelectedDevice_Netmask.AutoSize = true;
            labell_SelectedDevice_Netmask.Location = new Point(6, 52);
            labell_SelectedDevice_Netmask.Name = "labell_SelectedDevice_Netmask";
            labell_SelectedDevice_Netmask.Size = new Size(54, 15);
            labell_SelectedDevice_Netmask.TabIndex = 4;
            labell_SelectedDevice_Netmask.Text = "Netmask";
            // 
            // labell_SelectedDevice_Ip
            // 
            labell_SelectedDevice_Ip.AutoSize = true;
            labell_SelectedDevice_Ip.Location = new Point(6, 37);
            labell_SelectedDevice_Ip.Name = "labell_SelectedDevice_Ip";
            labell_SelectedDevice_Ip.Size = new Size(17, 15);
            labell_SelectedDevice_Ip.TabIndex = 3;
            labell_SelectedDevice_Ip.Text = "Ip";
            // 
            // label_SelectedDevice_Version
            // 
            label_SelectedDevice_Version.AutoSize = true;
            label_SelectedDevice_Version.Location = new Point(6, 22);
            label_SelectedDevice_Version.Name = "label_SelectedDevice_Version";
            label_SelectedDevice_Version.Size = new Size(47, 15);
            label_SelectedDevice_Version.TabIndex = 2;
            label_SelectedDevice_Version.Text = "Version";
            // 
            // groupBox_AddManual
            // 
            groupBox_AddManual.Controls.Add(comboBox1);
            groupBox_AddManual.Controls.Add(button_AddManual_Connect);
            groupBox_AddManual.Controls.Add(textBox_AddManual_Port);
            groupBox_AddManual.Controls.Add(textBox_AddManual_Ip);
            groupBox_AddManual.Location = new Point(604, 148);
            groupBox_AddManual.Name = "groupBox_AddManual";
            groupBox_AddManual.Size = new Size(168, 138);
            groupBox_AddManual.TabIndex = 3;
            groupBox_AddManual.TabStop = false;
            groupBox_AddManual.Text = "Add Manual";
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(6, 80);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(156, 23);
            comboBox1.TabIndex = 13;
            // 
            // button_AddManual_Connect
            // 
            button_AddManual_Connect.Location = new Point(6, 109);
            button_AddManual_Connect.Name = "button_AddManual_Connect";
            button_AddManual_Connect.Size = new Size(156, 23);
            button_AddManual_Connect.TabIndex = 8;
            button_AddManual_Connect.Text = "Connect";
            button_AddManual_Connect.UseVisualStyleBackColor = true;
            // 
            // textBox_AddManual_Port
            // 
            textBox_AddManual_Port.Location = new Point(6, 51);
            textBox_AddManual_Port.Name = "textBox_AddManual_Port";
            textBox_AddManual_Port.Size = new Size(156, 23);
            textBox_AddManual_Port.TabIndex = 1;
            // 
            // textBox_AddManual_Ip
            // 
            textBox_AddManual_Ip.Location = new Point(6, 22);
            textBox_AddManual_Ip.Name = "textBox_AddManual_Ip";
            textBox_AddManual_Ip.Size = new Size(156, 23);
            textBox_AddManual_Ip.TabIndex = 0;
            // 
            // groupBox_Control
            // 
            groupBox_Control.Controls.Add(button_Control_Disconnect);
            groupBox_Control.Controls.Add(button_Control_ActiveOnceShutter);
            groupBox_Control.Controls.Add(button_Control_Offset_Set);
            groupBox_Control.Controls.Add(button_Control_Offset_Get);
            groupBox_Control.Controls.Add(textBox_Control_Offset);
            groupBox_Control.Controls.Add(comboBox_Control_PseudoColor);
            groupBox_Control.Controls.Add(label_Control_Offset);
            groupBox_Control.Controls.Add(label_Control_PseudoColor);
            groupBox_Control.Location = new Point(387, 292);
            groupBox_Control.Name = "groupBox_Control";
            groupBox_Control.Size = new Size(385, 133);
            groupBox_Control.TabIndex = 3;
            groupBox_Control.TabStop = false;
            groupBox_Control.Text = "Control";
            // 
            // button_Control_Disconnect
            // 
            button_Control_Disconnect.Location = new Point(6, 103);
            button_Control_Disconnect.Name = "button_Control_Disconnect";
            button_Control_Disconnect.Size = new Size(373, 23);
            button_Control_Disconnect.TabIndex = 12;
            button_Control_Disconnect.Text = "Disconnect";
            button_Control_Disconnect.UseVisualStyleBackColor = true;
            // 
            // button_Control_ActiveOnceShutter
            // 
            button_Control_ActiveOnceShutter.Location = new Point(6, 74);
            button_Control_ActiveOnceShutter.Name = "button_Control_ActiveOnceShutter";
            button_Control_ActiveOnceShutter.Size = new Size(373, 23);
            button_Control_ActiveOnceShutter.TabIndex = 11;
            button_Control_ActiveOnceShutter.Text = "Active Once Shutter";
            button_Control_ActiveOnceShutter.UseVisualStyleBackColor = true;
            // 
            // button_Control_Offset_Set
            // 
            button_Control_Offset_Set.Location = new Point(208, 44);
            button_Control_Offset_Set.Name = "button_Control_Offset_Set";
            button_Control_Offset_Set.Size = new Size(63, 23);
            button_Control_Offset_Set.TabIndex = 10;
            button_Control_Offset_Set.Text = "Get";
            button_Control_Offset_Set.UseVisualStyleBackColor = true;
            // 
            // button_Control_Offset_Get
            // 
            button_Control_Offset_Get.Location = new Point(139, 44);
            button_Control_Offset_Get.Name = "button_Control_Offset_Get";
            button_Control_Offset_Get.Size = new Size(63, 23);
            button_Control_Offset_Get.TabIndex = 9;
            button_Control_Offset_Get.Text = "Get";
            button_Control_Offset_Get.UseVisualStyleBackColor = true;
            // 
            // textBox_Control_Offset
            // 
            textBox_Control_Offset.Location = new Point(62, 45);
            textBox_Control_Offset.Name = "textBox_Control_Offset";
            textBox_Control_Offset.Size = new Size(71, 23);
            textBox_Control_Offset.TabIndex = 9;
            textBox_Control_Offset.Text = "0";
            // 
            // comboBox_Control_PseudoColor
            // 
            comboBox_Control_PseudoColor.FormattingEnabled = true;
            comboBox_Control_PseudoColor.Location = new Point(102, 16);
            comboBox_Control_PseudoColor.Name = "comboBox_Control_PseudoColor";
            comboBox_Control_PseudoColor.Size = new Size(121, 23);
            comboBox_Control_PseudoColor.TabIndex = 5;
            // 
            // label_Control_Offset
            // 
            label_Control_Offset.AutoSize = true;
            label_Control_Offset.Location = new Point(6, 49);
            label_Control_Offset.Name = "label_Control_Offset";
            label_Control_Offset.Size = new Size(50, 15);
            label_Control_Offset.TabIndex = 4;
            label_Control_Offset.Text = "Offset : ";
            // 
            // label_Control_PseudoColor
            // 
            label_Control_PseudoColor.AutoSize = true;
            label_Control_PseudoColor.Location = new Point(6, 19);
            label_Control_PseudoColor.Name = "label_Control_PseudoColor";
            label_Control_PseudoColor.Size = new Size(90, 15);
            label_Control_PseudoColor.TabIndex = 3;
            label_Control_PseudoColor.Text = "Pseudo Color : ";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 437);
            Controls.Add(groupBox_Control);
            Controls.Add(groupBox_AddManual);
            Controls.Add(groupBox_SelectedDevice);
            Controls.Add(groupBox_Broadcast);
            Controls.Add(groupBox_Preview);
            Name = "MainForm";
            Text = "LK SVS SDK";
            groupBox_Preview.ResumeLayout(false);
            groupBox_Preview.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox_CmosPreview).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox_ThermalPreview).EndInit();
            groupBox_Broadcast.ResumeLayout(false);
            groupBox_Broadcast.PerformLayout();
            groupBox_SelectedDevice.ResumeLayout(false);
            groupBox_SelectedDevice.PerformLayout();
            groupBox_AddManual.ResumeLayout(false);
            groupBox_AddManual.PerformLayout();
            groupBox_Control.ResumeLayout(false);
            groupBox_Control.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox_Preview;
        private GroupBox groupBox_Broadcast;
        private GroupBox groupBox_SelectedDevice;
        private GroupBox groupBox_AddManual;
        private GroupBox groupBox_Control;
        private PictureBox pictureBox_CmosPreview;
        private PictureBox pictureBox_ThermalPreview;
        private ListView listView_Broadcast;
        private Label labell_SelectedDevice_Gateway;
        private Label labell_SelectedDevice_Port;
        private Label labell_SelectedDevice_Netmask;
        private Label labell_SelectedDevice_Ip;
        private Label label_SelectedDevice_Version;
        private Button button_SelectedDevice_Connect;
        private Button button_AddManual_Connect;
        private TextBox textBox_AddManual_Port;
        private TextBox textBox_AddManual_Ip;
        private Label label_Control_Offset;
        private Label label_Control_PseudoColor;
        private ComboBox comboBox_Control_PseudoColor;
        private Button button_Control_Offset_Get;
        private TextBox textBox_Control_Offset;
        private Button button_Control_Offset_Set;
        private Button button_Control_Disconnect;
        private Button button_Control_ActiveOnceShutter;
        private ComboBox comboBox1;
        private Label label_Broadcast_Count;
        private Label label_Broadcast_CountName;
        private Label label_Preview_MaxName;
        private Label label_Preview_Min;
        private Label label_Preview_Avrg;
        private Label label_Preview_Max;
        private Label label_Preview_MinName;
        private Label label_Preview_AvrgName;
    }
}
