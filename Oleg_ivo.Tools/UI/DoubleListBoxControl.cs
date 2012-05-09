using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Oleg_ivo.Tools.UI
{
    /// <summary>
    /// 
    /// </summary>
    public partial class DoubleListBoxControl : UserControl
    {
        private IList _left;
        private IList _right;

        /// <summary>
        /// 
        /// </summary>
        public DoubleListBoxControl()
        {
            InitializeComponent();
        }

        #region обработка событий
        private void btnMoveToLeft_Click(object sender, EventArgs e)
        {
            MoveToLeft(true);
        }

        private void btnMoveToRight_Click(object sender, EventArgs e)
        {
            MoveToRight(true);
        }

        private void btnMoveToLeftAll_Click(object sender, EventArgs e)
        {
            MoveToLeft(false);
        }

        private void btnMoveToRightAll_Click(object sender, EventArgs e)
        {
            MoveToRight(false);
        }

        private void lbLeft_DoubleClick(object sender, EventArgs e)
        {
            MoveToRight(true);
        }

        private void lbRight_DoubleClick(object sender, EventArgs e)
        {
            MoveToLeft(true);
        }

        private void lbLeft_SelectedValueChanged(object sender, EventArgs e)
        {
            btnMoveToRight.Enabled = btnMoveToRightAll.Enabled = lbLeft.SelectedItems.Count > 0;
        }

        private void lbRight_SelectedValueChanged(object sender, EventArgs e)
        {
            btnMoveToLeft.Enabled = btnMoveToLeftAll.Enabled = lbRight.SelectedItems.Count > 0;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public IList SelectionLeft
        {
            get { return lbLeft.SelectedItems; }
        }

        /// <summary>
        /// 
        /// </summary>
        public IList SelectionRight
        {
            get { return lbRight.SelectedItems; }
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler SelectionLeftChanged
        {
            add { lbLeft.SelectedIndexChanged += value; }
            remove { lbLeft.SelectedIndexChanged += value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler SelectionRightChanged
        {
            add { lbRight.SelectedIndexChanged += value; }
            remove { lbRight.SelectedIndexChanged += value; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public void InitSources(IList left,IList right)
        {
            if (left == null && right == null) throw new Exception("Должен быть указан хотя бы один из двух списков!");

/*
            if(left==null)
            {
                Type type = right.GetType();
                InterfaceMapping interfaceMapping = type.GetInterfaceMap(typeof(IList));
                if(interfaceMapping.)
                ConstructorInfo constructorInfo = type.GetConstructor(Type.EmptyTypes);
                object obj = constructorInfo.Invoke(null);
                right = obj;
            }
*/

            if (SourceLeft != left) _left = left;
            if (SourceRight != right) _right = right;

            lbLeft.Items.Clear();
            lbLeft.Items.AddRange(_left.OfType<object>().ToArray());

            lbRight.Items.Clear();
            lbRight.Items.AddRange(_right.OfType<object>().ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        public IList SourceLeft
        {
            get { return _left; }
        }

        /// <summary>
        /// 
        /// </summary>
        public IList SourceRight
        {
            get { return _right as IList; }
        }

        private void MoveToLeft(bool onlySelected)
        {
            IEnumerable<object> list = onlySelected ? lbRight.SelectedItems.OfType<object>() : lbRight.Items.OfType<object>();
            MoveFromSourceToTarget(list, Direction.RightToLeft);
        }

        private void MoveToRight(bool onlySelected)
        {
            IEnumerable<object> list = onlySelected ? lbLeft.SelectedItems.OfType<object>() : lbLeft.Items.OfType<object>();
            MoveFromSourceToTarget(list, Direction.LeftToRight);
        }

        private void MoveFromSourceToTarget(IEnumerable<object> list, Direction direction)
        {
            IList source = direction == Direction.LeftToRight ? SourceLeft : SourceRight;
            IList target = direction == Direction.LeftToRight ? SourceRight : SourceLeft;

            try
            {
                SuspendLayout();

                foreach (object movingObject in list.ToArray())
                    if (OnMoving(movingObject, direction))
                    {
                        target.Add(movingObject);
                        source.Remove(movingObject);

                        OnMoved(movingObject, direction);
                    }

                InitSources(SourceLeft, SourceRight);
            }
            finally 
            {
                ResumeLayout(false);
            }
        }

        /// <summary>
        /// Возвращает <see langword="true"/>, если все согласны с перемещением
        /// </summary>
        /// <param name="movingObject"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        private bool OnMoving(object movingObject, Direction direction)
        {
            MovingEventArgs eventArgs = new MovingEventArgs(direction, movingObject);
            InvokeItemMoving(eventArgs);
            return !eventArgs.Cancel;
        }

        private void OnMoved(object movedObject, Direction direction)
        {
            InvokeItemMoved(new MovedEventArgs(direction, movedObject));
        }

        private void InvokeItemMoving(MovingEventArgs e)
        {
            EventHandler<MovingEventArgs> handler = ItemMoving;
            if (handler != null) handler(this, e);
        }

        private void InvokeItemMoved(MovedEventArgs e)
        {
            EventHandler<MovedEventArgs> handler = ItemMoved;
            if (handler != null) handler(this, e);
        }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<MovingEventArgs> ItemMoving;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<MovedEventArgs> ItemMoved;

        /// <summary>
        /// Направление переноса
        /// </summary>
        public enum Direction
        {
            RightToLeft = 0,
            LeftToRight = 1
        }

    }

}
