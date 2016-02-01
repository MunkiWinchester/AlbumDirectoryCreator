using AlbumDirectoryCreator.Properties;
using Logic.DataObjects;
using System;
using System.Linq;
using System.Windows.Forms;

namespace AlbumDirectoryCreator.Components
{
    public partial class StarsBox : UserControl
    {
        private Stars _stars;

        public StarsBox()
        {
            InitializeComponent();
        }

        public void SetStars(Stars? stars)
        {
            _stars = stars ?? Stars.Zero;
            SetStarsInternal(_stars);
        }

        public Stars GetStars()
        {
            return _stars;
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            SetAllStars(false);
            SetStarsInternal(GetStarsInternal(sender));
        }

        private Stars GetStarsInternal(object control)
        {
            var box = control as PictureBox;
            if (box != null)
            {
                var boxNumber = int.Parse(box.Name.Last().ToString());
                switch (boxNumber)
                {
                    case 1:
                        return Stars.One;

                    case 2:
                        return Stars.Two;

                    case 3:
                        return Stars.Three;

                    case 4:
                        return Stars.Four;

                    case 5:
                        return Stars.Five;

                    default:
                        return Stars.Zero;
                }
            }
            return Stars.Zero;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            _stars = GetStarsInternal(sender);
        }

        private void SetStarsInternal(Stars stars)
        {
            SetAllStars(false);
            if (stars == Stars.Zero)
            {
                SetAllStars(false);
            }
            if (stars == Stars.One)
            {
                pictureBox1.Image = Resources.Stars_1;
            }
            if (stars == Stars.Two)
            {
                pictureBox1.Image = Resources.Stars_1;
                pictureBox2.Image = Resources.Stars_1;
            }
            if (stars == Stars.Three)
            {
                pictureBox1.Image = Resources.Stars_1;
                pictureBox2.Image = Resources.Stars_1;
                pictureBox3.Image = Resources.Stars_1;
            }
            if (stars == Stars.Four)
            {
                pictureBox1.Image = Resources.Stars_1;
                pictureBox2.Image = Resources.Stars_1;
                pictureBox3.Image = Resources.Stars_1;
                pictureBox4.Image = Resources.Stars_1;
            }
            if (stars == Stars.Five)
            {
                SetAllStars(true);
            }
        }

        private void SetAllStars(bool filled)
        {
            var image = filled ? Resources.Stars_1 : Resources.Stars_0;
            foreach (var control in Controls)
            {
                var box = control as PictureBox;
                if (box != null)
                    box.Image = image;
            }
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            SetStarsInternal(_stars);
        }
    }
}