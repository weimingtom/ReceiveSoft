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
    public partial class _108 : UserControl, ICommandControl
    {
        public _108()
        {
            InitializeComponent();
        }

        public string[] GetCommand(string[] Stcds, string NFOINDEX, out string CommandCode)
        {
            CommandCode = "";
            return null;
        }
    }
}
