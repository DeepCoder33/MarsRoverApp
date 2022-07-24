namespace Domain.ValueTypes
{
    public class SurfaceSize
    {
        public int Height;
        public int Width;

        public SurfaceSize(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}