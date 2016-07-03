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

        private void SetStarsInternal(Stars stars)
        {
            SetAllStars(false);
            if (stars == Stars.Zero)
            {
                SetAllStars(false);
            }
            if (stars == Stars.One)
            {
                pictureBox1.Image = Resources.Stars_Yellow;
            }
            if (stars == Stars.Two)
            {
                pictureBox1.Image = Resources.Stars_Yellow;
                pictureBox2.Image = Resources.Stars_Yellow;
            }
            if (stars == Stars.Three)
            {
                pictureBox1.Image = Resources.Stars_Yellow;
                pictureBox2.Image = Resources.Stars_Yellow;
                pictureBox3.Image = Resources.Stars_Yellow;
            }
            if (stars == Stars.Four)
            {
                pictureBox1.Image = Resources.Stars_Yellow;
                pictureBox2.Image = Resources.Stars_Yellow;
                pictureBox3.Image = Resources.Stars_Yellow;
                pictureBox4.Image = Resources.Stars_Yellow;
            }
            if (stars == Stars.Five)
            {
                SetAllStars(true);
            }
        }

        private void SetAllStars(bool filled)
        {
            var image = filled ? Resources.Stars_Yellow : Resources.Stars_Grey;
            foreach (var box in Controls.OfType<PictureBox>())
            {
                box.Image = image;
            }
        }

        private static Stars GetStarsInternal(object control)
        {
            var box = control as PictureBox;
            if (box == null) return Stars.Zero;

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

        private void pictureBox_Click(object sender, EventArgs e)
        {
            _stars = GetStarsInternal(sender);
        }

        private void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            SetStarsInternal(_stars);
        }

        private void pictureBox_MouseHover(object sender, EventArgs e)
        {
            SetAllStars(false);
            SetStarsInternal(GetStarsInternal(sender));
        }
    }
}