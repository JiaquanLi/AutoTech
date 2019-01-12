using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoTech
{
    public partial class FormClibration : Form
    {

        double dImgX1;
        double dImgY1;
        double dImgX2;
        double dImgY2;

        double dMechX1;
        double dMechY1;
        double dMechX2;
        double dMechY2;

        public FormClibration()
        {
            InitializeComponent();
        }

        private void btn_Clibration_Click(object sender, EventArgs e)
        {
            double dAx, dBx;
            double dAy, dBy;
            string strResult;

            if (double.TryParse(tb_ImgX1.Text, out dImgX1) == false)
            {

            }
            if (double.TryParse(tb_ImgY1.Text, out dImgY1) == false)
            {

            }
            if (double.TryParse(tb_ImgX2.Text, out dImgX2) == false)
            {

            }
            if (double.TryParse(tb_ImgY2.Text, out dImgY2) == false)
            {

            }
            if (double.TryParse(tb_MechX1.Text, out dMechX1) == false)
            {

            }
            if (double.TryParse(tb_MechY1.Text, out dMechY1) == false)
            {

            }
            if (double.TryParse(tb_MechX2.Text, out dMechX2) == false)
            {

            }
            if (double.TryParse(tb_MechY2.Text, out dMechY2) == false)
            {

            }


            dAx = (dMechX1 - dMechX2) / (dImgX1 - dImgX2);
            dBx = dMechX1 - dAx * dImgX1;

            dAy = (dMechY1 - dMechY2) / (dImgY1 - dImgY2);
            dBy = dMechY1 - dAy * dImgY1;

            strResult = string.Format("ax: {0}  bx: {1}\r\n ay: {2}  by: {3}", dAx, dBx, dAy, dBy);

            tb_Result.Text = strResult;


        }
    }
}
