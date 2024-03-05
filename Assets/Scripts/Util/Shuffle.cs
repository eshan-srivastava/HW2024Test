namespace Util
{
    public static class Shuffle<T>
    {
        public static void ShuffleArray(ref T[] array)
        {
            var p = array.Length;
            for (var n = p - 1; n >= 0; n--)
            {
                var r = UnityEngine.Random.Range(0, n);
                (array[r], array[n]) = (array[n], array[r]);
            }
        }
    }
}