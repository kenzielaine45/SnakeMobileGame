using System;
using System.Collections.Generic;

namespace Mono.Samples.Snake
{
	public class Coordinate
	{
		public int X { get; set; }
		public int Y { get; set; }

		public Coordinate (int x, int y)
		{
			X = x;
			Y = y;
		}

		public override bool Equals (object obj)
		{
			Coordinate other = (Coordinate)obj;

			return X == other.X && Y == other.Y;
		}

		public override String ToString ()
		{
			return string.Format ("Coordinate: [{0}, {1}]", X, Y);
		}

		public static int[] ListToArray (List<Coordinate> list)
		{
			List<int> array = new List<int> ();

			foreach (Coordinate c in list) {
				array.Add (c.X);
				array.Add (c.Y);
			}

			return array.ToArray ();
		}

		public static List<Coordinate> ArrayToList (int[] array)
		{
			List<Coordinate> list = new List<Coordinate> ();

			for (int index = 0; index < array.Length; index += 2)
				list.Add (new Coordinate (array[index], array[index + 1]));

			return list;
		}
	}
}
