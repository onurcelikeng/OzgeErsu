using Windows.UI.Xaml.Controls;

namespace OzgeErsu.Helpers
{
    public class FadeHelper
    {
        public bool Out(MediaElement player)
        {
            if (player.Volume == 0)
            {
                player.Pause(); return true;
            }

            else
            {
                if (player.Volume < 0.1) player.Volume = 0;
                else player.Volume = player.Volume - 0.1;

                return false;
            }
        }

        public bool In(MediaElement player)
        {
            if (player.Volume == 1)
            {
                return true;
            }

            player.Volume = player.Volume + 0.1;
            return false;
        }
    }
}