using System;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Util;
using Android.Views;

namespace Mono.Samples.Snake
{

	public class TileView : View
	{
		protected static int tile_size = 80;

		protected static int x_tile_count;
		protected static int y_tile_count;

		private static int x_offset; 
		private static int y_offset;

		private Bitmap[] tile_bitmaps;
		private TileType[,] tiles;

		private Paint paint = new Paint ();

		public TileView (Context context, IAttributeSet attrs, int defStyle) :
			base (context, attrs, defStyle)
		{

		}

		public TileView (Context context, IAttributeSet attrs) :
			base (context, attrs)
		{

		}

		#region Public Methods

		public void ResetTiles (int tileCount)
		{
			tile_bitmaps = new Bitmap[tileCount];
		}

		public void LoadTile (TileType type, Drawable tile)
		{
			Bitmap bitmap = Bitmap.CreateBitmap (tile_size, tile_size, Bitmap.Config.Argb8888);
			Canvas canvas = new Canvas (bitmap);

			tile.SetBounds (0, 0, tile_size, tile_size);
			tile.Draw (canvas);

			tile_bitmaps[(int)type] = bitmap;
		}

		public void ClearTiles ()
		{
			for (int x = 0; x < x_tile_count; x++)
				for (int y = 0; y < y_tile_count; y++)
					SetTile (0, x, y);
		}

		public void SetTile (TileType tile, int x, int y)
		{
			tiles[x, y] = tile;
		}
		#endregion

		#region Protected Methods
		protected override void OnSizeChanged (int w, int h, int oldw, int oldh)
		{
			x_tile_count = (int)System.Math.Floor ((double)w / tile_size);
			y_tile_count = (int)System.Math.Floor ((double)h / tile_size);

			x_offset = ((w - (tile_size * x_tile_count)) / 2);
			y_offset = ((h - (tile_size * y_tile_count)) / 2);

			tiles = new TileType[x_tile_count, y_tile_count];

			ClearTiles ();
		}

		protected override void OnDraw (Canvas canvas)
		{
			base.OnDraw (canvas);

			for (int x = 0; x < x_tile_count; x += 1)
				for (int y = 0; y < y_tile_count; y += 1)
					if (tiles[x, y] > 0)
						canvas.DrawBitmap (tile_bitmaps[(int)tiles[x, y]],
							    x_offset + x * tile_size,
							    y_offset + y * tile_size,
							    paint);
		}
		#endregion
	}
}