using System;

namespace Oleg_ivo.Tools.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class MovedEventArgs : EventArgs
    {
        /// <summary>
        /// Направление переноса
        /// </summary>
        public DoubleListBoxControl.Direction MoveDirection { get; private set; }

        /// <summary>
        /// Перемещаемый объект
        /// </summary>
        public object MovingObject { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="movingObject"></param>
        public MovedEventArgs(DoubleListBoxControl.Direction direction, object movingObject)
        {
            MoveDirection = direction;
            MovingObject = movingObject;
        }
    }
}