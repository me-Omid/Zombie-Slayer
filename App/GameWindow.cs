using KWEngine3;

namespace GruppeC.App
{
    public class GameWindow : GLWindow
    {
        public GameWindow() : base(1400, 800) 
        {
			
			
            GameWorld01 gws = new GameWorld01();
            SetWorld(gws);
        }
    }
}
