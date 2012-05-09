namespace Oleg_ivo.Tools.UI
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class InputBox
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prompt"></param>
        /// <param name="title"></param>
        /// <param name="defaultResponse"></param>
        /// <returns></returns>
        public static string Show(string prompt, string title, string defaultResponse)
        {
            return Microsoft.VisualBasic.Interaction.InputBox(prompt, title, defaultResponse, -1, -1);
        }
    }
}