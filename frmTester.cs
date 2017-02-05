using DodoIDE.Dialogs;
using FenixLib.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FenixLib.BitmapConvert;
using DodoIDE.Controls;

namespace DodoIDE
{
    public partial class frmTester : Form
    {
        public frmTester()
        {
            InitializeComponent();
        }

        private void frmTester_Load(object sender, EventArgs e)
        {
            //fontViewer1.LoadFile(@"C:\Users\Theos\Documents\Visual Studio 2015\Projects\fenixlib-master\FenixLib\TestFiles\8bpp-div-minusc.fnt");
            //paletteControl1.LoadFile(PaletteControl.DefaultPalette());
            /*
            BitmapToGraphicConverterCreator map = new BitmapToGraphicConverterCreator();
            var res = map.Create(new Bitmap(@"C:\Users\Theos\Documents\Visual Studio 2015\Projects\fenixlib-master\FenixLib\TestFiles\pig.png"));
            mapEditorControl1.LoadFile(new Sprite(res.Convert()));
            /*
            ResourseTemplateDialog dlg = new ResourseTemplateDialog();
            if (dlg.ShowDialog() == DialogResult.OK)
            {

            }*/
        }
    }
}
