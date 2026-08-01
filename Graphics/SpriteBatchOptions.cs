using Microsoft.Xna.Framework.Graphics;

namespace MonogameLibrary.Graphics
{
    public struct SpriteBatchOptions
    {
        public SpriteSortMode SortMode;
        public BlendState BlendState;
        public SamplerState SamplerState;
        public DepthStencilState DepthStencilState;
        public RasterizerState RasterizerState;
        public Effect Effect;


        public SpriteBatchOptions()
        {
            SortMode = SpriteSortMode.BackToFront;
            BlendState = BlendState.AlphaBlend;
            SamplerState = SamplerState.PointClamp;
            DepthStencilState = DepthStencilState.Default;
            RasterizerState = RasterizerState.CullNone;
            Effect = null;
        }

    }
}
