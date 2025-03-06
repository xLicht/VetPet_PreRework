using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace VetPet_
{
    public partial class FormPadre : Form
    {
        protected Form1 parentForm;
        protected float originalWidth;
        protected float originalHeight;
        protected Dictionary<Control, (float width, float height, float left, float top, float fontSize)> controlInfo
            = new Dictionary<Control, (float width, float height, float left, float top, float fontSize)>();

        public FormPadre()
        {
            InitializeComponent();
            this.Load += FormPadre_Load;       // Evento Load
            this.Resize += FormPadre_Resize;   // Evento Resize
        }

        public FormPadre(Form1 parent) : this()
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void FormPadre_Load(object sender, EventArgs e)
        {
            originalWidth = this.Width;
            originalHeight = this.Height;

            StoreControlInfo(this);
        }

        private void FormPadre_Resize(object sender, EventArgs e)
        {
            ResizeControls(this);
        }

        private void StoreControlInfo(Control parent)
        {
            foreach (Control control in parent.Controls)
            {
                controlInfo[control] = (
                    control.Width,
                    control.Height,
                    control.Left,
                    control.Top,
                    control.Font.Size
                );

                if (control.HasChildren)
                {
                    StoreControlInfo(control);
                }
            }
        }

        private void ResizeControls(Control parent)
        {
            float scaleX = this.ClientSize.Width / originalWidth;
            float scaleY = this.ClientSize.Height / originalHeight;

            foreach (Control control in parent.Controls)
            {
                if (controlInfo.ContainsKey(control))
                {
                    var info = controlInfo[control];

                    control.Width = (int)(info.width * scaleX);
                    control.Height = (int)(info.height * scaleY);
                    control.Left = (int)(info.left * scaleX);
                    control.Top = (int)(info.top * scaleY);
                    
                    control.Font = new Font(control.Font.FontFamily, info.fontSize * Math.Min(scaleX, scaleY));
                }

                if (control.HasChildren)
                {
                    ResizeControls(control);
                }
            }
        }
    }
}
