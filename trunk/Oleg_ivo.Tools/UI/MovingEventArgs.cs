namespace Oleg_ivo.Tools.UI
{
    /// <summary>
    /// 
    /// </summary>
    public class MovingEventArgs : MovedEventArgs
    {
        /// <summary>
        /// Отменить перемещение
        /// </summary>
        public bool Cancel { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="movingObject"></param>
        public MovingEventArgs(DoubleListBoxControl.Direction direction, object movingObject) 
            : this(direction, movingObject, false)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="movingObject"></param>
        /// <param name="cancel"></param>
        public MovingEventArgs(DoubleListBoxControl.Direction direction, object movingObject, bool cancel) 
            : base(direction, movingObject)
        {
            Cancel = cancel;
        }
    }
}