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
	public static int Count<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
	{
		throw new NotImplementedException();
	}

	public static int Count<TSource>(this IEnumerable<TSource> source)
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

	public static IEnumerable<TResult> CastTo<TResult>(IEnumerable source)
	{
		throw new NotImplementedException();
	}

	public static bool ForAll<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
	{
		throw new NotImplementedException();
	}

}

struct BufferData<TElement>
{
	internal TElement[] items;
	internal int count;

	public BufferData(IEnumerable<TElement> source)
	{
		throw new NotImplementedException();
	}

	internal TElement[] ToArray()
	{
		throw new NotImplementedException();
	}
}



