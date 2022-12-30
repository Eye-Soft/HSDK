namespace EyeSoft.LinqPad.Test;

using LINQPad;

public class TestResult
{
	public TestResult(string @class, string test, string? error, long elapsed) : this(@class, test, elapsed)
	{
		Error = error;
	}

	public TestResult(string @class, string test, long elapsed)
	{
		Class = @class;
		Test = test;
		Elapsed = elapsed;
	}

	public string Class { get; }

	public string Test { get; }

	public string? Error { get; }

	public long Elapsed { get; }

#pragma warning disable IDE0051
	private object ToDump()
#pragma warning restore IDE0051
	{
		return new
		{
			Status = Error == null ? Util.Highlight("       ", "#00FF00") : Util.Highlight("       ", "#FF0000"),
			Class,
			Test,
			Error,
			Elapsed
		};
	}
}