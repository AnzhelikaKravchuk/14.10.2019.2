<Query Kind="Program" />

void Main()
{

}

public static class Enumerable
{
	public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source,
		Func<TSource, bool> predicate)
	{
		throw new NotImplementedException();
	}

	public static IEnumerable<int> Range(int start, int count)
	{
		throw new NotImplementedException();
	}

	public static IEnumerable<TSource> Reverse<TSource>(this IEnumerable<TSource> source)
	{
		throw new NotImplementedException();
	}

	public static IEnumerable<TResult> Transform<TSource, TResult>(this IEnumerable<TSource> source,
		Func<TSource, TResult> transformer)
	{
		throw new NotImplementedException();
	}

	public static int Count<TSource>(this IEnumerable<TSource> source,
		Func<TSource, bool> predicate)
	{
		throw new NotImplementedException();
	}

	public static int Count<TSource>(this IEnumerable<TSource> source)
	{
		throw new NotImplementedException();
	}

	public static IEnumerable<TResult> CastTo<TResult>(IEnumerable source)
	{
		throw new NotImplementedException();
	}

	public static bool ForAll<TSource>(this IEnumerable<TSource> source,
		Func<TSource, bool> predicate)
	{
		throw new NotImplementedException();
	}

	public static IEnumerable<TSource> SortBy<TSource, TKey>(this IEnumerable<TSource> source,
		Func<TSource, TKey> key)
	{
		throw new NotImplementedException();
	}

	public static IEnumerable<TSource> SortBy<TSource, TKey>(this IEnumerable<TSource> source,
		Func<TSource, TKey> key, IComparer<TKey> comparer)
	{
		throw new NotImplementedException();
	}
}

struct BufferData<T>
{
	internal T[] items;
	internal int count;

	internal BufferData(IEnumerable<T> source)
	{
		throw new NotImplementedException();
	}

	internal T[] ToArray()
	{
		throw new NotImplementedException();
	}
}