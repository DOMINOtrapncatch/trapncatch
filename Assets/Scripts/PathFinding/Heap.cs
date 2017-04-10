using UnityEngine;
using System;
using System.Collections;

public class Heap<T> where T : IHeapItem<T> {

	T[] items;
	int currentItemCount;

	public Heap(int maxHeapSize)
	{
		items = new T[maxHeapSize];
	}

	public void Add(T item)
	{
		// Add item to the last position of the heap
		item.HeapIndex = currentItemCount;
		items[currentItemCount] = item;

		// Sort down the added item
		SortUp(item);

		currentItemCount++;
	}

	public T RemoveFirst()
	{
		// Remove first item
		T firstItem = items[0];
		currentItemCount--;

		// Replace first item by last
		items[0] = items[currentItemCount];
		items[0].HeapIndex = 0;

		// Sort down the replaced item
		SortDown(items[0]);

		return firstItem;
	}

	public void UpdateItem(T item)
	{
		SortUp(item);
		// We do not need to sort down because of the A* implem
	}

	public int Count { get { return currentItemCount; } }

	public bool Contains(T item)
	{
		return Equals(items[item.HeapIndex], item);
	}

	public void SortDown(T item)
	{
		while(true)
		{
			int childIndexLeft = item.HeapIndex * 2 + 1;
			int childIndexRight = item.HeapIndex * 2 + 2;
			int swapIndex = 0;

			if(childIndexLeft < currentItemCount) // If a child exists on the left
			{
				swapIndex = childIndexLeft;

				if(childIndexRight < currentItemCount)  // If a child exists on the right
				{
					if(items[childIndexLeft].CompareTo(items[childIndexRight]) < 0) // If left child is lower than right child
					{
						swapIndex = childIndexRight;
					}
				}

				if(item.CompareTo(items[swapIndex]) < 0) // If the added item is greater than the lower of his childs, swap it
				{
					Swap(item, items[swapIndex]);
				}
				else // Parent correctly placed
				{
					return;
				}
			}
			else // No children => Parent correctly placed
			{
				return;
			}
		}
	}

	public void SortUp(T item)
	{
		int parentIndex = (item.HeapIndex - 1) / 2;

		while(true)
		{
			// Getting parent
			T parentItem = items[parentIndex];

			// CompareTo:
			//    - Higher priority: 1
			//    - Same priority: 0
			//    - Lower priority: -1
			if (item.CompareTo(parentItem) > 0) // If parent is more than item, swap them
			{
				Swap(item, parentItem);
			}
			else
			{
				break;
			}

			// Searching for further parent
			parentIndex = (item.HeapIndex - 1) / 2;
		}
	}

	public void Swap(T itemA, T itemB)
	{
		// Swap items
		items[itemA.HeapIndex] = itemB;
		items[itemB.HeapIndex] = itemA;

		// Swap indexes
		int itemAIndex = itemA.HeapIndex;
		itemA.HeapIndex = itemB.HeapIndex;
		itemB.HeapIndex = itemAIndex;
	}
}

public interface IHeapItem<T> : IComparable<T> {
	int HeapIndex { get; set; }
}