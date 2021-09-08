using System.Numerics;
using BLITZZ;
using BLITZZ.Content;
using BLITZZ.Gfx;

using var blitz = new Blitzz();

var game = new SpritesExample();

blitz.LoadContent = game.Load;
blitz.Update = game.Update;
blitz.Draw = game.Draw;


blitz.Run();

internal class SpritesExample
{
    private SpriteBatch spriteBatch;
    private Texture texture;
    private Texture texture2;
    private Pixmap pixmap;

    public void Load()
    {
        spriteBatch = new SpriteBatch();
        texture = Assets.Get<Texture>("logo1");

        pixmap = Pixmap.Create(320, 240, Color.Red);

        Blitter.Begin(pixmap);

        Blitter.SetColor(Color.Yellow);
        Blitter.FillRect(0, 0, 160, 120);
        Blitter.FillRect(160, 120, 160, 120);

        Blitter.End();

        texture2 = Texture.Create(pixmap);
    }

    public void Update(float dt) {}

    public void Draw()
    {
        spriteBatch.Begin();
        
        spriteBatch.Draw(texture, new Vector2(10f, 10f), Color.White);
        
        spriteBatch.Draw(texture2, new Vector2(800/2.0f - texture.Width/2.0f, 600/2.0f - texture.Height/2.0f), Color.White);

        spriteBatch.End();

    }
}