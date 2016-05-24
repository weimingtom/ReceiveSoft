﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace YYApp.CommandControl
{
    public partial class _16 : UserControl, ICommandControl
    {
        public _16()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        public string[] GetCommand(string[] Stcds, string NFOINDEX, out string CommandCode)
        {
            CommandCode = "";
            YanYu.WRIMR.Protocol.Pack P = new YanYu.WRIMR.Protocol.Pack();
            string[] commands = null;

            if (rb1.Checked)
            {
                string sjy = Validate();
                if (sjy == null)
                {
                    DevComponents.DotNetBar.MessageBoxEx.Show("输入剩余水量报警值有误！", "[提示]", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return null;
                }

                int gnm = 0x16;
                CommandCode = "16";
                commands = new string[Stcds.Length];
                for (int i = 0; i < Stcds.Length; i++)
                {
                    var RTU = from rtu in ExecRTUList.Lrdm where rtu.STCD == Stcds[i] select rtu;

                    byte[] b = P.pack(Stcds[i], 0, 0, gnm, sjy, int.Parse(RTU.First().PWD));

                    commands[i] = YanYu.WRIMR.Protocol.PackageHelper.ByteToHexStr(b);
                }
            }
            else 
            {
                commands = new string[Stcds.Length];
                int gnm = 0x56;
                CommandCode = "56";
                for (int i = 0; i < Stcds.Length; i++)
                {
                    var RTU = from rtu in ExecRTUList.Lrdm where rtu.STCD == Stcds[i] select rtu;

                    byte[] b = P.pack(Stcds[i], 0, 0, gnm, "", int.Parse(RTU.First().PWD));

                    commands[i] = YanYu.WRIMR.Protocol.PackageHelper.ByteToHexStr(b);
                }
            }
            return commands;
        }

        private string Validate()
        {
            int A1 = 0;
            if (int.TryParse(tb1.Text.Trim(), out A1))
            {
                if (A1 >= 0 && A1 <= 999999)
                {
                    return tb1.Text.Trim();
                }
            }

            return null;
        }

        private void rb2_CheckedChanged(object sender, EventArgs e)
        {
            if (rb2.Checked)
            {
                foreach (var item in groupBox1.Controls)
                {
                    (item as Control).Enabled = false;
                }
            }
            else
            {
                foreach (var item in groupBox1.Controls)
                {
                    (item as Control).Enabled = true;
                }
            }
        }
    }
}