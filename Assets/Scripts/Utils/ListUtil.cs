using System;
using System.Collections.Generic;

public static class ListUtil
{
	static Random rng = new Random();

    public static List<T> Shuffle<T>(this List<T> list)
	{
        List<T> newList = new List<T>();
        foreach (T e in list)
            newList.Add(e);

		int n = list.Count;
		while (n > 1)
		{
			n--;
			int k = rng.Next(n + 1);
			T value = newList[k];
			newList[k] = newList[n];
			newList[n] = value;
		}

        return newList;
	}
}