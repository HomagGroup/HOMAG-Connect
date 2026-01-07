using System;
using System.Collections;
using System.Threading.Tasks;
using Shouldly;

namespace FluentAssertions
{
    // Minimal shim to support existing tests with Shouldly underneath.
    public static class ShouldExtensions
    {
        public static ObjectShouldWrapper<T> Should<T>(this T actual) => new(actual);
        public static TaskShouldWrapper Should(this Func<Task> actual) => new(actual);
        public static TaskShouldWrapper<T> Should<T>(this Func<Task<T>> actual) => new(actual);
    }

    // Use classes to avoid struct capture restrictions
    public class ObjectShouldWrapper<T>
    {
        private readonly T _actual;
        public ObjectShouldWrapper(T actual) { _actual = actual; }

        public void Be(T expected, string? because = null)
        {
            _actual.ShouldBe(expected, because);
        }
        public void NotBe(T unexpected, string? because = null)
        {
            _actual.ShouldNotBe(unexpected, because);
        }
        public void BeNull(string? because = null)
        {
            (_actual as object).ShouldBeNull(because);
        }
        public void NotBeNull(string? because = null)
        {
            (_actual as object).ShouldNotBeNull(because);
        }
        public void BeEmpty(string? because = null)
        {
            if (_actual is string s) s.ShouldBeEmpty(because);
            else if (_actual is IEnumerable e)
            {
                var any = e.GetEnumerator().MoveNext();
                any.ShouldBeFalse(because);
            }
        }
        public void NotBeEmpty(string? because = null)
        {
            if (_actual is string s) s.ShouldNotBeEmpty(because);
            else if (_actual is IEnumerable e)
            {
                var any = e.GetEnumerator().MoveNext();
                any.ShouldBeTrue(because);
            }
        }
        public void BeGreaterOrEqualTo<TComparable>(TComparable expected, string? because = null) where TComparable : IComparable
        {
            if (_actual is IComparable comp)
            {
                comp.CompareTo(expected).ShouldBeGreaterThanOrEqualTo(0, because);
            }
        }
        public void BeLessThanOrEqualTo<TComparable>(TComparable expected, string? because = null) where TComparable : IComparable
        {
            if (_actual is IComparable comp)
            {
                comp.CompareTo(expected).ShouldBeLessThanOrEqualTo(0, because);
            }
        }
        public void BeEquivalentTo(object expected, string? because = null)
        {
            // Approximate equivalence by equality
            _actual.ShouldBe(expected, because);
        }
        public void NotBe(object expected, string? because = null)
        {
            _actual.ShouldNotBe(expected, because);
        }
        public void NotBeNullOrEmpty(string? because = null)
        {
            if (_actual is string s) s.ShouldNotBeNullOrEmpty(because);
            else if (_actual is IEnumerable e)
            {
                var any = e.GetEnumerator().MoveNext();
                any.ShouldBeTrue(because);
            }
        }
    }

    public class TaskShouldWrapper
    {
        private readonly Func<Task> _actual;
        public TaskShouldWrapper(Func<Task> actual) { _actual = actual; }
        public async Task NotThrowAsync(string? because = null)
        {
            await Should.NotThrowAsync(_actual, because);
        }
    }

    public class TaskShouldWrapper<T>
    {
        private readonly Func<Task<T>> _actual;
        public TaskShouldWrapper(Func<Task<T>> actual) { _actual = actual; }
        public async Task NotThrowAsync(string? because = null)
        {
            var local = _actual;
            Func<Task> f = async () => { await local(); };
            await Should.NotThrowAsync(f, because);
        }
    }
}
