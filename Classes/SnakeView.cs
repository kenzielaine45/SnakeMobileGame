using System;
using System.Collections.Generic;

using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Mono.Samples.Snake
{
    public class SnakeView : TileView
    {
        private static String TAG = "SnakeView";
        private static Random RNG = new Random();

        private GameMode mode = GameMode.Ready;

        private Direction mDirection = Direction.North;
        private Direction mNextDirection = Direction.North;

        private long mScore = 0;

        private int mMoveDelay = 100;

        private long mLastMove;

        private TextView mStatusText;

        private List<Coordinate> snake_trail = new List<Coordinate>();

        private List<Coordinate> apples = new List<Coordinate>();

        private RefreshHandler mRedrawHandler;

        #region Constructors
        public SnakeView(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            InitSnakeView();
            mRedrawHandler = new RefreshHandler(this);
        }

        public SnakeView(Context context, IAttributeSet attrs, int defStyle) :
            base(context, attrs, defStyle)
        {
            InitSnakeView();
            mRedrawHandler = new RefreshHandler(this);
        }

        private void InitSnakeView()
        {
            Focusable = true;
            FocusableInTouchMode = true;

            ResetTiles(4);

            LoadTile(TileType.Red, Resources.GetDrawable(Resource.Drawable.redstar));
            LoadTile(TileType.Yellow, Resources.GetDrawable(Resource.Drawable.yellowstar));
            LoadTile(TileType.Green, Resources.GetDrawable(Resource.Drawable.greenstar));

            Click += new EventHandler(SnakeView_Click);
        }
        #endregion

        #region Game Initialization
        private void InitNewGame()
        {
            snake_trail.Clear();
            apples.Clear();

            snake_trail.Add(new Coordinate(7, 7));
            snake_trail.Add(new Coordinate(6, 7));
            snake_trail.Add(new Coordinate(5, 7));
            snake_trail.Add(new Coordinate(4, 7));
            snake_trail.Add(new Coordinate(3, 7));
            snake_trail.Add(new Coordinate(2, 7));

            mNextDirection = Direction.North;

            AddRandomApple();
            AddRandomApple();

            mMoveDelay = 100;
            mScore = 0;
        }

        private void AddRandomApple()
        {
            Coordinate newCoord = null;
            bool found = false;

            while (!found)
            {
                int newX = 1 + RNG.Next(x_tile_count - 2);
                int newY = 1 + RNG.Next(y_tile_count - 2);

                newCoord = new Coordinate(newX, newY);

                bool collision = false;

                int snakelength = snake_trail.Count;
                for (int index = 0; index < snakelength; index++)
                {
                    if (snake_trail[index] == newCoord)
                    {
                        collision = true;
                    }
                }

                found = !collision;
            }

            if (newCoord == null)
                Log.Error(TAG, "Somehow ended up with a null newCoord!");

            apples.Add(newCoord);
        }

        public void SetTextView(TextView newView)
        {
            mStatusText = newView;
        }
        #endregion

        #region Game Logic
        public override bool OnKeyDown(Keycode keyCode, KeyEvent msg)
        {
            if (keyCode == Keycode.DpadUp || keyCode == Keycode.VolumeUp)
            {
                if (mode == GameMode.Ready | mode == GameMode.Lost)
                {
                    InitNewGame();

                    SetMode(GameMode.Running);
                    Update();

                    return true;
                }

                if (mode == GameMode.Paused)
                {
                    SetMode(GameMode.Running);
                    Update();

                    return true;
                }

                if (keyCode == Keycode.VolumeUp)
                {
                    mNextDirection = (Direction)(((int)mDirection + 1) % 4);
                }
                else
                {
                    if (mDirection != Direction.South)
                        mNextDirection = Direction.North;
                }

                return true;
            }

            if (keyCode == Keycode.DpadDown)
            {
                if (mDirection != Direction.North)
                    mNextDirection = Direction.South;

                return true;
            }

            if (keyCode == Keycode.VolumeDown)
            {
                mNextDirection = (Direction)(((int)mDirection - 1) % 4);

                return true;
            }

            if (keyCode == Keycode.DpadLeft)
            {
                if (mDirection != Direction.East)
                    mNextDirection = Direction.West;

                return true;
            }

            if (keyCode == Keycode.DpadRight)
            {
                if (mDirection != Direction.West)
                    mNextDirection = Direction.East;

                return (true);
            }

            return base.OnKeyDown(keyCode, msg);
        }

        private void SnakeView_Click(object sender, EventArgs e)
        {
            if (mode == GameMode.Ready | mode == GameMode.Lost)
            {
                InitNewGame();

                SetMode(GameMode.Running);
                Update();
            }
        }

        public void SetMode(GameMode newMode)
        {
            GameMode oldMode = mode;
            mode = newMode;

            if (newMode == GameMode.Running & oldMode != GameMode.Running)
            {
                mStatusText.Visibility = ViewStates.Invisible;
                Update();

                return;
            }

            var str = "";

            if (newMode == GameMode.Paused)
                str = Resources.GetText(Resource.String.mode_pause);
            else if (newMode == GameMode.Ready)
                str = Resources.GetText(Resource.String.mode_ready);
            else if (newMode == GameMode.Lost)
            {
                var lose_prefix = Resources.GetString(Resource.String.mode_lose_prefix);
                var lose_suffix = Resources.GetString(Resource.String.mode_lose_suffix);
                str = string.Format("{0}{1}{2}", lose_prefix, mScore, lose_suffix);
            }

            mStatusText.Text = str;
            mStatusText.Visibility = ViewStates.Visible;
        }

        public void Update()
        {
            if (mode == GameMode.Running)
            {
                long now = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

                if (now - mLastMove > mMoveDelay)
                {
                    ClearTiles();
                    UpdateWalls();
                    UpdateSnake();
                    UpdateApples();
                    mLastMove = now;
                }

                mRedrawHandler.Sleep(mMoveDelay);
            }
        }

        private void UpdateWalls()
        {
            for (int x = 0; x < x_tile_count; x++)
            {
                SetTile(TileType.Green, x, 0);
                SetTile(TileType.Green, x, y_tile_count - 1);
            }

            for (int y = 1; y < y_tile_count - 1; y++)
            {
                SetTile(TileType.Green, 0, y);
                SetTile(TileType.Green, x_tile_count - 1, y);
            }
        }

        private void UpdateApples()
        {
            foreach (Coordinate c in apples)
                SetTile(TileType.Yellow, c.X, c.Y);
        }

        private void UpdateSnake()
        {
            bool growSnake = false;

            Coordinate head = snake_trail[0];
            Coordinate newHead = new Coordinate(1, 1);

            mDirection = mNextDirection;

            switch (mDirection)
            {
                case Direction.East:
                    newHead = new Coordinate(head.X + 1, head.Y);
                    break;
                case Direction.West:
                    newHead = new Coordinate(head.X - 1, head.Y);
                    break;
                case Direction.North:
                    newHead = new Coordinate(head.X, head.Y - 1);
                    break;
                case Direction.South:
                    newHead = new Coordinate(head.X, head.Y + 1);
                    break;
            }

            if ((newHead.X < 1) || (newHead.Y < 1) || (newHead.X > x_tile_count - 2)
                || (newHead.Y > y_tile_count - 2))
            {
                SetMode(GameMode.Lost);

                return;
            }

            foreach (Coordinate snake in snake_trail)
            {
                if (snake.Equals(newHead))
                {
                    SetMode(GameMode.Lost);
                    return;
                }
            }

            foreach (Coordinate apple in apples)
            {
                if (apple.Equals(newHead))
                {
                    apples.Remove(apple);
                    AddRandomApple();

                    mScore++;
                    Log.Info("tag", mMoveDelay.ToString());
                    mMoveDelay = (int)(mMoveDelay * 0.9);
                    Log.Info("tag", mMoveDelay.ToString());

                    growSnake = true;

                    break;
                }
            }

            snake_trail.Insert(0, newHead);

            if (!growSnake)
                snake_trail.RemoveAt(snake_trail.Count - 1);

            int index = 0;

            foreach (Coordinate c in snake_trail)
            {
                if (index == 0)
                    SetTile(TileType.Green, c.X, c.Y);
                else
                    SetTile(TileType.Red, c.X, c.Y);

                index++;
            }
        }
        #endregion

        #region Save/Load State

        public Bundle SaveState()
        {
            Bundle map = new Bundle();

            map.PutIntArray("mAppleList", Coordinate.ListToArray(apples));
            map.PutInt("mDirection", (int)mDirection);
            map.PutInt("mNextDirection", (int)mNextDirection);
            map.PutInt("mMoveDelay", mMoveDelay);
            map.PutLong("mScore", mScore);
            map.PutIntArray("mSnakeTrail", Coordinate.ListToArray(snake_trail));

            return map;
        }

        public void RestoreState(Bundle icicle)
        {
            SetMode(GameMode.Paused);

            apples = Coordinate.ArrayToList(icicle.GetIntArray("mAppleList"));
            mDirection = (Direction)icicle.GetInt("mDirection");
            mNextDirection = (Direction)icicle.GetInt("mNextDirection");
            mMoveDelay = icicle.GetInt("mMoveDelay");
            mScore = icicle.GetLong("mScore");
            snake_trail = Coordinate.ArrayToList(icicle.GetIntArray("mSnakeTrail"));
        }
        #endregion
    }
}